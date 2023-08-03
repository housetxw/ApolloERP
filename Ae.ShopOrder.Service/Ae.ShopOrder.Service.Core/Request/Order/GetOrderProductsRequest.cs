using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrderProductsRequest : BasePageRequest
    {
        public string OrderNo { get; set; }

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();
    }
}
