using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
{
  public  class GetUserAddressRequest
    {
        public string UserId { get; set; }

        public long AddressId { get; set; }
    }
}
