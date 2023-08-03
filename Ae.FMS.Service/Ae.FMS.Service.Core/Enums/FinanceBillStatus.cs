using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 财务账单的状态
    /// </summary>
    public enum FinanceBillStatus
    {

        ///// <summary>
        ///// 0未对账
        ///// </summary>
        //[Remark("未对账")]
        //WaitingReconciliation = 0,
        /// <summary>
        /// 1异常
        /// </summary>
        [Description("异常")]
        Exception = 1,
        /// <summary>
        /// 2已对账
        /// </summary>
        [Description("已对账")]
        HaveReconciliation = 2,

        /// <summary>
        /// 3 待提现
        /// </summary>
        [Description("待提现")]
        WaitingWithdrawal = 3,
    }
}
