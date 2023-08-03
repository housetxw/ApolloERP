using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetArrivalOrderListRequest
    {
        public List<long> ArrivalIds { get; set; }
    }
}
