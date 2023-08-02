using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class InvalidatedUserCouponClientRequest
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 延期优惠券
    /// </summary>
    public class DelayUserCouponClientRequest
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndDay { get; set; }
    }
}
