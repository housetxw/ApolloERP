using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetWaitingPaySmallWarehouseOrderResponse
    {
        public string OrderNo { get; set; }

        public decimal ActualAmount { get; set; }

        public string CreateTime { get; set; }
    }
}
