using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingLiyangRepository : IRepository<VehicleTypeTimingLiyangDo>
    {
        /// <summary>
        /// 获取所有力洋数据
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        Task<List<VehicleTypeTimingLiyangDo>> GetAllVehicleTypeTimingLiyang(int version = -1);

        Task<bool> Delete(List<string> levelIdList);
    }
}
