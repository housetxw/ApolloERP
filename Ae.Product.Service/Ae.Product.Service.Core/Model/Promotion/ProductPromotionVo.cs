using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Promotion
{
    /// <summary>
    /// ProductPromotionVo
    /// </summary>
    public class ProductPromotionVo
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

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

        /// <summary>
        /// 活动起始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 副标题-广告语
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }
    }
}
