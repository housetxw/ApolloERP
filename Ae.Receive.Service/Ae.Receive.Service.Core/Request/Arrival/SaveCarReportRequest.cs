using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class SaveCarReportRequest
    {
        public string Url { get; set; }

        public long ArrivalId { get; set; }

        public string CreateUser { get; set; }
    }
}
