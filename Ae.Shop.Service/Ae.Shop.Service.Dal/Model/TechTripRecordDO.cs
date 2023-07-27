using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("tech_trip_record")]
    public class TechTripRecordDO
    {
        /// <summary>
        /// 主键ID
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
        /// 技师Id
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 状态  0未还车 1已还车
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
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
        /// 开始里程图片
        /// </summary>
        [Column("start_mileage_img")]
        public string StartMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 结束里程图片
        /// </summary>
        [Column("end_mileage_img")]
        public string EndMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 出发油耗 （格）
        /// </summary>
        [Column("start_oil")]
        public int StartOil { get; set; }
        /// <summary>
        /// 还车油耗 （格）
        /// </summary>
        [Column("end_oil")]
        public int EndOil { get; set; } 
        /// <summary>
        /// 出发油耗图片
        /// </summary>
        [Column("start_oil_img")]
        public string StartOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 还车油耗图片
        /// </summary>
        [Column("end_oil_img")]
        public string EndOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 加油L
        /// </summary>
        [Column("refuelled")]
        public decimal Refuelled { get; set; }
        /// <summary>
        /// 用车开始时间
        /// </summary>
        [Column("start_time")]
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 还车时间
        /// </summary>
        [Column("return_time")]
        public DateTime ReturnTime { get; set; } = new DateTime(1900, 1, 1);
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