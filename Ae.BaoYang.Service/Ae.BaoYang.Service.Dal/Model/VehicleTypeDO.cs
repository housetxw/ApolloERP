using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("vehicle_type")]
    public class VehicleTypeDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 车系Id
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; }
        /// <summary>
        /// 车系
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; }
        /// <summary>
        /// 轮胎尺寸;分割
        /// </summary>
        [Column("tires")]
        public string Tires { get; set; }
        /// <summary>
        /// 轮胎匹配
        /// </summary>
        [Column("tires_match")]
        public string TiresMatch { get; set; }
        /// <summary>
        /// 德系;意系;日系……
        /// </summary>
        [Column("brand_category")]
        public string BrandCategory { get; set; }
        /// <summary>
        /// 大型车;中型车;
        /// </summary>
        [Column("vehicle_body_type")]
        public string VehicleBodyType { get; set; }
        /// <summary>
        /// 均价
        /// </summary>
        [Column("avg_price")]
        public decimal AvgPrice { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        [Column("max_price")]
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        [Column("min_price")]
        public decimal MinPrice { get; set; }
        /// <summary>
        /// 车系级别
        /// </summary>
        [Column("vehicle_level")]
        public string VehicleLevel { get; set; }
        /// <summary>
        /// 四轮定位服务等级
        /// </summary>
        [Column("service_price_level")]
        public string ServicePriceLevel { get; set; }
        /// <summary>
        /// 品牌下的厂商排序
        /// </summary>
        [Column("factory_rank")]
        public int FactoryRank { get; set; }
        /// <summary>
        /// 厂商下的车型排序
        /// </summary>
        [Column("vehicle_rank")]
        public int VehicleRank { get; set; }
        /// <summary>
        /// 品牌拼音
        /// </summary>
        [Column("brand_py")]
        public string BrandPy { get; set; }
        /// <summary>
        /// 品牌简音
        /// </summary>
        [Column("brand_jpy")]
        public string BrandJpy { get; set; }
        /// <summary>
        /// 车系拼音
        /// </summary>
        [Column("vehicle_py")]
        public string VehiclePy { get; set; }
        /// <summary>
        /// 车系简音
        /// </summary>
        [Column("vehicle_jpy")]
        public string VehicleJpy { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        [Column("actived")]
        public int Actived { get; set; }
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
