using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
    public class GetAccountCheckDetailRequest
    {
        public string OrderNo { get; set; }

        public long Id { get; set; }
    }
}
