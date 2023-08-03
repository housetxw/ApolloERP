using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model.VideoComment
{
    /// <summary>
    /// VideoCommentListVo
    /// </summary>
    public class VideoCommentListVo
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// 视频id
        /// </summary>
        public long VideoId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 评论用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHeadUrl { get; set; }

        /// <summary>
        /// 评论点赞次数
        /// </summary>
        public int LikeNum { get; set; }

        /// <summary>
        /// 是否已赞
        /// </summary>
        public bool IsLike { get; set; }

        /// <summary>
        /// 是否农主评论
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 指向目标评论
        /// </summary>
        public VideoCommentListVo TargetComment { get; set; }
    }


    public class VideoCommentCountRes
    {
        public long VideoId { get; set; }

        public int Num { get; set; }
    }
}
