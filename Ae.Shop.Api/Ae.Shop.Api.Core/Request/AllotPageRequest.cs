using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class AllotPageRequest : BasePageRequest
    {
        public long ShopId { get; set; }

        public long TaskId { get; set; }

        public string SourceWarehouse { get; set; } = string.Empty;
        public string TargetWarehouse { get; set; } = string.Empty;

        public string TaskStatus { get; set; } = string.Empty;

        public string ProductName { get; set; }

        public List<string> Times { get; set; } = new List<string>();

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
