using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request.AccountCheck
{
    public class CalculationReconciliationFeeRequest
    {
        public string OrderNo { get; set; }

        public string CreateBy { get; set; }
    }
}
