using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 删除OE件号
    /// </summary>
    public class DeleteOePartCodeRequest
    {
        /// <summary>
        /// OE件号
        /// </summary>
        [Required(ErrorMessage = "OE号不能为空")]
        public string OePartCode { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
