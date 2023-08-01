using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_type_timing")]
    public class VehicleTypeTimingDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }
        /// <summary>
        /// 车系id
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; }
        /// <summary>
        /// 厂商
        /// </summary>
        [Column("factory")]
        public string Factory { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        [Column("vehicle_series")]
        public string VehicleSeries { get; set; }
        /// <summary>
        /// 车系
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; }
        /// <summary>
        /// 排量
        /// </summary>
        [Column("pai_liang")]
        public string PaiLiang { get; set; }
        /// <summary>
        /// 款型
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; }
        /// <summary>
        /// 年款
        /// </summary>
        [Column("nian")]
        public string Nian { get; set; }
        /// <summary>
        /// 车系级别：中型车，紧凑型SUV，紧凑型车……
        /// </summary>
        [Column("vehicle_level")]
        public string VehicleLevel { get; set; }
        /// <summary>
        /// 车系类型：三厢车，SUV，两厢车，三厢车……
        /// </summary>
        [Column("vehicle_type")]
        public string VehicleType { get; set; }
        /// <summary>
        /// 上市月份
        /// </summary>
        [Column("listed_month")]
        public string ListedMonth { get; set; }
        /// <summary>
        /// 上市年份
        /// </summary>
        [Column("listed_year")]
        public string ListedYear { get; set; }
        /// <summary>
        /// 生产年份
        /// </summary>
        [Column("manufacture_year")]
        public string ManufactureYear { get; set; }
        /// <summary>
        /// 生产状况
        /// </summary>
        [Column("production_status")]
        public string ProductionStatus { get; set; }
        /// <summary>
        /// 排气标准：国Ⅴ，国Ⅳ（国Ⅴ），欧Ⅵ……
        /// </summary>
        [Column("emission_standard")]
        public string EmissionStandard { get; set; }
        /// <summary>
        /// 停产年份
        /// </summary>
        [Column("stop_production_year")]
        public string StopProductionYear { get; set; }
        /// <summary>
        /// 品牌所属国家
        /// </summary>
        [Column("country")]
        public string Country { get; set; }
        /// <summary>
        /// 销售状态：在销，停销
        /// </summary>
        [Column("sales_status")]
        public string SalesStatus { get; set; }
        /// <summary>
        /// 国产;合资;进口
        /// </summary>
        [Column("joint_venture")]
        public string JointVenture { get; set; }
        /// <summary>
        /// 机油类别：汽油，柴油，
        /// </summary>
        [Column("fuel_type")]
        public string FuelType { get; set; }
        /// <summary>
        /// 指导价
        /// </summary>
        [Column("guide_price")]
        public decimal GuidePrice { get; set; }
        /// <summary>
        /// 最小价
        /// </summary>
        [Column("min_price")]
        public decimal MinPrice { get; set; }
        /// <summary>
        /// 均价
        /// </summary>
        [Column("avg_price")]
        public decimal AvgPrice { get; set; }
        /// <summary>
        /// 最大价
        /// </summary>
        [Column("max_price")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// 气缸数量
        /// </summary>
        [Column("cylinder_number")]
        public int CylinderNumber { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public string Status { get; set; }

        [Column("factory_code")]
        public string FactoryCode { get; set; }

        [Column("factory_en")]
        public string FactoryEn { get; set; }

        [Column("vehicle_en")]
        public string VehicleEn { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
