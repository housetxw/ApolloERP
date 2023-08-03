using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model.User;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Client.Response.User;

namespace Ae.ConsumerOrder.Service.Client.Clients
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

        public async Task<ApiResult<List<UserAddressDTO>>> GetUserAddress(UserAddressRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<UserAddressRequest, ApiResult<UserAddressResponse>>(configuration["UserServer:GetUserAddressList"], request);

            return new ApiResult<List<UserAddressDTO>>()
            {
                Code = response.Code,
                Data = response.Data.AddressVOs
            };
            //return response
        }

        public async Task<ApiResult<UserInfoResponse>> GetUserInfo(GetUserInfoRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.GetAsJsonAsync<GetUserInfoRequest, ApiResult<UserInfoResponse>>(configuration["UserServer:GetUserInfo"], request);
            return response;
        }

        public async Task<ApiResult<bool>> OperateUserPoint(OperateUserPointRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.PostAsJsonAsync<OperateUserPointRequest, ApiResult<bool>>(configuration["UserServer:OperateUserPoint"], request);
            return response;
        }

        public async Task<ApiResult<bool>> OperateUserGrowthValue(OperateUserGrowthValueRequest request)
        {
            var client = clientFactory.CreateClient("UserServer");
            var response = await client.PostAsJsonAsync<OperateUserGrowthValueRequest, ApiResult<bool>>(configuration["UserServer:OperateUserGrowthValue"], request);
            return response;
        }
    }
}
