using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 签收状态枚举
    /// </summary>
    public enum SignStatusEnum
    {
        /// <summary>
        /// 未签收
        /// </summary>
        [Description("未签收")]
        NotSign =0,
        /// <summary>
        /// 已签收
        /// </summary>
        [Description("已签收")]
        HaveSign = 1,
    }
}
