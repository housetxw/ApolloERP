using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request.Address
{
    public class DeleteUserAddressClientRequest
    {
        public long AddressId { get; set; }

        public string UserId { get; set; }

        public string UpdateBy { get; set; }
    }
}
