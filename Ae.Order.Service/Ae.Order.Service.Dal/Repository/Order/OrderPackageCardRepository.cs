using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
namespace Ae.Order.Service.Dal.Repository.Order
{

    public class OrderPackageCardRepository : AbstractRepository<OrderPackageCardDO>, IOrderPackageCardRepository
    {
        public OrderPackageCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }
    }
}
