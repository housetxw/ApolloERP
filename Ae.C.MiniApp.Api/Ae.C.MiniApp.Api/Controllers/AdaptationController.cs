using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using Ae.C.MiniApp.Api.Core.Response.Adaptation;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 适配相关接口（保养 && 轮胎）
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AdaptationController))]
    [ApiController]
    public class AdaptationController : ControllerBase
    {
        private IBaoYangService _baoYangService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public AdaptationController(IBaoYangService baoYangService)
        {
            _baoYangService = baoYangService;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(
            ApiRequest<GetBaoYangPackagesRequest> request)
        {
            var result = await _baoYangService.GetBaoYangPackages(request.Data);

            return result;
        }

        /// <summary>
        /// 保养更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SearchProductModel<BaoYangPackageProductModel>>> SearchPackageProductsWithCondition(
            ApiRequest<SearchProductRequest> request)
        {
            var result = await _baoYangService.SearchPackageProductsWithCondition(request.Data);

            return Result.Success(result);
        }

        /*/// <summary>
        /// 保养切换安装方式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<BaoYangPackageModel>> GetBaoYangPackageByInstallType(
            ApiRequest<GetBaoYangPackageByInstallTypeRequest> request)
        {
            return await Task.FromResult(new ApiResult<BaoYangPackageModel>());
        }*/

        /// <summary>
        /// 轮胎服务适配页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetTireServiceListResponse>> GetTireCategoryList(
            ApiRequest<TireCategoryListRequest> request)
        {
            var result = await _baoYangService.GetTireCategoryList(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<TireProductVO>> GetTireListAsync(ApiRequest<GetTireListRequest> request)
        {
            var result = await _baoYangService.GetTireListAsync(request.Data);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 校验商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<VerifyAdaptiveProductResponse>> VerifyAdaptiveProduct(
            ApiRequest<VerifyAdaptiveProductRequest> request)
        {
            var result = await _baoYangService.VerifyAdaptiveProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<VerifyAdaptiveProductForBuyResponse>> VerifyAdaptiveProductForBuy(
            ApiRequest<VerifyAdaptiveProductForBuyRequest> request)
        {
            var result = await _baoYangService.VerifyAdaptiveProductForBuy(request.Data);

            return Result.Success(result);
        }
    }
}