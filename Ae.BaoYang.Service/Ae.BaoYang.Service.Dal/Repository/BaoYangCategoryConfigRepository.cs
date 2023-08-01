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
    public class BaoYangCategoryConfigRepository: AbstractRepository<BaoYangCategoryConfigDO>, IBaoYangCategoryConfigRepository
    {
        public BaoYangCategoryConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取保养一级分类
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangCategoryConfigDO>> GetBaoYangCategoryConfigAsync()
        {
            var result = await GetListAsync<BaoYangCategoryConfigDO>("");

            return result?.OrderBy(_ => _.DisplayIndex);
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <param name="categoryAlias"></param>
        /// <returns></returns>
        public async Task<BaoYangCategoryConfigDO> GetBaoYangCategoryConfigByAlias(string categoryAlias)
        {
            var para = new DynamicParameters();
            para.Add("@categoryAlias", categoryAlias);
            var result = await GetListAsync<BaoYangCategoryConfigDO>("WHERE category_alias = @categoryAlias", para);
            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <param name="packageType"></param>
        /// <returns></returns>
        public async Task<List<BaoYangCategoryConfigDO>> GetBaoYangCategoryConfigByPackageType(string packageType)
        {
            var para = new DynamicParameters();
            para.Add("@packageType", packageType);
            var result = await GetListAsync<BaoYangCategoryConfigDO>("WHERE package_type = @packageType", para);
            return result?.ToList() ?? new List<BaoYangCategoryConfigDO>();
        }
    }
}
