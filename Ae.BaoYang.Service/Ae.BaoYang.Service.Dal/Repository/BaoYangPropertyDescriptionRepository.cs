using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPropertyDescriptionRepository : AbstractRepository<BaoYangPropertyDescriptionDO>,
        IBaoYangPropertyDescriptionRepository
    {
        public BaoYangPropertyDescriptionRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取五级属性描述
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPropertyDescriptionDO>> GetBaoYangPropertyDescription()
        {
            var result = await GetListAsync<BaoYangPropertyDescriptionDO>("");

            return result;
        }
    }
}
