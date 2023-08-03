using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetStoragePlanProductsRequest:BasePageRequest
    {
        public long PlanId { get; set; }
        public long WarehouseId { get; set; }

        public string Status { get; set; }
    }
}
