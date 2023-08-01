using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Dal.Model;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleBrandRepository
    {
        /// <summary>
        /// 获取所有车型品牌
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VehicleBrandDO>> GetAllVehicleBrandsAsync();
    }
}
