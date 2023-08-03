using System.ComponentModel;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class ReferrerInfo { }

    public class ReferrerInfoDTO
    {
        public int ReferrerShopId { get; set; }

        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 渠道来源(推广人)类型（0未设置 1 B端员工 2 C端用户）
        /// </summary>
        public ChannelType Channel { get; set; }

        /// <summary>
        /// 类型（0未设置 1文章 2海报 3段子 4门店码 管家码 6商品促销 7自行搜索 8直接转发小程序）
        /// </summary>
        public ReferrerType ReferrerType { get; set; }
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

        [Description("管家码")]
        GooseAppCode = 5,

        [Description("商品促销")]
        ProductPromotion = 6,

        [Description("自行搜索")]
        SelfSearch = 7,

        [Description("直接转发小程序")]
        DirectFowardMiniApp = 8
    }
}
