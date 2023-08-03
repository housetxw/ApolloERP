using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public interface IOrderForBossRepository : IRepository<OrderDO>
    {
        Task<PagedEntity<GetOrderListForBossDO>> GetOrderListForBoss(GetOrderListForBossRequest request);
    }
}
