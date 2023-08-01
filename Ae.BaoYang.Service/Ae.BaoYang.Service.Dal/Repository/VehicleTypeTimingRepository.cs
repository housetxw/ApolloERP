using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Extend;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class VehicleTypeTimingRepository: AbstractRepository<VehicleTypeTimingDO>,
        IVehicleTypeTimingRepository
    {
        public VehicleTypeTimingRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取所有Tid
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SimpleVehicleDo>> GetAllTid()
        {
            string sql = @"SELECT
	tid AS Tid 
FROM
	vehicle_type_timing 
WHERE
	is_deleted =0";

            IEnumerable<SimpleVehicleDo> result = new List<SimpleVehicleDo>();
            await OpenSlaveConnectionAsync(async conn => { result = await conn.QueryAsync<SimpleVehicleDo>(sql); });
            return result;
        }

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
        public async Task<IEnumerable<VehicleTypeTimingDO>> SearchVehicleTypeTiming(string brand, List<string> vehicleId,
            string paiLiang, string startYear, string endYear, string nian,string factory)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE brand = @brand");
            var para = new DynamicParameters();
            para.Add("@brand", brand);
            if (vehicleId != null && vehicleId.Any())
            {
                condition.Append(" AND vehicle_id IN @vehicleId");
                para.Add("@vehicleId", vehicleId);
            }

            if (!string.IsNullOrEmpty(paiLiang))
            {
                condition.Append(" AND pai_liang = @paiLiang");
                para.Add("@paiLiang", paiLiang);
            }

            if (!string.IsNullOrEmpty(nian))
            {
                condition.Append(" AND nian = @nian");
                para.Add("@nian", nian);
            }

            if (!string.IsNullOrEmpty(startYear))
            {
                condition.Append(" AND listed_year >= @startYear");
                para.Add("@startYear", startYear);
            }

            if (!string.IsNullOrEmpty(endYear))
            {
                condition.Append(" AND stop_production_year <= @endYear");
                para.Add("@endYear", endYear);
            }

            if (!string.IsNullOrEmpty(factory))
            {
                condition.Append(" AND factory = @factory");
                para.Add("@factory", factory);
            }

            var result = await GetListAsync<VehicleTypeTimingDO>(condition.ToString(), para);

            return result;
        }
    }
}
