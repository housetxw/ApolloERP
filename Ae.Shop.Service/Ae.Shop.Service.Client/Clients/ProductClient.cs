using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<BasicDataClient> _logger;
        private IConfiguration configuration { get; }


        public ProductClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<BasicDataClient> logger
        )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductDetailDTO>> GetProductsByProductCodes(ProductDetailSearchClientRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductDetailDTO>> result = await client.GetAsJsonAsync<ProductDetailSearchClientRequest, ApiResult<List<ProductDetailDTO>>>(configuration["ProductServer:GetProductsByProductCodes"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetProductsByProductCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
