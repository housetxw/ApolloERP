using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetAvailableBatchsRequest
    {
        public long ShopId { get; set; }

        public string ProductId { get; set; }
    }
}
