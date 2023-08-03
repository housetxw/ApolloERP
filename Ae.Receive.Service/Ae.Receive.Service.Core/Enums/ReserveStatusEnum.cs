using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    public enum ReserveStatusEnum
    {
        /// <summary>
        /// 待确认
        /// </summary>
        [Description("待确认")]
        Unconfirmed = 0,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        Confirmed = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 2,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 3
    }
}
