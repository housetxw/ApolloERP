using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 作废优惠券
    /// </summary>
    public class InvalidatedUserCouponRequest
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }
    }

    /// <summary>
    /// 延期优惠券
    /// </summary>
    public class DelayUserCouponRequest
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 延期天数
        /// </summary>
        public int DayNum { get; set; }
    }
}
