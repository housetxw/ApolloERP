using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Login.Api.Core.Enums
{
    public class Login { }

    public enum LoginPlatform
    {
        [Description("未设置")]
        None = 0,

        [Description("门店管家APP")]
        App = 10,

        [Description("BOSS总部平台")]
        BOSS = 20,

        [Description("Shop门店管理平台")]
        Shop = 30
    }

}
