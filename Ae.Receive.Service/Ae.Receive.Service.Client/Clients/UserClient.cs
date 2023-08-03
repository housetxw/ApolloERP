using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.User;
using Ae.Receive.Service.Client.Response;
using Ae.Receive.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Clients
{
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<UserClient> _logger;
        public UserClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<UserClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoClientResponse> GetUserInfo(GetUserInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            ApiResult<UserInfoClientResponse> result = await client.GetAsJsonAsync<GetUserInfoClientRequest, ApiResult<UserInfoClientResponse>>(configuration["UserServer:GetUserInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetUserInfo_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateUser(CreateUserClientRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<CreateUserClientRequest, ApiResult<string>>(
                    configuration["UserServer:CreateUser"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"CreateUser_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
    }
}
