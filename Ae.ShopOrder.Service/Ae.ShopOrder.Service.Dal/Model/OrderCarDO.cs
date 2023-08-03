using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("order_car")]
    public class OrderCarDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Column("car_id")]
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 车辆昵称
        /// </summary>
        [Column("nick_name")]
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        [Column("car_number")]
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 品牌（奥迪）
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系（A4L)
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 车系Id
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        /// <summary>
        /// 排量（2.0T）
        /// </summary>
        [Column("pai_liang")]
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年份（2009）
        /// </summary>
        [Column("nian")]
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// Tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; } = string.Empty;
        /// <summary>
        /// 款型（2009款 2.0T 无极 标准版）
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; } = string.Empty;
        /// <summary>
        /// 总公里数
        /// </summary>
        [Column("total_mileage")]
        public int TotalMileage { get; set; }
        /// <summary>
        /// 最后一次保养公里数
        /// </summary>
        [Column("last_bao_yang_km")]
        public int LastBaoYangKm { get; set; }
        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        [Column("last_bao_yang_time")]
        public DateTime LastBaoYangTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// VIN码（车辆识别代号）
        /// </summary>
        [Column("vin_code")]
        public string VinCode { get; set; } = string.Empty;
        /// <summary>
        /// 默认车型
        /// </summary>
        [Column("default_car")]
        public sbyte DefaultCar { get; set; }
        /// <summary>
        /// 发动机编号
        /// </summary>
        [Column("engine_no")]
        public string EngineNo { get; set; } = string.Empty;
        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        [Column("tire_size_for_single")]
        public string TireSizeForSingle { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
