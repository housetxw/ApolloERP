using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Core.Interfaces
{
    public interface ITestService
    {
        Task<GetListResponse> GetPurchase(GetListRequest request);
        Task<bool> GetShopInfo();
    }
}
