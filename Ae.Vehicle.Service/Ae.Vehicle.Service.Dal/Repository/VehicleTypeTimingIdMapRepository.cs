using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeTimingIdMapRepository : AbstractRepository<VehicleTypeTimingIdMapDo>,
        IVehicleTypeTimingIdMapRepository
    {
        public VehicleTypeTimingIdMapRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelIds"></param>
        /// <returns></returns>
        public async Task<List<VehicleTypeTimingIdMapDo>> GetVehicleTimingIdMapByLevelId(List<string> levelIds)
        {
            var para = new DynamicParameters();
            para.Add("@externalIds", levelIds);
            para.Add("@source", "LiYangLevelId");

            var result =
                await GetListAsync<VehicleTypeTimingIdMapDo>("WHERE external_id IN @externalIds AND source = @source",
                    para);

            return result?.ToList() ?? new List<VehicleTypeTimingIdMapDo>();
        }

        public async Task<bool> Delete(List<string> levelIdList)
        {
            string sql = @"UPDATE vehicle_type_timing_id_map 
SET is_deleted = 1 
WHERE
	external_id IN @levelIds 
	AND source = @source";

            var para = new DynamicParameters();
            para.Add("@levelIds", levelIdList);
            para.Add("@source", "LiYangLevelId");

            await OpenConnectionAsync(async conn => { await conn.ExecuteAsync(sql, para); });

            return true;
        }
    }
}
