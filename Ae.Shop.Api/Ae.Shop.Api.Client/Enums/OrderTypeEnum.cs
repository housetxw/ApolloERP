using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Enums
{
    /// <summary>
    /// 订单类型枚举
    /// </summary>
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,
        /// <summary>
        /// C端
        /// </summary>
        Consumer = 1,
        /// <summary>
        /// 门店
        /// </summary>
        Shop = 2
    }
}
