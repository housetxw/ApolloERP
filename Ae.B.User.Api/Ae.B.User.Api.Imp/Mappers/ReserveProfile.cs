using AutoMapper;
using Ae.B.User.Api.Client.Model.Reserve;
using Ae.B.User.Api.Core.Response.Reserve;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Imp.Mappers
{
    public class ReserveProfile : Profile
    {
        public ReserveProfile()
        {
            CreateMap<ReserveDateDto, ReserveDateVo>();
            CreateMap<ReserveTimeRecordDto, ReserveTimeRecord>();
        }
    }
}
