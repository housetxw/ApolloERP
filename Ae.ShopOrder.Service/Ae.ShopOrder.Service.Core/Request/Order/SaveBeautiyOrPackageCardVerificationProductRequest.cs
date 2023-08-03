using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SaveBeautiyOrPackageCardVerificationProductRequest:BaseGetRequest
    {
        public int RuleId { get; set; }

        public List<string> Pids { get; set; }

        public string UpdateBy { get; set; }
    }
}
