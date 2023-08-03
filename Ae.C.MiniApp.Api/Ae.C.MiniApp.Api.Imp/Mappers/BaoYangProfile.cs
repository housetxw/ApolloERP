using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using Ae.C.MiniApp.Api.Client.Response.BaoYang;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using Ae.C.MiniApp.Api.Core.Response.Adaptation;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class BaoYangProfile:Profile
    {
        public BaoYangProfile()
        {
            CreateMap<GetBaoYangPackagesRequest, BaoYangPackagesClientRequest>().ReverseMap();
            CreateMap<VehicleRequest, VehicleClientRequest>().ReverseMap();
            CreateMap<VehicleProperty, VehicleClientProperty>().ReverseMap();
            CreateMap<BaoYangCategoryDto, BaoYangCategoryVO>();
            CreateMap<BaoYangPackageDto, BaoYangPackageModel>();
            CreateMap<TagInfoDto, TagInfo>();
            CreateMap<BaoYangPackageItemDto, BaoYangPackageItemModel>();
            CreateMap<BaoYangPackageProductDto, BaoYangPackageProductModel>();
            CreateMap<PropertySelectDto, PropertySelectModel>();
            CreateMap<PropertyResultDto, PropertyResult>();

            CreateMap<SearchProductRequest, SearchProductClientRequest>();

            CreateMap<TireCategoryListRequest, TireCategoryListClientRequest>();
            CreateMap<TireServiceListClientResponse, GetTireServiceListResponse>();
            CreateMap<TireCategoryDto, TireCategoryVO>();
            CreateMap<TireProductDto, TireProductVO>();
            CreateMap<GetTireListRequest, TireListClientRequest>();
            CreateMap<PidCountVo, PidCountDto>();

            CreateMap<VerifyAdaptiveProductForBuyClientResponse, VerifyAdaptiveProductForBuyResponse>();
        }
    }
}
