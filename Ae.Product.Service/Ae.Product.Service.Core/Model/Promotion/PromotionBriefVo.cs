using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Promotion
{
    /// <summary>
    /// 促销Model
    /// </summary>
    public class PromotionBriefVo
    {
        /// <summary>
        /// 促销活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 主标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbUrl { get; set; }
    }
}
