using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateOrderDispatchStatusRequest
    {
        public List<string> OrderNos { get; set; }

        public string CreateBy { get; set; }

        public int DispatchStatus { get; set; } = 1;
    }
}
