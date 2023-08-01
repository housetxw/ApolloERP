using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPartsMapConfigRepository : AbstractRepository<BaoYangPartsMapConfigDO>,
        IBaoYangPartsMapConfigRepository
    {
        public BaoYangPartsMapConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 配件名关联BaoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartsMapConfigDO>> GetBaoYangPartsMapAsync()
        {
            var result = await GetListAsync<BaoYangPartsMapConfigDO>("");

            return result;
        }
    }
}
