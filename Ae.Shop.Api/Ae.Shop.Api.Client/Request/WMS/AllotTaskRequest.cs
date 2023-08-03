using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    public class AllotTaskRequest
    {
        public string TaskNo { get; set; }

        public string UpdateBy { get; set; }
        public string TaskStatus { get; set; }

    }
}
