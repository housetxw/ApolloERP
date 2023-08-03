using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IBasicDataService
    {
        Task<GetRegionChinaResponse> GetAllRegionChinaWithThreeLayer();
        Task<List<RegionChinaVO>> GetAllCitys();
        Task<LocationResponse> GetPosition(LocationRequest request);
        Task<ThreeLevelRegionChinaResponse> GetThreeLevelRegionChina();
    }
}
