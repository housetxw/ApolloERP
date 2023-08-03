using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request.AccountCheck
{
    public class CalculationPurchaseReconciliationFeeRequest
    {
        /// <summary>
        /// 采购单Id
        /// </summary>
        public long PurchaseId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUser { get; set; }
    }
}
