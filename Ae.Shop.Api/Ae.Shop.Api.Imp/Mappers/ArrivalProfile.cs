using AutoMapper;
using Ae.Shop.Api.Core.Model.Company;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Dal.Model.Company;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ArrivalProfile : Profile
    {
        public ArrivalProfile()
        {
            CreateMap<ArrivalTrendStatisticsReqDTO, ArrivalTrendStatisticsReqDO>();
            
        }
    }
}
