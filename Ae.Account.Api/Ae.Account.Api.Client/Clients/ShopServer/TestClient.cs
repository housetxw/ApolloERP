using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ae.Account.Api.Client.Clients.ShopServer
{
    public class TestClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public TestClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<bool> UpdateShopInfo()
        {
            var client = clientFactory.CreateClient("ShopServer");
            GetShopInfoRequest request = new GetShopInfoRequest();
            request.ShopId = 1;
            request.ShopName = "供应商";
            ApiResult<ShopInfo> shop = await client.GetAsJsonAsync<GetShopInfoRequest, ApiResult<ShopInfo>>(configuration["ShopServer:GetShopInfo"], request);
            return true;
            //await client.PostAsJsonAsync<GetShopInfoRequest, ShopInfo>("Demo/GetShopInfo", request);

        }
    }
}
