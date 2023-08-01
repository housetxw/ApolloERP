using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    public class DeleteUserAddressRequest
    {
        public long AddressId { get; set; }

        public string UserId { get; set; }

        public string UpdateBy { get; set; }
    }
}
