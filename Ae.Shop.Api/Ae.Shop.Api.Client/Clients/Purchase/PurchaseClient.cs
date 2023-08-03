using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients
{
    public class PurchaseClient : IPurchaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<PurchaseClient> _logger;
        public PurchaseClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<PurchaseClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierPurchaseProducts(GetSupplierPurchaseRequest request)
        {
            var client = _httpClientFactory.CreateClient("PurchaseServer");
            ApiResult<List<SupplierProductStockDTO>> result =
                 await client.GetAsJsonAsync<GetSupplierPurchaseRequest, ApiResult<List<SupplierProductStockDTO>>>(
                    _configuration["PurchaseServer:GetSupplierPurchaseProducts"], request);
            return result;
        }

        public async Task<List<VenderDTO>> GetVenders(GetSupplierPurchaseRequest request)
        {
            var client = _httpClientFactory.CreateClient("PurchaseServer");
            ApiResult<List<VenderDTO>> result =
                 await client.GetAsJsonAsync<GetSupplierPurchaseRequest, ApiResult<List<VenderDTO>>>(
                    _configuration["PurchaseServer:GetVendersCommon"], request);

            return result.Data;

        }

        public async Task<ApiPagedResult<VenderProductForAppVo>> SearchVenderProductListForApp(SearchVenderProductListForAppRequest request)
        {
            var client = _httpClientFactory.CreateClient("PurchaseServer");
            var result =
                 await client.GetAsJsonAsync<SearchVenderProductListForAppRequest, ApiPagedResult<VenderProductForAppVo>>(
                    _configuration["PurchaseServer:SearchVenderProductListForApp"], request);

            return result;
        }
    }
}
