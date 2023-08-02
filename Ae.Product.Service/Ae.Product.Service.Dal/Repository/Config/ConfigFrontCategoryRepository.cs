using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public class ConfigFrontCategoryRepository : AbstractRepository<ConfigFrontCategoryDo>,
        IConfigFrontCategoryRepository
    {
        public ConfigFrontCategoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");

        }

        public async Task<List<ConfigFrontCategoryDo>> GetConfigFrontCategoryByParentId(string terminalType,
            int parentId)
        {
            var para = new DynamicParameters();
            para.Add("@terminalType", terminalType);
            para.Add("@parentId", parentId);

            var result = await GetListAsync("WHERE terminal_type = @terminalType AND parent_id = @parentId", para);

            return result?.OrderBy(_ => _.Rank)?.ToList() ?? new List<ConfigFrontCategoryDo>();
        }
    }
}
