using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class UpdateCouponActivityStatusClientRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long ActivityId { get; set; }

        /// <summary>
        /// 状态（0未发布 1可领取 2暂停领取 3已作废）
        /// </summary>
        public CouponActivityStatue Status { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
