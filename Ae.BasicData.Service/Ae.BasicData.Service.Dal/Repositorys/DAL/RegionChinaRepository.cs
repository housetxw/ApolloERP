using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Log;
using ApolloErp.Data.DapperExtensions;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;

namespace Ae.BasicData.Service.Dal.Repositorys.DAL
{
    public class RegionChinaRepository : AbstractRepository<RegionChinaDO>, IRegionChinaRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<RegionChinaRepository> logger;

        public RegionChinaRepository(ApolloErpLogger<RegionChinaRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<RegionChinaDO>> GetAllRegionChinaList()
        {
            IEnumerable<RegionChinaDO> res = new List<RegionChinaDO>();
            var sql = @"SELECT region_id regionId, name, parent_id parentId, country_code countryCode, initial, spell, `level`, 
                        is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM region_china
                        ORDER BY region_id ASC;";
            await OpenSlaveConnectionAsync(async conn =>
            {
                res = await conn.QueryAsync<RegionChinaDO>(sql);
            });

            return res.ToList();

            ////var sql = "WHERE is_deleted = 0 ORDER BY region_id ASC";
            //var sql = "WHERE is_deleted = 0";
            //var res = await GetListAsync(sql);
            //return res.ToList();
        }

        public async Task<List<RegionChinaDO>> GetRegionChinaListByRegionId(RegionChinaReqByRegionIdDTO req)
        {
            //var whereClause = "WHERE is_deleted = 0 ORDER BY region_id ASC";
            if (string.IsNullOrWhiteSpace(req?.RegionId)) return new List<RegionChinaDO>();

            var condition = " AND parent_id = @parentId";
            var param = new DynamicParameters();
            param.Add("@parentId", req.RegionId);
            var whereClause = " WHERE is_deleted = 0";
            var res = await GetListAsync(whereClause + condition, param);
            return res.ToList();
        }

        public async Task<List<RegionChinaDO>> GetRegionChinaListByLevel(RegionChinaReqByLevelDTO req)
        {
            if (req?.Level <= 0) return new List<RegionChinaDO>();

            var condition = " AND level = @level";
            var param = new DynamicParameters();
            param.Add("@level", req?.Level);
            var whereClause = " WHERE is_deleted = 0";
            var res = await GetListAsync(whereClause + condition, param);
            return res.ToList();
        }

        /// <summary>
        /// 根据区ID逆推省市ID
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public async Task<RegionIdDO> GetConverseRegion(string districtId)
        {
            var sql = @"SELECT
	                        d.region_id DistrictId,
	                        d.parent_id CityId,
	                        c.parent_id ProvinceId 
                        FROM
	                        region_china d
	                        LEFT JOIN region_china c ON d.parent_id = c.region_id 
                        WHERE
	                        d.region_id = @DistrictId and d.is_deleted = 0";
            var param = new DynamicParameters();
            param.Add("@DistrictId", districtId);

            var regionDo = new RegionIdDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                regionDo = await conn.QueryFirstAsync<RegionIdDO>(sql, param);
            });
            return regionDo;
        }

        public async Task<RegionChinaDO> GetRegionChinaByRegionId(string regionId)
        {
            var param = new DynamicParameters();
            param.Add("@regionId", regionId);
            var result = await GetListAsync<RegionChinaDO>("WHERE region_id = @regionId", param);

            return result?.FirstOrDefault();
        }

        // ---------------------------------- 私有方法 --------------------------------------

    }
}
