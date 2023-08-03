using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public interface IOrderRepository : IRepository<OrderDO>
    {
        Task<OrderDO> GetOrder(string userId, string orderNo, bool useMaster = false);

        Task<OrderDO> GetOrder(string orderNo, bool useMaster = false);

        Task<List<OrderDO>> GetOrderList(GetOrderListRequest request);
        Task<List<ShopOrderDO>> GetSimpleOrderList(GetOrderListRequest request);


        /// <summary>
        /// 修改订单优惠金额
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateCouponAmount(string orderNo, decimal totalCouponAmount, decimal actualAmount, string remark, string updateBy);

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateOrderPayStatus(UpdateOrderPayStatusRequest request);

        /// <summary>
        /// 更新订单号和安装码
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="installCode"></param>
        /// <returns></returns>
        bool UpdateOrderNo(long orderId, string orderNo);

        /// <summary>
        /// 更新取消订单For预约或到店
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderCancelForReserverOrArrival(List<string> orderNos, string userId);

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateOrderCompleteStatus(BatchUpdateCompleteStatusRequest request);

        /// 更新派工状态
        /// </summary>
        /// <param name="orderNos"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderDispatchStatus(List<string> orderNos, string updateBy,int dispatchStatus=1);

        /// <summary>
        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);


        /// <summary>
        /// 更新订单库存状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="stockStatus"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderStockStatus(long orderId, sbyte stockStatus, string updateBy);

        /// <summary>
        /// 更新订单号和安装码
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="installCode"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderNoAndInstallCode(long orderId, string orderNo, string installCode);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        Task<bool> CancelOrder(string orderNo, string createBy);

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
        Task<bool> UpdatePayStatus(string orderNo, sbyte payStatus, DateTime payTime, string updateBy, sbyte payMethod, sbyte payChannel);


        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="moneyArriveStatus"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateMoneyArriveStatus(string orderNo, sbyte moneyArriveStatus, string updateBy);

        /// <summary>
        /// 修改订单的安装状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderInstallStatus(string orderNo, string updateBy);


        /// <summary>
        /// 更新订单的完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateOrderCompleteStatus(string orderNo, string updateBy);

        /// <summary>
        ///  更新订单的签收状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderSignStatus(string orderNo, string updateBy);

        /// <summary>
        /// 更改订单的确认状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderConfirmStatus(string orderNo, string updateBy);

        Task<PagedEntity<OrderDO>> GetOrderStaticReport(GetOrderDetailStaticReportRequest request);


        Task<int> CheckUserFirstOrderForSpecialProduct(string userId, string productId);


        Task<PagedEntity<OrderDO>> GetOrdersForOffice(GetOrdersForOfficeRequest request);

        Task<PagedEntity<GetOrderOutProductResponse>> GetOrderOutProducts(GetOrderOutProductsProfitRequest request);
        Task<PagedEntity<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfit(GetOrderOutProductsProfitRequest request);

        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopOrderDO>> GetShopOrderList(GetShopSalesMonthRequest request);

        /// <summary>
        /// 更改订单的分类状态
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<long> UpdateOrderChannel(string orderNo, int productType);

        /// <summary>
        /// 更改订单备注
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<long> UpdateOrderRemark(string orderNo, string remark);

    }
}
