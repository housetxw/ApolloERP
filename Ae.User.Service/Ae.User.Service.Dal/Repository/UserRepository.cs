using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.User.Service.Common.Constant;
using Ae.User.Service.Common.Exceptions;
using Ae.User.Service.Common.Extension;
using Ae.User.Service.Core.Enums;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public class UserRepository : AbstractRepository<UserDO>, IUserRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<UserRepository> logger;
        private readonly string className;

        public UserRepository(ApolloErpLogger<UserRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<UserDO>, int>> GetUserListAsync(GetUserListRequest request)
        {
            var para = new DynamicParameters();
            para.Add("@userId", request.UserId);
            para.Add("@userName", $"%{ request.UserName}%");
            para.Add("@userTel", request.UserTel);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 0=0");
            if (!string.IsNullOrEmpty(request.UserId))
            {
                condition.Append(" AND id = @userId");
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                condition.Append(" AND (user_name LIKE @userName OR nick_name LIKE @userName)");
            }

            if (!string.IsNullOrEmpty(request.UserTel))
            {
                condition.Append(" AND mobile_number = @userTel");
            }

            var pageResult = await GetListPagedAsync<UserDO>(request.PageIndex, request.PageSize, condition.ToString(),
                "create_time desc", para);

            return new Tuple<IEnumerable<UserDO>, int>(pageResult.Items, pageResult.TotalItems);
        }

        /// <summary>
        /// 批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<UserDO>> GetUsersByUserIds(List<string> userIds)
        {
            var para = new DynamicParameters();

            para.Add("@userIds", userIds);

            var result = await GetListAsync<UserDO>("WHERE id IN @userIds", para);

            return result?.ToList() ?? new List<UserDO>();
        }

        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDO> GetUserInfo(string userId)
        {
            return await GetAsync<UserDO>(userId);
        }

        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="userTel"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserDO> GetUserInfoByUserTel(string userTel, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@mobileNumber", userTel);
            var result = await GetListAsync<UserDO>("WHERE mobile_number = @mobileNumber", para, !readOnly);
            return result?.FirstOrDefault();
        }

        public async Task<List<UserDO>> GetCommonUserInfo(List<string> userIds, string employeeId, string searchContent, string userName, string userTel)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            if (userIds != null && userIds.Any())
            {
                condition.Append(" AND `id` IN @userIds");
                para.Add("@userIds", userIds);
            }

            if (!string.IsNullOrEmpty(employeeId))
            {
                condition.Append(" AND `referrer_user_id` = @employeeId");
                para.Add("@employeeId", employeeId);
            }

            if (!string.IsNullOrEmpty(searchContent))
            {
                condition.Append(" AND (`mobile_number` = @userTel OR `user_name` LIKE @userName)");
                para.Add("@userTel", searchContent);
                para.Add("@userName", $"%{searchContent}%");
            }

            if (!string.IsNullOrEmpty(userName))
            {
                condition.Append(" AND `user_name` LIKE @user_name");
                para.Add("@user_name", $"%{userName}%");
            }

            if (!string.IsNullOrEmpty(userTel))
            {
                condition.Append(" AND `mobile_number` = @mobile_number");
                para.Add("@mobile_number", userTel);
            }

            string conditionStr = condition.ToString();
            if (conditionStr.Length == 0)
            {
                return new List<UserDO>();
            }

            conditionStr = "WHERE 0 = 0" + conditionStr;

            var result = await GetListAsync<UserDO>(conditionStr, para);

            return result?.ToList() ?? new List<UserDO>();
        }

        public async Task<PagedEntity<UserDO>> SearchUserInfo(SearchUserInfoCondition request)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 0 = 0");
            var para = new DynamicParameters();
            if (!string.IsNullOrEmpty(request.UserName))
            {
                condition.Append(" AND user_name LIKE @userName");
                para.Add("@userName", $"%{request.UserName}%");
            }

            if (!string.IsNullOrEmpty(request.UserTel))
            {
                condition.Append(" AND mobile_number = @userTel");
                para.Add("@userTel", request.UserTel);
            }

            var result = await GetListPagedAsync<UserDO>(request.PageIndex, request.PageSize, condition.ToString(),
                "create_time DESC", para);

            return result;
        }

        public async Task<bool> EditUserInfo(UserDO userDo)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userDo.Id);
            para.Add("@updateBy", userDo.UpdateBy);
            StringBuilder condition = new StringBuilder();

            #region 更新字段

            if (!string.IsNullOrEmpty(userDo.UserName))
            {
                condition.Append("user_name = @user_name,");
                para.Add("@user_name", userDo.UserName);
            }

            if (!string.IsNullOrEmpty(userDo.NickName))
            {
                condition.Append("nick_name = @nick_name,");
                para.Add("@nick_name", userDo.NickName);
            }

            if (!string.IsNullOrEmpty(userDo.HeadUrl))
            {
                condition.Append("head_url = @head_url,");
                para.Add("@head_url", userDo.HeadUrl);
            }

            if (userDo.Gender == 1 || userDo.Gender == 2)
            {
                condition.Append("gender = @gender,");
                para.Add("@gender", userDo.Gender);
            }

            if (userDo.BirthDay > new DateTime(1900, 1, 1))
            {
                condition.Append("birth_day = @birth_day,");
                para.Add("@birth_day", userDo.BirthDay);
            }

            if (!string.IsNullOrEmpty(userDo.PersonalSign))
            {
                condition.Append("personal_sign = @personal_sign,");
                para.Add("@personal_sign", userDo.PersonalSign);
            }

            if (!string.IsNullOrEmpty(userDo.MobileNumber))
            {
                condition.Append("mobile_number = @mobile_number,");
                para.Add("@mobile_number", userDo.MobileNumber);
            }


            if (userDo.DriverLicenseExpiry > new DateTime(1900, 1, 1))
            {
                condition.Append("driver_license_expiry = @driver_license_expiry,");
                para.Add("@driver_license_expiry", userDo.DriverLicenseExpiry);
            }

            if (userDo.MobileNumber != null)
            {
                condition.Append("mobile_number = @mobile_number,");
                para.Add("@mobile_number", userDo.MobileNumber);
            }

            if (userDo.IdNumber.IsNotNullOrWhiteSpace())
            {
                condition.Append("id_number = @IdNumber,");
                para.Add("@IdNumber", userDo.IdNumber);
            }
            if (userDo.ContactAddress.IsNotNullOrWhiteSpace())
            {
                condition.Append("contact_address = @ContactAddress,");
                para.Add("@ContactAddress", userDo.ContactAddress);
            }
        
            #endregion

            string sql = condition.ToString();

            if (!string.IsNullOrEmpty(sql))
            {
                sql =
                    $"UPDATE `user` SET {sql}update_by = @updateBy,update_time = NOW( ) WHERE id = @userId AND is_deleted = 0";

                int id = 0;
                await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
                return id > 0;
            }

            return false;
        }


        // ---------------------------------- 推荐信息相关逻辑 --------------------------------------

        /// <summary>
        /// 获取用户分享获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<int> GetReferrerCustomerNum(ReferrerCustomerRequest req)
        {
            string sql = @"select count(1)  from `user`
                                  WHERE channel = 2  AND referrer_user_id = @ReferrerUserId";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new
                {
                    ReferrerUserId = req.UserId,              
                });
            });
            return count;
        }

        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagedEntity<UserDO>> GetReferrerCustomerList(ReferrerCustomerListRequest req)
        {
            var para = new DynamicParameters();

            para.Add("@ReferrerUserId", req.UserId);

            var result = await GetListPagedAsync<UserDO>(req.PageIndex, req.PageSize, "WHERE channel = 2  AND referrer_user_id = @ReferrerUserId",
                "create_time DESC", para);

            return result;

        }

        public async Task<ShopReferrerCustomerResDTO> GetShopNewCustomerInfo(ShopReferrerCustomerReqDTO req)
        {
            ShopReferrerCustomerResDTO res = new ShopReferrerCustomerResDTO();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@CurrentMonthDate", DateTimeExtension.GetFirstDayTimeOfMonth());

                    var whrClu = @" WHERE channel = 1 AND create_time >= @CurrentMonthDate
                                    AND referrer_shop_id = @ShopId ";
                    var sql = @"SELECT referrer_shop_id shopId, COUNT(referrer_shop_id) Amount FROM `user`"
                                + whrClu + " LIMIT 1;";
                    var sqlList = @"SELECT id, referrer_type referrerType, referrer_shop_id referrerShopId, IFNULL(referrer_user_id,'') referrerUserId
                                    FROM `user`" + whrClu;

                    var resTmp = conn.QueryFirstOrDefaultAsync<ShopReferrerCustomerResDTO>(sql, param);
                    var listTmp = conn.QueryAsync<UserDO>(sqlList, param);

                    Task.WaitAll(resTmp, listTmp);

                    res.ShopId = resTmp.Result.ShopId;
                    res.Amount = resTmp.Result.Amount;
                    res.UserIds.AddRange(listTmp.Result.Select(s => s.Id.ToString()).Distinct().ToList());
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        public async Task<PagedEntity<EmployeeReferrerCustomerResDTO>> GetEmployeeReferrerNewCustomerPage(EmployeeReferrerCustomerPageReqDTO req)
        {
            PagedEntity<EmployeeReferrerCustomerResDTO> res = new PagedEntity<EmployeeReferrerCustomerResDTO>();
            IEnumerable<EmployeeReferrerCustomerResDTO> enumerable = null;
            var total = 0;

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
	                                SELECT COUNT(referrer_user_id) Amount FROM `user`
                                    WHERE channel = 1 AND create_time BETWEEN @StartTime AND @EndTime
                                    AND referrer_shop_id = @ShopId
	                                GROUP BY referrer_user_id
                                ) AS t";

                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT referrer_user_id employeeId, COUNT(referrer_user_id) Amount FROM `user`
                                    WHERE channel = 1 AND create_time BETWEEN @StartTime AND @EndTime
                                    AND referrer_shop_id = @ShopId
	                                GROUP BY referrer_user_id
                                    LIMIT @index, @size";

                        var sqlList = @"SELECT id, referrer_user_id referrerUserId
                                        FROM `user`
                                        WHERE channel = 1 AND create_time BETWEEN @StartTime AND @EndTime
                                        AND referrer_shop_id = @ShopId";

                        enumerable = await conn.QueryAsync<EmployeeReferrerCustomerResDTO>(sql, param);
                        res.Items = enumerable.ToList();
                        res.TotalItems = total;

                        var resList = await conn.QueryAsync<UserDO>(sqlList, param);
                        foreach (var item in res.Items)
                        {
                            item.UserIds.AddRange(resList
                                .Where(w => w.ReferrerUserId.EqualsIgnoreCase(item.EmployeeId))
                                .Select(s => s.Id.ToString()).Distinct());
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

        public async Task<UserDO> GetUserByRelationShopId(long shopId)
        {
            UserDO result = null;

            string sql = @"SELECT
	a.id AS Id,
	a.user_name AS UserName,
	a.nick_name AS NickName,
	a.contact_name AS ContactName,
	a.gender AS Gender,
	a.mobile_number AS MobileNumber,
	a.head_url AS HeadUrl 
FROM
	`user` a
	INNER JOIN user_expand_shop b ON ( a.id = b.user_id ) 
WHERE
	b.relation_shop_id = @shopId 
	AND a.is_deleted = 0 
	AND b.is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@shopId", shopId);

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryFirstOrDefaultAsync<UserDO>(sql, para);
            });

            return result;
        }
    }
}
