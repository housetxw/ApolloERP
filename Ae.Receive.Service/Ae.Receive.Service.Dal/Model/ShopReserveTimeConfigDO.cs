using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Receive.Service.Dal.Model
{
    [Table("shop_reserve_time_config")]
    public class ShopReserveTimeConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; } 
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; } 
        /// <summary>
        /// 星期
        /// </summary>
        [Column("week_day")]
        public int WeekDay { get; set; } 
        /// <summary>
        /// 日期
        /// </summary>
        [Column("year_day")]
        public string YearDay { get; set; } = string.Empty;
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("start_time")]
        public string StartTime { get; set; } = string.Empty;
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("end_time")]
        public string EndTime { get; set; } = string.Empty;
        /// <summary>
        /// 可预约数量
        /// </summary>
        [Column("reserve_count")]
        public int ReserveCount { get; set; } 
        /// <summary>
        /// 配置类型（0：默认配置，1：自定义配置）
        /// </summary>
        [Column("config_type")]
        public int ConfigType { get; set; } 
        /// <summary>
        /// 预约类型（TireMaintenance：轮胎\\保养\\维修，OtherTemporary：美容\\装潢，SheetMetalSprayPainting：钣金喷漆）
        /// </summary>
        [Column("reserve_type")]
        public string ReserveType { get; set; } = string.Empty;
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