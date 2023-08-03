using Ae.ShopOrder.Service.Core.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderApplicableCouponEntityResDTO
    {
        /// <summary>
        /// 最优可用优惠券
        /// </summary>
        public UserCouponVO OrderApplicableCoupon { get; set; }

        /// <summary>
        /// 订单可用优惠券UserCouponId集合
        /// </summary>
        public List<long> UserCouponId { get; set; }
    }
}
