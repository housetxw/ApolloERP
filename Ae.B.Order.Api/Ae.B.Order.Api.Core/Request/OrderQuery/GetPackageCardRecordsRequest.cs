using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetPackageCardRecordsRequest : BasePageRequest
    {
        public string UserName { get; set; }
    }
}
