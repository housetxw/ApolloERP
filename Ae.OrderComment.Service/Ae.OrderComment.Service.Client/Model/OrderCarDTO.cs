using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Model
{
    public class OrderCarDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 车辆昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; } = string.Empty;
        /// <summary>
        /// 品牌（奥迪）
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系（A4L)
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;
        /// <summary>
        /// 排量（2.0T）
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年份（2009）
        /// </summary>
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// Tid
        /// </summary>
        public string Tid { get; set; } = string.Empty;
        /// <summary>
        /// 款型（2009款 2.0T 无极 标准版）
        /// </summary>
        public string SalesName { get; set; } = string.Empty;
        /// <summary>
        /// 总公里数
        /// </summary>
        public int TotalMileage { get; set; }
        /// <summary>
        /// 最后一次保养公里数
        /// </summary>
        public int LastBaoYangKm { get; set; }
        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        public DateTime LastBaoYangTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// VIN码（车辆识别代号）
        /// </summary>
        public string VinCode { get; set; } = string.Empty;
        /// <summary>
        /// 默认车型
        /// </summary>
        public sbyte DefaultCar { get; set; }
        /// <summary>
        /// 发动机编号
        /// </summary>
        public string EngineNo { get; set; } = string.Empty;
        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSizeForSingle { get; set; } = string.Empty;
    }
}
