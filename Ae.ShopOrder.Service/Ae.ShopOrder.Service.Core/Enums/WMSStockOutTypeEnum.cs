using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// WMS 出库类型
    /// </summary>
    public enum WmsStockOutTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        Null =0,
        /// <summary>
        /// 订单
        /// </summary>
        [Description("订单")]
        Order =1,
        /// <summary>
        /// 移库
        /// </summary>
        [Description("移库")]
        Transfer =2,
        /// <summary>
        /// 铺货
        /// </summary>
        [Description("铺货")]
        Distribute = 3
    }
}
