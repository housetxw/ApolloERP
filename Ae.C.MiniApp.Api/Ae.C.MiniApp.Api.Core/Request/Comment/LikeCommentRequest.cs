using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class LikeCommentRequest
    {
        /// <summary>
        /// 点赞哪个评论Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "评论ID必须大于0")]
        public long CommentId { get; set; }
    }
}
