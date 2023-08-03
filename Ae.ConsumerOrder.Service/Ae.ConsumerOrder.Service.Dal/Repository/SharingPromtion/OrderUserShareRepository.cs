using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion
{
    public class OrderUserShareRepository : AbstractRepository<OrderUserShareDO>,IOrderUserShareRepository
    {
        public OrderUserShareRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderUserShareDO> GetOrderUserShare(string userId)
        {
            var list = await GetListAsync("where user_id=@UserId", new { UserId = userId }, true);
            return list?.FirstOrDefault();
        }

        public async Task<long> UpdateOrderUserShareNum(string userId,int shareSumNum, int exchangeNum,int exchangeCouponNum )
        {
            string sql = @"UPDATE order_user_share 
                            SET share_valid_sum_order_num = share_valid_sum_order_num + @ShareSumNum,exchange_sum_order_num=exchange_sum_order_num+@ExchangeNum,exchanged_coupon_num=exchanged_coupon_num+@ExchangeCouponNum
                                
                            WHERE
                                user_id = @UserId and is_deleted=0";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { UserId = userId, ShareSumNum= shareSumNum, ExchangeNum=exchangeNum, ExchangeCouponNum = exchangeCouponNum });
            });
            return count;
        }
    }
}
