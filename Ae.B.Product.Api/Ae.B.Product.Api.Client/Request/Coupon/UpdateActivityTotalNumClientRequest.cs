using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateActivityTotalNumClientRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
