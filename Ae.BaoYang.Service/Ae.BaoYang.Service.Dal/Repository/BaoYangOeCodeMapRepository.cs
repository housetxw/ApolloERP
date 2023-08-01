using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangOeCodeMapRepository : AbstractRepository<BaoYangOeCodeMapDO>, IBaoYangOeCodeMapRepository
    {
        public BaoYangOeCodeMapRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据oe号查询零件号
        /// </summary>
        /// <param name="oeCode"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangOeCodeMapDO>> GetPartCodeByOe(string oeCode, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@oeCode", oeCode);
            return await GetListAsync<BaoYangOeCodeMapDO>("WHERE oe_part_code = @oeCode", para, !readOnly);
        }

        /// <summary>
        /// Oe映射查询
        /// </summary>
        /// <param name="oeCode"></param>
        /// <param name="partCode"></param>
        /// <param name="partType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Tuple<List<BaoYangOeCodeMapDO>, int>> GetOeCodeMap(string oeCode, string partCode,
            string partType, int pageIndex, int pageSize)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE is_deleted = 0");
            para.Add("@startIndex", (pageIndex - 1) * pageSize);
            para.Add("@pageSize", pageSize);
            if (!string.IsNullOrEmpty(oeCode))
            {
                condition.Append(" AND oe_part_code = @oeCode");
                para.Add("@oeCode", oeCode);
            }

            if (!string.IsNullOrEmpty(partCode))
            {
                condition.Append(" AND part_code = @partCode");
                para.Add("@partCode", partCode);
            }

            if (!string.IsNullOrEmpty(partType))
            {
                condition.Append(" AND part_name = @partType");
                para.Add("@partType", partType);
            }

            string sql = @"SELECT
	oe_part_code AS OePartCode,
	part_name AS PartName,
	vehicle_brand AS VehicleBrand,
	GROUP_CONCAT( part_code SEPARATOR ';' ) AS PartCode 
FROM
	bao_yang_oe_code_map {0}	 
GROUP BY
	oe_part_code 
ORDER BY
	id DESC 
	LIMIT @startIndex,
	@pageSize;";
            sql = string.Format(sql, condition.ToString());

            string sqlCount = @"SELECT
	COUNT( DISTINCT oe_part_code ) 
FROM
	bao_yang_oe_code_map {0}";

            sqlCount = string.Format(sqlCount, condition.ToString());

            int totalCount = 0;
            List<BaoYangOeCodeMapDO> result = new List<BaoYangOeCodeMapDO>();
            List<Task> tasks = new List<Task>();
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<BaoYangOeCodeMapDO>(sql, para))?.AsList() ??
                         new List<BaoYangOeCodeMapDO>();
            }));
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new Tuple<List<BaoYangOeCodeMapDO>, int>(result, totalCount);
        }

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="oePartCode"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> DeleteOePartCode(string oePartCode, string submitBy)
        {
            string sql = @"UPDATE bao_yang_oe_code_map 
SET is_deleted = 1,
update_by = @updatedBy,
update_time = NOW( ) 
WHERE
	oe_part_code = @oePartCode 
	AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@oePartCode", oePartCode);
            para.Add("@updatedBy", submitBy);

            int result = 0;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }
    }
}
