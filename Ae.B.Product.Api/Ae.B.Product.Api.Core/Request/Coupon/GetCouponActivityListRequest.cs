using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCouponActivityListRequest : BasePageRequest
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string CouponActivityId { get; set; }

        /// <summary>
        /// 活动状态 （-1查所有  0未发布  1可领取  2暂停领取  3已作废）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 优惠券Id
        /// </summary>
        public string CouponRuleId { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public string CodeId { get; set; }
    }
}
