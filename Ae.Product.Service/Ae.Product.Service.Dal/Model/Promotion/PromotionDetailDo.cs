using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    public class PromotionDetailDo
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public Guid ActivityId { get; set; }

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
        /// <summary>
        /// 缩略图Url
        /// </summary>
        public string ThumbUrl { get; set; }
        /// <summary>
        /// 活动类型（0门店商品促销）
        /// </summary>
        public int ActivityType { get; set; }
        /// <summary>
        /// 促销类型（0自定义价格 1打折）
        /// </summary>
        public int PromotionType { get; set; }
        /// <summary>
        /// 标签展示
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 适用渠道
        /// </summary>
        public string ApplyChannel { get; set; }
        /// <summary>
        /// 审核状态（0待审核 1已审核 2已拒绝 3已结束）
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditBy { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        /// 
        public string AuditRemark { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }
        
    }
}
