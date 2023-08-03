using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.Product
{
    public  class GetShopProductByCodeRequest
    {
        public List<string> ShopProductCodes { get; set; }
    }
}
