using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class GetCarByOrderNoClientRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
