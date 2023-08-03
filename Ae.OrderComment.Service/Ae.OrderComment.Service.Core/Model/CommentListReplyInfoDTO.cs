using Ae.OrderComment.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class CommentListReplyInfoDTO
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
        /// 显示标题，示例：用户69天后追评
        /// </summary>
        public string DisplayTitle { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 追评图片
        /// </summary>
        public List<string> ImgUrl { get; set; }
    }
}
