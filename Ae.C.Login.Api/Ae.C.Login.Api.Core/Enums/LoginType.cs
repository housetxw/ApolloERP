using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Enums
{
    /// <summary>
    /// 三方登录类型
    /// </summary>
    public enum ThirtyLoginType
    {
        /// <summary>
        /// 测试
        /// </summary>
        [Display(Name = "测试")]
        Test = 0,
        /// <summary>
        /// 微信
        /// </summary>
        [Display(Name = "微信")]
        WeChat = 1,
        /// <summary>
        /// QQ
        /// </summary>
        [Display(Name = "QQ")]
        QQ = 2,
        /// <summary>
        /// 微博
        /// </summary>
        [Display(Name = "微博")]
        Weibo = 3
    }

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

        [Description("门店管家码")]
        GooseAppCode = 5,

        [Description("商品促销")]
        ProductPromotion = 6,

        [Description("自行搜索")]
        SelfSearch = 7,

        [Description("直接转发小程序")]
        DirectFowardMiniApp = 8,

        [Description("技师推广")]
        TechPromote = 9,

        [Description("C端用户分享有礼")]
        UserShare = 10
    }


}
