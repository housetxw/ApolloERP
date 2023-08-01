using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Filters;

namespace Ae.BaoYang.Service.Controllers
{
    /// <summary>
    /// 保养适配相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(BaoYangController))]
    public class BaoYangController : ControllerBase
    {
        private readonly IBaoYangService _baoYangService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baoYangService"></param>
        public BaoYangController(IBaoYangService baoYangService)
        {
            _baoYangService = baoYangService;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangCategory>>> GetBaoYangPackagesAsync(
            [FromBody] GetBaoYangPackagesRequest request)
        {
            var result = await _baoYangService.GetBaoYangPackagesAsync(request);

            return result;
        }

        /// <summary>
        /// 保养适配首页接口 - 返回所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangPackageModel>>> GetBaoYangPackagesAllProductAsync(
            [FromBody] GetBaoYangPackagesRequest request)
        {
            var result = await _baoYangService.GetBaoYangPackagesAllProductAsync(request);

            return result;
        }

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<BaoYangPackageProductModel>>
            SearchPackageProductsWithConditionAsync([FromBody] SearchProductRequest request)
        {
            var result = await _baoYangService.SearchPackageProductsWithConditionAsync(request);

            return Result.Success(result.Products.ToList(), result.Products.Count());
        }

        /// <summary>
        /// 筛选器接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<FilterCategory>>> GetFilterConditionsAsync(
            [FromBody] SearchProductRequest request)
        {
            return await Task.FromResult(new ApiResult<List<FilterCategory>>());
        }

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPartsResponse>>> GetBaoYangParts()
        {
            var result = await _baoYangService.GetBaoYangParts();

            return Result.Success(result);
        }

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AdaptiveProductsResponse>> VerifyAdaptiveProducts(
            [FromBody] VerifyAdaptiveProductsRequest request)
        {
            var result = await _baoYangService.VerifyAdaptiveProducts(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<VerifyAdaptiveProductForBuyResponse>> VerifyAdaptiveProductForBuy(
            [FromBody] VerifyAdaptiveProductForBuyRequest request)
        {
            var result = await _baoYangService.VerifyAdaptiveProductForBuy(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据配件类型 和 车型适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<PartProductRefVo>>> GetAdaptiveProductByPartType(
            [FromBody] AdaptiveProductByPartTypeRequest request)
        {
            var result = await _baoYangService.GetAdaptiveProductByPartType(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据CarId 和 配件类型 适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<PartProductRefVo>>> GetAdaptiveProductByPartTypeAndCarId(
            [FromBody] AdaptiveProductByPartTypeAndCarIdRequest request)
        {
            var result = await _baoYangService.GetAdaptiveProductByPartTypeAndCarId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 切换安装方式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<BaoYangPackageModel>> GetBaoYangPackageByInstallTypeAsync(
            [FromBody] GetBaoYangPackageByInstallTypeRequest request)
        {
            return await Task.FromResult(new ApiResult<BaoYangPackageModel>());
        }

        /// <summary>
        /// 刷新Redis缓存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> RefreshRedisAsync([FromBody] RefreshRedisRequest request)
        {
            var result = await _baoYangService.RefreshRedis(request.Key);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ServiceTypeEnumVo>>> GetServiceTypeEnum()
        {
            var result = await _baoYangService.GetServiceTypeEnum();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取BaoYangType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangTypeConfigVo>>> GetBaoYangTypeConfig()
        {
            var result = await _baoYangService.GetBaoYangTypeConfig();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据产品带出服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<InstallServiceByProductResponse>> GetInstallServiceByProduct(
            [FromBody] InstallServiceByProductRequest request)
        {
            var result = await _baoYangService.GetInstallServiceByProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询服务费加价
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<InstallServiceDetailVo>>> GetAdditionalPriceByServiceId(
            [FromBody] AdditionalPriceByServiceIdRequest request)
        {
            var result = await _baoYangService.GetAdditionalPriceByServiceId(request);

            return Result.Success(result);
        }
    }
}