using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Shop
{
    public class ShopCustomerClient : IShopCustomerClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ShopCustomerClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<bool>> AddShopUserRelation(AddShopUserRelationRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<AddShopUserRelationRequest, ApiResult<bool>>(configuration["ShopManageServer:AddShopUserRelation"], request);
            return response;
        }
    }
}
