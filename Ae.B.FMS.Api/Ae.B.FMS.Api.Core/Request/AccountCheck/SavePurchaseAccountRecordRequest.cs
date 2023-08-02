using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request.AccountCheck
{
    public class SavePurchaseAccountRecordRequest
    {
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUser { get; set; }


        public string Status { get; set; }

        public decimal SettlementAmount { get; set; }

        /// <summary>
        /// 采购单No
        /// </summary>
        public List<PurchaseAccount> PurchaseAccounts { get; set; }

    }

    public class PurchaseAccount
    {
        public long Id { get; set; }

    }
}
