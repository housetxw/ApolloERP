using Ae.ConsumerOrder.Service.Client.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response.User
{
    public class UserAddressResponse
    {
        public List<UserAddressDTO> AddressVOs { get; set; }
    }
}
