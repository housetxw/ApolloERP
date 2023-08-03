using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IBasicDataClient
    {
        Task<GetRegionChinaClientResponse> GetAllRegionChinaWithThreeLayer();
        Task<List<RegionChinaClientDTO>> GetAllCitys(RegionChinaReqByLevelClientRequest request);
        Task<GetPositionClientResponse> GetPosition(GetPositionClientRequest request);
        Task<ThreeLevelRegionChinaClientResponse> GetThreeLevelRegionChina();
    }
}
