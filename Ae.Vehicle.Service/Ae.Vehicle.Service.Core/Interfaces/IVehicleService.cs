using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Core.Model;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Core.Response;

namespace Ae.Vehicle.Service.Core.Interfaces
{
    /// <summary>
    /// 车型
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        Task<List<GetVehicleBrandResponse>> GetVehicleBrandsAsync();

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        Task<List<GetVehicleListResponse>> GetVehicleListByBrandAsync(string brand);

        /// <summary>
        /// 获取排量
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Task<List<string>> GetPaiLiangByVehicleIdAsync(string vehicleId);

        /// <summary>
        /// 根据排量查询生产年份
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <returns></returns>
        Task<List<string>> GetVehicleNianByPaiLiangAsync(string vehicleId, string paiLiang);

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <returns></returns>
        Task<List<GetVehicleSalesNameByNianResponse>> GetVehicleSalesNameByNianAsync(string vehicleId,
            string paiLiang, string nian);

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleSearchByNameResponse>> VehicleSearchByNameAsync(VehicleSearchByNameRequest request);

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleInfo>> GetVehicleInfoListAsync(VehicleInfoListRequest request);

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetTidsByVehicle(TidsByVehicleRequest request);

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListAsync(VehicleInfoListRequest request);

        /// <summary>
        /// 根据Tids查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListByTids(
            VehicleInfoListByTidsRequest request);

        /// <summary>
        /// 五级车型查询车型配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleConfigVO>> GetVehicleConfigByTidList(VehicleConfigRequest request);

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
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserVehicleVO>> GetAllUserVehiclesAsync(AllUserVehiclesRequest request);

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleVO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request);

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleVO> GetUserDefaultVehicleAsync(UserDefaultVehicleRequest request);

        /// <summary>
        /// 批量获取用户默认车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserVehicleVO>> GetUserDefaultVehicleBatch(UserDefaultVehicleBatchRequest request);

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
        /// 根据车系Id查详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VehicleTypeVo> GetVehicleTypeById(VehicleTypeByIdRequest request);

        /// <summary>
        /// 五级车型查询原配轮胎尺寸
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OeTireSizeResponse> GetOeTireSizeByTid(OeTireSizeByTidRequest request);

        /// <summary>
        /// 原配轮胎查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OeTirePidByTidResponse> GetOeTirePidByTid(OeTirePidByTidRequest request);

        /// <summary>
        /// 用户车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserCarFileVo> GetUserCarFile(UserCarFileRequest request);

        /// <summary>
        /// 更新车辆部位检查信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateCarPartCheckResult(UpdateCarPartCheckResultRequest request);

        /// <summary>
        /// 车辆部位异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserCarComponentsResultVo>> GetUserCarComponentsErrorCheck(UserCarComponentsErrorCheckRequest request);

        /// <summary>
        /// 根据车牌查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<string>, int>> GetUserIdByCarPlate(UserIdByCarPlateRequest request);

        /// <summary>
        /// Vin码识别车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<VehicleSimpleVo>> GetVehiclesByVin(VehiclesByVinRequest request);
    }
}
