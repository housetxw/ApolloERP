using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum OrderStatusForOfficeEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 待支付
        /// </summary>
        WatingPay=1,

        /// <summary>
        /// 已完成
        /// </summary>
        Complete=2
    }
}
