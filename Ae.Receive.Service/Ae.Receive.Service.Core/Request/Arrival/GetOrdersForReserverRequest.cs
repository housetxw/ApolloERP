using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 预约订单
    /// </summary>
    public class GetOrdersForReserverRequest:BaseGetRequest
    {
        /// <summary>
        /// 订单集合
        /// </summary>
        public List<string> OrderNos { get; set; }
    }
}
