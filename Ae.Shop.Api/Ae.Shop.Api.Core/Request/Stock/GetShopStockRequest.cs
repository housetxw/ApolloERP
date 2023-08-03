using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class GetShopStockRequest
    {
        public long ShopId { get; set; }

        public string ProductId { get; set; }

        public long BatchNo { get; set; }
    }
}
