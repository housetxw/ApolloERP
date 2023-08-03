using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("full_flow_log")]
    public class FullFlowLogDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 来源单id
        /// </summary>
        [Column("source_inventory_id")]
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 来源单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 流水类型
        /// </summary>
        [Column("flow_type")]
        public string FlowType { get; set; } = string.Empty;
        /// <summary>
        /// 流水类型文本
        /// </summary>
        [Column("flow_type_text")]
        public string FlowTypeText { get; set; } = string.Empty;
        /// <summary>
        /// 流水步骤
        /// </summary>
        [Column("flow_step")]
        public long FlowStep { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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
