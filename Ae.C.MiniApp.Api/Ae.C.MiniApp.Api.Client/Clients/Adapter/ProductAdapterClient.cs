using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.BasicData;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Adapter
{
    public class ProductAdapterClient: IProductAdapterClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<BasicDataClient> _logger;
        private IConfiguration configuration { get; }


        public ProductAdapterClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<BasicDataClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AdaptiveProductsClientResponse> VerifyAdaptiveBaoYangProducts(VerifyAdaptiveProductsClientRequest request)
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            ApiResult<AdaptiveProductsClientResponse> result = 
                await client.PostAsJsonAsync<VerifyAdaptiveProductsClientRequest, ApiResult<AdaptiveProductsClientResponse>>(
                    configuration["BaoYangServer:BaoYangAdaptiveProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"VerifyAdaptiveProducts_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 轮胎商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AdaptiveProductsClientResponse> VerifyAdaptiveTireProducts(VerifyTireProductClientRequest request)
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            ApiResult<AdaptiveProductsClientResponse> result =
                await client.PostAsJsonAsync<VerifyTireProductClientRequest, ApiResult<AdaptiveProductsClientResponse>>(
                    configuration["BaoYangServer:TireAdaptiveProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"VerifyAdaptiveProducts_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


    }
}
