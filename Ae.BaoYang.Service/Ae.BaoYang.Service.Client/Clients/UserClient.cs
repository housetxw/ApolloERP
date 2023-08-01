using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Common.Exceptions;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class UserClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<UserClient> _logger;

        public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<UserClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取用户已关注商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetUserAttentionPid(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new List<string>();
            }
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserAttentionPidClientResponse> result =
                await client
                    .GetAsJsonAsync<UserAttentionPidClientRequest, ApiResult<UserAttentionPidClientResponse>>(
                        _configuration["UserServer:GetUserAttentionProduct"],
                        new UserAttentionPidClientRequest { UserId = userId });
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.PidList ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetUserAttentionProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
