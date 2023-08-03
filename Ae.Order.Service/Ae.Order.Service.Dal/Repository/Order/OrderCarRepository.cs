using System;
using ApolloErp.Data.DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Repository.Interfaces;

namespace Ae.Order.Service.Dal.Repository
{
    public class OrderCarRepository : AbstractRepository<OrderCarDO>, IOrderCarRepository
    {
        public OrderCarRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderCarDO> GetOrderCar(long orderId)
        {
            var list = await GetListAsync("where is_deleted=0 and order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }

        public async Task<List<OrderCarDO>> GetOrderCars(List<long> orderIds)
        {
            var list = await GetListAsync("where is_deleted=0 and order_id in @OrderIds", new { OrderIds = orderIds });
            return list?.ToList();
        }
    }
}
