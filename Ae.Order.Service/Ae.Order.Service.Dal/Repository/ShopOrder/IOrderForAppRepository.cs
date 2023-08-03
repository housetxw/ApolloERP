using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Condition.ShopOrder;
using Ae.Order.Service.Dal.Model;

namespace Ae.Order.Service.Dal.Repository.ShopOrder
{
    public interface IOrderForAppRepository : IRepository<OrderDO>
    {
        /// <summary>
        /// 查询订单信息根据客户搜索条件（手机号、订单号、产品信息) For  App
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetMainOrdersForSearch(GetMainOrdersForSearchCondition request);

        /// <summary>
        /// 得到订单主信息ForApp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetMainOrdersForStatus(GetMainOrdersForStatusCondition request);
    }
}
