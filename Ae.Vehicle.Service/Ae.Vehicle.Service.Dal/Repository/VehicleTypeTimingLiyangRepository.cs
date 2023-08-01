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
    public class VehicleTypeTimingLiyangRepository : AbstractRepository<VehicleTypeTimingLiyangDo>,
        IVehicleTypeTimingLiyangRepository
    {
        public VehicleTypeTimingLiyangRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取所有力洋数据
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<List<VehicleTypeTimingLiyangDo>> GetAllVehicleTypeTimingLiyang(int version = -1)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            if (version >= 0)
            {
                condition.Append("WHERE `version` = @version");
                para.Add("@version", version);
            }


            var result = await GetListAsync<VehicleTypeTimingLiyangDo>(condition.ToString(), para);

            return result?.ToList() ?? new List<VehicleTypeTimingLiyangDo>();
        }

        public async Task<bool> Delete(List<string> levelIdList)
        {
            string sql = @"UPDATE vehicle_type_timing_liyang 
SET is_deleted = 1 
WHERE
	level_id IN @levelIds";

            var para = new DynamicParameters();
            para.Add("@levelIds", levelIdList);

            await OpenConnectionAsync(async conn => { await conn.ExecuteAsync(sql, para); });

            return true;
        }
    }
}
