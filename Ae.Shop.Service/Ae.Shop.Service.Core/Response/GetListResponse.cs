using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetListResponse
    {
        public List<Purchase> ShopList { get; set; }
        public int TotalItems { get; set; }
    }
}
