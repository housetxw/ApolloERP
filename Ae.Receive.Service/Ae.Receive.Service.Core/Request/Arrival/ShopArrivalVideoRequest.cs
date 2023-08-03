using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class ShopArrivalVideoRequest:BaseGetRequest
    {
        public long ArrivalId { get; set; }
    }
}
