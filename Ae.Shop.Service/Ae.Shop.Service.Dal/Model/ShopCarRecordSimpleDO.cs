using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class ShopCarRecordSimpleDO
    {
        /// <summary>
        /// 门店车辆使用记录
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 车辆ID
        /// </summary>
        public long CarId { get; set; }
        /// <summary>
        /// 技师名称
        /// </summary>
        public string Technician { get; set; } = string.Empty;
        /// <summary>
        /// 技师手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 用车开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 用车结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始公里数
        /// </summary>
        public int StartMileage { get; set; }
        /// <summary>
        /// 结束公里数
        /// </summary>
        public int EndMileage { get; set; }
        /// <summary>
        /// 油耗 L
        /// </summary>
        public decimal OilWear { get; set; }
    }
}
