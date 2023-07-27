using AutoMapper;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Dal.Model;

namespace Ae.BasicData.Service.Imp.Mappers
{
    public class BasicDataProfile : Profile
    {
        public BasicDataProfile()
        {
            CreateMap<Purchase, PurchaseInfo>().ReverseMap();

            #region RegionChina

            CreateMap<RegionChinaDO, RegionChinaDTO>().ReverseMap();
            CreateMap<RegionChinaDO, RegionChinaResDTO>().ReverseMap();
            CreateMap<RegionChinaDO, RegionChinaResByLevelDTO>().ReverseMap();

            #endregion RegionChina

            #region AppOperationLog

            CreateMap<AppVersionDO, AppVersionDTO>().ReverseMap();
            CreateMap<AppVersionDO, AppVersionListResDTO>().ReverseMap();
            CreateMap<AppVersionDO, AppVersionEntityResDTO>().ReverseMap();
            CreateMap<CreateAppOperationLogReqDTO, AppOperationLogDO>().ReverseMap();

            #endregion AppOperationLog
            

        }
    }
}
