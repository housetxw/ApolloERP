using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// DeleteVideoCommentRequest
    /// </summary>
    public class DeleteVideoCommentRequest
    {
        /// <summary>
        /// CommentId
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// CommentId
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
