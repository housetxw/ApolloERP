using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.OrderForC
{
    /// <summary>
    /// 订单车型请求
    /// </summary>
    public class GetOrderCarClientRequest
    {
        public List<long> OrderIds { get; set; }
    }
}
