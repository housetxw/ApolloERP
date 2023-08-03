using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    /// <summary>
    /// 订单用户可执行操作枚举
    /// </summary>
    public enum OrderUserOperationEnum
    {
        /// <summary>
        /// 支付订单
        /// </summary>
        PayOrder = 0,
        /// 取消订单
        /// </summary>
        CancelOrder = 1,
        /// <summary>
        /// 再次购买
        /// </summary>
        BuyAgain = 2,
        /// <summary>
        /// 确认收货
        /// </summary>
        ConfirmReceive = 3,
        /// <summary>
        /// 申请预约
        /// </summary>
        ApplyReserve = 4,
        /// <summary>
        /// 找人代办
        /// </summary>
        FindProxy = 5,
        /// <summary>
        /// <summary>
        /// 评价订单
        /// </summary>
        CommentOrder = 6,
        /// <summary>
        /// 追加点评
        /// </summary>
        AppendComment = 7,
        /// <summary>
        /// 安装订单
        /// </summary>
        Install = 8
    }
}
