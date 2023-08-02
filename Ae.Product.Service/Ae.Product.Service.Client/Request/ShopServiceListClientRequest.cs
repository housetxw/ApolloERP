using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Request
{
    public class ShopServiceListClientRequest
    {
        public int ShopId { get; set; }

        public List<string> ProductIds { get; set; }
    }
}
