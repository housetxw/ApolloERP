using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPartAccessoryMapRepository: AbstractRepository<BaoYangPartAccessoryMapDO>,
        IBaoYangPartAccessoryMapRepository
    {
        public BaoYangPartAccessoryMapRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取辅料关联baoYangType配置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartAccessoryMapDO>> GetParAccessoryMapAsync()
        {
            return await GetListAsync<BaoYangPartAccessoryMapDO>("");
        }
    }
}
