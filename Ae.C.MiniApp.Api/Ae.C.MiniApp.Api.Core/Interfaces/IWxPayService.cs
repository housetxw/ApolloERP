using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IWxPayService
    {
        Task<ApiResult<CreateWxPrePayOrderResponse>> CreateWxPrePayOrder(ApiRequest<CreateWxPrePayOrderRequest> request);
        Task<string> WxPayResultNotify(string requestXml);
    }
}
