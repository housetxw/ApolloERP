using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class AllotTaskDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        public string TaskNo { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 源仓库编号
        /// </summary>
        public long SourceWarehouse { get; set; }
        /// <summary>
        /// 源类型
        /// </summary>
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 源仓库名称
        /// </summary>
        public string SourceWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓库编号
        /// </summary>
        public long TargetWarehouse { get; set; }
        /// <summary>
        /// 目标类型
        /// </summary>
        public string TargetType { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓库名称
        /// </summary>
        public string TargetWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2已审核 3已取消 4已发出 5已驳回)
        /// </summary>
        public string TaskStatus { get; set; } = string.Empty;
        /// <summary>
        /// 任务类型(1铺货 2移库 3调拨)
        /// </summary>
        public string TaskType { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        public string Operator { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime OperatorTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核状态
        /// </summary>
        public sbyte IsAudit { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核说明
        /// </summary>
        public string AuditRemark { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        
        public List<AllotProductDTO> Products { get; set; } = new List<AllotProductDTO>();
    }
}