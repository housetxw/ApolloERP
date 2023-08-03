using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ReserveTrackDetailRepository : AbstractRepository<ReserveTrackDetailDO>, IReserveTrackDetailRepository
    {
        public ReserveTrackDetailRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }
    }
}
