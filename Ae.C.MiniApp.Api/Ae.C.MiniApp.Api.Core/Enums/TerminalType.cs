using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 终端类型
    /// </summary>
    public enum TerminalType
    {
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")] CbJApplet = 0,

        /// <summary>
        /// 养车
        /// </summary>
        [Description("养车")] YcJApplet = 1,

        /// <summary>
        /// 汽配
        /// </summary>
        [Description("汽配")] QpJApplet = 2,

        /// <summary>
        /// 门店管家
        /// </summary>
        [Description("门店管家")] ShopApp = 3
    }
}
