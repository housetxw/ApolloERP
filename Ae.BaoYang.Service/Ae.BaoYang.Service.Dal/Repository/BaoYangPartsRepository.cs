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
    public class BaoYangPartsRepository : AbstractRepository<BaoYangPartsDO>, IBaoYangPartsRepository
    {
        public BaoYangPartsRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据tid查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAsync(string tid)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            return await GetListAsync<BaoYangPartsDO>("WHERE tid = @tid AND validated = 1", para);
        }

        /// <summary>
        /// 根据tid 和 partNames 查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAndNamesAsync(string tid,
            List<string> partNames)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@part_name", partNames);
            return await GetListAsync<BaoYangPartsDO>("WHERE tid = @tid AND part_name IN @part_name AND validated = 1",
                para);
        }

        /// <summary>
        /// 根据车型 配件名 查适配产品pid
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartProductDO>> GetBaoYangPartProductsAsync(string tid,
            List<string> partName)
        {
            string sql = @"SELECT
	A.tid AS Tid,
	A.part_name AS PartName,
	A.part_code AS PartCode,
	A.oe_part_code AS OePartCode,
	A.brand AS Brand,
	B.pid AS Pid 
FROM
	`bao_yang_parts` A
	INNER JOIN bao_yang_product_ref B ON ( A.part_code = B.part_code AND B.is_deleted = 0 ) 
WHERE
	A.tid = @tid 
	AND A.part_name IN @part_name 
	AND A.is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@part_name", partName);

            IEnumerable<BaoYangPartProductDO> result = new List<BaoYangPartProductDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<BaoYangPartProductDO>(sql, para);
            });
            return result;
        }

        /// <summary>
        /// 根据配件号查询Parts
        /// </summary>
        /// <param name="partCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByParCode(List<string> partCode)
        {
            var para = new DynamicParameters();
            para.Add("@partCode", partCode);
            return await GetListAsync<BaoYangPartsDO>("WHERE part_code = @partCode", para);
        }

        /// <summary>
        /// 根据Pid查询适配车型tid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SimpleVehicleDo>> GetTidByPid(string pid)
        {
            string sql = @"SELECT DISTINCT
	A.tid 
FROM
	bao_yang_parts A
	INNER JOIN bao_yang_product_ref B ON ( A.part_code = B.part_code AND B.is_deleted = 0 ) 
WHERE
	B.pid = @pid 
	AND A.is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@pid", pid);

            IEnumerable<SimpleVehicleDo> result = new List<SimpleVehicleDo>();

            await OpenSlaveConnectionAsync(
                async conn => { result = await conn.QueryAsync<SimpleVehicleDo>(sql, para); });

            return result;
        }

        /// <summary>
        /// 根据配件号查询适配车型Tid
        /// </summary>
        /// <param name="partCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SimpleVehicleDo>> GetTidByParCode(string partCode)
        {
            string sql = @"SELECT DISTINCT
	A.tid 
FROM
	bao_yang_parts A 
WHERE
	A.part_code = @partCode 
	AND A.is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@partCode", partCode);

            IEnumerable<SimpleVehicleDo> result = new List<SimpleVehicleDo>();

            await OpenSlaveConnectionAsync(
                async conn => { result = await conn.QueryAsync<SimpleVehicleDo>(sql, para); });

            return result;
        }

        /// <summary>
        /// 配件类型 品牌 查五级车型Tid
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SimpleVehicleDo>> GetTidByParCode(List<string> brand, List<string> partName)
        {

            var para = new DynamicParameters();
            para.Add("@partName", partName);
            StringBuilder condition = new StringBuilder();
            if (brand != null && brand.Any())
            {
                condition.Append(" AND brand IN @brand ");
                para.Add("@brand", brand);
            }
            string sql = @"SELECT DISTINCT
	tid 
FROM
	bao_yang_parts 
WHERE
	part_name IN @partName {0}
	AND is_deleted = 0;";

            sql = string.Format(sql, condition);

            IEnumerable<SimpleVehicleDo> result = new List<SimpleVehicleDo>();

            await OpenSlaveConnectionAsync(
                async conn => { result = await conn.QueryAsync<SimpleVehicleDo>(sql, para); });

            return result;
        }

        /// <summary>
        /// 根据tid 和 partName PartCode 查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <param name="partCode"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAndCodeAsync(string tid, string partName,
            string partCode, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@part_name", partName);
            para.Add("@part_code", partCode);
            return await GetListAsync<BaoYangPartsDO>(
                "WHERE tid = @tid AND part_name = @part_name AND part_code = @part_code",
                para, !readOnly);
        }

        /// <summary>
        /// 根据TidList PartNames 查询配件适配
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="partNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangParts(List<string> tidList, List<string> partNames)
        {
            var para = new DynamicParameters();
            para.Add("@tidList", tidList);
            para.Add("@part_name", partNames);
            return await GetListAsync<BaoYangPartsDO>(
                "WHERE tid IN @tidList AND part_name IN @part_name AND validated = 1",
                para);
        }

        public async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByOeCode(string oeCode, List<string> partNames, List<string> brands)
        {
            var para = new DynamicParameters();
            para.Add("@oeCode", oeCode);
            para.Add("@partNames", partNames);
            para.Add("@brands", brands);
            return await GetListAsync<BaoYangPartsDO>(
               "WHERE oe_part_code = @oeCode AND part_name IN @partNames AND brand IN @brands",
               para);
        }
    }
}
