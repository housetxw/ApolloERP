using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class CancelOrderForReserverOrArrivalRequest
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }
    }
}
