using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Reverse
{
    public class ReverseClient : IReverseClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ReverseClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelClientRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.PostAsJsonAsync<CreateReverseOrderForCancelClientRequest, ApiResult<CreateReverseOrderBaseClientResponse>>(configuration["ReverseServer:CreateReverseOrderForCancel"], request);
            return response;
        }

        public async Task<ApiResult<List<ReverseReasonConfigDTO>>> GetReverseReasonConfigs(GetReverseReasonConfigsClientRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.GetAsJsonAsync<GetReverseReasonConfigsClientRequest, ApiResult<List<ReverseReasonConfigDTO>>>(configuration["ReverseServer:GetReverseReasonConfigs"], request);
            return response;
        }
    }
}
