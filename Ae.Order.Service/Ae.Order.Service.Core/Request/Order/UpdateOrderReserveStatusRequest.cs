using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateOrderReserveStatusRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public sbyte ReserveStatus { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; }
    }
}
