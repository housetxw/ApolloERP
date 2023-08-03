using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
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
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// 得到订单车型信息
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public async Task<List<OrderCarDO>> GetOrderCars(List<long> orderIds)
        {
            if (orderIds != null && !orderIds.Any())
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();
                condition.AppendLine(" where order_id IN @OrderId");

                parameters.Add("@OrderId", orderIds);

                var getOrderCars = await GetListAsync<OrderCarDO>(condition.ToString(), parameters, false);
                return getOrderCars.ToList();
            }
        }
    }
}
