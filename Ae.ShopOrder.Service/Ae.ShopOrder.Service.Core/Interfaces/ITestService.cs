using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    public interface ITestService
    {
        Task<GetListResponse> GetPurchase(GetListRequest request);
        Task<bool> GetShopInfo();
    }
}
