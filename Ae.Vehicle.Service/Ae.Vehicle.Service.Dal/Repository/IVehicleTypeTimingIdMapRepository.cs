using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingIdMapRepository : IRepository<VehicleTypeTimingIdMapDo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelIds"></param>
        /// <returns></returns>
        Task<List<VehicleTypeTimingIdMapDo>> GetVehicleTimingIdMapByLevelId(List<string> levelIds);

        Task<bool> Delete(List<string> levelIdList);
    }
}
