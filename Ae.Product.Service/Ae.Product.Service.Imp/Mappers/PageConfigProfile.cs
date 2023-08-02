using AutoMapper;
using Ae.Product.Service.Common.Util;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Imp.Mappers
{
    public class PageConfigProfile : Profile
    {
        public PageConfigProfile()
        {
            CreateMap<ConfigAdvertisementDo, ConfigAdvertisementVo>()
                .ForMember(t => t.ImageUrl, (m) => m.MapFrom(t => t.ImageUrl.AddImageDomain())).ReverseMap();
            CreateMap<ConfigFrontCategoryDo, ConfigFrontCategoryVo>()
                .ForMember(t => t.ImageUrl, (m) => m.MapFrom(t => t.ImageUrl.AddImageDomain())).ReverseMap();

            CreateMap<FctProductDO, ConfigHotProductVo>().ReverseMap();

            CreateMap<FctProductDO, PackageCardProductVo>().ReverseMap();

            CreateMap<FctProductDO, GrouponProductVo>().ReverseMap();

            CreateMap<ConfigShopPackageCardDO, ConfigShopPackageCardDTO>().ReverseMap();
        }
    }
}
