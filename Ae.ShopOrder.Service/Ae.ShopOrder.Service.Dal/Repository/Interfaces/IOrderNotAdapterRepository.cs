using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    /// <summary>
    /// 订单仓储
    /// </summary>
    public interface IOrderNotAdapterRepository : IRepository<OrderNotAdapterDO>
    {
        /// <summary>
        /// 得到不适配订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderNotAdapterDO> GetOrderNotAdapter(GetOrderNotAdapterRequest request);

        /// <summary>
        /// 保存不适配订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> SaveOrderNotAdapter(OrderNotAdapterDO request);

       

    }
}
