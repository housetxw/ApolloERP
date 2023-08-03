using System;
using System.Collections.Generic;
using System.Text;
using Ae.OrderComment.Service.Core.Enums;

namespace Ae.OrderComment.Service.Core.Model.OrderCommentForApp
{
    /// <summary>
    /// 评论列表回复信息
    /// </summary>
    public class CommentListReplyInfoVO
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 点评类型（使用到的枚举值：ReplyComment CustomerAppendComment ReplyAppendComment）
        /// </summary>
        public CommentTypeEnum CommentType { get; set; }
        /// <summary>
        /// 显示标题，示例：商家回复（1天后）
        /// </summary>
        public string DisplayTitle { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
    }
}
