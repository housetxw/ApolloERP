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
    public class ConfigFrontCategoryProductRepository : AbstractRepository<ConfigFrontCategoryProductDo>,
        IConfigFrontCategoryProductRepository
    {
        public ConfigFrontCategoryProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取类目产品配置关系
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ConfigFrontCategoryProductDo>> GetConfigFrontCategoryProductByCategoryId(long categoryId)
        {
            var para = new DynamicParameters();
            para.Add("@categoryId", categoryId);
            var result = await GetListAsync<ConfigFrontCategoryProductDo>("WHERE category_id = @categoryId", para);
            return result?.ToList() ?? new List<ConfigFrontCategoryProductDo>();
        }
    }
}
