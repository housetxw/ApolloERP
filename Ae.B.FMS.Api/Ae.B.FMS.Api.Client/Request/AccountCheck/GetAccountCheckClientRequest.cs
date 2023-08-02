using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
   public class GetAccountCheckClientRequest
    {
        public int LocationId { get; set; }

        public string OrderNo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string LocationIds { get; set; }

    }
}
