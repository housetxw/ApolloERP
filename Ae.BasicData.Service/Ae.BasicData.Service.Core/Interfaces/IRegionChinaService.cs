using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;

namespace Ae.BasicData.Service.Core.Interfaces
{
    public interface IRegionChinaService
    {
        Task<List<RegionChinaResDTO>> GetAllRegionChinaList();

        Task<List<RegionChinaResDTO>> GetRegionChinaListByRegionId(RegionChinaReqByRegionIdDTO req);

        Task<List<RegionChinaResByLevelDTO>> GetRegionChinaListByLevel(RegionChinaReqByLevelDTO req);
        Task<GetRegionChinaResponse> GetAllRegionChinaWithThreeLayer();
        Task<ApiResult<GetPositionResponse>> GetPosition(GetPositionRequest request);
        Task<ThreeLevelRegionChinaResponse> GetThreeLevelRegionChina();

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetCoordinateResponse>> GetCoordinate(GetCoordinateRequest request);

        /// <summary>
        /// 获取父级RegionId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> GetParentRegionId(ParentRegionIdRequest request);
    }
}
