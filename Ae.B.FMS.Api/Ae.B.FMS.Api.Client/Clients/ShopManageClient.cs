using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Model;
using Ae.B.FMS.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.FMS.Api.Client.Clients
{
  public  class ShopManageClient: IShopManageClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ShopManageClient> _logger;

        public ShopManageClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopManageClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async  Task<ApiPagedResult<ShopSimpleInfoClientDTO>> GetShopListAsync(GetShopListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopSimpleInfoClientDTO> result =
                await client.PostAsJsonAsync<GetShopListClientRequest, ApiPagedResult<ShopSimpleInfoClientDTO>>(
                    _configuration["ShopManageServer:GetShopListAsync"], request);
            return result;
        }
    }
}
