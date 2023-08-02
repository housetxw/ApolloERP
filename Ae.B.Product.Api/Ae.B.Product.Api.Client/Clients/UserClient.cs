using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.User;
using Ae.B.Product.Api.Client.Request.User;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class UserClient : IUserClient
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
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserBaseInfoDto> GetUserInfoByUserTel(
            UserInfoByUserTelClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserBaseInfoDto> result =
                await client
                    .GetAsJsonAsync<UserInfoByUserTelClientRequest, ApiResult<UserBaseInfoDto>>(
                        _configuration["UserServer:GetUserInfoByUserTel"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUserInfoByUserTel_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 用户姓名/手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserInfoDto>> SearchUserInfo(SearchUserInfoClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiPagedResult<UserInfoDto> result =
                await client.GetAsJsonAsync<SearchUserInfoClientRequest, ApiPagedResult<UserInfoDto>>(
                    _configuration["UserServer:SearchUserInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"SearchUserInfo_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<UserBaseInfoDto>> GetUsersByUserIds(UsersByUserIdsClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
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

        private List<List<string>> PageHandler(List<string> pidList, int pageSize)
        {
            List<List<string>> result = new List<List<string>>();
            int totalSize = pidList.Count / pageSize;
            int t = pidList.Count % pageSize;
            if (t > 0)
            {
                totalSize = totalSize + 1;
            }

            for (int i = 0; i < totalSize; i++)
            {
                var item = pidList.Skip(i * pageSize).Take(pageSize).ToList();
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoDto>> GetUsersByUserIds(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return new List<UserBaseInfoDto>();
            }

            List<UserBaseInfoDto> result = new List<UserBaseInfoDto>();
            var pageResult = PageHandler(userIds, 100);
            var allTask =
                await Task.WhenAll(pageResult.Select(_ =>
                    GetUsersByUserIds(new UsersByUserIdsClientRequest {UserIds = _})));
            foreach (var itemTask in allTask)
            {
                result.AddRange(itemTask);
            }

            return result;
        }
    }
}
