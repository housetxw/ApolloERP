using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface ITestService
    {
        Task<GetListResponse> GetPurchase(GetListRequest request);
        Task<bool> GetShopInfo();
    }
}
