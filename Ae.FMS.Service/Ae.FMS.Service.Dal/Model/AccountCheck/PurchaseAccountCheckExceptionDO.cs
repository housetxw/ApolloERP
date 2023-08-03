using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.AccountCheck
{
    [Table("purchase_account_check_exception")]
    public class PurchaseAccountCheckExceptionDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 对账单Id
        /// </summary>
        [Column("purchase_id")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// 关联单号(订单号,采购单号)
        /// </summary>
        [Column("relation_no")]
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 产品编码
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 上报人
        /// </summary>
        [Column("report_by")]
        public string ReportBy { get; set; } = string.Empty;
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
        /// 上报原因
        /// </summary>
        [Column("report_reason")]
        public string ReportReason { get; set; } = string.Empty;
        /// <summary>
        /// 上报时间
        /// </summary>
        [Column("report_time")]
        public DateTime ReportTime { get; set; } = new DateTime(1900, 1, 1);
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
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
    }
}
