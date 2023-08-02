using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetMergeOrderListRequest : BasePageRequest
    {
        public string OrderNo { get; set; }

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }

    }
}
