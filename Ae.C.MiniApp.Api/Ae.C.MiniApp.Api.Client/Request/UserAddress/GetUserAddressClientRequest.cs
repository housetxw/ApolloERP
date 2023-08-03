using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.UserAddress
{
   public class GetUserAddressClientRequest
    {
        public string UserId { get; set; }

        public long AddressId { get; set; }
    }
}
