using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.AccountCheck
{
    [Table("purchase_settlement_pay_review")]
    public class PurchaseSettlementPayReviewDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 结算批次id
        /// </summary>
        [Column("settlement_batch_id")]
        public long SettlementBatchId { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        [Column("settlement_batch_no")]
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 状态(0:未付款 1：已付款 2：付款失败)
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
