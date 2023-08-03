using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class CalculatePurchaseOrderRequest
    {
        public long PurchaseId { get; set; }

        public string CreateUser { get; set; }
    }
}
