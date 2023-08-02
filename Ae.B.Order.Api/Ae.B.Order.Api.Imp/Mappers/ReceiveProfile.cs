using AutoMapper;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Imp.Mappers
{
    public class ReceiveProfile : Profile
    {
        public ReceiveProfile() 
        {
            CreateMap<ModifyReserveTimeRequest, ModifyReserveTimeClientRequest>();
        }
    }
}
