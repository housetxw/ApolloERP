using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class PurchaseSettlementDetailRepository : AbstractRepository<PurchaseSettlementDetailDO>, IPurchaseSettlementDetailRepository
    {
        public PurchaseSettlementDetailRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");
        }
    }
}
