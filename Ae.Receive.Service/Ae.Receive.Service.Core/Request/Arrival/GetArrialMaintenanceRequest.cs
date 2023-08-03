using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetArrialMaintenanceRequest
    {
        public string CarId { get; set; }

        public long ShopId { get; set; }
    }
}
