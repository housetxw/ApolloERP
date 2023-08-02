using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class SaveVerificationRuleRequest
    {
        public int Id { get; set; }
        public int IsValid { get; set; }
        public string Name { get; set; }

        public int IsByShopType { get; set; }

        public int ShopType { get; set; }

        public int IsByShopId { get; set; }

        public string UseRuleDesc { get; set; }

        public int ValidStartType { get; set; }

        public int ValidEndType { get; set; }

        public int ValidDays { get; set; }

        public string EarliestUseDate { get; set; }

        public string LatestUseDate { get; set; }

        public List<int> ShopIds { get; set; }

        public string UpdateBy { get; set; }

        public string UpdateTime { get; set; }

    }
}
