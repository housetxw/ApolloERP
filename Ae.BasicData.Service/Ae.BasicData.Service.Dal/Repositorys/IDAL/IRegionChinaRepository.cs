using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;

namespace Ae.BasicData.Service.Dal.Repositorys.IDAL
{
    public interface IRegionChinaRepository
    {
        Task<List<RegionChinaDO>> GetAllRegionChinaList();

        Task<List<RegionChinaDO>> GetRegionChinaListByRegionId(RegionChinaReqByRegionIdDTO req);

        Task<List<RegionChinaDO>> GetRegionChinaListByLevel(RegionChinaReqByLevelDTO req);
        Task<RegionIdDO> GetConverseRegion(string districtId);

        Task<RegionChinaDO> GetRegionChinaByRegionId(string regionId);
    }
}