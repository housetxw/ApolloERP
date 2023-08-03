using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 订单用户可执行操作枚举（具体显示名称由API定义生成）
    /// </summary>
    public enum OrderUserOperationEnum
    {
        /// <summary>
        /// 支付订单
        /// </summary>
        PayOrder = 0,
        /// <summary>
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
        Install = 8,
        /// <summary>
        /// 查看预约
        /// </summary>
        CheckReserve = 10,
        /// <summary>
        /// 重新预约
        /// </summary>
        AgainReserve = 11
    }
}
