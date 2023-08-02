using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request
{
   public class GetOrderPackageCardsRequest:BasePageRequest
    {
        public string UserPhone { get; set; }

        public string SourceOrderNo { get; set; }

        public string ProductName { get; set; }

        public int Status { get; set; }

        public string Code { get; set; }

        public int ShopId { get; set; } = 0;
    }
}
