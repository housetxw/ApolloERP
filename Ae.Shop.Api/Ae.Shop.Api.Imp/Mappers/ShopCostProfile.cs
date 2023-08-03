using AutoMapper;
using Ae.Shop.Api.Core.Model.ShopCost;
using Ae.Shop.Api.Core.Request.ShopCost;
using Ae.Shop.Api.Core.Response.ShopCost;
using Ae.Shop.Api.Dal.Model.ShopCost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopCostProfile: Profile
    {
        public ShopCostProfile()
        {
            CreateMap<ShopCostDO, GetShopCostResponse>();
            CreateMap<ShopCostTypeDO, ShopCostTypeVO>();
            CreateMap<AddShopCostRequest, ShopCostDO>();
            CreateMap<AddShopCostRequest, AddShopCostDo>();
            CreateMap<ShopCostDetailDO, GetShopCostResponse>();
        }
    }
}
