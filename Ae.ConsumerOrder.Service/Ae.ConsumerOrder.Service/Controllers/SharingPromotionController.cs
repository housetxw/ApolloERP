using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Core.Response.SharingPromotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Controllers
{
    /// <summary>
    /// 分享有礼
    /// </summary>
    [Route("[controller]/[action]")]

    public class SharingPromotionController : ControllerBase
    {
        private readonly ISharingPromotionService _sharingPromotionService;

        public SharingPromotionController(ISharingPromotionService sharingPromotionService)
        {
            _sharingPromotionService = sharingPromotionService;
        }
        /// <summary>
        /// 得到分享摘要
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary([FromQuery]GetSharingSummaryRequest request)
        {
            return await _sharingPromotionService.GetSharingSummary(request);
        }

        /// <summary>
        /// 得到分享得规则说明
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetSharingRuleDescription()
        {
            return await Task.FromResult(new ApiResult<List<string>>()
            {
                Code = ResultCode.Success,
                Data = new List<string>()
                {
                    "通过分享产品详情页后引导注册的用户才能进入统计名单。",
                    "需要注册用户购买订单金额大于等于168元且订单状态是已完成的订单。",
                    "单个用户的多次购买行为符合条件的只会计算一次。",
                    "符合条件的订单数量大于等于10，当前用户才能免费获得一次价值为358元的4L机油+机滤的小保养套餐服务。",
                    "免费保养的活动支持叠加统计。",
                   " 4L保养套餐中超出部分的机油需要用户另外支付购买。"
                }
            });
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders([FromQuery]GetSharingOrdersRequest request)
        {
            return await _sharingPromotionService.GetSharingOrders(request);
        }

        [HttpGet]
        public async Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon([FromQuery]GetSharingCouponRequest request)
        {
            return await _sharingPromotionService.GetSharingCoupon(request);
        }
    }
}
