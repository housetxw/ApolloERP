using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopGrouponProductRequest
    {
        public long ShopId { get; set; }

        public sbyte ? Status { get; set; }
    }
}
