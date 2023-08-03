using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
   public class GetUserAddressRequest
    {
        public string UserId { get; set; }

        public long AddressId { get; set; }
    }
}
