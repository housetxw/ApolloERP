using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.UserAddress;
using Ae.C.MiniApp.Api.Client.Request.User;
using Ae.C.MiniApp.Api.Client.Request.UserAddress;
using Ae.C.MiniApp.Api.Client.Response.UserAddress;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Response.UserAddress;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.UserAddress
{
    public class UserAddressClient: IUserAddressClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<UserAddressClient> _logger;
        private readonly IConfiguration _configuration;


        public UserAddressClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<UserAddressClient> logger)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
            _logger = logger;
        }

        public async  Task<ApiResult<int>> CreateUserAddress(CreateUserAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<int> result = await client.PostAsJsonAsync<CreateUserAddressClientRequest, ApiResult<int>>(
                _configuration["UserServer:CreateUserAddress"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"CreateUserAddress_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<string> result = await client.PostAsJsonAsync<CreateUserAddressTagClientRequest, ApiResult<string>>(
                _configuration["UserServer:CreateUserAddressTag"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"CreateUserAddressTag_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiResult<bool>> DeleteUserAddress(DeleteUserAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<DeleteUserAddressClientRequest, ApiResult<bool>>(
                _configuration["UserServer:DeleteUserAddress"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"DeleteUserAddress_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiResult<UserAddressDTO>> GetUserAddressDetail(GetUserAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<UserAddressDTO> result = await client.GetAsJsonAsync<GetUserAddressClientRequest, ApiResult<UserAddressDTO>>(
                _configuration["UserServer:GetUserAddressDetail"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"GetUserAddressDetail_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiResult<UserAddressClientResponse>> GetUserAddressList(GetUserAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<UserAddressClientResponse> result = await client.GetAsJsonAsync<GetUserAddressClientRequest, ApiResult<UserAddressClientResponse>>(
                _configuration["UserServer:GetUserAddressList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"GetUserAddressList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiResult<UserAddressTagClientResponse>> GetUserAddressTagList(GetUserAddressTagClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<UserAddressTagClientResponse> result = await client.GetAsJsonAsync<GetUserAddressTagClientRequest, ApiResult<UserAddressTagClientResponse>>(
                _configuration["UserServer:GetUserAddressTagList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"GetUserAddressTagList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiResult<bool>> UpdateUserAddress(UpdateUserAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<UpdateUserAddressClientRequest, ApiResult<bool>>(
                _configuration["UserServer:UpdateUserAddress"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Info($"UpdateUserAddress_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<bool> SetDefaultAddress(SetDefaultAddressClientRequest request)
        {
            var client = _clientFactory.CreateClient("UserServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<SetDefaultAddressClientRequest, ApiResult<bool>>(
                _configuration["UserServer:SetDefaultAddress"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"SetDefaultAddress_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
