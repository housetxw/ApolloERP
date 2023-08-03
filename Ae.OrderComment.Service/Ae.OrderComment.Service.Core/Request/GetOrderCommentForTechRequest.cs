using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
   public class GetOrderCommentForTechRequest: GetOrderCommentBaseRequest
    {
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 技师级别
        /// </summary>
        public int TechLevel { get; set; } = -1;
    }
}
