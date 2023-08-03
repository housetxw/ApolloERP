using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class SharePromotionContentRequest
    {
        /// <summary>
        /// 转发类型
        /// </summary>
        public PromoteContentForwardTypeEnum ForwardType { get; set; } = PromoteContentForwardTypeEnum.None;
        /// <summary>
        /// 推广内容类型（0未设置 1文章 2海报 3段子 4门店码 5门店管家码 6商品促销 7自行搜索 8直接转发小程序 9技师推广 10商品详情）
        /// </summary>
        public PromoteContentTypeEnum PromoteContentType { get; set; } = PromoteContentTypeEnum.None;
        /// <summary>
        /// 推广内容ID（当推广内容类型为文章、海报、段子时传此值）
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 商品促销ID（当推广内容类型为商品促销时传此值）
        /// </summary>
        public string ProductPromotionId { get; set; } = string.Empty;
        /// <summary>
        /// 商品ID（当推广内容类型为商品详情时传此值）
        /// </summary>
        public string Pid { get; set; } = string.Empty;
    }
}
