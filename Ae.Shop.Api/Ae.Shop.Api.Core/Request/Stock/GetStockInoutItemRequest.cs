using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetStockInoutItemRequest
    {
        public long ShopId { get; set; }

        public List<string> PIds { get; set; } = new List<string>();

        public string ObjectId { get; set; }

        public string ObjectType { get; set; }
    }
}
