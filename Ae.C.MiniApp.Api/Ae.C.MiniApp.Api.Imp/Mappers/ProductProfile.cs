using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model.Product;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAllInfoDto, ProductDetalInfoVo>().ReverseMap();
            CreateMap<ProductInstallservicDto, InstallserviceVo>().ReverseMap();
            CreateMap<ProductBaseInfoDto, ProductBaseInfoVo>().ReverseMap();
            CreateMap<ProductBaseInfoDto, CategoryProductVo>().ReverseMap();
            CreateMap<ProductBaseInfoDto, SearchProductVo>().ReverseMap();

            CreateMap<ConfigHotProductDto, HotProductVo>()
                .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.Image1.AddImageDomain())).ReverseMap();

            CreateMap<PackageCardProductDto, PackageCardProductVo>()
                .ForMember(dest => dest.Image1, opt => opt.MapFrom(src => src.Image1.AddImageDomain())).ReverseMap();
        }
    }
}

