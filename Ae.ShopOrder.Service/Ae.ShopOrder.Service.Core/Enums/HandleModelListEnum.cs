using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 处理方式枚举
    /// </summary>
    public enum HandleModelListEnum
    {
        /// <summary>
        /// 0订单已取消(或部分取消)
        /// </summary>
        [Description("订单已取消(或部分取消)")]
        OrderCancel = 0,
        /// <summary>
        /// 1门店商品替换
        /// </summary>
        [Description("门店商品替换")]
        ShopProductReplace = 1,
        /// <summary>
        /// 2其他
        /// </summary>
        [Description("其他")]
        Other = 2
    }
}
