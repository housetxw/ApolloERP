using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.OrderQuery
{
    public class GetPackageVerificationCodeDetailRequest : BaseGetRequest
    {
        public string Code { get; set; }
    }
}
