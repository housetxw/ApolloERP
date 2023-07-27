using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
  public  class GetShopServiceTypePageRequest: BasePageRequest
    {
        public long ShopId { get; set; }
    }
}
