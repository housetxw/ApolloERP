using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_performance_config")]
    public class ShopPerformanceConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 绩效类型(0默认， 1单品，2小品类)
        /// </summary>
        [Column("performance_type")]
        public sbyte PerformanceType { get; set; }
        /// <summary>
        /// 类型参数（多个用逗号隔开）
        /// </summary>
        [Column("type_value")]
        public string TypeValue { get; set; } = string.Empty;
        /// <summary>
        /// 配置类型(比例 1/金额2)
        /// </summary>
        [Column("config_type")]
        public sbyte ConfigType { get; set; }
        /// <summary>
        /// 比例/金额
        /// </summary>
        [Column("config_point")]
        public decimal ConfigPoint { get; set; }
        /// <summary>
        /// 配置启用时间(备用字段)
        /// </summary>
        [Column("config_time")]
        public DateTime ConfigTime { get; set; } = new DateTime(1900, 1, 1);
       
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
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