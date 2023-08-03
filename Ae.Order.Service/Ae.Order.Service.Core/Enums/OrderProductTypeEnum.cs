using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    public enum OrderProductTypeEnum
    {
        /// <summary>
        /// 0 普通产生
        /// </summary>
        Ordinary=0,

        /// <summary>
        /// 1购买核销
        /// </summary>
        BuyVerification=1,

        /// <summary>
        /// 2 使用核销
        /// </summary>
        UseVerification=2,

        /// <summary>
        /// 3 再次购买产生
        /// </summary>
        AgainBuy = 3,

        /// <summary>
        /// 4 追加下单产生
        /// </summary>
        AppendOrder = 4,
    }
}
