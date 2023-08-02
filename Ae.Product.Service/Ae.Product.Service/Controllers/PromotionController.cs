using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model.Promotion;
using Ae.Product.Service.Core.Request.Promotion;
using Ae.Product.Service.Filters;

namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// 促销活动
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(PromotionController))]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// 促销列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PromotionBriefVo>> GetPromotionList([FromQuery]PromotionListRequest request)
        {
            var result = await _promotionService.GetPromotionList(request);

            return Result.Success(result.Items, result.TotalItems);
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
        /// 结束促销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> FinishPromotion([FromBody]FinishPromotionRequest request)
        {
            var result = await _promotionService.FinishPromotion(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 商品促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductPromotionDetailVo>> GetProductPromotionDetail([FromQuery] ProductPromotionDetailRequest request)
        {
            var result = await _promotionService.GetProductPromotionDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 活动列表查询
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
        public async Task<ApiResult<string>> AddPromotionActivity([FromBody]AddPromotionActivityRequest request)
        {
            ApiResult<string> result = new ApiResult<string>
            {
                Code = ResultCode.Success
            };

            result.Data = await _promotionService.AddPromotionActivity(request);

            return result;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AuditPromotionActivity([FromBody]AuditPromotionActivityRequest request)
        {
            var result = await _promotionService.AuditPromotionActivity(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductPromotionInfoVo>>> GetPromotionActivitByProductCodeList([FromBody]PromotionActivitByProductCodeListRequest request)
        {
            var result = await _promotionService.GetPromotionActivitByProductCodeList(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 更新促销商品数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InsertPromotionActivityOrder([FromBody]InsertPromotionActivityOrderRequest request)
        {
            var result = await _promotionService.InsertPromotionActivityOrder(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<PromotionActivityDetailVo>> GetPromotionActivityDetail([FromQuery]PromotionActivityDetailRequest request)
        {
            var result = await _promotionService.GetPromotionActivityDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据订单号查询商品参加的活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductActivityVo>>> GetProductActivityByOrderNos(
            [FromBody] ProductActivityByOrderNosRequest request)
        {
            var result = await _promotionService.GetProductActivityByOrderNos(request);

            return Result.Success(result);
        }
    }
}