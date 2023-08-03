using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.OrderComment.Service.Core.Enums;

namespace Ae.OrderComment.Service.Core.Request.OrderCommentForApp
{
    /// <summary>
    /// 评论提交
    /// </summary>
    public class ReplyCommentRequest
    {
        /// <summary>
        /// 回复类型（使用到的枚举值：ReplyComment ReplyAppendComment）
        /// </summary>
        [Required]
        public CommentTypeEnum ReplyType { get; set; }
        /// <summary>
        /// 回复给的评论Id
        /// </summary>
        [Required]
        public long ReplyToCommentId { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        [Required]
        [StringLength(500)]
        public string ReplyContent { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
