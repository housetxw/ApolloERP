using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 获取优惠券类型
    /// </summary>
    public class CouponTypeEnumRequest
    {
        /// <summary>
        /// 是否展示全部
        /// </summary>
        public bool ShowAll { get; set; }
    }
}
