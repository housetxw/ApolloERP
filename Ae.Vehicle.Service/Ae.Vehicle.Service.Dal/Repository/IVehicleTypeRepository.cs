using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeRepository:IRepository<VehicleTypeDO>
    {
        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeDO>> GetVehicleListByBrand(string brand);

        /// <summary>
        /// 获取所有车系
        /// </summary>
        Task<List<VehicleTypeDO>> GetAllVehicleList();

        /// <summary>
        /// 根据车系Id查车系信息
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Task<VehicleTypeDO> GetVehicleByVehicleIdAsync(string vehicleId);

        /// <summary>
        /// 根据车系Id查车系信息
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeDO>> GetVehicleByVehicleIdAsync(List<string> vehicleIds);

        /// <summary>
        /// 车系模糊搜索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeDO>> VehicleSearchByNameAsync(string name);
    }
}
