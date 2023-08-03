using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderCouponRepository : AbstractRepository<OrderCouponDO>, IOrderCouponRepository
    {
        public OrderCouponRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderCouponDO> GetOrderCoupon(long orderId)
        {
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }

        public async Task<int> DeleteOrderCoupon(long couponId, string createBy)
        {
            int id = 0;

            string sql = "Update order_coupon set is_deleted=1,update_by=@UpdateBy,update_time=NOW(3) where user_coupon_id=@CouponId";
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateBy", createBy);
            parameters.Add("@CouponId", couponId);
            await OpenSlaveConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, parameters); });
            return id;
        }
    }
}
