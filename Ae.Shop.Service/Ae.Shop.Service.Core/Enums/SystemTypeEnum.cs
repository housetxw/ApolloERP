using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum SystemTypeEnum
    {
        [Description("普通")]
        Ordinary = 0,
        [Description("高级")]
        High = 1,
        [Description("个人")]
        Personal = 2,
        //  门店的0：普通 1：高级 2：个人 ；
    }
}
