using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeTimingConfigRepository : AbstractRepository<VehicleTypeTimingConfigDO>,
        IVehicleTypeTimingConfigRepository
    {
        public VehicleTypeTimingConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据tid查询车型配置信息
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeTimingConfigDO>> GetVehicleTypeTimingConfigByTidsAsync(
            List<string> tidList)
        {
            var para = new DynamicParameters();
            para.Add("@TidList", tidList);
            var result =
                await GetListAsync<VehicleTypeTimingConfigDO>("WHERE tid IN @TidList AND is_deleted = 0", para);
            return result;
        }
    }
}
