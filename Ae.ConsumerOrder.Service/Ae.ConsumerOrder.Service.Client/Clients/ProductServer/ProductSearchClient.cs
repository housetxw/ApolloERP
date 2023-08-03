using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Product;
using Ae.ConsumerOrder.Service.Client.Request;


namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class ProductSearchClient : IProductSearchClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ProductSearchClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<List<ProductDetailDTO>>> GetProductsByProductCodes(ProductDetailSearchRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<ProductDetailSearchRequest, ApiResult<List<ProductDetailDTO>>>(configuration["ProductServer:GetProductsByProductCodes"], request);
            return response;
        }

        public async Task<ApiResult<List<ProductPackageDTO>>> GetPackageProductsByCodes(PackageProductSearchRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<PackageProductSearchRequest, ApiResult<List<ProductPackageDTO>>>(configuration["ProductServer:GetPackageProductsByCodes"], request);
            return response;
        }

        public async Task<ApiResult<List<GetShopProductByCodeDTO>>> GetShopProductByCodes(GetShopProductByCodeRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<GetShopProductByCodeRequest, ApiResult<List<GetShopProductByCodeDTO>>>(configuration["ProductServer:GetShopProductByCodes"], request);
            return response;
        }

        public async Task<ApiResult<FlashSaleConfigDTO>> GetFlashSaleProduct(FlashSaleConfigDTO request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.GetAsJsonAsync<FlashSaleConfigDTO, ApiResult< FlashSaleConfigDTO >> (configuration["ProductServer:GetFlashSaleProduct"], request);
            return response;
        }

        public async Task<ApiResult<string>> UpdateFlashSaleConfigNum(FlashSaleConfigDTO request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<FlashSaleConfigDTO, ApiResult<string>>(configuration["ProductServer:UpdateFlashSaleConfigNum"], request);
            return response;
        }

        public async Task<ApiResult<List<FlashSaleConfigDTO>>> GetActiveFlashSaleConfigs()
        {
            var client = clientFactory.CreateClient("ProductServer");
           
            var response = await client.GetAsJsonAsync<ApiResult<List<FlashSaleConfigDTO>>>(configuration["ProductServer:GetActiveFlashSaleConfigs"],null);

            return response;
        }

        public async Task<ApiResult<List<CheckDoorProductVo>>> CheckDoorProduct(CheckDoorProductRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<CheckDoorProductRequest, ApiResult<List<CheckDoorProductVo>>>(configuration["ProductServer:CheckDoorProduct"], request);
            return response;
        }
    }
}
