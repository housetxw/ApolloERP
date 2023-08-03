using ApolloErp.Web.WebApi;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Client.Interface;
using System.Collections.Generic;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;

namespace Ae.C.MiniApp.Api.Client.Clients.Order
{
    public class OrderQueryClient : IOrderQueryClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public OrderQueryClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiPagedResult<GetOrderListForMiniAppClientResponse>> GetOrderListForMiniApp(GetOrderListForMiniAppClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderListForMiniAppClientRequest, ApiPagedResult<GetOrderListForMiniAppClientResponse>>(configuration["OrderServer:GetOrderListForMiniApp"], request);
            return response;
        }

        public async Task<ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>> GetEachStatusOrderCountForMiniApp(GetEachStatusOrderCountForMiniAppRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetEachStatusOrderCountForMiniAppRequest, ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>>(configuration["OrderServer:GetEachStatusOrderCountForMiniApp"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderPackageCardsRequest, ApiPagedResult< GetOrderPackageCardsResponse>>(configuration["OrderServer:GetOrderPackageCards"], request);
            return response;
        }

        public async Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetPackageCardMainInfoRequest, ApiResult<List<GetPackageCardMainInfoResponse>>>(configuration["OrderServer:GetPackageCardMainInfo"], request);
            return response;
        }
    }
}
