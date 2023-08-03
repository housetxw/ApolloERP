using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetWaitingPaySmallWarehouseOrderRequest
    {
        public string OrderNo { get; set; }

        public string ReconciliationDate { get; set; }

        public long ShopId { get; set; }


    }
}
