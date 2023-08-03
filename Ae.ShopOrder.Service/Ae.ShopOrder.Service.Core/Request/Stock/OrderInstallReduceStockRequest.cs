using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Stock
{
    public class OrderInstallReduceStockRequest
    {
        public string QueueNo { get; set; }

        public string CreateBy { get; set; }

        public string SourceType { get; set; }

        public string QueueStatus { get; set; }

        public long TargetShopId { get; set; }

    }
}
