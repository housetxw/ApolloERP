using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangTypeConfigRepository : AbstractRepository<BaoYangTypeConfigDO>,
        IBaoYangTypeConfigRepository
    {
        public BaoYangTypeConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取保养type
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangTypeConfigDO>> GetBaoYangTypeConfigAsync()
        {
            return await GetListAsync<BaoYangTypeConfigDO>("");
        }

        /// <summary>
        /// 获取所有baoYangTypeConfig
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigDO>> GetAllBaoYangTypeConfigAsync()
        {
            List<BaoYangTypeConfigDO> result = new List<BaoYangTypeConfigDO>();

            string sql = @"SELECT
	id AS Id,
	bao_yang_type AS BaoYangType,
	display_name AS DisplayName,
	category_name AS CategoryName,
	child_categories AS ChildCategories,
	type_group AS TypeGroup,
	image_url AS ImageUrl,
	is_deleted AS IsDeleted,
	create_by AS CreateBy,
	create_time AS CreateTime,
	update_by AS UpdateBy,
	update_time AS UpdateTime 
FROM
	bao_yang_type_config;";

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<BaoYangTypeConfigDO>(sql))?.ToList() ??
                         new List<BaoYangTypeConfigDO>();
            });

            return result;
        }
    }
}
