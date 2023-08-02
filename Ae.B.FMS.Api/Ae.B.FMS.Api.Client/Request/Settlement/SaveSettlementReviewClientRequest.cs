using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
 public   class SaveSettlementReviewClientRequest
    { 
        /// <summary>
        /// 结算批次no
        /// </summary>
        public string SettlementBatchNo { get; set; } = string.Empty;
     
        /// <summary>
        /// 状态(0:未付款 1：已付款 2：付款失败)
        /// </summary>

        public sbyte Status { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Remark { get; set; } = string.Empty;
      

        public string CreateBy { get; set; } = string.Empty;
        
      
    }
}
