using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Common.Constant;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Dal.Repository.Order;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response.Order;
using System;
using Ae.Order.Service.Dal.Repository.OrderProduct;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Common.Exceptions;
using System.Text.RegularExpressions;
using Ae.Order.Service.Client.Interface;

namespace Ae.Order.Service.Imp.Services
{
    /// <summary>
    /// 实现订单查询服务
    /// </summary>
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMapper mapper;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IShopOrderClient _shopOrderClient;
        private readonly IConsumerOrderClient _consumerOrderClient;
        private readonly IOrderAddressRepository _orderAddressRepository;

        private readonly IOrderCouponRepository _orderCouponRepository;

        public OrderQueryService(IMapper mapper, IConfiguration configuration, ApolloErpLogger<OrderQueryService> logger, IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository, IShopOrderClient shopOrderClient,
            IConsumerOrderClient consumerOrderClient, IOrderAddressRepository orderAddressRepository, IOrderCouponRepository orderCouponRepository)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.orderProductRepository = orderProductRepository;
            _consumerOrderClient = consumerOrderClient;
            _shopOrderClient = shopOrderClient;
            _orderAddressRepository = orderAddressRepository;
            _orderCouponRepository = orderCouponRepository;
        }

        /// <summary>
        /// 获取订单显示状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string GetDisplayOrderStatus(OrderDO order)
        {
            string displayOrderStatus = string.Empty;
            switch ((OrderStatusEnum)order.OrderStatus)
            {
                case OrderStatusEnum.Submitted:
                    if (order.PayStatus == 0)
                    {
                        if (order.PayMethod == 1)
                        {
                            displayOrderStatus = "待付款";
                        }
                    }
                    else
                    {
                        displayOrderStatus = "待审核";
                    }
                    break;
                case OrderStatusEnum.Confirmed:
                    if (order.ProduceType == 1)
                    {
                        displayOrderStatus = "待安装";
                    }
                    else if (order.DeliveryType == 1)
                    {
                        if (order.DeliveryStatus == 1)
                        {
                            if (order.SignStatus == 0)
                            {
                                displayOrderStatus = "待收货";
                            }
                            else
                            {
                                displayOrderStatus = "待安装";
                            }
                        }
                        else
                        {
                            displayOrderStatus = "待发货";
                        }
                    }
                    break;
                case OrderStatusEnum.Completed:
                    if (order.CommentStatus == 0)
                    {
                        displayOrderStatus = "待评价";
                    }
                    else
                    {
                        displayOrderStatus = "已完成";
                    }
                    break;
                default:
                    break;
                case OrderStatusEnum.Canceled:
                    displayOrderStatus = "已取消";
                    break;
            }
            if (order.OrderStatus < (sbyte)OrderStatusEnum.Canceled
                && order.IsOccurReverse == 1 && order.ReverseStatus != (sbyte)ReverseStatusEnum.Canceled)
            {
                if (order.PayStatus == 1)
                {
                    if (order.RefundStatus == 0)
                    {
                        displayOrderStatus = "退款中";
                    }
                }
            }
            return displayOrderStatus;
        }

        /// <summary>
        /// 得到订单明细详情根据orderIds
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public async Task<List<OrderProductDTO>> GetOrderDetailByOrderIds(List<string> orderNos)
        {
            if (orderNos != null && orderNos.Any())
            {
                var dalResponse = await orderRepository.GetOrderDetails(orderNos);

                List<OrderProductDTO> response = mapper.Map<List<OrderProductDTO>>(dalResponse);

                return response;
            }

            return null;
        }

        /// <summary>
        /// 查询订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderDTO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
            var dalResponse = await orderRepository.GetOrderBaseInfo(request);
            List<OrderDTO> response = mapper.Map<List<OrderDTO>>(dalResponse);
            return response;
        }

        /// <summary>
        /// 查询订单基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OrderDTO>> GetOrderBaseInfoForBusinessStatus(GetOrderBaseInfoForBusinessStatusRequest request)
        {
            GetOrderBaseInfoForBusinessStatusRequest dalRequest = request;
            if (request.Channels != null && request.Channels.Any())
            {

                request.Channels = new List<int>() { (int)ChannelEnum.ApolloErpToC, (int)ChannelEnum.ApolloErpToShop };

            }

            var dalResponse = await orderRepository.GetOrderBaseInfoForBusinessStatus(dalRequest);
            ApiPagedResultData<OrderDTO> response = mapper.Map<ApiPagedResultData<OrderDTO>>(dalResponse);
            return response;
        }

        public async Task<ApiResult<OrderDTO>> GetOrderByNo(GetOrderByNoRequest request)
        {
            var result = Result.Failed<OrderDTO>();
            try
            {
                var orderDO = await orderRepository.GetOrderByNo(request.OrderNo);
                var orderDTO = mapper.Map<OrderDTO>(orderDO);
                result = Result.Success(orderDTO);
            }
            catch (Exception ex)
            {
                result = Result.Exception<OrderDTO>(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 得到对账单的金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetAccountCheckAmountResponse>>> GetAccountCheckAmount(GetAccountCheckAmountRequest request)
        {
            var getAccountCheckAmountDalResponse = await orderRepository.GetAccountCheckAmount(request.OrderNos);
            if (getAccountCheckAmountDalResponse != null)
            {

                decimal.TryParse(configuration["WithdrawalRate"], out var withdrawRate);

                getAccountCheckAmountDalResponse?.ForEach(x =>
                {

                    var commissionFee = Math.Ceiling((x.TotalCost * withdrawRate) * 100) / 100;
                    x.SettlementFee = x.TotalCost - commissionFee;
                    x.CommissionFee = commissionFee;
                });


                // Math.Round(12.33m, 2, MidpointRounding.ToPositiveInfinity)

                //getAccountCheckAmountDalResponse?.ForEach(x => { x.CommissionFee = x.TotalCost * withdrawRate; });

                List<GetAccountCheckAmountResponse> data =
                    mapper.Map<List<GetAccountCheckAmountResponse>>(getAccountCheckAmountDalResponse);
                return new ApiResult<List<GetAccountCheckAmountResponse>>()
                {
                    Code = ResultCode.Success,
                    Data = data
                };
            }

            return new ApiResult<List<GetAccountCheckAmountResponse>>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };

            return null;
        }

        /// <summary>
        /// 返回未对账的数据根据门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetNoReconciliationAmountDTO>>> GetNoReconciliationAmount(ShopRequest request)
        {
            if (request.ShopIds == null || !request.ShopIds.Any())
                return new ApiResult<List<GetNoReconciliationAmountDTO>>
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ParameterNone
                };
            var getNoReconciliationOrderNumDalResponse = await orderRepository.GetNoReconciliationOrderNum(request.ShopIds);
            if (getNoReconciliationOrderNumDalResponse != null)
            {
                var getNoReconciliationAmountDalResponse = await orderRepository.GetNoReconciliationAmount(request.ShopIds);
                //if (getNoReconciliationAmountDalResponse != null)
                {
                    decimal.TryParse(configuration["WithdrawalRate"], out var withdrawRate);
                    var response = new List<GetNoReconciliationAmountDTO>();

                    getNoReconciliationOrderNumDalResponse?.ForEach(x =>
                    {
                        var totalCost = getNoReconciliationAmountDalResponse?.Where(item => item.ShopId == x.ShopId)
                                            ?.Sum(item => item.TotalCost) ?? 0;

                        var commissionFee = Math.Ceiling((withdrawRate * totalCost) * 100) / 100;
                        response.Add(new GetNoReconciliationAmountDTO()
                        {
                            Num = x.StatisticsNum,
                            ServiceFee = getNoReconciliationAmountDalResponse?.Where(item => item.ShopId == x.ShopId)?.Sum(item => item.ServiceFee) ?? 0,
                            TotalCost = totalCost,
                            SettlementFee = totalCost - commissionFee,
                            ShopId = x.ShopId
                        });
                    });
                    if (!response.Any())
                    {
                        var emptyResult = new List<GetNoReconciliationAmountDTO>();
                        request.ShopIds?.ForEach(item =>
                        {
                            emptyResult.Add(new GetNoReconciliationAmountDTO()
                            {
                                Num = 0,
                                ServiceFee = 0,
                                TotalCost = 0,
                                SettlementFee = 0,
                                ShopId = item
                            });
                        });

                        response = emptyResult;
                    }
                    return new ApiResult<List<GetNoReconciliationAmountDTO>>
                    {
                        Code = ResultCode.Success,
                        Data = response
                    };
                }
            }
            return new ApiResult<List<GetNoReconciliationAmountDTO>>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到订单的数量（待签收、待安装、未对账、异常中、已取消
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderNumForHomeResponse>> GetOrderNumForHome(GetOrderNumForHomeRequest request)
        {

            if (request.ShopId <= 0)
                return new ApiResult<GetOrderNumForHomeResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ParameterNone
                };


            var getOrderNumForHomeResponse = new GetOrderNumForHomeResponse();
            int waitingSignNum = 0;
            int waitingInstallNum = 0;
            int waitingReconciliationNum = 0;
            int exceptionReconciliationNum = 0;
            int canceledNum = 0;
            List<long> shopIds = new List<long>();
            shopIds.Add(request.ShopId);
            var taskAmount = new Task[]
            {
                Task.Factory.StartNew(async () =>
                {
                   var  dalResponse= await orderRepository.GetWaitingSignOrderNum(shopIds);
                   waitingSignNum= dalResponse?.Where(x => x.ShopId == request.ShopId)?.Sum(x => x.StatisticsNum) ?? 0;

                }),
                Task.Factory.StartNew(function: async () => {
                    var  dalResponse= await orderRepository.GetWaitingInstallOrderNum(shopIds);
                    waitingInstallNum= dalResponse?.Where(x => x.ShopId == request.ShopId)?.Sum(x => x.StatisticsNum) ?? 0;

                }),
                Task.Factory.StartNew(function: async () => {
                    var  dalResponse= await orderRepository.GetNoReconciliationOrderNum(shopIds);
                    waitingReconciliationNum= dalResponse?.Where(x => x.ShopId == request.ShopId)?.Sum(x => x.StatisticsNum) ?? 0;

                }),
                Task.Factory.StartNew(function: async () => {
                    var  dalResponse= await orderRepository.GetExceptionReconciliationOrderNum(shopIds);
                    exceptionReconciliationNum= dalResponse?.Where(x => x.ShopId == request.ShopId)?.Sum(x => x.StatisticsNum) ?? 0;

                }),
                Task.Factory.StartNew(function: async () => {
                    var  dalResponse= await orderRepository.GetCanceledOrderNum(shopIds);
                    canceledNum= dalResponse?.Where(x => x.ShopId == request.ShopId)?.Sum(x => x.StatisticsNum) ?? 0;

                }),
            };
            await Task.WhenAll(taskAmount).ContinueWith(_ =>
            {
                getOrderNumForHomeResponse.WaitingSignNum = waitingSignNum;
                getOrderNumForHomeResponse.WaitingInstallNum = waitingInstallNum;
                getOrderNumForHomeResponse.WaitingReconciliationNum = waitingReconciliationNum;
                getOrderNumForHomeResponse.ExceptionReconciliationNum = exceptionReconciliationNum;
                getOrderNumForHomeResponse.CanceledNum = canceledNum;
            });

            return new ApiResult<GetOrderNumForHomeResponse>()
            {
                Code = ResultCode.Success,
                Data = getOrderNumForHomeResponse
            };
        }

        /// <summary>
        /// 根据用户ID获取购买过的商品ID集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<string>>> GetPidsByUserId(GetPidsByUserIdRequest request)
        {
            var result = Result.Failed<List<string>>("查询异常");
            try
            {
                var pids = await orderProductRepository.GetPidsByUserId(request.UserId);
                result = Result.Success(pids, "查询成功");
            }
            catch (Exception ex)
            {
                result = Result.Exception<List<string>>(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取未安装订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetUninstalledOrderListResponse>> GetUninstalledOrderList(GetUninstalledOrderListRequest request)
        {
            var result = new ApiPagedResult<GetUninstalledOrderListResponse>()
            {
                Code = ResultCode.Failed,
                Message = "查询异常",
                Data = new ApiPagedResultData<GetUninstalledOrderListResponse>() { Items = new List<GetUninstalledOrderListResponse>() }
            };
            try
            {
                var orderPageList = await orderRepository.GetUninstalledOrderList(request);
                if (orderPageList == null || orderPageList.TotalItems <= 0)
                {
                    result = new ApiPagedResult<GetUninstalledOrderListResponse>()
                    {
                        Code = ResultCode.SUCCESS_NOT_EXIST,
                        Message = "没有已签收未安装且未派工的订单",
                        Data = new ApiPagedResultData<GetUninstalledOrderListResponse>() { TotalItems = 0, Items = new List<GetUninstalledOrderListResponse>() }
                    };
                    return result;
                }

                var orderIds = from order in orderPageList.Items
                               select order.Id;

                var products = await orderProductRepository.GetOrderProductsByOrderIds(orderIds);
                if (products == null || !products.Any())
                {
                    result = new ApiPagedResult<GetUninstalledOrderListResponse>()
                    {
                        Code = ResultCode.SUCCESS_NOT_EXIST,
                        Message = "没有获取到未安装订单的商品信息",
                        Data = new ApiPagedResultData<GetUninstalledOrderListResponse>() { TotalItems = 0, Items = new List<GetUninstalledOrderListResponse>() }
                    };
                    return result;
                }

                var productList = products.ToList();
                var responseList = new List<GetUninstalledOrderListResponse>();
                foreach (var item in orderPageList.Items)
                {
                    var findProducts = productList.FindAll(_ => _.OrderId == item.Id);
                    var response = new GetUninstalledOrderListResponse()
                    {
                        OrderNo = item.OrderNo,
                        OrderStatusDisplayName = GetDisplayOrderStatus(item),
                        ActualAmount = item.ActualAmount,
                        UninstalledOrderProducts = mapper.Map<List<UninstalledOrderProductDTO>>(findProducts)
                    };
                    responseList.Add(response);
                }

                result = new ApiPagedResult<GetUninstalledOrderListResponse>()
                {
                    Code = ResultCode.Success,
                    Message = "查询成功",
                    Data = new ApiPagedResultData<GetUninstalledOrderListResponse>()
                    {
                        TotalItems = orderPageList.TotalItems,
                        Items = responseList
                    }
                };
            }
            catch (Exception ex)
            {
                logger.Error("GetUninstalledOrderListEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据PID列表批量获取订单商品销量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetSalesByPidsResponse>>> GetSalesByPids(GetSalesByPidsRequest request)
        {
            var result = Result.Failed<List<GetSalesByPidsResponse>>("查询异常");
            try
            {
                if (request.Pids == null || !request.Pids.Any() || request.Pids.Count > 100)
                {
                    throw new CustomException("必须指定PID并且目前最多支持100个，更多查询请循环获取");
                }
                var sales = await orderProductRepository.GetSalesByPids(request.Pids);
                var responseList = mapper.Map<List<GetSalesByPidsResponse>>(sales);
                result = Result.Success(responseList, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetSalesByPidsEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BatchGetOrderCountByShopIdResponse>>> BatchGetOrderCountByShopId(BatchGetOrderCountByShopIdRequest request)
        {
            var result = Result.Failed<List<BatchGetOrderCountByShopIdResponse>>("查询异常");
            try
            {
                if (request.ShopIds == null || !request.ShopIds.Any() || request.ShopIds.Count > 100)
                {
                    throw new CustomException("必须指定门店ID并且目前最多支持100个，更多查询请循环获取");
                }
                var batchGetOrderCountByShopIdDOs = await orderRepository.BatchGetOrderCountByShopId(request.ShopIds);
                var responseList = mapper.Map<List<BatchGetOrderCountByShopIdResponse>>(batchGetOrderCountByShopIdDOs);
                result = Result.Success(responseList, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("BatchGetOrderCountByShopIdEx", ex);
            }
            return result;
        }

        /// <summary>
        ///  得到车辆的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var result = new ApiResult<List<OrderCarDTO>>()
            {
                Code = ResultCode.Success,
                Data = new List<OrderCarDTO>()
            };
            var orderToB = new List<long>(10);
            var orderToC = new List<long>(10);
            request.OrderNos?.ForEach(_ =>
            {
                int.TryParse(Regex.Replace(_, @"\D", ""), out var orderId);
                if (_.StartsWith("RGB"))
                {
                    orderToB.Add(orderId);
                }
                if (_.StartsWith("RGC"))
                {
                    orderToC.Add(orderId);
                }
            });
            if (orderToB?.Count() > 0)
            {
                var getCars = await _shopOrderClient.GetOrderCarsInfo(new GetOrderCarsRequest()
                {
                    OrderIds = orderToB
                });
                if (getCars.IsNotNullSuccess())
                {
                    getCars.Data?.ForEach(_ =>
                    {
                        _.OrderNo = $"RGB{_.OrderId.ToString().PadLeft(5, '0')}";
                    });

                    result.Data.AddRange(getCars.Data);
                }

            }
            if (orderToC?.Count() > 0)
            {
                var getCars = await _consumerOrderClient.GetOrderCarsInfo(new GetOrderCarsRequest()
                {
                    OrderIds = orderToC
                });
                if (getCars.IsNotNullSuccess())
                {
                    getCars.Data?.ForEach(_ =>
                    {
                        _.OrderNo = $"RGC{_.OrderId.ToString().PadLeft(5, '0')}";
                    });
                    result.Data.AddRange(getCars.Data);
                }

            }
            return result;
        }

        /// <summary>
        /// 根据用户和门店查订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetOrdersByUserIdResponse>>> GetOrdersByUserId(GetOrdersByUserIdRequest request)
        {
            var result = Result.Success(new List<GetOrdersByUserIdResponse>());
            try
            {
                //根据userid查订单列表
                List<OrderDO> orderList = await orderRepository.GetOrderInfoByUserId(request);
                if (orderList != null && orderList.Any())
                {
                    //查订单产品和车型
                    List<GetOrdersByUserIdResponse> orderMain = mapper.Map<List<GetOrdersByUserIdResponse>>(orderList);
                    var orderNos = orderMain.Select(x => x.OrderNo).ToList();
                    var getOrderDetails = await GetOrderDetailByOrderIds(orderNos);
                    var orderCars = await GetOrderCarsInfo(new GetOrderCarsRequest() { OrderNos = orderNos });
                    orderMain.ForEach(o =>
                    {
                        var orderProducts = getOrderDetails.Where(_ => o.Id == _.OrderId).ToList();
                        o.OrderProductList = orderProducts;

                        var ordercar = orderCars.Data.Where(_ => o.OrderNo == _.OrderNo).FirstOrDefault();
                        o.OrderCar = ordercar;
                    });

                    result = Result.Success(orderMain);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                result = Result.Exception<List<GetOrdersByUserIdResponse>>(ex.Message);
            }
            return result;
        }

        public async Task<ApiPagedResultData<OrderDTO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request)
        {
            var dalResponse = await orderRepository.GetNotPayHaveStockOrder(request);
            ApiPagedResultData<OrderDTO> response = mapper.Map<ApiPagedResultData<OrderDTO>>(dalResponse);
            return response;
        }

        public async Task<ApiResult<List<OrderAddressDTO>>> BatchGetOrderAddress(BatchGetOrderAddressRequest request)
        {
            var getOrderAddress = await _orderAddressRepository.GetOrderAddress(request.OrderIds);

            var getOrderAddressDtos = mapper.Map<List<OrderAddressDTO>>(getOrderAddress);

            return Result.Success(getOrderAddressDtos?.Take(20)?.ToList());
        }

        public async Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(GetOrderCompleteStaticReportRequest request)
        {
            var data = await orderRepository.GetOrderCompleteStaticReport(request);

            return Result.Success(data);
        }

        public async Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(GetOrderNotCompleteStaticReportRequest request)
        {
            var data = await orderRepository.GetOrderNotCompleteStaticReport(request);

            return Result.Success(data);
        }

        public async Task<ApiResult<List<OrderCouponDTO>>> GetOrderCoupons(GetOrderCouponsRequest request)
        {
            var data = await _orderCouponRepository.GetOrderCoupons(request);
            return Result.Success(data);
        }
    }
}
