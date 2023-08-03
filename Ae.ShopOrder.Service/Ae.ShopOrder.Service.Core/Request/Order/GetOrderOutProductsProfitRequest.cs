using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrderOutProductsProfitRequest:BasePageRequest
    {
        public string OrderNo { get; set; }
        public string PurchaseNo { get; set; }
        public string ProductId { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }
    }
}
