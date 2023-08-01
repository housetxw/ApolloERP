using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingRepository : IRepository<VehicleTypeTimingDO>
    {
        /// <summary>
        /// 获取排量
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> GetPaiLiangByVehicleIdAsync(string vehicleId);

        /// <summary>
        /// 获取生产年份
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleNianByPaiLiangAsync(string vehicleId, string paiLiang);

        /// <summary>
        /// 根据生产年份查款型
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleSalesNameByNianAsync(string vehicleId,
            string paiLiang, int nian);

        /// <summary>
        /// 查询车型信息
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleListAsync(string vehicle, string paiLiang,
            string nian, string tid);

        /// <summary>
        /// 查询车型信息
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleListAsync(List<string> tidList);

        /// <summary>
        /// 根据tidList查询车型
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<List<VehicleTypeTimingDO>> GetVehiclesByTidList(List<string> tidList);

        /// <summary>
        /// 根据Tid查车型信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<VehicleTypeTimingDO> GetVehicleDetailByTidAsync(string tid);

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<bool> DeleteVehicleByTid(string tid, string submitBy);

        Task<List<VehicleTypeTimingDO>> GetAllVehicleTiming();

        Task<bool> Delete(List<string> tidList);

        Task<List<string>> GetEmptyTid();
    }
}
