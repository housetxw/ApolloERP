using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    /// <summary>
    /// 订单优惠券查询
    /// </summary>
    public class GetOrderCouponsRequest
    {
        /// <summary>
        /// 用户优惠券Id
        /// </summary>
        public List<long> CouponIds { get; set; }

        /// <summary>
        /// 优惠券规则Id
        /// </summary>
        public List<long> CouponRuleIds { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public List<long> OrderIds { get; set; }
    }
}
