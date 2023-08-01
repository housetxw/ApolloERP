using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IVehicleTypeRepository : IRepository<VehicleTypeDO>
    {
        /// <summary>
        /// 根据品牌模糊查询
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        Task<IEnumerable<VehicleTypeDO>> GetVehicleByBrand(string brand);
    }
}
