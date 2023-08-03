using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response.Coupon
{
    /// <summary>
    /// GiftCouponForOrderResponse
    /// </summary>
    public class GiftCouponForOrderResponse
    {
        /// <summary>
        /// 优惠券活动id
        /// </summary>
        public List<long> CouponActivityIds { get; set; }
    }
}
