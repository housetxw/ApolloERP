using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.ConsumerOrder.Service.Dal.Model
{
    [Table("order_log")]
    public class OrderLogDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 自定义类型1
        /// </summary>
        [Column("type1")]
        public string Type1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义类型2
        /// </summary>
        [Column("type2")]
        public string Type2 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤1
        /// </summary>
        [Column("filter1")]
        public string Filter1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤2
        /// </summary>
        [Column("filter2")]
        public string Filter2 { get; set; } = string.Empty;
        /// <summary>
        /// 操作摘要
        /// </summary>
        [Column("summary")]
        public string Summary { get; set; } = string.Empty;
        /// <summary>
        /// 操作前值
        /// </summary>
        [Column("before_value")]
        public string BeforeValue { get; set; } = string.Empty;
        /// <summary>
        /// 操作后值
        /// </summary>
        [Column("after_value")]
        public string AfterValue { get; set; } = string.Empty;
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
