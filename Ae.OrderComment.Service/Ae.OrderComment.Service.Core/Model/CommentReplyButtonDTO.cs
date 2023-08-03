using Ae.OrderComment.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class CommentReplyButtonDTO
    {
        /// <summary>
        /// 回复类型（使用到的枚举值：ReplyComment ReplyAppendComment）
        /// </summary>
        public CommentTypeEnum ReplyType { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 回复给的评论Id
        /// </summary>
        public long ReplyToCommentId { get; set; }
    }
}
