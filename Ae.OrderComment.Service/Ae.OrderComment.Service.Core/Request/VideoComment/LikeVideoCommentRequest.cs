using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// LikeVideoCommentRequest
    /// </summary>
    public class LikeVideoCommentRequest
    {
        /// <summary>
        /// 评论id
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 状态：1点赞 0取消点赞
        /// </summary>
        public sbyte Status { get; set; }
    }
}
