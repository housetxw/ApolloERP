using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetSupplierStockRequest
    {
        public string ProductId { get; set; }

        public long VenderId { get; set; }

        public string VenderShortName { get; set; }
    }
}
