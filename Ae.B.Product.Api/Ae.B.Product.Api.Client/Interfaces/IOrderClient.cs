using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Model.Order;
using Ae.B.Product.Api.Client.Request.Order;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IOrderClient
    {
        /// <summary>
        /// 根据优惠券Id查询订单相关信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderCouponDto>> GetOrderCoupons(GetOrderCouponsClientRequest request);
    }
}
