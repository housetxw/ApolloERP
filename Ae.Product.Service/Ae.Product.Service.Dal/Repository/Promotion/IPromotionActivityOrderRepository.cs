using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public interface IPromotionActivityOrderRepository: IRepository<PromotionActivityOrderDo>
    {
        /// <summary>
        /// 订单号查询商品活动
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        Task<List<PromotionActivityOrderDo>> GetPromotionActivityOrderByOrderNos(List<string> orderNos);
    }
}
