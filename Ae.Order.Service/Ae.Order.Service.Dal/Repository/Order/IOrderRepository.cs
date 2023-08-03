using Ae.Order.Service.Dal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.Order.Service.Core.Request.Order;
using ApolloErp.Data.DapperExtensions;
using System;
using JetBrains.Annotations;
using Ae.Order.Service.Dal.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Response.Order;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public interface IOrderRepository : IRepository<OrderDO>
    {
        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<OrderDO> GetOrderByNo(string orderNo);

        /// <summary>
        /// 得到订单详情根据OrderIds
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task<List<OrderProductDO>> GetOrderDetails(List<string> orderNos);

        /// <summary>
        /// 得到订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderDO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 得到订单的基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetOrderBaseInfoForBusinessStatus(
            GetOrderBaseInfoForBusinessStatusRequest request);

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

        /// <summary>
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
        /// 修改订单优惠金额
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateCouponAmount(string orderNo, decimal totalCouponAmount, decimal actualAmount, string remark, string updateBy);

        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="moneyArriveStatus"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateMoneyArriveStatus(long orderId, sbyte moneyArriveStatus, string updateBy);

        /// <summary>
        /// 得到门店对账金额
        /// </summary>
        /// <param name="OrderNos"></param>
        /// <returns></returns>
        Task<List<GetAccountCheckAmountDO>> GetAccountCheckAmount(List<string> OrderNos);

        /// <summary>
        /// 更改订单的对账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <param name="reconciliationStatus"></param>
        /// <returns></returns>
        Task<long> UpdateOrderReconciliationStatus(List<string> orderNos, string updateBy, int reconciliationStatus);

        /// <summary>
        /// 统计门店未对账的订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetStatisticsDO>> GetNoReconciliationOrderNum(List<long> shopIds);

        /// <summary>
        /// 待签收的订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetStatisticsDO>> GetWaitingSignOrderNum(List<long> shopIds);

        /// <summary>
        /// 待安装的订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetStatisticsDO>> GetWaitingInstallOrderNum(List<long> shopIds);

        /// <summary>
        /// 异常对账的订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetStatisticsDO>> GetExceptionReconciliationOrderNum(List<long> shopIds);

        /// <summary>
        /// 得到取消订单的数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetStatisticsDO>> GetCanceledOrderNum(List<long> shopIds);

        /// <summary>
        /// 得到未对账订单的金额
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<GetNoReconciliationAmountDO>> GetNoReconciliationAmount(List<long> shopIds);

        /// <summary>
        /// 获取未安装订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetUninstalledOrderList(GetUninstalledOrderListRequest request);
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reserveStatus"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderReserveStatus(long orderId, sbyte reserveStatus, DateTime reserveTime);

        /// <summary>
        /// 更新订单逆向信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="reverseStatus"></param>
        /// <param name="refundStatus"></param>
        /// <param name="updateBy"></param>
        /// <param name="isOccurReverse">默认是</param>
        /// <returns></returns>
        Task<bool> UpdateOrderReverseInfo(long orderId, sbyte reverseStatus, sbyte refundStatus, string updateBy, decimal refundAmount = 0, sbyte isOccurReverse = 1);

        /// <summary>
        ///  更新订单的配送状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderDeliveryStatus(string orderNo, string updateBy);

        /// <summary>
        ///  更新订单的点评状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderCommentStatus(string orderNo, string updateBy,int commentStatus);

        Task<bool> UpdateOrderCancelForReserverOrArrival(List<string> orderNos, string userId);


        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<BatchGetOrderCountByShopIdDO>> BatchGetOrderCountByShopId(List<long> shopIds);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request);

        /// <summary>
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderDispatchStatus(List<string> orderNos, string updateBy,int dispatchStatus = 1);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request);

        /// <summary>
        /// 更改订单的安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);

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
        ///得的未支付的订单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<PagedEntity<OrderDO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request);



        /// <summary>
        /// 根据用户查用户订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderDO>> GetOrderInfoByUserId(GetOrdersByUserIdRequest request);

        Task<GetOrderCompleteStaticReportResponse> GetOrderCompleteStaticReport(
            GetOrderCompleteStaticReportRequest request);

        Task<GetOrderNotCompleteStaticReportResponse> GetOrderNotCompleteStaticReport(
            GetOrderNotCompleteStaticReportRequest request);

        Task<List<GetPackageCardMainInfoResponse>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request);

    }
}
