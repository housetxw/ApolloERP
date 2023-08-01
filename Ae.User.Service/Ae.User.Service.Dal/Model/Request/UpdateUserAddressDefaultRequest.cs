using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model.Request
{
   public class UpdateUserAddressDefaultRequest
    {
        public string UpdateBy { get; set; }

        public long AddressId { get; set; }

        public int DefaultAddress { get; set; }
    }
}
