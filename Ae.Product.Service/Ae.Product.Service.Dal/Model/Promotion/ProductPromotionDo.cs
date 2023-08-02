using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    public class ProductPromotionDo
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 审核状态（0待审核 1已审核 2已拒绝 3已结束4已售完）
        /// </summary>
        public int AuditStatus { get; set; }

        public string Label { get; set; }

        /// <summary>
        /// 促销渠道
        /// </summary>
        public string ApplyChannel { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

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
