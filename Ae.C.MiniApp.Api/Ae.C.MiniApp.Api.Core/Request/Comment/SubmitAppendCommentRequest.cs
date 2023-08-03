using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class SubmitAppendCommentRequest : BaseOrderCommentSubmitVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 追评哪个评论Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "评论ID必须大于0")]
        public long CommentId { get; set; }
    }
}
