using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class TireCategoryConfigRepository: AbstractRepository<TireCategoryConfigDO>,
        ITireCategoryConfigRepository
    {
        public TireCategoryConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 轮胎配置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TireCategoryConfigDO>> GetTireCategoryConfig()
        {
            return await GetListAsync<TireCategoryConfigDO>("");
        }
    }
}
