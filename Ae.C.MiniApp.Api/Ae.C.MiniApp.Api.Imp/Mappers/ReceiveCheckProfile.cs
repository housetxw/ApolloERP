using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Client.Response.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    class ReceiveCheckProfile: Profile
    {
        public ReceiveCheckProfile()
        {
            CreateMap<CheckReportClientResponse, CheckReportResponse>();
            CreateMap<OtherProjectResultDto, OtherProjectResult>();
            CreateMap<CheckReportMainDataDto, CheckReportMainData>();
            CreateMap<CheckResultClassifyDto, CheckResultClassify>();
            CreateMap<CheckClassfyDto, CheckClassfy>();
            CreateMap<CheckResultItemDto, CheckResultItem>();
            CreateMap<ErrorDesListDto, ErrorDesList>();

            CreateMap<UserVehicleFileClientResponse, UserVehicleFileResponse>();
            CreateMap<VehicleFileMainDataDto, VehicleFileMainData>();
            CreateMap<CarPartsSituationDto, CarPartsSituation>();
            CreateMap<CarPartsItemDto, CarPartsItem>();
            CreateMap<MaintenancerecordDto, Maintenancerecord>();
            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceRecordDto, ServiceRecord>();
            CreateMap<TagInfoDto, TagInfo>();
            CreateMap<ServiceProjectDto, ServiceProject>();

            CreateMap<UserReceiveCheckDto, UserReceiveCheckVo>();
            CreateMap<ReceiveCheckStatisticsDto, ReceiveCheckStatisticsVo>();
            CreateMap<CheckStatisticsDataDto, CheckStatisticsData>();

            CreateMap<CheckErrorDetailDto, CheckErrorDetailVo>();
            CreateMap<CheckPropertyResultDto, CheckPropertyResult>();
        }
    }
}
