using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Core.Interfaces
{
    /// <summary>
    /// Shop 的操作
    /// </summary>
    public interface IOrderQueryForShopService
    {
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request);
    }
}
