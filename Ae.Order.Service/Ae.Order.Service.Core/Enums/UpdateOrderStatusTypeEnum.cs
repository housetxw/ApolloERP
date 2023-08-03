using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 更新订单状态类型枚举
    /// </summary>
    public enum UpdateOrderStatusTypeEnum
    {
   
        /// <summary>
        /// 1签收状态
        /// </summary>
        SignStatus = 1,
        /// <summary>
        /// 安装状态
        /// </summary>
        InstallStatus = 2,
        /// <summary>
        /// 已对账
        /// </summary>
        HaveReconciliation = 3,
        /// <summary>
        /// 异常对账
        /// </summary>
        ExceptionReconciliation = 4,
        /// <summary>
        /// 未对账
        /// </summary>
        NoReconciliation = 5,
        /// <summary>
        /// 配送状态
        /// </summary>
        DeliveryStatus=6,
        /// <summary>
        /// 客户点评
        /// </summary>
        CustomerComment=7,
        /// <summary>
        /// 客户追评
        /// </summary>
        CustomerAppendComment=8,
    }
}
