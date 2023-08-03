using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderLogRepository : AbstractRepository<OrderLogDO>, IOrderLogRepository
    {
        public OrderLogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }
    }
}
