using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Vehicle;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Core.Model.Vehicle;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    /// <summary>
    /// 车型Interface
    /// </summary>
    public interface IVehicleService
    {
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
        Task<List<VehicleSalesNameByNianResponse>>
            GetVehicleSalesNameByNianAsync(VehicleSalesNameByNianRequest request);

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleSearchByNameResponse>> VehicleSearchByNameAsync(VehicleSearchByNameRequest request);

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddUserCarAsync(AddUserCarRequest request);

        /// <summary>
        /// 设置用户的默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SetUserDefaultVehicleAsync(SetUserDefaultVehicleRequest request);

        /// <summary>
        /// 获取用户配置的所有车型信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<UserVehicleVO>> GetAllUserVehiclesAsync(string userId);

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleVO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request);

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserVehicleVO> GetUserDefaultVehicleAsync(string userId);

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteUserCarAsync(DeleteUserCarRequest request);

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserCarAsync(EditUserCarRequest request);

        /// <summary>
        /// 获取保险公司列表
        /// </summary>
        /// <returns></returns>
        Task<List<InsuranceCompanyVo>> GetInsuranceCompanyListAsync(InsuranceCompanyListRequest request);
    }
}
