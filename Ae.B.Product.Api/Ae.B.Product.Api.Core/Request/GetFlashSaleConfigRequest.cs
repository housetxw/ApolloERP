using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    public class GetFlashSaleConfigRequest : BasePageRequest
    {
        public string ProductName { get; set; }

        public string Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
