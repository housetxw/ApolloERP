using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Model.VideoComment;
using Ae.OrderComment.Service.Core.Request.VideoComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Core.Interfaces
{
    /// <summary>
    /// IVideoCommentService
    /// </summary>
    public interface IVideoCommentService
    {
        /// <summary>
        /// 获取视频的评论列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<VideoCommentListVo>> GetVideoCommentPageList(VideoCommentPageListRequest request);

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <returns></returns>
        Task<List<VideoCommentListVo>> GetChildVideoCommentList(ChildVideoCommentListRequest request);

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddVideoComment(AddVideoCommentRequest request);

        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> LikeVideoComment(LikeVideoCommentRequest request);

        /// <summary>
        /// 获取视频最新的一条评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VideoCommentListVo>> GetLastCommentByVideoIds(LastCommentByVideoIdsRequest request);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteVideoComment(DeleteVideoCommentRequest request);
        Task<List<VideoCommentCountRes>> GetVideoCommentCount(GetVideoCommentCountRequest request);
    }
}
