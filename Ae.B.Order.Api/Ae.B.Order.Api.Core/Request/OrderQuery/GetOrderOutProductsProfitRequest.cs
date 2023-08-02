using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderOutProductsProfitRequest : BasePageRequest
    {
        public string OrderNo { get; set; }
        public string PurchaseNo { get; set; }
        public string ProductId { get; set; }

        public long ShopId { get; set; }

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();
    }
}
