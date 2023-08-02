using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateActivityTotalNumRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "活动Id必须大于0")]
        public long CouponActivityId { get; set; }

        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }
    }
}
