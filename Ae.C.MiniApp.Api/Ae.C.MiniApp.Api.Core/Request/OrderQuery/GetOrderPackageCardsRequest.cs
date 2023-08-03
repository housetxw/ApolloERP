using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.OrderQuery
{
   public class GetOrderPackageCardsRequest: BasePageRequest
    {
        public string SourceOrderNo { get; set; }
    }
}
