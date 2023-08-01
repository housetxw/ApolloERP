using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Core.Request;

namespace Ae.Vehicle.Service.Core.Interfaces
{
    /// <summary>
    /// 车型操作Service
    /// </summary>
    public interface IVehicleOperateService
    {
        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteVehicleByTid(DeleteVehicleByTidRequest request);

        /// <summary>
        /// 力洋车型数据初始化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> InitLiYangData(InitLiYangDataRequest request);

        /// <summary>
        /// 同步vehicleId
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncVehicleId();

        /// <summary>
        /// 新增力洋新数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddVehicleByLiYang(AddVehicleByLiYangRequest request);

        /// <summary>
        /// 根据力洋Id删除车型数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteVehicleByLiYang(DeleteVehicleByLiYangRequest request);

        /// <summary>
        /// 保养适配数据同步
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncBaoYangParts();

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddVehicle(AddVehicleRequest request);
    }
}
