using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class SharePromotionContentClientRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 推广人类型（0未设置 1员工 2用户）
        /// </summary>
        public sbyte PromoterType { get; set; } = 1;
        /// <summary>
        /// 推广人标识
        /// </summary>
        public string PromoterId { get; set; } = string.Empty;
        /// <summary>
        /// 推广人名称
        /// </summary>
        public string PromoterName { get; set; } = string.Empty;
        /// <summary>
        /// 转发类型
        /// </summary>
        public PromoteContentForwardTypeEnum ForwardType { get; set; }
        /// <summary>
        /// 推广内容类型
        /// </summary>
        public PromoteContentTypeEnum PromoteContentType { get; set; } = PromoteContentTypeEnum.None;
        /// <summary>
        /// 推广内容ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 商品促销ID
        /// </summary>
        public string ProductPromotionId { get; set; } = string.Empty;
        /// <summary>
        /// 商品ID
        /// </summary>
        public string Pid { get; set; } = string.Empty;
    }
}
