using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 门店类型
    /// </summary>
    public enum ShopTypeEnum
    {
        [Description("合作店")]
        CooperationShop=1,
        [Description("直营店")]
        OwnShop =2,
        [Description("上门养车")]
        DoorToDoorShop =4,
        [Description("认证店")]
        AuthenticationShop = 8,
        /// <summary>
        /// 技师先生（工作室)
        /// </summary>
        [Description("技师先生")]
        ApolloErpMen = 16,
    }
    /// <summary>
    /// 门店状态： 0正常 1终止 2暂停
    /// </summary>
    public enum ShopStatusEnum
    {
        [Description("正常营业")]
        Normal = 0,
        [Description("终止合作")]
        Stop = 1,
        [Description("暂停营业")]
        Suspend = 2,
    }
}
