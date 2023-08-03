using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Product;
using Ae.ShopOrder.Service.Client.Response.Product;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.Product;
using Ae.ShopOrder.Service.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Product
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ProductClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }
        /// <summary>
        /// 得到产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetShopProductByCodeResponse>>> GetShopProductByCodes(GetShopProductByCodeRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<GetShopProductByCodeRequest,ApiResult<List<GetShopProductByCodeResponse>>>(configuration["ProductServer:GetShopProductByCodes"], request);
            return response;
        }

        public async Task<ApiResult> InsertPromotionActivityOrder(InsertPromotionActivityOrderRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            ApiResult result = 
                await client.PostAsJsonAsync<InsertPromotionActivityOrderRequest, ApiResult>(
                configuration["ProductServer:InsertPromotionActivityOrder"], request);
            return result;
        }

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetPromotionActivitByProductCodeListResponse>>> GetPromotionActivitByProductCodeList(GetPromotionActivitByProductCodeListRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            ApiResult<List<GetPromotionActivitByProductCodeListResponse>> result = await client.PostAsJsonAsync<GetPromotionActivitByProductCodeListRequest, ApiResult<List<GetPromotionActivitByProductCodeListResponse>>>(
                configuration["ProductServer:GetPromotionActivitByProductCodeList"], request);
            return result;
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
    }
}
