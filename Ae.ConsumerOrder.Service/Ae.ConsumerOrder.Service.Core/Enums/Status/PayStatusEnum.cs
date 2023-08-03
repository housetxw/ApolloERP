using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    /// <summary>
    /// 支付状态枚举
    /// </summary>
    public enum PayStatusEnum
    {
        /// <summary>
        /// 0未支付
        /// </summary>
        [Description("未支付")]
        NotPay = 0,
        /// <summary>
        /// 1已支付
        /// </summary>
        [Description("已支付")]
        HavePay = 1,
    }
}
