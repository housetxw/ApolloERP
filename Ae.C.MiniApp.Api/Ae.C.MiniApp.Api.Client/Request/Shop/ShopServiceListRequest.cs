using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Shop
{
   public class ShopServiceListRequest
    {
        public int ShopId { get; set; }

        public List<string> ProductIds { get; set; }
    }
}
