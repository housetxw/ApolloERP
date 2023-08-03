using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public interface IOrderCarRepository: IRepository<OrderCarDO>
    {
        Task<List<OrderCarDO>> GetOrderCars(List<long> orderIds);
        Task<OrderCarDO> GetOrderCar(long orderId);
    }
}
