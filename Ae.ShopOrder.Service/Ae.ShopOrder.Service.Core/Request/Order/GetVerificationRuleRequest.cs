using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetVerificationRuleRequest:BasePageRequest
    {
        public string Name { get; set; }
        public string Pid { get; set; }

    }
}
