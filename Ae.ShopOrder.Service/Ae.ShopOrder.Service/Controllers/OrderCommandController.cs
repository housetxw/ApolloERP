using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.WebApi.Filters;

namespace Ae.ShopOrder.Service.Controllers
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(OrderQueryController))]
    public class OrderCommandController : Controller
    {
        private readonly IOrderCommandService _orderCommandService;

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="orderCommandService"></param>
        public OrderCommandController(IOrderCommandService orderCommandService)
        {
            _orderCommandService = orderCommandService;
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            return await _orderCommandService.UpdateOrderStatus(request);

        }

        /// <summary>
        /// 订单不适配提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveOrderNotAdapter([FromBody] OrderNotAdapterRequest request)
        {
            return await _orderCommandService.SaveOrderNotAdapter(request);
        }

        /// <summary>
        /// 创建订单For 预约 Or 到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<string>>> CreateOrderForArrivalOrReserver([FromBody] CreateOrderForArrivalOrReserverRequest request)
        {
            var createArrivalRequest = new ApiRequest<CreateOrderForArrivalOrReserverRequest>()
            {
                Data = request
            };
            return await _orderCommandService.CreateOrderForArrivalOrReserver(createArrivalRequest);
        }

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderPayStatus([FromBody] ApiRequest<UpdateOrderPayStatusRequest> request)
        {
            return await _orderCommandService.UpdateOrderPayStatus(request.Data);
        }

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrderForReserverOrArrival([FromBody] ApiRequest<CancelOrderForReserverOrArrivalRequest> request)
        {
            return await _orderCommandService.CancelOrderForReserverOrArrival(request);
        }

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderReverseNotify([FromBody] OrderReverseNotifyRequest request)
        {
            return await _orderCommandService.OrderReverseNotify(request);
        }

        /// <summary>
        /// 添加订单派工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveOrderDispatch([FromBody] ApiRequest<List<SaveOrderDispatchRequest>> request)
        {
            return await _orderCommandService.SaveOrderDispatch(request);
        }

        /// <summary>
        /// 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderCompleteStatus([FromBody] ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            return await _orderCommandService.UpdateOrderCompleteStatus(request.Data);
        }

        /// <summary>
        /// 更改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateInstallStatus([FromBody] BatchUpdateInstallStatusRequest request)
        {
            return await _orderCommandService.BatchUpdateInstallStatus(request);
        }

        /// <summary>
        /// 修改订单优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateOrderCouponAmount([FromBody] UpdateCouponAmountRequest request)
        {
            return await _orderCommandService.UpdateCouponAmount(request);
        }

        /// <summary>
        /// 修改订单手动优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateOrderOtherCouponAmount([FromBody] ApiRequest<UpdateOtherCouponAmountRequest> request)
        {
            return await _orderCommandService.UpdateOrderOtherCouponAmount(request.Data);
        }

        /// <summary>
        /// 更新订单产品的成本价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderProductCostPrice([FromBody] ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            return await _orderCommandService.UpdateOrderProductCostPrice(request.Data);
        }

        /// <summary>
        /// 更新订单产品的优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderProductActualPayAmount([FromBody] ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            return await _orderCommandService.UpdateOrderProductActualPayAmount(request.Data);
        }

        /// <summary>
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount([FromBody] ApiRequest<BatchUpdateOrderRequest> request)
        {
            return await _orderCommandService.BatchUpdateOrderProductCostPriceActualPayAmount(request.Data);
        }

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder([FromBody] ApiRequest<SubmitOrderRequest> request)
        {
            if (request.Data.ProduceType == BuyProductTypeEnum.BuyVerticationCode.ToSbyte())
            {
                return await _orderCommandService.SubmitVerificationCodeOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.DelegateShopDepositOrder.ToSbyte() ||
                request.Data.ProduceType == BuyProductTypeEnum.DelegateCompanyDepositOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitDelegateDepositOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.OfficeOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitOfficeOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.PayGoods.ToSbyte())
            {
                return await _orderCommandService.SubmitPayGoodsOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.DelegatePay.ToSbyte())
            {
                return await _orderCommandService.SubmitDelegatePay(request);
            }
            //else if (request.Data.ProduceType == BuyProductTypeEnum.QuickOrder.ToSbyte())
            //{快速下单并入标准下单SubmitOrder
            //    return await _orderCommandService.SubmitQuickOrder(request);
            //}
            else if (request.Data.ProduceType == BuyProductTypeEnum.CustomerToSamllWarehouseOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitCustomerToSamllWarehouseOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.ShopToSamllWarehouseOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitShopToSamllWarehouseOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.MonthReconciliationOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitMonthSamllWarehouseOrder(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.BuyPackageCard.ToSbyte())
            {
                return await _orderCommandService.SubmitBuyPackageCard(request);
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.MeiTuanOrder.ToSbyte())
            {
                return await _orderCommandService.SubmitMeiTuanOrder(request);//boss订单详情：美团核销
            }
            else if (request.Data.PayChannel == PayChannelEnum.DaiZhiFu.ToSbyte())
            {
                return await _orderCommandService.SubmitDaiZhiFuOrder(request);//boss订单详情：代支付
            }
            else if (request.Data.ProduceType == BuyProductTypeEnum.InsuranceImport.ToSbyte())
            {
                return await _orderCommandService.SubmitDakehuOrder(request);//boss订单详情：大客户核销
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Data.InsuranceName))  // && !string.IsNullOrEmpty(request.Data.InsuranceRefNo)
                {
                    request.Data.ProduceType = BuyProductTypeEnum.InsuranceImport.ToSbyte();
                }
                return await _orderCommandService.SubmitOrder(request);
            }
        }

        /// <summary>
        ///提交核销码使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder([FromBody] ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            if (request.Data.Code.Contains(ValidateCodeTypeEnum.HX.ToString()))
            {
                return await _orderCommandService.SubmitUseVerificationCodeOrder(request);
            }
            else if (request.Data.Code.Contains(ValidateCodeTypeEnum.TC.ToString()))
            {
                return await _orderCommandService.UseBuyPackageOrderV2(request);
            }
            else
            {
                return new ApiResult<SubmitUseVerificationCodeOrderResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = "验码失败,请扫描正确码"
                };
            }

        }

        /// <summary>
        /// 添加订单派工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderDispatch([FromBody] ApiRequest<List<UpdateOrderDispatchRequest>> request)
        {
            return await _orderCommandService.UpdateOrderDispatch(request);
        }

        /// <summary>
        /// 添加订单派工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrderDispatch([FromBody] ApiRequest<CancelOrderDispatchRequest> request)
        {
            return await _orderCommandService.CancelOrderDispatch(request);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrder([FromBody] ApiRequest<CancelOrderRequest> request)
        {
            return await _orderCommandService.CancelOrder(request);
        }

        /// <summary>
        /// 订单支付成功通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderPaySuccessNotify([FromBody] ApiRequest<OrderPaySuccessNotifyRequest> request)
        {
            return await _orderCommandService.OrderPaySuccessNotify(request.Data);
        }


        /// <summary>
        /// 收款完成后安装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderPayInstall([FromBody] ApiRequest<OrderPayInstallRequest> request)
        {
            if (request.Data.OrderNo.Contains("RGH"))
            {
                return await _orderCommandService.MergeOrderPayInstall(request.Data);
            }
            else
            {
                return await _orderCommandService.OrderPayInstall(request.Data);
            }
        }

        /// <summary>
        /// 释放库存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> ReleaseShopStock(ReleaseShopStockRequest request)
        {
            return await _orderCommandService.ReleaseShopStock(request);
        }

        /// <summary>
        /// 更新订单地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderAddress([FromBody] ApiRequest<UpdateOrderAddressRequest> request)
        {
            return await _orderCommandService.UpdateOrderAddress(request);
        }

        /// <summary>
        /// 外采机滤 替换
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveOrderMachineFilter([FromBody] ApiRequest<SaveOrderMachineFilterRequest> request)
        {
            return await _orderCommandService.SaveOrderMachineFilter(request);
        }

        [HttpPost]
        public async Task<ApiPagedResult<UserCouponVO>> GetUserCoupons([FromBody] ApiRequest<GetUserCouponsRequest> request)
        {
            return await _orderCommandService.GetUserCoupons(request.Data);
        }

        [HttpPost]
        public async Task<GetOrderConfirmPackageProductServicesResponse> GetOrderConfirmPackageProductServices([FromBody] ApiRequest<GetOrderConfirmPackageProductServicesRequest> request)
        {
            return await _orderCommandService.GetOrderConfirmPackageProductServices(request.Data,null);
        }

        [HttpPost]
        public async Task<ApiResult> SaveVerificationRule([FromBody] ApiRequest<SaveVerificationRuleRequest> request)
        {
            return await _orderCommandService.SaveVerificationRule(request);
        }
        [HttpPost]
        public async Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct([FromBody] ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request)
        {
            return await _orderCommandService.SaveBeautiyOrPackageCardVerificationProduct(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult> SaveShopSmallWarehouseOrderStatus([FromBody] SaveShopSmallWarehouseOrderStatusRequest request)
        {
            return await _orderCommandService.SaveShopSmallWarehouseOrderStatus(request);
        }


    }
}
