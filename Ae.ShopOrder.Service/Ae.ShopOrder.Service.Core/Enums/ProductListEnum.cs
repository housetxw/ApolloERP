using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ae.ShopOrder.Service.Common.CustomAttribute;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 图片类型枚举
    /// </summary>
    public enum ProductListEnum
    {
        /// <summary>
        /// 0新产品照片
        /// </summary>
        [Description("新产品照片")]
        [EnumDescription("New")]
        New = 0,
        /// <summary>
        /// 1旧产品照片
        /// </summary>
        [Description("旧产品照片")]
        [EnumDescription("Old")]
        Old = 1,
        /// <summary>
        /// 1新旧对比照片
        /// </summary>
        [Description("新旧对比照片")]
        [EnumDescription("NewOldComparison")]
        NewOldComparison = 2,

    }
}
