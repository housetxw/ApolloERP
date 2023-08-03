using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Promotion;
using Ae.Shop.Api.Core.Request.Promotion;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 门店客户
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[Filter(nameof(PromotionController))]
    public class PromotionController : ControllerBase
    {
        /// <summary>
        /// 促销Service
        /// </summary>
        private readonly IPromotionService promotionService;

        /// <summary>
        /// 构造
        /// </summary>
        public PromotionController(IPromotionService promotionService)
        {
            this.promotionService = promotionService;
        }

        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PromotionActivityListVo>> SearchPromotionActivity([FromQuery]SearchPromotionActivityRequest request)
        {
            var result = await promotionService.SearchPromotionActivity(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<PromotionActivityDetailVo>> GetPromotionActivityDetail([FromQuery]PromotionActivityDetailRequest request)
        {
            var result = await promotionService.GetPromotionActivityDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="request">门店商品编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopProductVo>> GetShopProductByCode([FromQuery]ShopProductByCodeRequest request)
        {
            var result = await promotionService.GetShopProductByCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 商品搜索（根据Pid或商品名称）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopProductVo>>> SearchProductByNameOrCode([FromQuery]SearchProductByNameOrCodeRequest request)
        {
            var result = await promotionService.SearchProductByNameOrCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddPromotionActivity([FromBody]ApiRequest<AddPromotionActivityRequest> request)
        {
            ApiResult<string> result = new ApiResult<string>
            {
                Code = ResultCode.Success
            };
            result.Data = await promotionService.AddPromotionActivity(request.Data);

            return result;
        }

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> FinishPromotion([FromBody] ApiRequest<FinishPromotionRequest> request)
        {
            var result = await promotionService.FinishPromotion(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑促销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditPromotion([FromBody] ApiRequest<EditPromotionRequest> request)
        {
            var result = await promotionService.EditPromotion(request.Data);

            return Result.Success(result);
        }
    }
}