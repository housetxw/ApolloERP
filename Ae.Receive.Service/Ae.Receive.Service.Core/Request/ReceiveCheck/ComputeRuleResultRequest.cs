using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class ComputeRuleResultRequest
    {
        /// <summary>
        /// 检查结果Id
        /// </summary>
        public long CheckSubItemId { get; set; }

        /// <summary>
        /// 结果值
        /// </summary>
        public string Value { get; set; }
    }
}
