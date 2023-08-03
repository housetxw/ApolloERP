using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("occupy_stock_log")]
    public class OccupyStockLogDO
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
        [Column("object_no")]
        public string ObjectNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联子单号
        /// </summary>
        [Column("object_product_id")]
        public long ObjectProductId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 日志级别(1Info  2Warning 3Error 4Critical)
        /// </summary>
        [Key]
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