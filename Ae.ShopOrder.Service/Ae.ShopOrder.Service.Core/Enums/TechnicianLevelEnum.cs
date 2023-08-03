using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 技师级别
    /// </summary>
    public enum TechnicianLevelEnum
    {
        ///// <summary>
        ///// 初级技师
        ///// </summary>
        //[Description("初级技师")]
        //primary = 0,
        ///// <summary>
        ///// 中级技师
        ///// </summary>
        //[Description("中级技师")]
        //Middle = 1,
        ///// <summary>
        ///// 高级技师
        ///// </summary>
        //[Description("高级技师")]
        //High = 2

        [Description("未设置")]
        None = 0,

        /// <summary>
        /// 1级
        /// </summary>
        [Description("1级")]
        First = 1,
        /// <summary>
        /// 2级
        /// </summary>
        [Description("2级")]
        Second = 2,
        /// <summary>
        /// 3级
        /// </summary>
        [Description("3级")]
        Third = 3,
        /// <summary>
        /// 4级
        /// </summary>
        [Description("4级")]
        Four = 4
    }
}
