using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.OrderCommentForApp
{
    /// <summary>
    /// 评论明细
    /// </summary>
    public class ReplyDetailRequest
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
