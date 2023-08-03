using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Dal.Model.Extend;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopReserveRepository : AbstractRepository<ShopReserveDO>, IShopReserveRepository
    {
        public ShopReserveRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 获取用户已预约数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetReservedCount(string userId) 
        {
            string sql = @"SELECT
	Count( * ) 
FROM
	shop_reserve 
WHERE
	user_id = @userId
    AND `status` != 3";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            para.Add("@userId", userId);

            var count = 0;
            await OpenSlaveConnectionAsync(async conn =>  
            {
                count = await conn.ExecuteScalarAsync<int>(sql,para);
            });

            return count;
        }

        /// <summary>
        /// 查询预约详情
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        public async Task<ReserveInfoDO> GetReserveInfo(long reserveId,long shopId,string orderNo = "") 
        {
            string sql = @"SELECT
	r.id,
	r.reserve_time as ReserveTime,
    r.reserve_no as ReserveNo,
    r.user_id UserId,
    r.user_name as UserName,
	r.user_tel as UserTel,
	r.is_wait as IsWait,
	r.is_any_order as IsAnyOrder,
	r.status,
	r.car_id as CarId,
	r.vin_no as VinNo,
	r.car_no as CarNo,
	r.brand,
	r.vehicle,
	r.pai_liang as PaiLiang,
	r.nian,
	r.sales_name as SalesName,
    r.car_logo CarLogo,
	r.shop_id as ShopId,
    r.tech_name TechName,
	r.service_type as ServiceCode,
	r.service_name as ServiceName,
    r.remark,
	od.order_no as OrderNO
FROM
	shop_reserve r
	LEFT JOIN shop_reserve_order od ON r.id = od.reserve_id AND od.is_deleted = 0
WHERE  r.is_deleted = 0  ";

            var para = new DynamicParameters();
            para.Add("@ReserveId", reserveId);
            para.Add("@ShopId", shopId);
            para.Add("@OrderNo", orderNo);

            StringBuilder condition = new StringBuilder();
            if (reserveId > 0)
            {
                condition.Append(" AND r.id = @ReserveId ");
            }
            if (shopId > 0) 
            {
                condition.Append(" AND r.shop_id = @ShopId");
            }

            if (!string.IsNullOrEmpty(orderNo))
            {
                condition.Append(" AND od.order_no = @OrderNo");
            }
            sql += condition.ToString();

            var result = new ReserveInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ReserveInfoDO>(sql, para)).FirstOrDefault();
            });
            return result;
        }

        /// <summary>
        /// 更新预约
        /// </summary>
        /// <param name="reserveId"></param>
        /// <param name="reserveTime"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> UpdateReserve(UpdateReserveDTO dTO)
        {
            string sql = @"UPDATE shop_reserve 
SET reserve_time = @ReserveDateTime,
update_by = @UpdateBy,
update_time = @UpdateTime,
is_wait = @IsWait";

            StringBuilder condition = new StringBuilder();
            if (dTO.ShopId > 0) 
            {
                condition.Append(" ,shop_id = @ShopId");
            }
            if (!string.IsNullOrEmpty(dTO.ServiceCode)) 
            {
                condition.Append(" ,service_code = @ServiceCode");
            }
            if (!string.IsNullOrEmpty(dTO.ServiceName))
            {
                condition.Append(" ,service_name = @ServiceName");
            }
            if (!string.IsNullOrEmpty(dTO.CarId)) 
            {
                condition.Append(" ,car_id = @CarId");
            }
            if (!string.IsNullOrEmpty(dTO.CarNo)) 
            {
                condition.Append(" ,car_no = @CarNo");
            }
            if (!string.IsNullOrEmpty(dTO.Brand))
            {
                condition.Append(" ,brand = @Brand");
            }
            if (!string.IsNullOrEmpty(dTO.Vehicle))
            {
                condition.Append(" ,vehicle = @Vehicle");
            }
            if (!string.IsNullOrEmpty(dTO.PaiLiang))
            {
                condition.Append(" ,pai_liang = @PaiLiang");
            }
            if (!string.IsNullOrEmpty(dTO.Nian))
            {
                condition.Append(" ,nian = @Nian");
            }
            if (!string.IsNullOrEmpty(dTO.SalesName))
            {
                condition.Append(" ,sales_name = @SalesName");
            }
            if (!string.IsNullOrEmpty(dTO.CarLogo))
            {
                condition.Append(" ,car_logo = @CarLogo");
            }

            condition.Append(" WHERE id = @ReserveId");

            

            int id = 0;
            sql = sql + condition.ToString();
            try
            {
                await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, dTO); });
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return id > 0;
        }
        /// <summary>
        /// 修改预约状态
        /// 状态 0待确认 1已确认 2已完成 3已取消
        /// </summary>
        /// <param name="reserveId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> UpdateReserveStatus(long reserveId, int status,string updateBy)
        {
            string sql = @"UPDATE shop_reserve 
SET `status` = @Status,
update_by = @UpdateBy,
update_time = @UpdateTime
WHERE
	id = @ReserveId";

            var dt = DateTime.Now;
            var para = new DynamicParameters();
            para.Add("@ReserveId", reserveId);
            para.Add("@Status", status);
            para.Add("@UpdateBy", updateBy);
            para.Add("@UpdateTime", dt);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserve(CancelReserveRequest request)
        {
            string sql = @"UPDATE shop_reserve 
SET `status` = 3,
cancel_time = @UpdateTime,
cancel_by = @UpdateBy,
cancel_reason = @CancleReason,
update_by = @UpdateBy,
update_time = @UpdateTime
WHERE
	id = @ReserveId";

            var dt = DateTime.Now;
            var para = new DynamicParameters();
            para.Add("@ReserveId", request.ReserveId);
            para.Add("@CancleReason", request.CancelReason);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", dt);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 查询已预约列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<GetReservedListDO> GetReserveListAsync(string userId,int pageIndex,int pageSize)
        {
            //查询数量
            string sqlCount = @"SELECT COUNT(*) FROM
	shop_reserve r ";
            //查询列表
            string sql = @"SELECT
	r.id,
	r.reserve_time as ReserveTime,
    r.reserve_no as ReserveNo,
	r.is_wait as IsWait,
	r.is_any_order as IsAnyOrder,
	r.status,
    r.type,
	r.car_id as CarId,
	r.vin_no as VinNo,
	r.car_no as CarNo,
	r.brand,
	r.vehicle,
	r.pai_liang as PaiLiang,
	r.nian,
	r.sales_name as SalesName,
    r.car_logo CarLogo,
	r.shop_id as ShopId,
	r.service_code as ServiceCode,
	r.service_name as ServiceName
FROM
	shop_reserve r";


            var para = new DynamicParameters();
            para.Add("@UserId", userId);

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE r.is_deleted = 0 AND r.user_id = @UserId");

            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            GetReservedListDO result = new GetReservedListDO();
            IEnumerable<ReserveInfoDO> ReserveList = new List<ReserveInfoDO>();

            if (count > 0)
            {
                var Offset = (pageIndex - 1) * pageSize;

                para.Add("@Offset", Offset);
                para.Add("@PageSize", pageSize);
                condition.Append(" ORDER BY r.id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                try
                {
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        ReserveList = await conn.QueryAsync<ReserveInfoDO>(sql, para);
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                result.List = ReserveList.ToList();
            }

            result.TotalItems = count;
            return result;
        }

        /// <summary>
        /// 预约列表V2
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<GetReservedListDO> GetReserveListV2Async(string userId, int pageIndex, int pageSize,int type)
        {
            //查询数量
            string sqlCount = @"SELECT COUNT(*) FROM
	shop_reserve r
	LEFT JOIN shop_reserve_order od ON r.id = od.reserve_id";
            //查询列表
            string sql = @"SELECT
	r.id,
	r.reserve_time as ReserveTime,
    r.reserve_no as ReserveNo,
    r.status,
	r.car_id as CarId,
	r.vin_no as VinNo,
	r.car_no as CarNo,
	r.brand,
	r.vehicle,
	r.pai_liang as PaiLiang,
	r.nian,
	r.sales_name as SalesName,
    r.car_logo CarLogo,
	r.shop_id as ShopId,
	r.service_code as ServiceCode,
	r.service_name as ServiceName,
	od.order_no as OrderNO
FROM
	shop_reserve r
	LEFT JOIN shop_reserve_order od ON r.id = od.reserve_id ";


            var para = new DynamicParameters();
            para.Add("@UserId", userId);

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE r.user_id = @UserId");
            
            if (type == 1)
            {
                condition.Append(" AND r.status = 1");
            }
            else 
            {
                condition.Append(" AND r.status IN (2,3) ");
            }

            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            GetReservedListDO result = new GetReservedListDO();
            IEnumerable<ReserveInfoDO> ReserveList = new List<ReserveInfoDO>();

            if (count > 0)
            {
                var Offset = (pageIndex - 1) * pageSize;

                para.Add("@Offset", Offset);
                para.Add("@PageSize", pageSize);
                condition.Append(" ORDER BY r.id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ReserveList = await conn.QueryAsync<ReserveInfoDO>(sql, para);
                });
                result.List = ReserveList.ToList();
            }

            result.TotalItems = count;
            return result;
        }

        /// <summary>
        /// 获取门店一天各个时间点预约数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeNumDO>> GetReservedCountByShopId(long shopId,DateTime reserveTime)
        {
            string sql = @"SELECT
reserve_time as ReserveTime,
Count( * ) as ReserveCount
FROM
	shop_reserve 
WHERE
	shop_id = @ShopId
    AND `status` != 3 AND reserve_time >= @ReserveTimeStart 
	AND reserve_time < @ReserveTimeEnd
GROUP BY reserve_time";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            para.Add("@ShopId", shopId);
            para.Add("@ReserveTimeStart", reserveTime);
            para.Add("@ReserveTimeEnd", reserveTime.AddDays(1));
            IEnumerable<ReserveTimeNumDO> ReserveList = new List<ReserveTimeNumDO>();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ReserveList = await conn.QueryAsync<ReserveTimeNumDO>(sql, para);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return ReserveList.ToList();
        }

        /// <summary>
        /// 获取门店一天已预约数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeNumDO>> GetReservedCountDay(long shopId, DateTime startTime, DateTime endTime)
        {
            string sql = @"SELECT
 DATE_FORMAT(reserve_time,'%Y-%m-%d') as ReserveDate,
Count( * ) as ReserveCount
FROM
	shop_reserve 
WHERE
	shop_id = @ShopId
    AND `status` != 3 AND reserve_time >= @ReserveTimeStart 
	AND reserve_time < @ReserveTimeEnd
GROUP BY ReserveDate";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            para.Add("@ShopId", shopId);
            para.Add("@ReserveTimeStart", startTime);
            para.Add("@ReserveTimeEnd", endTime);
            IEnumerable<ReserveTimeNumDO> ReserveList = new List<ReserveTimeNumDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ReserveList = await conn.QueryAsync<ReserveTimeNumDO>(sql, para);
            });
            return ReserveList.ToList();
        }

        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<int> GetShopTotalReserve(long shopId)
        {
            string sql = @"SELECT
	Count( * ) 
FROM
	shop_reserve 
WHERE
	shop_id = @ShopId
    AND `status` != 3";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            para.Add("@ShopId", shopId);

            var count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, para);
            });

            return count;
        }

        /// <summary>
        /// 查询用户当天预约信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ReserveInfoDO> GetSameDayReserveSimpleInfo(string userId, long shopId,string carId)
        {
            string sql = @"SELECT
	r.id,
	r.reserve_time as ReserveTime,
    r.reserve_no as ReserveNo,
    r.user_id UserId,
    r.user_name as UserName,
	r.user_tel as UserTel,
	r.is_wait as IsWait,
	r.is_any_order as IsAnyOrder,
	r.status,
	r.car_id as CarId,
	r.vin_no as VinNo,
	r.car_no as CarNo,
	r.brand,
	r.vehicle,
	r.pai_liang as PaiLiang,
	r.nian,
	r.sales_name as SalesName,
    r.car_logo CarLogo,
	r.shop_id as ShopId,
    r.tech_name TechName,
	r.service_code as ServiceCode,
	r.service_name as ServiceName,
    r.remark,
	od.order_no as OrderNO
FROM
	shop_reserve r
	LEFT JOIN shop_reserve_order od ON r.id = od.reserve_id 
WHERE
	r.status != 3 AND r.car_id=@CarId AND r.shop_id=@ShopId AND r.is_deleted = 0 AND r.reserve_time >= @ReserveTimeStart 
	AND r.reserve_time < @ReserveTimeEnd AND r.user_id =@UserId  order by r.reserve_time desc";

            DateTime reserveTime = DateTime.Today;

            var para = new DynamicParameters();
            para.Add("@UserId", userId);
            para.Add("@ShopId", shopId);
            para.Add("@CarId", carId);
            para.Add("@ReserveTimeStart", reserveTime);
            para.Add("@ReserveTimeEnd", reserveTime.AddDays(1));


            var result = new ReserveInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ReserveInfoDO>(sql, para)).FirstOrDefault();
            });
            return result;
        }


        /// <summary>
        /// 查询用户某一天是否有预约
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ReserveInfoDO> GetTheDayReserveeInfo(string userId,int reserveValue,string carId,long shopId)
        {
            string sql = @"SELECT
	r.id,
	r.reserve_time as ReserveTime,
    r.reserve_no as ReserveNo,
    r.user_id UserId,
    r.user_name as UserName,
	r.user_tel as UserTel,
	r.is_wait as IsWait,
	r.is_any_order as IsAnyOrder,
	r.status,
	r.car_id as CarId,
	r.vin_no as VinNo,
	r.car_no as CarNo,
	r.brand,
	r.vehicle,
	r.pai_liang as PaiLiang,
	r.nian,
	r.sales_name as SalesName,
    r.car_logo CarLogo,
	r.shop_id as ShopId,
    r.tech_name TechName,
	r.service_code as ServiceCode,
	r.service_name as ServiceName,
    r.remark
FROM
	shop_reserve r
WHERE
	r.status != 3 AND r.is_deleted = 0 AND r.shop_id=@ShopId  AND r.reserve_time >= @ReserveTimeStart 
	AND r.reserve_time < @ReserveTimeEnd AND r.user_id =@UserId AND r.car_id = @CarId limit 1";

            DateTime reserveTime = DateTime.Today.AddDays(reserveValue);

            var para = new DynamicParameters();
            para.Add("@UserId", userId);
            para.Add("@ReserveTimeStart", reserveTime);
            para.Add("@ReserveTimeEnd", reserveTime.AddDays(1));
            para.Add("@CarId", carId);
            para.Add("@ShopId", shopId);

            var result = new ReserveInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ReserveInfoDO>(sql, para)).FirstOrDefault();
            });
            return result;
        }

        #region  APP预约

        /// <summary>
        /// 查询已预约列表---APP
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ReserveInfoDO>> GetReserveListForAppAsync(ReservedListForAppRequest request)
        {

            string dt = DateTime.Now.ToShortDateString();
            DateTime startTime = DateTime.Parse(dt).AddDays(request.OffsetValue);
            DateTime endTime = DateTime.Parse(dt).AddDays(request.OffsetValue + 1);

            //查询列表
            string sql = @"SELECT
	res.id,
	res.reserve_time as ReserveTime,
    res.user_name as UserName,
	res.user_tel as UserTel,
	res.car_no as CarNo,
	res.vehicle,
	res.pai_liang as PaiLiang,
	res.nian,
	res.sales_name as SalesName,
	res.shop_id as ShopId,
	res.service_name as ServiceName,
	rec.arrival_time as ArrivalTime,
    res.channel
FROM
	shop_reserve res
	LEFT JOIN reserve_receive_relation rrr ON rrr.reserve_id = res.id
	LEFT JOIN shop_receive rec on rrr.receive_id = rec.id 
WHERE
    res.shop_id = @ShopId
    AND res.is_deleted = 0
    AND res.reserve_time >= @StartTime
    AND res.reserve_time < @EndTime
    AND res.`status` IN(0, 1)
ORDER BY res.reserve_time asc";
            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@StartTime", startTime);
            para.Add("@EndTime", endTime);
            para.Add("@SearchValue", request.SearchValue);
            StringBuilder condition = new StringBuilder();
            if (!string.IsNullOrEmpty(request.SearchValue)) 
            {
                condition.Append(" AND (res.user_tel LIKE CONCAT('%',@SearchValue,'%') OR res.car_no LIKE CONCAT('%',@SearchValue,'%'))");
                sql = sql + condition.ToString();
            }
            
            IEnumerable<ReserveInfoDO> ReserveList = new List<ReserveInfoDO>();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ReserveList = await conn.QueryAsync<ReserveInfoDO>(sql, para);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return ReserveList.ToList();
        }

        /// <summary>
        /// 预约列表查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopReserveDO>> GetReserveListWithCondition(ReceiveListWithCondition request)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE shop_id = @shopId AND `status` != 0");
            para.Add("@shopId", request.ShopId);
            if (!string.IsNullOrEmpty(request.ReserveType))
            {
                condition.Append(" AND `service_type` = @serviceType");
                para.Add("@serviceType", request.ReserveType);
            }

            if (request.HideCanceled)
            {
                condition.Append(" AND `status` != 3");
            }

            if (request.EndTime != null)
            {
                condition.Append(" AND `reserve_time` < @endTime");
                para.Add("@endTime", request.EndTime.Value);
            }

            if (request.StarTime != null)
            {
                condition.Append(" AND `reserve_time` >= @startTime");
                para.Add("@startTime", request.StarTime.Value);
            }

            if (!string.IsNullOrEmpty(request.UserTel) && !string.IsNullOrEmpty(request.CarPlate))
            {
                condition.Append(" AND (`user_tel` = @user_tel OR `car_no` = @car_no)");
                para.Add("@user_tel", request.UserTel);
                para.Add("@car_no", request.CarPlate);
            }
            else if (!string.IsNullOrEmpty(request.UserTel))
            {
                condition.Append(" AND `user_tel` = @user_tel");
                para.Add("@user_tel", request.UserTel);
            }
            else if (!string.IsNullOrEmpty(request.CarPlate))
            {
                condition.Append(" AND `car_no` = @car_no");
                para.Add("@car_no", request.CarPlate);
            }

            if (!string.IsNullOrEmpty(request.TechId))
            {
                condition.Append(" AND `tech_id` = @tech_id");
                para.Add("@tech_id", request.TechId);
            }

            var result = await GetListAsync<ShopReserveDO>(condition.ToString(), para);

            return result?.ToList() ?? new List<ShopReserveDO>();
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="reserveId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<ShopReserveDO> GetReserveDetail(long reserveId, bool readOnly = true)
        {
            return await GetAsync(reserveId, !true);
        }

        /// <summary>
        /// 查用户有效预约
        /// </summary>
        /// <param name="userTel"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveDO>> GetValidReserveList(string userTel, int shopId)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@userTel", userTel);
            var result = await GetListAsync<ShopReserveDO>(
                "WHERE `shop_id` = @shopId AND `user_tel` = @userTel AND `status` = 1 AND reserve_time >= DATE_SUB(NOW(),interval 0 MINUTE)",
                para);
            return result?.ToList() ?? new List<ShopReserveDO>();
        }

        /// <summary>
        /// 预约列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<ShopReserveRelationDo>, int>> GetCommonReserveListPage(CommonReserveListPageCondition request)
        {
            string sql = @"SELECT
	a.id AS `Id`,
	a.user_name AS UserName,
	a.user_tel AS UserTel,
	a.channel AS Channel,
	a.service_type AS ServiceType,
	a.reserve_time AS ReserveTime,
	a.tech_id AS TechId,
	a.tech_name AS TechName,
	a.car_no AS CarNo,
	a.vehicle AS Vehicle,
	a.pai_liang AS PaiLiang,
	a.nian AS Nian,
	a.sales_name AS SalesName,
    a.`status` AS `Status`,
	MAX( b.arrival_time ) AS ArriveTime  
FROM
	shop_reserve a
	LEFT JOIN shop_arrival b ON ( a.id = b.reserve_no ) {0}
GROUP BY
	a.id  {1}
ORDER BY
	reserve_time DESC 
	LIMIT @startPage,@endPage;";

            string sqlCount = @"SELECT COUNT(*) FROM(SELECT
	a.*,
	MAX( b.arrival_time ) AS ArriveTime 
FROM
	shop_reserve a
	LEFT JOIN shop_arrival b ON ( a.id = b.reserve_no ) {0}
GROUP BY
	a.id {1} ) S;";

            StringBuilder condition = new StringBuilder();
            string having = string.Empty;
            var para = new DynamicParameters();
            condition.Append("WHERE a.shop_id = @shopId AND a.`status` != 0");
            para.Add("@shopId", request.ShopId);
            para.Add("@startPage", (request.PageIndex - 1) * request.PageSize);
            para.Add("@endPage", request.PageSize);
            if (!string.IsNullOrEmpty(request.UserTel))
            {
                condition.Append(" AND a.user_tel = @userTel");
                para.Add("@userTel", request.UserTel);
            }

            if (!string.IsNullOrEmpty(request.CarPlate))
            {
                condition.Append(" AND a.car_no LIKE @carPlate");
                para.Add("@carPlate", $"%{request.CarPlate}%");
            }

            if (request.ReserveIds != null && request.ReserveIds.Any())
            {
                condition.Append(" AND a.`id` IN @reserveIds");
                para.Add("@reserveIds", request.ReserveIds);
            }

            if (!string.IsNullOrEmpty(request.ReserveType))
            {
                condition.Append(" AND a.`service_type` = @reserveType");
                para.Add("@reserveType", request.ReserveType);
            }

            if (request.StarTime != null)
            {
                condition.Append(" AND a.`reserve_time` >= @startTime");
                para.Add("@startTime", request.StarTime.Value);
            }

            if (request.EndTime != null)
            {
                condition.Append(" AND a.`reserve_time` < @endTime");
                para.Add("@endTime", request.EndTime.Value);
            }

            if (request.ReserveChannel > 0)
            {
                condition.Append(" AND a.`channel` = @channel");
                para.Add("@channel", request.ReserveChannel);
            }

            if (request.ReserveTech)
            {
                condition.Append(" AND a.`tech_id` != ''");
            }

            if (request.Status == 1)
            {
                condition.Append(" AND a.`status` = 1 AND a.`reserve_time` >= NOW( )");
            }
            else if (request.Status == 2)//正常到店
            {
                condition.Append(" AND a.`status` = 2");
                having = "HAVING (ArriveTime IS NULL OR ArriveTime <= a.`reserve_time`)";
            }
            else if (request.Status == 3)//逾期到店
            {
                condition.Append(" AND a.`status` = 2");
                having = "HAVING ArriveTime > a.`reserve_time`";
            }
            else if (request.Status == 4)
            {
                condition.Append(" AND a.`status` = 1 AND a.`reserve_time` < NOW( )");
            }
            else if (request.Status == 5)
            {
                condition.Append(" AND a.`status` = 3");
            }

            sql = string.Format(sql, condition.ToString(), having);
            sqlCount = string.Format(sqlCount, condition.ToString(), having);

            List<ShopReserveRelationDo> data = new List<ShopReserveRelationDo>();
            int records = 0;

            List<Task> tasks = new List<Task>();

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<ShopReserveRelationDo>(sql, para))?.ToList() ?? new List<ShopReserveRelationDo>();
            }));

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                records = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new Tuple<List<ShopReserveRelationDo>, int>(data, records);
        }

        #endregion

    }
}
