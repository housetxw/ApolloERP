using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Core.Model.Vehicle;
using Ae.B.Product.Api.Core.Request.Vehicle;
using Ae.B.Product.Api.Core.Response.Vehicle;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        Task<List<VehicleBrandResponse>> GetVehicleBrandList();

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleListResponse>> GetVehicleListByBrand(VehicleListByBrandRequest request);

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        Task<List<string>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdRequest request);

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetVehicleNianByPaiLiang(VehicleNianByPaiLiangRequest request);

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleSalesNameByNianResponse>> GetVehicleSalesNameByNian(VehicleSalesNameByNianRequest request);

        /// <summary>
        /// 车型详细信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleDetailVo>> GetVehicleInfoList(VehicleInfoListRequest request);

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteVehicleByTid(DeleteVehicleByTidRequest request);

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetTidsByVehicle(TidsByVehicleRequest request);
    }
}
