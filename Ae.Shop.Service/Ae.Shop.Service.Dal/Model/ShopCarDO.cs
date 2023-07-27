using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_car")]
    public class ShopCarDO
    {
        /// <summary>
        /// 门店车ID
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 状态 0正常 1禁用 2未还车
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; } 
        /// <summary>
        /// 车牌
        /// </summary>
        [Column("car_number")]
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 车系Id
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; } = string.Empty;
        /// <summary>
        /// 排量
        /// </summary>
        [Column("pai_liang")]
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年
        /// </summary>
        [Column("nian")]
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; } = string.Empty;
        /// <summary>
        /// 款型
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; } = string.Empty;
        /// <summary>
        /// 车架号
        /// </summary>
        [Column("vin_code")]
        public string VinCode { get; set; } = string.Empty;
        /// <summary>
        /// 公里数
        /// </summary>
        [Column("total_mileage")]
        public int TotalMileage { get; set; } 
        /// <summary>
        /// 车辆期初价格
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; } 
        /// <summary>
        /// 保险开始日期
        /// </summary>
        [Column("insure_start")]
        public DateTime InsureStart { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 保险结束日期
        /// </summary>
        [Column("insure_end")]
        public DateTime InsureEnd { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 保险公司
        /// </summary>
        [Column("insurance_company")]
        public string InsuranceCompany { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
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