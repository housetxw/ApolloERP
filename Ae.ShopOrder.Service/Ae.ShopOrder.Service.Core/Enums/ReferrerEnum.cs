using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum ChannelType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,

        /// <summary>
        /// B端员工
        /// </summary>
        [Description("B端员工")]
        Employee = 1,

        /// <summary>
        /// C端用户
        /// </summary>
        [Description("C端用户")]
        Consumer = 2,

        /// <summary>
        /// Boss
        /// </summary>
        [Display(Name = "Boss")]
        [Description("Boss")]
        Boss = 3
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
        TechSpread = 9,

        [Description("商品详情")]
        ProductDetail = 10,

        [Description("手动添加")]
        Nomal = 11,

        [Description("快速排队码")]
        QueueCode = 12
    }

}
