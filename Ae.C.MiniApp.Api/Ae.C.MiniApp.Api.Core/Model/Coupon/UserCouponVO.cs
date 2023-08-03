using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Coupon
{
    public class UserCouponVO { }

    /// <summary>
    /// 用户优惠券：使用状态
    /// </summary>
    public enum UserCouponStatus
    {
        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Unused = 0,

        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")]
        Used = 1,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expired = 2
    }


}
