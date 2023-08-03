using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Client.Model.Vehicle;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IVehicleClient
    {
        Task<UserVehicleDTO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request);

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddUserCarAsync(AddUserCarClientRequest request);

        /// <summary>
        /// 得到默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleDTO> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request);

        /// <summary>
        /// 编辑车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserCarAsync(EditUserCarClientRequest request);

        /// <summary>
        /// 用户车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserCarFileDto> GetUserCarFile(UserCarFileClientRequest request);

        /// <summary>
        /// 更新车辆部位检查信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateCarPartCheckResult(UpdateCarPartCheckResultClientRequest request);

        /// <summary>
        /// 车辆部位异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserCarComponentsResultDto>> GetUserCarComponentsErrorCheck(UserCarComponentsErrorClientCheckRequest request);
    }
}
