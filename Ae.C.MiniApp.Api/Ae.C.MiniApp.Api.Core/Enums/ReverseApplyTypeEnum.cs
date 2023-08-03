using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 逆向申请类型枚举
    /// </summary>
    public enum ReverseApplyTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 取消
        /// </summary>
        [Description("取消")]
        Cancel = 1,
        /// <summary>
        /// 退款
        /// </summary>
        [Description("退款")]
        Refund = 2,
        /// <summary>
        /// 换货
        /// </summary>
        [Description("换货")]
        Change = 3
    }
}
