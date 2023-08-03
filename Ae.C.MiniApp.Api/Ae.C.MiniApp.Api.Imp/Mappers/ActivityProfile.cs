using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<SharePromotionContentRequest, SharePromotionContentClientRequest>();
            CreateMap<ApiRequest<SharePromotionContentRequest>, ApiRequest<SharePromotionContentClientRequest>>();
        }
    }
}
