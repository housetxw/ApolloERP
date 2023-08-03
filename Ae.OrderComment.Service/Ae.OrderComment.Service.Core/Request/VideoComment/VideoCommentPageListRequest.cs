using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// VideoCommentPageListRequest
    /// </summary>
    public class VideoCommentPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 视频id
        /// </summary>
        [Required(ErrorMessage = "视频id不能为空")]
        public long VideoId { get; set; }

        /// <summary>
        /// 视频类型：0 摄像头  1：录播视频
        /// </summary>
        public sbyte VideoType { get; set; }

        /// <summary>
        /// 用户id
        /// 判断当前用户是否已点赞评论
        /// </summary>
        public string UserId { get; set; }
    }

    public class GetVideoCommentCountRequest {

        public List<long> VideoIds { get; set; } = new List<long>();
        public sbyte VideoType { get; set; }

    }
}
