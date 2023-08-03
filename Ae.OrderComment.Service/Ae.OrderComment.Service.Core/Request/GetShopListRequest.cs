using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
  public  class GetShopListRequest
    {
        public long ShopId { get; set; }

        public string ShopName { get; set; }
    }
}
