using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public interface IOrderProductRepository : IRepository<OrderProductDO>
    {
        /// <summary>
        /// 获取订单商品
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<List<OrderProductDO>> GetOrderProducts(long orderId);
        /// <summary>
        /// 更新商品库存状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="stockStatus"></param>
        /// <param name="updateBy"></param>
        /// <param name="orderPids"></param>
        /// <returns></returns>
        Task<bool> UpdateProductStockStatus(long orderId, sbyte stockStatus, string updateBy, List<long> orderPids = null);
        /// <summary>
        /// 更新订单商品成本价
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderPid"></param>
        /// <param name="totalCostPrice"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateProductTotalCostPrice(long orderId, long orderPid, decimal totalCostPrice, string updateBy);

        Task<List<OrderProductDO>> GetOrderProducts(List<string> orderNos);
    }
}
