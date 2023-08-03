using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderSaleRepository : AbstractRepository<OrderSaleDO>, IOrderSaleRepository
    {
        public OrderSaleRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }

    }
}
