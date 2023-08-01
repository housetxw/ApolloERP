using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IUserCarRepository
    {
        /// <summary>
        /// 新增用户车型
        /// </summary>
        /// <param name="userCarDo"></param>
        /// <returns></returns>
        Task<bool> AddUserCarAsync(UserCarDO userCarDo);

        /// <summary>
        /// 设置用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> SetUserDefaultVehicleAsync(string userId, string carId, string updateBy);

        /// <summary>
        /// 查一个用户所有车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<UserCarDO>> GetAllUserVehiclesAsync(string userId, bool readOnly = true);

        /// <summary>
        /// 根据userId和carId获取用户车型数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserCarDO> GetUserVehicleByCarIdAsync(string userId, string carId, bool readOnly = true);

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserCarDO> GetUserDefaultVehicleAsync(string userId, bool readOnly = true);

        /// <summary>
        /// 批量获取用户默认车辆
        /// </summary>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        Task<List<UserCarDO>> GetUserDefaultVehicleBatch(List<string> userIdList);

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="submitter"></param>
        /// <returns></returns>
        Task<bool> DeleteUserCarAsync(string userId, string carId, string submitter);

        /// <summary>
        /// 更新用户车辆信息
        /// </summary>
        /// <param name="userCarDo"></param>
        /// <returns></returns>
        Task<bool> EditUserCarAsync(EditUserCarRequest userCarDo);

        /// <summary>
        /// 根据车牌号查询用户车辆信息
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        Task<List<UserCarDO>> GetUserCarsByCarPlate(string carPlate);
    }
}
