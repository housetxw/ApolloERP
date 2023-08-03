using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveCustomerDescriptionRequest
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

        /// <summary>
        /// 客户描述
        /// </summary>
        [MaxLength(200, ErrorMessage = "客户描述不能超过200字符")]
        public string Narration { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
