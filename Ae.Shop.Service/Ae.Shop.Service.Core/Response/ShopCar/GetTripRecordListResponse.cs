using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetTripRecordListResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; } 
        /// <summary>
        /// 出行时间
        /// </summary>
        public string TripTime { get; set; }
        /// <summary>
        /// 状态  0未还车 1已还车
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 还车状态 0未还  1已还
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 技师手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 用车时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 还车时间
        /// </summary>
        public DateTime ReturnTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
