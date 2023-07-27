using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum ChannelType
    {
        [Description("未设置")]
        None = 0,

        [Description("B端员工")]
        Employee = 1,

        [Description("C端用户")]
        Consumer = 2
    }

    public enum ReferrerType
    {
        [Description("未设置")]
        None = 0,

        [Description("文章")]
        Article = 1,

        [Description("海报")]
        Poster = 2,

        [Description("段子")]
        Joke = 3,

        [Description("门店码")]
        ShopCode = 4,

        [Description("鹅管家码")]
        GooseAppCode = 5,

        [Description("商品促销")]
        ProductPromotion = 6,

        [Description("自行搜索")]
        SelfSearch = 7,

        [Description("直接转发小程序")]
        DirectFowardMiniApp = 8
    }
}
