using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetTodayReceivePackageRequest:BasePageRequest
    {
        public long ShopId { get; set; }
    }
}
