using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
  public  class GetShopArrivalOrderForStaticResponse
    {
        public List<ShopArrivalOrderDTO> ShopArrivalOrders { get; set; } = new List<ShopArrivalOrderDTO>();

        public List<string> UserIds { get; set; } = new List<string>();
    }
}
