using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class InOrOutlookInspectionResponse
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public List<NormalProjectResult> CheckResult { get; set; } = new List<NormalProjectResult>();
    }
}
