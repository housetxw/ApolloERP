using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangConfigRepository: AbstractRepository<BaoYangConfigDO>, IBaoYangConfigRepository
    {
        public BaoYangConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取保养Xml配置
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public async Task<string> FetchBaoYangConfigByKeyAsync(string configName)
        {
            string sql = @"SELECT
	`config` 
FROM
	bao_yang_config 
WHERE
	config_name = @config_name
	AND is_deleted =0;";

            var para = new DynamicParameters();
            para.Add("@config_name", configName);

            string config = string.Empty;
            await OpenSlaveConnectionAsync(async conn =>
            {
                config = await conn.ExecuteScalarAsync<string>(sql, para);
            });
            return config;
        }

        /// <summary>
        /// 保养配件配置查询
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="specialPart"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsAdaptationDO>> GetBaoYangPartAdaptationsAsync(List<string> tidList,
            List<string> specialPart)
        {

            string sql = @"SELECT
	A.tid AS Tid,
	A.id AS Id,
	A.part_name AS PartName,
	A.oe_part_code AS OePartCode,
	A.part_code AS PartCode,
	A.brand AS Brand,
	C.product_code AS Pid,
	C.on_sale AS OnSale,
	C.stockout AS StockOut 
FROM
	bao_yang_parts A
	LEFT JOIN bao_yang_product_ref B ON ( A.part_code = B.part_code AND B.is_deleted = 0 )
	LEFT JOIN fct_product C ON ( B.pid = C.product_code AND C.is_deleted = 0 ) 
WHERE
	A.tid IN @TidList 
	AND A.is_deleted = 0 
	AND A.part_name NOT IN @SpecialPart UNION
SELECT
	A.tid AS Tid,
	A.id AS Id,
	A.part_name AS PartName,
	A.oe_part_code AS OePartCode,
	A.part_code AS PartCode,
	A.brand AS Brand,
	C.product_code AS Pid,
	C.on_sale AS OnSale,
	C.stockout AS StockOut 
FROM
	bao_yang_parts A
	LEFT JOIN fct_product C ON ( A.part_code = C.part_no AND C.is_deleted = 0 ) 
WHERE
	A.tid IN @TidList 
	AND A.is_deleted = 0 
	AND A.part_name IN @SpecialPart;";

            var para = new DynamicParameters();
            para.Add("@TidList", tidList);
            para.Add("@SpecialPart", specialPart);
            IEnumerable<BaoYangPartsAdaptationDO> resut = new List<BaoYangPartsAdaptationDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                resut = await conn.QueryAsync<BaoYangPartsAdaptationDO>(sql, para);
            });
            return resut;
        }

        /// <summary>
        /// 添加partCode
        /// </summary>
        /// <param name="baoYangPartsDo"></param>
        /// <returns></returns>
        public async Task<int> InsertBaoYangPartCodeAsync(BaoYangPartsDO baoYangPartsDo)
        {
            string sql =
                @"INSERT bao_yang_parts ( tid, part_name, part_code, oe_part_code, source, brand, validated, validated_by, validated_time, create_by ) SELECT
@tid,
@part_name,
@part_code,
@oe_part_code,
@source,
@brand,
1,
@create_by,
NOW( ),
@create_by 
FROM
DUAL 
WHERE
	NOT EXISTS ( SELECT id FROM bao_yang_parts WHERE tid = @tid AND part_name = @part_name AND part_code = @part_code AND is_deleted = 0 );";

            var para = new DynamicParameters();
            para.Add("@tid", baoYangPartsDo.Tid);
            para.Add("@part_name", baoYangPartsDo.PartName);
            para.Add("@part_code", baoYangPartsDo.PartCode);
            para.Add("@oe_part_code", baoYangPartsDo.OePartCode ?? String.Empty);
            para.Add("@source", baoYangPartsDo.Source);
            para.Add("@brand", baoYangPartsDo.Brand ?? String.Empty);
            para.Add("@create_by", baoYangPartsDo.CreateBy);
            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id;
        }

        /// <summary>
        /// 根据id删除配件
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeByIdAsync(string tid, int id, string updatedBy)
        {
            string sql = @"UPDATE bao_yang_parts 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	id = @id 
	AND tid = @tid;";
            var para = new DynamicParameters();
            para.Add("@id", id);
            para.Add("@update_by", updatedBy);
            para.Add("@tid", tid);

            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }

        /// <summary>
        /// 根据OE件号删除配件
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <param name="oePartCode"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeByOeCodeAsync(string tid, string partName, string oePartCode, string updatedBy)
        {
            string sql = @"UPDATE bao_yang_parts 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	tid = @tid 
	AND part_name = @part_name 
	AND oe_part_code = @oe_part_code;";
            var para = new DynamicParameters();
            para.Add("@oe_part_code", oePartCode);
            para.Add("@update_by", updatedBy);
            para.Add("@tid", tid);
            para.Add("@part_name", partName);

            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }

        /// <summary>
        /// 根据PartNames删除配件号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeByPartNamesAsync(string tid, List<string> partNames, string updatedBy)
        {
            string sql = @"UPDATE bao_yang_parts 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	tid = @tid 
	AND part_name IN @part_names;";
            var para = new DynamicParameters();
            para.Add("@update_by", updatedBy);
            para.Add("@tid", tid);
            para.Add("@part_names", partNames);

            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <param name="originalOePart"></param>
        /// <param name="oePartCode"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOePartCodeAsync(string tid, List<string> partNames, string originalOePart,
            string oePartCode, string updatedBy)
        {
            string sql = @"UPDATE bao_yang_parts 
SET oe_part_code = @oe_part_code,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	tid = @tid 
	AND oe_part_code = @originalOePart 
	AND is_deleted = 0 
	AND part_name IN @part_names;";
            var para = new DynamicParameters();
            para.Add("@update_by", updatedBy);
            para.Add("@tid", tid);
            para.Add("@part_names", partNames);
            para.Add("@oe_part_code", oePartCode ?? String.Empty);
            para.Add("@originalOePart", originalOePart ?? String.Empty);
            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="id"></param>
        /// <param name="partCode"></param>
        /// <param name="brand"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePartCodeAsync(string tid, int id, string partCode, string brand, string updatedBy)
        {
            var para = new DynamicParameters();
            string condition = string.Empty;
            if (brand != null)
            {
                condition = "brand = @brand,";
                para.Add("@brand", brand);
            }

            para.Add("@update_by", updatedBy);
            para.Add("@tid", tid);
            para.Add("@id", id);
            para.Add("@part_code", partCode);

            string sql = @"UPDATE bao_yang_parts 
SET part_code = @part_code,{0}
update_by = @update_by,
update_time = NOW( ) 
WHERE
	id = @id 
	AND tid = @tid;";
            sql = string.Format(sql, condition);

            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }
    }
}
