using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using Ae.C.MiniApp.Api.Client.Response.BaoYang;
using Ae.C.MiniApp.Api.Common.Exceptions;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IBaoYangClient
    {
        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangCategoryDto>>> GetBaoYangPackagesAsync(BaoYangPackagesClientRequest request);

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageProductDto>> SearchPackageProductsWithConditionAsync(
            SearchProductClientRequest request);

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TireServiceListClientResponse> GetTireCategoryListAsync(TireCategoryListClientRequest request);

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<TireProductDto>> GetTireListAsync(TireListClientRequest request);

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> BaoYangAdaptiveProduct(BaoYangAdaptiveProductRequest request);

        /// <summary>
        /// 轮胎商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> TireAdaptiveProduct(TireAdaptiveProductClientRequest request);

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyAdaptiveProductForBuyClientResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyClientRequest request);
    }
}
