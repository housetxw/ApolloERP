using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Receive.Service.Dal.Model
{
    [Table("shop_reserve")]
    public class ShopReserveDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 预约到店时间
        /// </summary>
        [Column("reserve_time")]
        public DateTime ReserveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 预约编号
        /// </summary>
        [Column("reserve_no")]
        public int ReserveNo { get; set; } 
        /// <summary>
        /// 是否到店等待
        /// </summary>
        [Column("is_wait")]
        public sbyte IsWait { get; set; } 
        /// <summary>
        /// 是否有订单预约
        /// </summary>
        [Column("is_any_order")]
        public int IsAnyOrder { get; set; }
        /// <summary>
        /// 渠道 1小程序 2门店
        /// </summary>
        [Column("channel")]
        public int Channel { get; set; } 
        /// <summary>
        /// 状态 0待确认 1已确认 2已完成 3已取消
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 0到店保养  1上门养护
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 客户联系方式
        /// </summary>
        [Column("user_tel")]
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 客户性别
        /// </summary>
        [Column("user_sex")]
        public string UserSex { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Column("car_id")]
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// VIN码
        /// </summary>
        [Column("vin_no")]
        public string VinNo { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        [Column("car_no")]
        public string CarNo { get; set; } = string.Empty;
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
        /// 款型
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; } = string.Empty;

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; } = string.Empty;

        /// <summary>
        /// 款型
        /// </summary>
        [Column("car_logo")]
        public string CarLogo { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; } 
        /// <summary>
        /// 工位号
        /// </summary>
        [Column("station_id")]
        public long StationId { get; set; } 
        /// <summary>
        /// 技师Id
        /// </summary>
        [Column("tech_id")]
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        [Column("tech_name")]
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 服务大类
        /// </summary>
        [Column("service_code")]
        public string ServiceCode { get; set; } = string.Empty;
        /// <summary>
        /// 服务大类名称
        /// </summary>
        [Column("service_name")]
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 取消人
        /// </summary>
        [Column("cancel_by")]
        public string CancelBy { get; set; } = string.Empty;
        /// <summary>
        /// 取消时间
        /// </summary>
        [Column("cancel_time")]
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 取消原因
        /// </summary>
        [Column("cancel_reason")]
        public string CancelReason { get; set; } = string.Empty;
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