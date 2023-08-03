using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Receive.Service.Dal.Model
{
    [Table("reserve_track")]
    public class ReserveTrackDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        [Column("reserve_id")]
        public long ReserveId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Column("opt_type")]
        public string OptType { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;

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
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}