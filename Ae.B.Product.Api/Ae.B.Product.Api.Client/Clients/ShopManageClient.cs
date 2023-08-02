using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class ShopManageClient : IShopManageClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ShopManageClient> _logger;

        public ShopManageClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopManageClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<ShopSimpleInfoClientDto>> GetShopListAsync(ShopListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopSimpleInfoClientDto> result =
                await client.PostAsJsonAsync<ShopListClientRequest, ApiPagedResult<ShopSimpleInfoClientDto>>(
                    _configuration["ShopManageServer:GetShopListAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result ?? new ApiPagedResult<ShopSimpleInfoClientDto>();
            }
            else
            {
               _logger.Info($"GetShopListAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeDto>> GetShopServiceTypeAsync(long shopId)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceTypeDto>> result =
                await client.GetAsJsonAsync<object, ApiResult<List<ShopServiceTypeDto>>>(
                    _configuration["ShopManageServer:GetShopServiceTypeAsync"], new { ShopId = shopId });
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopServiceTypeDto>();
            }
            else
            {
               _logger.Info($"GetShopServiceTypeAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取门店信息byIds
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoClientDto>> GetShopListByIdsAsync(List<long> shopIds)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopSimpleInfoClientDto>> result =
                await client.PostAsJsonAsync<object, ApiResult<List<ShopSimpleInfoClientDto>>>(
                    _configuration["ShopManageServer:GetShopListByIdsAsync"], new { shopIds = shopIds });
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopSimpleInfoClientDto>();
            }
            else
            {
               _logger.Info($"GetShopListByIdsAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopBaseInfoDto> GetShopSimpleInfoAsync(ShopBaseInfoRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<ShopBaseInfoDto> result =
                await client.GetAsJsonAsync<ShopBaseInfoRequest, ApiResult<ShopBaseInfoDto>>(
                    _configuration["ShopManageServer:GetShopSimpleInfoAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new ShopBaseInfoDto();
            }
            else
            {
                _logger.Info($"GetShopSimpleInfoAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

    }
}
