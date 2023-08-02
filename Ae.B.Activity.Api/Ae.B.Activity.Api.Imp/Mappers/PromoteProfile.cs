using AutoMapper;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using ApolloErp.Web.WebApi;
using System.Collections.Generic;

namespace Ae.B.Activity.Api.Imp.Mappers
{
    public class PromoteProfile : Profile
    {
        public PromoteProfile()
        {
            CreateMap<GetPromoteContentsClientRequest, GetPromoteContentsRequest>();

            CreateMap<GetPromoteContentsRequest, GetPromoteContentsClientRequest>();

            CreateMap<PromoteContentClientDTO, PromoteContentDTO>();

            CreateMap<PromoteContentDTO, PromoteContentClientDTO>();
            CreateMap<PromoteContentAttachmentDTO, PromoteContentAttachmentClientDTO>();

            CreateMap<PromoteContentAttachmentClientDTO, PromoteContentAttachmentDTO>();


            CreateMap<ApiPagedResult<PromoteContentClientDTO>, ApiPagedResult<PromoteContentDTO>>();

            CreateMap<ApiPagedResultData<PromoteContentClientDTO>, ApiPagedResultData<PromoteContentDTO>>();

            CreateMap<ApiResult<PromoteContentClientDTO>, ApiResult<PromoteContentDTO>>();

            CreateMap<UpdatePromoteStatusRequest, UpdatePromoteStatusClientRequest>();

            CreateMap<ActivityLogDTO, ActivityLogClientDTO>();

            CreateMap<ActivityLogClientDTO, ActivityLogDTO>();

            CreateMap<ApiResult<List<ActivityLogClientDTO>>, ApiResult<List<ActivityLogDTO>>>();

            CreateMap<ApiResult<ActivityLogClientDTO>, ApiResult<ActivityLogDTO>>();

            CreateMap<GetActivityLogRequest, GetActivityLogClientRequest>();            
        }
    }
}
