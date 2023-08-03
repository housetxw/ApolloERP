using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum OrderTypeEnum
    {
        [Description("总体")]
        Total = 0,
        [Description("轮胎")]
        Tire = 1,
        [Description("保养")]
        BaoYang = 2,
        [Description("美容")]
        Wash = 3,
        [Description("钣金维修")]
        SheetMetal = 4,
        [Description("汽车改装")]
        CarModification = 5,
        [Description("其他")]
        Other = 6
    }
}
