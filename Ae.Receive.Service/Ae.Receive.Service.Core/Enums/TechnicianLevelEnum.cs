using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Receive.Service.Core.Enums
{
    /// <summary>
    /// 技师级别
    /// </summary>
    public enum TechnicianLevelEnum
    {
        /// <summary>
        /// 初级技师
        /// </summary>
        [Description("初级技师")]
        primary=0,
        /// <summary>
        /// 中级技师
        /// </summary>
        [Description("中级技师")]
        Middle =1,
        /// <summary>
        /// 高级技师
        /// </summary>
        [Description("高级技师")]
        High =2
    }
}
