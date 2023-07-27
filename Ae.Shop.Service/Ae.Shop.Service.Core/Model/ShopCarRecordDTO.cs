using System;
using System.Collections.Generic;
using System.Text;
 
namespace Ae.Shop.Service.Core.Model
{
    public class ShopCarRecordDTO
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
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 车辆ID
        /// </summary>
        public long CarId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
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
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
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