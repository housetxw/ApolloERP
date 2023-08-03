using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 派工状态枚举
    /// </summary>
    public enum DispatchStatusEnum
    {
        /// <summary>
        /// 0未派工
        /// </summary>
        [Description("未派工")]
        NotDispatch =0,
        /// <summary>
        /// 1已派工
        /// </summary>
        [Description("已派工")]
        HaveDispatch =1,
    }
}
