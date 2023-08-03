using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    /// <summary>
    /// 库存客户端
    /// </summary>
    public interface IStockClient
    {
        /// <summary>
        /// 发送占库请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SendOrderUseStock(SendOrderUseStockClientRequest request);
        /// <summary>
        /// 发起订单释放库存请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SendOrderReleaseStock(SendOrderReleaseStockClientRequest request);
    }
}
