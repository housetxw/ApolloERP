using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public interface IOrderAddressRepository : IRepository<OrderAddressDO>
    {
        Task<OrderAddressDO> GetOrderAddress(long orderId);
    }
}
