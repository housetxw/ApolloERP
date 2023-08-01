using ApolloErp.Data.DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Dal.Model;
using Dapper;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeTimingRepository : AbstractRepository<VehicleTypeTimingDO>, IVehicleTypeTimingRepository
    {
        public VehicleTypeTimingRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<IEnumerable<VehicleTypeTimingDO>> GetPaiLiangByVehicleIdAsync(string vehicleId)
        {
            var para = new DynamicParameters();
            para.Add("@vehicleId", vehicleId);

            string sql = @"SELECT DISTINCT
	pai_liang AS `PaiLiang` 
FROM
	vehicle_type_timing 
WHERE
	vehicle_id = @vehicleId
	AND is_deleted = 0;";

            IEnumerable<VehicleTypeTimingDO> result = new List<VehicleTypeTimingDO>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<VehicleTypeTimingDO>(sql, para);
            });

            return result;
        }

        public async Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleNianByPaiLiangAsync(string vehicleId,
            string paiLiang)
        {
            string sql = @"SELECT
	listed_year AS `ListedYear`,
	nian AS `Nian`,
	stop_production_year AS `StopProductionYear` 
FROM
	vehicle_type_timing 
WHERE
	vehicle_id = @vehicleId 
	AND pai_liang = @paiLiang 
	AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@vehicleId", vehicleId);
            para.Add("@paiLiang", paiLiang);

            IEnumerable<VehicleTypeTimingDO> result = new List<VehicleTypeTimingDO>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<VehicleTypeTimingDO>(sql, para);
            });

            return result;
        }

        /// <summary>
        /// 根据生产年份查款型
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleSalesNameByNianAsync(string vehicleId,
            string paiLiang, int nian)
        {
            string sql = @"SELECT
	tid AS `Tid`,
	nian AS `Nian`,
	sales_name AS `SalesName` 
FROM
	vehicle_type_timing 
WHERE
	vehicle_id = @vehicleId 
	AND pai_liang = @paiLiang 
	AND ( @nian BETWEEN listed_year AND IF ( stop_production_year IS NULL OR stop_production_year = '', YEAR ( NOW( ) ), stop_production_year ) )
    AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@vehicleId", vehicleId);
            para.Add("@paiLiang", paiLiang);
            para.Add("@nian", nian);

            IEnumerable<VehicleTypeTimingDO> result = new List<VehicleTypeTimingDO>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<VehicleTypeTimingDO>(sql, para);
            });

            return result;
        }

        /// <summary>
        /// 查询车型信息
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleListAsync(string vehicle, string paiLiang,
            string nian, string tid)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();

            if (!string.IsNullOrEmpty(tid))
            {
                condition.Append("WHERE tid = @tid");
                para.Add("@tid", tid);
            }
            else
            {
                condition.Append("WHERE vehicle_id = @vehicleId");
                para.Add("@vehicleId", vehicle);
                if (!string.IsNullOrEmpty(paiLiang))
                {
                    condition.Append(" AND pai_liang = @paiLiang");
                    para.Add("@paiLiang", paiLiang);
                }
                if (!string.IsNullOrEmpty(nian))
                {
                    condition.Append(
                        " AND ( @nian BETWEEN listed_year AND IF ( stop_production_year IS NULL OR stop_production_year = '', YEAR ( NOW( ) ), stop_production_year ) )");
                    para.Add("@nian", nian);
                }
            }

            condition.Append(" AND is_deleted = 0");

            var result = await GetListAsync<VehicleTypeTimingDO>(condition.ToString(), para);

            return result;
        }

        /// <summary>
        /// 查询车型信息
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeTimingDO>> GetVehicleListAsync(List<string> tidList)
        {
            var para = new DynamicParameters();
            if (tidList == null || !tidList.Any())
            {
                return new List<VehicleTypeTimingDO>();
            }

            para.Add("@tid", tidList);

            var result = await GetListAsync<VehicleTypeTimingDO>("WHERE tid IN @tid", para);

            return result;
        }

        /// <summary>
        /// 根据tidList查询车型
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<List<VehicleTypeTimingDO>> GetVehiclesByTidList(List<string> tidList)
        {
            IEnumerable<VehicleTypeTimingDO> result = new List<VehicleTypeTimingDO>();
            var para = new DynamicParameters();
            para.Add("@tids", tidList);

            string sql = @"SELECT
	b.brand AS Brand,
	b.vehicle AS Vehicle,
	a.vehicle_id AS VehicleId,
	a.pai_liang AS PaiLiang,
	a.nian AS Nian,
	a.listed_year AS ListedYear,
	a.sales_name AS SalesName,
	a.tid AS Tid 
FROM
	vehicle_type_timing a
	INNER JOIN vehicle_type b ON ( a.vehicle_id = b.vehicle_id AND b.is_deleted = 0 ) 
WHERE
	a.tid IN @tids 
	AND a.is_deleted = 0;";

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<VehicleTypeTimingDO>(sql, para);
            });

            return result?.ToList() ?? new List<VehicleTypeTimingDO>();
        }

        /// <summary>
        /// 根据Tid查车型信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<VehicleTypeTimingDO> GetVehicleDetailByTidAsync(string tid)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            var result = await GetListAsync<VehicleTypeTimingDO>("WHERE tid = @tid", para);
            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleByTid(string tid, string submitBy)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@updateBy", submitBy);

            string sql = @"UPDATE vehicle_type_timing 
SET is_deleted = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	tid = @tid;";

            int result = 0;

            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });

            return result > 0;
        }

        public async Task<List<VehicleTypeTimingDO>> GetAllVehicleTiming()
        {
            var result = await GetListAsync<VehicleTypeTimingDO>("");

            return result?.ToList() ?? new List<VehicleTypeTimingDO>();
        }

        public async Task<bool> Delete(List<string> tidList)
        {
            string sql = @"UPDATE vehicle_type_timing 
SET is_deleted = 1 
WHERE
	tid IN @tidList";

            var para = new DynamicParameters();
            para.Add("@tidList", tidList);

            await OpenConnectionAsync(async conn => { await conn.ExecuteAsync(sql, para); });

            return true;
        }

        public async Task<List<string>> GetEmptyTid()
        {
            List<string> tidList = new List<string>();
            string sql = @"SELECT
	a.tid AS Tid 
FROM
	vehicle_type_timing a
	LEFT JOIN vehicle_type_timing_id_map b ON ( a.tid = b.tid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0 
	AND b.id IS NULL;";

            await OpenSlaveConnectionAsync(async conn =>
            {
                var result = await conn.QueryAsync<VehicleTypeTimingDO>(sql);

                tidList = result.Select(_ => _.Tid).ToList();
            });

            return tidList;
        }
    }
}
