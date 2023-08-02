using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderCommand
{
    public class SaveBeautiyOrPackageCardVerificationProductRequest : BaseGetRequest
    {
        public int RuleId { get; set; }

        public List<string> Pids { get; set; }

        public string UpdateBy { get; set; }
    }
}
