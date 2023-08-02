using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Client.Clients
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

        public async Task<ApiResult<List<ReverseReasonConfigDTO>>> GetReverseReasonConfigs(GetReverseReasonConfigsRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.GetAsJsonAsync<GetReverseReasonConfigsRequest, ApiResult<List<ReverseReasonConfigDTO>>>(configuration["ReverseServer:GetReverseReasonConfigs"], request);
            return response;
        }

        public async Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelClientRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.PostAsJsonAsync<CreateReverseOrderForCancelClientRequest, ApiResult<CreateReverseOrderBaseClientResponse>>(configuration["ReverseServer:CreateReverseOrderForCancel"], request);
            return response;
        }

        public async Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForRefund(CreateReverseOrderForRefundClientRequest request)
        {
            var client = clientFactory.CreateClient("ReverseServer");
            var response = await client.PostAsJsonAsync<CreateReverseOrderForRefundClientRequest, ApiResult<CreateReverseOrderBaseClientResponse>>(configuration["ReverseServer:CreateReverseOrderForRefund"], request);
            return response;
        }
    }
}
