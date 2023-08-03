using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Client.Model;
using Ae.Order.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Clients
{
   public class ShopManageClient: IShopManageClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ShopManageClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<ShopSimpleInfoClientDTO>> GetShopSimpleInfoAsync(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<ShopSimpleInfoClientDTO> result =
                await client.GetAsJsonAsync<GetShopRequest, ApiResult<ShopSimpleInfoClientDTO>>(
                    configuration["ShopManageServer:GetShopSimpleInfoAsync"], request);
            return result;
        }
    }
}
