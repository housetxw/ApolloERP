using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.OrderProduct
{
    public class OrderProductRepository : AbstractRepository<OrderProductDO>, IOrderProductRepository
    {
        public OrderProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<IEnumerable<OrderProductDO>> GetOrderProductsByOrderIds(IEnumerable<long> orderIds)
        {
            var orderProducts = await GetListAsync("where order_id in @OrderIds", new { OrderIds = orderIds });
            return orderProducts;
        }

        public async Task<List<string>> GetPidsByUserId(string userId)
        {
            string sql = @"SELECT DISTINCT
	                            product_id 
                            FROM
	                            order_product AS op
	                            LEFT JOIN `order` AS o ON op.order_id = o.id 
                            WHERE
	                            product_attribute <> 2 
	                            AND o.user_id = @UserId";

            IEnumerable<string> list = new List<string>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<string>(sql, new
                {
                    UserId = userId
                });
            });
            return list?.ToList();
        }

        public async Task<List<GetSalesByPidsDO>> GetSalesByPids(List<string> pids)
        {
            string sql = @"SELECT
	                            product_id AS Pid,
	                            count( 0 ) AS Sales 
                            FROM
	                            order_product 
                            WHERE
	                            product_id IN @ProductIds 
                            GROUP BY
	                            product_id";

            IEnumerable<GetSalesByPidsDO> list = new List<GetSalesByPidsDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<GetSalesByPidsDO>(sql, new
                {
                    ProductIds = pids
                });
            });
            return list?.ToList();
        }

        public async Task<bool> UpdateProductStockStatus(long orderId, sbyte stockStatus, string updateBy, List<long> orderPids = null)
        {
            string sql = @"UPDATE order_product 
                        SET stock_status = @StockStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_id = @OrderId 
	                        AND stock_status <= @StockStatus";

            if (orderPids != null && orderPids.Any())
            {
                sql += @" AND order_product_id IN @OrderPids";
            }

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    StockStatus = stockStatus,
                    UpdateBy = updateBy,
                    OrderPids = orderPids
                });
            });
            return count > 0;
        }


        public async Task<bool> UpdateProductTotalCostPrice(long orderId, long orderPid, decimal totalCostPrice, string updateBy, string remark = "")
        {
            string sql = @"UPDATE order_product 
                        SET total_cost_price = @TotalCostPrice,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_id = @OrderId
                            AND order_product_id = @OrderPid";

            string updateOrderSql =
                "update `order` set remark=@Remark ,update_by=@UpdateBy,update_time=Now(3) where id=@OrderId and is_deleted=0 ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {

                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    OrderPid = orderPid,
                    TotalCostPrice = totalCostPrice,
                    UpdateBy = updateBy
                });

                if (!string.IsNullOrEmpty(remark))
                    await conn.ExecuteAsync(updateOrderSql, new
                    {
                        Remark = remark,
                        UpdateBy = updateBy,
                        OrderId = orderId,
                    });
            });
            return count > 0;
        }
    }
}
