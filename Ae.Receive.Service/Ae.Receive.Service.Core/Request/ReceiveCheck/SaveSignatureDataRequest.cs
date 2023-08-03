using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 签字Request
    /// </summary>
    public class SaveSignatureDataRequest
    {
        /// <summary>
        /// 检查报告Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "检查Id必须大于0")]
        public long CheckId { get; set; }

        /// <summary>
        /// 项目Code
        /// </summary>
        public string CheckModuleCode { get; set; }

        /// <summary>
        /// 签字图片Url
        /// </summary>
        [Required(ErrorMessage = "签字不能为空")]
        public string SignatureUrl { get; set; }

        

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
