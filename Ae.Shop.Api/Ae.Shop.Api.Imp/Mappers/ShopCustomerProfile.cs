using AutoMapper;
using Ae.Shop.Api.Client.Model.ShopManage;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopCustomerProfile: Profile
    {
        public ShopCustomerProfile()
        {
            CreateMap<ShopCustomerListDto, ShopCustomerListVo>();
            CreateMap<ReceiveVehicleDto, ReceiveVehicleVo>();
        }
    }
}
