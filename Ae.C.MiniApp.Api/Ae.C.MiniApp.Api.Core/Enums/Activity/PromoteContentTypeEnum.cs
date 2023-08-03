using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Enums
{
    /// <summary>
    /// 推广内容类型枚举
    /// </summary>
    public enum PromoteContentTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 文章
        /// </summary>
        [Description("文章")]
        Article = 1,
        /// <summary>
        /// 海报
        /// </summary>
        [Description("海报")]
        Poster = 2,
        /// <summary>
        /// 段子
        /// </summary>
        [Description("段子")]
        Joke = 3,
        /// <summary>
        /// 门店码
        /// </summary>
        [Description("门店码")]
        ShopCode = 4,
        /// <summary>
        /// 门店管家码
        /// </summary>
        [Description("门店管家码")]
        GooseAppCode = 5,
        /// <summary>
        /// 商品促销
        /// </summary>
        [Description("商品促销")]
        ProductPromotion = 6,
        /// <summary>
        /// 自行搜索
        /// </summary>
        [Description("自行搜索")]
        SelfSearch = 7,
        /// <summary>
        /// 直接转发小程序
        /// </summary>
        [Description("直接转发小程序")]
        DirectFowardMiniApp = 8,
        /// <summary>
        /// 技师推广
        /// </summary>
        [Description("技师推广")]
        TechPromote = 9,
        /// <summary>
        /// 商品详情
        /// </summary>
        [Description("商品详情")]
        ProductDetail = 10
    }
}
