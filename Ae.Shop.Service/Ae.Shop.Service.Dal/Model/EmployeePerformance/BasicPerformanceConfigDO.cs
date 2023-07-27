using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("basic_performance_config")]
    public class BasicPerformanceConfigDO
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
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        public string ServiceType { get; set; } = string.Empty;
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
        /// 单项服务修改时间  暂时还用不到
        /// </summary>
        [Column("config_time")]
        public DateTime ConfigTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 单项服务开关
        /// </summary>
        [Column("service_config_flag")]
        public sbyte ServiceConfigFlag { get; set; }
        /// <summary>
        /// 绩效配置总开关
        /// </summary>
        [Column("config_flag")]
        public sbyte ConfigFlag { get; set; }
        /// <summary>
        /// 绩效配置修改时间
        /// </summary>
        [Column("config_flag_time")]
        public DateTime ConfigFlagTime { get; set; } = new DateTime(1900, 1, 1);
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