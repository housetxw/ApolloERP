using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Core.Interfaces
{
    public interface ITestService
    {
        Task<GetListResponse> GetPurchase(GetListRequest request);
        Task<bool> GetShopInfo();
    }
}
