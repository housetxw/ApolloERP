using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BasicData.Service.Core.Interfaces
{
    public interface ITestService
    {
        Task<GetListResponse> GetPurchase(GetListRequest request);
        Task<bool> GetShopInfo();
    }
}
