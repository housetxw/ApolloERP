using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangAntifreezeSettingRepository : AbstractRepository<BaoYangAntifreezeSettingDO>,
        IBaoYangAntifreezeSettingRepository
    {
        public BaoYangAntifreezeSettingRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<IEnumerable<BaoYangAntifreezeSettingDO>> GetBaoYangAntifreezeSettingAsync()
        {
            return await GetListAsync<BaoYangAntifreezeSettingDO>("");
        }
    }
}
