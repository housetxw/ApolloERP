using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request.Settlement
{
    public class SavePurchaseSettlementReviewRequest
    {
        public long ShopId { get; set; }
        public string CreateUser { get; set; }

        public string SettlementNo { get; set; }

        public sbyte Status { get; set; }

        public string Remark { get; set; }
    }
}
