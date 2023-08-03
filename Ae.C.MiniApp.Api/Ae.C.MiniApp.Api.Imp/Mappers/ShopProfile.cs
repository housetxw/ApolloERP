using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Model.Shop;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<JoinInRequest, JoinInClientRequest>().ReverseMap();
            CreateMap<GetNearShopListRequest, GetNearShopListClientRequest>().ReverseMap();
            CreateMap<NearShopInfoDTO, NearShopInfoVO>()
                .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.Img.AddImageDomain())).ForMember(dest => dest.Type, opt => opt.MapFrom(src => ((ShopTypeEnum)src.Type).GetDescription()))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => ((ShopStatusEnum)src.Status).GetDescription()));
            CreateMap<GetShopDetailClientResponse, GetShopDetailResponse>().ReverseMap();
            CreateMap<GetShopDetailRequest, GetShopDetailClientRequest>().ReverseMap();
            CreateMap<TechDTO, TechVO>()
                .ForMember(dest => dest.HeadImg, opt => opt.MapFrom(src => src.HeadImg.AddImageDomain()));//图片绝对路径
            CreateMap<SimpleServiceDTO, SimpleServiceVO>().ReverseMap();
            CreateMap<ShopLocationDTO, ShopLocationVO>().ReverseMap(); 
            CreateMap<GetRegionByCityIdRequest, GetRegionByCityIdClientRequest>().ReverseMap();
            CreateMap<GetCompanyInfoRequest, GetCompanyInfoClientRequest>().ReverseMap();
            CreateMap<CompanySimpleInfoClientResponse, GetCompanyInfoResponse>().ReverseMap();
            CreateMap<RegionChinaDTO, RegionChinaVO>().ReverseMap();
            CreateMap<GetCityListClientResponse, GetAllCitysResponse>().ReverseMap();
            CreateMap<GetAllCitysDTO, GetAllCitysVO>().ReverseMap();
            CreateMap<CityInfo, CityInfoVO>().ReverseMap();
            CreateMap<ShopServiceProjectDTO, ShopServiceProjectVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<GetOptimalShopRequest, GetOptimalShopClientRequest>().ReverseMap();
            CreateMap<GetOptimalShopClientResponse, GetOptimalShopResponse>().ReverseMap();

            CreateMap<BrandDTO, BrandVO>().ReverseMap();
        }
    }
}
