using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class VehicleProfile: Profile
    {
        public VehicleProfile()
        {
            CreateMap<AddUserCarRequest, AddUserCarClientRequest>().ReverseMap();
            CreateMap<VehiclePropertyRequest, VehiclePropertyClientRequest>().ReverseMap();
            CreateMap<UserVehicleDTO, UserVehicleVO>().ForMember(dest => dest.InsureExpireDate, opt => opt.MapFrom(src => src.InsureExpireDate == null ? string.Empty : src.InsureExpireDate.Value.ToString("yyyy-MM-dd")));
            CreateMap<VehiclePropertyDTO, VehiclePropertyRequest>().ReverseMap();
        }
    }
}
