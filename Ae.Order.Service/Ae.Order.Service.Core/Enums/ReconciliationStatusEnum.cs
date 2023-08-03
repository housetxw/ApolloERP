using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 对账状态
    /// </summary>
    public enum ReconciliationStatusEnum
    {

        /// <summary>
        /// 0未对账
        /// </summary>
        NotReconciliation=0,
        /// <summary>
        /// 异常
        /// </summary>
        ExceptionReconciliation = 1,
        /// <summary>
        /// 已对账
        /// </summary>
        HaveReconciliation=2
    }
}
