using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Enums
{
    /// <summary>
    /// 逆向单状态枚举
    /// </summary>
    public enum ReverseStatusEnum
    {
        /// <summary>
        /// 已提交
        /// </summary>
        [Description("已提交")]
        Submitted = 10,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        Confirmed = 20,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 30,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 40
    }
}
