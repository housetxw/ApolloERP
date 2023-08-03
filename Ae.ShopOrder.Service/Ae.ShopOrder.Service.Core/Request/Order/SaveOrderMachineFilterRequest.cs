using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SaveOrderMachineFilterRequest
    {
        public string OrderNo { get; set; }

        public long ShopId { get; set; }

        public string CreateBy { get; set; }
    }
}
