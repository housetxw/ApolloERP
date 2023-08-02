using AutoMapper;
using Ae.B.User.Api.Client.Model;
using Ae.B.User.Api.Client.Request;
using Ae.B.User.Api.Client.Response;
using Ae.B.User.Api.Core.Model.User;
using Ae.B.User.Api.Core.Request.User;
using Ae.B.User.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.User.Api.Core.Response.User;
using Ae.B.User.Api.Client.Model.Vehicle;
using Ae.B.User.Api.Core.Request.Vehicle;
using Ae.B.User.Api.Client.Request.Vehicle;
using Ae.B.User.Api.Core.Request.Address;
using Ae.B.User.Api.Client.Request.Address;

namespace Ae.B.User.Api.Imp.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserListRequest, GetUserListClientRequest>();
            CreateMap<UserBaseInfoDto, UserBaseInfoVo>();
            CreateMap<UserInfoClientResponse, UserInfoResponse>();
            CreateMap<UserVehicleDto, UserVehicleVo>();
            CreateMap<VehiclePropertyDto, VehiclePropertyVo>();
            CreateMap<UserAddressDTO, UserAddressVo>();
            CreateMap<UserInfoDto, UserInfoVo>();
            CreateMap<AddUserCarRequest, AddUserCarClientRequest>();
            CreateMap<VehiclePropertyRequest, VehiclePropertyClientRequest>();
            CreateMap<AddUserAddressRequest, CreateUserAddressClientRequest>();
            CreateMap<EditUserAddressRequest, UpdateUserAddressClientRequest>();
            CreateMap<UserBaseInfoDto, UserInfoResponse>();
        }
    }
}
