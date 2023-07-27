using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_car_record")]
    public class ShopCarRecordDO
    {
        /// <summary>
        /// 门店车辆使用记录
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
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 车辆ID
        /// </summary>
        [Column("car_id")]
        public long CarId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        [Column("car_number")]
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称
        /// </summary>
        [Column("technician")]
        public string Technician { get; set; } = string.Empty;
        /// <summary>
        /// 技师手机号
        /// </summary>
        [Column("mobile")]
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 用车开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 用车结束时间
        /// </summary>
        [Column("end_time")]
        public DateTime EndTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始公里数
        /// </summary>
        [Column("start_mileage")]
        public int StartMileage { get; set; } 
        /// <summary>
        /// 结束公里数
        /// </summary>
        [Column("end_mileage")]
        public int EndMileage { get; set; } 
        /// <summary>
        /// 油耗 L
        /// </summary>
        [Column("oil_wear")]
        public decimal OilWear { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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