using Ae.Shop.Api.Client.Model.Vehicle;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Client.Response.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.VehicleServer
{
    public interface IVehicleClient
    {
        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserVehicleDto>> GetAllUserVehiclesAsync(AllUserVehiclesClientRequest request);

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleDto> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request);

        Task<List<VehicleBrandClientResponse>> GetVehicleBrandListAsync();

        Task<List<VehicleListByBrandClientResponse>> GetVehicleListByBrandAsync(VehicleListByBrandClientRequest request);

        Task<List<string>> GetPaiLiangByVehicleIdAsync(PaiLiangByVehicleIdClientRequest request);

        Task<List<string>> GetVehicleNianByPaiLiangAsync(VehicleNianByPaiLiangClientRequest request);

        Task<List<VehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNianAsync(VehicleSalesNameByNianClientRequest request);
    }
}
