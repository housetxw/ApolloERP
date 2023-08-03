using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Pay
{
    public class ClosePayRequest
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        [Required(ErrorMessage = "支付单号不可为空")]
        public string PayNo { get; set; } = string.Empty;
    }
}
