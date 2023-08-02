using Ae.B.Activity.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Response
{
    public class GetListResponse
    {
        public List<PurchaseVO> ShopList { get; set; }
        public int TotalItems { get; set; }
    }
}
