using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class TechTripRecordDTO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 技师Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 状态 0未还车 1已还车
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 车辆ID
        /// </summary>
        public long CarId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 开始公里数
        /// </summary>
        public int StartMileage { get; set; }
        /// <summary>
        /// 结束公里数
        /// </summary>
        public int EndMileage { get; set; }
        /// <summary>
        /// 开始里程图片
        /// </summary>
        public string StartMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 结束里程图片
        /// </summary>
        public string EndMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 出发油耗 （格）
        /// </summary>
        public int StartOil { get; set; }
        /// <summary>
        /// 还车油耗 （格）
        /// </summary>
        public int EndOil { get; set; }
        /// <summary>
        /// 出发油耗图片
        /// </summary>
        public string StartOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 还车油耗图片
        /// </summary>
        public string EndOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 加油L
        /// </summary>
        public decimal Refuelled { get; set; }
        /// <summary>
        /// 还车时间
        /// </summary>
        public DateTime ReturnTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否删除 0否 1是
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
