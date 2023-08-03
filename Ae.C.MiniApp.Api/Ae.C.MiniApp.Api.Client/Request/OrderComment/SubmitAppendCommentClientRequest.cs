using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class SubmitAppendCommentClientRequest : BaseOrderCommentSubmitDTO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 追评哪个评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 请求渠道
        /// </summary>
        public string Channel { get; set; }
    }
}
