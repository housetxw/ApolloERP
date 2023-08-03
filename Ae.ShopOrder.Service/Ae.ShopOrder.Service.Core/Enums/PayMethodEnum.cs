using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum PayMethodEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 在线安装
        /// </summary>
        [Description("在线安装")]
        Online,
        /// <summary>
        /// 保养
        /// </summary>
        [Description("到店")]
        ToShop = 2,
        /// <summary>
        ///月结
        /// </summary>
        [Description("月结")]
        Month = 3
    }
}
