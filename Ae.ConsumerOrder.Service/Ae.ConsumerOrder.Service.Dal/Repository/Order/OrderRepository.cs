using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{

    public class OrderRepository : AbstractRepository<OrderDO>, IOrderRepository
    {
        public OrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
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
        ///  更新订单的签收状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOrderSignStatus(string orderNo, string updateBy)
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
                    SignStatus = SignStatusEnum.HaveSign,
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
        public virtual async Task<long> UpdateOrderInstallStatus(string orderNo, string updateBy)
        {
            string sql = @"UPDATE `order` 
                            SET install_status = @InstallStatus,update_by=@UpdateBy,update_time=Now(3),install_time=Now(3),order_status=@OrderStatus
                            WHERE
                                order_no = @OrderNo AND install_status=@WaitingInstallStatus AND pay_status=@PayStatus";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNo = orderNo,
                    InstallStatus = (int)InstallStatusEnum.HaveInstall,
                    WaitingInstallStatus = (int)InstallStatusEnum.NotInstall,
                    PayStatus = (int)PayStatusEnum.HavePay,
                    OrderStatus = (int)OrderStatusEnum.Completed,
                    UpdateBy = updateBy
                });
            });
            return count;
        }

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
        /// 更改订单的对账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="UpdateBy"></param>
        /// <param name="reconciliationStatus"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOrderReconciliationStatus(List<string> OrderNos, string updateBy, int reconciliationStatus)
        {
            string sql = @"UPDATE `order` 
                            SET reconciliation_status = @ReconciliationStatus,update_by=@UpdateBy,update_time=Now(3),reconciliation_time=Now(3)
                            WHERE
                                order_no IN @OrderNos and pay_status=1 and money_arrive_status=1";

            long count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderNos = OrderNos,
                    ReconciliationStatus = reconciliationStatus,
                    UpdateBy = updateBy
                });
            });
            return count;
        }

        public async Task<OrderDO> GetOrderByInstallCode(string installCode)
        {
            var list = await GetListAsync("where install_code=@InstallCode", new { InstallCode = installCode });
            return list?.FirstOrDefault();
        }

        public async Task<bool> UpdateOrderReverseInfo(long orderId, sbyte reverseStatus, sbyte refundStatus, string updateBy, sbyte isOccurReverse = 1)
        {
            string sql = @"UPDATE `order` 
                        SET is_occur_reverse = @IsOccurReverse,
                        reverse_status = ( CASE reverse_status WHEN @ReverseStatus > reverse_status THEN @ReverseStatus ELSE reverse_status END ),
                        refund_status = ( CASE refund_status WHEN 0 THEN @RefundStatus ELSE refund_status END ),
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
                    UpdateBy = updateBy
                });
            });
            return count > 0;
        }

        /// <summary>
        /// 更新订单的产品成本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<long> UpdateOrderProductCost(UpdateOrderProductRequest request)
        {


            long count = 0;


            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    string sql = @"update order_product set total_cost_price=@TotalCost,update_by=@UpdateBy,update_time=Now(3)
                            where is_deleted=0 and order_no=@OrderNo and product_id=@ProductId";

                    string updateOrderSql =
                        "update `order` set remark=@Remark ,update_by=@UpdateBy,update_time=Now(3) where order_no=@OrderNO and is_deleted=0 ";

                    await conn.ExecuteAsync(sql, new
                    {
                        TotalCost = request.Cost,
                        UpdateBy = request.UpdateBy,
                        OrderNo = request.OrderNo,
                        ProductId = request.Pid
                    }, transaction: tran);

                    count = await conn.ExecuteAsync(updateOrderSql, new
                    {
                        Remark = request.Remark,
                        UpdateBy = request.UpdateBy,
                        OrderNo = request.OrderNo,
                    }, transaction: tran);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                }
            });


            return count;
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
                    DeliveryStatus = (int)DeliveryStatusEnum.HaveSend,
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

        public async Task<List<OrderDO>> GetUnpaidOrders(int hours = 23, int minutes = 30)
        {
            var list = await GetListAsync(@"WHERE order_status = 10 AND (pay_status = 0 AND pay_method = 1) AND order_time <= @OrderTime", new { OrderTime = DateTime.Now.AddHours(-hours).AddMinutes(-minutes) });
            return list?.ToList();
        }

        public async Task<List<OrderDO>> GetTimeOutUncanceledOrders(int hours = 24)
        {
            var list = await GetListAsync(@"WHERE order_status = 10 AND (pay_status = 0 AND pay_method = 1) AND order_time <= @OrderTime", new { OrderTime = DateTime.Now.AddHours(-hours) });
            return list?.ToList();
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

        public async Task<List<OrderDO>> GetOrders(List<string> orderNos)
        {
            var list = await GetListAsync("where order_no=@OrderNo", new { OrderNo = orderNos }, true);
            return list?.ToList();
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
                count =await conn.ExecuteScalarAsync<int>(sql, new
                {
                    UserId = userId,
                    ProductId = productId,
                });
            });
            return count;

        }
    }
}
