using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Shop
{
    public class GetShopGrouponProductRequest
    {
        public long ShopId { get; set; }

        public sbyte? Status { get; set; }
    }
}
