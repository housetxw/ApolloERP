using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public class OrderAddressRepository : AbstractRepository<OrderAddressDO>, IOrderAddressRepository
    {
        public OrderAddressRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<List<OrderAddressDO>> GetOrderAddress(List<long> orderIds)
        {
            var list = await GetListAsync("where order_id in @OrderIds", new { OrderIds = orderIds });
            return list?.ToList();
        }
    }
}
