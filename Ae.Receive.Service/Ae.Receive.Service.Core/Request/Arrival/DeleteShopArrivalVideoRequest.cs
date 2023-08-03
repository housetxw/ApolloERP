using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class DeleteShopArrivalVideoRequest
    {
        public long Id { get; set; }
        public long ArrivalId { get; set; }

        public string CreateBy { get; set; }
    }
}
