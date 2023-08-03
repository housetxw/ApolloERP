using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.Promotion
{
    [Table("promotion_activity")]
    public class PromotionActivityDo
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 副标题-广告语
        /// </summary>
        [Column("subtitle")]
        public string Subtitle { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
        /// 缩略图Url
        /// </summary>
        [Column("thumb_url")]
        public string ThumbUrl { get; set; }
        /// <summary>
        /// 活动类型（0门店商品促销）
        /// </summary>
        [Column("activity_type")]
        public int ActivityType { get; set; }
        /// <summary>
        /// 促销类型（0自定义价格 1打折）
        /// </summary>
        [Column("promotion_type")]
        public int PromotionType { get; set; }
        /// <summary>
        /// 标签展示
        /// </summary>
        [Column("label")]
        public string Label { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 适用渠道
        /// </summary>
        [Column("apply_channel")]
        public string ApplyChannel { get; set; }
        /// <summary>
        /// 审核状态（0待审核 1已审核 2已拒绝 3已结束4已售完）
        /// </summary>
        [Column("audit_status")]
        public int AuditStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("audit_by")]
        public string AuditBy { get; set; } = string.Empty;
        /// <summary>
        /// 审核备注
        /// </summary>
        [Column("audit_remark")]
        public string AuditRemark { get; set; } = string.Empty;

        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("audit_time")]
        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
