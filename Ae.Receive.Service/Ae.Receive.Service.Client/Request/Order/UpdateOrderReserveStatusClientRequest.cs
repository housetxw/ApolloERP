using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class UpdateOrderReserveStatusClientRequest
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
