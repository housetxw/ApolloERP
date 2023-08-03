using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 结算单付款状态
    /// </summary>
    public enum SettlementBatchStatusEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None =-1,
        /// <summary>
        /// 0未付款
        /// </summary>
        [Description("未付款")]
        Apply=0,
        /// <summary>
        /// 1未付款
        /// </summary>
        [Description("付款失败")]
        PayFailure =1,
        /// <summary>
        /// 2已付款
        /// </summary>
        [Description("已付款")]
        HavePay =2
    }
}
