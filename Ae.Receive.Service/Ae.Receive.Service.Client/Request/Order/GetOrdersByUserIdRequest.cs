using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class GetOrdersByUserIdRequest
    {
        public string UserId { get; set; }

        public long ShopId { get; set; }
    }
}
