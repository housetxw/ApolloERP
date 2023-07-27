using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class ModifyShopSuspendTimeRequest
    {
        public List<string> Times { get; set; }

        public long ShopId { get; set; }

        public string UdpateBy { get; set; } = string.Empty;
    }
}
