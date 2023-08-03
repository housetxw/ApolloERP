using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request.AccountCheck
{
    public class GetNoReconciliationAmountRequest
    {
        public List<long> ShopIds { get; set; }
    }
}
