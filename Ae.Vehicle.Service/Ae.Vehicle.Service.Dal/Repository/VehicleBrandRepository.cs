using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleBrandRepository: AbstractRepository<VehicleBrandDO>, IVehicleBrandRepository
    {
        public VehicleBrandRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取所有车型品牌
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleBrandDO>> GetAllVehicleBrandsAsync()
        {
            var result = await GetListAsync<VehicleBrandDO>("WHERE is_deleted = 0");

            return result;
        }
    }
}
