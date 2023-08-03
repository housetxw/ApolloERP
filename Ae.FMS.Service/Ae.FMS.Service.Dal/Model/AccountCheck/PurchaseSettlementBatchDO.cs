using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.AccountCheck
{
    [Table("purchase_settlement_batch")]
    public class PurchaseSettlementBatchDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        [Column("settlement_batch_no")]
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 结算方式(现金  、月结)
        /// </summary>
        [Column("settlement_type")]
        public string SettlementType { get; set; } = string.Empty;
        /// <summary>
        /// 结算年份
        /// </summary>
        [Column("settlement_year")]
        public int SettlementYear { get; set; }
        /// <summary>
        /// 结算月份
        /// </summary>
        [Column("settlement_month")]
        public int SettlementMonth { get; set; }
        /// <summary>
        /// 状态(0:未付款 1:付款失败 2：已付款)
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 结算人员
        /// </summary>
        [Column("settlement_staff")]
        public string SettlementStaff { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        [Column("apply_user")]
        public string ApplyUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Column("apply_time")]
        public DateTime ApplyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 本期对账金额
        /// </summary>
        [Column("bill_amount")]
        public decimal BillAmount { get; set; }
        /// <summary>
        /// 银行的名称
        /// </summary>
        [Column("bank_name")]
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡卡号
        /// </summary>
        [Column("bank_no")]
        public string BankNo { get; set; } = string.Empty;
        /// <summary>
        /// 最新备注
        /// </summary>
        [Column("top_remark")]
        public string TopRemark { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
