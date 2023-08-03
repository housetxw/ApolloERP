using Ae.C.MiniApp.Api.Client.Model.UserAddress;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.UserAddress
{
   public class UserAddressClientResponse
    {
        public List<UserAddressDTO> AddressVOs { get; set; }
    }
}
