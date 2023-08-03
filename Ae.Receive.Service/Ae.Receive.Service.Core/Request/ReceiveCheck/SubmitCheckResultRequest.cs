using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 提交检查报告
    /// </summary>
    public class SubmitCheckResultRequest
    {
        /// <summary>
        /// 检查报告Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "检查Id必须大于0")]
        public long CheckId { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
