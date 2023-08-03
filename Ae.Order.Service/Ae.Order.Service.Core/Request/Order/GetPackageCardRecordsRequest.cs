using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request
{
   public class GetPackageCardRecordsRequest:BasePageRequest
    {
        public string UserName { get; set; }
   }
}
