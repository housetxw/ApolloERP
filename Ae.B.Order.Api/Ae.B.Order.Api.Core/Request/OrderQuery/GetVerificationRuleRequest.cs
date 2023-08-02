using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetVerificationRuleRequest : BasePageRequest
    {
        public string Name { get; set; }
        public string Pid { get; set; }
    }
}
