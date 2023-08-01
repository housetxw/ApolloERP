using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_priority_setting")]
    public class BaoYangPrioritySettingDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("bao_yang_type")]
        public string BaoYangType { get; set; } = string.Empty;

        /// <summary>
        /// 优先级字段
        /// </summary>
        [Column("priority_field")]
        public string PriorityField { get; set; } = string.Empty;

        /// <summary>
        /// 优先级value
        /// </summary>
        [Column("priority_value")]
        public string PriorityValue { get; set; } = string.Empty;

        /// <summary>
        /// 优先级：1>2>3
        /// </summary>
        [Column("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// 标记删除
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
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
