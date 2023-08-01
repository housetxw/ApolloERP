using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Renci.SshNet;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPackageMapConfigRepository: AbstractRepository<BaoYangPackageMapConfigDO>,
        IBaoYangPackageMapConfigRepository
    {
        public BaoYangPackageMapConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取packageType 关联 baoyangType
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageMapConfigDO>> GetPackageMapConfigAsync()
        {
            var result = await GetListAsync<BaoYangPackageMapConfigDO>("");
            return result;
        }

        /// <summary>
        /// 删除PackageMapConfig
        /// </summary>
        /// <param name="packageType"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> DeletePackageMapConfig(string packageType, string submitBy)
        {
            string sql = @"UPDATE bao_yang_package_map_config 
SET is_deleted = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	package_type = @packageType 
	AND is_deleted = 0;";

            int result = 0;

            var para = new DynamicParameters();
            para.Add("@packageType", packageType);
            para.Add("@updateBy", submitBy);

            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });

            return result;
        }
    }
}
