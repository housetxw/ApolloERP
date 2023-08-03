using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
{
    public class GetListResponse
    {
        public List<ShopVO> ShopList { get; set; }
        public int TotalItems { get; set; }
    }
}
