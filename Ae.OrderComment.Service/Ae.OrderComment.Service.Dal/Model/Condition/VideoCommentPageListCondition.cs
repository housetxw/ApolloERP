using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model.Condition
{
    /// <summary>
    /// VideoCommentPageListCondition
    /// </summary>
    public class VideoCommentPageListCondition
    {
        /// <summary>
        /// 视频id
        /// </summary>
        public long VideoId { get; set; }

        /// <summary>
        /// 视频类型
        /// </summary>
        public sbyte VideoType { get; set; }


        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
