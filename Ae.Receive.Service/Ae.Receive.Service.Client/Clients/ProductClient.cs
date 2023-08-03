using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model.Product;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Common.Exceptions;


namespace Ae.Receive.Service.Client.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ProductClient> _logger;

        public ProductClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ProductClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        /// <summary>
        /// 根据订单号查询商品参加的活动
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductActivityDTO>> GetProductActivityByOrderNos(ProductActivityByOrderNosClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductActivityDTO>> result =
                await client.PostAsJsonAsync<ProductActivityByOrderNosClientRequest,ApiResult<List<ProductActivityDTO>>>(
                    _configuration["ProductServer:GetProductActivityByOrderNos"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ProductActivityDTO>();
            }
            else
            {
                _logger.Info($"GetProductActivityByOrderNos_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
