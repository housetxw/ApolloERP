using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.Vehicle.Service.Dal.Model;
using Ae.Vehicle.Service.Core.Model;
using Ae.Vehicle.Service.Core.Response;

namespace Ae.Vehicle.Service.Imp.Mappers
{
    public class VehicleMapperProfile : Profile
    {
        public VehicleMapperProfile()
        {
            CreateMap<VehicleBrandDO, GetVehicleBrandResponse>().ReverseMap();
            CreateMap<VehicleTypeDO, GetVehicleListResponse>();
            CreateMap<VehicleTypeTimingDO, VehicleInfo>();
            CreateMap<UserCarDO, UserVehicleVO>();
            CreateMap<VehicleTypeTimingDO, VehicleBaseInfo>();
            CreateMap<VehicleTypeTimingConfigDO, VehicleConfigVO>();
            CreateMap<VehicleTypeDO, VehicleTypeVo>();
            CreateMap<UserCarDO, UserCarFileVo>();
        }
    }
}
