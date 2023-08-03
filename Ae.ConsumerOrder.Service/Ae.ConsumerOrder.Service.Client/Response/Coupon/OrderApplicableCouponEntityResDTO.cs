using Ae.ConsumerOrder.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response
{
    public class OrderApplicableCouponEntityResDTO
    {
        /// <summary>
        /// 最优可用优惠券
        /// </summary>
        public OrderApplicableCoupon OrderApplicableCoupon { get; set; }

        /// <summary>
        /// 订单可用优惠券UserCouponId集合
        /// </summary>
        public List<long> UserCouponId { get; set; }
    }
}
