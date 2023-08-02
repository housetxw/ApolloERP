using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderProductsRequest : BasePageRequest
    {
        public string OrderNo { get; set; }

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public long ShopId { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();
    }
}
