using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    /// <summary>
    /// 未消费离店原因返回对象
    /// </summary>
    public class LeaveShopReasonResponse
    {
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
