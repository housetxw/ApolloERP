using AutoMapper;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Core.Model.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleInfo, VehicleDetailVo>().ReverseMap();
        }
    }
}
