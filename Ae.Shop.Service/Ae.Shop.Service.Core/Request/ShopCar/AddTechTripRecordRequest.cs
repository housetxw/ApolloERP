using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddTechTripRecordRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 技师Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;
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
        /// 开始里程图片
        /// </summary>
        public string StartMileageImg { get; set; } = string.Empty;
        /// <summary>
        /// 出发油耗 （格）
        /// </summary>
        public int StartOil { get; set; }
        /// <summary>
        /// 出发油耗图片
        /// </summary>
        public string StartOilImg { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
    }
}
