using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetTripRecordPageListRequest : BaseOnlyPageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; } = 0;
        /// <summary>
        /// 员工Id
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称或手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 状态  0未还车 1已还车
        /// </summary>
        public sbyte Status { get; set; } = -1;
        /// <summary>
        /// 用车时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 还车时间
        /// </summary>
        public DateTime ReturnTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
