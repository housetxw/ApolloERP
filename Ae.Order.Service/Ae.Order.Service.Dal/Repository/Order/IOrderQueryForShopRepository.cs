using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public interface IOrderQueryForShopRepository :  IRepository<OrderDO>
    {
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request);
    }
}
