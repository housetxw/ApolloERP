using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class LikeCommentRequest
    {
        /// <summary>
        /// 点赞哪个评论Id
        /// </summary>
        public long CommentId { get; set; }
    }
}
