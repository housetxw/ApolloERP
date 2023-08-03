using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Request.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;

namespace Ae.OrderComment.Service.Core.Interfaces
{
    /// <summary>
    /// /订单评论服务
    /// </summary>
    public interface IOrderCommentForAppService
    {
        /// <summary>
        /// 订单评论列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetCommentListResponse>> GetCommentList([FromQuery] GetCommentListRequest request);

        /// <summary>
        /// 点评明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ReplyDetailResponse>> ReplyDetail(ReplyDetailRequest request);

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ReplyComment([FromBody] ReplyCommentRequest request);

        /// <summary>
        /// 今日点评数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<ApiResult<GetTodayCommentResponse>> GetTodayCommentCount(int shopId);
    }
}
