using Ae.Shop.Service.Client.Model.Receive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Response
{
   public class GetShopArrivalOrderForStaticResponse
    {
        public List<ShopArrivalOrderClientDTO> ShopArrivalOrders { get; set; } = new List<ShopArrivalOrderClientDTO>();

        public List<string> UserIds { get; set; } = new List<string>();

    }
}
