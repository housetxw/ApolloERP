using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IShopServerClient
    {
        Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request);
    }
}
