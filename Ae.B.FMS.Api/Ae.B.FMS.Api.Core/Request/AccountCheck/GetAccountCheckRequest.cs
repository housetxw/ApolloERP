using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request
{
    public class GetAccountCheckRequest
    {
        public int LocationId { get; set; }

        public string OrderNo { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PageIndex { get; set; }

        public string Telephone { get; set; }


        public int PageSize { get; set; }
       public List<string> InstallTime { get; set; }
        public string LocationIds { get; set; }

        public List<int> LocationIdList { get; set; }

    }
}
