using Ae.OrderComment.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Response
{
    public class GetListResponse
    {
        public List<PurchaseVO> ShopList { get; set; }
        public int TotalItems { get; set; }
    }
}
