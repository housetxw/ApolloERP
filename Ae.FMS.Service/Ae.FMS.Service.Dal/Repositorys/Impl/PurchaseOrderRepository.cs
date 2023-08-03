using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using Ae.FMS.Service.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class PurchaseOrderRepository : AbstractRepository<PurchaseOrderDO>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSqlReadOnly");

        }
    }
}
