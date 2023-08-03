using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model.VideoComment;
using Ae.OrderComment.Service.Core.Request.VideoComment;

namespace Ae.OrderComment.Service.Controllers
{
    /// <summary>
    /// 视频评论
    /// </summary>
    [Route("[controller]/[action]")]
    public class VideoCommentController : ControllerBase
    {
        private readonly IVideoCommentService _videoCommentService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="videoCommentService"></param>
        public VideoCommentController(IVideoCommentService videoCommentService)
        {
            _videoCommentService = videoCommentService;
        }

        /// <summary>
        /// 获取视频的评论列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<VideoCommentListVo>> GetVideoCommentPageList(
            [FromQuery] VideoCommentPageListRequest request)
        {
            var result = await _videoCommentService.GetVideoCommentPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VideoCommentListVo>>> GetChildVideoCommentList(
            [FromQuery] ChildVideoCommentListRequest request)
        {
            var result = await _videoCommentService.GetChildVideoCommentList(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddVideoComment([FromBody] AddVideoCommentRequest request)
        {
            var result = await _videoCommentService.AddVideoComment(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> LikeVideoComment([FromBody] LikeVideoCommentRequest request)
        {
            var result = await _videoCommentService.LikeVideoComment(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取视频最新的一条评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<VideoCommentListVo>>> GetLastCommentByVideoIds(
            [FromBody] LastCommentByVideoIdsRequest request)
        {
            var result = await _videoCommentService.GetLastCommentByVideoIds(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteVideoComment([FromBody] DeleteVideoCommentRequest request)
        {
            var result = await _videoCommentService.DeleteVideoComment(request);

            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiResult<List<VideoCommentCountRes>>> GetVideoCommentCount([FromBody] GetVideoCommentCountRequest request)
        {
            var result = await _videoCommentService.GetVideoCommentCount(request);

            return Result.Success(result);
        }
    }
}