using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    /// <summary>
    /// 门店商户性质
    /// </summary>
    public enum ShopNatureEnum
    {
        /// <summary>
        /// 门店加盟
        /// </summary>
        [Description("门店加盟")]
        Shop = 0,
        /// <summary>
        /// 平台先生
        /// </summary>
        [Description("平台先生")]
        MrApolloErp = 1,
        /// <summary>
        /// 配件改装
        /// </summary>
        [Description("配件改装")]
        PartsModify = 2,
        /// <summary>
        /// 工厂直销
        /// </summary>
        [Description("工厂直销")]
        FactoryOutlet = 3
    }
}
