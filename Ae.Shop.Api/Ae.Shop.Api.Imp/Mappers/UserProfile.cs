using AutoMapper;
using Ae.Shop.Api.Client.Model.Vehicle;
using Ae.Shop.Api.Client.Response.User;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfoClientResponse, ShopCustomerDetailVo>();
            CreateMap<UserVehicleDto, UserVehicleVo>();
            CreateMap<VehiclePropertyDto, VehiclePropertyVo>();
        }
    }
}
