using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public interface IOrderRepository : IRepository<OrderDO>
    {
        /// <summary>
        /// 根据用户ID和订单号获取订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderNo"></param>
        /// <param name="useMaster"></param>
        /// <returns></returns>
        Task<OrderDO> GetOrder(string userId, string orderNo, bool useMaster = false);

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderNo"></param>
        /// <param name="useMaster"></param>
        /// <returns></returns>
        Task<OrderDO> GetOrder(string orderNo, bool useMaster = false);

        Task<List<OrderDO>> GetOrders(List<string> orderNos);

        /// <summary>
        /// 更新订单号和安装码
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="installCode"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderNoAndInstallCode(long orderId, string orderNo, string installCode);

        /// <summary>
        ///  更新订单的签收状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderSignStatus(string orderNo, string updateBy);

        /// <summary>
        /// 更新订单的安装状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderInstallStatus(string orderNo, string updateBy);



        ///  更新订单主状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderMainStatus(long orderId, sbyte orderStatus, string updateBy);

        /// <summary>
        /// 更新订单库存状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="stockStatus"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderStockStatus(long orderId, sbyte stockStatus, string updateBy);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="payStatus"></param>
        /// <param name="payTime"></param>
        /// <param name="updateBy"></param>
        /// <param name="payMethod"></param>
        /// <param name="payChannel"></param>
        /// <returns></returns>
        Task<bool> UpdatePayStatus(long orderId, sbyte payStatus, DateTime payTime, string updateBy, sbyte payMethod, sbyte payChannel);

        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="moneyArriveStatus"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateMoneyArriveStatus(long orderId, sbyte moneyArriveStatus, string updateBy);

        /// <summary>
        /// 更新订单的对账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="UpdateBy"></param>
        /// <param name="reconciliationStatus"></param>
        /// <returns></returns>
        Task<long> UpdateOrderReconciliationStatus(List<string> OrderNos, string UpdateBy, int reconciliationStatus);

        /// <summary>
        /// 根据安装码获取订单
        /// </summary>
        /// <param name="installCode"></param>
        /// <returns></returns>
        Task<OrderDO> GetOrderByInstallCode(string installCode);

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

        /// <summary>
        /// 更新订单成本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateOrderProductCost(UpdateOrderProductRequest request);


        /// <summary>
        /// 更新订单的配送状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderDeliveryStatus(string orderNo, string updateBy);

        /// <summary>
        /// 更新订单的点评状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderCommentStatus(string orderNo, string updateBy, int commentStatus);

        /// <summary>
        /// 获取未支付订单
        /// </summary>
        /// <param name="hours">多少小时</param>
        /// <param name="minutes">多少分钟</param>
        /// <returns></returns>
        Task<List<OrderDO>> GetUnpaidOrders(int hours = 23, int minutes = 30);

        /// <summary>
        /// 获取超时未取消订单
        /// </summary>
        /// <param name="hours">超时时间小时数</param>
        /// <returns></returns>
        Task<List<OrderDO>> GetTimeOutUncanceledOrders(int hours = 24);

        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reserveStatus"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderReserveStatus(long orderId, sbyte reserveStatus, DateTime reserveTime);

        /// <summary>
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderDispatchStatus(List<string> orderNos, string updateBy, int dispatchStatus = 1);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        Task<bool> CancelOrder(string orderNo, string createBy);

        /// <summary>
        /// 更改订单的完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateOrderCompleteStatus(UpdateCompleteStatusRequest request);

        /// <summary>
        /// 判断用户首次下单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<int> CheckUserFirstOrderForSpecialProduct(string userId, string productId);
    }
}
