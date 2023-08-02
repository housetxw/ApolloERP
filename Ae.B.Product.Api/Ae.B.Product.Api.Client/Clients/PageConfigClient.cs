using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class PageConfigClient : IPageConfigClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<PageConfigClient> _logger;

        public PageConfigClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<PageConfigClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                 await client.PostAsJsonAsync<ConfigAdvertisementVo, ApiResult<string>>(
                    _configuration["ProductServer:AddConfigAdvertisement"], request);
            return result;
        }

        public async Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                 await client.PostAsJsonAsync<ConfigAdvertisementVo, ApiResult<string>>(
                    _configuration["ProductServer:DeleteConfigAdvertisement"], request);
            return result;
        }

        public async Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ConfigAdvertisementVo> result =
                 await client.GetAsJsonAsync<ConfigAdvertisementVo, ApiResult<ConfigAdvertisementVo>>(
                    _configuration["ProductServer:GetConfigAdvertisement"], request);
            return result;
        }

        public async Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result =
                 await client.GetAsJsonAsync<GetConfigAdvertisementsRequest, ApiPagedResult<ConfigAdvertisementVo>>(
                    _configuration["ProductServer:GetConfigAdvertisements"], request);
            return result;
        }

        public async Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(ApiRequest<GetShopCardDetailRequest> request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<GetShopPackageCardProductPageListVo> result =
                 await client.PostAsJsonAsync<ApiRequest<GetShopCardDetailRequest>, ApiResult<GetShopPackageCardProductPageListVo>>(
                    _configuration["ProductServer:GetShopCardDetail"], request);
            return result;
        }

        public async Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(ApiRequest<GetShopPackageCardProductPageListRequest> request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<GetShopPackageCardProductPageListVo> result =
                 await client.PostAsJsonAsync<ApiRequest<GetShopPackageCardProductPageListRequest>, ApiPagedResult<GetShopPackageCardProductPageListVo>>(
                    _configuration["ProductServer:GetShopPackageCardProductPageList"], request);
            return result;
        }

        public async Task<ApiResult> SaveShopPackageCard(ApiRequest<ConfigShopPackageCardDTO> request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult result =
                 await client.PostAsJsonAsync<ApiRequest<ConfigShopPackageCardDTO>, ApiResult>(
                    _configuration["ProductServer:SaveShopPackageCard"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                 await client.PostAsJsonAsync<ConfigAdvertisementVo, ApiResult<string>>(
                    _configuration["ProductServer:UpdateConfigAdvertisement"], request);
            return result;
        }
    }
}
