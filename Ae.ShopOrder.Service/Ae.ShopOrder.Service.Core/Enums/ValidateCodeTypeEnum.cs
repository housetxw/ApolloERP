using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 验码类型
    /// </summary>
    public enum ValidateCodeTypeEnum
    {
        /// <summary>
        /// 核销
        /// </summary>
        HX = 1,
        /// <summary>
        /// 安装
        /// </summary>
        AZ = 2,
        /// <summary>
        /// 套餐卡
        /// </summary>
        TC = 3
    }
}
