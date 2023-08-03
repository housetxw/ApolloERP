using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public class OrderAddressRepository : AbstractRepository<OrderAddressDO>, IOrderAddressRepository
    {
        public OrderAddressRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderAddressDO> GetOrderAddress(long orderId)
        {
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }
    }
}
