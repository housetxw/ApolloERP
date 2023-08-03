using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public class OrderProductRepository : AbstractRepository<OrderProductDO>, IOrderProductRepository
    {
        public OrderProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<List<OrderProductDO>> GetOrderProducts(long orderId)
        {
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.ToList();
        }

        public async Task<List<OrderProductDO>> GetOrderProducts(List<string> orderNos)
        {
            var list = await GetListAsync("where order_no in @OrderNos", new { OrderNos = orderNos });
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
	                        AND stock_status < @StockStatus AND  product_attribute=0";

            if (orderPids != null && orderPids.Any())
            {
                sql += @" AND id IN @OrderPids";
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

        public async Task<bool> UpdateProductTotalCostPrice(long orderId, long orderPid, decimal totalCostPrice, string updateBy)
        {
            string sql = @"UPDATE order_product 
                        SET total_cost_price = @TotalCostPrice,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_id = @OrderId
                            AND id = @OrderPid";

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
            });
            return count > 0;
        }
    }
}
