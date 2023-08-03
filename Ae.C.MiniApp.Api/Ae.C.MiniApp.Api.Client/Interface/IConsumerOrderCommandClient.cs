using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Client.Response.Order;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IConsumerOrderCommandClient
    {
        Task<ApiResult<SubmitOrderClientResponse>> SubmitOrder(ApiRequest<SubmitOrderClientRequest> request);
        Task<ApiResult<BuyAgainClientResponse>> BuyAgain(ApiRequest<BuyAgainClientRequest> request);
    }
}
