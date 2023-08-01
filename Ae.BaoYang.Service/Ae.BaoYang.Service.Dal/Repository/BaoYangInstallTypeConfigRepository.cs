using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangInstallTypeConfigRepository : AbstractRepository<BaoYangInstallTypeConfigDO>,
        IBaoYangInstallTypeConfigRepository
    {
        public BaoYangInstallTypeConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 保养安装方式配置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangInstallTypeConfigDO>> GetBaoYangInstallTypeConfigAsync()
        {
            return await GetListAsync<BaoYangInstallTypeConfigDO>("");
        }
    }
}
