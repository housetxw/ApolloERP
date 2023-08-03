using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 订单日志类型枚举
    /// </summary>
    public enum OrderLoggerTypeEnum
    {
        /// <summary>
        /// 订单
        /// </summary>
        Order = 1,
        /// <summary>
        /// 签收
        /// </summary>
        Sign = 2,
        /// <summary>
        /// 安装
        /// </summary>
        Install = 3,
        /// <summary>
        /// 对账
        /// </summary>
        Reconciliation = 4,
        /// <summary>
        /// 更新成本
        /// </summary>
        UpdateCost = 5,
        /// <summary>
        /// 配送状态
        /// </summary>
        Delivery = 6,
        /// <summary>
        /// 评论状态
        /// </summary>
        Comment = 7,
        /// <summary>
        /// 审核
        /// </summary>
        Check = 8,
        /// <summary>
        /// 支付状态
        /// </summary>
        PayStatus = 9,
        /// <summary>
        /// 支付到账状态
        /// </summary>
        MoneyArriveStatus = 10,
        /// <summary>
        /// 订单逆向信息
        /// </summary>
        OrderReverse = 11,
        /// <summary>
        /// 订单状态
        /// </summary>
        OrderStatus = 12,
        /// <summary>
        /// 预约状态
        /// </summary>
        ReserveStatus = 13,
        /// <summary>
        /// 占用库存
        /// </summary>
        UseStock = 14,
        /// <summary>
        /// 释放库存
        /// </summary>
        ReleaseStock = 15,
    }
}
