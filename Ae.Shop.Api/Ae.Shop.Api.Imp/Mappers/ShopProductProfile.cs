using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.Shop.Api.Client.Model.Product;
using Ae.Shop.Api.Client.Response.Product;
using Ae.Shop.Api.Core.Model.ShopProduct;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopProductProfile : Profile
    {
        public ShopProductProfile()
        {
            CreateMap<ProductCategoryDto, ProductCategoryVo>().ReverseMap();

            CreateMap<CategoryInfoDto, CategoryInfoVo>().ReverseMap();

            CreateMap<ProductAllInfoDto, ProductAllInfoVo>().ReverseMap();

            CreateMap<ProductAttributeValueVo, ProductAttributevalueDto>().ReverseMap();

            CreateMap<ProductPackageDetailVo, ProductPackageDetailDto>().ReverseMap();

            CreateMap<ProductDetailClientVo, ProductDetailVo>().ReverseMap();
            CreateMap<ProductAllInfoClientVo, ProductAllInfoVo>().ReverseMap();
            CreateMap<ProductInstallserviceClientVo, ProductInstallServiceVo>().ReverseMap();
            CreateMap<ProductAttributevalueClientVo, ProductAttributeValueVo>().ReverseMap();

            CreateMap<CategoryAttributeDto, ProductAttributeValueVo>().ReverseMap();
        }
    }
}
