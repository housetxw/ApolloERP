using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("video_comment")]
    public class VideoCommentDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 视频id
        /// </summary>
        [Column("video_id")]
        public long VideoId { get; set; }

        /// <summary>
        /// 视频类型：0摄像头 1视频
        /// </summary>
        [Column("video_type")]
        public sbyte VideoType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 点赞数量
        /// </summary>
        [Column("like_num")]
        public int LikeNum { get; set; }

        /// <summary>
        /// 留言用户id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 回复/评论渠道（0未设置 1C端Android 2C端ios 3C端小程序  10B端Android 11B端Ios）
        /// </summary>
        [Column("terminal_type")]
        public sbyte TerminalType { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 指向目标id
        /// </summary>
        [Column("target_id")]
        public long TargetId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Column("check_status")]
        public sbyte CheckStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [Column("check_comment")]
        public string CheckComment { get; set; } = string.Empty;

        /// <summary>
        /// 审核人
        /// </summary>
        [Column("check_by")]
        public string CheckBy { get; set; } = string.Empty;

        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("check_time")]
        public DateTime CheckTime { get; set; } = new DateTime(1900, 1, 1);

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
