using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Client.Model;
using Ae.FMS.Service.Client.Request;
using Ae.FMS.Service.Client.Response;

namespace Ae.FMS.Service.Client.Clients
{
    public class ProductClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ProductClient(IHttpClientFactory clientFactory, IConfiguration configuration)
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

        public async Task<ApiResult<List<ShopProductDto>>> GetShopProductByCodes(ShopProductDetialRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<ShopProductDetialRequest, ApiResult<List<ShopProductDto>>>(configuration["ProductServer:GetShopProductByCodes"], request);
            return response;
        }

    }
}
