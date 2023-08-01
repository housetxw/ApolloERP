using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPackageConfigRepository : AbstractRepository<BaoYangPackageConfigDO>,
        IBaoYangPackageConfigRepository
    {
        public BaoYangPackageConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取保养Package
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageConfigDO>> GetBaoYangPackageConfigAsync()
        {
            var result = await GetListAsync<BaoYangPackageConfigDO>("");

            return result?.OrderBy(_ => _.DisplayIndex);
        }

        /// <summary>
        /// 获取所有PackageType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPackageConfigDO>> GetAllBaoYangPackageConfigAsync()
        {
            List<BaoYangPackageConfigDO> result = new List<BaoYangPackageConfigDO>();
            string sql = @"SELECT
	id AS Id,
	package_type AS PackageType,
	display_name AS DisplayName,
	suggest_tip AS SuggestTip,
	brief_description AS BriefDescription,
	description_link AS DescriptionLink,
	detail_description AS DetailDescription,
	product_type AS ProductType,
	image_url AS ImageUrl,
	display_index AS DisplayIndex,
	service_id AS ServiceId,
	is_deleted AS IsDeleted,
	create_by AS CreateBy,
	create_time AS CreateTime,
	update_by AS UpdateBy,
	update_time AS UpdateTime 
FROM
	bao_yang_package_config;";

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<BaoYangPackageConfigDO>(sql))?.ToList() ??
                         new List<BaoYangPackageConfigDO>();
            });

            return result;
        }
    }
}
