using AutoMapper;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Model.Product;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Request.Product;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Common.Util;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Product.Api.Common.Extension;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAllInfoDto, ProductAllInfoVo>().ReverseMap();
            CreateMap<ProductSearchRequest, ProductSearchClientRequest>().ReverseMap();
            CreateMap<ProductSearchClientResponse, ProductSearchResponse>().ReverseMap();
            CreateMap<UnitDto, UnitVo>().ReverseMap();
            CreateMap<ProductDetailSearchRequest, ProductDetailSearchClientRequest>().ReverseMap();
            CreateMap<ProductInstallserviceDto, ProductInstallserviceVo>().ReverseMap();
            CreateMap<ProductAttributevalueDto, ProductAttributevalueVo>().ReverseMap();
            CreateMap<ProductDetailDto, ProductDetailVo>().ReverseMap();
            CreateMap<CategoryInfoDto, CategoryInfoVo>().ReverseMap();
            CreateMap<BrandDto, CatalogBrandVo>().ReverseMap();
            CreateMap<CategoryAttributeDto, CategoryAttributeVo>().ReverseMap();
            CreateMap<ProductPackageDetailVo, ProductPackageDetailDto>().ReverseMap();
            CreateMap<ProductEditRequest, ProductEditClientRequest>();
            CreateMap<CategoryAttributeDto, ProductAttributevalueVo>();

            CreateMap<DimCategoryDto, DimCategoryVo>();
            CreateMap<ListCategoryDto, ListCategoryVo>();
            CreateMap<SecondCategoryDto, SecondCategoryVo>();
            CreateMap<ThirdCategoryDto, ThirdCategoryVo>();
            CreateMap<AttributeNameDto, AttributeNameVo>().ReverseMap();
            CreateMap<AttributeSearchRequest, AttributeSearchClientRequest>();
            CreateMap<AttributevalueDto, AttributevalueVo>().ReverseMap();
            CreateMap<AttributeClientResponse, AttributeResponse>();
            CreateMap<AttributeEditRequest, AttributeEditClientRequest>();
            CreateMap<CategoryAttributeEditRequest, CategoryAttributeEditClientRequest>();

            CreateMap<AddDoorProductsVo, AddDoorProductsDto>().ReverseMap();
            CreateMap<DoorProductDto, DoorProductVo>().ReverseMap();

            CreateMap<ProductInstallServiceDto, ProductInstallServiceVo>().ReverseMap();
            CreateMap<InstallServiceDto, InstallServiceVo>().ReverseMap();
            CreateMap<ProductBaseInfoDto, ProductBaseInfoVo>().ReverseMap();

            CreateMap<ConfigHotProductDto, ConfigHotProductVo>()
                .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.Image1.AddImageDomain())).ReverseMap();

            CreateMap<PackageCardProductDto, PackageCardProductVo>()
                .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.Image1.AddImageDomain()))
                .ForMember(d => d.TypeDisplay, o => o.MapFrom(s => s.Type.GetEnumDescription()));

            CreateMap<GrouponProductDto, GrouponProductVo>()
                .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.Image1.AddImageDomain())).ReverseMap();
        }
    }
}
