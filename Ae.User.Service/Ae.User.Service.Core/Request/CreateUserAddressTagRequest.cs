using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
{
   public class CreateUserAddressTagRequest
    {
        public string UserId { get; set; }

        public string TagName { get; set; }

        public string CreateBy { get; set; }
    }
}
