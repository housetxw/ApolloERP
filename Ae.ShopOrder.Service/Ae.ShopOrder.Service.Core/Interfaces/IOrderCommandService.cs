using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    /// <summary>
    /// 订单操作
    /// </summary>
    public interface IOrderCommandService
    {
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);

        /// <summary>
        /// 订单不适配提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveOrderNotAdapter(OrderNotAdapterRequest request);

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderPayStatus(UpdateOrderPayStatusRequest request);

        /// <summary>
        /// 创建预约到店订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<string>>> CreateOrderForArrivalOrReserver(ApiRequest<CreateOrderForArrivalOrReserverRequest> request);

        /// <summary>
        /// 取消订单For预约Or到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrderForReserverOrArrival(ApiRequest<CancelOrderForReserverOrArrivalRequest> request);

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request);


        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderCompleteStatus(BatchUpdateCompleteStatusRequest request);

        /// <summary>
        /// 更新订单产品的成本价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderProductCostPrice(BatchUpdateCompleteStatusRequest request);

        /// <summary>
        /// 更新订单产品的优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderProductActualPayAmount(BatchUpdateCompleteStatusRequest request);

        /// <summary>
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(BatchUpdateOrderRequest request);

        /// <summary>
        /// 添加排工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveOrderDispatch(ApiRequest<List<SaveOrderDispatchRequest>> request);

        /// <summary>
        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);

        /// <summary>
        /// 修改订单优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request);

        /// <summary>
        /// 修改订单手动优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateOrderOtherCouponAmount(UpdateOtherCouponAmountRequest request);

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request);

        /// <summary>
        /// 核销码订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> SubmitVerificationCodeOrder(ApiRequest<SubmitOrderRequest> request);

        /// <summary>
        /// 提交使用核销码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request);

        /// <summary>
        /// 修改订单派工记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderDispatch(ApiRequest<List<UpdateOrderDispatchRequest>> request);

        /// <summary>
        /// 取消派工
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrderDispatch(ApiRequest<CancelOrderDispatchRequest> request);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        /// <summary>
        /// 支付成功回写
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderPaySuccessNotify(OrderPaySuccessNotifyRequest request);

        /// <summary>
        /// 收款安装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderPayInstall( OrderPayInstallRequest request);

        /// <summary>
        /// 合并支付收款安装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> MergeOrderPayInstall(OrderPayInstallRequest request);

        /// <summary>
        /// 释放库存配合自动任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ReleaseShopStock(ReleaseShopStockRequest request);

        /// <summary>
        /// 更新订单地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request);

        Task<ApiResult> SaveOrderMachineFilter(ApiRequest<SaveOrderMachineFilterRequest> request);

        Task<ApiPagedResult<UserCouponVO>> GetUserCoupons(GetUserCouponsRequest request);

        Task<ApiResult<SubmitOrderResponse>> SubmitDelegateDepositOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitOfficeOrder(ApiRequest<SubmitOrderRequest> request);


        Task<ApiResult<SubmitOrderResponse>> SubmitPayGoodsOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitDelegatePay(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitQuickOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitCustomerToSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitShopToSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitMonthSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitBuyPackageCard(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitMeiTuanOrder(ApiRequest<SubmitOrderRequest> request);
        Task<ApiResult<SubmitOrderResponse>> SubmitDakehuOrder(ApiRequest<SubmitOrderRequest> request);
        Task<ApiResult<SubmitOrderResponse>> SubmitDaiZhiFuOrder(ApiRequest<SubmitOrderRequest> request);

        /// <summary>
        /// 提交套餐卡核销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> UseBuyPackageOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request);
        Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> UseBuyPackageOrderV2(ApiRequest<SubmitUseVerificationCodeOrderRequest> request);

        Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request);

        Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(SaveBeautiyOrPackageCardVerificationProductRequest request);

        Task<ApiResult> SaveShopSmallWarehouseOrderStatus(SaveShopSmallWarehouseOrderStatusRequest request);


        /// <summary>
        /// 获取订单确认页 自营产品适配出的数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetOrderConfirmPackageProductServicesResponse> GetOrderConfirmPackageProductServices(GetOrderConfirmPackageProductServicesRequest request, List<UpdateProductInfo> updateProductInfos = null);






    }
}
