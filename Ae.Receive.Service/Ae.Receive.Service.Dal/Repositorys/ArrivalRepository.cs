using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Receive.Service.Common.Constant;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Common.Extension;
using Ae.Receive.Service.Core.Enums;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Core.Response.Arrival;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ArrivalRepository : AbstractRepository<ShopArrivalDO>, IArrivalRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<ArrivalRepository> logger;
        private readonly string className;

        public ArrivalRepository(ApolloErpLogger<ArrivalRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<TodayArrivalShopStatisticsResDTO>> GetTodayArrivalShopStatistics(TodayArrivalShopStatisticsReqDTO req)
        {
            var res = new List<TodayArrivalShopStatisticsResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@Start", DateTimeExtension.GetTodayStartTime());
                    param.Add("@End", DateTimeExtension.GetTodayEndTime());

                    var sql = @"SELECT `status`, user_id userId
                                FROM shop_arrival 
                                WHERE is_deleted = 0 AND shop_id = @ShopId 
                                AND arrival_time BETWEEN @Start AND @End ";
                    var resDo = await conn.QueryAsync<ShopArrivalDO>(sql, param);
                    if (resDo != null && resDo.Any())
                    {
                        res = resDo.OrderBy(o => o.Status)
                            .GroupBy(g => g.Status)
                            .Select(s => new TodayArrivalShopStatisticsResDTO
                            {
                                Status = (ShopArrivalStatus)s.Key,
                                Amount = s.Count()
                            }).ToList();
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"SVC: {className}.GetTodayArrivalShopStatistics 请求值：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"SVC: {className}.GetTodayArrivalShopStatistics 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<MonthArrivalShopStatisticsResDTO>> GetMonthArrivalShopStatistics(MonthArrivalShopStatisticsReqDTO req)
        {
            var res = new List<MonthArrivalShopStatisticsResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@Start", req.StartTime);
                    param.Add("@End", req.EndTime);

                    var sql = @"SELECT arrival_time arrivalTime, user_id userId
                                FROM shop_arrival 
                                WHERE is_deleted = 0 AND shop_id = @ShopId 
                                AND arrival_time BETWEEN @Start AND @End ";
                    var resDo = await conn.QueryAsync<ShopArrivalDO>(sql, param);
                    if (resDo != null && resDo.Any())
                    {
                        res = resDo.OrderBy(o => o.ArrivalTime.Date)
                            .GroupBy(g => g.ArrivalTime.Date)
                            .Select(s => new MonthArrivalShopStatisticsResDTO
                            {
                                Date = s.Key,
                                Amount = s.Count()
                            }).ToList();
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"SVC: {className}.GetMonthArrivalShopStatistics 请求值：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"SVC: {className}.GetMonthArrivalShopStatistics 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<NewCustomerArrivalShopResDTO>> GetNewCustomerArrivalShopStatistics(NewCustomerArrivalShopReqDTO req)
        {
            var res = new List<NewCustomerArrivalShopResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var sb = new StringBuilder();
                    var len = req.Params.Count;
                    for (int i = 0; i < len; i++)
                    {
                        var obj = req.Params[i];

                        if (!obj.UserIds.Any()) continue;

                        var sel = $"SELECT DISTINCT '{obj.Key}' `Key`, user_id userId FROM shop_arrival ";
                        var whrClu = $"WHERE shop_id = {req.ShopId} AND user_id IN ('{string.Join("','", obj.UserIds)}') ";

                        sb.AppendLine(sel).AppendLine(whrClu);
                        if (i != len - 1) { sb.AppendLine("UNION ALL "); }
                    }

                    if (sb.Length == CommonConstant.Zero)
                    {
                        logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.NotExpectationResult));
                        return;
                    }

                    var resDo = await conn.QueryAsync<ShopArrivalCustomDO>(sb.ToString());
                    if (resDo != null && resDo.Any())
                    {
                        var tmp = resDo.GroupBy(g => g.Key)
                            .Select(s => new NewCustomerArrivalShopResDTO
                            {
                                Key = s.Key,
                                Amount = s.Count()
                            });
                        res.AddRange(tmp);
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"SVC: {className}.GetNewCustomerArrivalShopStatistics 请求值：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"SVC: {className}.GetNewCustomerArrivalShopStatistics 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<NewCustomerArrivalShopSaleroomResDTO>> GetNewCustomerArrivalShopSaleroomStatistics(NewCustomerArrivalShopSaleroomReqDTO req)
        {
            var res = new List<NewCustomerArrivalShopSaleroomResDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var sb = new StringBuilder();
                    var len = req.Params.Count;
                    for (int i = 0; i < len; i++)
                    {
                        var obj = req.Params[i];

                        if (!obj.UserIds.Any()) continue;

                        var sel = $"  SELECT '{obj.Key}' `Key`, ao.price, ao.num " +
                                        @"  FROM shop_arrival a
                                            INNER JOIN shop_arrival_order ao ON a.id = ao.arrival_id ";
                        var whrClu = $"WHERE a.is_deleted = 0 AND ao.is_deleted=0 AND a.shop_id = {req.ShopId} AND a.status=2 AND a.user_id IN ('{string.Join("','", obj.UserIds)}') ";

                        sb.AppendLine(sel).AppendLine(whrClu);
                        if (i != len - 1) { sb.AppendLine("UNION ALL "); }
                    }

                    if (sb.Length == CommonConstant.Zero)
                    {
                        logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.NotExpectationResult));
                        return;
                    }

                    var resDo = await conn.QueryAsync<ShopArrivalCustomDO>(sb.ToString());
                    if (resDo != null && resDo.Any())
                    {
                        var tmp = resDo.GroupBy(g => g.Key)
                            .Select(s => new NewCustomerArrivalShopSaleroomResDTO
                            {
                                Key = s.Key,
                                Amount = s.Select(e => e.Num * e.Price).Aggregate((accumulate, next) => accumulate + next)
                            });
                        res.AddRange(tmp);
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"SVC: {className}.GetNewCustomerArrivalShopSaleroomStatistics 请求值：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"SVC: {className}.GetNewCustomerArrivalShopSaleroomStatistics 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        /// <summary>
        /// 得到生成的Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> GetQuqueNumberGenerator(GetQuqueNumberGeneratorRequest request)
        {
            var result = 1;
            if (request.ShopId <= 0)
                return result;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();
                condition.AppendLine(" where shop_id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);

                condition.AppendLine(" and generator_type=@GeneratorType");
                parameters.Add("@GeneratorType", request.GeneratorType);

                condition.AppendLine(" and generator_date between @Today  and @Tomorrow");

                parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

                var getListAsync = await GetListAsync<QuqueNumberGeneratorDO>(condition.ToString(), parameters, false);

                if (getListAsync?.Count() > 0)
                {
                    await OpenConnectionAsync(async conn =>
                    {
                        var id = getListAsync?.FirstOrDefault()?.Id;
                        var updateSql =
                            $" UPDATE quque_number_generator set current_number=current_number+1 WHERE is_deleted = 0 AND id={id} ";

                        var updateResult = await conn.ExecuteAsync(updateSql, null);

                        if (updateResult > 0)
                        {
                            var currentNum = getListAsync?.FirstOrDefault()?.CurrentNumber ?? 0 + 1;
                            result = currentNum + 1;
                        }
                    });

                }
            }

            return result;
        }

        /// <summary>
        /// 保存排队号
        /// </summary>
        /// <param name="ququeNumberGeneratorDO"></param>
        /// <returns></returns>
        public async Task<long> SaveQuqueNumberGenerator(QuqueNumberGeneratorDO ququeNumberGeneratorDO)
        {
            return await InsertAsync(ququeNumberGeneratorDO);
        }

        /// <summary>
        /// 保存到店记录
        /// </summary>
        /// <param name="shopArrivalDO"></param>
        /// <returns></returns>
        public async Task<long> SaveArrival(ShopArrivalDO shopArrivalDO)
        {
            return await InsertAsync(shopArrivalDO);
        }

        /// <summary>
        /// 用户车型历史消费记录
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<HistoryArrivalConsumerDTO> GetHistoryArrivalConsumer(string carId, string userId)
        {
            IEnumerable<HistoryArrivalConsumerDTO> historyArrivalConsumerDTOs = null;
            await OpenConnectionAsync(async conn =>
            {
                var sql =
                    @" select 0 As ArrivalSumCount,0 AS SumPrice,Max(A.arrival_time) As LastArrivalTime from shop_arrival A
                    where A.is_deleted = 0 
                    and A.car_id = '" + carId + "' and A.user_id = '" + userId + "' " +
                    " ORDER BY A.create_time DESC";

                historyArrivalConsumerDTOs = await conn.QueryAsync<HistoryArrivalConsumerDTO>(sql, null);
            });
            return historyArrivalConsumerDTOs.FirstOrDefault();
        }

        /// <summary>
        /// 得到历史到店记录
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalDO>> GetShopArrivals(string carId, string userId)
        {
            IEnumerable<ShopArrivalDO> shopArrivalDOs = null;
            await OpenConnectionAsync(async conn =>
            {

                var sql =
                    @" select id as Id,arrival_time AS ArrivalTime,status As Status,user_id AS UserId,
                       user_name AS UserName,user_tel AS  UserTel,car_id As  CarId,
                     car_no AS CarNo ,vehicle AS  Vehicle,brand AS Brand,service_type AS ServiceType,shop_id AS ShopId ,
                     shop_name AS ShopName ,remark AS  Remark,
                    tech_id AS TechId ,tech_name AS TechName,level AS Level,
                     reserve_no AS ReserveNo ,queue_type AS QueueType,queue_number AS QueueNumber ,create_time As CreateTime from shop_arrival 
                    where car_id='" + carId + "' and user_id='" + userId + "' and is_deleted=0 ORDER BY create_time DESC";

                shopArrivalDOs = await conn.QueryAsync<ShopArrivalDO>(sql, null);
            });

            return shopArrivalDOs.ToList();
        }

        /// <summary>
        /// 到店记录下面订单
        /// </summary>
        /// <param name="arrivalIds"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalOrderDO>> GetShopArrivalOrders(List<long> arrivalIds)
        {
            var sql =
                @" select id AS Id,arrival_id AS ArrivalId,order_no As OrderNo,order_type As OrderType,
                       product_name As ProductName,price As Price,num As Num,create_time As CreateTime from shop_arrival_order 
                    where arrival_id IN  @ArrivalIds  and is_deleted=0";

            IEnumerable<ShopArrivalOrderDO> list = new List<ShopArrivalOrderDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<ShopArrivalOrderDO>(sql, new
                {
                    ArrivalIds = arrivalIds
                });
            });
            return list.ToList();
        }

        /// <summary>
        /// 得到到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request)
        {
            PagedEntity<GetArrivalListResponse> response = new PagedEntity<GetArrivalListResponse>();

            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where is_deleted = 0 ");

            builder.AppendLine(" and shop_Id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            if (!string.IsNullOrEmpty(request.Telephone))
            {
                builder.AppendLine($" and  user_tel=@UseTel");
                parameters.Add("@UseTel", request.Telephone);
            }

            if (!string.IsNullOrEmpty(request.CarNo))
            {
                builder.AppendLine($" and  car_no=@CarNo");
                parameters.Add("@CarNo", request.CarNo);
            }

            if (request.Status != ArrivalRecordStatusEnum.All)
            {
                builder.AppendLine(" and status=@Status");
                parameters.Add("@Status", request.Status.ToInt());
            }

            if (request.IncloudNoFinish && (request.Status == ArrivalRecordStatusEnum.Waiting || request.Status == ArrivalRecordStatusEnum.Dispatching))
            {
            }
            else if (!string.IsNullOrEmpty(request.ArrivalDate))
            {
                bool isSuccess = DateTime.TryParse(request.ArrivalDate, out var arrivalDate);
                if (isSuccess)
                {
                    builder.AppendLine($" and  arrival_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", arrivalDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", arrivalDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }
        
            var sqlCount = @"select Count(1) FROM shop_arrival " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            var sql = @"select  id as ArrivalId,car_id As CarId, brand As Brand,'' As CarLogo,
                    vehicle As Vehicle,car_no As CarNo,user_name As UserName,
                    user_tel As Telephone,service_type As ShowServiceType,
                    queue_type As ShowQueueType,queue_number As QueueNumber,0 AS ShowMinute,
                    arrival_time AS ShowArrivalDate,update_time AS UpdateDate,status As Status   from shop_arrival " + builder.ToString() 
                    + " order by status, create_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;

            IEnumerable<GetArrivalListResponse> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetArrivalListResponse>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos.ToList();

            return response;
        }

        public async Task<GetArrivalListResponse> GetLastArrival(GetLastArrivalRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where is_deleted = 0 ");

            builder.AppendLine(" and shop_Id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            if (!string.IsNullOrEmpty(request.Telephone))
            {
                builder.AppendLine($" and  user_tel=@UseTel");
                parameters.Add("@UseTel", request.Telephone);
            }

            if (!string.IsNullOrEmpty(request.CarNo))
            {
                builder.AppendLine($" and  car_no=@CarNo");
                parameters.Add("@CarNo", request.CarNo);
            }

            if (request.IsNoFinish )
            {
                builder.AppendLine(" and status < 2");
            }

            var sql = @"select  id as ArrivalId,car_id As CarId, brand As Brand,'' As CarLogo,
                    vehicle As Vehicle,car_no As CarNo,user_name As UserName,
                    user_tel As Telephone,service_type As ShowServiceType,
                    queue_type As ShowQueueType,queue_number As QueueNumber,0 AS ShowMinute,
                    arrival_time AS ShowArrivalDate,update_time AS UpdateDate,status As Status   from shop_arrival " + builder.ToString()
                    + " order by create_time desc limit 0,1" ;

            IEnumerable<GetArrivalListResponse> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetArrivalListResponse>(sql, parameters));

            return orderDos?.FirstOrDefault();
        }


        /// <summary>
        /// 得到到店记录详情
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        public async Task<ShopArrivalDO> GetShopArrival(long arrivalId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ArrivalId", arrivalId);
            var getList = await GetListAsync<ShopArrivalDO>("where id=@ArrivalId", parameters, false);
            return getList?.FirstOrDefault();
        }

        /// <summary>
        /// 更新技师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> UpdateTechnicians(DispatchTechnicianSaveRequest request)
        {
            string sql = @"UPDATE shop_arrival 
                            SET status = @Status,tech_id=@TechId,tech_name=@TechName,level=@Level,update_time=Now(3),dispatch_time=Now(3)
                            WHERE
                                id = @ArrivalId ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    Status = ArrivalRecordStatusEnum.Dispatching.ToInt(),
                    TechId = request.UserId,
                    TechName = request.Name,
                    Level = request.Level,
                    ArrivalId = request.ArrivalId
                });
            });
            return count;
        }

        /// <summary>
        /// 得到未消费离店原因
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopArrivalReasonDO>> GetShopArrivalOutPay()
        {
            var getList = await GetListAsync<ShopArrivalReasonDO>("where is_deleted=0", null, false);
            return getList?.ToList();
        }

        /// <summary>
        /// 离店原因保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> LeaveShopReasonSave(ShopArrivalCancelDO request)
        {
            return await InsertAsync<ShopArrivalCancelDO>(request);
        }

        /// <summary>
        ///  更新到店记录状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="UpdateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateShopArrivalStaus(int status, long arrivalId, string userName)
        {
            string sql = @"UPDATE shop_arrival 
                            SET status = @Status,update_by=@UserName,update_time=Now(3)
                            WHERE
                                id = @ArrivalId ";

            if (status == ArrivalRecordStatusEnum.Leave.ToInt())
            {
                sql = @"UPDATE shop_arrival 
                            SET status = @Status,update_by=@UserName,update_time=Now(3),cancel_time=Now(3)
                            WHERE
                                id = @ArrivalId ";
            }
            if (status == ArrivalRecordStatusEnum.Finish.ToInt())
            {
                sql = @"UPDATE shop_arrival 
                            SET status = @Status,update_by=@UserName,update_time=Now(3),finish_time=Now(3)
                            WHERE
                                id = @ArrivalId ";
            }

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    Status = status,
                    ArrivalId = arrivalId,
                    UserName = userName
                });
            });
            return count;
        }

        /// <summary>
        /// 删除到店记录订单
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        public async Task<int> DeleteShopArrivalOrder(long arrivalId, string OrderNo, string userName)
        {
            string sql = @"UPDATE shop_arrival_order 
                            SET is_deleted=1,update_by=@UserName,update_time=Now(3)
                            WHERE
                                arrival_id = @ArrivalId and order_no=@OrderNo ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    ArrivalId = arrivalId,
                    OrderNo = OrderNo,
                    UserName = userName
                });
            });
            return count;
        }



        /// <summary>
        /// 删除到店记录订单
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        public async Task<int> DeleteShopArrivalOrder(long arrivalId, string userName)
        {
            string sql = @"UPDATE shop_arrival_order 
                            SET is_deleted=1,update_by=@UserName,update_time=Now(3)
                            WHERE
                                arrival_id = @ArrivalId";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    ArrivalId = arrivalId,
                    UserName = userName
                });
            });
            return count;
        }

        /// <summary>
        /// 删除到店记录订单
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public async Task<int> DeleteShopArrivalOrder(string OrderNo, string userName)
        {
            string sql = @"UPDATE shop_arrival_order 
                            SET is_deleted=1,update_by=@UserName,update_time=Now(3)
                            WHERE
                                 order_no=@OrderNo ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = OrderNo,
                    UserName = userName
                });
            });
            return count;
        }

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> GetLastShopForLastArrival(GetLastShopForLastArrivalRequest request)
        {
            var sql = @"select shop_id from shop_arrival
                where user_id=@UserId  and is_deleted=0
                order BY create_time desc";
            IEnumerable<long> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<long>(sql, new { UserId = request.UserId }));
            return orderDos?.FirstOrDefault() ?? 0;
        }

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalOrderDO>> GetOrdersForReserver(GetOrdersForReserverRequest request)
        {
            var sql =
                @" select id AS Id,arrival_id AS ArrivalId,order_no As OrderNo,order_type As OrderType,
                       product_name As ProductName,price As Price,num As Num,create_time As CreateTime,pid AS Pid from shop_arrival_order 
                    where order_no IN  @OrderNos and is_deleted=0 ";

            IEnumerable<ShopArrivalOrderDO> list = new List<ShopArrivalOrderDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<ShopArrivalOrderDO>(sql, new
                {
                    OrderNos = request.OrderNos
                });
            });
            return list.ToList();
        }

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> UpdateArrivalVehicle(UpdateArrivalVehicleRequest request)
        {
            string sql = @"UPDATE shop_arrival 
                            SET car_no=@CarNo,vehicle=@Vehicle,brand=@Brand,tid=@Tid,vehicle_id=@VehicleId,pai_liang=@PaiLiang,nian=@Nian,sales_name=@SalesName
                            WHERE
                                id = @ArrivalId and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    CarNo = request.CarNo,
                    Vehicle = request.Vehicle,
                    Brand = request.Brand,
                    ArrivalId = request.ArrivalId,
                    Tid=request.Tid,
                    VehicleId=request.VehicleId,
                    PaiLiang=request.PaiLiang,
                    Nian =request.Nian,
                    SalesName =request.SalesName,
                });
            });
            return count;
        }

        /// <summary>
        /// 得到排队号是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public async Task<bool> GetShopArrivalForQueue(string userId, string carId, long shopId)
        {
            var sql = @"select Count(1) FROM shop_arrival where user_id=@UserId and car_id=@CarId and shop_id=@ShopId and arrival_time between 
                        @StartDate and @EndDate ";
            var total = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@CarId", carId);
            parameters.Add("@ShopId", shopId);
            parameters.Add("@StartDate", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            parameters.Add("@EndDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            });
            return total > 0 ? true : false;
        }

        /// <summary>
        /// 得到到店记录消费金额
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal?> GetHistoryArrivalConsumerPrice(string carId, string userId)
        {
            decimal? result = 0;
            await OpenConnectionAsync(async conn =>
            {

                var sql =
                    @" select SUM(B.price) AS SumPrice from shop_arrival A
                    left join  shop_arrival_order B
                    on A.id = B.arrival_id and A.is_deleted = 0 and B.is_deleted=0 and A.status=2
                    where A.car_id = '" + carId + "' and A.user_id = '" + userId + "' " +
                    " ORDER BY A.create_time DESC";

                result = await conn.QueryFirstOrDefaultAsync<decimal?>(sql);
            });
            return result;
        }

        public async Task<int> GetHistoryArrivalConsumerCount(string carId, string userId)
        {
            int result = 0;
            await OpenConnectionAsync(async conn =>
            {
                var sql =
                    @" select Count(A.id) As ArrivalSumCount from shop_arrival A
                    where  A.is_deleted = 0 
                    and A.car_id = '" + carId + "' and A.user_id = '" + userId + "'";

                result = await conn.QueryFirstOrDefaultAsync<int>(sql);
            });
            return result;
        }

        /// <summary>
        /// 得到到店记录状态数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetArrivalListCountResponse> GetArrivalListCount(GetArrivalListRequest request)
        {

            request.Status = ArrivalRecordStatusEnum.Waiting;
            var getArrivalWaitingNum = await GetArrivalCount(request);
            request.Status = ArrivalRecordStatusEnum.Dispatching;
            var getArrivalDispatchingNum = await GetArrivalCount(request);
            request.Status = ArrivalRecordStatusEnum.Finish;
            var getArrivalFinishNum = await GetArrivalCount(request);
            request.Status = ArrivalRecordStatusEnum.Leave;
            var getArrivalLeaveNum = await GetArrivalCount(request);

            return new GetArrivalListCountResponse()
            {
                WaitingNum = getArrivalWaitingNum,
                DispatchingNum = getArrivalDispatchingNum,
                FinishNum = getArrivalFinishNum,
                LeaveNum = getArrivalLeaveNum,
            };
        }

        public async Task<List<ShopArrivalDO>> GetShopArrivalForStatic(GetShopArrivalForStaticRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" where is_deleted=0 and `status`=2 ");
            builder.AppendLine(" and user_id in @UserIds");
            parameters.Add("@UserIds", request.UserId);
            if (!string.IsNullOrWhiteSpace(request.StartTime))
            {

                bool isSuccess = DateTime.TryParse(request.StartTime, out var startTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and arrival_time>=@StartTime");
                    parameters.Add("@StartTime", startTime);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.EndTime))
            {

                bool isSuccess = DateTime.TryParse(request.EndTime, out var endTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and arrival_time<=@EndTime");
                    parameters.Add("@EndTime", endTime);
                }
            }


            var getList = await GetListAsync<ShopArrivalDO>(builder.ToString(), parameters, false);
            return getList?.ToList();
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private async Task<int> GetArrivalCount(GetArrivalListRequest request)
        {

            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where is_deleted = 0 ");

            builder.AppendLine(" and shop_Id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            builder.AppendLine(" and status=@Status");
            parameters.Add("@Status", request.Status.ToInt());

            if (!string.IsNullOrEmpty(request.Telephone))
            {
                builder.AppendLine($" and  user_tel=@UseTel");
                parameters.Add("@UseTel", request.Telephone);
            }

            if (!string.IsNullOrEmpty(request.CarNo))
            {
                builder.AppendLine($" and  car_no=@CarNo");
                parameters.Add("@CarNo", request.CarNo);
            }

            if (request.IncloudNoFinish && (request.Status == ArrivalRecordStatusEnum.Waiting || request.Status == ArrivalRecordStatusEnum.Dispatching))
            {
            }
            else if (!string.IsNullOrEmpty(request.ArrivalDate))
            {
                bool isSuccess = DateTime.TryParse(request.ArrivalDate, out var arrivalDate);
                if (isSuccess)
                {
                    builder.AppendLine($" and  arrival_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", arrivalDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", arrivalDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }


            var sqlCount = @"select Count(1) FROM shop_arrival " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });
            return total;
        }

        /// <summary>
        /// 得到车辆最后入店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetLastArriveTimeByCarId(GetArrialMaintenanceRequest request)
        {
            var sql = @"select arrival_time FROM shop_arrival where car_id=@CarId  and is_deleted=0 
                            order BY arrival_time DESC limit 1";
            var parameters = new DynamicParameters();
            parameters.Add("@CarId", request.CarId);
            var asrrivalTime = string.Empty;
            await OpenSlaveConnectionAsync(async conn =>
            {
                asrrivalTime = await conn.QueryFirstOrDefaultAsync<string>(sql, parameters);
            });
            return asrrivalTime;
        }

        /// <summary>
        /// 得到到店的次数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> GetArriveCountByCarId(GetArrialMaintenanceRequest request)
        {
            var sql = @"select count(1) FROM shop_arrival where 
                        car_id=@CarId and is_deleted=0";
            var parameters = new DynamicParameters();
            parameters.Add("@CarId", request.CarId);
            var count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            });
            return count;
        }

        /// <summary>
        /// 得到到店的消费金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<double> GetArriveConsumptionAmountByCarId(GetArrialMaintenanceRequest request)
        {
            var sql = @"select Sum(B.price) FROM shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0 
                        where A.`status`=2
                        and A.car_id=@CarId";
            var parameters = new DynamicParameters();
            parameters.Add("@CarId", request.CarId);
            double? amount = 0.0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                amount = await conn.QueryFirstOrDefaultAsync<double?>(sql, parameters);
            });
            return amount ?? 0;


        }

        /// <summary>
        /// 得到维修保养记录头
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetArrivalMaintenanceListByCarIdVo>> GetArrivalMaintenanceHeader(GetArrivalMaintenanceListByCarIdRequest request)
        {
            var sql = @"select count(1) Num,'Tire' ServiceType,'轮胎安装' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='轮胎安装' ) t
                        union
                        select count(1) Num,'Maintenance' ServiceType,'保养服务' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='保养服务' ) t
                        UNION
                        select count(1) Num,'Beauty' ServiceType,'美容洗车' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='美容洗车' ) t
                        union
                        select count(1) Num,'SheetMetal' ServiceType,'钣金维修' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='钣金维修' ) t
                        union
                        select count(1) Num,'CarModification' ServiceType,'汽车改装' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='汽车改装' ) t
                        union
                        select count(1) Num,'Other' ServiceType,'其他' ServiceTypeText from (
                        select DISTINCT A.id from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id and A.is_deleted=0 and B.is_deleted=0
                        where A.car_id=@CarId and B.order_type='其他' ) t
";
            var parameters = new DynamicParameters();
            parameters.Add("@CarId", request.CarId);
            IEnumerable<GetArrivalMaintenanceListByCarIdVo> list = new List<GetArrivalMaintenanceListByCarIdVo>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<GetArrivalMaintenanceListByCarIdVo>(sql, parameters);
            });
            return list?.ToList();
        }

        /// <summary>
        /// 得到维修保养项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetArrivalMaintenanceItemResponse>> GetArrivalMaintenanceItem(GetArrivalMaintenanceListByCarIdRequest request)
        {
            var sql = @"select A.id AS Id, A.arrival_time As ArrivalTime,A.shop_name As ShopName,B.order_type As OrderType,B.pid As Pid,B.product_name As ProductName,B.price As Price,B.order_no OrderNo,B.num Num,A.tech_name TechName from shop_arrival A
                        inner join shop_arrival_order B
                        on A.id=B.arrival_id
                        where A.car_id=@CarId and A.is_deleted=0 and b.is_deleted=0
                        ";
            var parameters = new DynamicParameters();
            parameters.Add("@CarId", request.CarId);
            IEnumerable<GetArrivalMaintenanceItemResponse> list = new List<GetArrivalMaintenanceItemResponse>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<GetArrivalMaintenanceItemResponse>(sql, parameters);
            });
            return list?.ToList();
        }
    }
}
