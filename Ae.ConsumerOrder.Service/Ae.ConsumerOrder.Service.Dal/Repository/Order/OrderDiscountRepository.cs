using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Data.DapperExtensions.Data;
using Ae.ConsumerOrder.Service.Dal.Model.Order;

namespace Ae.ConsumerOrder.Service.Dal.Repository.Order
{
    public class OrderDiscountRepository : AbstractRepository<OrderDiscountDO>,IOrderDiscountRepository
    {
        public OrderDiscountRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }
    }
}
