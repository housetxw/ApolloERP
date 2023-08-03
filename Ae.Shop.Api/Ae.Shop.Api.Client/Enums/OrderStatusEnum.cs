using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Enums
{
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 已提交
        /// </summary>
        Submitted = 10,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 20,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 30,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 40
    }
}
