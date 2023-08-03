using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.OrderProduct
{
    public interface IOrderProductRepository : IRepository<OrderProductDO>
    {
        /// <summary>
        /// 根据订单ID批量获取商品集合
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderProductDO>> GetOrderProductsByOrderIds(IEnumerable<long> orderIds);
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
        /// 根据用户ID获取购买过的商品ID集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<string>> GetPidsByUserId(string userId);
        /// <summary>
        /// 根据PID列表批量获取订单商品销量
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        Task<List<GetSalesByPidsDO>> GetSalesByPids(List<string> pids);
        /// <summary>
        /// 更新订单商品成本价
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderPid"></param>
        /// <param name="totalCostPrice"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateProductTotalCostPrice(long orderId, long orderPid, decimal totalCostPrice, string updateBy,string remark="");
    }
}
