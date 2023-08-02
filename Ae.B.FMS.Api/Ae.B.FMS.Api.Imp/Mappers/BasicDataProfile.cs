using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Model;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;

namespace Ae.B.FMS.Api.Imp.Mappers
{
    public class BasicDataProfile:Profile
    {
        public BasicDataProfile() {
            CreateMap<GetRegionChinaListByRegionIdRequest, GetRegionChinaListByRegionIdClientRequest>();

            CreateMap<GetRegionChinaListByRegionIdDTO, GetRegionChinaListByRegionIdResponse>();
        }
    }
}
