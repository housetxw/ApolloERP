using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 检查报告
    /// </summary>
    public class CheckReportRequest
    {
        /// <summary>
        /// 检查报告Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "检查Id必须大于0")]
        public long CheckId { get; set; }
    }
}
