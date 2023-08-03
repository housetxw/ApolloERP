using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum PromotionChannelEnum
    {
        /// <summary>
        /// 0 小程序
        /// </summary>
        [Description("小程序")]
        MiniApp = 0,
        /// <summary>
        /// 1门店管家
        /// </summary>
        [Description("门店管家")]
        ShopApp = 1
    }
}
