using Ae.Shop.Service.Client.Model.ShopOrder;
using Ae.Shop.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
  public  interface IShopOrderClient
    {
        Task<List<OrderDispatchClientDTO>> GetOrderDispatch(GetOrderDispatchClientRequest request);
    }
}
