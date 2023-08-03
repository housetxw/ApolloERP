using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class AppendCommentRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 追评哪个评论Id
        /// </summary>
        public long CommentId { get; set; }
    }
}
