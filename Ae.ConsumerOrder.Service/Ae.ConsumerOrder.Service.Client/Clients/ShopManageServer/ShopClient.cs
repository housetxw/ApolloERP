using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Core.Model.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class ShopClient : IShopClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ShopClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<ShopDTO>> GetShopById(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<GetShopRequest, ApiResult<ShopDTO>>(configuration["ShopManageServer:GetShopById"], request);
            return response;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopListRequest, ApiPagedResult<ShopSimpleInfoDTO>>(configuration["ShopManageServer:GetShopListAsync"], request);
            return response;
        }

        public async Task<ApiResult<ShopConfigDTO>> GetShopConfigByShopId(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<GetShopRequest, ApiResult<ShopConfigDTO>>(configuration["ShopManageServer:GetShopConfigByShopId"], request);
            return response;
        }

        public async Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopGrouponProductDTO>> result =
                await client.PostAsJsonAsync<GetShopGrouponProductRequest, ApiResult<List<ShopGrouponProductDTO>>>(
                    configuration["ShopManageServer:GetShopGrouponProduct"], request);
            return result;
        }
    }
}
