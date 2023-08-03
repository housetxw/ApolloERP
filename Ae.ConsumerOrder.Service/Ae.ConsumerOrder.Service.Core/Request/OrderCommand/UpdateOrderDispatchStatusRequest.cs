using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderCommand
{
    public class UpdateOrderDispatchStatusRequest
    {
        public List<string> OrderNos { get; set; }

        public string CreateBy { get; set; }

        public int DispatchStatus { get; set; } = 1;
    }
}
