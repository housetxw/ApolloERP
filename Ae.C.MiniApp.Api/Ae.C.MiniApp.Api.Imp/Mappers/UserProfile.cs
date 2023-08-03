using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.C.MiniApp.Api.Client.Model.User;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Client.Response.User;
using Ae.C.MiniApp.Api.Core.Response.User;
using Ae.C.MiniApp.Api.Client.Request.UserAddress;
using Ae.C.MiniApp.Api.Core.Request.User;
using ApolloErp.Web.WebApi;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddressDto, UserAddressVO>().ReverseMap();
            CreateMap<UserPointClientResponse, UserPointResponse>().ReverseMap();
            CreateMap<UserPointRecordDto, UserPointRecordVO>().ReverseMap();
            CreateMap<MemberLevelClientResponse, MemberLevelResponse>().ReverseMap();
            CreateMap<MemberGradeDto, MemberGradeVO>().ReverseMap();
            CreateMap<AddUserAddressRequest, CreateUserAddressClientRequest>().ReverseMap();
            CreateMap<EditUserAddressRequest, UpdateUserAddressClientRequest>().ReverseMap();
            CreateMap<UserInfoClientResponse, UserInfoResponse>();
            CreateMap<ApiPagedResult<UserInfoClientResponse>, ApiPagedResult<UserInfoResponse>>().ReverseMap();
            CreateMap<ApiPagedResultData<UserInfoClientResponse>, ApiPagedResultData<UserInfoResponse>>().ReverseMap();
        }
    }
}
