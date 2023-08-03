using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase.Imp
{

    public class VenderRepository : AbstractRepository<VenderDO>, IVenderRepository
    {
        public VenderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSqlReadOnly");
        }
    }
}
