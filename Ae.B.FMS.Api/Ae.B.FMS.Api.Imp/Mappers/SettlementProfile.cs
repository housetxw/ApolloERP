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
    public class SettlementProfile : Profile
    {
        public SettlementProfile()
        {

            //CreateMap<ApiPagedResultData<GetSettlementDetailDTO>, ApiPagedResultData<GetSettlementDetailResponse>>();
            CreateMap<GetSettlementDetailDTO, GetSettlementDetailResponse>();

            CreateMap<ApiPagedResultData<GetSettlementLisClientDTO>, ApiPagedResultData<GetSettlementListResponse>>();
            CreateMap<GetSettlementLisClientDTO, GetSettlementListResponse>();
            CreateMap<ApiPagedResult<GetSettlementLisClientDTO>, ApiPagedResult<GetSettlementListResponse>>();

            CreateMap<SaveSettlementReviewRequest, SaveSettlementReviewClientRequest>();

            CreateMap<GetSettlementDetailRequest, GetSettlementDetailClientRequest>();
            CreateMap<GetSettlementListRequest, GetSettlementListClientRequest>();

            CreateMap<SettlementPayReviewClientDTO, SettlementPayReviewResponse>();

            CreateMap<ApiResult<SettlementPayReviewClientDTO>, ApiResult<SettlementPayReviewResponse>>();

        }

    }
}
