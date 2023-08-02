using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Product.Api.Core.Enums
{
    /// <summary>
    /// PackageCardEnum
    /// </summary>
    public enum PackageCardEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")] NoSet = 0,

        /// <summary>
        /// 保养
        /// </summary>
        [Description("保养")] BaoYang = 1,

        /// <summary>
        /// 美容
        /// </summary>
        [Description("美容")] Beauty = 2,

        /// <summary>
        /// 洗车
        /// </summary>
        [Description("洗车")] WashCar = 3
    }
}
