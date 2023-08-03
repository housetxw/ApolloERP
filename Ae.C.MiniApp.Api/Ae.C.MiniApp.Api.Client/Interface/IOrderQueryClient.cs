using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IOrderQueryClient
    {
        Task<ApiPagedResult<GetOrderListForMiniAppClientResponse>> GetOrderListForMiniApp(GetOrderListForMiniAppClientRequest request);
        Task<ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>> GetEachStatusOrderCountForMiniApp(GetEachStatusOrderCountForMiniAppRequest clientRequest);

        Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request);

        Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request);
    }
}