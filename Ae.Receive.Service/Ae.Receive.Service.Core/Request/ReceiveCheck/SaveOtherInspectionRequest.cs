using Ae.Receive.Service.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveOtherInspectionRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "到店记录编号必须大于0")]
        public long RecId { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// f附加检查项   /// </summary>
        public List<OtherProjectResult> OtherProjectResult { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
