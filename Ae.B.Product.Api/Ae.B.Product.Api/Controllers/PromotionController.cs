using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Model.Promotion;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Request.Promotion;
using Ae.B.Product.Api.Core.Response;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// 促销活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PromotionActivityListVo>> SearchPromotionActivity([FromQuery]SearchPromotionActivityRequest request)
        {
            var result = await _promotionService.SearchPromotionActivity(request);

            return Result.Success(result.Items, result.TotalItems);
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
            result.Data = await _promotionService.AddPromotionActivity(request.Data);

            return result;
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PromotionDetailVo>> GetPromotionDetail([FromQuery]PromotionDetailRequest request)
        {
            var result = await _promotionService.GetPromotionDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopProductVo>> GetShopProductByCode(string shopProductCode)
        {
            var result = await _promotionService.GetShopProductByCode(shopProductCode);

            return Result.Success(result);
        }

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> FinishPromotion([FromBody] ApiRequest<FinishPromotionRequest> request)
        {
            var result = await _promotionService.FinishPromotion(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AuditPromotionActivity([FromBody]ApiRequest<AuditPromotionActivityRequest> request)
        {
            var result = await _promotionService.AuditPromotionActivity(request.Data);

            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetCouponActivityListForShopResponse>> GetCouponActivityListForShop(
          [FromBody] ApiRequest<GetCouponActivityListForShopRequest> request)
        {

            return await _promotionService.GetCouponActivityListForShop(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> SaveShopGrantCoupon([FromBody] ApiRequest<ShopGrantCouponDTO> request)
        {
            
            return await _promotionService.SaveShopGrantCoupon(request);
        }
    }
}