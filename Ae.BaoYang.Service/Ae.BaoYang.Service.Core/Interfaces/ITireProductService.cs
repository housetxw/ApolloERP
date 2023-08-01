using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Core.Interfaces
{
    /// <summary>
    /// 轮胎适配
    /// </summary>
    public interface ITireProductService
    {
        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TireServiceListResponse> GetTireCategoryListAsync(TireCategoryListRequest request);

        /// <summary>
        /// 轮胎适配列表 - 展示所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TireServiceListResponse> GetTireCategoryListAllProductAsync(TireCategoryListRequest request);

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<TireProductVo>, int>> GetTireListAsync(TireListRequest request);

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AdaptiveProductsResponse> VerifyAdaptiveProducts(VerifyTireProductRequest request);
    }
}
