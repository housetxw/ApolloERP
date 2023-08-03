using AutoMapper;
using Ae.Shop.Api.Core.Model.Company;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<AddCompanyRequest, CompanyDO>().ReverseMap();
            CreateMap<CompanyDO, CompanyDTO>().ReverseMap();
            CreateMap<CompanyDO, CompanyPageListForShopDTO>()
                .ForMember(dest=> dest.RegisterTime,opt=>opt.MapFrom(o=>o.RegisterTime.ToString("yyyy-MM-dd")));
            CreateMap<ShopSimpleInfoDO, ShopSimpleInfoResponse>().ReverseMap();
        }
    }
}
