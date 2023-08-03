using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request.VideoComment
{
    /// <summary>
    /// LastCommentByVideoIdsRequest
    /// </summary>
    public class LastCommentByVideoIdsRequest
    {
        /// <summary>
        /// 视频id
        /// </summary>
        public List<long> VideoIdList { get; set; }
    }
}
