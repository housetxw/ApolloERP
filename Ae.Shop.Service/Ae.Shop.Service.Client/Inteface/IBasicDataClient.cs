using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IBasicDataClient
    {
        Task<List<RegionChinaDTO>> RegionChinaReqByRegionId(RegionChinaReqByRegionIdClientRequest request);
        Task<List<RegionChinaDTO>> GetAllCitys(RegionChinaReqByLevelClientRequest request);
        Task<GetPositionClientResponse> GetPosition(GetPositionClientRequest request);

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetCoordinateClientResponse> GetCoordinate(GetCoordinateClientRequest request);

        /// <summary>
        /// 获取ParentId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> GetParentRegionId(ParentRegionIdClientRequest request);
    }
}
