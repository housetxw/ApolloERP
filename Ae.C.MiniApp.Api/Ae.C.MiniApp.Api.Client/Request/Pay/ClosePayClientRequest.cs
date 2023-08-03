using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Pay
{
    public class ClosePayClientRequest
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        [Required(ErrorMessage = "支付单号不可为空")]
        public string PayNo { get; set; } = string.Empty;
        /// <summary>
        /// 操作人
        /// </summary>
        [Required(ErrorMessage = "操作人不可为空")]
        public string UpdateBy { get; set; } = string.Empty;
    }
}
