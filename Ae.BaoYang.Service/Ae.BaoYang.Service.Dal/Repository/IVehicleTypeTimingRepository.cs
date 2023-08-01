using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Extend;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IVehicleTypeTimingRepository:IRepository<VehicleTypeTimingDO>
    {
        /// <summary>
        /// 获取所有Tid
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SimpleVehicleDo>> GetAllTid();

        /// <summary>
        /// 搜索车型
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="nian"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeTimingDO>> SearchVehicleTypeTiming(string brand, List<string> vehicleId,
            string paiLiang, string startYear, string endYear, string nian, string factory);
    }
}
