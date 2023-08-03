using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IOrderCouponRepository : IRepository<OrderCouponDO>
    {
        Task<OrderCouponDO> GetOrderCoupon(long orderId);

        Task<int> DeleteOrderCoupon(long couponId, string createBy);
    }
}
