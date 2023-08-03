using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using Ae.C.MiniApp.Api.Client.Model.ShopProduct;
using Ae.C.MiniApp.Api.Client.Response.Product;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.ShopProduct;
using Ae.C.MiniApp.Api.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ShopProductProfile : Profile
    {
        public ShopProductProfile()
        {
            CreateMap<ShopProductVO, ShopProductDto>().ReverseMap();

            CreateMap<ProductDetailPageDataClientResponse, ProductDetailPageDataResponse>().ReverseMap();
            CreateMap<ProductDataDto, ProductDataVo>().ReverseMap();
            CreateMap<ShopInfoDto, ShopInfoVo>().ReverseMap();
            CreateMap<ProductPromotionDto, ProductPromotionVo>().ReverseMap();
            CreateMap<TagInfoDto, TagInfo>().ReverseMap();

            CreateMap<ProductListByServiceTypeClientResponse, ProductListByServiceTypeResponse>();
            CreateMap<CategoryProductListDto, CategoryProductListVo>();
        }
    }
}
