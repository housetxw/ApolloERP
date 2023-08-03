using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Dal.Model.Condition;
using Ae.OrderComment.Service.Dal.Model.Extend;

namespace Ae.OrderComment.Service.Dal.Repositorys.VideoComment
{
    public interface IVideoCommentRepository : IRepository<VideoCommentDO>
    {
        /// <summary>
        /// 视频评论列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<PagedEntity<VideoCommentDO>> GetVideoCommentPageList(VideoCommentPageListCondition condition);

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<VideoCommentDO>> GetVideoCommentByParentId(long parentId);

        Task<List<ReplyCountDo>> GetReplyCount(List<long> commentList);

        /// <summary>
        /// UpdateLikeNum
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="stepSize"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<int> UpdateLikeNum(long comment, int stepSize, string updateBy);

        /// <summary>
        /// 获取最近一次评论
        /// </summary>
        /// <param name="videoIds"></param>
        /// <returns></returns>
        Task<List<VideoCommentDO>> GetLastComment(List<long> videoIds);

        Task<List<VideoCommentDO>> GetVideoCommentCount(List<long> videoIds,int videoType);
    }
}
