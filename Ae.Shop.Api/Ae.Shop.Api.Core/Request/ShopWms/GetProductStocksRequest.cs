using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class GetProductStocksRequest
    {
        public string ProductId { get; set; }

        public List<string> Times { get; set; }

        public long LocationId { get; set; }
    }
}
