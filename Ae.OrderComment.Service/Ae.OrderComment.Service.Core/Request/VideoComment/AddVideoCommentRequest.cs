using Ae.OrderComment.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// AddVideoCommentRequest
    /// </summary>
    public class AddVideoCommentRequest
    {
        /// <summary>
        /// 一级评论id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 被回复评论id
        /// </summary>
        public long TargetId { get; set; }

        /// <summary>
        /// 视频id
        /// </summary>
        public long VideoId { get; set; }

        /// <summary>
        /// 视频类型：0摄像头 1录播视频
        /// </summary>
        public sbyte VideoType { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [Required(ErrorMessage = "回复内容不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 门店id（视频所属门店id）
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalTypeEnum TerminalType { get; set; }
    }
}
