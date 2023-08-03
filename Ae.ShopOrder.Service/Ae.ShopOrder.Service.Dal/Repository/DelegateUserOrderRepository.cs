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
    public class DelegateUserOrderRepository: AbstractRepository<DelegateUserOrderDO>,IDelegateUserOrderRepository
    {
        public DelegateUserOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<bool> UpdateOrderRefInfo(List<string> orderNos, string refOrderNo, string updateBy)
        {
            string sql = @"UPDATE delegate_user_order 
                            SET ref_order_no = @RefOrderNo,update_by=@UpdateBy,update_time=NOW(3)
                            WHERE
                                order_no in @OrderNos";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { RefOrderNo = refOrderNo, UpdateBy = updateBy, OrderNos=orderNos });
            });
            return count > 0;
        }

        public async Task<bool> ClearOrderRefInfo(string refOrderNo, string updateBy)
        {
            string sql = @"UPDATE delegate_user_order 
                            SET ref_order_no ='',update_by=@UpdateBy,update_time=NOW(3)
                            WHERE
                                ref_order_no = @OrderNos";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNos = refOrderNo, UpdateBy = updateBy });
            });
            return count > 0;
        }

     
    }
}
