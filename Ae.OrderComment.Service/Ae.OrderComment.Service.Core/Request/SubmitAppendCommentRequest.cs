using Ae.OrderComment.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class SubmitAppendCommentRequest : BaseOrderCommentSubmitDTO
    {
        /// <summary>
        /// 追评哪个评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 请求渠道
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
    }
}
