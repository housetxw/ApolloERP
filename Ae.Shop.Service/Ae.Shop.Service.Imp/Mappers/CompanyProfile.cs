using AutoMapper;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Model.Company;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Imp.Mappers
{
    /// <summary>
    /// 公司
    /// </summary>
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyDTO, CompanyDO>().ReverseMap();
            CreateMap<CompanyDO, CompanyDTO>().ReverseMap();
            CreateMap<CompanyDO, CompanyPageListForShopDTO>();
            CreateMap<CompanyLessInfoDO, CompanyLessInfoDTO>();
        }
    }
}
