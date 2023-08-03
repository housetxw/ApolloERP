using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.UserAddress;
using Ae.C.MiniApp.Api.Client.Request.UserAddress;
using Ae.C.MiniApp.Api.Client.Response.UserAddress;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response.UserAddress;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
   public class UserAddressProfile: Profile
    {
        public UserAddressProfile() {
            CreateMap<CreateUserAddressRequest, CreateUserAddressClientRequest>();

            CreateMap<DeleteUserAddressRequest, DeleteUserAddressClientRequest>();
            CreateMap<GetUserAddressRequest, GetUserAddressClientRequest>();
            CreateMap<UpdateUserAddressRequest, UpdateUserAddressClientRequest>();

            CreateMap<UserAddressDTO, UserAddressVO>();

            CreateMap<CreateUserAddressTagRequest, CreateUserAddressTagClientRequest>();
            CreateMap<UserAddressTagDTO, UserAddressTagVO>();

            CreateMap<GetUserAddressTagRequest, GetUserAddressTagClientRequest>();

            CreateMap<UserAddressTagClientResponse, UserAddressTagResponse>();

            CreateMap<UserAddressClientResponse, UserAddressResponse>();

            CreateMap<ApiResult<UserAddressTagClientResponse>, ApiResult<UserAddressTagResponse>>();

            CreateMap<ApiResult<UserAddressClientResponse>, ApiResult<UserAddressResponse>>();

            CreateMap<ApiResult<UserAddressDTO>, ApiResult<UserAddressVO>>();
        }
    }
}
