using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.VideoComment
{
    public class VideoCommentFavourRepository : AbstractRepository<VideoCommentFavourDO>, IVideoCommentFavourRepository
    {
        public VideoCommentFavourRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }

        /// <summary>
        /// 根据评论id批量获取点赞明细
        /// </summary>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentFavourDO>> GetVideoCommentFavourByCommentIds(List<long> commentIds)
        {
            var param = new DynamicParameters();
            param.Add("@commentIds", commentIds);
            param.Add("@status", 1);
            var result = await GetListAsync("WHERE comment_id IN @commentIds AND `status` = @status", param);
            return result?.AsList() ?? new List<VideoCommentFavourDO>();
        }

        /// <summary>
        /// GetVideoCommentFavourByCommentId
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentFavourDO>> GetVideoCommentFavourByCommentId(long commentId, string userId,
            bool readOnly = true)
        {
            var param = new DynamicParameters();
            param.Add("@commentId", commentId);
            param.Add("@userId", userId);
            param.Add("@status", 1);
            var result =
                await GetListAsync("WHERE comment_id = @commentId AND user_id = @userId AND `status` = @status", param,
                    !readOnly);
            return result?.AsList() ?? new List<VideoCommentFavourDO>();
        }
    }
}
