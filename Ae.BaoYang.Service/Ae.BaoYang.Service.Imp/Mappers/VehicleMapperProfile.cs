using AutoMapper;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Mappers
{
    public class VehicleMapperProfile : Profile
    {
        public VehicleMapperProfile()
        {
            CreateMap<VehicleConfigDTO, VehicleConfigModel>();
        }
    }
}
