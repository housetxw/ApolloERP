using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// 兑换码查询优惠券
    /// </summary>
    public class CouponActivityByCodeRequest
    {
        /// <summary>
        /// 兑换码
        /// </summary>
        [Required(ErrorMessage = "兑换码不能为空")]
        public string RedeemCode { get; set; }
    }
}
