using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 支付状态枚举
    /// </summary>
    public enum PayStatusEnum
    {
        /// <summary>
        /// 0未支付
        /// </summary>
        NotPay = 0,
        /// <summary>
        /// 1已支付
        /// </summary>
        HavePay = 1,
    }
}
