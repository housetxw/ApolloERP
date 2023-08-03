using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Pay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Interface
{
    public interface IWxPayClient
    {
        Task<ApiResult<CreateWxPrePayOrderForMiniAppResponse>> CreateWxPrePayOrderForMiniApp(CreateWxPrePayOrderForMiniAppRequest request);
        Task<string> WxPayResultNotify(string requestXml);
    }
}
