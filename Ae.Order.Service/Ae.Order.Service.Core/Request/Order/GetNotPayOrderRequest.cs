using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetNotPayOrderRequest:BasePageRequest
    {
        public string StartDate { get; set; }

    }
}
