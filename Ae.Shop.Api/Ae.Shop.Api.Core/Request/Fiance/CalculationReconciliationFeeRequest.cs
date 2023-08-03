using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Fiance
{
    public class CalculationReconciliationFeeRequest
    {
        public string OrderNo { get; set; }

        public string CreateBy { get; set; }
    }
}
