using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Pay;
using Ae.ShopOrder.Service.Client.Request.Pay;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.PayServer
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

        public async Task<ApiResult<List<MergePayDTO>>> GetMergePaysByMergeOrder(string orderno)
        {
            var client = clientFactory.CreateClient("PayServer");
            GetMergePayOrderRequest pay = new GetMergePayOrderRequest() { OrderNo = orderno };
            var response = await client.GetAsJsonAsync<GetMergePayOrderRequest, ApiResult<List<MergePayDTO>>>(configuration["PayServer:GetMergePaysByMergeOrder"], pay);
            return response;
        }
    }
}
