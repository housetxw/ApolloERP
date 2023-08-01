using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Request
{
    public class ShopServiceListRequest
    {
        public int ShopId { get; set; }

        public List<string> ProductIds { get; set; }
    }
}
