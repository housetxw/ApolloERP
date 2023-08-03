using AutoMapper;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<GetEmployeeInfoRequest, GetEmployeeInfoClientRequest>();
            CreateMap<GetEmployeeClientResponse, EmployeeInfoVO>()
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar.AddImageDomain()));//图片绝对路径
        }
    }
}
