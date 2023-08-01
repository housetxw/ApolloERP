using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class ShopManageClient
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
        /// 查询门店服务上下架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceDTO>> GetShopServiceListAsync(ShopServiceListRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceDTO>> result =
                await client.PostAsJsonAsync<ShopServiceListRequest, ApiResult<List<ShopServiceDTO>>>(
                    _configuration["ShopManageServer:GetShopServiceListWithPID"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ShopServiceDTO>();
            }
            else
            {
                _logger.Error($"GetShopServiceListWithPID_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<ShopServiceDTO>> GetAllOnLineServicesByShopId(
            GetAllOnLineServicesByShopIdRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceDTO>> result =
                await client.GetAsJsonAsync<GetAllOnLineServicesByShopIdRequest, ApiResult<List<ShopServiceDTO>>>(
                    _configuration["ShopManageServer:GetAllOnLineServicesByShopId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ShopServiceDTO>();
            }
            else
            {
                _logger.Error($"GetAllOnLineServicesByShopId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
