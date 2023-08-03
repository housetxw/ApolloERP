using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request
{
    /// <summary>
    /// 订单车型请求对象
    /// </summary>
    public class GetOrderCarRequest
    {
        public List<long> OrderIds { get; set; }
    }
}
