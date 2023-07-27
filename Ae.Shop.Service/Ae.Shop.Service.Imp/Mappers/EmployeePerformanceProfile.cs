using AutoMapper;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Imp.Mappers
{
    public class EmployeePerformanceProfile : Profile
    {
        public EmployeePerformanceProfile()
        {
            CreateMap<CreateOrUpdatePullNewConfigRequest, PullNewConfigDO>();

            CreateMap<PullNewConfigDO, GetPullNewConfigResponse>();

            CreateMap<BasicPerformanceConfigDO, ShopServiceConfigDTO>();

            CreateMap<ShopPerformanceConfigDO, ShopPerformanceConfigDTO>().ReverseMap();


        }
    }
}
