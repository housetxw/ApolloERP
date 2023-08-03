using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class ShopOrderRepository : AbstractRepository<OrderDO>, IShopOrderRepository
    {        
        // ---------------------------------- 变量和构造器 --------------------------------------
        public ShopOrderRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<OrderDO> GetOrder(string orderNo, bool useMaster = false)
        {
            var list = await GetListAsync("where order_no=@OrderNo", new { OrderNo = orderNo }, useMaster);
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
    }
}
