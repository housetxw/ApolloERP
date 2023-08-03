using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.C.MiniApp.Api.Client.Response.User;
using Ae.C.MiniApp.Api.Client.Request.User;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.User;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;

namespace Ae.C.MiniApp.Api.Client.Clients.User
{
    public class UserClient:IUserClient
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
        /// 获取用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoClientResponse> GetUserInfo(GetUserInfoClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserInfoClientResponse> result =
                await client.GetAsJsonAsync<GetUserInfoClientRequest, ApiResult<UserInfoClientResponse>>(
                    _configuration["UserServer:GetUserInfo"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserInfo_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserPointClientResponse> GetUserPoint(UserPointClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserPointClientResponse> result =
                await client.GetAsJsonAsync<UserPointClientRequest, ApiResult<UserPointClientResponse>>(
                    _configuration["UserServer:GetUserPoint"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserPoint_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MemberLevelClientResponse> GetMemberLevel(MemberLevelClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<MemberLevelClientResponse> result =
                await client.GetAsJsonAsync<MemberLevelClientRequest, ApiResult<MemberLevelClientResponse>>(
                    _configuration["UserServer:GetMemberLevel"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetMemberLevel_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserInfo(EditUserInfoClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditUserInfoClientRequest, ApiResult<bool>>(
                    _configuration["UserServer:EditUserInfo"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"EditUserInfo_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserBaseInfoDto> result =
                await client.GetAsJsonAsync<UserInfoByUserTelClientRequest, ApiResult<UserBaseInfoDto>>(
                    _configuration["UserServer:GetUserInfoByUserTel"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserInfoByUserTel_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPersonalProduct(AddPersonalProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddPersonalProductClientRequest, ApiResult<bool>>(
                    _configuration["UserServer:AddPersonalProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AddPersonalProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelPersonalProduct(CancelPersonalProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<CancelPersonalProductClientRequest, ApiResult<bool>>(
                    _configuration["UserServer:CancelPersonalProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"CancelPersonalProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 我的关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetUserAttentionProduct(UserAttentionProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserAttentionProductClientResponse> result =
                await client
                    .GetAsJsonAsync<UserAttentionProductClientRequest, ApiResult<UserAttentionProductClientResponse>>(
                        _configuration["UserServer:GetUserAttentionProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.PidList ?? new List<string>();
            }
            else
            {
                _logger.Info($"GetUserAttentionProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        public async Task<int> GetReferrerCustomerNum(BaseUserClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<int> result =
                await client.GetAsJsonAsync<BaseUserClientRequest, ApiResult<int>>(
                    _configuration["UserServer:GetReferrerCustomerNum"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetReferrerCustomerNum_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        public async Task<ApiPagedResult<UserInfoClientResponse>> GetReferrerCustomerList(GetSharingOrdersRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<GetSharingOrdersRequest, ApiPagedResult<UserInfoClientResponse>>(
                _configuration["UserServer:GetReferrerCustomerList"], request);
            return response;
        }

    }
}
