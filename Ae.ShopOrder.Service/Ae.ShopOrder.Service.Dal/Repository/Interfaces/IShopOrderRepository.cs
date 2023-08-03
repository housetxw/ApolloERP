using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IShopOrderRepository: IRepository<OrderDO>
    {
        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderNo"></param>
        /// <param name="useMaster"></param>
        /// <returns></returns>
        Task<OrderDO> GetOrder(string orderNo, bool useMaster = false);

        /// <summary>
        /// 更新订单逆向信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reverseStatus"></param>
        /// <param name="refundStatus"></param>
        /// <param name="updateBy"></param>
        /// <param name="isOccurReverse">默认是</param>
        /// <returns></returns>
        Task<bool> UpdateOrderReverseInfo(long orderId, sbyte reverseStatus, sbyte refundStatus, string updateBy, sbyte isOccurReverse = 1);

        ///  更新订单主状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderMainStatus(long orderId, sbyte orderStatus, string updateBy);
    }
}
