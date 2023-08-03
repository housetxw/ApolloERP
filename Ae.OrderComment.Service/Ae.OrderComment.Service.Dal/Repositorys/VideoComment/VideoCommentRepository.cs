using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Dal.Model.Condition;
using Ae.OrderComment.Service.Dal.Model.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.VideoComment
{
    public class VideoCommentRepository : AbstractRepository<VideoCommentDO>, IVideoCommentRepository
    {
        public VideoCommentRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }

        /// <summary>
        /// 视频评论列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<PagedEntity<VideoCommentDO>> GetVideoCommentPageList(VideoCommentPageListCondition condition)
        {
            var param = new DynamicParameters();
            param.Add("@videoId", condition.VideoId);
            param.Add("@videoType", condition.VideoType);
            var result = await GetListPagedAsync(condition.PageIndex, condition.PageSize,
                "WHERE video_id = @videoId AND video_type = @videoType AND parent_id = 0 AND check_status = 1",
                "`id` DESC", param);
            return result;
        }

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentDO>> GetVideoCommentByParentId(long parentId)
        {
            var param = new DynamicParameters();
            param.Add("@parentId", parentId);
            var result = await GetListAsync("WHERE parent_id = @parentId AND check_status = 1", param);
            return result?.AsList() ?? new List<VideoCommentDO>();
        }

        public async Task<List<ReplyCountDo>> GetReplyCount(List<long> commentList)
        {
            List<ReplyCountDo> result = new List<ReplyCountDo>();
            string sql = @"SELECT
	parent_id AS ParentId,
	COUNT( * ) AS Count 
FROM
	video_comment 
WHERE
	parent_id IN @parentId 
	AND check_status = 1 
	AND is_deleted = 0 
GROUP BY
	parent_id;";

            var param = new DynamicParameters();
            param.Add("@parentId", commentList);
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ReplyCountDo>(sql, param))?.AsList() ?? new List<ReplyCountDo>();
            });
            return result;
        }

        /// <summary>
        /// UpdateLikeNum
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="stepSize"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<int> UpdateLikeNum(long comment, int stepSize, string updateBy)
        {
            string sql = @"UPDATE video_comment 
SET like_num = like_num + @stepSize,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	id = @commentId;";

            var param = new DynamicParameters();
            param.Add("@stepSize", stepSize);
            param.Add("@updateBy", updateBy);
            param.Add("@commentId", comment);
            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, param); });
            return result;
        }

        /// <summary>
        /// 获取最近一次评论
        /// </summary>
        /// <param name="videoIds"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentDO>> GetLastComment(List<long> videoIds)
        {
            string sql = @"SELECT
	id AS Id,
	video_id AS VideoId,
	video_type AS VideoType,
	content AS Content,
	like_num AS LikeNum,
	user_id AS UserId,
	shop_id AS ShopId,
	terminal_type AS TerminalType,
	parent_id AS ParentId,
	target_id AS TargetId,
    create_time AS CreateTime
FROM
	video_comment 
WHERE
	id IN (
SELECT
	MAX( id ) 
FROM
	video_comment 
WHERE
	video_id IN @videoIds
	AND video_type = 0 
	AND parent_id = 0 
	AND check_status = 1 
	AND is_deleted = 0 
GROUP BY
	video_id 
	);";

            var param = new DynamicParameters();
            param.Add("@videoIds", videoIds);

            List<VideoCommentDO> result = new List<VideoCommentDO>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<VideoCommentDO>(sql, param))?.ToList() ??
                         new List<VideoCommentDO>();
            });

            return result;
        }

        public async Task<List<VideoCommentDO>> GetVideoCommentCount(List<long> videoIds, int videoType)
        {
            var sql = @"SELECT
	                    video_id VideoId,
	                    count( 1 ) LikeNum 
                    FROM
	                    video_comment 
                    WHERE
	                    video_id IN @videoIds 
	                    AND video_type = @video_type 
	                    AND is_deleted = 0 
	                    AND parent_id = 0 
                    GROUP BY
	                    video_id ";

            var videoComments = new List<VideoCommentDO>();
            var param = new DynamicParameters();
            param.Add("@video_type", videoType);

            param.Add("@videoIds", videoIds);

            await OpenSlaveConnectionAsync(async conn =>
            {
                videoComments = (await conn.QueryAsync<VideoCommentDO>(sql, param))?.ToList() ??
                         new List<VideoCommentDO>();
            });

            return videoComments;
        }
    }
}
