using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetStoragePlanRequest : BasePageRequest
    {
        public List<string> Times { get; set; } = new List<string>();

        public string Status { get; set; }

        public string Type { get; set; }

        public long ShopId { get; set; }

        public long WarehouseId { get; set; }

        public string SourceType { get; set; }
    }
}
