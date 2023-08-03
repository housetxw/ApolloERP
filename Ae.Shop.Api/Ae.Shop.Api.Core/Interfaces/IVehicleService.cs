using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using Ae.Shop.Api.Core.Request.Vehicle;
using Ae.Shop.Api.Core.Response.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IVehicleService
    {
        /// <summary>
        /// 用户车型列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserVehicleSimpleVo>> GetAllUserVehicles(UserVehiclesRequest request);

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        Task<List<VehicleBrandResponse>> GetVehicleBrandListAsync();

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleListResponse>> GetVehicleListByBrandAsync(VehicleListByBrandRequest request);

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetPaiLiangByVehicleIdAsync(GetPaiLiangByVehicleIdRequest request);

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetVehicleNianByPaiLiangAsync(VehicleNianByPaiLiangRequest request);

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleSalesNameByNianResponse>> GetVehicleSalesNameByNianAsync(
           VehicleSalesNameByNianRequest request);
    }
}
