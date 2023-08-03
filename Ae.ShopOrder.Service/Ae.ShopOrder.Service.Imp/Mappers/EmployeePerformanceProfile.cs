using AutoMapper;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Imp.Mappers
{
    public class EmployeePerformanceProfile : Profile
    {
        public EmployeePerformanceProfile()
        {

            CreateMap<EmployeePerformanceOrderDO, EmployeePerformanceOrderDTO>().ReverseMap();


        }
    }
}
