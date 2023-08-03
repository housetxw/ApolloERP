using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 签收的类型
    /// </summary>
    public enum SignTypeEnum
    {
        /// <summary>
        /// 0未设置
        /// </summary>
        [Description("未设置")]
        Null = 0,
        /// <summary>
        /// 1订单
        /// </summary>
        [Description("订单")]
        Order = 1,
        /// <summary>
        /// 2包裹
        /// </summary>
        [Description("包裹")]
        Package = 2
    }
}
