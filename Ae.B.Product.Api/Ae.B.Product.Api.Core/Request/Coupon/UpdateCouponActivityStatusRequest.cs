using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 更新活动状态
    /// </summary>
    public class UpdateCouponActivityStatusRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long ActivityId { get; set; }

        /// <summary>
        /// 状态（0未发布 1可领取 2暂停领取 3已作废）
        /// </summary>
        public CouponActivityStatue Status { get; set; }
    }
}
