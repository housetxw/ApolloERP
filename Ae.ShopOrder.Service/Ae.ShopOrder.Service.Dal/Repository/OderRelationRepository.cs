using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OderRelationRepository: AbstractRepository<OderRelationDO>, IOrderRelationRepository
    {
        public OderRelationRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }
    }
}
