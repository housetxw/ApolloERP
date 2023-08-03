using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetOrderCommentForReplyRequest : GetOrderCommentBaseRequest
    {
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 技师级别
        /// </summary>
        public string TechLevel { get; set; } = string.Empty;

        public int Type { get; set; }

    }
}
