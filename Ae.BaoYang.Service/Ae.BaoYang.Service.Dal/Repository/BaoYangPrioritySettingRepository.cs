using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPrioritySettingRepository : AbstractRepository<BaoYangPrioritySettingDO>,
        IBaoYangPrioritySettingRepository
    {
        public BaoYangPrioritySettingRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }
    }
}
