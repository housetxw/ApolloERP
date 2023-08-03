using Ae.ShopOrder.Service.Client.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Response.User
{
    public class UserAddressResponse
    {
        public List<UserAddressDTO> AddressVOs { get; set; }
    }
}
