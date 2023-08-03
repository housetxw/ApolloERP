using AutoMapper;
using Ae.Shop.Api.Client.Model.Reserve;
using Ae.Shop.Api.Client.Model.Vehicle;
using Ae.Shop.Api.Client.Request.Reserve;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using Ae.Shop.Api.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ReserveProfile: Profile
    {
        public ReserveProfile()
        {
            CreateMap<ShopReserveListDto, ShopReserveListVo>();
            CreateMap<ReserveDetailForWebDto, ReserveDetailForWebVo>();
            CreateMap<ReserveVehicleDto, ReserveVehicleVo>();
            CreateMap<ReserveProjectDto, ReserveProjectVo>();
            CreateMap<ReserveImageDto, ReserveImageVo>();
            CreateMap<ReserveTrackDto, ReserveTrackVo>();
            CreateMap<HistoryReceiveDto, HistoryReceiveVo>();
            CreateMap<ReserveDateDto, ReserveDateVo>();
            CreateMap<ReserveTimeRecordDto, ReserveTimeRecord>();
            CreateMap<UserVehicleDto, UserVehicleSimpleVo>().ForMember(dest => dest.CarPlate, opt => opt.MapFrom(src => src.CarNumber));

            CreateMap<EditReserveRequest, EditReserveClientRequest>();
            CreateMap<ReserveVehicleRequest, ReserveVehicleClientRequest>();
            CreateMap<AddReserveRequest, AddReserveClientRequest>();

            CreateMap<UserVehicleDto, UserVehicleVo>();
            CreateMap<VehiclePropertyDto, VehiclePropertyVo>();

            CreateMap<ReserveListDto, ReserveListVo>();
        }
    }
}
