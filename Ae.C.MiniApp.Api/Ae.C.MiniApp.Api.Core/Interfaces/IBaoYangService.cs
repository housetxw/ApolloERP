using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using Ae.C.MiniApp.Api.Core.Response.Adaptation;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaoYangService
    {
        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(GetBaoYangPackagesRequest request);

        /// <summary>
        /// 保养更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsWithCondition(
            SearchProductRequest request);

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetTireServiceListResponse> GetTireCategoryList(TireCategoryListRequest request);

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<TireProductVO>> GetTireListAsync(GetTireListRequest request);

        /// <summary>
        /// 校验商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyAdaptiveProductResponse> VerifyAdaptiveProduct(VerifyAdaptiveProductRequest request);

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyAdaptiveProductForBuyResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyRequest request);
    }
}
