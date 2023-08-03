using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class GetShopProductByCodeRequest
    {
        public List<string> ShopProductCodes { get; set; }
    }
}
