using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys.ShopCustomer
{
    public class ShopUserRelationRepository : AbstractRepository<ShopUserRelationDO>, IShopUserRelationRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<ShopUserRelationRepository> logger;
        private readonly string className;

        public ShopUserRelationRepository(ApolloErpLogger<ShopUserRelationRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<ShopUserRelationDO> GetShopUserRelation(long shopId, string userId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            para.Add("@shopId", shopId);
            var result =
                await GetListAsync<ShopUserRelationDO>("WHERE user_id = @userId AND shop_id = @shopId", para,
                    !readOnly);

            return result?.OrderByDescending(_ => _.Id)?.FirstOrDefault();
        }

        /// <summary>
        /// 获取门店用户列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<ShopUserRelationDO>> GetShopUserRelationList(long shopId, List<string> userIds)
        {
            var para = new DynamicParameters();
            para.Add("@userIds", userIds);
            para.Add("@shopId", shopId);
            var result =
                await GetListAsync<ShopUserRelationDO>("WHERE shop_id = @shopId AND user_id IN @userIds", para);

            return result?.ToList() ?? new List<ShopUserRelationDO>();
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="offsetMonth"></param>
        /// <param name="memberTag"></param>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<List<ShopUserRelationDO>> GetShopUserRelationByCondition(int offsetMonth, int memberTag,
            int shopId,string employeeId)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id = @shopId");
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            if (offsetMonth > 0)
            {
                condition.Append(
                    " AND (last_arrive_time < DATE_SUB( NOW( ), INTERVAL @monthNum MONTH ) OR last_arrive_time IS NULL)");
                para.Add("@monthNum", offsetMonth);
            }

            if (memberTag > 0)
            {
                condition.Append(" AND `member_tag` = @memberTag");
                para.Add("@memberTag", memberTag);
            }

            if (!string.IsNullOrEmpty(employeeId))
            {
                condition.Append(" AND `referrer_user_id` = @employeeId");
                para.Add("@employeeId", employeeId);
            }

            var result =
                await GetListAsync<ShopUserRelationDO>(condition.ToString(), para);

            return result?.OrderByDescending(_ => _.Id)?.ToList() ?? new List<ShopUserRelationDO>();
        }

        public async Task<ShopReferrerCustomerResDTO> GetCurMonthShopNewCustomerStatisticsInfo(ShopReferrerCustomerReqDTO req)
        {
            ShopReferrerCustomerResDTO res = new ShopReferrerCustomerResDTO();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@CurrentMonthDate", DateTimeExtension.GetFirstDayTimeOfMonth());

                    var whrClu = @" WHERE create_time >= @CurrentMonthDate
                                    AND shop_id = @ShopId ";
                    var sql = @"SELECT shop_id shopId, COUNT(shop_id) Amount FROM shop_user_relation "
                                + whrClu + " LIMIT 1;";
                    var sqlList = @"SELECT id, referrer_type referrerType, shop_id ShopId, user_id UserId
                                        FROM shop_user_relation " + whrClu;

                    var resTmp = await conn.QueryFirstOrDefaultAsync<ShopReferrerCustomerResDTO>(sql, param);
                    var listTmp = await conn.QueryAsync<ShopUserRelationDO>(sqlList, param);

                    //Task.WaitAll(resTmp, listTmp);

                    res.ShopId = resTmp.ShopId;
                    res.Amount = resTmp.Amount;
                    res.UserIds.AddRange(listTmp
                        .Select(s => s.UserId.ToString())
                        .Distinct().ToList());
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        public async Task<PagedEntity<DrainageStatisticsResDTO>> GetDrainageStatisticsPage(DrainageStatisticsPageReqDTO req)
        {
            PagedEntity<DrainageStatisticsResDTO> res = new PagedEntity<DrainageStatisticsResDTO>
            {
                Items = new List<DrainageStatisticsResDTO>()
            };

            switch (req.DrainageStatisticsType)
            {
                case DrainageStatisticsType.ShareType:
                    res = await GetDrainageStatisticsPageByShareType(req);
                    break;
                default:
                    res = await GetDrainageStatisticsPageByEmployee(req);
                    break;
            }

            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private async Task<PagedEntity<DrainageStatisticsResDTO>> GetDrainageStatisticsPageByEmployee(DrainageStatisticsPageReqDTO req)
        {
            PagedEntity<DrainageStatisticsResDTO> res = new PagedEntity<DrainageStatisticsResDTO>();
            IEnumerable<DrainageStatisticsResDTO> enumerable = new List<DrainageStatisticsResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                    param.Add("@size", req.PageSize);
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@StartTime", Convert.ToDateTime(req.StartTime).GetStartTimeOfDay());
                    param.Add("@EndTime", Convert.ToDateTime(req.EndTime).GetEndTimeOfDay());

                    var sqlCount = @"SELECT COUNT(*) FROM 
                                    (
	                                    SELECT COUNT(user_id) Amount FROM shop_user_relation
	                                    WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime
	                                    GROUP BY referrer_user_id
                                    ) AS t";

                    var total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT referrer_user_id `key`, COUNT(user_id) Amount FROM shop_user_relation
                                    WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime
                                    GROUP BY referrer_user_id
                                    LIMIT @index, @size";

                        var sqlList = @"SELECT referrer_user_id referrerUserId, user_id userId FROM shop_user_relation
                                        WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime";

                        enumerable = await conn.QueryAsync<DrainageStatisticsResDTO>(sql, param);
                        res.Items = enumerable.ToList();
                        res.TotalItems = total;

                        var resList = await conn.QueryAsync<ShopUserRelationDO>(sqlList, param);
                        foreach (var item in res.Items)
                        {
                            item.UserIds.AddRange(resList
                                .Where(w => w.ReferrerUserId.EqualsIgnoreCase(item.Key))
                                .Select(s => s.UserId.ToString()).Distinct());
                        }
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        private async Task<PagedEntity<DrainageStatisticsResDTO>> GetDrainageStatisticsPageByShareType(DrainageStatisticsPageReqDTO req)
        {
            PagedEntity<DrainageStatisticsResDTO> res = new PagedEntity<DrainageStatisticsResDTO>();
            IEnumerable<DrainageStatisticsResDTO> enumerable = new List<DrainageStatisticsResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                    param.Add("@size", req.PageSize);
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@StartTime", Convert.ToDateTime(req.StartTime).GetStartTimeOfDay());
                    param.Add("@EndTime", Convert.ToDateTime(req.EndTime).GetEndTimeOfDay());

                    var sqlCount = @"SELECT COUNT(*) FROM 
                                    (
	                                    SELECT 
	                                    CASE referrer_type
					                                    WHEN 1 THEN 1
					                                    WHEN 2 THEN 2
					                                    WHEN 3 THEN 3
					                                    WHEN 6 THEN 6
				                                        WHEN 8 THEN 8
					                                    ELSE 0
				                                    END	
	                                    referrerType, COUNT(referrer_type) Amount FROM shop_user_relation
	                                    WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime
	                                    GROUP BY referrerType
                                    ) AS t";

                    var total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT
                                    CASE referrer_type
				                                    WHEN 1 THEN 1
				                                    WHEN 2 THEN 2
				                                    WHEN 3 THEN 3
				                                    WHEN 6 THEN 6
				                                    WHEN 8 THEN 8
				                                    ELSE 0
			                                    END	
                                    `key`, COUNT(user_id) Amount FROM shop_user_relation
                                    WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime
                                    GROUP BY `key`
                                    LIMIT @index, @size";

                        var sqlList = @"SELECT 
                                        CASE referrer_type
				                                        WHEN 1 THEN 1
				                                        WHEN 2 THEN 2
				                                        WHEN 3 THEN 3
				                                        WHEN 6 THEN 6
				                                        WHEN 8 THEN 8
				                                        ELSE 0
			                                        END	
                                        referrerType, user_id userId FROM shop_user_relation
                                        WHERE shop_id = @ShopId AND create_time BETWEEN @StartTime AND @EndTime";

                        enumerable = await conn.QueryAsync<DrainageStatisticsResDTO>(sql, param);
                        res.Items = enumerable.ToList();
                        res.TotalItems = total;

                        var resList = await conn.QueryAsync<ShopUserRelationDO>(sqlList, param);
                        foreach (var item in res.Items)
                        {
                            item.UserIds.AddRange(resList
                                .Where(w => w.ReferrerType.ToString() == item.Key)
                                .Select(s => s.UserId.ToString()).Distinct());
                        }
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        /// <summary>
        /// 获取技师邀请注册用户 昨日的!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopUserRelationDO>> GetShopUserRegisterList(GetShopNewCustomerRequest request)
        {
            var param = new DynamicParameters();
            string conditions = string.Empty;
            if (request.UserIds.Any())
            {
                var userIds = new StringBuilder();
                foreach (var item in request.UserIds)
                {
                    userIds.Append("'" + item + "',");
                }
                conditions += $" and referrer_user_id in ({userIds.ToString().TrimEnd(',')})";
            }
            if (request.ShopIds.Any())
            {
                conditions += $" and referrer_shop_id in ({string.Join(',', request.ShopIds)})";
            }

            string sqlWhere = " where is_deleted =0 and referrer_type=9 and  TO_DAYS( NOW( ) ) - TO_DAYS( create_time) <= 1 " + conditions;
            var result = await GetListAsync(sqlWhere);
            return result?.ToList() ?? new List<ShopUserRelationDO>();
        }

        /// <summary>
        /// 技师邀请未消费用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopUserRelationDO>> GetShopUserUnConsumes(List<long> shopIds)
        {
            var sqlWhere = " where is_first_order =0 and referrer_type=9 and is_deleted =0 ";
            if (shopIds.Any())
            {
                sqlWhere += $" and referrer_shop_id in ({string.Join(',', shopIds)}) ";
            }
            var result = await GetListAsync(sqlWhere);

            return result?.ToList() ?? new List<ShopUserRelationDO>();
        }

        /// <summary>
        /// 更新是否首销标识
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopUserFirstOrderFlag(ShopUserRelationDO request,string userId)
        {
            var sql = @"UPDATE shop_user_relation 
                        SET is_first_order = 1,
                        update_by = 'SystemTask',
                        update_time = SYSDATE( ) 
                        WHERE
	                        user_id = @user_id 
	                        AND referrer_shop_id = @referrer_shop_id 
	                        AND referrer_user_id = @referrer_user_id";

            var param = new DynamicParameters();
            param.Add("@user_id",userId);
            param.Add("@referrer_shop_id", request.ReferrerShopId);
            param.Add("@referrer_user_id", request.ReferrerUserId);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 平台汽配 - 我的客户列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopUserRelationDO>> GetShopCustomerListForQp(long shopId, int pageIndex,
            int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            var result = await GetListPagedAsync<ShopUserRelationDO>(pageIndex, pageSize, "WHERE shop_id = @shopId",
                "last_order_time DESC", para);
            return result;
        }
    }
}
