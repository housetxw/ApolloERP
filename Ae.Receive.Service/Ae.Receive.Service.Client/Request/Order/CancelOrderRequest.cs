using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class CancelOrderRequest
    {
        public string UserId { get; set; }

        public List<string> OrderNos { get; set; }
    }
}
