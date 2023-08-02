using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.B.Product.Api.Client.Model.BaoYang;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class BaoYangProfile : Profile
    {
        public BaoYangProfile()
        {
            CreateMap<GetBaoYangPackagesRequest, BaoYangPackagesClientRequest>();
            CreateMap<VehicleRequest, VehicleClientRequest>();
            CreateMap<VehicleProperty, VehicleClientProperty>();

            CreateMap<BaoYangCategoryDto, BaoYangCategoryVO>();
            CreateMap<BaoYangPackageTypeDto, BaoYangPackageModel>();
            CreateMap<TagInfoDto, TagInfo>();
            CreateMap<BaoYangPackageItemDto, BaoYangPackageItemModel>();
            CreateMap<BaoYangPackageProductDto, BaoYangPackageProductModel>();
            CreateMap<PropertySelectDto, PropertySelectModel>();
            CreateMap<PropertyResultDto, PropertyResult>();

            CreateMap<SearchProductRequest, SearchProductClientRequest>();
            CreateMap<PidCountVo, PidCountDto>();
        }
    }
}
