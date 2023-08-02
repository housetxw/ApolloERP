using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderQuery
{
    public class GetOrderDetailStaticReportResponse
    {
        public long OrderId { get; set; }
        public string OrderTime { get; set; }

        public string OrderNo { get; set; }

        public string OrderStatus { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public long ShopId { get; set; }

        public string CarNumber { get; set; }

        public string DispatchTime { get; set; }

        public string ShopName { get; set; }

        public string TechName { get; set; }

        public string CheckName { get; set; }

        public string OrderAmount { get; set; }

        public string ActualAmount { get; set; }

        public string DiscountContent { get; set; }

        public string Remark { get; set; }
    }
}
