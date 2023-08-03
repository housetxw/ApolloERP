using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Coupon
{
    /// <summary>
    /// 优惠券兑换
    /// </summary>
    public class CouponActivityByCodeResponse
    {
        /// <summary>
        /// 可领取
        /// </summary>
        public bool CanDraw { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 兑换码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
