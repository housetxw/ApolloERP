using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.ShopProduct
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopProductDetailVo : ShopProductVo
    {
        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();

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
