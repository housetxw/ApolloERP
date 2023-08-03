using System;
using Ae.Order.Service.Dal.Model;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Request.Order;
using System.Data;
using Ae.Order.Service.Common.Extension;
using Ae.Order.Service.Dal.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Response.Order;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public class OrderRepository : AbstractRepository<OrderDO>, IOrderRepository
    {
        public OrderRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderDO> GetOrderByNo(string orderNo)
        {
            var list = await GetListAsync("where order_no=@OrderNo", new { OrderNo = orderNo });
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// 得到订单详情根据productIds
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public async Task<List<OrderProductDO>> GetOrderDetails(List<string> orderNos)
        {
            if (orderNos == null || !orderNos.Any())
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();
                condition.AppendLine(" where order_no IN @OrderNos and is_deleted=0");
                parameters.Add("@OrderNos", orderNos);

                var getOrderProducts = await GetListAsync<OrderProductDO>(condition.ToString(), parameters, false);
                return getOrderProducts.ToList();
            }
        }


        /// <summary>
        /// 得到订单的基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderDO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
            var condition = new StringBuilder("where 1=1");
            var parameters = new DynamicParameters();
            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_Id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (request.OrderNos != null && request.OrderNos.Any())
            {
                condition.AppendLine(" and order_no IN @OrderNos");
                parameters.Add("@OrderNos", request.OrderNos);
            }
            if (request.OrderIds != null && request.OrderIds.Any())
            {
                condition.AppendLine(" and id IN @OrderIds");
                parameters.Add("@OrderIds", request.OrderIds);
            }
            if (request.InstallCodes != null && request.InstallCodes.Any())
            {
                condition.AppendLine(" and install_code IN @InstallCode");
                parameters.Add("@InstallCode", request.InstallCodes);
            }

            if ((request.OrderNos?.Count() ?? 0) == 0 && (request.OrderIds?.Count() ?? 0) == 0 && (request.InstallCodes?.Count() ?? 0) == 0)
            {
                return new List<OrderDO>();
            }

            var getOrders = await GetListAsync<OrderDO>(condition.ToString(), parameters, false);
            return getOrders.ToList();

        }

        /// <summary>
        /// 根据用户查用户订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderDO>> GetOrderInfoByUserId(GetOrdersByUserIdRequest request)
        {
            if (request.ShopId == 0)
                return default(List<OrderDO>);
            var condition = new StringBuilder("where is_deleted = 0 ");
            var parameters = new DynamicParameters();
            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_Id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                condition.AppendLine(" and user_id = @UserId");
                parameters.Add("@UserId", request.UserId);
            }

            if (request.BeginOrderTime < new DateTime(2000, 1, 1))
            {
                request.BeginOrderTime = DateTime.Now.AddYears(-1);
            }
            condition.AppendLine(" and order_time > @BeginOrderTime");
            parameters.Add("@BeginOrderTime", request.BeginOrderTime);

            var getOrders = await GetListAsync<OrderDO>(condition.ToString(), parameters, false);
            return getOrders.ToList();

        }


        /// <summary>
        /// 得到订单的基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderDO>> GetOrderBaseInfoForBusinessStatus(
            GetOrderBaseInfoForBusinessStatusRequest request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();
            if (request.ShopId <= 0)
                return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));
            else
            {
                var parameters = new DynamicParameters();
                var builder = new StringBuilder();
                builder.AppendLine("where is_deleted=0");
                if (request.Channels != null && request.Channels.Any())
                {
                    builder.AppendLine(" and channel in @Channel");
                    parameters.Add("@Channel", request.Channels);
                }
                builder.AppendLine(" and shop_Id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);
                builder.AppendLine(" and delivery_type=@DeliveryType");
                parameters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop);


                if (!string.IsNullOrEmpty(request?.OtherSqlWhere))
                {
                    builder.AppendLine(" and ( " + request?.OtherSqlWhere  + ")");
                }

                if (!string.IsNullOrEmpty(request?.OrderNo))
                {
                    builder.AppendLine(" and order_no=@OrderNo");
                    parameters.Add("@OrderNo", request.OrderNo.Trim());
                }
                if (!string.IsNullOrEmpty(request?.Telephone))
                {
                    builder.AppendLine(" and user_phone=@Telephone");
                    parameters.Add("@Telephone", request?.Telephone?.Trim());
                }

                if (!string.IsNullOrEmpty(request.StartOrderTime))
                {
                    bool isSuccess = DateTime.TryParse(request.StartOrderTime, out var startOrderTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and order_time>=@OrderTime");
                        parameters.Add("@OrderTime", startOrderTime);
                    }
                }

                if (!string.IsNullOrEmpty(request.EndOrderTime))
                {
                    bool isSuccess = DateTime.TryParse(request.EndOrderTime, out var endDateTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and order_time<@EndTime");
                        parameters.Add("@EndTime", endDateTime);
                    }
                }


                if (!string.IsNullOrEmpty(request.StarInstallTime))
                {
                    bool isSuccess = DateTime.TryParse(request.StarInstallTime, out var startInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and install_time>=@StartInstallTime");
                        parameters.Add("@StartInstallTime", startInstalledTime);
                    }
                }
                if (!string.IsNullOrEmpty(request.EndInstallTime))
                {
                    bool isSuccess = DateTime.TryParse(request.EndInstallTime, out var endInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and install_time<@EndInstallTime");
                        parameters.Add("@EndInstallTime", endInstalledTime);
                    }
                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingSign)
                {

                    builder.AppendLine(" and order_status=@OrderStatus");
                    parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                    builder.AppendLine(" and delivery_status=@DeliveryStatus");
                    parameters.Add("@DeliveryStatus", (int)DeliveryStatusEnum.HaveSend);

                    builder.AppendLine(" and sign_status=@SignStatus");
                    parameters.Add("@SignStatus", (int)SignStatusEnum.NotSign);

                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "delivery_time desc",
                        parameters, false);
                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingDispatch)
                {
                    builder.AppendLine(" and order_status=@OrderStatus");
                    parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                    builder.AppendLine(" and sign_status=@SignStatus");
                    parameters.Add("@SignStatus", (int)SignStatusEnum.HaveSign);

                    builder.AppendLine("and dispatch_status=@DispatchStatus");
                    parameters.Add("@DispatchStatus", DispatchStatusEnum.NotDispatch);

                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "sign_time desc",
                        parameters, false);

                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.ABuilding)
                {
                    builder.AppendLine(" and order_status=@OrderStatus");
                    parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                    builder.AppendLine("and dispatch_status=@DispatchStatus");
                    parameters.Add("@DispatchStatus", (int)DispatchStatusEnum.HaveDispatch);

                    builder.AppendLine("and pay_status=@PayStatus");
                    parameters.Add("@PayStatus", (int)PayStatusEnum.NotPay);

                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "dispatch_time desc",
                        parameters, false);

                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Installed)
                {
                    builder.AppendLine("and install_status=@InstallStatus");
                    parameters.Add("@InstallStatus", (int)InstallStatusEnum.HaveInstall);


                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "install_time desc",
                        parameters, false);

                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Canceled)
                {
                    builder.AppendLine(" and order_status=@OrderStatus");
                    parameters.Add("@OrderStatus", (int)OrderStatusEnum.Canceled);

                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "cancel_time desc",
                        parameters, false);
                }

                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingReconciliation)
                {
                    List<int> productTyps = new List<int>() { 
                        ProductTypeEnum.DelegateCompanyDepositOrder.ToInt(), 
                        ProductTypeEnum.DelegateShopDepositOrder.ToInt(),
                        ProductTypeEnum.BuyVerification.ToInt(),
                        ProductTypeEnum.DelegatePay.ToInt(),
                        ProductTypeEnum.PayGoods.ToInt(), 
                        ProductTypeEnum.BuyPackageCard.ToInt(), 
                        ProductTypeEnum.MonthReconciliationOrder.ToInt() };
                    builder.AppendLine(" and produce_type not in @ProductType");
                    parameters.Add("@ProductType", productTyps);

                    builder.AppendLine(" and pay_status=@PayStatus");
                    parameters.Add("@PayStatus", (int)PayStatusEnum.HavePay);

                    builder.AppendLine(" and money_arrive_status=@MoneyArriveStatus");
                    parameters.Add("@MoneyArriveStatus", (int)MoneyArriveStatusEnum.Arrive);

                    builder.AppendLine(" and install_status=@InstallStatus");
                    parameters.Add("@InstallStatus", (int)InstallStatusEnum.HaveInstall);

                    builder.AppendLine(" and reconciliation_status=@ReconciliationStatus");
                    parameters.Add("@ReconciliationStatus", (int)ReconciliationStatusEnum.NotReconciliation);

                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "pay_time desc",
                        parameters, false);
                }


                if (request.OrderBusinessStatus == OrderBusinessStatusEnum.All)
                {
                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                        parameters, false);
                }

                return response;




            }
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
                            SET sign_status = @SignStatus,update_by=@UpdateBy,update_time=Now(3),sign_time=Now(3)
                            WHERE
                                order_no = @OrderNo AND sign_status=@WaitingSignStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    SignStatus = SignStatusEnum.HaveSign,
                    UpdateBy = updateBy,
                    WaitingSignStatus = SignStatusEnum.NotSign
                });
            });
            return count;
        }

        /// <summary>
        /// 更新订单的安装状态
        /// </summary>
        /// <param name="orderId"></param>
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

        public async Task<bool> UpdateOrderMainStatus(long orderId, sbyte orderStatus, string updateBy)
        {
            var otherUpdateFields = new StringBuilder();
            switch (orderStatus)
            {
                case (sbyte)OrderStatusEnum.Confirmed:
                    otherUpdateFields.Append(",confirm_time = Now( 3 ) ");
                    break;
                case (sbyte)OrderStatusEnum.Canceled:
                    otherUpdateFields.Append(",cancel_time = Now( 3 ) ");
                    break;
                default:
                    break;
            }

            string sql = $@"UPDATE `order` 
                        SET order_status = @OrderStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        {otherUpdateFields.ToString()}
                        WHERE
	                        id = @OrderId 
	                        AND order_status <= @OrderStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    OrderStatus = orderStatus,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
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

        public async Task<bool> UpdatePayStatus(long orderId, sbyte payStatus, DateTime payTime, string updateBy, sbyte payMethod, sbyte payChannel)
        {
            string sql = @"UPDATE `order` 
                        SET pay_status = @PayStatus,
                        pay_time = @PayTime,
                        pay_method = @PayMethod,
                        pay_channel = @PayChannel,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        id = @OrderId 
	                        AND pay_status <= @PayStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    PayStatus = payStatus,
                    PayTime = payTime,
                    PayMethod = payMethod,
                    PayChannel = payChannel,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

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

        public async Task<bool> UpdateMoneyArriveStatus(long orderId, sbyte moneyArriveStatus, string updateBy)
        {
            string sql = @"UPDATE `order` 
                        SET money_arrive_status = @MoneyArriveStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        id = @OrderId 
	                        AND money_arrive_status <= @MoneyArriveStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    MoneyArriveStatus = moneyArriveStatus,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

        /// <summary>
        /// 核对账单金额
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<List<GetAccountCheckAmountDO>> GetAccountCheckAmount(List<string> OrderNos)
        {
            if (OrderNos == null || !OrderNos.Any())
                return null;
            else
            {
                var sql = @"select A.order_no AS OrderNo,A.actual_amount AS ActualAmount,
                SUM(B.total_amount) AS ServiceFee,SUM(B.total_cost_price) AS TotalCost, 0 AS OtherFee,
                0 AS CommissionFee, SUM(B.total_cost_price) AS SettlementFee from `order` A
                    INNER JOIN order_product B
                    ON A.order_no = B.order_no
                AND A.is_deleted = 0 AND B.is_deleted = 0
                AND A.order_no IN @OrderNOs
               GROUP BY A.order_no ,A.actual_amount";

                var paramters = new DynamicParameters();
                paramters.Add("@OrderNOs", OrderNos);

                IEnumerable<GetAccountCheckAmountDO> orderDos = null;
                await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetAccountCheckAmountDO>(sql, paramters));
                return orderDos?.ToList();

            }
        }

        /// <summary>
        /// 更改订单的对账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <param name="reconciliationStatus"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderReconciliationStatus(List<string> orderNos, string updateBy, int reconciliationStatus)
        {
            string sql = @"UPDATE `order` 
                            SET reconciliation_status = @ReconciliationStatus,update_by=@UpdateBy,update_time=Now(3),reconciliation_time=Now(3)
                            WHERE
                                order_no IN @orderNos and pay_status=1 and money_arrive_status=1";

            long count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    orderNos = orderNos,
                    ReconciliationStatus = reconciliationStatus,
                    UpdateBy = updateBy
                });
            });
            return count;
        }

        /// <summary>
        /// 统计门店未对账的订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetStatisticsDO>> GetNoReconciliationOrderNum(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select shop_id AS ShopId, Count(1) AS StatisticsNum from `order`
                                where shop_id IN @ShopIds
                                and pay_status=@PayStatus and money_arrive_status=@MoneyArriveStatus
                                AND reconciliation_status=@ReconciliationStatus and is_deleted=0
                                and delivery_type=@DeliveryType and install_status=@InstallStatus
                                and produce_type not in (1,8,9,12,13,14,23) GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@PayStatus", PayStatusEnum.HavePay.ToInt());
                paramters.Add("@MoneyArriveStatus", MoneyArriveStatusEnum.Arrive.ToInt());
                paramters.Add("@ReconciliationStatus", ReconciliationStatusEnum.NotReconciliation.ToInt());
                //   paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend.ToInt());
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop.ToInt());
                paramters.Add("@InstallStatus", InstallStatusEnum.HaveInstall.ToInt());

                IEnumerable<GetStatisticsDO> orderDos = null;
                await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetStatisticsDO>(sql, paramters));
                return orderDos?.ToList();

            }
        }

        /// <summary>
        /// 待签收的订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetStatisticsDO>> GetWaitingSignOrderNum(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select shop_id AS ShopId, Count(1) AS StatisticsNum from `order`
                            where shop_id IN (@ShopIds)
                            and sign_status=@SignStatus and order_status=@OrderStatus
                            and delivery_status=@DeliveryStatus and delivery_type=@DeliveryType and is_deleted=0 
                            and ( produce_type !=@BuyVerification AND produce_type !=@UseVerification )
                            and channel IN(1,2) GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@SignStatus", SignStatusEnum.NotSign.ToInt());
                paramters.Add("@OrderStatus", OrderStatusEnum.Confirmed.ToInt());
                paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend.ToInt());
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop.ToInt());
                paramters.Add("@BuyVerification", OrderProductTypeEnum.BuyVerification.ToInt());
                paramters.Add("@UseVerification", OrderProductTypeEnum.UseVerification.ToInt());

                IEnumerable<GetStatisticsDO> orderDos = null;
                await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetStatisticsDO>(sql, paramters));
                return orderDos?.ToList();

            }
        }

        /// <summary>
        /// 待安装的订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetStatisticsDO>> GetWaitingInstallOrderNum(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select shop_id AS ShopId, Count(1) AS StatisticsNum from `order`
                            where shop_id IN (@ShopIds)
                            and order_status=@OrderStatus
                            and sign_status=@SignStatus  and pay_status=@PayStatus and is_deleted=0
                            and install_status=@InstallStatus
                            and delivery_type=@DeliveryType
                            and ( produce_type !=@BuyVerification AND produce_type !=@UseVerification )
                            and channel IN(1,2) GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@OrderStatus", OrderStatusEnum.Confirmed.ToInt());
                paramters.Add("@SignStatus", SignStatusEnum.HaveSign.ToInt());
                paramters.Add("@PayStatus", PayStatusEnum.HavePay.ToInt());
                paramters.Add("@InstallStatus", InstallStatusEnum.NotInstall.ToInt());
                // paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend.ToInt());
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop.ToInt());
                paramters.Add("@BuyVerification", OrderProductTypeEnum.BuyVerification.ToInt());
                paramters.Add("@UseVerification", OrderProductTypeEnum.UseVerification.ToInt());

                IEnumerable<GetStatisticsDO> orderDos = null;
                await OpenConnectionAsync(async conn =>
                    orderDos = await conn.QueryAsync<GetStatisticsDO>(sql, paramters));
                return orderDos?.ToList();
            }
        }

        /// <summary>
        /// 异常对账的订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetStatisticsDO>> GetExceptionReconciliationOrderNum(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select shop_id AS ShopId, Count(1) AS StatisticsNum from `order`
                                where shop_id IN @ShopIds
                                and pay_status=@PayStatus and money_arrive_status=@MoneyArriveStatus
                                AND reconciliation_status=@ReconciliationStatus and is_deleted=0
                                and delivery_type=@DeliveryType
                                and channel IN(1,2) GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@PayStatus", PayStatusEnum.HavePay.ToInt());
                paramters.Add("@MoneyArriveStatus", MoneyArriveStatusEnum.Arrive.ToInt());
                paramters.Add("@ReconciliationStatus", ReconciliationStatusEnum.ExceptionReconciliation.ToInt());
                //  paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend.ToInt());
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop.ToInt());

                IEnumerable<GetStatisticsDO> orderDos = null;
                await OpenConnectionAsync(async conn =>
                orderDos = await conn.QueryAsync<GetStatisticsDO>(sql, paramters));
                return orderDos?.ToList();
            }
        }

        /// <summary>
        /// 得到取消订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetStatisticsDO>> GetCanceledOrderNum(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select shop_id AS ShopId, Count(1) AS StatisticsNum from `order`
                                where shop_id IN @ShopIds
                                AND order_status=@OrderStatus and is_deleted=0
                                and delivery_type=@DeliveryType
                                and(produce_type != @BuyVerification AND produce_type != @UseVerification)
                                and channel IN(1,2)  GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@OrderStatus", OrderStatusEnum.Canceled.ToInt());
                //paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend.ToInt());
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop.ToInt());
                paramters.Add("@BuyVerification", OrderProductTypeEnum.BuyVerification.ToInt());
                paramters.Add("@UseVerification", OrderProductTypeEnum.UseVerification.ToInt());


                IEnumerable<GetStatisticsDO> orderDos = null;
                await OpenConnectionAsync(async conn =>
                orderDos = await conn.QueryAsync<GetStatisticsDO>(sql, paramters));
                return orderDos?.ToList();
            }
        }

        /// <summary>
        /// 得到未对账订单的金额
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<GetNoReconciliationAmountDO>> GetNoReconciliationAmount(List<long> shopIds)
        {
            if (shopIds == null || !shopIds.Any())
                return null;
            else
            {
                var sql = @"select A.shop_id AS ShopId,
                            SUM(B.total_amount) AS ServiceFee,SUM(B.total_cost_price) AS TotalCost, 0 AS SettlementFee
                            from `order` A
                                INNER JOIN order_product B
                                ON A.id = B.order_id
                            AND A.is_deleted = 0 AND B.is_deleted = 0
                            AND B.product_attribute = 2
                            AND A.shop_Id IN @ShopIds
                                and A.pay_status=@PayStatus and A.money_arrive_status=@MoneyArriveStatus
                                AND A.reconciliation_status=@ReconciliationStatus 
                                and A.delivery_status=@DeliveryStatus and A.delivery_type=@DeliveryType
                                and A.produce_type not in (1,8,9,12,13,14,23)
                           GROUP BY A.shop_id ";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", shopIds);
                paramters.Add("@PayStatus", PayStatusEnum.HavePay);
                paramters.Add("@MoneyArriveStatus", MoneyArriveStatusEnum.Arrive);
                paramters.Add("@ReconciliationStatus", ReconciliationStatusEnum.NotReconciliation);
                paramters.Add("@DeliveryStatus", DeliveryStatusEnum.HaveSend);
                paramters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop);

                IEnumerable<GetNoReconciliationAmountDO> orderDos = null;
                await OpenConnectionAsync(async conn =>
                    orderDos = await conn.QueryAsync<GetNoReconciliationAmountDO>(sql, paramters));
                return orderDos?.ToList();
            }
        }

        public async Task<PagedEntity<OrderDO>> GetUninstalledOrderList(GetUninstalledOrderListRequest request)
        {
            var pageList = await GetListPagedAsync(request.PageIndex, request.PageSize,
                "where user_id=@UserId and order_status=20 and is_need_install=1 and sign_status=1 and install_status=0 and reserve_status=0",
                "id desc", new { request.UserId });
            return pageList;
        }

        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reserveStatus"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderReserveStatus(long orderId, sbyte reserveStatus, DateTime reserveTime)
        {
            string sql = @"UPDATE `order` 
                        SET reserve_status = @ReserveStatus,
                        reserve_time =@ReserveTime
                        WHERE
	                        id = @OrderId";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    ReserveStatus = reserveStatus,
                    ReserveTime = reserveTime
                });
            });
            return count > 0;
        }

        public async Task<bool> UpdateOrderReverseInfo(long orderId, sbyte reverseStatus, sbyte refundStatus, string updateBy, decimal refundAmount = 0, sbyte isOccurReverse = 1)
        {
            //reverse_status = (CASE reverse_status WHEN @ReverseStatus > reverse_status THEN @ReverseStatus ELSE reverse_status END ),
            //            refund_status = (CASE refund_status WHEN 0 THEN @RefundStatus ELSE refund_status END ),
            string sql = @"UPDATE `order` 
                        SET is_occur_reverse = @IsOccurReverse,
                        reverse_status =  @ReverseStatus ,
                        refund_status =  @RefundStatus ,
                        refund_amount = refund_amount + @RefundAmount,
                        refund_time = Now( 3 ) ,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        id = @OrderId";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    IsOccurReverse = isOccurReverse,
                    ReverseStatus = reverseStatus,
                    RefundStatus = refundStatus,
                    RefundAmount = refundAmount,
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

        /// <summary>
        /// 更新订单的配送状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderDeliveryStatus(string orderNo, string updateBy)
        {
            string sql = @"UPDATE `order` 
                            SET delivery_status = @DeliveryStatus,update_by=@UpdateBy,update_time=Now(3),delivery_time=Now(3)
                            WHERE
                                order_no = @OrderNo";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    DeliveryStatus = DeliveryStatusEnum.HaveSend.ToInt(),
                    UpdateBy = updateBy
                });
            });
            return count;
        }

        /// <summary>
        /// 更新订单的点评状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<long> UpdateOrderCommentStatus(string orderNo, string updateBy, int commentStatus)
        {
            string sql = @"UPDATE `order` 
                            SET comment_status = @CommentStatus,update_by=@UpdateBy,update_time=Now(3)
                            WHERE
                                order_no = @OrderNo";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    CommentStatus = commentStatus,
                    UpdateBy = updateBy
                });
            });
            return count;
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

        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<BatchGetOrderCountByShopIdDO>> BatchGetOrderCountByShopId(List<long> shopIds)
        {
            string sql = @"SELECT
	                            shop_id ShopId,
	                            count( 0 ) OrderCount 
                            FROM
	                            `order` 
                            WHERE
	                            shop_id IN @ShopIds 
                            AND ( pay_status = 1 AND order_status >= 20 ) 
                            GROUP BY
	                            shop_id";

            IEnumerable<BatchGetOrderCountByShopIdDO> list = null;
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<BatchGetOrderCountByShopIdDO>(sql, new
                {
                    ShopIds = shopIds
                });
            });
            return list?.ToList();
        }

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request)
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
        /// 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request)
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
        /// 更新订单的派工状态
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

        public async Task<long> UpdateOrderCompleteStatus(UpdateCompleteStatusRequest request)
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
                    OrderNo = request.OrderNo,
                    UpdateBy = request.UpdateBy,
                });
            });
            return count;
        }

        /// <summary>
        /// 得到未支付的订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderDO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();

            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1");

            builder.AppendLine(" and pay_status=@PayStatus");
            parameters.Add("@PayStatus", PayStatusEnum.NotPay.ToInt());

            builder.AppendLine(" and stock_status=@StockStatus");
            parameters.Add("@StockStatus", StockStatusEnum.HavedStock);

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                bool isSuccess = DateTime.TryParse(request.StartDate, out var startTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and create_time<=@StartTime");
                    parameters.Add("@StartTime", startTime);
                }
            }
            else
            {
                var startTime = DateTime.Now.AddDays(-3);
                builder.AppendLine(" and create_time<=@StartTime");
                parameters.Add("@StartTime", startTime);
            }

            response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                parameters, false);

            return response;
        }

        public async Task<GetOrderCompleteStaticReportResponse> GetOrderCompleteStaticReport(GetOrderCompleteStaticReportRequest request)
        {
            GetOrderCompleteStaticReportResponse data = new GetOrderCompleteStaticReportResponse();

            DateTime.TryParse("2020-07-01", out var startCreateTime);
            string sql = @"select SUM(actual_amount) OrderAmount,COUNT(order_no) OrderNum  from `order` where  is_deleted=0 and order_status=30 and order_time>=@StartTime and order_time<=@EndTime and create_time>=@StartCreateTime";


            var parameters = new DynamicParameters();

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
                parameters.Add("@shopIds", request.ShopIds);
                conditions.Append(" and shop_id in @shopIds");
            }
            sql += conditions.ToString();

            parameters.Add("@StartCreateTime", startCreateTime);

            if (startTime && endTime)
                await OpenConnectionAsync(async conn =>
                    {
                        data = await conn.QueryFirstOrDefaultAsync<GetOrderCompleteStaticReportResponse>(sql, parameters);
                    });
            return await Task.FromResult(data);
        }

        public async Task<GetOrderNotCompleteStaticReportResponse> GetOrderNotCompleteStaticReport(GetOrderNotCompleteStaticReportRequest request)
        {
            GetOrderNotCompleteStaticReportResponse data = new GetOrderNotCompleteStaticReportResponse();


            DateTime.TryParse("2020-07-01", out var startCreateTime);
            string sql = @"select SUM(actual_amount) OrderAmount,COUNT(order_no) OrderNum  from `order` where is_deleted=0 and  order_status!=30 and order_status!=40 and order_time>=@StartTime and order_time<=@EndTime  and create_time>=@StartCreateTime";

            var conditions = new StringBuilder();

            var parameters = new DynamicParameters();

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

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                parameters.Add("@shopIds",request.ShopIds);
                conditions.Append(" and shop_id in @shopIds");
            }

            parameters.Add("@StartCreateTime", startCreateTime);

            sql += conditions.ToString();
            if (startTime && endTime)
                await OpenConnectionAsync(async conn =>
            {
                data = await conn.QueryFirstOrDefaultAsync<GetOrderNotCompleteStaticReportResponse>(sql, parameters);
            });
            return await Task.FromResult(data);
        }

        public async Task<List<GetPackageCardMainInfoResponse>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request)
        {

            var sql = @"select a.order_no orderNo,a.create_time CreateTime,a.actual_amount ActualAmount,b.package_name Name from `order` a
                                inner join order_package_card b
                                on a.order_no=b.source_order_no
                                and a.is_deleted=0 and b.is_deleted=0
                                where a.produce_type=14 and a.user_id=@UserId
                                GROUP BY a.order_no ,a.create_time ,a.actual_amount ,b.package_name";

            var paramters = new DynamicParameters();
            paramters.Add("@UserId", request.UserId);


            IEnumerable<GetPackageCardMainInfoResponse> orderDos = null;
            await OpenConnectionAsync(async conn =>
                orderDos = await conn.QueryAsync<GetPackageCardMainInfoResponse>(sql, paramters));
            return orderDos?.ToList();

        }

    }
}
