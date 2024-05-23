using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Core.Model;
using ApolloErp.Web.WebApi;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Extend;
using Ae.Product.Service.Core.Model.DoorProduct;

namespace Ae.Product.Service.Imp.Mappers
{
    public class ProductManageProfile : Profile
    {
        public ProductManageProfile()
        {
            CreateMap<DimBrandDO, BrandVO>().ReverseMap();
            CreateMap<RelProductInstallserviceDO, ProductInstallserviceVo>().ReverseMap();
            CreateMap<FctProductDO, ProductSearchInfoVo>().ReverseMap();
            CreateMap<FctProductDO, PackageInfoVo>().ForMember(t => t.PackageId, (m) => m.MapFrom(t => t.Id)).ReverseMap();
            CreateMap<DimUnitDO, UnitVo>().ReverseMap();
            CreateMap<DimCategoryDO, CategoryInfoVo>().ReverseMap();
            CreateMap<RelProductPackageDO, ProductPackageDetailVo>();
            CreateMap<FctProductDO, ProductAllInfoVo>().ReverseMap();
            CreateMap<DimAttributemameDO, AttributeNameVo>().ReverseMap();

            CreateMap<DimBrandDO, GetProductBrandVo>().ReverseMap();
            CreateMap<PagedEntity<DimBrandDO>, ApiPagedResultData<GetProductBrandVo>>().ReverseMap();

            CreateMap<DimUnitDO, GetProductUnitListVo>().ReverseMap();
            CreateMap<PagedEntity<DimUnitDO>, ApiPagedResultData<GetProductUnitListVo>>().ReverseMap();

            CreateMap<DimCategoryDO, DimCategoryVo>().ReverseMap();
            CreateMap<DimCategoryDO, ListCategoryVo>().ReverseMap();
            CreateMap<DimCategoryDO, SecondCategoryVo>().ReverseMap();
            CreateMap<DimCategoryDO, ThirdCategoryVo>().ReverseMap();

            CreateMap<DimCategoryVo, TreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName)).ReverseMap();
            CreateMap<ListCategoryVo, CategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName)).ReverseMap();
            CreateMap<SecondCategoryVo, SecondCategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName)).ReverseMap();
            CreateMap<ThirdCategoryVo, ThirdCategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName)).ReverseMap();

            CreateMap<FctProductDO, ProductBaseInfoVo>().ReverseMap();

            CreateMap<DoorProductDTO, DoorProductVo>().ReverseMap();


            CreateMap<FlashSaleConfigDO, FlashSaleConfigDTO>().ReverseMap();
            CreateMap<PagedEntity<FlashSaleConfigDO>, ApiPagedResultData<FlashSaleConfigDTO>>().ReverseMap();
        }
    }
}
