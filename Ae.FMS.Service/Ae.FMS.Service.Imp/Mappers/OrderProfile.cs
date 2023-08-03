using AutoMapper;
using Ae.FMS.Service.Client.Model;
using Ae.FMS.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<GetAccountCheckAmountClientDTO, GetAccountCheckAmountDTO>();
            CreateMap<GetNoReconciliationAmountClientDTO, GetNoReconciliationAmountDTO>();
            
        }
    }
}
