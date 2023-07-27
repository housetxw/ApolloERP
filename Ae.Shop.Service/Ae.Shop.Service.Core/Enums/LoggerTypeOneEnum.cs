using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum LoggerTypeOneEnum
    {
        /// <summary>
        /// 新增门店
        /// </summary>
        [Description("新增门店")]
        ShopCreate=0,
        /// <summary>
        /// 更新门店信息
        /// </summary>
        [Description("更新门店信息")]
        ShopEdit =1,
        /// <summary>
        /// 删除门店
        /// </summary>
        [Description("删除门店")]
        ShopDelete =2
    }
}
