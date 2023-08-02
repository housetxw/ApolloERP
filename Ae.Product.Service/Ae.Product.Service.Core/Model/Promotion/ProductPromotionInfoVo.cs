using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Promotion
{
    /// <summary>
    /// 商品促销信息
    /// </summary>
    public class ProductPromotionInfoVo
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActicityId { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 促销标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 可售数量
        /// </summary>
        public int AvailQuantity { get; set; }
    }
}
