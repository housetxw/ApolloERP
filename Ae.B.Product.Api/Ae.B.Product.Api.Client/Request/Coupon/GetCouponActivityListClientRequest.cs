using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class GetCouponActivityListClientRequest
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 活动状态 （-1查所有  0未发布  1可领取  2暂停领取  3已作废）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 优惠券Id
        /// </summary>
        public long CouponRuleId { get; set; }

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
