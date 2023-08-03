using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Response.SharingPromotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 分享
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
        //[AllowAnonymous]
        public async Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary([FromQuery]GetSharingSummaryRequest request)
        {
            return await _sharingPromotionService.GetSharingSummary(request);
        }

        /// <summary>
        /// 得到分享得规则说明
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<string>>> GetSharingRuleDescription()
        {
            return await _sharingPromotionService.GetSharingRuleDescription();
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders([FromQuery]GetSharingOrdersRequest request)
        {
            return await _sharingPromotionService.GetSharingOrders(request);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon([FromQuery]GetSharingCouponRequest request)
        {
            return await _sharingPromotionService.GetSharingCoupon(request);
        }
    }
}
