using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.User;
using Ae.ShopOrder.Service.Client.Request.User;
using Ae.ShopOrder.Service.Client.Response.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.User.Impl
{
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public UserClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<UserInfoResponse>> GetUserInfo(GetUserInfoRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<GetUserInfoRequest, ApiResult<UserInfoResponse>>(configuration["UserServer:GetUserInfo"], request);
            return response;
        }

        public async Task<ApiResult<List<UserAddressDTO>>> GetUserAddress(UserAddressRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<UserAddressRequest, ApiResult<UserAddressResponse>>(configuration["UserServer:GetUserAddressList"], request);

            return new ApiResult<List<UserAddressDTO>>()
            {
                Code = response.Code,
                Data = response.Data.AddressVOs
            };
        }

        public async Task<ApiResult<GetUserInfoByUserTelResponse>> GetUserInfoByUserTel(GetUserInfoByUserTelRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<GetUserInfoByUserTelRequest, ApiResult<GetUserInfoByUserTelResponse>>(configuration["UserServer:GetUserInfoByUserTel"], request);
            return response;
        }

        public async Task<ApiResult<string>> CreateUser(CreateUserRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.PostAsJsonAsync<CreateUserRequest, ApiResult<string>>(configuration["UserServer:CreateUser"], request);
            return response;
        }

        public async Task<ApiResult<bool>> OperateUserPoint(OperateUserPointRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.PostAsJsonAsync<OperateUserPointRequest, ApiResult<bool>>(configuration["UserServer:OperateUserPoint"], request);
            return response;
        }
    }
}
