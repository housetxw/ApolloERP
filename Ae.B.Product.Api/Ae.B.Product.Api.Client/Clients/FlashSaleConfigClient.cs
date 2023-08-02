using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{

    public class FlashSaleConfigClient : IFlashSaleConfigClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<FlashSaleConfigClient> _logger;

        public FlashSaleConfigClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<FlashSaleConfigClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResult<string>> CreatFlashSaleConfig(FlashSaleConfigDTO request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                 await client.PostAsJsonAsync<FlashSaleConfigDTO, ApiResult<string>>(
                    _configuration["ProductServer:CreatFlashSaleConfig"], request);
            return result;

        }

        public async Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs(GetFlashSaleConfigRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<FlashSaleConfigDTO> result =
                 await client.GetAsJsonAsync<GetFlashSaleConfigRequest, ApiPagedResult<FlashSaleConfigDTO>>(
                    _configuration["ProductServer:GetFlashSaleConfigs"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdateFlashSaleConfig(FlashSaleConfigDTO request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                 await client.PostAsJsonAsync<FlashSaleConfigDTO, ApiResult<string>>(
                    _configuration["ProductServer:UpdateFlashSaleConfig"], request);
            return result;
        }
    }
}
