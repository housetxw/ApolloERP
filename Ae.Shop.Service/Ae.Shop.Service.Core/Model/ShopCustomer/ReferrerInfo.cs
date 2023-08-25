using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Model
{
    public class ReferrerInfo { }

    public class ReferrerInfoDTO
    {
        public int ReferrerShopId { get; set; } = CommonConstant.Zero;

        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 渠道来源(推广人)类型（0未设置 1 B端员工 2 C端用户）
        /// </summary>
        public ChannelType Channel { get; set; } = ChannelType.None;

        /// <summary>
        /// 类型（0未设置 1文章 2海报 3段子 4门店码 5门店APP码 6商品促销 7自行搜索 8直接转发小程序）
        /// </summary>
        public ReferrerType ReferrerType { get; set; } = ReferrerType.None;
    }
}
