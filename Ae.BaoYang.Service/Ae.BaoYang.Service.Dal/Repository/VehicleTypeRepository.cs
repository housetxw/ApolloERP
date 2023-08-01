using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class VehicleTypeRepository : AbstractRepository<VehicleTypeDO>, IVehicleTypeRepository
    {
        public VehicleTypeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据品牌模糊查询
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeDO>> GetVehicleByBrand(string brand)
        {
            var para = new DynamicParameters();
            para.Add("@brand", $"%{brand}%");

            var result = await GetListAsync<VehicleTypeDO>("WHERE brand LIKE @brand", para);

            return result;
        }
    }
}
