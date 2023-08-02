using AutoMapper;
using Ae.Product.Service.Client.Model.BaoYang;
using Ae.Product.Service.Client.Request.BaoYang;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Imp.Mappers
{
    public class ShopProductProfile:Profile
    {
        public ShopProductProfile()
        {

            CreateMap<BaseShopProductVo, FctShopProductDO>();
            CreateMap<ShopProductVo, FctShopProductDO>().ReverseMap();
            CreateMap<VehicleRequest, VehicleClientRequest>();
            CreateMap<PropertySelectModelDto, PropertySelectModel>();
            CreateMap<PropertyResultDto, PropertyResult>();
        }
    }
}
