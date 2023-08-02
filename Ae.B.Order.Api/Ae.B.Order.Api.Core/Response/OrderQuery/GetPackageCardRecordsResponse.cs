using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderQuery
{
   public class GetPackageCardRecordsResponse
    {
        public long Id { get; set; }

        public string OrderNo { get; set; }

        public DateTime OrderTime { get; set; }

        public string UserName { get; set; }

        public string UserPhone { get; set; }

        public decimal ActualAmount { get; set; }

    }
}
