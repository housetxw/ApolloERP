using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class OrderAddressListClientRequest
    {
        public List<long> OrderIds { get; set; }
    }
}
