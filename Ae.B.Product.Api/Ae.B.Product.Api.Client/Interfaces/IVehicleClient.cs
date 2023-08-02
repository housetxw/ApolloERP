using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Model.Vehicle;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Request.Vehicle;
using Ae.B.Product.Api.Client.Response.Vehicle;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IVehicleClient
    {
        Task<List<VehicleInfo>> GetVehicleInfoList(VehicleInfoListRequest request);

        Task<List<VehicleBrandClientResponse>> GetVehicleBrandListAsync();

        Task<List<VehicleListByBrandClientResponse>> GetVehicleListByBrandAsync(
            VehicleListByBrandClientRequest request);

        Task<List<string>> GetPaiLiangByVehicleIdAsync(
            PaiLiangByVehicleIdClientRequest request);

        Task<List<string>> GetVehicleNianByPaiLiangAsync(
            VehicleNianByPaiLiangClientRequest request);

        Task<List<VehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianClientRequest request);


        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleBaseInfo>> GetVehicleBaseInfoList(VehicleInfoListRequest request);

        /// <summary>
        /// 根据Tids查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListByTids(
            VehicleBaseInfoListByTidsClientRequest request);

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteVehicleByTid(DeleteVehicleByTidClientRequest request);

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetTidsByVehicle(TidsByVehicleClientRequest request);
    }
}
