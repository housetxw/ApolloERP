using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IReverseClient
    {
        Task<ApiResult<List<ReverseReasonConfigDTO>>> GetReverseReasonConfigs(GetReverseReasonConfigsClientRequest request);
        Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelClientRequest request);
    }
}
