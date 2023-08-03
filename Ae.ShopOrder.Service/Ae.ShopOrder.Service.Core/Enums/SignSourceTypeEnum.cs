using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 签收的来源类型
    /// </summary>
    public enum SignSourceTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        Null=0,
        /// <summary>
        /// 订单
        /// </summary>
        Order=1,

        /// <summary>
        /// 铺货
        /// </summary>
        Distribute
    }
}
