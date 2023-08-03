using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    public class CouponEnum { }

    public enum CouponListEntranceType
    {
        /// <summary>
        /// 订单确认页
        /// </summary>
        [Description("订单确认页")]
        Order = 0,

        /// <summary>
        /// 我的页面
        /// </summary>
        [Description("我的页面")]
        Mine = 1
    }

}
