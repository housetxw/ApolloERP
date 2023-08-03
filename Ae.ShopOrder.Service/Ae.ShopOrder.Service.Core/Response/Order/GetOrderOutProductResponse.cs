using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetOrderOutProductResponse
    {
        public long ShopId { get; set; }
        public string OrderNo { get; set; }

        public  string UserId { get; set; }

        public string UserPhone { get; set; }

        public  string ProductId { get; set; }

        public string ProductName { get; set; }

        public int TotalNumber { get; set; }

        public decimal Amount { get; set; }

        public  decimal SaleOrderPrice { get; set; }

        public string CarNumber { get; set; }

        public string CarInfo { get; set; }

        public  string CreateTime { get; set; }
    }
}
