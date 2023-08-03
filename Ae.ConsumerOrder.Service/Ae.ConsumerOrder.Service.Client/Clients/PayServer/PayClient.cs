using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Request;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class PayClient : IPayClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public PayClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<List<PayDTO>>> GetPaysByOrderNo(GetPaysByOrderNoRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response = await client.GetAsJsonAsync<GetPaysByOrderNoRequest, ApiResult<List<PayDTO>>>(configuration["PayServer:GetPaysByOrderNo"], request);
            return response;
        }
    }
}
