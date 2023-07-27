using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum OrganizationType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,
        [Description("拓展")]
        Extend = 3,
        [Description("供应商")]
        Supplier = 4
    }
}
