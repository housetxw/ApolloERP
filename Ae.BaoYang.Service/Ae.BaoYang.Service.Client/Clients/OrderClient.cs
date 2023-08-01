using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using Ae.BaoYang.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Common.Exceptions;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class OrderClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<OrderClient> _logger;

        public OrderClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 根据用户ID获取购买过的非服务商品ID集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<string>> GetPidsByUserId(PidsByUserIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            ApiResult<List<string>> result =
                await client
                    .GetAsJsonAsync<PidsByUserIdClientRequest, ApiResult<List<string>>>(
                        _configuration["OrderServer:GetPidsByUserId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetPidsByUserId_Error {result.Message}");
                return new List<string>();
            }
        }

        /// <summary>
        /// 根据用户ID获取购买过的非服务商品ID集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPidsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new List<string>();
            }

            var result = await GetPidsByUserId(new PidsByUserIdClientRequest()
            {
                UserId = userId
            });
            return result ?? new List<string>();
        }
    }
}
