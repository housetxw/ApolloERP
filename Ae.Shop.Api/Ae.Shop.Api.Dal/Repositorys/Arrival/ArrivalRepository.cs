using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Api.Common.Extension;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Request.Arrival;
using Ae.Shop.Api.Core.Response.Arrival;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
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
            builder.AppendLine("where 1=1");

            builder.AppendLine(" and shop_Id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);


            if (request.Status != ArrivalRecordStatusEnum.All)
            {
                builder.AppendLine(" and status=@Status");
                parameters.Add("@Status", request.Status.ToInt());
            }

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


            if (!string.IsNullOrEmpty(request.StartDate))
            {
                bool isSuccessStart = DateTime.TryParse(request.StartDate, out var startDate);
                bool isSuccessEnd = DateTime.TryParse(request.EndDate, out var endDate);
                if (isSuccessStart && isSuccessEnd)
                {
                    builder.AppendLine($" and  arrival_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", endDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(request.Telephone) && string.IsNullOrEmpty(request.CarNo))
                {
                    builder.AppendLine($" and  arrival_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }

            var sqlCount = @"select Count(1) FROM shop_arrival " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            var sql = @"select  id as Id,car_id As CarId, brand As Brand,'' As CarLogo,
                    vehicle As Vehicle,car_no As CarNo,user_name As UserName,
                    user_tel As Telephone,tech_name As TechName, service_type As ShowServiceType,
                    queue_type As ShowQueueType,queue_number As QueueNumber,0 AS ShowMinute,
                    arrival_time AS ShowArrivalDate,update_time AS UpdateDate,status As Status   from shop_arrival " + builder.ToString() + " order by create_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;

            IEnumerable<GetArrivalListResponse> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetArrivalListResponse>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos.ToList();

            return response;
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

        public async Task<List<ArrivalTrendChartEntityDTO>> GetArrivalTrendStatisticsByStatus(ArrivalTrendStatisticsReqDO req)
        {
            var res = new List<ArrivalTrendChartEntityDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@ShopId", req.ShopId);
                    param.Add("@Start", req.StartTime);
                    param.Add("@End", req.EndTime);

                    var whrCla = "";
                    if (req.Status.Any())
                    {
                        param.Add("@Status", req.Status);
                        whrCla = " AND a.`status` IN @Status ";
                    }

                    var sql = $@"SELECT CAST(arrival_time AS date) arrivaltime, COUNT(0) Amount
                                        FROM shop_arrival a
                                        WHERE a.is_deleted = 0 
                                        AND a.shop_id = @ShopId {whrCla}
                                        AND arrival_time BETWEEN @Start AND @End
                                        GROUP BY CAST(arrival_time AS date) 
                                        ORDER BY CAST(arrival_time AS date) DESC;";
                    res = (await conn.QueryAsync<ArrivalTrendChartEntityDTO>(sql, param)).ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"SVC: {className}.GetArrivalTrendStatistics 请求值：{JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.GetArrivalTrendStatistics 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------

    }
}
