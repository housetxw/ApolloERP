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
    public class OrderDiscountRepository : AbstractRepository<OrderDiscountDO>, IOrderDiscountRepository
    {
        public OrderDiscountRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        /// <summary>
        /// 得到订单折扣金额
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<OrderDiscountDO> GetOrderDiscount(string orderNo)
        {
            var list = await GetListAsync("where order_no=@OrderNo", new { OrderNo = orderNo });
            return list?.FirstOrDefault();
        }
    }
}
