using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class ProductActivityByOrderNosClientRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNos { get; set; }
    }
}
