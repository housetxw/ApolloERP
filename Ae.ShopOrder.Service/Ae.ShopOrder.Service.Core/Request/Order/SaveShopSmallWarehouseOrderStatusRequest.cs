using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SaveShopSmallWarehouseOrderStatusRequest
    {
        public string OrderNo { get; set; }

        public string UpdateBy { get; set; }
    }
}
