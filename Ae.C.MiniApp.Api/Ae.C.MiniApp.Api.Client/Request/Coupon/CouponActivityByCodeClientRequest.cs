using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Coupon
{
    /// <summary>
    /// 兑换码兑换
    /// </summary>
    public class CouponActivityByCodeClientRequest
    {
        /// <summary>
        /// 兑换码
        /// </summary>
        public string RedeemCode { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
