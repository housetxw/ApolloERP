using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class GetShopOwnProductStockRequest:BasePageRequest
    {
        public long LocationId { get; set; }

        public string ProductName { get; set; }
        
    }
}
