using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeTimingTiresMatchRepository : AbstractRepository<VehicleTypeTimingTiresMatchDO>,
        IVehicleTypeTimingTiresMatchRepository
    {
        public VehicleTypeTimingTiresMatchRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 查五级车型原配轮胎
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeTimingTiresMatchDO>> GetVehicleTiresMatchListAsync(List<string> tidList)
        {
            var para = new DynamicParameters();
            para.Add("@TidList", tidList);
            var result =
                await GetListAsync<VehicleTypeTimingTiresMatchDO>("WHERE tid IN @TidList", para);

            return result;
        }
    }
}
