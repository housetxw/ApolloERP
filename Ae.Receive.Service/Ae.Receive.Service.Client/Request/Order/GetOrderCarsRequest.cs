using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class GetOrderCarsRequest
    {
        /// <summary>
        /// 订单IDS
        /// </summary>
        public List<string> OrderNos { get; set; }
    }
}
