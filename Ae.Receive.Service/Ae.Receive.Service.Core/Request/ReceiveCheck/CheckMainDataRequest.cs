using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 检查报告主数据Request
    /// </summary>
    public class CheckMainDataRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "到店记录编号必须大于0")]
        public long RecId { get; set; }

        /// <summary>
        /// 检查报告Id
        /// </summary>
        public long CheckId { get; set; }
    }
}
