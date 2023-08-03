using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.UserAddress
{
   public class CreateUserAddressTagClientRequest
    {
        public string UserId { get; set; }

        public string TagName { get; set; }

        public string CreateBy { get; set; }
    }
}
