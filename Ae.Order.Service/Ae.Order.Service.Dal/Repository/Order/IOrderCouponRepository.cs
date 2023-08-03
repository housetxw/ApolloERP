using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public interface IOrderCouponRepository : IRepository<OrderCouponDO>
    {
        Task<List<OrderCouponDTO>> GetOrderCoupons(GetOrderCouponsRequest request);
    }
}
