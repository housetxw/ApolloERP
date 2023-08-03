using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    /// <summary>
    /// 订单操作服务
    /// </summary>
    public interface IOrderCommandService
    {
        Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request);
        Task<ApiResult<BuyAgainResponse>> BuyAgain(ApiRequest<BuyAgainRequest> request);
    }
}
