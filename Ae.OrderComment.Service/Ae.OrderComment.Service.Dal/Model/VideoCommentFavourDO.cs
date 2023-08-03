using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
    /// <summary>
    /// VideoCommentFavourDO
    /// </summary>
    [Table("video_comment_favour")]
    public class VideoCommentFavourDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 评论id
        /// </summary>
        [Column("comment_id")]
        public long CommentId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 状态：1点赞 0取消点赞
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
