using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Activity;
using Ae.C.MiniApp.Api.Core.Response.Activity;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 活动
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ActivityController))]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        /// <summary>
        /// H5推广文章查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<H5PromoteArticleQueryResponse>> H5PromoteArticleQuery([FromQuery]H5PromoteArticleQueryRequest request)
        {
            return await activityService.H5PromoteArticleQuery(request);
        }

        /// <summary>
        /// 解析微信小程序场景参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<object>> GetWxacodeScene([FromQuery]GetWxacodeSceneRequest request)
        {
            /// 返回参数为动态对象，建议使用?表达式获取属性并判空
            /// 示例1：new { PromoteContentType = 1, ShopId = 7 }
            /// 示例2：new { PromoteContentType = 1, ShopId = 7, EmployeeId = "46661238-70a0-482d-aa16-bbd710880257" }
            return await activityService.GetWxacodeScene(request);
        }

        /// <summary>
        /// 分享推广内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SharePromotionContent([FromBody] ApiRequest<SharePromotionContentRequest> request)
        {
            return await activityService.SharePromotionContent(request);
        }
    }
}
