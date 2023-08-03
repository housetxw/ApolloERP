using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class OccupyStockLogRepository : AbstractRepository<OccupyStockLogDO>, IOccupyStockLogRepository
    {
        public OccupyStockLogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSqlReadOnly");
        }
    }
}
