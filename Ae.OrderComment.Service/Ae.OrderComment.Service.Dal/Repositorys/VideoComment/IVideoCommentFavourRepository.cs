using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;

namespace Ae.OrderComment.Service.Dal.Repositorys.VideoComment
{
    public interface IVideoCommentFavourRepository : IRepository<VideoCommentFavourDO>
    {
        /// <summary>
        /// 根据评论id批量获取点赞明细
        /// </summary>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        Task<List<VideoCommentFavourDO>> GetVideoCommentFavourByCommentIds(List<long> commentIds);

        /// <summary>
        /// GetVideoCommentFavourByCommentId
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<VideoCommentFavourDO>> GetVideoCommentFavourByCommentId(long commentId, string userId,
            bool readOnly = true);
    }
}
