using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderQuery
{
    public class GetPackageVerificationCodeDetailRequest:BaseGetRequest
    {
        public string Code { get; set; }
    }
}
