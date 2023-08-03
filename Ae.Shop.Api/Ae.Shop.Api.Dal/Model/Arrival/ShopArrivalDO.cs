using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.Shop.Api.Dal.Model.Arrival
{
    [Table("shop_arrival")]
    public class ShopArrivalDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 到店时间
        /// </summary>
        [Column("arrival_time")]
        public DateTime ArrivalTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 状态 0等待中 1施工中 2已完结 3 未消费离店
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; } = 0;
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
        /// 车辆Id
        /// </summary>
        [Column("car_id")]
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        [Column("car_no")]
        public string CarNo { get; set; } = string.Empty;
        /// <summary>
        /// 车系（A4L)
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 品牌（奥迪）
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; } = 0;
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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
        /// 职级
        /// </summary>
        [Column("level")]
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// 预约编号
        /// </summary>
        [Column("reserve_no")]
        public int ReserveNo { get; set; } = 0;
        /// <summary>
        /// 排队类型（预约排队，到店排队）
        /// </summary>
        [Column("queue_type")]
        public string QueueType { get; set; } = string.Empty;
        /// <summary>
        /// 排队编号
        /// </summary>
        [Column("queue_number")]
        public string QueueNumber { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; } = 0;
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

        /// <summary>
        /// 未消费离店时间
        /// </summary>
        [Column("cancel_time")]
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        ///完结时间
        /// </summary>
        [Column("finish_time")]
        public DateTime FinishTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        ///派工时间
        /// </summary>
        [Column("dispatch_time")]
        public DateTime DispatchTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
