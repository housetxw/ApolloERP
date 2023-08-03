using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Model.Vehicle;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Vehicle;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Request.Vehicle;

namespace Ae.C.MiniApp.Api.Client.Clients.Interface
{
    public interface IVehicleClient
    {
        Task<string> AddUserCarAsync(AddUserCarClientRequest request);
        Task<bool> DeleteUserCarAsync(DeleteUserCarClientRequest request);
        Task<bool> EditUserCarAsync(EditUserCarClientRequest request);
        Task<List<UserVehicleDTO>> GetAllUserVehiclesAsync(AllUserVehiclesClientRequest request);
        Task<List<InsuranceCompanyDto>> GetInsuranceCompanyListAsync(InsuranceCompanyListClientRequest request);
        Task<List<string>> GetPaiLiangByVehicleIdAsync(PaiLiangByVehicleIdClientRequest request);
        Task<UserVehicleDTO> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request);
        Task<UserVehicleDTO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request);
        Task<List<VehicleBrandClientResponse>> GetVehicleBrandListAsync();

        Task<List<VehicleListByBrandClientResponse>>
            GetVehicleListByBrandAsync(VehicleListByBrandClientRequest request);

        Task<List<string>> GetVehicleNianByPaiLiangAsync(VehicleNianByPaiLiangClientRequest request);

        Task<List<VehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianClientRequest request);

        Task<bool> SetUserDefaultVehicleAsync(SetUserDefaultVehicleClientRequest request);

        Task<List<VehicleSearchByNameClientResponse>>
            VehicleSearchByNameAsync(VehicleSearchByNameClientRequest request);

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleBaseInfoDto>> GetVehicleBaseInfoList(VehicleInfoListClientRequest request);
    }
}