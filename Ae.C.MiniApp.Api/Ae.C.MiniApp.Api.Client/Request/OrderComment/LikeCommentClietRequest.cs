using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class LikeCommentClietRequest
    {
        /// <summary>
        /// 点赞哪个评论Id
        /// </summary>
        public long CommentId { get; set; }
    }
}
