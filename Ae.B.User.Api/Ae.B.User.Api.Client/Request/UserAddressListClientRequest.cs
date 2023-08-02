using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request
{
    public class UserAddressListClientRequest
    {
        public string UserId { get; set; }

        public long AddressId { get; set; }
    }
}
