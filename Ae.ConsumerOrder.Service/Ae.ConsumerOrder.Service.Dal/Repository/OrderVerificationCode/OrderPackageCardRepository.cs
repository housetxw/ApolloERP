using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model.OrderVerificationCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.OrderVerificationCode
{
    public class OrderPackageCardRepository : AbstractRepository<OrderPackageCardDO>, IOrderPackageCardRepository
    {
        public OrderPackageCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<bool> ActiveByOrderNo(string orderNo)
        {

            var sql = "update order_package_card set is_deleted=0 where source_order_no=@OrderNo";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNo = orderNo });
            });
            return count > 0;

        }
    }
}
