using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Fms
{
    public class CalculationReconciliationFeeRequest
    {
        public string OrderNo { get; set; }

        public string CreateBy { get; set; }
    }
}
