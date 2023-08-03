using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory_exception_file")]
    public class InventoryExceptionFileDO
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
        [Column("relation_id")]
        public long RelationId { get; set; }
        /// <summary>
        /// 关联类型
        /// </summary>
        [Column("relation_type")]
        public string RelationType { get; set; } = string.Empty;
        /// <summary>
        /// 文件名
        /// </summary>
        [Column("file_name")]
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 文件地址
        /// </summary>
        [Column("file_url")]
        public string FileUrl { get; set; } = string.Empty;
        /// <summary>
        /// 文件类型(图片 文件)
        /// </summary>
        [Column("file_type")]
        public string FileType { get; set; } = string.Empty;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("is_active")]
        public sbyte IsActive { get; set; }
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