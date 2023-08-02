using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Product.Service.Core.Enums
{
    /// <summary>
    /// 终端类型
    /// </summary>
    public enum TerminalType
    {
        /// <summary>
        /// B端小程序
        /// </summary>
        [Description("B端小程序")] CbJApplet = 0,

        /// <summary>
        /// C端小程序
        /// </summary>
        [Description("C端小程序")] YcJApplet = 1,

        /// <summary>
        /// 其他小程序
        /// </summary>
        [Description("其他小程序")] QpJApplet = 2,

        /// <summary>
        /// B端APP
        /// </summary>
        [Description("B端APP")] ShopApp = 3
    }
}
