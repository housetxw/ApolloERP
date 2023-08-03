using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Reverse;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Reserve;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IReverseService
    {
        Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons(GetReverseReasonsRequest request);
        Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder(ApiRequest<CancelOrderRequest> request);

        Task<ApiResult> CancelNewOrder(ApiRequest<CancelNewOrderRequest> request);



    }
}
