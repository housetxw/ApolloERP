using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Coupon
{
    public class CouponRuleVO { }

    /// <summary>
    /// 优惠券规则：优惠券分类
    /// </summary>
    public enum CouponCategory
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        NotSet = 0,

        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")]
        ApolloErp = 1,
        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")]
        Shop = 2
    }
}
