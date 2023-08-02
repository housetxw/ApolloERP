using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    /// <summary>
    /// 用户发放优惠券Request
    /// </summary>
    public class GrantUserCouponClientRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public long ActivityId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public List<string> UserIdList { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
