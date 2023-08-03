using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.HomeCare
{

    public class OrderDispatchRepository : AbstractRepository<OrderDispatchDO>, IOrderDispatchRepository
    {
        public OrderDispatchRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }

    }

}
