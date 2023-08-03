using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Core.Enums;

namespace Ae.Receive.Service.Core.Model
{
    public class ShopArrivalDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 到店时间
        /// </summary>
        public DateTime ArrivalTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 状态 0等待中 1施工中 2已完结 3 未消费离店
        /// </summary>
        public ShopArrivalStatus Status { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 客户联系方式
        /// </summary>
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; } = string.Empty;
        /// <summary>
        /// 车系（A4L)
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 品牌（奥迪）
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; }
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 职级
        /// </summary>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// 预约编号
        /// </summary>
        public int ReserveNo { get; set; }
        /// <summary>
        /// 排队类型（预约排队，到店排队）
        /// </summary>
        public string QueueType { get; set; } = string.Empty;
        /// <summary>
        /// 排队编号
        /// </summary>
        public string QueueNumber { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
