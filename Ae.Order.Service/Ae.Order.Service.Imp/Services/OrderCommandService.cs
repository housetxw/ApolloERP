
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Common.Constant;
using Ae.Order.Service.Common.Extension;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Model.Vehicle;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Repository.Interfaces;
using Ae.Order.Service.Dal.Repository.Order;
using Ae.Order.Service.Dal.Repository.OrderProduct;

namespace Ae.Order.Service.Imp.Services
{
    /// <summary>
    /// 订单操作
    /// </summary>
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommandService> logger;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IOrderCarRepository orderCarRepository;
        private readonly IVehicleClient vehicleClient;

        private readonly IConsumerOrderClient _consumerOrderClient;

        public OrderCommandService(IOrderRepository orderRepository, IMapper mapper,
            ApolloErpLogger<OrderCommandService> logger, IOrderProductRepository orderProductRepository, IConsumerOrderClient consumerOrderClient, IOrderCarRepository orderCarRepository, IVehicleClient vehicleClient)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.orderProductRepository = orderProductRepository;
            _consumerOrderClient = consumerOrderClient;
            this.orderCarRepository = orderCarRepository;
            this.vehicleClient = vehicleClient;
        }

        public OrderCommandService()
        {
        }

        /// <summary>
        /// 更新订单子状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            if (request.OrderNos == null || !request.OrderNos.Any())
                return new ApiResult<long>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ParameterNone
                };
            long dalUpdateOrderStatus = 0;
            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.SignStatus)
            {
                dalUpdateOrderStatus = await orderRepository.UpdateOrderSignStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
            }

            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.InstallStatus)
            {
                dalUpdateOrderStatus =
                    await orderRepository.UpdateOrderInstallStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
            }

            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.NoReconciliation ||
              request.UpdateStatusType == UpdateOrderStatusTypeEnum.ExceptionReconciliation ||
              request.UpdateStatusType == UpdateOrderStatusTypeEnum.HaveReconciliation)
            {
                int reconciliationStatus = 0;
                if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.NoReconciliation)
                {
                    reconciliationStatus = ReconciliationStatusEnum.NotReconciliation.ToInt();
                }

                if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.ExceptionReconciliation)
                {
                    reconciliationStatus = ReconciliationStatusEnum.ExceptionReconciliation.ToInt();

                }

                if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.HaveReconciliation)
                {
                    reconciliationStatus = ReconciliationStatusEnum.HaveReconciliation.ToInt();

                }
                dalUpdateOrderStatus = await orderRepository.UpdateOrderReconciliationStatus(request.OrderNos, request.UpdateBy, reconciliationStatus);

            }

            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.DeliveryStatus)
            {
                dalUpdateOrderStatus =
                    await orderRepository.UpdateOrderDeliveryStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
            }

            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerComment ||
                request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerAppendComment)
            {

                int commentStatus = 0;
                if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerComment)
                {
                    commentStatus = CustomerCommentStatusEnum.CustomerComment.ToInt();
                }

                if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerAppendComment)
                {
                    commentStatus = CustomerCommentStatusEnum.CustomerAppendComment.ToInt();

                }
                dalUpdateOrderStatus =
                    await orderRepository.UpdateOrderCommentStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy, commentStatus);
            }



            if (dalUpdateOrderStatus > 0)
                return new ApiResult<long>()
                {
                    Code = ResultCode.Success,
                    Data = dalUpdateOrderStatus
                };
            else
            {
                return new ApiResult<long>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ErrorOccured
                };
            }
        }

        public async Task<ApiResult> SyncOrder(SyncOrderRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = mapper.Map<OrderDO>(request.Order);
                var products = mapper.Map<List<OrderProductDO>>(request.OrderProducts);

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (order.Id > 0)
                    {
                        order.Id = 0;
                    }
                    var orderId = await orderRepository.InsertAsync(order);

                    if (orderId > 0)
                    {
                        products.ForEach(_ =>
                        {
                            _.OrderId = orderId;
                            if (_.Id > 0)
                            {
                                _.Id = 0;
                            }
                        });
                        await orderProductRepository.InsertBatchAsync(products);
                        result = Result.Success();
                    }
                    else
                    {
                        result = Result.Failed();
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdateOrderMainStatus(UpdateOrderMainStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderRepository.UpdateOrderMainStatus(order.Id, request.OrderStatus, request.UpdateBy);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateMainStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdateOrderStockStatus(UpdateOrderStockStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderRepository.UpdateOrderStockStatus(order.Id, request.StockStatus, request.UpdateBy);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateOrderStockStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdateProductStockStatus(UpdateProductStockStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderProductRepository.UpdateProductStockStatus(order.Id, request.StockStatus, request.UpdateBy, request.OrderPids);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateProductStockStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderRepository.UpdatePayStatus(order.Id, request.PayStatus, request.PayTime, request.UpdateBy, request.PayMethod, request.PayChannel);

                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdatePayStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request)
        {
            var result = new ApiResult<bool>()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ParameterNone
            };
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<bool>(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                if (order.TotalProductAmount - order.DeliveryFee - request.TotalCouponAmount != request.ActualAmount)
                {
                    result = Result.Failed<bool>( "订单金额不正确："+ (order.TotalProductAmount - order.DeliveryFee - request.TotalCouponAmount).ToString());
                    return result;
                }
                bool isSuccess = await orderRepository.UpdateCouponAmount(request.OrderNo, request.TotalCouponAmount, request.ActualAmount,request.Remark, request.UpdateBy);

                if (isSuccess)
                {
                    result = Result.Success(true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateCouponAmount", ex);
                result = Result.Exception<bool>(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderRepository.UpdateMoneyArriveStatus(order.Id, request.MoneyArriveStatus, request.UpdateBy);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateMoneyArriveStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult> UpdateProductTotalCostPrice(UpdateProductTotalCostPriceRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderProductRepository.UpdateProductTotalCostPrice(order.Id, request.OrderPid, request.TotalCostPrice, request.UpdateBy, request.Remark);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateProductTotalCostPriceEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await orderRepository.UpdateOrderReverseInfo(order.Id, (sbyte)request.ReverseStatus, request.RefundStatus, request.UpdateBy, request.RefundAmount);
                    if (order.OrderStatus != (sbyte)OrderStatusEnum.Canceled)
                    {
                        switch (request.ApplyType)
                        {
                            case ReverseApplyTypeEnum.None:
                                break;
                            case ReverseApplyTypeEnum.Cancel:
                                await orderRepository.UpdateOrderMainStatus(order.Id, (sbyte)OrderStatusEnum.Canceled, request.UpdateBy);
                                break;
                            case ReverseApplyTypeEnum.Refund:
                                break;
                            case ReverseApplyTypeEnum.Change:
                                break;
                            default:
                                break;
                        }
                    }
                    ts.Complete();
                }
                result = Result.Success();
            }
            catch (Exception ex)
            {
                logger.Error("OrderReverseNotifyEx", ex);
            }
            return result;
        }
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrderByNo(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = await orderRepository.UpdateOrderReserveStatus(order.Id, request.ReserveStatus, request.ReserveTime);
                if (isSuccess)
                {
                    result = Result.Success();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdateOrderReserveStatusEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrderForReserverOrArrival(CancelOrderForReserverOrArrivalRequest request)
        {
            var getReserverOrArrival = await orderRepository.
                UpdateOrderCancelForReserverOrArrival(request.OrderNos, request.UserId);

            if (getReserverOrArrival)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "更新失败"
            };
        }

        /// <summary>
        /// 批量更新付款状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request)
        {
            var payStatus = await orderRepository.BatchUpdatePayStatus(request);
            if (payStatus > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "更新失败"
            };
        }

        /// <summary>
        /// 批量更新完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request)
        {
            var completeStatus = await orderRepository.BatchUpdateCompleteStatus(request);
            if (completeStatus > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "更新失败"
            };
        }

        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request)
        {
            bool response = await orderRepository.UpdateOrderDispatchStatus(orderNos: request.OrderNos, request.CreateBy, request.DispatchStatus);

            if (response)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "更新失败"
            };

        }

        /// <summary>
        /// 更改订单的安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request)
        {
            var installStatus = await orderRepository.BatchUpdateInstallStatus(request);
            if (installStatus > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "更新失败"
            };
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var isSuccess = await orderRepository.CancelOrder(request.Data.OrderNo, request.Data.CreateBy);
            if (isSuccess)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "取消成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "取消失败"
            };
        }

        public async Task<ApiResult> UpdateOrderCompleteStatus(ApiRequest<UpdateCompleteStatusRequest> request)
        {
            var result = await orderRepository.UpdateOrderCompleteStatus(request.Data);
            if (result > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "更新失败"
            };
        }

        public async Task<ApiResult> UpdateOrderCar(ApiRequest<UpdateOrderCarRequest> request)
        {
            int.TryParse(Regex.Replace(request.Data.OrderNo, @"\D", ""), out var orderId);

            var orderCar = await orderCarRepository.GetOrderCar(orderId);
            if (orderCar == null)
            {
                var getUserVehicleInfo = await vehicleClient.GetUserVehicleByCarIdAsync(new Core.Request.Vehicle.UserVehicleByCarIdRequest()
                {
                    CarId = request.Data.CarId,
                    UserId = request.Data.UserId
                });

                if (getUserVehicleInfo.IsNotNullSuccess<UserVehicleDTO>())
                {
                    OrderCarDO orderCarDO = mapper.Map<OrderCarDO>(getUserVehicleInfo.Data);

                    if (orderCarDO != null)
                    {
                        orderCarDO.OrderId = orderId;
                        await orderCarRepository.InsertAsync(orderCarDO);
                        return Result.Success("操作成功");
                    }

                }
            }

            return Result.Failed("操作失败");
        }
    }
}
