using AutoMapper;
using Ae.Receive.Service.Client.Model.Vehicle;
using Ae.Receive.Service.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Imp.Mappers
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<CarPartsSituationDto, CarPartsSituation>();
            CreateMap<CarPartsItemDto, CarPartsItem>();
        }
    }
}
