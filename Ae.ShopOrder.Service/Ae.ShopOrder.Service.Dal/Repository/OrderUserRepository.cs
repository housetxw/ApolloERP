using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderUserRepository: AbstractRepository<OrderUserDO>, IOrderUserRepository
    {
        public OrderUserRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderUserDO> GetOrderUser(long orderId)
        {
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }
    }
}
