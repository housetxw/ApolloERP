using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Filters;

namespace Ae.BaoYang.Service.Controllers
{
    /// <summary>
    /// 轮胎适配商品相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(TireProductController))]
    public class TireProductController : ControllerBase
    {
        private readonly ITireProductService _tireProductService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tireProductService"></param>
        public TireProductController(ITireProductService tireProductService)
        {
            _tireProductService = tireProductService;
        }

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TireServiceListResponse>> GetTireCategoryListAsync(
            [FromBody] TireCategoryListRequest request)
        {
            var result = await _tireProductService.GetTireCategoryListAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<TireProductVo>> GetTireListAsync([FromBody] TireListRequest request)
        {
            var result = await _tireProductService.GetTireListAsync(request);
            return Result.Success(result.Item1, result.Item2);
        }

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AdaptiveProductsResponse>> VerifyAdaptiveProducts(
            [FromBody] VerifyTireProductRequest request)
        {
            var result = await _tireProductService.VerifyAdaptiveProducts(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 轮胎适配列表 - 展示所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TireServiceListResponse>> GetTireCategoryListAllProductAsync(
            [FromBody] TireCategoryListRequest request)
        {
            var result = await _tireProductService.GetTireCategoryListAllProductAsync(request);

            return Result.Success(result);
        }
    }
}