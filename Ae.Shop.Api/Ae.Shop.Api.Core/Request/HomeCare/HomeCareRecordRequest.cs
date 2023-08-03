using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class HomeCareRecordRequest: BasePageRequest
    {
        public string TechId { get; set; }

        public string ProductName { get; set; }

        public string Status { get; set; }

        public List<string> Times { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public long ShopId { get; set; }
    }
}
 