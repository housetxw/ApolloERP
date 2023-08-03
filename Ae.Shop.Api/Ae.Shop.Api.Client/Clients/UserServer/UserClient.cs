using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.User;
using Ae.Shop.Api.Client.Request.User;
using Ae.Shop.Api.Client.Response.User;
using Ae.Shop.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.UserServer
{
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<UserClient> logger;

        public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<UserClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }


        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoClientResponse> GetUserInfo(UserInfoClientRequest request)
        {
            var client = httpClientFactory.CreateClient("UserServer");
            ApiResult<UserInfoClientResponse> result =
                await client
                    .GetAsJsonAsync<UserInfoClientRequest, ApiResult<UserInfoClientResponse>>(
                        configuration["UserServer:GetUserInfo"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetUserInfo_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelRequest request)
        {
            var client = httpClientFactory.CreateClient("UserServer");
            ApiResult<UserBaseInfoDto> result =
                await client.GetAsJsonAsync<UserInfoByUserTelRequest, ApiResult<UserBaseInfoDto>>(
                    configuration["UserServer:GetUserInfoByUserTel"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetUserInfoByUserTel_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }
    }
}
