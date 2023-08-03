using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using Ae.C.MiniApp.Api.Client.Response.BaoYang;
using Ae.C.MiniApp.Api.Common.Exceptions;

namespace Ae.C.MiniApp.Api.Client.Clients.BaoYang
{
    public class BaoYangClient: IBaoYangClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<BaoYangClient> _logger;

        public BaoYangClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangCategoryDto>>> GetBaoYangPackagesAsync(
            BaoYangPackagesClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangCategoryDto>> result =
                await client.PostAsJsonAsync<BaoYangPackagesClientRequest, ApiResult<List<BaoYangCategoryDto>>>(
                    _configuration["BaoYangServer:GetBaoYangPackagesAsync"], request);
            if (result.Code != ResultCode.Success)
            {
                _logger.Info($"GetBaoYangPackagesAsync_Error {result.Message}");
            }

            return result;
        }

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageProductDto>> SearchPackageProductsWithConditionAsync(
            SearchProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<BaoYangPackageProductDto> result =
                await client.PostAsJsonAsync<SearchProductClientRequest, ApiPagedResult<BaoYangPackageProductDto>>(
                    _configuration["BaoYangServer:SearchPackageProductsWithConditionAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.Items?.ToList() ?? new List<BaoYangPackageProductDto>();
            }
            else
            {
                _logger.Info($"SearchPackageProductsWithConditionAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TireServiceListClientResponse> GetTireCategoryListAsync(TireCategoryListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<TireServiceListClientResponse> result =
                await client.PostAsJsonAsync<TireCategoryListClientRequest, ApiResult<TireServiceListClientResponse>>(
                    _configuration["BaoYangServer:GetTireCategoryListAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetTireCategoryListAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<TireProductDto>> GetTireListAsync(TireListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<TireProductDto> result =
                await client.PostAsJsonAsync<TireListClientRequest, ApiPagedResult<TireProductDto>>(
                    _configuration["BaoYangServer:GetTireListAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.Items?.ToList() ?? new List<TireProductDto>();
            }
            else
            {
                _logger.Info($"GetTireListAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> BaoYangAdaptiveProduct(BaoYangAdaptiveProductRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<BaoYangAdaptiveProductResponse> result =
                await client.PostAsJsonAsync<BaoYangAdaptiveProductRequest, ApiResult<BaoYangAdaptiveProductResponse>>(
                    _configuration["BaoYangServer:BaoYangAdaptiveProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.AdaptivePid ?? new List<string>();
            }
            else
            {
                _logger.Info($"BaoYangAdaptiveProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyAdaptiveProductForBuyClientResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<VerifyAdaptiveProductForBuyClientResponse> result =
                await client
                    .PostAsJsonAsync<VerifyAdaptiveProductForBuyClientRequest,
                        ApiResult<VerifyAdaptiveProductForBuyClientResponse>>(
                        _configuration["BaoYangServer:VerifyAdaptiveProductForBuy"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"VerifyAdaptiveProductForBuy_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 轮胎商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> TireAdaptiveProduct(TireAdaptiveProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<AdaptiveProductsResponse> result =
                await client.PostAsJsonAsync<TireAdaptiveProductClientRequest, ApiResult<AdaptiveProductsResponse>>(
                    _configuration["BaoYangServer:TireAdaptiveProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.AdaptivePid ?? new List<string>();
            }
            else
            {
                _logger.Info($"TireAdaptiveProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
