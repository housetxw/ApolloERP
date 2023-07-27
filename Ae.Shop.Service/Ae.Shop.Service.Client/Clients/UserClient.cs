using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model.User;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;

namespace Ae.Shop.Service.Client.Clients
{
    public class UserClient: IUserClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<UserClient> _logger;
        private readonly IConfiguration _configuration;

        public UserClient(IHttpClientFactory clientFactory, ApolloErpLogger<UserClient> logger,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoDto>> GetCommonUserInfo(CommonUserInfoRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<List<UserBaseInfoDto>> result =
                await client.PostAsJsonAsync<CommonUserInfoRequest, ApiResult<List<UserBaseInfoDto>>>(
                    _configuration["UserServer:GetCommonUserInfo"], request);
            _logger.Info(
                $"GetCommonUserInfo_Result Result={JsonConvert.SerializeObject(result)} Para={JsonConvert.SerializeObject(request)}");
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UserBaseInfoDto>();
            }
            else
            {
                _logger.Warn($"GetCommonUserInfo_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// GetUsersByUserIds
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoDto>> GetUsersByUserIds(UsersByUserIdsClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<List<UserBaseInfoDto>> result =
                await client.PostAsJsonAsync<UsersByUserIdsClientRequest, ApiResult<List<UserBaseInfoDto>>>(
                    _configuration["UserServer:GetUsersByUserIds"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UserBaseInfoDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUsersByUserIds_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
