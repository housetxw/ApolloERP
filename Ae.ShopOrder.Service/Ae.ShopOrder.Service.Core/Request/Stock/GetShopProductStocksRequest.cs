using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Stock
{
    public class GetShopProductStocksRequest
    {
        public long ShopId { get; set; }

        public List<string> Pids { get; set; } = new List<string>();
    }
}
