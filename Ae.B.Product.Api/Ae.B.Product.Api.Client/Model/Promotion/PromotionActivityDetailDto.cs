using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Promotion
{
    public class PromotionActivityDetailDto
    {
        /// <summary>
        /// 促销活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 活动名称
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
        /// 状态显示
        /// </summary>
        public string StatusDisplay { get; set; }

        /// <summary>
        /// 状态 1待审核 2已拒绝  3未开始  4进行中 5已结束
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 促销渠道
        /// </summary>
        public string PromotionChannel { get; set; }

        /// <summary>
        /// 促销描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 促销商品
        /// </summary>
        public List<PromotionProductDto> Products { get; set; }
    }

    /// <summary>
    /// 促销商品
    /// </summary>
    public class PromotionProductDto
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public int LimitQuantity { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public int SoldQuantity { get; set; }
    }
}
