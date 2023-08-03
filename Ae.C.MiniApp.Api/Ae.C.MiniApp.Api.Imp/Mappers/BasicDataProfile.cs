using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class BasicDataProfile : Profile
    {
        public BasicDataProfile()
        {
            CreateMap<GetRegionChinaClientResponse, GetRegionChinaResponse>().ReverseMap();
            CreateMap<RegionChinaClientDTO, RegionChinaVO>().ReverseMap();
            CreateMap<LocationRequest, GetPositionClientRequest>().ReverseMap();
            CreateMap<GetPositionClientResponse, LocationResponse>().ReverseMap();
            CreateMap<ThreeLevelRegionChinaClientResponse, ThreeLevelRegionChinaResponse>().ReverseMap();
            CreateMap<RegionChinaSiteDTO, RegionChinaSiteVO>().ReverseMap();
            CreateMap<RegionChinaSiteCityDTO, RegionChinaSiteCityVO>().ReverseMap();
            CreateMap<RegionChinaSiteDistrictDTO, RegionChinaSiteDistrictVO>().ReverseMap();
        }
    }
}
