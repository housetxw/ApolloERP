using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    public enum OrderTypeEnum
    {
        [Description("全部")]
        None = 0,

        [Description("轮胎安装")]
        Tire = 1,

        [Description("保养服务")]
        BaoYang = 2,

        [Description("美容洗车")]
        Wash = 3,

        [Description("钣金维修")]
        SheetMetal = 4,

        [Description("汽车改装")]
        CarModification = 5,

        [Description("其他服务")]
        Other = 6
    }
}
