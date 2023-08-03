using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetPackageCardMainInfoRequest:BaseGetRequest
    {
        public string UserId { get; set; }
    }
}
