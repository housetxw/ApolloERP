using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public interface IOrderDispatchRepository : IRepository<OrderDispatchDO>
    {
        /// <summary>
        /// 得到派工订单信息
        /// </summary>
        Task<List<OrderDispatchDO>> GetOrderDispatch(GetOrderDispatchRequest request);

        Task<bool> DeleteOrderDispatch(UpdateOrderDispatchRequest request);
    }
}
