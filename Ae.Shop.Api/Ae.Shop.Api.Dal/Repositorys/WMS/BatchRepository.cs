using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.WMS
{
    public class BatchRepository : AbstractRepository<BatchDO>, IBatchRepository
    {
        public BatchRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("WMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("WMSSqlReadOnly");
        }
    }

}
