using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;

namespace Ae.ConsumerOrder.Service.Client.Clients
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

        public async Task<ApiResult<CreateReverseOrderBaseResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.PostAsJsonAsync<CreateReverseOrderForCancelRequest, ApiResult<CreateReverseOrderBaseResponse>>(configuration["ReverseServer:CreateReverseOrderForCancel"], request);
            return response;
        }
    }
}
