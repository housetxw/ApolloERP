using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class BatchGetOrderAddressRequest
    {
        public List<long> OrderIds { get; set; }
    }
}
