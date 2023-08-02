using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using Ae.B.User.Api.Client.Model;
using Ae.B.User.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Common.Exceptions;
using Ae.B.User.Api.Client.Response;
using Ae.B.User.Api.Client.Request.Vehicle;
using Ae.B.User.Api.Client.Model.Vehicle;
using Ae.B.User.Api.Client.Request.Address;

namespace Ae.B.User.Api.Client.Clients
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
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserBaseInfoDto>> GetUserList(GetUserListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiPagedResult<UserBaseInfoDto> result =
                await client
                    .GetAsJsonAsync<GetUserListClientRequest, ApiPagedResult<UserBaseInfoDto>>(
                        _configuration["UserServer:GetUserList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetUserList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoClientResponse> GetUserInfo(UserInfoClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserInfoClientResponse> result =
                await client
                    .GetAsJsonAsync<UserInfoClientRequest, ApiResult<UserInfoClientResponse>>(
                        _configuration["UserServer:GetUserInfo"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetUserInfo_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 手机号查用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserBaseInfoDto> result =
                await client.GetAsJsonAsync<UserInfoByUserTelClientRequest, ApiResult<UserBaseInfoDto>>(
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
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserAddressDTO>> GetUserAddressList(UserAddressListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<UserAddressClientResponse> result =
                await client
                    .GetAsJsonAsync<UserAddressListClientRequest, ApiResult<UserAddressClientResponse>>(
                        _configuration["UserServer:GetUserAddressList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data?.AddressVOs ?? new List<UserAddressDTO>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUserAddressList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateUser(CreateUserClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<CreateUserClientRequest, ApiResult<string>>(
                    _configuration["UserServer:CreateUser"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"CreateUser_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<bool> EditUserInfo(EditUserInfoClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditUserInfoClientRequest, ApiResult<bool>>(
                    _configuration["UserServer:EditUserInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EditUserInfo_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 搜索用户
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

        public async Task<string> AddUserCarAsync(AddUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<AddUserCarClientRequest, ApiResult<string>>(
                    _configuration["VehicleServer:AddUserCarAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"AddUserCarAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<bool> DeleteUserCarAsync(DeleteUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteUserCarClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:DeleteUserCarAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DeleteUserCarAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<bool> EditUserCarAsync(EditUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditUserCarClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:EditUserCarAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EditUserCarAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<UserVehicleDto> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDto> result =
                await client.GetAsJsonAsync<UserVehicleByCarIdClientRequest, ApiResult<UserVehicleDto>>(
                    _configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUserVehicleByCarIdAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult<int>> CreateUserAddress(CreateUserAddressClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<int> result = await client.PostAsJsonAsync<CreateUserAddressClientRequest, ApiResult<int>>(
                _configuration["UserServer:CreateUserAddress"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"CreateUserAddress_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult<bool>> UpdateUserAddress(UpdateUserAddressClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<UpdateUserAddressClientRequest, ApiResult<bool>>(
                _configuration["UserServer:UpdateUserAddress"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"UpdateUserAddress_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult<bool>> DeleteUserAddress(DeleteUserAddressClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("UserServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<DeleteUserAddressClientRequest, ApiResult<bool>>(
                _configuration["UserServer:DeleteUserAddress"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DeleteUserAddress_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
