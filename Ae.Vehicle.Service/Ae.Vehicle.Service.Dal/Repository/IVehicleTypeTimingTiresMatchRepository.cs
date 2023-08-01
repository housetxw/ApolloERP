using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Dal.Model;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingTiresMatchRepository
    {
        /// <summary>
        /// 查五级车型原配轮胎
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingTiresMatchDO>> GetVehicleTiresMatchListAsync(List<string> tidList);
    }
}
