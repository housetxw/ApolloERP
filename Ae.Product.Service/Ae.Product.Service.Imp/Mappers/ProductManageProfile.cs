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
            CreateMap<DimBrandDO, BrandVO>();
            CreateMap<RelProductInstallserviceDO, ProductInstallserviceVo>();
            CreateMap<FctProductDO, ProductSearchInfoVo>();
            CreateMap<FctProductDO, PackageInfoVo>().ForMember(t => t.PackageId, (m) => m.MapFrom(t => t.Id));
            CreateMap<DimUnitDO, UnitVo>();
            CreateMap<DimCategoryDO, CategoryInfoVo>().ReverseMap();
            CreateMap<RelProductPackageDO, ProductPackageDetailVo>();
            CreateMap<FctProductDO, ProductAllInfoVo>().ReverseMap();
            CreateMap<DimAttributemameDO, AttributeNameVo>().ReverseMap();

            CreateMap<DimBrandDO, GetProductBrandVo>();
            CreateMap<PagedEntity<DimBrandDO>, ApiPagedResultData<GetProductBrandVo>>();

            CreateMap<DimUnitDO, GetProductUnitListVo>();
            CreateMap<PagedEntity<DimUnitDO>, ApiPagedResultData<GetProductUnitListVo>>();

            CreateMap<DimCategoryDO, DimCategoryVo>().ReverseMap();
            CreateMap<DimCategoryDO, ListCategoryVo>();
            CreateMap<DimCategoryDO, SecondCategoryVo>();
            CreateMap<DimCategoryDO, ThirdCategoryVo>();

            CreateMap<DimCategoryVo, TreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName));
            CreateMap<ListCategoryVo, CategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName));
            CreateMap<SecondCategoryVo, SecondCategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName));
            CreateMap<ThirdCategoryVo, ThirdCategoryTreeSelectVo>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.DisplayName));

            CreateMap<FctProductDO, ProductBaseInfoVo>();

            CreateMap<DoorProductDTO, DoorProductVo>();

            CreateMap<DoorProductDTO, DoorProductVo>();

            CreateMap<FlashSaleConfigDO, FlashSaleConfigDTO>();
            CreateMap<FlashSaleConfigDTO, FlashSaleConfigDO>();
        }
    }
}
