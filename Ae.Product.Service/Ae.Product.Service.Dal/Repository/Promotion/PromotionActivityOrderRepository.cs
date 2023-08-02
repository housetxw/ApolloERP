using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public class PromotionActivityOrderRepository : AbstractRepository<PromotionActivityOrderDo>, IPromotionActivityOrderRepository
    {
        public PromotionActivityOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 订单号查询商品活动
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityOrderDo>> GetPromotionActivityOrderByOrderNos(List<string> orderNos)
        {
            var para = new DynamicParameters();
            para.Add("@orderNos", orderNos);

            var result = await GetListAsync<PromotionActivityOrderDo>("WHERE `order_no` IN @orderNos", para);

            return result?.AsList() ?? new List<PromotionActivityOrderDo>();
        }
    }
}
