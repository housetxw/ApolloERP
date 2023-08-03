using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Dal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.ConsumerOrder
{
    public interface IOrderForMiniAppRepository : IRepository<OrderDO>
    {
        Task<PagedEntity<OrderDO>> GetOrderListForMiniApp(GetOrderListForMiniAppRequest request);
        Task<int> GetOrderCountForMiniApp(GetOrderCountForMiniAppRequest request);
        Task<List<OrderDO>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request);
    }
}
