using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IReverseClient
    {
        Task<ApiResult<CreateReverseOrderBaseResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelRequest request);
    }
}
