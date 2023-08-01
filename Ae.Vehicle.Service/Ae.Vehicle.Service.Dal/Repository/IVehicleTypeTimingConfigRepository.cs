using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingConfigRepository : IRepository<VehicleTypeTimingConfigDO>
    {
        /// <summary>
        /// 根据tid查询车型配置信息
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingConfigDO>> GetVehicleTypeTimingConfigByTidsAsync(
            List<string> tidList);
    }
}
