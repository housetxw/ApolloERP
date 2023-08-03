using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// ChildVideoCommentListRequest
    /// </summary>
    public class ChildVideoCommentListRequest
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "评论id必须大于0")]
        public long CommentId { get; set; }

        /// <summary>
        /// 用户id
        /// 判断当前用户是否已点赞评论
        /// </summary>
        public string UserId { get; set; }
    }
}
