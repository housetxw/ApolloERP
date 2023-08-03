using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// DecapAwardDetailByCodeRequest
    /// </summary>
    public class DecapAwardDetailByCodeRequest
    {
        /// <summary>
        /// 兑换码
        /// </summary>
        [Required(ErrorMessage = "兑换码不能为空")]
        public string Code { get; set; }
    }
}
