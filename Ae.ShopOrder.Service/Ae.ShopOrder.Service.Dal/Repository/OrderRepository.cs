using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderRepository : AbstractRepository<OrderDO>, IOrderRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<OrderRepository> _logger;
        private readonly string className;

        public OrderRepository(ApolloErpLogger<OrderRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");

            this._logger = logger;
            className = this.GetType().Name;
        }


        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<OrderDO> GetOrder(string userId, string orderNo, bool useMaster = false)
        {
            var list = await GetListAsync("where user_id=@UserId and order_no=@OrderNo", new { UserId = userId, OrderNo = orderNo }, useMaster);
            return list?.FirstOrDefault();
        }

        public async Task<OrderDO> GetOrder(string orderNo, bool useMaster = false)
        {
            var list = await GetListAsync("where order_no=@OrderNo", new { OrderNo = orderNo }, useMaster);
            return list?.FirstOrDefault();
        }


        public async Task<List<ShopOrderDO>> GetSimpleOrderList(GetOrderListRequest request)
        {
            _logger.Info($"GetOrderNoList：{JsonConvert.SerializeObject(request)} ");
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            condition.AppendLine("  and create_time >=@startDate and create_time <= @endDate ");
            parameters.Add("@startDate", request.StartDate.ToString("yyyy-MM-dd"));
            parameters.Add("@endDate", request.EndDate.ToString("yyyy-MM-dd"));

            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (request.OrderNos != null && request.OrderNos.Any())
            {
                condition.AppendLine(" and order_no in @OrderNos");
                parameters.Add("@OrderNos", request.OrderNos);
            }

            var sql = @"select a.id AS Id,a.shop_id AS ShopId,a.order_no AS OrderNo, a.order_status AS OrderStatus,a.produce_type as ProduceType,
                        a.order_type AS OrderType, a.`actual_amount` as ActualAmount,a.install_status AS InstallStatus,a.install_time AS InstallTime, 
                        a.user_id AS UserId from `order`  a
                        where  is_deleted=0
                        " + condition.ToString();
            IEnumerable<ShopOrderDO> costTypeDos = null;
            await OpenConnectionAsync(async conn => costTypeDos = await conn.QueryAsync<ShopOrderDO>(sql, parameters));

            return costTypeDos?.ToList();
            
        }

        public async Task<List<OrderDO>> GetOrderList(GetOrderListRequest request)
        {
            _logger.Info($"GetOrderList：{JsonConvert.SerializeObject(request)} ");
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            condition.AppendLine("where  is_deleted=0  and create_time >=@startDate and create_time <= @endDate ");
            parameters.Add("@startDate", request.StartDate.ToString("yyyy-MM-dd"));
            parameters.Add("@endDate", request.EndDate.ToString("yyyy-MM-dd"));

            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (request.OrderNos != null && request.OrderNos.Any())
            {
                condition.AppendLine(" and order_no in @OrderNos");
                parameters.Add("@OrderNos", request.OrderNos);
            }

            _logger.Info($"GetOrderList：{JsonConvert.SerializeObject(condition)} ");
            var list = await GetListAsync(condition.ToString(), parameters, false);
            return list?.ToList();
        }

        // ---------------------------------- 私有方法 --------------------------------------

        public async Task<bool> UpdateCouponAmount(string orderNo, decimal totalCouponAmount, decimal actualAmount, string remark, string updateBy)
        {
            string sql = @"UPDATE `order` 
                        SET `total_coupon_amount`  = @TotalCouponAmount,
                        actual_amount = @ActualAmount,                
                        remark =  concat(remark, @Remark) ,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_no = @OrderNo ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    TotalCouponAmount = totalCouponAmount,
                    ActualAmount = actualAmount,
                    Remark = remark,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }


        /// <summary>
        /// 更新取消订单For预约或到店
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderCancelForReserverOrArrival(List<string> orderNos, string userId)
        {
            string sql = @"UPDATE `order` 
                            SET order_status = 40,update_by=@UpdateBy,update_time=NOW(3),is_occur_reverse=1,reverse_status=20,cancel_time=NOW(3)
                            WHERE
                                order_no in @OrderNos";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNos = orderNos, UpdateBy = userId });
            });
            return count > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool UpdateOrderNo(long orderId, string orderNo)
        {
            string sql = @"UPDATE `order` 
                            SET order_no = @OrderNo
                            WHERE
                                id = @OrderId";
            int count = 0;
            OpenConnection(async conn =>
            {
                count = conn.Execute(sql, new { OrderId = orderId, OrderNo = orderNo });
            });
            return count > 0;
        }

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderPayStatus(UpdateOrderPayStatusRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            if (request.PayChannel > 0 && request.PayMethod > 0)
            {
                builder.AppendLine("  ,pay_method=@PayMethod,pay_channel=@PayChannel");
            }
            string sql = @"UPDATE `order`
                             SET pay_status=1,money_arrive_status=1,pay_time=NOW(3),update_time=NOW(3),update_by=@UpdateBy" + builder.ToString() +
                            @" WHERE
                                order_no in @OrderNos and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNos = request.OrderNo,
                    UpdateBy = request.UpdateBy,
                    PayMethod = request.PayMethod,
                    PayChannel = request.PayChannel
                });
            });
            return count;
        }

        /// <summary>
        /// 更改订单的完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderCompleteStatus(BatchUpdateCompleteStatusRequest request)
        {
            string sql = @"UPDATE `order`
                             SET order_status=30,update_time=NOW(3),update_by=@UpdateBy
                            WHERE
                                order_no in @OrderNos and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNos = request.OrderNo,
                    UpdateBy = request.UpdateBy,
                });
            });
            return count;
        }

        /// <summary>
        /// 更新派工状态
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>

        public async Task<bool> UpdateOrderDispatchStatus(List<string> orderNos, string updateBy, int dispatchStatus = 1)
        {
            string sql = @"UPDATE `order`
                             SET dispatch_status=@DispatchStatus,dispatch_time=NOW(3),update_time=NOW(3),update_by=@UpdateBy
                            WHERE
                                order_no in @OrderNos and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNos = orderNos,
                    UpdateBy = updateBy,
                    DispatchStatus = dispatchStatus
                });
            });
            return count > 0;
        }

        /// <summary>
        /// 批量修改订单的安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request)
        {
            string sql = @"UPDATE `order`
                             SET update_time=NOW(3),update_by=@UpdateBy,install_status=1,install_time=NOW(3)
                            WHERE
                                order_no in @OrderNos and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNos = request.OrderNo,
                    UpdateBy = request.UpdateBy,
                });
            });
            return count;
        }

        public async Task<bool> UpdateOrderStockStatus(long orderId, sbyte stockStatus, string updateBy)
        {
            string sql = @"UPDATE `order` 
                        SET stock_status = @StockStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        id = @OrderId 
	                        AND stock_status <= @StockStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    StockStatus = stockStatus,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

        public async Task<bool> UpdateOrderNoAndInstallCode(long orderId, string orderNo, string installCode)
        {
            string sql = @"UPDATE `order` 
                            SET order_no = @OrderNo,
                                install_code = @InstallCode
                            WHERE
                                id = @OrderId";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderId = orderId, OrderNo = orderNo, InstallCode = installCode });
            });
            return count > 0;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        public async Task<bool> CancelOrder(string orderNo, string createBy)
        {
            string sql = @"UPDATE `order` 
                            SET order_status = 40,update_by=@UpdateBy,update_time=NOW(3),cancel_time=NOW(3)
                            WHERE
                                order_no = @OrderNo";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNo = orderNo, UpdateBy = createBy });
            });
            return count > 0;
        }

        public async Task<bool> UpdatePayStatus(string orderNo, sbyte payStatus, DateTime payTime, string updateBy, sbyte payMethod, sbyte payChannel)
        {
            string sql = @"UPDATE `order` 
                        SET pay_status = @PayStatus,
                        pay_time = @PayTime,
                        pay_method = @PayMethod,
                        pay_channel = @PayChannel,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_no = @OrderNo 
	                        AND pay_status <= @PayStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    PayStatus = payStatus,
                    PayTime = payTime,
                    PayMethod = payMethod,
                    PayChannel = payChannel,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

        public async Task<bool> UpdateMoneyArriveStatus(string orderNo, sbyte moneyArriveStatus, string updateBy)
        {
            string sql = @"UPDATE `order` 
                        SET money_arrive_status = @MoneyArriveStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_no = @OrderNo 
	                        AND money_arrive_status <= @MoneyArriveStatus";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    MoneyArriveStatus = moneyArriveStatus,
                    UpdateBy = updateBy
                });
            });
            return count > 0;

        }

        /// <summary>
        /// 更改订单的安装状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderInstallStatus(string orderNo, string updateBy)
        {
            string sql = @"UPDATE `order` 
                            SET install_status = @InstallStatus,update_by=@UpdateBy,update_time=Now(3),install_time=Now(3)
                            WHERE
                                order_no = @OrderNo";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    InstallStatus = InstallStatusEnum.HaveInstall.ToInt(),
                    UpdateBy = updateBy
                });
            });
            return count;
        }

        /// <summary>
        ///  更新订单的签收状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderSignStatus(string orderNo, string updateBy)
        {
            string sql = @"UPDATE `order` 
                            SET `sign_status` = @SignStatus,`update_by`=@UpdateBy,`update_time`=Now(3),`sign_time`=Now(3)
                            WHERE
                                order_no = @OrderNo AND sign_status=@WaitingSignStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    UpdateBy = updateBy,
                    SignStatus = 1,
                    WaitingSignStatus = 0
                });
            });
            return count;
        }

        /// <summary>
        /// 更改订单的完结状态状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderCompleteStatus(string orderNo, string updateBy)
        {
            string sql = @"UPDATE `order`
                             SET order_status=30,update_time=NOW(3),update_by=@UpdateBy
                            WHERE
                                order_no = @OrderNo and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    UpdateBy = updateBy,
                });
            });
            return count;
        }

        public async Task<long> UpdateOrderConfirmStatus(string orderNo, string updateBy)
        {
            var otherUpdateFields = new StringBuilder();

            otherUpdateFields.Append(",confirm_time = Now( 3 ) ");


            string sql = $@"UPDATE `order` 
                        SET order_status = @OrderStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        {otherUpdateFields.ToString()}
                        WHERE
	                        order_no = @OrderNo  
	                        AND order_status <= @OrderStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    OrderStatus = OrderStatusEnum.Confirmed.ToInt(),
                    UpdateBy = updateBy
                });
            });
            return count;
        }

        public async Task<PagedEntity<OrderDO>> GetOrderStaticReport(GetOrderDetailStaticReportRequest request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();

            var parameters = new DynamicParameters();

            DateTime.TryParse("2020-07-01", out var startCreateTime);
            bool startTime = false;
            bool endTime = false;

            if (!string.IsNullOrEmpty(request.StartTime))
            {
                startTime = DateTime.TryParse(request.StartTime, out var startOrderTime);
                if (startTime)
                {
                    parameters.Add("@StartTime", startOrderTime);
                }
            }

            if (!string.IsNullOrEmpty(request.EndTime))
            {
                endTime = DateTime.TryParse(request.EndTime, out var endOrderTime);
                if (endTime)
                {
                    parameters.Add("@EndTime", endOrderTime);
                }
            }

            var conditions = new StringBuilder();
            if (request.ShopIds != null && request.ShopIds.Any())
            {
                conditions.Append(" and shop_id in @shopIds");
                parameters.Add("@shopIds", request.ShopIds);
            }

            parameters.Add("@StartCreateTime", startCreateTime);

            if (startTime && endTime)
            {

                var list = await GetListPagedAsync(request.PageIndex, request.PageSize, "where is_deleted=0 and order_status!=40 and order_time>=@StartTime and order_time<=@EndTime and create_time>=@StartCreateTime "+conditions.ToString(), "order_time desc", parameters);

                return list;

            }
            return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));



        }

        public async Task<int> CheckUserFirstOrderForSpecialProduct(string userId, string productId)
        {
            string sql = @"select count(1)  from `order` A
                                    inner join order_product B
                                    ON A.order_no=B.order_no AND A.is_deleted=0
                                    where A.user_id=@UserId and product_id=@ProductId";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new
                {
                    UserId = userId,
                    ProductId = productId,
                });
            });
            return count;

        }

        public async Task<PagedEntity<OrderDO>> GetOrdersForOffice(GetOrdersForOfficeRequest request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();
            if (request.ShopId <= 0)
                return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));
            else
            {

                var parameters = new DynamicParameters();
                var builder = new StringBuilder();

                //  builder.AppendLine(" where B.is_deleted=0 and B.produce_type in ("+BuyProductTypeEnum.OfficeOrder.ToSbyte()+","+ BuyProductTypeEnum.ShopToSamllWarehouseOrder.ToSbyte()+" ) ");
                builder.AppendLine(" where B.is_deleted=0  ");
                parameters.Add("@ShopId", request.ShopId);


                string orderby = string.Empty;

                if (request.OrderBusinessStatus == OrderStatusForOfficeEnum.All)
                {
                    orderby = " B.Id desc";
                }

                if (request.OrderBusinessStatus == OrderStatusForOfficeEnum.WatingPay)
                {

                    builder.AppendLine(" and B.order_status=20");

                    builder.AppendLine(" and B.pay_status=@PayStatus");
                    parameters.Add("@PayStatus", (int)PayStatusEnum.NotPay);

                    orderby = " B.Id  desc";
                }

                if (request.OrderBusinessStatus == OrderStatusForOfficeEnum.Complete)
                {

                    builder.AppendLine(" and B.order_status=30");

                    orderby = " B.install_time DESC";

                }


                var sqlCount = @"select count(1) from delegate_user_order A
                                        INNER JOIN `order` B
                                        on A.order_no=B.order_no
                                        and A.shop_id=@ShopId  " + builder.ToString();


                var total = 0;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                });

                parameters.Add("@OrderBy", orderby);

                var sql = @" select B.order_no OrderNo,B.channel Channel,B.order_type OrderType,
                                        B.order_status OrderStatus,B.order_time OrderTime,
                                        B.total_product_num  TotalProductNum,B.actual_amount ActualAmount,
                                        B.pay_status PayStatus,B.sign_status SignStatus,B.install_time InstallTime  from delegate_user_order A
                                        INNER JOIN `order` B
                                        on A.order_no=B.order_no
                                        and A.shop_id=@ShopId  " + builder.ToString() + @"
                                        order by " + orderby + "  limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;


                IEnumerable<OrderDO> orderDos = null;
                await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<OrderDO>(sql, parameters));

                response.TotalItems = total;
                response.Items = orderDos.ToList();
                return response;


            }
        }

        public async Task<PagedEntity<GetOrderOutProductResponse>> GetOrderOutProducts(GetOrderOutProductsProfitRequest request)
        {
            PagedEntity<GetOrderOutProductResponse> response = new PagedEntity<GetOrderOutProductResponse>();


            var parameters = new DynamicParameters();
            var builder = new StringBuilder();

            //  builder.AppendLine(" where B.is_deleted=0 and B.produce_type in ("+BuyProductTypeEnum.OfficeOrder.ToSbyte()+","+ BuyProductTypeEnum.ShopToSamllWarehouseOrder.ToSbyte()+" ) ");
            builder.AppendLine(" where 1=1  ");
            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                builder.AppendLine(" and B.order_no=@OrderNo");
                parameters.Add("@OrderNo", request.OrderNo);
            }

            if (request.ShopIds!=null && request.ShopIds.Any())
            {
                builder.AppendLine(" and B.shop_id in @ShopIds");
                parameters.Add("@ShopIds", request.ShopIds);
            }


            if (!string.IsNullOrEmpty(request.StartCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.StartCreateTime, out var startOrderTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and B.order_time>=@OrderTime");
                    parameters.Add("@OrderTime", startOrderTime);
                }
            }

            if (!string.IsNullOrEmpty(request.EndCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.EndCreateTime, out var endDateTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and B.order_time<@EndTime");
                    parameters.Add("@EndTime", endDateTime);
                }
            }



            string orderby = "B.id desc";





            var sqlCount = @"select count(1) from `order` B
                                inner join order_product A
                                on B.order_no=A.order_no and A.is_deleted=0
                                and B.order_status=30
                                and A.product_attribute=5
                                left join order_car C
                                on B.id=c.order_id
                                and c.is_deleted=0 " + builder.ToString();
            ;


            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            parameters.Add("@OrderBy", orderby);

            var sql = @" select B.shop_id ShopId, B.order_no OrderNo,B.user_id UserId,B.user_phone UserPhone,A.product_id ProductId,A.product_name ProductName,A.total_number                  TotalNumber,A.amount Amount,A.total_amount SaleOrderPrice,
                                C.car_number CarNumber,CONCAT(c.brand,'|',c.vehicle,'|',c.nian,'|',c.pai_liang,c.sales_name) CarInfo,B.create_time CreateTime from `order` B
                                inner join order_product A
                                on B.order_no=A.order_no and A.is_deleted=0
                                and B.order_status=30
                                and A.product_attribute=5
                                left join order_car C
                                on B.id=c.order_id
                                and c.is_deleted=0 " + builder.ToString() + @"
                                order by " + orderby + "  limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;


            IEnumerable<GetOrderOutProductResponse> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetOrderOutProductResponse>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos.ToList();
            return response;



        }

        public async Task<PagedEntity<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfit(GetOrderOutProductsProfitRequest request)
        {
            PagedEntity<GetOrderOutProductsProfitResponse> response = new PagedEntity<GetOrderOutProductsProfitResponse>();


            var parameters = new DynamicParameters();
            var builder = new StringBuilder();

            //  builder.AppendLine(" where B.is_deleted=0 and B.produce_type in ("+BuyProductTypeEnum.OfficeOrder.ToSbyte()+","+ BuyProductTypeEnum.ShopToSamllWarehouseOrder.ToSbyte()+" ) ");
            builder.AppendLine(" where 1=1  ");
            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                builder.AppendLine(" and B.order_no=@OrderNo");
                parameters.Add("@OrderNo", request.OrderNo);
            }

            if (!string.IsNullOrEmpty(request.PurchaseNo))
            {
                builder.AppendLine(" and p.id =@PurchaseNo");
                parameters.Add("@PurchaseNo", request.PurchaseNo);
            }

            if (!string.IsNullOrEmpty(request.ProductId))
            {
                builder.AppendLine(" and A.product_id =@ProductId");
                parameters.Add("@ProductId", request.ProductId);
            }

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                builder.AppendLine(" and B.shop_id in @ShopIds");
                parameters.Add("@ShopIds", request.ShopIds);
            }


            if (!string.IsNullOrEmpty(request.StartCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.StartCreateTime, out var startOrderTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and B.order_time>=@OrderTime");
                    parameters.Add("@OrderTime", startOrderTime);
                }
            }

            if (!string.IsNullOrEmpty(request.EndCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.EndCreateTime, out var endDateTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and B.order_time<@EndTime");
                    parameters.Add("@EndTime", endDateTime);
                }
            }

            string orderby = " P.create_time desc, B.order_time desc";


            var sqlCount = @"select count(1) from `order` B
                                inner join order_product A   on B.order_no=A.order_no and A.is_deleted=0 and B.order_status=30 and A.product_attribute=5
								inner join shop_purchase.purchase_order_product pp on pp.product_code = A.product_id and pp.is_deleted=0
								inner join shop_purchase.purchase_order p on p.id = pp.po_id and p.purchase_type=2 and p.status=6
                                left join order_car C on B.id=c.order_id and c.is_deleted=0 " + builder.ToString();


            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            parameters.Add("@OrderBy", orderby);

            var sql = @" select B.shop_id ShopId,s.simple_name SimpleName,  B.order_no SaleOrder,B.actual_amount ActualAmount,B.create_time CreateTime ,B.user_phone UserPhone,B.user_name UserName,
A.product_id ProductId,A.product_name ProductName,A.price SalePrice,A.total_number TotalNumber,A.total_amount SaleOrderPrice,
p.vender_short_name VenderShortName,p.id PurchaseOrder,pp.price PurchasePrice,pp.num PurchaseNumber,pp.amount PurchaseTotalPrice,
                                C.car_number CarNumber,CONCAT(c.brand,'|',c.vehicle,'|',c.nian,'|',c.pai_liang,c.sales_name) CarInfo,
								i.can_use_num StockNum,P.create_time PurchaseTime
								from `order` B
                                inner join order_product A   on B.order_no=A.order_no and A.is_deleted=0 and B.order_status=30 and A.product_attribute=5
								inner join shop_purchase.purchase_order_product pp on pp.product_code = A.product_id and pp.is_deleted=0
								inner join shop_purchase.purchase_order p on p.id = pp.po_id and p.purchase_type=2 and p.status=6
                                left join shop_manage.shop s on s.id = B.shop_id and s.is_deleted=0
								left join shop_wms.inventory i on i.location_id = B.shop_id and i.product_id = A.product_id and i.is_deleted=0
                                left join order_car C on B.id=c.order_id and c.is_deleted=0 " + builder.ToString() + @"
                                order by " + orderby + "  limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;


            IEnumerable<GetOrderOutProductsProfitResponse> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetOrderOutProductsProfitResponse>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos.ToList();
            return response;

        }

        /// <summary>
        /// 经营月报报表查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopOrderDO>> GetShopOrderList(GetShopSalesMonthRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" and a.is_deleted=0 ");
            builder.AppendLine(" and a.shop_id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                bool isSuccessStart = DateTime.TryParse(request.StartDate, out var startDate);
                bool isSuccessEnd = DateTime.TryParse(request.EndDate, out var endDate);
                if (isSuccessStart && isSuccessEnd)
                {
                    builder.AppendLine($" and  a.install_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", endDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }
            //2022-7-21 不统计套餐卡核销，统计套餐卡销售
            var sql = @"select a.id AS Id,a.shop_id AS ShopId,a.order_no AS OrderNo, a.order_status AS OrderStatus,
                        case when a.produce_type = 14 then 7 else a.order_type end AS OrderType,
                        case when a.produce_type = 15 then c.avg_price * c.num else a.`actual_amount` end as ActualAmount,a.install_status AS InstallStatus,DATE_FORMAT(a.install_time,'%Y-%m-%d') AS InstallTime, 
                        a.user_id AS UserId from `order`  a
                        left join order_package_card c on a.order_no = c.verify_order_no 
                        where a.produce_type in (0,2,15,16,17,18,19,20)
                        and a.order_status=30
                        " + builder.ToString();
            IEnumerable<ShopOrderDO> costTypeDos = null;
            await OpenConnectionAsync(async conn => costTypeDos = await conn.QueryAsync<ShopOrderDO>(sql, parameters));

            return costTypeDos?.ToList();
        }

        public async Task<long> UpdateOrderChannel(string orderNo, int channel)
        {
            var otherUpdateFields = new StringBuilder();

            string sql = $@"UPDATE `order` 
                        SET channel = @Channel,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_no = @OrderNo ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    Channel = channel
                });
            });
            return count;
        }

        public async Task<long> UpdateOrderRemark(string orderNo, string remark)
        {
            var otherUpdateFields = new StringBuilder();

            string sql = $@"UPDATE `order` 
                        SET remark = CONCAT( remark ,' ', @Remark),
                        update_time = Now( 3 ) 
                        WHERE
	                        order_no = @OrderNo ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    Remark = remark
                });
            });
            return count;
        }
    }
}