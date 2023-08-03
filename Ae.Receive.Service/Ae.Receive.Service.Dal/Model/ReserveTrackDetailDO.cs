using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    /// <summary>
    /// 预约日志详情
    /// </summary>
    [Table("reserve_track_detail")]
    public class ReserveTrackDetailDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 日志Id
        /// </summary>
        [Column("log_id")]
        public int LogId { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        [Column("field")]
        public string Field { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        [Column("field_name")]
        public string FieldName { get; set; }
        /// <summary>
        /// 修改前
        /// </summary>
        [Column("before_value")]
        public string BeforeValue { get; set; }
        /// <summary>
        /// 修改后
        /// </summary>
        [Column("after_value")]
        public string AfterValue { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("created_by")]
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }
    }
}
