using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Response.Arrival;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Imp.Mappers
{
    public class ArrivalMapper : Profile
    {
        public ArrivalMapper()
        {
            CreateMap<PagedEntity<GetArrivalListResponse>, ApiPagedResultData<GetArrivalListResponse>>().ReverseMap();


            CreateMap<ShopArrivalOrderDO, ShopArrivalOrderDTO>();

            CreateMap<ShopArrivalVideoDO, ShopArrivalVideoResponse>();
        }
     
    }
}
