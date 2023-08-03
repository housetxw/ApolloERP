using AutoMapper;
using RedGoose.Log;
using RedGoose.Web.WebApi;
using Rg.C.MiniApp.Api.Client.Interface;
using Rg.C.MiniApp.Api.Client.Request.UserAddress;
using Rg.C.MiniApp.Api.Core.Interfaces;
using Rg.C.MiniApp.Api.Core.Model;
using Rg.C.MiniApp.Api.Core.Request;
using Rg.C.MiniApp.Api.Core.Response.UserAddress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rg.C.MiniApp.Api.Imp.Services
{
  public  class UserAddressService: IUserAddressService
    {
        private readonly IMapper mapper;
        private readonly RedGooseLogger<UserAddressService> logger;
        private readonly IUserAddressClient userAddressClient;

        public UserAddressService(IMapper mapper, RedGooseLogger<UserAddressService> logger, IUserAddressClient userAddressClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.userAddressClient = userAddressClient;
        }

        public async  Task<ApiResult<string>> CreateUserAddress(CreateUserAddressRequest request)
        {
            var clientRequest = mapper.Map<CreateUserAddressClientRequest>(request);
            var result = await userAddressClient.CreateUserAddress(clientRequest);
            return result;
        }

        public  async  Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request)
        {
            var clientRequest = mapper.Map<CreateUserAddressTagClientRequest>(request);
            var result = await userAddressClient.CreateUserAddressTag(clientRequest);
            return result;
        }

        public async  Task<ApiResult<string>> DeleteUserAddress(DeleteUserAddressRequest request)
        {
            var clientRequest = mapper.Map<DeleteUserAddressClientRequest>(request);
            var result = await userAddressClient.DeleteUserAddress(clientRequest);
            return result;
        }

        public async  Task<ApiResult<UserAddressVO>> GetUserAddressDetail(GetUserAddressRequest request)
        {
            var clientRequest = mapper.Map<GetUserAddressClientRequest>(request);
            var result = await userAddressClient.GetUserAddressDetail(clientRequest);

            var vo = mapper.Map<ApiResult<UserAddressVO>>(result);
            return vo;
        }

        public async  Task<ApiResult<UserAddressResponse>> GetUserAddressList(GetUserAddressRequest request)
        {
            var clientRequest = mapper.Map<GetUserAddressClientRequest>(request);
            var result = await userAddressClient.GetUserAddressList(clientRequest);

            var vo = mapper.Map<ApiResult<UserAddressResponse>>(result);
            return vo;
        }

        public async  Task<ApiResult<UserAddressTagResponse>> GetUserAddressTagList(GetUserAddressTagRequest request)
        {
            var clientRequest = mapper.Map<GetUserAddressTagClientRequest>(request);
            var result = await userAddressClient.GetUserAddressTagList(clientRequest);

            var vo = mapper.Map<ApiResult<UserAddressTagResponse>>(result);
            return vo;
        }

        public async  Task<ApiResult<string>> UpdateUserAddress(UpdateUserAddressRequest request)
        {
            var clientRequest = mapper.Map<UpdateUserAddressClientRequest>(request);
            var result = await userAddressClient.UpdateUserAddress(clientRequest);
            return result;
        }
    }
}
