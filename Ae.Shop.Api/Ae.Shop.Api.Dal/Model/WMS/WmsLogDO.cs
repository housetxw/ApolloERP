using System;
using System.Collections.Generic;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("shop_wms_log")]
    public class WmsLogDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("object_id")]
        public long ObjectId { get; set; }
        /// <summary>
        /// 关联类型
        /// </summary>
        [Column("object_type")]
        public string ObjectType { get; set; } = string.Empty;
        /// <summary>
        /// 日志级别(1Info  2Warning 3Error 4Critical)
        /// </summary>
        [Column("log_level")]
        public string LogLevel { get; set; } = string.Empty;
        /// <summary>
        /// 修改前值
        /// </summary>
        [Column("before_value")]
        public string BeforeValue { get; set; } = string.Empty;
        /// <summary>
        /// 修改后值
        /// </summary>
        [Column("after_value")]
        public string AfterValue { get; set; } = string.Empty;
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
