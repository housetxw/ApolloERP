using Ae.Shop.Service.Common.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum ShopServiceTypeEnum
    {

        [EnumDescription("保养养护")]
        BaoYang = 1,

        [EnumDescription("轮胎服务")]
        Tire,

        [EnumDescription("美容洗车")]
        Wash,

        [EnumDescription("钣金喷漆")]
        SheetMetal,

        [EnumDescription("汽车改装")]
        CarModification,

        [EnumDescription("其他业务")]
        Other
    }
}
