using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum LogTypeSummaryEnum
    {
        [Description("新增门店")]
        CreateShop=0,
        [Description("编辑门店")]
        EditShop =1,
        [Description("删除门店")]
        DeleteShop =2
    }
}
