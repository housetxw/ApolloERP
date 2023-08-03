using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetShopInfoByRefSmallWarehouseIdRequest:BaseGetRequest
    {
        public int SmallWarehouseId { get; set; }
    }
}
