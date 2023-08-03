using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("occupy_queue")]
    public class OccupyQueueDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 类型(Order,OrderMQ)
        /// </summary>
        [Column("queue_type")]
        public string QueueType { get; set; } = string.Empty;
        /// <summary>
        /// 关联的单号
        /// </summary>
        [Column("source_no")]
        public string SourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否是处理中
        /// </summary>
        [Column("is_processing")]
        public string IsProcessing { get; set; } = string.Empty;
        /// <summary>
        /// 占用状态(1新建,2未占用，3已占用，4已取消)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 占库优先级
        /// </summary>
        [Column("pripority")]
        public short Pripority { get; set; }
        /// <summary>
        /// 占库规则id
        /// </summary>
        [Column("rule_id")]
        public long RuleId { get; set; }
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