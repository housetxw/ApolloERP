using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase.Imp
{
    public class PurchaseOrderLogRepository : AbstractRepository<PurchaseOrderLogDO>, IPurchaseOrderLogRepository
    {
        public PurchaseOrderLogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSqlReadOnly");
        }
    }
}
