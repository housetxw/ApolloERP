using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model.WMS
{
    [Table("allot_task")]
    public class AllotTaskDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 任务单号
        /// </summary>
        [Column("task_no")]
        public string TaskNo { get; set; } = string.Empty;
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
        /// 源仓库编号
        /// </summary>
        [Column("source_warehouse")]
        public long SourceWarehouse { get; set; }
        /// <summary>
        /// 源类型
        /// </summary>
        [Column("source_type")]
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 源仓库名称
        /// </summary>
        [Column("source_warehouse_name")]
        public string SourceWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓库编号
        /// </summary>
        [Column("target_warehouse")]
        public long TargetWarehouse { get; set; }
        /// <summary>
        /// 目标类型
        /// </summary>
        [Column("target_type")]
        public string TargetType { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓库名称
        /// </summary>
        [Column("target_warehouse_name")]
        public string TargetWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2已审核 3已取消 4已发出 5已驳回)
        /// </summary>
        [Column("task_status")]
        public string TaskStatus { get; set; } = string.Empty;
        /// <summary>
        /// 任务类型(1铺货 2移库 3调拨)
        /// </summary>
        [Column("task_type")]
        public string TaskType { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        [Column("operator")]
        public string Operator { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Column("operator_time")]
        public DateTime OperatorTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核状态
        /// </summary>
        [Column("is_audit")]
        public sbyte IsAudit { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("audit_user")]
        public string AuditUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("audit_time")]
        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核说明
        /// </summary>
        [Column("audit_remark")]
        public string AuditRemark { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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
