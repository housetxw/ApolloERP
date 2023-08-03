using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    /// <summary>
    /// 对账状态
    /// </summary>
    public enum ReconciliationStatusEnum
    {
        /// <summary>
        /// 0未对账
        /// </summary>
        NoReconciliation = 0,
        /// <summary>
        /// 1异常对账
        /// </summary>
        ExceptionReconciliation = 1,
        /// <summary>
        /// 2已对账
        /// </summary>
        HaveReconciliation = 2,
    }
}
