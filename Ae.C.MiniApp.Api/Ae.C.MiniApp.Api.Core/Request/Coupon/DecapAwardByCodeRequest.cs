using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class DecapAwardByCodeRequest
    {
        /// <summary>
        /// 兑换码
        /// </summary>
        [Required(ErrorMessage = "兑换码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        [Required(ErrorMessage = "OpenId不能为空")]
        public string OpenId { get; set; }
    }
}
