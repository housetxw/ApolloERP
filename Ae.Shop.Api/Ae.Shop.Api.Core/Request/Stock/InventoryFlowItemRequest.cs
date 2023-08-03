using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class InventoryFlowItemRequest
    {
        public IEnumerable<long> StockIds { get; set; } = new List<long>();

        public string UpdateBy { get; set; }

        public int UpdateType { get; set; }

    }
}
