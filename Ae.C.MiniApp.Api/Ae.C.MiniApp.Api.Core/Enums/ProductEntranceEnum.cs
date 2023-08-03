using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// ProductEntranceEnum
    /// </summary>
    public enum ProductEntranceEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")] 
        NoSet = 0,

        /// <summary>
        /// 秒杀
        /// </summary>
        [Description("秒杀")]
        SecKill = 1
    }
}
