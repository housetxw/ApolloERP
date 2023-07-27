using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
  public  class GetShopNewCustomerRequest
    {
        public List<string> UserIds { get; set; } = new List<string>();

        public List<long> ShopIds { get; set; } = new List<long>();
    }
}
