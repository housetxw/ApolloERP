using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopReceiveCheckLogRepository: AbstractRepository<ShopReceiveCheckLogDo>, IShopReceiveCheckLogRepository
    {
        public ShopReceiveCheckLogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 检查报告日志查询
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckLogDo>> GetShopReceiveCheckLog(long checkId, int categoryId)
        {

            var para = new DynamicParameters();
            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);

            var result = await GetListAsync<ShopReceiveCheckLogDo>("WHERE check_id = @checkId AND category_id = @categoryId", para);

            return result?.OrderByDescending(_ => _.Id)?.ToList() ?? new List<ShopReceiveCheckLogDo>();
        }

        /// <summary>
        /// 批量查询检查报告日志
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckLogDo>> GetReceiveCheckLogByCheckIds(List<long> checkIds, int categoryId)
        {
            var para = new DynamicParameters();
            para.Add("@checkIds", checkIds);
            para.Add("@categoryId", categoryId);

            var result = await GetListAsync<ShopReceiveCheckLogDo>("WHERE check_id IN @checkIds AND category_id = @categoryId", para);

            return result?.ToList() ?? new List<ShopReceiveCheckLogDo>();
        }

        /// <summary>
        /// 批量删除上次检查结果数据
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> BatchDeleteLastData(long checkId, int categoryId, string checkModule, string submitBy)
        {
            var para = new DynamicParameters();
            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);
            para.Add("@checkModule", checkModule);
            para.Add("@submitBy", submitBy);

            string sql = @"UPDATE shop_check_result A 
SET A.is_deleted = 1,
A.update_time = NOW( ),
A.update_by = @submitBy 
WHERE
	A.category_id = @categoryId 
	AND A.submit_batch_id IN (
SELECT
	B.id 
FROM
	shop_receive_check_log B 
WHERE
	B.check_id = @checkId 
	AND B.check_module_code = @checkModule 
	AND B.category_id = @categoryId 
	AND B.is_deleted = 0 
	);
UPDATE shop_check_result_image A 
SET A.is_deleted = 1,
A.update_time = NOW( ),
A.update_by = @submitBy 
WHERE
	A.category_id = @categoryId 
	AND A.submit_batch_id IN (
SELECT
	B.id 
FROM
	shop_receive_check_log B 
WHERE
	B.check_id = @checkId 
	AND B.check_module_code = @checkModule 
	AND B.category_id = @categoryId 
	AND B.is_deleted = 0 
	);
UPDATE shop_receive_check_log 
SET is_deleted = 1,
update_time = NOW( ),
update_by = @submitBy 
WHERE
	check_id = @checkId 
	AND ( check_module_code = @checkModule OR check_module_code = 'CustomerSignature' ) 
	AND category_id = @categoryId 
    AND is_deleted = 0; ";

            int count = 0;
            await OpenConnectionAsync(async conn => { count = await conn.ExecuteAsync(sql, para); });
            return count;
        }

        /// <summary>
        /// 批量删除上次检查结果数据
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> BatchDeleteLastSignatureData(long checkId, int categoryId, string checkModule, string submitBy)
        {
            var para = new DynamicParameters();
            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);
            para.Add("@checkModule", checkModule);
            para.Add("@submitBy", submitBy);

            string sql = @"
UPDATE shop_receive_check_log 
SET is_deleted = 1,
update_time = NOW( ),
update_by = @submitBy 
WHERE
	check_id = @checkId 
	AND check_module_code = @checkModule 
	AND category_id = @categoryId 
    AND is_deleted = 0; ";

            int count = 0;
            await OpenConnectionAsync(async conn => { count = await conn.ExecuteAsync(sql, para); });
            return count;
        }

        /// <summary>
        /// 批量删除上次检查结果数据（升级项目数据）
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> BatchDeleteLastUpgradeData(long checkId, int categoryId, string checkModule, string submitBy)
        {
            var para = new DynamicParameters();
            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);
            para.Add("@checkModule", checkModule);
            para.Add("@submitBy", submitBy);

            string sql = @"UPDATE shop_check_result A 
SET A.is_deleted = 1,
A.update_time = NOW( ),
A.update_by = @submitBy 
WHERE
	A.category_id = @categoryId 
	AND A.submit_batch_id IN (
SELECT
	B.id 
FROM
	shop_receive_check_log B 
WHERE
	B.check_id = @checkId 
	AND B.check_module_code = @checkModule 
	AND B.category_id = @categoryId 
	AND B.is_deleted = 0 
	);
UPDATE shop_receive_check_result_word A 
SET A.is_deleted = 1,
A.update_time = NOW( ),
A.update_by = @submitBy 
WHERE
	A.category_id = @categoryId 
	AND A.submit_batch_id IN (
SELECT
	B.id 
FROM
	shop_receive_check_log B 
WHERE
	B.check_id = @checkId 
	AND B.check_module_code = @checkModule 
	AND B.category_id = @categoryId 
	AND B.is_deleted = 0 
	);
UPDATE shop_check_result_image A 
SET A.is_deleted = 1,
A.update_time = NOW( ),
A.update_by = @submitBy 
WHERE
	A.category_id = @categoryId 
	AND A.submit_batch_id IN (
SELECT
	B.id 
FROM
	shop_receive_check_log B 
WHERE
	B.check_id = @checkId 
	AND B.check_module_code = @checkModule 
	AND B.category_id = @categoryId 
	AND B.is_deleted = 0 
	);
UPDATE shop_receive_check_log 
SET is_deleted = 1,
update_time = NOW( ),
update_by = @submitBy 
WHERE
	check_id = @checkId 
	AND ( check_module_code = @checkModule OR check_module_code = 'CustomerSignature' ) 
	AND category_id = @categoryId 
	AND is_deleted = 0;";

            int count = 0;
            await OpenConnectionAsync(async conn => { count = await conn.ExecuteAsync(sql, para); });
            return count;
        }
    }
}
