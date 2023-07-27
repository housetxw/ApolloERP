using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    /// <summary>
    /// 公司类型枚举
    /// </summary>
    public enum CompanyTypeEnum
    {
        /// <summary>
        /// 公司
        /// </summary>
        [Description("公司")]
        Company = 0,
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")]
        Shop =1,
        /// <summary>
        /// 仓库
        /// </summary>
        [Description("仓库")]
        Warehouse = 2
    }
}
