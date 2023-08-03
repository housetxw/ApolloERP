using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Log;
using Ae.OrderComment.Service.Common.Exceptions;

namespace Ae.OrderComment.Service.Client.Clients
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
        public async Task<ApiResult<UserInfoClientResponse>> GetUserInfo(GetUserInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            ApiResult<UserInfoClientResponse> result = await client.GetAsJsonAsync<GetUserInfoClientRequest, ApiResult<UserInfoClientResponse>>(configuration["UserServer:GetUserInfo"], request);
            return result;
        }

        /// <summary>
        /// userId批量查询用户信息 限制100
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<UserBaseInfoDto>> GetUsersByUserIds(UsersByUserIdsClientRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            ApiResult<List<UserBaseInfoDto>> result =
                await client.PostAsJsonAsync<UsersByUserIdsClientRequest, ApiResult<List<UserBaseInfoDto>>>(
                    configuration["UserServer:GetUsersByUserIds"], request);
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
        ///  userId批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoDto>> GetUsersByUserIds(List<string> userIds)
        {
            List<UserBaseInfoDto> result = new List<UserBaseInfoDto>();
            if (userIds != null && userIds.Any())
            {
                var pageResult = PageHandler(userIds, 100);
                var allUser = await Task.WhenAll(pageResult.Select(_ => GetUsersByUserIds(
                    new UsersByUserIdsClientRequest
                    {
                        UserIds = _
                    })));

                foreach (var itemUser in allUser)
                {
                    result.AddRange(itemUser);
                }
            }

            return result;
        }
    }
}
