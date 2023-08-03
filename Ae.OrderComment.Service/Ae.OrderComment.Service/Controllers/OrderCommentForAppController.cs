using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Request.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;
using Ae.OrderComment.Service.Filters;

namespace Ae.OrderComment.Service.Controllers
{
    /// <summary>
    /// App评价
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommentForAppController))]
    public class OrderCommentForAppController : ControllerBase
    {
        public IOrderCommentForAppService _orderCommentForAppService;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="commentForAppService"></param>
        public OrderCommentForAppController(IOrderCommentForAppService commentForAppService)
        {
            _orderCommentForAppService = commentForAppService;
        }
        /// <summary>
        /// 获取评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetCommentListResponse>> GetCommentList([FromQuery]GetCommentListRequest request)
        {
            return await _orderCommentForAppService.GetCommentList(request);
        }

        /// <summary>
        /// 回评详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReplyDetailResponse>> ReplyDetail([FromQuery] ReplyDetailRequest request)
        {
            return await _orderCommentForAppService.ReplyDetail(request);
        }

        /// <summary>
        /// 回复评价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ReplyComment([FromBody]ReplyCommentRequest request)
        {
            return await _orderCommentForAppService.ReplyComment(request);
        }

        /// <summary>
        /// 今日评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetTodayCommentResponse>> GetTodayCommentCount(GetTodayCommentCountRequest request)
        {
            return await _orderCommentForAppService.GetTodayCommentCount(request.ShopId);
        }

    }
}