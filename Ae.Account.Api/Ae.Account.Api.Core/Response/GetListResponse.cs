using Ae.Account.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Core.Response
{
    public class GetListResponse
    {
        public List<Purchase> ShopList { get; set; }
        public int TotalItems { get; set; }
    }
}
