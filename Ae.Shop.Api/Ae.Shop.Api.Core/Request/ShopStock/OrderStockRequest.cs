using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class OrderStockRequest
    {
        public string QueueNo { get; set; }

        public string CreateBy { get; set; }

        public string OperationType { get; set; }
        public string SourceType { get; set; }

        public string QueueStatus { get; set; }

        public long TargetShopId { get; set; }
    }


    public class GetOrderQueueRequest : BasePageRequest
    {
        public long Id { get; set; }

        public string QueueStatus { get; set; }

        public string QueueNo { get; set; }
    }
}
