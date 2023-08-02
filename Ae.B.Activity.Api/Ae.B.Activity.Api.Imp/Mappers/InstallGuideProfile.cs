using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Imp.Mappers
{
    public class InstallGuideProfile : Profile
    {
        public InstallGuideProfile()
        {
            CreateMap<GetInstallGuidePagesRequest, GetInstallGuidePagesClientRequest>();

            CreateMap<UpdateInstallGuideStatusRequest, UpdateInstallGuideStatusClientRequest>();

            CreateMap<InstallGuideCategoryClientDTO, InstallGuideCategoryDTO>();

            CreateMap<ApiResult<List<InstallGuideCategoryClientDTO>>, ApiResult<List<InstallGuideCategoryDTO>>>();

            CreateMap<InstallGuideCategoryClientDTO, InstallGuideCategoryDTO>();

            CreateMap<InstallGuideDTO, InstallGuideClientDTO>();

            CreateMap<InstallGuideClientDTO, InstallGuideDTO>();

            CreateMap<ApiResult<InstallGuideClientDTO>, ApiResult<InstallGuideDTO>>();

            CreateMap<ApiPagedResult<InstallGuideClientDTO>, ApiPagedResult<InstallGuideDTO>>();

            CreateMap<ApiPagedResultData<InstallGuideClientDTO>, ApiPagedResultData<InstallGuideDTO>>();

            CreateMap<InstallGuideFileClientDTO, InstallGuideFileDTO>();

            CreateMap<InstallGuideFileDTO, InstallGuideFileClientDTO>();
        }
    }
}
