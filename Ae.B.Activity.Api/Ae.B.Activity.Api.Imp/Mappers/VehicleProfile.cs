using AutoMapper;
using Ae.B.Activity.Api.Client.Request.Vehicle;
using Ae.B.Activity.Api.Client.Response.Vehicle;
using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Imp.Mappers
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<GetVehicleListByBrandRequest, GetVehicleListByBrandClientRequest>();

            CreateMap<GetPaiLiangByVehicleIdRequest, GetPaiLiangByVehicleIdClientRequest>();

            CreateMap<GetVehicleNianByPaiLiangRequest, GetVehicleNianByPaiLiangClientRequest>();

            CreateMap<GetVehicleSalesNameByNianRequest, GetVehicleSalesNameByNianClientRequest>();

            CreateMap<GetVehicleBrandClientResponse,  GetVehicleBrandResponse> ();

            CreateMap<GetVehicleListClientResponse,  GetVehicleListResponse> ();

            CreateMap< GetVehicleSalesNameByNianClientResponse, GetVehicleSalesNameByNianResponse>();
        }

    }
}
