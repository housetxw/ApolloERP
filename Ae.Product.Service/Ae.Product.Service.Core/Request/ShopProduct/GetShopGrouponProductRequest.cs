using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    public class GetShopGrouponProductRequest
    {
        public long ShopId { get; set; }

        public sbyte? Status { get; set; }
    }
}
