using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class OrderProductRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNos { get; set; }
    }
}
