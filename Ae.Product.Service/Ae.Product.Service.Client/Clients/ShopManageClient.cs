using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Client.Interface;
using Ae.Product.Service.Client.Model;
using Ae.Product.Service.Client.Request;
using Ae.Product.Service.Client.Response;
using Ae.Product.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Client.Clients
{
    public class ShopManageClient : IShopManageClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ShopManageClient> _logger;

        public ShopManageClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopManageClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 根据ShopId查询门店主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<GetShopByIdResponse> GetShopById(GetShopByIdRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<GetShopByIdResponse> result =
                await client.GetAsJsonAsync<GetShopByIdRequest, ApiResult<GetShopByIdResponse>>(
                    _configuration["ShopManageServer:GetShopById"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetShopById_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 根据ShopId查询门店主表信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<GetShopByIdResponse> GetShopById(int shopId)
        {
            if (shopId > 0)
            {
                return await GetShopById(new GetShopByIdRequest() {ShopId = shopId});
            }

            return null;
        }

        /// <summary>
        /// 查询门店服务上下架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceDto>> GetShopServiceListAsync(ShopServiceListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceDto>> result =
                await client.PostAsJsonAsync<ShopServiceListClientRequest, ApiResult<List<ShopServiceDto>>>(
                    _configuration["ShopManageServer:GetShopServiceListWithPID"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ShopServiceDto>();
            }
            else
            {
                _logger.Error($"GetShopServiceListWithPID_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopGrouponProductDTO>> result =
                await client.PostAsJsonAsync<GetShopGrouponProductRequest, ApiResult<List<ShopGrouponProductDTO>>>(
                    _configuration["ShopManageServer:GetShopGrouponProduct"], request);
            return result;
        }
    }
}
