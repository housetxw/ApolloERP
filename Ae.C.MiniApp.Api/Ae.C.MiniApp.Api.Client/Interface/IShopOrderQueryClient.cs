using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Core.Request.Reverse;

namespace Ae.C.MiniApp.Api.Client.Interface
{
   public interface IShopOrderQueryClient
    {
        Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetailForMiniApp(GetOrderDetailClientRequest request);

        Task<ApiResult> CancelOrder(ApiRequest<CancelNewOrderRequest> request);

    }
}
