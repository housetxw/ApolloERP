using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.ShopProduct
{
    public class ShopProductDetailDto : ShopProductDto
    {
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfoDto> Tags { get; set; } = new List<TagInfoDto>();

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; } = string.Empty;

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }
    }
}
