using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Clients;
using Ae.B.Order.Api.Client.Clients.ShopOrder;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderCommand;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Imp.Services
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommandService> logger;
        private readonly IIdentityService identityService;
        private readonly IConsumerOrderClient consumerOrderClient;
        private readonly IReverseClient reverseClient;
        private readonly IShopOrderClient _shopOrderClient;
        private readonly IOrderClient _orderClient;

        public OrderCommandService(IMapper mapper, ApolloErpLogger<OrderCommandService> logger, IIdentityService identityService,
            IConsumerOrderClient consumerOrderClient,
            IReverseClient reverseClient, IShopOrderClient shopOrderClient, IOrderClient orderClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.consumerOrderClient = consumerOrderClient;
            this.reverseClient = reverseClient;
            _shopOrderClient = shopOrderClient;
            _orderClient = orderClient;
        }

        public async Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderRequest> request)
        {
            var result = Result.Failed<AppendSubmitOrderResponse>("下单失败");
            try
            {
                var clientRequest = mapper.Map<ApiRequest<AppendSubmitOrderClientRequest>>(request);
                clientRequest.Data.CreatedBy = identityService.GetUserName();
                var clientResult = await consumerOrderClient.AppendSubmitOrder(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<AppendSubmitOrderResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<AppendSubmitOrderResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("AppendSubmitOrderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            // var result = Result.Failed<CreateReverseOrderBaseResponse>("申请失败");

            //var clientRequest = mapper.Map<CreateReverseOrderForCancelClientRequest>(request.Data);
            //clientRequest.OperatorType = Core.Enums.ReverseOperatorTypeEnum.CustomerService;
            //clientRequest.OperatorName = identityService.GetUserName();
            //var clientResult = await reverseClient.CreateReverseOrderForCancel(clientRequest);
            //if (clientResult.IsNotNullSuccess())
            //{
            //    var response = mapper.Map<CreateReverseOrderBaseResponse>(clientResult.Data);
            //    result = Result.Success(response, clientResult.Message);
            //}
            //else
            //{
            //    result = Result.Failed<CreateReverseOrderBaseResponse>(clientResult.Code, clientResult.Message);
            //}
            request.Data.CreateBy = identityService.GetUserName() ?? string.Empty;
            var apiResult = await _shopOrderClient.CancelOrder(request);
            return new ApiResult<CreateReverseOrderBaseResponse>()
            {
                Code = apiResult.Code,
                Message = apiResult.Message,
                Data = default(CreateReverseOrderBaseResponse)
            };
        }

        public async Task<ApiResult> CheckOrder(ApiRequest<CheckOrderRequest> request)
        {
            var result = Result.Failed("审核失败");
            try
            {
                var orderNo = request.Data.OrderNo;
                var clientRequest = new CheckOrderClientRequest()
                {
                    OrderNo = orderNo,
                    CheckType = 2,
                    IsCheckPass = true,
                    UpdateBy = identityService.GetUserName()
                };
                var clientResult = await consumerOrderClient.CheckOrder(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success(clientResult.Message);
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("CheckOrderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<CreateReverseOrderBaseResponse>> RefundApply(ApiRequest<RefundApplyRequest> request)
        {
            var result = Result.Failed<CreateReverseOrderBaseResponse>("申请失败");
            try
            {
                var clientRequest = mapper.Map<CreateReverseOrderForRefundClientRequest>(request.Data);
                clientRequest.OperatorType = Core.Enums.ReverseOperatorTypeEnum.CustomerService;
                clientRequest.OperatorName = identityService.GetUserName();
                var clientResult = await reverseClient.CreateReverseOrderForRefund(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<CreateReverseOrderBaseResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<CreateReverseOrderBaseResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("RefundApplyEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request)
        {
            return await _shopOrderClient.SubmitOrder(request);
        }

        public async Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName() ?? string.Empty;
            return await _shopOrderClient.UpdateOrderAddress(request);
        }

        public Task<ApiResult<long>> UpdateOrderSignStatus(UpdateOrderStatusRequest request)
        {
            request.UpdateBy = identityService.GetUserName() ?? "SystemSign";
            request.UpdateStatusType = UpdateOrderStatusTypeEnum.SignStatus;
            return _orderClient.UpdateOrderStatus(request);
        }

        public Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request)
        {
            request.UpdateBy = identityService.GetUserName() ?? "SystemSign";
            //return _orderClient.UpdateCouponAmount(request);
            return _shopOrderClient.UpdateOrderCouponAmount(request);
        }

        public Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName();
            return _shopOrderClient.SaveVerificationRule(request);
        }

        public Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName();
            return _shopOrderClient.SaveBeautiyOrPackageCardVerificationProduct(request);
        }

        public Task<ApiResult> UpdateOrderPackage(ApiRequest<UpdateOrderPackageRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName();
            return _orderClient.UpdateOrderPackage(request);
        }

        public async Task<ApiResult> UpdateOrderProductCostPrice(ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName() ?? string.Empty;
            return await _shopOrderClient.UpdateOrderProductCostPrice(request);
           
        }

        public async Task<ApiResult> UpdateOrderProductActualPayAmount(ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName() ?? string.Empty;
            return await _shopOrderClient.UpdateOrderProductActualPayAmount(request);

        }

        /// <summary>
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(ApiRequest<BatchUpdateOrderRequest> request)
        {
            
            logger.Info($"GetUserId:{identityService.GetUserId()}");
            request.Data.UpdateBy = identityService.GetUserName() ?? string.Empty;
            logger.Info($"BatchUpdateOrderProductCostPriceActualPayAmount request:{JsonConvert.SerializeObject(request)}");
            return await _shopOrderClient.BatchUpdateOrderProductCostPriceActualPayAmount(request);

        }

        /// <summary>
        /// 批次更新员工绩效订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateEmployeePerformanceOrder(ApiRequest<BatchUpdateOrderRequest> request)
        {
            request.Data.UpdateBy = identityService.GetUserName() ?? string.Empty;
            logger.Info($"BatchUpdateEmployeePerformanceOrder request:{JsonConvert.SerializeObject(request)}");
            return await _shopOrderClient.BatchUpdateEmployeePerformanceOrder(request);

        }
    }
}
