using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request.AccountCheck
{
    public class GetPurchaseAccountListRequest : BasePageRequest
    {
        public long ShopId { get; set; }

        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }


        public string Status { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseNo { get; set; }
    }
}
