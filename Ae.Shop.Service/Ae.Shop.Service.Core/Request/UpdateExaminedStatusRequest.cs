using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class UpdateExaminedStatusRequest
    {
        public string FailedExaminedReason { get; set; }

        public string UpdateBy { get; set; } = string.Empty;

        public long ShopId { get; set; }

        /// <summary>
        /// 审核状态 
        /// </summary>
        public int CheckStatus { get; set; }
    }
}
