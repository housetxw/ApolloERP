using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    /// <summary>
    /// 单位类型 0：公司；1：门店；2：仓库
    /// </summary>
    public enum EmployeeType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2
    }

    public enum DimissionType
    {
        [Description("在职")]
        OnJob = 0,

        [Description("自动离职")]
        VoluntaryTurnover = 1,

        [Description("辞退")]
        Dismiss = 2
    }

    public enum EmployeeGenderEnum
    {
        /// <summary>
        /// 保密
        /// </summary>
        [Description("保密")]
        Secret = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2
    }

    public enum EmployeeLevelEnum
    {
        /// <summary>
        /// 初级
        /// </summary>
        [Description("初级")]
        Primary = 0,
        /// <summary>
        /// 中级
        /// </summary>
        [Description("中级")]
        Middle = 1,
        /// <summary>
        /// 高级
        /// </summary>
        [Description("高级")]
        Higher = 2
    }
}
