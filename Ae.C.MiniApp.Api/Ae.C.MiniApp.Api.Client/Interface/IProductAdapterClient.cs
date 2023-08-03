using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IProductAdapterClient
    {

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AdaptiveProductsClientResponse> VerifyAdaptiveBaoYangProducts(VerifyAdaptiveProductsClientRequest request);

        /// <summary>
        /// 轮胎商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AdaptiveProductsClientResponse> VerifyAdaptiveTireProducts(VerifyTireProductClientRequest request);
    }
}
