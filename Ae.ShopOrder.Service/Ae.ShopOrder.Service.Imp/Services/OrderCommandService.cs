using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using AutoMapper.Execution;
using Dapper;
using Newtonsoft.Json;
using NLog;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Clients.BaoYang;
using Ae.ShopOrder.Service.Client.Clients.Coupon;
using Ae.ShopOrder.Service.Client.Clients.FMS;
using Ae.ShopOrder.Service.Client.Clients.Order;
using Ae.ShopOrder.Service.Client.Clients.OrderForC;
using Ae.ShopOrder.Service.Client.Clients.PayServer;
using Ae.ShopOrder.Service.Client.Clients.Product;
using Ae.ShopOrder.Service.Client.Clients.Receive;
using Ae.ShopOrder.Service.Client.Clients.ShopServer;
using Ae.ShopOrder.Service.Client.Clients.Stock;
using Ae.ShopOrder.Service.Client.Clients.User;
using Ae.ShopOrder.Service.Client.Clients.Vehicle;
using Ae.ShopOrder.Service.Client.Model.Order;
using Ae.ShopOrder.Service.Client.Model.Vehicle;
using Ae.ShopOrder.Service.Client.Request.Coupon;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Client.Request.Product;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Client.Request.User;
using Ae.ShopOrder.Service.Client.Request.Vehicle;
using Ae.ShopOrder.Service.Client.Response.Coupon;
using Ae.ShopOrder.Service.Client.Response.Product;
using Ae.ShopOrder.Service.Client.Response.User;
using Ae.ShopOrder.Service.Common.Constant;
using Ae.ShopOrder.Service.Common.Exceptions;
using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model.BaoYang;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.BaoYang;
using Ae.ShopOrder.Service.Core.Request.Fms;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;
using Ae.ShopOrder.Service.Core.Request.Product;
using Ae.ShopOrder.Service.Core.Request.Shop;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Core.Response.Product;
using Ae.ShopOrder.Service.Core.Response.Shop;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using Ae.ShopOrder.Service.Imp.Helpers;

namespace Ae.ShopOrder.Service.Imp.Services
{
    /// <summary>
    /// 订单操作服务
    /// </summary>
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<OrderCommandService> _logger;
        private readonly IOrderCommandForCClient _orderCommandForCClient;
        private readonly IOrderNotAdapterRepository orderNotAdapterRepository;
        private readonly IRgStockClient _rgStockClient;
        private readonly IUserClient _userClient;
        private readonly IVehicleClient _vehicleClient;
        private readonly IShopClient _shopClient;
        private readonly IProductClient _productClient;
        private readonly IOrderClient _orderClient;
        private readonly IShopOrderRepository _shopOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDispatchRepository _orderDispatchRepository;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IOrderUserRepository _orderUserRepository;
        private readonly IOrderCarRepository _orderCarRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IOrderAddressRepository _orderAddressRepository;
        private readonly IVerificationRuleRepository _verificationRuleRepository;
        private readonly IOrderVerificationCodeRepository _orderVerificationCodeRepository;
        private readonly IVerificationRuleShopIdRepository _verificationRuleShopIdRepository;
        private readonly IReceiveClient _receiveClient;
        private readonly IShopStockClient _shopStockClient;
        private readonly ICouponClient _couponClient;
        private readonly IOrderCouponRepository _orderCouponRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IOrderDiscountRepository _orderDiscountRepository;
        private readonly IDelegateUserOrderRepository _delegateUserOrderRepository;
        private readonly IOrderProductDiscountRepository _orderProductDiscountRepository;
        private readonly IFmsClient _fmsClient;
        private readonly IFranchisesConfigRepository _franchisesConfigRepository;
        private readonly IOrderInsuranceCompanyRepository _orderInsuranceCompanyRepository;
        private readonly IOrderPackageCardRepository _orderPackageCardRepository;
        private readonly IVerificationRulePidRepository _verificationRulePidRepository;
        private readonly IUserClient userClient;
        private readonly IPayClient iPayClient;
        private readonly IOrderRelationRepository _orderRelationRepository;
        private readonly IOrderSaleRepository _orderSaleRepository;
        private readonly IEmployeePerformanceService _employeePerformanceService;

        public OrderCommandService(IMapper mapper, ApolloErpLogger<OrderCommandService> logger, IOrderCommandForCClient orderCommandForCClient, IOrderNotAdapterRepository orderNotAdapterRepository,
            IRgStockClient rgStockClient, IUserClient userClient, IVehicleClient vehicleClient,
            IShopClient shopClient, IProductClient productClient, IOrderClient orderClient, IShopOrderRepository shopOrderRepository, IOrderRepository orderRepository,
            IOrderDispatchRepository orderDispatchRepository, IBaoYangClient baoYangClient,
            IOrderUserRepository orderUserRepository, IOrderCarRepository orderCarRepository,
            IOrderProductRepository orderProductRepository, IOrderAddressRepository orderAddressRepository,
            IVerificationRuleRepository verificationRuleRepository, IEmployeePerformanceService employeePerformanceService,
            IOrderVerificationCodeRepository orderVerificationCodeRepository, IVerificationRuleShopIdRepository verificationRuleShopIdRepository,
            IReceiveClient receiveClient, IShopStockClient shopStockClient, ICouponClient couponClient, IOrderCouponRepository orderCouponRepository, Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOrderDiscountRepository orderDiscountRepository, IDelegateUserOrderRepository delegateUserOrderRepository, IOrderProductDiscountRepository orderProductDiscountRepository, IFmsClient fmsClient,
            IFranchisesConfigRepository franchisesConfigRepository, IOrderSaleRepository orderSaleRepository,
            IOrderInsuranceCompanyRepository orderInsuranceCompanyRepository, IOrderPackageCardRepository orderPackageCardRepository, IVerificationRulePidRepository verificationRulePidRepository, IPayClient iPayClient, IOrderRelationRepository orderRelationRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _orderCommandForCClient = orderCommandForCClient;
            this.orderNotAdapterRepository = orderNotAdapterRepository;
            _rgStockClient = rgStockClient;
            _userClient = userClient;
            _vehicleClient = vehicleClient;
            _shopClient = shopClient;
            _productClient = productClient;
            _orderClient = orderClient;
            _shopOrderRepository = shopOrderRepository;
            _orderRepository = orderRepository;
            _orderDispatchRepository = orderDispatchRepository;
            _baoYangClient = baoYangClient;
            _orderUserRepository = orderUserRepository;
            _orderCarRepository = orderCarRepository;
            _orderProductRepository = orderProductRepository;
            _orderAddressRepository = orderAddressRepository;
            _verificationRuleRepository = verificationRuleRepository;
            _orderVerificationCodeRepository = orderVerificationCodeRepository;
            _verificationRuleShopIdRepository = verificationRuleShopIdRepository;
            _receiveClient = receiveClient;
            _shopStockClient = shopStockClient;
            _couponClient = couponClient;
            _orderCouponRepository = orderCouponRepository;
            _configuration = configuration;
            _orderDiscountRepository = orderDiscountRepository;
            _delegateUserOrderRepository = delegateUserOrderRepository;
            _orderProductDiscountRepository = orderProductDiscountRepository;
            _fmsClient = fmsClient;
            _franchisesConfigRepository = franchisesConfigRepository;
            _orderInsuranceCompanyRepository = orderInsuranceCompanyRepository;
            _orderPackageCardRepository = orderPackageCardRepository;
            _verificationRulePidRepository = verificationRulePidRepository;
            this.iPayClient = iPayClient;
            _orderRelationRepository = orderRelationRepository;
            _orderSaleRepository = orderSaleRepository;
            this._employeePerformanceService = employeePerformanceService;
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var updateResponse = await _orderCommandForCClient.UpdateOrderStatus(request);
            if (updateResponse.Code == ResultCode.Success)
            {
                if (request.UpdateStatusType == Core.Enums.UpdateOrderStatusTypeEnum.InstallStatus)
                {
                    var updateOrderInstallResponse = await _rgStockClient.UpdateOrderInstallReleaseStock(new Core.Request.Stock.UpdateOrderInstallReleaseStockRequest
                    {
                        OrderNo = request?.OrderNos?.FirstOrDefault(),
                        UpdateBy = request.UpdateBy
                    });
                    if (updateOrderInstallResponse.Code != ResultCode.Success)
                    {
                        _logger.Error($"UpdateOrderInstallReleaseStock{ request.OrderNos?.FirstOrDefault()} Exception {updateOrderInstallResponse.Message}", null);
                    }
                }
            }
            return updateResponse;
        }

        /// <summary>
        /// 订单不适配信息提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveOrderNotAdapter(OrderNotAdapterRequest request)
        {
            OrderNotAdapterDO dalRequest = _mapper.Map<OrderNotAdapterDO>(request);

            var saveOrderNotAdapterDalResponse = await orderNotAdapterRepository.SaveOrderNotAdapter(dalRequest);
            if (saveOrderNotAdapterDalResponse > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "提交成功"
                };
            }

            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };

        }

        /// <summary>
        ///  更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderPayStatus(UpdateOrderPayStatusRequest request)
        {
            var updateOrderPayStatus = await _orderRepository.UpdateOrderPayStatus(request);

            if (updateOrderPayStatus > 0)
            {
                await _orderClient.BatchUpdatePayStatus(new Core.Model.Order.BatchUpdatePayStatusRequest()
                {
                    OrderNo = request.OrderNo,
                    UpdateBy = request.UpdateBy,
                    PayMethod = request.PayMethod,
                    PayChannel = request.PayChannel
                });

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
        /// 创建预约到店订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<string>>> CreateOrderForArrivalOrReserver(ApiRequest<CreateOrderForArrivalOrReserverRequest> request)
        {
            // return _orderCommandForCClient

            _logger.Info($"CreateOrderForArrivalOrReserver:{JsonConvert.SerializeObject(request)}");

            var promotionProducts = request.Data?.Items?.Where(_ => !string.IsNullOrEmpty(_.ActivityId))?.ToList();
            if (promotionProducts?.Count() > 0)
            {
                var promotionActivitys = await _productClient.GetPromotionActivitByProductCodeList(new Core.Request.Product.GetPromotionActivitByProductCodeListRequest()
                {
                    ProductCodeList = promotionProducts?.Select(_ => _.Pid)?.ToList(),
                    PromotionChannel = request.Data?.PromotionChannel
                });
                var chkPromotionActivity = (promotionActivitys?.Data?.Count() ?? 0) == 0 ? true : false;
                promotionActivitys.Data?.ForEach(_ =>
                {
                    var pidSumNum = promotionProducts.Where(x => x.Pid == _.ProductCode)?.Sum(x => x.Num) ?? 0;
                    if (pidSumNum > _.AvailQuantity)
                        chkPromotionActivity = true;
                });
                if (chkPromotionActivity)
                {
                    throw new CustomException("活动已结束");
                }
            }


            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
            {
                throw new CustomException("用户信息不存在");
            }

            var getUserVehicleByCarIdResult = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest()
            { UserId = request.Data.UserId, CarId = request.Data.CarId });
            var car = getUserVehicleByCarIdResult?.GetSuccessData();
            if (car == null)
            {
                throw new CustomException("车辆信息不存在");
            }

            if (request.Data.ShopId <= 0)
            {
                throw new CustomException("服务门店不存在");
            }
            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.ShopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
            {
                throw new CustomException("门店信息不存在");
            }



            var createBy = string.IsNullOrEmpty(user.UserName) ? (user.NickName ?? string.Empty) : user.UserName;//创建人

            var codes = request?.Data?.Items?.Select(x => x.Pid)?.ToList();
            var products = await _productClient.GetShopProductByCodes(new Client.Request.Product.GetShopProductByCodeRequest()
            {
                ShopProductCodes = codes
            });

            var productCodes = products?.Data?.Select(_ => _.ProductCode)?.ToList();
            var requestCodes = request.Data.Items?.Select(_ => _.Pid)?.Distinct()?.ToList();
            bool productCodeIsEqual = productCodes.Equal_EveryItem(requestCodes);
            if (!productCodeIsEqual)
            {
                throw new CustomException("产品信息异常");
            }

            var createTime = DateTime.Now;//创建时间
            var syncOrderRequest = new List<SyncOrderRequest>();

            var orderNos = new List<string>();

            List<InsertPromotionActivityOrderRequest> insertPromotionActivityOrderRequest = new List<InsertPromotionActivityOrderRequest>();
            #region 开启事务存储数据
            using (TransactionScope ts = new TransactionScope(asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                request.Data?.Items?.ForEach(_ =>
                {
                    var order = new OrderDO()
                    {
                        Channel = 2,
                        TerminalType = 1,//TODO: 可判断校验request.Channel
                        AppVersion = request.ApiVersion ?? string.Empty,
                        OrderType = _.OrderType > 0 ? (sbyte)_.OrderType : request.Data.OrderType == 0 ? OrderTypeEnum.Other.ToSbyte() : request.Data.OrderType,
                        ProduceType = ProductTypeEnum.Ordinary.ToSbyte(),
                        OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                        OrderTime = createTime,
                        UserId = request.Data.UserId,
                        UserName = createBy,
                        UserPhone = user.UserTel,
                        ContactId = request.Data.UserId,
                        ContactName = createBy,
                        ContactPhone = user.UserTel,
                        TotalProductNum = _.Num,
                        TotalProductAmount = _.Num * _.Price,
                        ServiceNum = 0,
                        ServiceFee = 0,
                        DeliveryFee = 0,
                        TotalCouponAmount = 0,
                        ActualAmount = _.Num * _.Price,
                        PayMethod = PayMethodEnum.ToShop.ToSbyte(),
                        PayChannel = 0,
                        IsNeedInvoice = 0,
                        IsNeedDelivery = 1,
                        DeliveryType = 1,
                        DeliveryMethod = 2,
                        IsNeedInstall = 1,
                        CompanyId = shop.CompanyId,
                        ShopId = request.Data.ShopId,
                        CreateBy = createBy,
                        CreateTime = createTime,
                        UpdateTime = createTime,
                        UpdateBy = createBy
                    };
                    var orderId = orderNotAdapterRepository.Insert<OrderDO>(order);
                    var orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    order.OrderNo = orderNo;
                    orderNos.Add(orderNo);
                    _orderRepository.UpdateOrderNo(orderId, orderNo);

                    if (!string.IsNullOrWhiteSpace(_.ActivityId))
                    {
                        insertPromotionActivityOrderRequest.Add(new InsertPromotionActivityOrderRequest()
                        {
                            OrderNo = orderNo,
                            UserId = request.Data.UserId,
                            SubmitBy = createBy,
                            Products = new List<PromotionProductVo>()
                            {
                                  new PromotionProductVo()
                                  {
                                        ActivityId = _.ActivityId,
                                        Num = _.Num,
                                        ProductCode = _.Pid
                                  }
                            }
                        });
                    }

                    var singleSyncOrderRequest = new SyncOrderRequest();
                    singleSyncOrderRequest.Order = _mapper.Map<MainOrderDTO>(order);

                    var product = products?.Data?.Where(x => x.ProductCode == _.Pid)?.FirstOrDefault();
                    var orderProduct = new OrderProductDO()
                    {
                        OrderId = orderId,
                        OrderNo = orderNo,
                        ProductId = _.Pid,
                        ProductName = product?.ProductName,
                        DisplayName = product?.DisplayName,
                        Description = product?.Description,
                        ProductAttribute = 0,
                        ParentOrderPackagePid = 0,
                        ServeForOrderPids = string.Empty,
                        CategoryCode = product.CategoryId.ToString(),
                        ItemCode = string.Empty,
                        Label = string.Empty,
                        ImageUrl = product.Icon,
                        PriceId = 0,
                        MarketingPrice = _.Price,
                        Price = _.Price,
                        TotalCostPrice = _.Price * _.Num,
                        Unit = "个",
                        Number = 1,
                        TotalNumber = _.Num,
                        StockStatus = 0,
                        Amount = _.Price,
                        TotalAmount = _.Price * _.Num,
                        TaxRate = 0,
                        ShareDiscountAmount = 0,
                        ActualPayAmount = 0,
                        IsDeleted = 0,
                        CreateBy = createBy,
                        UpdateBy = createBy,
                        CreateTime = createTime,
                        UpdateTime = createTime,

                    };
                    orderNotAdapterRepository.Insert<OrderProductDO>(orderProduct);

                    var productOrderDto = _mapper.Map<MainOrderProductDTO>(orderProduct);
                    singleSyncOrderRequest.OrderProducts = new List<MainOrderProductDTO>()
                    {
                        productOrderDto
                    };

                    syncOrderRequest.Add(singleSyncOrderRequest);

                    #region 组装地址
                    OrderAddressDO orderAddressDO = null;

                    orderAddressDO = _mapper.Map<OrderAddressDO>(shop);
                    orderAddressDO.AddressType = 1;
                    orderAddressDO.ShopId = request.Data.ShopId;
                    orderAddressDO.DetailAddress = shop.Address;

                    if (orderAddressDO != null)
                    {
                        orderAddressDO.ReceiverName = createBy;
                        orderAddressDO.ReceiverPhone = shop?.Telephone;
                        orderAddressDO.CreateBy = createBy;
                        orderAddressDO.CreateTime = DateTime.Now;
                    }
                    #endregion

                    orderAddressDO.OrderId = orderId;
                    orderNotAdapterRepository.Insert<OrderAddressDO>(orderAddressDO);

                    #region 组装用户
                    var orderUserDO = _mapper.Map<OrderUserDO>(user);
                    orderUserDO.UserId = request.Data.UserId;
                    #endregion

                    orderUserDO.OrderId = orderId;
                    orderUserDO.CreateBy = createBy;
                    orderUserDO.CreateTime = createTime;
                    orderNotAdapterRepository.Insert<OrderUserDO>(orderUserDO);

                    #region 组装车型
                    var orderCarDO = _mapper.Map<OrderCarDO>(car);
                    #endregion

                    orderCarDO.OrderId = orderId;
                    orderNotAdapterRepository.Insert<OrderCarDO>(orderCarDO);

                });

                ts.Complete();

            }
            #endregion

            ///同步订单
            for (var i = 0; i < syncOrderRequest.Count; i++)
            {
                await _orderClient.SyncOrder(syncOrderRequest[i]);
            }

            ///更新促销扣减库存
            for (var i = 0; i < insertPromotionActivityOrderRequest.Count; i++)
            {
                _logger.Info($"insertPromotionActivityOrderRequest:{JsonConvert.SerializeObject(insertPromotionActivityOrderRequest[i])}");
                await _productClient.InsertPromotionActivityOrder(insertPromotionActivityOrderRequest[i]);
            }

            return new ApiResult<List<string>>()
            {
                Code = ResultCode.Success,
                Message = "创建订单成功",
                Data = orderNos
            };
        }

        /// <summary>
        ///  取消订单For预约Or到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrderForReserverOrArrival(ApiRequest<CancelOrderForReserverOrArrivalRequest> request)
        {
            var updateArrival = await _orderRepository.UpdateOrderCancelForReserverOrArrival(request.Data.OrderNos, request.Data.UserId);

            if (updateArrival)
            {
                var cancelResponse = await _orderClient.CancelOrderForReserverOrArrival(request.Data);
                if (cancelResponse.Code != ResultCode.Success)
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Success,
                        Message = "更新失败"
                    };
                }
            }

            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "更新成功"
            };

        }

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request)
        {
            var result = Result.Failed("处理失败");
            try
            {
                var order = await _shopOrderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var proxyGenerator = new Castle.DynamicProxy.ProxyGenerator();
                    var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(_shopOrderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                    {
                        OrderId = order.Id,
                        CreateBy = request.UpdateBy,
                        UpdateBy = request.UpdateBy,
                        Type1 = OrderLoggerTypeEnum.Order.ToString(),
                        Type2 = OrderLoggerTypeEnum.OrderReverse.ToString(),
                        Filter1 = order.OrderNo,
                        Filter2 = string.Empty,
                        Summary = "更新逆向信息"
                    }));
                    var isUpdateOrderReverseInfoSuccess = await orderLoggerRepository.UpdateOrderReverseInfo(order.Id, (sbyte)request.ReverseStatus, request.RefundStatus, request.UpdateBy);
                    if (!isUpdateOrderReverseInfoSuccess)
                    {
                        throw new Exception("更新逆向信息异常");
                    }
                    if (order.OrderStatus != (sbyte)OrderStatusEnum.Canceled)
                    {
                        switch (request.ApplyType)
                        {
                            case ReverseApplyTypeEnum.None:
                                break;
                            case ReverseApplyTypeEnum.Cancel:
                                var orderLoggerRepositoryOrderStatus = proxyGenerator.CreateInterfaceProxyWithTarget(_shopOrderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                                {
                                    OrderId = order.Id,
                                    CreateBy = request.UpdateBy,
                                    UpdateBy = request.UpdateBy,
                                    Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                    Type2 = OrderLoggerTypeEnum.OrderStatus.ToString(),
                                    Filter1 = order.OrderNo,
                                    Filter2 = string.Empty,
                                    Summary = "修改订单状态"
                                }));
                                var isUpdateOrderMainStatusSuccess = await orderLoggerRepositoryOrderStatus.UpdateOrderMainStatus(order.Id, (sbyte)OrderStatusEnum.Canceled, request.UpdateBy);
                                if (!isUpdateOrderMainStatusSuccess)
                                {
                                    throw new Exception("修改订单状态异常");
                                }
                                break;
                            case ReverseApplyTypeEnum.Refund:
                                break;
                            case ReverseApplyTypeEnum.Change:
                                break;
                            default:
                                break;
                        }
                    }
                    //通知主库
                    var orderReverseNotifyResult = await _orderClient.OrderReverseNotify(request);
                    if (!orderReverseNotifyResult.IsNotNullSuccess())
                    {
                        throw new Exception("通知主库异常");
                    }
                    ts.Complete();
                }
                result = Result.Success("处理成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("OrderReverseNotifyEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 更改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderCompleteStatus(BatchUpdateCompleteStatusRequest request)
        {
            var updateOrderPayStatus = await _orderRepository.UpdateOrderCompleteStatus(request);

            if (updateOrderPayStatus > 0)
            {
                await _orderClient.BatchUpdateCompleteStatus(request);

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
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(BatchUpdateOrderRequest request)
        {
            _logger.Info($"BatchUpdateOrderProductCostPriceActualPayAmount：{JsonConvert.SerializeObject(request)} ");
            var result = Result.Success("处理成功");
            try
            {
                DateTime startDateTime = Convert.ToDateTime("1901-01-01");
                DateTime endDateTime = startDateTime;
                if (request.OrderDate.Length < 7)
                { return result; }
                else if (request.OrderDate.Length < 8)
                {
                    startDateTime = Convert.ToDateTime(request.OrderDate + "-01");
                    endDateTime = startDateTime.AddMonths(1);
                }
                else
                {
                    startDateTime = Convert.ToDateTime(request.OrderDate);
                    endDateTime = startDateTime.AddDays(1);
                }

                var orderList = (await _orderRepository.GetSimpleOrderList(new GetOrderListRequest()
                {
                    ShopId = request.ShopId,
                    OrderNos = request.OrderNos,
                    StartDate = startDateTime,
                    EndDate = endDateTime
                }))?.Where(x => x.OrderStatus != 40);
                _logger.Info($"orderList：{JsonConvert.SerializeObject(orderList)} ");

                if (orderList != null && orderList.Any())
                {
                    await UpdateOrderProductCostPrice(new BatchUpdateCompleteStatusRequest()
                    { 
                        ShopId = request.ShopId,
                        UpdateBy = request.UpdateBy,
                        OrderNo = orderList.Select(x=>x.OrderNo).ToList()
                    });


                    await UpdateOrderProductActualPayAmount(new BatchUpdateCompleteStatusRequest()
                    {
                        ShopId = request.ShopId,
                        UpdateBy = request.UpdateBy,
                        OrderNo = orderList.Select(x => x.OrderNo).ToList()
                    });

                }

            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Exception;
                result.Message = ex.Message;
                _logger.Error($"BatchUpdateOrderProductCostPriceActualPayAmount request:{JsonConvert.SerializeObject(request)}", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新订单产品的成本价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderProductCostPrice(BatchUpdateCompleteStatusRequest request)
        {
            _logger.Info($"UpdateOrderProductCostPrice：{JsonConvert.SerializeObject(request)} ");
            int result = 0;
            foreach (var orderNo in request.OrderNo)
            {
                var orderInfo = await _orderRepository.GetOrder(orderNo);
                //取订单明细
                var orderProducts = await _orderProductRepository.GetOrderProducts(orderInfo.Id);

                //平台产品(实物、服务、数字等)
                var selfProducts = orderProducts.Where(x => x.ProductAttribute < 4 )?.ToList();
                //服务产品
                var serviceProducts = orderProducts.Where(x => x.ProductAttribute == ProductAttributeEnum.ServiceProduct.ToSbyte())?.ToList();
                //门店外采产品
                var shopProducts = orderProducts.Where(x => x.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte())?.ToList();

                //取平台产品详情
                var productDetails = new List<ProductDetailDTO>();
                if (selfProducts != null && selfProducts.Any())// 
                {
                    var productDetailSearchRequest = new ProductDetailSearchRequest() {
                        ProductCodes = selfProducts.Select(_ => _.ProductId).ToList()
                    };
                    var getProductsByProductCodesResult = await _productClient.GetProductsByProductCodes(productDetailSearchRequest);
                    _logger.Info($"UpdateOrderProductCostPrice productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} getProductsByProductCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
                    productDetails = getProductsByProductCodesResult?.GetSuccessData();//商品详情数据
                    if (productDetails == null || !productDetails.Any())
                    {
                        _logger.Warn($"商品：{string.Join(";", productDetailSearchRequest.ProductCodes.ToString())}未找到");
                        throw new CustomException(CommonConstant.ProductInfomationException);
                    }

                }

                //取门店外采产品详情
                var shopProductDetails = new List<GetShopProductByCodeResponse>();
                if (shopProducts != null && shopProducts.Any())// 
                {
                    var productDetailSearchRequest = new GetShopProductByCodeRequest()
                    {
                        ShopProductCodes = shopProducts.Select(_ => _.ProductId).ToList()
                    };

                    var getProductsByProductCodesResult = await _productClient.GetShopProductByCodes(productDetailSearchRequest);

                    _logger.Info($"UpdateOrderProductCostPrice productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} GetShopProductByCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
                    shopProductDetails = getProductsByProductCodesResult?.GetSuccessData();//商品详情数据
                    if (shopProductDetails == null || !shopProductDetails.Any())
                    {
                        _logger.Warn($"外采商品：{string.Join(";", productDetailSearchRequest.ShopProductCodes.ToString())}未找到");
                        throw new CustomException(CommonConstant.ProductInfomationException);
                    }

                }

                //取门店上架服务
                var serviceCosts = new List<GetShopServiceListWithPIDResponse>();
                if (serviceProducts != null && serviceProducts.Any())
                {
                    var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
                    {
                        ShopId = request.ShopId,
                        ProductIds = serviceProducts.Select(_ => _.ProductId).ToList()
                    }); 
                    _logger.Info($"UpdateOrderProductCostPrice  getShopServiceListWithPIDResult={JsonConvert.SerializeObject(getShopServiceListWithPIDResult)}");

                    serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
                }
                    

                //先更新平台产品和外采产品的成本，
                foreach (var orderProduct in orderProducts)
                {
                    //服务产品
                    if (orderProduct.ProductAttribute == ProductAttributeEnum.ServiceProduct.ToSbyte())
                    {
                        orderProduct.TotalCostPrice = 0;
                        //服务产品先取结算价做成本，无的话再取产品成本
                        var findService = serviceCosts?.Where(x => x.PID == orderProduct.ProductId && x.IsOnline)?.FirstOrDefault();
                        if ( findService != null && findService.CostPrice > 0)
                        {
                            orderProduct.SettlementAmount = (findService.CostPrice) * orderProduct.TotalNumber;
                        }
                        else 
                        {
                            var findProduct = productDetails.Where(x => x.Product.ProductCode == orderProduct.ProductId).FirstOrDefault().Product;
                            orderProduct.SettlementAmount = (findProduct.SettlementPrice > 0 ? findProduct.SettlementPrice : findProduct.SalesPrice) * orderProduct.TotalNumber;
                        }
                    }
                    else
                    {
                        if (orderProduct.ProductAttribute < 4)
                        {
                            var findProduct = productDetails.Where(x => x.Product.ProductCode == orderProduct.ProductId).FirstOrDefault().Product;
                            orderProduct.TotalCostPrice = (findProduct.SettlementPrice > 0 ? findProduct.SettlementPrice : findProduct.SalesPrice) * orderProduct.TotalNumber;
                        }
                        else if (orderProduct.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte())
                        {
                            var findProduct = shopProductDetails.Where(x => x.ProductCode == orderProduct.ProductId).FirstOrDefault();
                            orderProduct.TotalCostPrice = (findProduct.PurchaseCost > 0 ? findProduct.PurchaseCost : findProduct.PurchasePrice) * orderProduct.TotalNumber;
                        }
                    }

                }

                //更新套餐成本
                foreach (var item in orderProducts)
                {
                    //如果有套餐明细
                    var packageProducts = orderProducts.Where(x => x.ParentOrderPackagePid == item.Id).ToList();
                    if (packageProducts != null && packageProducts.Any())
                    {
                        //实物成本
                        var rProducts = packageProducts.Where(x => x.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte()
                            || x.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.ToList();
                        item.TotalCostPrice = rProducts?.Sum(x => x.TotalCostPrice)?? 0;

                        //服务结算金额
                        var sProducts = packageProducts.Where(x => x.ProductAttribute == ProductAttributeEnum.ServiceProduct.ToSbyte())?.ToList();
                        item.SettlementAmount = sProducts?.Sum(x => x.SettlementAmount) ?? 0;

                    }
                }
                _logger.Info($"计算结果：{JsonConvert.SerializeObject(orderProducts)} ");

                foreach (var orderProduct in orderProducts)
                {
                    orderProduct.UpdateBy = request.UpdateBy;
                    orderProduct.UpdateTime = DateTime.Now;
                    result += await _orderProductRepository.UpdateAsync(orderProduct,
                        new List<string>() { "TotalCostPrice", "SettlementAmount",  "UpdateBy", "UpdateTime" });
                }

            }

            if (result > 0)
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
        /// 更新订单产品的优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderProductActualPayAmount(BatchUpdateCompleteStatusRequest request)
        {
            _logger.Info($"UpdateOrderProductActualPayAmount：{JsonConvert.SerializeObject(request)} ");
            int result = 0;
            foreach (var orderNo in request.OrderNo)
            {
                var orderInfo = await _orderRepository.GetOrder(orderNo);
                if (orderInfo == null) { continue; }

                //更新主表优惠
                if (orderInfo.ProduceType == BuyProductTypeEnum.UsePackageCard.ToSbyte())
                {
                    _logger.Info($"更新套餐卡主表：{JsonConvert.SerializeObject(orderInfo)} ");
                    var codeDO = (await _orderPackageCardRepository.GetOrderPackageCard(new GetOrderPackageCardRequest()
                    { VerifyOrderNos = new List<string> { orderNo } })).FirstOrDefault();
                    if (codeDO != null)
                    {
                        orderInfo.ActualAmount = codeDO.AvgPrice * codeDO.Num;
                        orderInfo.TotalCouponAmount = orderInfo.TotalProductAmount - orderInfo.ActualAmount;
                        orderInfo.UpdateBy = request.UpdateBy;
                        orderInfo.UpdateTime = DateTime.Now;

                        await _orderRepository.UpdateAsync(orderInfo,
                            new List<string>() { "ActualAmount", "TotalCouponAmount", "UpdateBy", "UpdateTime" });
                    }

                }
                //取订单明细
                var orderProducts = await _orderProductRepository.GetOrderProducts(orderInfo.Id);

                var totalProductAmount = orderInfo.TotalProductAmount;
                var totalCouponAmount = orderInfo.TotalCouponAmount + orderInfo.OtherCouponAmount;
                #region 分摊明细优惠金额
                //不加条件，0元时候也要算明细实付金额
                if (true)// (totalProductAmount >0 && totalCouponAmount >0)
                {
                    var totalProductB = totalProductAmount;
                    var totalCouponB = totalCouponAmount >= totalProductAmount ? totalProductAmount : totalCouponAmount;
                    var couponRate = (totalCouponB >= totalProductB || totalProductB == 0) ? 1 : totalCouponB / totalProductB;

                    //套餐和单品
                    var packageOrProducts = orderProducts.Where(x => x.ParentOrderPackagePid == 0).ToList();

                    packageOrProducts.ForEach(_ =>
                    {
                        _.ShareDiscountAmount = _.TotalAmount <= 0 ? 0 : (Math.Floor(_.TotalAmount * couponRate));
                        _.ActualPayAmount = _.TotalAmount - _.ShareDiscountAmount;
                        _.ActualPayAmount = _.ActualPayAmount < 0 ? 0 : _.ActualPayAmount;
                    });
                    var tempCouponB = packageOrProducts.Sum(x => x.ShareDiscountAmount);
                    if (tempCouponB != totalCouponB)
                    {
                        var maxProduct = packageOrProducts.OrderByDescending(x => x.ShareDiscountAmount).FirstOrDefault();
                        maxProduct.ShareDiscountAmount += (totalCouponB - tempCouponB);
                        maxProduct.ActualPayAmount = maxProduct.TotalAmount - maxProduct.ShareDiscountAmount;
                        maxProduct.ActualPayAmount = maxProduct.ActualPayAmount < 0 ? 0 : maxProduct.ActualPayAmount;
                    }

                    foreach (var item in packageOrProducts)
                    {
                        //如果有套餐明细
                        var packageProducts = orderProducts.Where(x => x.ParentOrderPackagePid == item.Id).ToList();
                        if (packageProducts != null && packageProducts.Any())
                        {
                            var totalProductA = packageProducts.Sum(x => x.TotalAmount);
                            var totalCouponA = totalProductA - item.ActualPayAmount;
                            var couponRateA = (totalCouponA >= totalProductA || totalProductA == 0) ? 1 : totalCouponA / totalProductA;

                            packageProducts.ForEach(_ =>
                            {
                                _.ShareDiscountAmount = _.TotalAmount <= 0 ? 0 : (Math.Floor(_.TotalAmount * couponRateA));
                                _.ActualPayAmount = _.TotalAmount - _.ShareDiscountAmount;
                                _.ActualPayAmount = _.ActualPayAmount < 0 ? 0 : _.ActualPayAmount;
                            });
                            var tempCouponA = packageProducts.Sum(x => x.ShareDiscountAmount);
                            if (tempCouponA != totalCouponA)
                            {
                                var maxProduct = packageProducts.OrderByDescending(x => x.ShareDiscountAmount).FirstOrDefault();
                                maxProduct.ShareDiscountAmount += (totalCouponA - tempCouponA);
                                maxProduct.ActualPayAmount = maxProduct.TotalAmount - maxProduct.ShareDiscountAmount;
                                maxProduct.ActualPayAmount = maxProduct.ActualPayAmount < 0 ? 0 : maxProduct.ActualPayAmount;
                            }

                        }
                    }

                    _logger.Info($"计算结果：{JsonConvert.SerializeObject(orderProducts)} ");
                    foreach (var orderProduct in orderProducts)
                    {
                        orderProduct.UpdateBy = request.UpdateBy;
                        orderProduct.UpdateTime = DateTime.Now;
                        result += await _orderProductRepository.UpdateAsync(orderProduct,
                            new List<string>() { "ShareDiscountAmount", "ActualPayAmount", "UpdateBy", "UpdateTime" });
                    }
                }
                #endregion

            }

            if (result > 0)
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

        /// 添加派工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveOrderDispatch(ApiRequest<List<SaveOrderDispatchRequest>> request)
        {
            //var getShopArrivalOrders = await _receiveClient.GetShopArrivalOrder(new GetShopArrivalOrderRequest()
            //{
            //    OrderNos = request.Data?.Select(_ => _.OrderNo)?.ToList()
            //});

            var orderNos = request.Data?.Select(_ => _.OrderNo)?.ToList();
            var orderBaseInfo = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = orderNos
            });

            //if (orderBaseInfo?.Data?.FirstOrDefault()?.ProduceType != ProductTypeEnum.OfficeOrder.ToInt())
            //{
            //    var getOrderNos = getShopArrivalOrders?.Data?.Select(_ => _.OrderNo)?.ToList();
            //    bool OrderNoIsEqual = orderNos.Equal_EveryItem(getOrderNos);
            //    if (!OrderNoIsEqual)
            //    {
            //        throw new CustomException("订单未关联到店记录");
            //    }
            //}

            List<OrderDispatchDO> orderDispatchDOs = new List<OrderDispatchDO>();
            request.Data?.ForEach(_ =>
            {
                orderDispatchDOs.Add(new OrderDispatchDO()
                {
                    OrderNo = _.OrderNo,
                    TechId = _.TechId,
                    TechName = _.TechName,
                    Level = _.Level,
                    ShopId = _.ShopId,
                    Percent = _.PercentValue,
                    CreateBy = _.CreateBy,
                    UpdateBy = _.CreateBy,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                });
            });

            if (orderDispatchDOs?.Count() > 0)
            {
                await _orderDispatchRepository.InsertBatchAsync(orderDispatchDOs);

                if (orderNos?.Count() > 0)
                {
                    if (orderNos?.FirstOrDefault()?.StartsWith("RGB") ?? false)
                        await _orderRepository.UpdateOrderDispatchStatus(orderNos, request?.Data?.Select(_ => _.CreateBy)?.FirstOrDefault() ?? "System");
                    if (orderNos?.FirstOrDefault()?.StartsWith("RGC") ?? false)
                        await _orderCommandForCClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                        {
                            OrderNos = orderNos,
                            CreateBy = request?.Data?.Select(_ => _.CreateBy)?.FirstOrDefault() ?? "System"
                        });
                    await _orderClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                    {
                        OrderNos = orderNos,
                        CreateBy = request?.Data?.Select(_ => _.CreateBy)?.FirstOrDefault() ?? "System"
                    });
                }
            }

            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "派工成功"
            };
        }

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request)
        {
            var installStatus = await _orderRepository.BatchUpdateInstallStatus(request);
            if (installStatus > 0)
            {
                await _orderClient.BatchUpdateInstallStatus(new BatchUpdateInstallStatusRequest()
                {
                    OrderNo = request.OrderNo,
                    UpdateBy = request.UpdateBy
                });

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

        public async Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request)
        {
            var result = new ApiResult<bool>()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ParameterNone
            };
            try
            {
                var order = await _orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<bool>(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                if (order.TotalProductAmount + order.DeliveryFee - request.TotalCouponAmount != request.ActualAmount)
                {
                    result = Result.Failed<bool>("订单金额不正确：" + (order.TotalProductAmount - order.DeliveryFee - request.TotalCouponAmount).ToString());
                    return result;
                }
                bool isSuccess = await _orderRepository.UpdateCouponAmount(request.OrderNo, request.TotalCouponAmount, request.ActualAmount, request.Remark, request.UpdateBy);

                if (isSuccess)
                {
                    await UpdateOrderProductActualPayAmount(new BatchUpdateCompleteStatusRequest() { 
                        ShopId = order.ShopId,
                        OrderNo = new List<string> { request.OrderNo },
                        UpdateBy = request.UpdateBy
                    });
                    result = Result.Success(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCouponAmount", ex);
                result = Result.Exception<bool>(ex.Message);
            }
            return result;
        }

        public async Task<ApiResult<bool>> UpdateOrderOtherCouponAmount(UpdateOtherCouponAmountRequest request)
        {
            var result = new ApiResult<bool>()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ParameterNone
            };
            try
            {
                var order = await _orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<bool>(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                //if (order.TotalProductAmount + order.DeliveryFee - request.TotalCouponAmount != request.ActualAmount)
                //{
                //    result = Result.Failed<bool>("订单金额不正确：" + (order.TotalProductAmount - order.DeliveryFee - request.TotalCouponAmount).ToString());
                //    return result;
                //}
                order.OtherCouponAmount = request.CouponAmount;
                order.ActualAmount = order.TotalProductAmount + order.DeliveryFee - order.TotalCouponAmount - order.OtherCouponAmount;
                order.ActualAmount = order.ActualAmount < 0 ? 0 : order.ActualAmount;
                order.Remark += request.Remark;
                order.UpdateBy = request.UpdateBy;
                order.UpdateTime = DateTime.Now;
                var iResult = await _orderRepository.UpdateAsync(order, new List<string> { "OtherCouponAmount", "ActualAmount", "Remark", "UpdateBy", "UpdateTime" });

                if (iResult > 0)
                {
                    await UpdateOrderProductActualPayAmount(new BatchUpdateCompleteStatusRequest()
                    {
                        ShopId = order.ShopId,
                        OrderNo = new List<string> { request.OrderNo },
                        UpdateBy = request.UpdateBy
                    });
                    result = Result.Success(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCouponAmount", ex);
                result = Result.Exception<bool>(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request)
        {
            _logger.Info($"SubmitOrder:{JsonConvert.SerializeObject(request)}");

            long shopId = request.Data.ShopId;
            if (request.Data.ProduceType == ProductTypeEnum.DelegateOrder.ToSbyte())
            {
                shopId = request.Data.DelegateUserShopId;
            }
            #region 基础验证
            //检查促销活动是否结束
            var promotionProducts = request.Data?.ProductInfos?.Where(_ => !string.IsNullOrEmpty(_.ActivityId))?.ToList();

            List<GetPromotionActivitByProductCodeListResponse> promotionActivityData = new List<GetPromotionActivitByProductCodeListResponse>();
            if (promotionProducts?.Count() > 0)
            {
                var promotionActivity = await _productClient.GetPromotionActivitByProductCodeList(new Core.Request.Product.GetPromotionActivitByProductCodeListRequest()
                {
                    ProductCodeList = promotionProducts?.Select(_ => _.Pid)?.ToList(),
                    PromotionChannel = request.Data?.PromotionChannel
                });
                promotionActivityData = promotionActivity?.Data;
                var chkPromotionActivity = (promotionActivity?.Data?.Count() ?? 0) == 0 ? true : false;
                promotionActivity?.Data?.ForEach(_ =>
                {
                    var pidSumNum = promotionProducts.Where(x => x.Pid == _.ProductCode)?.Sum(x => x.Number) ?? 0;
                    if (pidSumNum > _.AvailQuantity)
                        chkPromotionActivity = true;
                });
                if (chkPromotionActivity)
                    throw new CustomException(CommonConstant.ActivityIsFinished);
            }
            //检查用户信息
            var user = new UserInfoResponse();
            if (request.Data.ProduceType == BuyProductTypeEnum.QuickOrder.ToSbyte())
            {
                user.UserId = Guid.NewGuid().ToString();
                user.UserName = request.Data.ReceiverName;
                user.NickName = request.Data.ReceiverName;
                user.UserTel = request.Data.ReceiverPhone;
                user.UserTelDes = request.Data.ReceiverPhone;
                #region 把员工信息当客户
                //var getEmployeeInfoResult = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
                //{
                //    EmployeeId = request.Data.UserId
                //});

                //var employee = getEmployeeInfoResult?.GetSuccessData();
                //if (employee == null)
                //    throw new CustomException(CommonConstant.UserInformationIsNull);

                //user = new UserInfoResponse()
                //{
                //    UserId = request.Data.UserId,
                //    NickName = employee.Name,
                //    UserName = employee.Name,
                //    UserTel = employee.Mobile,
                //    UserTelDes = employee.Mobile
                //};
                #endregion
            }
            else
            {
                var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });
                user = getUserInfoResult?.GetSuccessData();
                if (user == null)
                    throw new CustomException(CommonConstant.UserInformationIsNull);
            }

            //检查车辆信息
            var car = new UserVehicleDTO();
            if (request.Data.ProduceType == BuyProductTypeEnum.QuickOrder.ToSbyte())
            {
                car.CarNumber = request.Data.CarNumber;
            }
            else if (!string.IsNullOrEmpty(request.Data.CarId)) //允许车辆为空
            {
                var getUserVehicleByCarIdResult = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest()
                { UserId = request.Data.UserId, CarId = request.Data.CarId });
                car = getUserVehicleByCarIdResult?.GetSuccessData();
                if (car == null)
                    throw new CustomException(CommonConstant.CarInformationIsNull);
            }
            
            //检查门店信息
            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = shopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);

            //检查预约时间
            var isCovertSuccess = DateTime.TryParse(request.Data.ReserverTime, out var reserverTime);

            if (isCovertSuccess)
            {
                var IsSatisfyReserver = await _receiveClient.CheckTheDayReserveWithUserCarId(new Client.Request.Receive.CheckReserveTimeRequest()
                {
                    CarId = car.CarId,
                    ReserveTime = reserverTime,
                    ShopId = shopId,
                    UserId = user.UserId
                });
                if (IsSatisfyReserver?.Data ?? false)
                    throw new CustomException(CommonConstant.ReserverTimeHaveExist);
            }

            #endregion

            //上门服务
            if (request.Data.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
            {
                request.Data.ProductInfos.Add(new SelectedProductInfoDTO()
                {
                    Pid = "BYFW-SMFW-SSMFW-2-FU",
                    Number = 1
                });
            }

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
            getPackageProductServicesRequest.CityId = shop.CityId;


            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();//自营产品
            var shopProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();//门店项目
            var shopPurchaseProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 2)?.ToList();//外采产品

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            var services = new List<OrderConfirmProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest, request.Data.UpdateProductInfos);
                products = getPackageProductServicesResponse.Products;
                services = getPackageProductServicesResponse.Services;
            }

            #endregion

          
            #region 验证服务是否在门店上架
            var allServiceProductIds = new List<string>();
            foreach (var item in products)
            {
                if (item.PackageOrProduct.ProductAttribute == 2)
                {
                    allServiceProductIds.Add(item.PackageOrProduct.ProductId);
                }
                if (item.PackageProducts != null && item.PackageProducts.Any())
                {
                    item.PackageProducts.ForEach(_ =>
                    {
                        if (_.ProductAttribute == 2)
                        {
                            allServiceProductIds.Add(_.ProductId);
                        }
                    });
                }
            }
            foreach (var item in services)
            {
                allServiceProductIds.Add(item.ProductId);
            }
            var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
            {
                ShopId = shopId,
                ProductIds = allServiceProductIds.ToList()
            });
            var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
            if (serviceCosts != null)
            {
                //项目里面排查2个固定的pid
                var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false && (_.PID != "MF-XC-PX-2-FU" && _.PID != "FW-BY-JC-2-FU"));//服务上下架校验
                if (findOfflineServices != null && findOfflineServices.Any())
                {
                    //2023-3-29先不验证服务是否在门店上架
                    //throw new CustomException(CommonConstant.ServiceShopNotSupport);
                }
            }
            #endregion
            

            #region 计算成本
            serviceCosts?.ForEach(_ =>
            {
                if (_.CostPrice > 0)
                {
                    var product = products?.SelectMany(item => item.PackageProducts)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (product != null)
                    {
                        product.TotalCostPrice = _.CostPrice * product.TotalNumber;
                    }
                    var packageProduct = products?.Select(item => item.PackageOrProduct)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (packageProduct != null)
                    {
                        packageProduct.TotalCostPrice = _.CostPrice * packageProduct.TotalNumber;
                    }
                    var service = services?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (service != null)
                    {
                        service.TotalCostPrice = _.CostPrice * service.TotalNumber;
                    }
                }
                else //非门店上架的服务，取产品信息里的门店结算价，为特殊产品使用。
                {
                    var product = products?.SelectMany(item => item.PackageProducts)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (product != null)
                    {
                        product.TotalCostPrice = product.SettlementPrice * product.TotalNumber;
                    }
                    var packageProduct = products?.Select(item => item.PackageOrProduct)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (packageProduct != null)
                    {
                        packageProduct.TotalCostPrice = packageProduct.SettlementPrice * packageProduct.TotalNumber;
                    }
                    var service = services?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (service != null)
                    {
                        service.TotalCostPrice = service.SettlementPrice * service.TotalNumber;
                    }
                }
            });

            #endregion
          

            #region 添加项目

            getPackageProductServicesRequest.ProductInfos = shopProducts;

            List<OrderProductDiscountDO> orderProductDiscountDOs = new List<OrderProductDiscountDO>(3);

            if (shopProducts?.Count() > 0)
            {
                var shopItems = await GetShopProduct(getPackageProductServicesRequest, promotionActivityData, request.Data.ProduceType);
                if (shopItems?.Products?.Count() > 0)
                {
                    products.AddRange(shopItems?.Products);
                    if (request.Data.ProduceType == ProductTypeEnum.DelegateOrder.ToSbyte())
                    {
                        shopItems?.Products?.ForEach(_ =>
                        {
                            orderProductDiscountDOs.Add(new OrderProductDiscountDO()
                            {
                                DiscountRate = _.PackageOrProduct?.DiscountRate ?? 0,
                                DiscountPrice = _.PackageOrProduct?.DiscountPrice ?? 0,
                                ProductId = _.PackageOrProduct?.ProductId,
                                Price = _.PackageOrProduct?.OriginPrice ?? 0
                            });
                        });

                    }
                }

            }


            #endregion

            #region 更新产品备注

            products?.ForEach(_ =>
            {
                var product = request.Data?.ProductInfos?.Where(item => item.Pid == _.PackageOrProduct.ProductId)?.FirstOrDefault();
                if (product != null)
                {
                    _.PackageOrProduct.Remark = product.Remark ?? "";
                }
                else
                {
                    _.PackageOrProduct.Remark = string.Empty;
                }
            });
            services?.ForEach(_ =>
            {
                var product = request.Data?.ProductInfos?.Where(item => item.Pid == _.ProductId)?.FirstOrDefault();
                if (product != null)
                {
                    _.Remark = product.Remark ?? "";
                }
                else
                {
                    _.Remark = string.Empty;
                }
            });
            #endregion


            #region 外采产品

            getPackageProductServicesRequest.ProductInfos = shopPurchaseProducts;

            if (shopPurchaseProducts?.Count() > 0)
            {
                var shopItems = await GetShopPurchaseProduct(getPackageProductServicesRequest);
                if (shopItems?.Products?.Count() > 0)
                {
                    products.AddRange(shopItems?.Products);
                }
            }

            #endregion

            #region 团购产品修改价格
            //txw,2023-4,直接取前端传来的价格
            //var getShopGroupProducts = await _shopClient.GetShopGrouponProduct(new GetShopGrouponProductRequest()
            //{
            //    ShopId = shopId,
            //    Status = 1
            //});

            //products?.ForEach(_ =>
            //{
            //    var getShopGroupProduct = getShopGroupProducts?.Data?.Where(t => t.ServiceId == _.PackageOrProduct.ProductId)?.FirstOrDefault();
            //    if (getShopGroupProduct != null)
            //    {
            //        _.PackageOrProduct.Price = getShopGroupProduct.Price;
            //        _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * getShopGroupProduct.Price;
            //    }
            //});

            #endregion

            #region 金额计算

            //价格取前端传来的价格.
            products?.ForEach(_ =>
            {
                var requestProduct = requestData?.ProductInfos?.Where(t => t.Pid == _.PackageOrProduct.ProductId)?.FirstOrDefault();
                if (requestProduct != null)
                {
                    _.PackageOrProduct.Price = requestProduct.Price;
                    _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * requestProduct.Price;
                }
            });

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = requestData.OtherCouponAmount; //decimal.Zero;//总优惠金额

            var userCouponId = requestData.UserCouponId;//用户选择的优惠券ID
            UserCouponEntityCustomResponse userCoupon = null;//用户优惠券信息
            if (userCouponId > 0)
            {
                //获取优惠券具体信息
                var getUserCouponEntityCustomByIdResult = await _couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdRequest() { UserCouponId = userCouponId });
                userCoupon = getUserCouponEntityCustomByIdResult.GetSuccessData();
                if (userCoupon != null)
                {
                    totalCouponAmount += userCoupon.Value;
                }
            }

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            // createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.UserName : request.Data.ReceiverName;
            // telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;

            var userName = string.Empty;

            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                userName = user.UserName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(user.NickName))
                {
                    userName = user.NickName;
                }
                else
                {
                    userName = user.UserTel;
                }
            }

            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? userName : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;

            if (string.IsNullOrEmpty(request.Data.CreatedBy))
            {
                createBy = userName;
            }
            else
            {
                createBy = request.Data.CreatedBy;
            }

            var createTime = DateTime.Now;//创建时间

            #region 用户为钻石会员 价格 折扣处理

            //钻石折扣
            // var payOrderPointValue = Convert.ToInt32(_configuration["OrderConfig:PayOrderPointValue"]);
            OrderDiscountDO orderDiscountDO = null;
            //if ((user.MemberGrade) == 50)//钻石折扣
            //{
            //    var diamondsDiscountRate = Convert.ToDecimal(_configuration["OrderConfig:DiamondsDiscountRate"]);

            //    var diamondsDiscountAmount = Math.Ceiling((actualAmount * diamondsDiscountRate) * 100) / 100;//超出分的金额直接进位
            //    orderDiscountDO = new OrderDiscountDO()
            //    {
            //        ActualAmount = actualAmount,
            //        CreateBy = request.Data.CreatedBy ?? string.Empty,
            //        CreateTime = DateTime.Now,
            //        DiscountAmount = diamondsDiscountAmount,
            //        DiscountRate = (float)diamondsDiscountRate,
            //        DiscountType = 1,
            //        Formula = "ActualAmount*DiscountRate",
            //        OrderNo = string.Empty,
            //        UpdateBy = string.Empty
            //    };


            //    actualAmount = diamondsDiscountAmount;
            //}


            #endregion

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = user.UserId,
                UserName = userName,
                UserPhone = user.UserTel ?? string.Empty,
                ContactId = user.UserId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = shop.CompanyId,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = (selfProducts.Count() == 0 && shopPurchaseProducts?.Count() == 0) ? (sbyte)1 : (sbyte)0,
                SignTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户
            user.BirthDay = string.IsNullOrEmpty(user.BirthDay) ? "1900-01-01" : user.BirthDay;

            var orderUserDO = _mapper.Map<OrderUserDO>(user);
            //orderUserDO.UserId = request.Data.UserId;

            #endregion

            #region 组装地址,到店订单无需地址
            OrderAddressDO orderAddressDO = null;

            //orderAddressDO = _mapper.Map<OrderAddressDO>(shop);
            //orderAddressDO.AddressType = 1;
            //orderAddressDO.ShopId = shopId;
            //orderAddressDO.DetailAddress = shop.Address;

            if (requestData.ReceiverAddressId > 0)
            {
                var getUserAddressResult = await _userClient.GetUserAddress(new UserAddressRequest() { UserId = user.UserId });
                var userAddresses = getUserAddressResult?.GetSuccessData();
                if (userAddresses != null)
                {
                    var address = userAddresses.Find(_ => _.AddressId == requestData.ReceiverAddressId);
                    orderAddressDO = _mapper.Map<OrderAddressDO>(address);
                    orderAddressDO.AddressType = 2;
                    orderAddressDO.AddressId = requestData.ReceiverAddressId;
                    orderAddressDO.Label = address.AddressTag;
                    orderAddressDO.IsDefault = (sbyte)(address.DefaultAddress ? 1 : 0);
                    orderAddressDO.DetailAddress = address.AddressLine;
                }
            }

            if (orderAddressDO != null)
            {
                orderAddressDO.ReceiverName = contactPhone;
                orderAddressDO.ReceiverPhone = contactPhone;
                orderAddressDO.CreateBy = createBy;
                orderAddressDO.CreateTime = DateTime.Now;
            }
            #endregion

            #region 组装车型
            var orderCarDO = _mapper.Map<OrderCarDO>(car);
            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                // 存车辆
                if (!string.IsNullOrWhiteSpace(orderCarDO.CarNumber))
                {
                    orderCarDO.OrderId = orderId;
                    orderCarDO.CreateBy = createBy;
                    orderCarDO.CreateTime = createTime;
                    await _orderCarRepository.InsertAsync(orderCarDO);
                }

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                //存储单独带出的服务
                var serviceProductDOs = new List<OrderProductDO>();
                foreach (var item in services)
                {
                    var serveForProductCodes = item.ServeForProductCodes.TrimEnd(';').Split(';');

                    var serveForProducts = allInsertedProducts.FindAll(_ => serveForProductCodes.Contains(_.ProductId));
                    var serveForOrderPids = new StringBuilder();
                    if (serveForProducts != null && serveForProducts.Any())
                    {
                        foreach (var serveForProduct in serveForProducts)
                        {
                            serveForOrderPids.Append($"{serveForProduct.ProductId};");
                        }
                    }
                    item.ServeForOrderPids = serveForOrderPids.ToString().TrimEnd(';');
                    var serviceProductDO = _mapper.Map<OrderProductDO>(item);
                    serviceProductDO.OrderId = orderId;
                    serviceProductDO.OrderNo = orderNo;
                    serviceProductDO.CreateBy = createBy;
                    serviceProductDO.CreateTime = createTime;
                    serviceProductDOs.Add(serviceProductDO);
                }
                if (serviceProductDOs.Any())
                {
                    await _orderProductRepository.InsertBatchAsync(serviceProductDOs);
                    allInsertedProducts.Union(serviceProductDOs);
                }

                #endregion

                //存地址
                if (orderAddressDO != null)
                {
                    orderAddressDO.OrderId = orderId;
                    await _orderAddressRepository.InsertAsync(orderAddressDO);
                }

                if (request.Data.ArrivalId > 0)
                {
                    await _receiveClient.ArrivalRelateOrder(new ArrivalRelateOrderRequest()
                    {
                        ArrivalId = request.Data.ArrivalId,
                        OrderNo = orderNo,
                        UserId = request.Data.UserId,
                        CreateBy = createBy
                    });
                }

                //存折扣订单
                if (orderDiscountDO != null)
                {
                    orderDiscountDO.OrderNo = orderNo;
                    await _orderDiscountRepository.InsertAsync(orderDiscountDO);
                }

                #region 使用并存优惠券
                if (userCouponId > 0 && userCoupon != null)
                {
                    //调用优惠券更新使用状态
                    var updateUserCouponStatusReqByIdDTO = new UpdateUserCouponStatusReqByIdRequest()
                    {
                        UserId = requestData.UserId,
                        UserCouponId = userCouponId
                    };
                    var updateUserCouponStatusUsedByIdResult = await _couponClient.UpdateUserCouponStatusUsedById(updateUserCouponStatusReqByIdDTO);
                    bool isUpdateUserCouponStatusUsedSuccess = updateUserCouponStatusUsedByIdResult.IsNotNullSuccess() && updateUserCouponStatusUsedByIdResult.GetSuccessData() == true;
                    if (!isUpdateUserCouponStatusUsedSuccess)
                    {
                        _logger.Error(CommonConstant.CouponUseFailure);
                    }
                    //组装存储订单优惠券
                    var orderCoupon = new OrderCouponDO()
                    {
                        OrderId = orderId,
                        UserCouponId = userCouponId,
                        CouponRuleId = userCoupon.CouponRuleId,
                        CouponName = userCoupon.DisplayName,
                        CouponAmount = userCoupon.Value,
                        CreateBy = createBy,
                        CreateTime = createTime
                    };
                    await _orderCouponRepository.InsertAsync(orderCoupon);
                }
                #endregion


                //代客下单
                if (request.Data.ProduceType == ProductTypeEnum.DelegateOrder.ToSbyte())
                {
                    await _delegateUserOrderRepository.InsertAsync(new DelegateUserOrderDO()
                    {
                        OrderNo = orderNo,
                        ShopId = request.Data.ShopId,
                        ProductType = BuyProductTypeEnum.DelegateOrder.ToSbyte(),
                        CreateBy = request.Data.DelegateUserId,
                        UserId = request.Data.DelegateUserId,
                        UserName = request.Data.DelegateUserName,
                        CreateTime = DateTime.Now
                    });

                    orderProductDiscountDOs?.ForEach(_ =>
                    {
                        _.OrderNo = orderNo;
                        _.Formula = " 价格 = 价格 - Math.Floor(价格 * 100 * 折扣率) / 100";
                        _.CreateBy = request.Data.DelegateUserName;
                        _.CreateTime = DateTime.Now;
                    });

                    if (orderProductDiscountDOs?.Count() > 0)
                        await _orderProductDiscountRepository.InsertBatchAsync(orderProductDiscountDOs);
                }

                //保险公司引流订单
                if (!string.IsNullOrEmpty(request.Data.InsuranceName))  // && !string.IsNullOrEmpty(request.Data.InsuranceRefNo)
                {
                    await _orderInsuranceCompanyRepository.InsertAsync(new OrderInsuranceCompanyDO()
                    {
                        OrderNo = orderNo,
                        Name = request.Data.InsuranceName,
                        RefNo = request.Data.InsuranceRefNo,
                        CreateBy = createBy,
                        CreateTime = createTime
                    });
                }

                ts.Complete();
            }

            #endregion

            //创建下单人记录,2023-3-28暂时不用
            //await _orderSaleRepository.InsertAsync(new OrderSaleDO()
            //{
            //    OrderNo = orderNo,
            //    ShopId = request.Data.ShopId,
            //    TechId = request.Data.DelegateUserId,
            //    TechName = request.Data.DelegateUserName,
            //    Percent = 1,
            //    CreateBy = createBy,
            //    CreateTime = createTime
            //});


            #region 扣减活动库存

            List<InsertPromotionActivityOrderRequest> insertPromotionActivityOrderRequest = new List<InsertPromotionActivityOrderRequest>();

            var promotionActivityProducts = new List<PromotionProductVo>();
            request?.Data?.ProductInfos?.ForEach(_ =>
            {
                if (!string.IsNullOrWhiteSpace(_.ActivityId))
                {
                    promotionActivityProducts.Add(new PromotionProductVo()
                    {
                        ActivityId = _.ActivityId,
                        Num = _.Number,
                        ProductCode = _.Pid
                    });
                }
            });

            if (promotionActivityProducts?.Count() > 0)
            {
                insertPromotionActivityOrderRequest.Add(new InsertPromotionActivityOrderRequest()
                {
                    OrderNo = orderNo,
                    UserId = request.Data.UserId,
                    SubmitBy = createBy,
                    Products = promotionActivityProducts
                });
            }

            ///更新促销扣减库存
            for (var i = 0; i < insertPromotionActivityOrderRequest.Count; i++)
            {
                _logger.Info($"insertPromotionActivityOrderRequest:{JsonConvert.SerializeObject(insertPromotionActivityOrderRequest[i])}");
                await _productClient.InsertPromotionActivityOrder(insertPromotionActivityOrderRequest[i]);
            }
            #endregion

            #region 通知订单中心进行信息同步

            //var syncOrderRequest = new SyncOrderRequest() { Order = _mapper.Map<MainOrderDTO>(orderDO) };
            //var orderProductDOs = await _orderProductRepository.GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            //syncOrderRequest.OrderProducts = _mapper.Map<List<MainOrderProductDTO>>(orderProductDOs?.ToList()); ;
            //var sysOrderResponse = await _orderClient.SyncOrder(syncOrderRequest);

            #endregion

            if (isCovertSuccess)
            {
                var addShopReserverV3 = await _receiveClient.AddShopReserveV3(new Client.Request.Receive.AddReserveV3Request()
                {
                    Year = reserverTime.Year,
                    Month = reserverTime.Month,
                    Day = reserverTime.Day,
                    ReserveTime = $"{reserverTime.Hour}:00",
                    OrderNo = orderNo,
                    UserId = request.Data.UserId
                });
            }

            #region 更新订单产品的成本价格
            await UpdateOrderProductCostPrice(new BatchUpdateCompleteStatusRequest() { 
                ShopId = request.Data.ShopId,
                UpdateBy = createBy,
                OrderNo = new List<string> { orderNo }
            } );
            #endregion


            #region 更新分摊明细优惠金额
            await UpdateOrderProductActualPayAmount(new BatchUpdateCompleteStatusRequest()
            {
                ShopId = request.Data.ShopId,
                UpdateBy = createBy,
                OrderNo = new List<string> { orderNo }
            });
            #endregion


            #region 更新签收
            //txw20221206 简化库存取消批次和占库
            await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);

            //var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;


            //products?.ForEach(item =>
            //{
            //    if (item.PackageProducts != null)
            //    {
            //        releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
            //    }
            //});

            //if (releaProductCount == 0 && shopPurchaseProducts?.Count == 0)
            //{
            //    await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);
            //}
            //else
            //{
            //    #region 通知占库

            //    _shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
            //    {
            //        QueueNo = orderNo,
            //        CreateBy = createBy,
            //        QueueStatus = "OrderService"
            //    });

            //    #endregion
            //}

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 押金单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitDelegateDepositOrder(ApiRequest<SubmitOrderRequest> request)
        {
            long shopId = request.Data.ShopId;

            // var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });

            var getUserInfoResult = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.UserId
            });

            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
            }

            #endregion

            #region 金额计算


            var franchisesConfig = await _franchisesConfigRepository.GetListAsync("Where is_deleted=0 and category=@Category", new { Category = request.Data.DepositOrderCategory }, true);

            decimal actualAmount = request.Data.ProductInfos?.FirstOrDefault()?.Price ?? 0;
            int num = request.Data.ProductInfos?.FirstOrDefault()?.Number ?? 0; ;
            if (franchisesConfig?.Count() > 0)
            {
                var franchisesConfigModel = franchisesConfig?.FirstOrDefault();
                if (franchisesConfigModel?.Category != FranchisesConfigCategoryEnum.Itself.ToString())
                {
                    actualAmount = franchisesConfigModel?.Amount ?? 0;
                    num = franchisesConfigModel?.Num ?? 0;
                }
                else
                {
                    actualAmount = request?.Data?.ProductInfos?.FirstOrDefault()?.Price ?? 0;
                    num = request?.Data?.ProductInfos?.FirstOrDefault()?.Number ?? 0;
                }
            }

            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.Name : request.Data.ReceiverName;
            telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.Mobile : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = getUserInfoResult?.Data?.Name ?? string.Empty,
                UserPhone = getUserInfoResult?.Data?.Mobile ?? string.Empty,
                ContactId = requestData.UserId,
                ContactName = createBy,
                ContactPhone = telephone,
                TotalProductNum = num,
                TotalProductAmount = actualAmount,
                ServiceNum = 0,
                ServiceFee = 0,
                DeliveryFee = 0,
                TotalCouponAmount = 0,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = 0,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = (sbyte)1,
                SignTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.UserId,
                NickName = user.Name,
                UserName = user.Name,
                UserTel = user.Mobile,
                UserTelDes = user.Mobile,
                CreateBy = createBy,
                CreateTime = createTime
            };

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    packageOrProductDO.Number = 1;
                    packageOrProductDO.TotalNumber = num;
                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);
                    allInsertedProducts.Add(packageOrProductDO);
                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }
                #endregion

                ts.Complete();
            }

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 待技师先生下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOfficeOrder(ApiRequest<SubmitOrderRequest> request)
        {
            long shopId = request.Data.ShopId;

            #region 基础验证

            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = shopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);
            if (request.Data.DelegateUserShopId <= 0)
                throw new CustomException(CommonConstant.DelegateShopIsNotNull);


            var getDelegateShop = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.DelegateUserShopId });

            var shopDepositAmount = getDelegateShop?.Data?.DepositAmount ?? 0;

            var getServiceTypes = await _baoYangClient.GetServiceTypeEnum();

            var getServiceType = getServiceTypes?.Data?.Where(_ => _.Id == request.Data.OrderType)?.FirstOrDefault();

            var productCategoryDepositAmount = getServiceType?.DepositAmount ?? 0;

            if (shopDepositAmount < productCategoryDepositAmount)
            {
                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.OPERATION_FAILED,
                    Data = null,
                    Message = $"下单需先交押金{productCategoryDepositAmount - shopDepositAmount} 元"
                };
            }

            var getDelegeateShopInfo = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.ShopId });

            var ownPhone = getDelegeateShopInfo?.Data.OwnerPhone;
            if (string.IsNullOrWhiteSpace(ownPhone))
                throw new CustomException(CommonConstant.OwnPhoneIsNull);

            var userId = string.Empty;

            var getOwnUserInfo = await _userClient.GetUserInfoByUserTel(new GetUserInfoByUserTelRequest()
            {
                UserTel = ownPhone
            });
            userId = getOwnUserInfo?.Data?.UserId;
            if (getOwnUserInfo?.Data == null)
            {
                var createUserResponse = await _userClient.CreateUser(new CreateUserRequest()
                {
                    UserTel = ownPhone,
                    UserName = getDelegeateShopInfo?.Data?.FullName,
                    NickName = getDelegeateShopInfo?.Data?.FullName,
                    Address = getDelegeateShopInfo?.Data?.Address,
                    SubmitBy = request.Data.DelegateUserName
                });
                userId = createUserResponse?.Data;
            }

            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = userId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);


            var promotionProducts = request.Data?.ProductInfos?.Where(_ => !string.IsNullOrEmpty(_.ActivityId))?.ToList();

            List<GetPromotionActivitByProductCodeListResponse> promotionActivityData = new List<GetPromotionActivitByProductCodeListResponse>();
            if (promotionProducts?.Count() > 0)
            {
                var promotionActivity = await _productClient.GetPromotionActivitByProductCodeList(new Core.Request.Product.GetPromotionActivitByProductCodeListRequest()
                {
                    ProductCodeList = promotionProducts?.Select(_ => _.Pid)?.ToList(),
                    PromotionChannel = request.Data?.PromotionChannel
                });
                promotionActivityData = promotionActivity?.Data;
                var chkPromotionActivity = (promotionActivity?.Data?.Count() ?? 0) == 0 ? true : false;
                promotionActivity?.Data?.ForEach(_ =>
                {
                    var pidSumNum = promotionProducts.Where(x => x.Pid == _.ProductCode)?.Sum(x => x.Number) ?? 0;
                    if (pidSumNum > _.AvailQuantity)
                        chkPromotionActivity = true;
                });
                if (chkPromotionActivity)
                    throw new CustomException(CommonConstant.ActivityIsFinished);
            }
            #endregion

            #region 添加项目

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            var shopProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();


            getPackageProductServicesRequest.ProductInfos = shopProducts;

            List<OrderProductDiscountDO> orderProductDiscountDOs = new List<OrderProductDiscountDO>(3);
            var products = new List<OrderConfirmPackageProductDTO>(3);

            if (shopProducts?.Count() > 0)
            {
                var shopItems = await GetShopProduct(getPackageProductServicesRequest, promotionActivityData, request.Data.ProduceType);
                if (shopItems?.Products?.Count() > 0)
                {
                    products.AddRange(shopItems?.Products);
                }

                if (shopProducts?.FirstOrDefault().Price < shopItems?.Products?.FirstOrDefault()?.PackageOrProduct?.Price)
                    throw new CustomException(CommonConstant.ApolloErpManOrderAmountIsMistake);

                products?.ForEach(item =>
                {
                    var shopItem = shopProducts?.Where(_ => _.Pid == item.PackageOrProduct.ProductId)?.FirstOrDefault();
                    var price = shopItem?.Price ?? 0;

                    item.PackageOrProduct.Price = price;
                    item.PackageOrProduct.TotalAmount = price * item.PackageOrProduct.TotalNumber;
                    item.PackageOrProduct.TotalCostPrice = price * item.PackageOrProduct.TotalNumber;
                });

            }

            #endregion

            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }

            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;


            var createTime = DateTime.Now;//创建时间

            var employee = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.DelegateUserId
            });

            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? request.Data.DelegateUserName : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? employee?.Data?.Mobile ?? string.Empty : request.Data.ReceiverPhone;

            createBy = request.Data.DelegateUserName;
            telephone = employee?.Data?.Mobile ?? string.Empty;

            //#region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Submitted.ToSbyte(),
                OrderTime = createTime,
                UserId = userId,
                UserName = request.Data.DelegateUserName,
                UserPhone = employee?.Data?.Mobile ?? string.Empty,
                ContactId = userId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = shop.CompanyId,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = 0,
                SignTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };


            #region 组装用户

            var orderUserDO = _mapper.Map<OrderUserDO>(user);
            orderUserDO.UserId = userId;

            #endregion

            #region 组装地址
            OrderAddressDO orderAddressDO = null;

            orderAddressDO = _mapper.Map<OrderAddressDO>(shop);
            orderAddressDO.AddressType = 1;
            orderAddressDO.ShopId = shopId;
            orderAddressDO.DetailAddress = shop.Address;

            if (orderAddressDO != null)
            {
                orderAddressDO.ReceiverName = contactName;
                orderAddressDO.ReceiverPhone = contactPhone;
                orderAddressDO.CreateBy = createBy;
                orderAddressDO.CreateTime = DateTime.Now;
            }

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {

                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);
                }

                #endregion

                //存地址
                if (orderAddressDO != null)
                {
                    orderAddressDO.OrderId = orderId;
                    await _orderAddressRepository.InsertAsync(orderAddressDO);
                }

                //技师先生工作室下单
                if (request.Data.ProduceType == ProductTypeEnum.OfficeOrder.ToSbyte())
                {
                    await _delegateUserOrderRepository.InsertAsync(new DelegateUserOrderDO()
                    {
                        OrderNo = orderNo,
                        ShopId = request.Data.DelegateUserShopId,
                        ProductType = BuyProductTypeEnum.OfficeOrder.ToSbyte(),
                        CreateBy = request.Data.DelegateUserId,
                        UserId = request.Data.DelegateUserId,
                        UserName = request.Data.DelegateUserName,
                        CreateTime = DateTime.Now
                    });
                }

                ts.Complete();
            }

            #endregion


            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);

        }

        /// <summary>
        /// 支付货款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitPayGoodsOrder(ApiRequest<SubmitOrderRequest> request)
        {
            long shopId = request.Data.ShopId;

            // var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });

            var getUserInfoResult = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.UserId
            });

            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
            }

            #endregion

            #region 金额计算

            decimal actualAmount = request.Data.ProductInfos?.FirstOrDefault()?.Price ?? 0;
            int num = request.Data.ProductInfos?.FirstOrDefault()?.Number ?? 0; ;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.Name : request.Data.ReceiverName;
            telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.Mobile : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = getUserInfoResult?.Data?.Name ?? string.Empty,
                UserPhone = getUserInfoResult?.Data?.Mobile ?? string.Empty,
                ContactId = requestData.UserId,
                ContactName = createBy,
                ContactPhone = telephone,
                TotalProductNum = num,
                TotalProductAmount = actualAmount,
                ServiceNum = 0,
                ServiceFee = 0,
                DeliveryFee = 0,
                TotalCouponAmount = 0,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = 0,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = (sbyte)1,
                SignTime = DateTime.Now,
                DispatchStatus = (sbyte)1,
                DispatchTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.UserId,
                NickName = user.Name,
                UserName = user.Name,
                UserTel = user.Mobile,
                UserTelDes = user.Mobile,
                CreateBy = createBy,
                CreateTime = createTime,
            };

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    packageOrProductDO.Number = 1;
                    packageOrProductDO.TotalNumber = num;
                    packageOrProductDO.Price = actualAmount;
                    packageOrProductDO.Amount = actualAmount;
                    packageOrProductDO.TotalAmount = actualAmount;
                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);
                    allInsertedProducts.Add(packageOrProductDO);
                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }
                #endregion

                ts.Complete();
            }

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 门店代收款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitDelegatePay(ApiRequest<SubmitOrderRequest> request)
        {
            long shopId = request.Data.ShopId;

            // var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });

            var getUserInfoResult = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.UserId
            });

            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
            }

            #endregion

            #region 金额计算

            decimal actualAmount = request.Data.ProductInfos?.FirstOrDefault()?.Price ?? 0;
            int num = request.Data.ProductInfos?.FirstOrDefault()?.Number ?? 0; ;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? string.Empty : request.Data.ReceiverName;
            telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? string.Empty : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = createBy,
                UserPhone = telephone,
                ContactId = requestData.UserId,
                ContactName = createBy,
                ContactPhone = telephone,
                TotalProductNum = num,
                TotalProductAmount = actualAmount,
                ServiceNum = 0,
                ServiceFee = 0,
                DeliveryFee = 0,
                TotalCouponAmount = 0,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = 0,
                ShopId = shopId,
                CreateBy = request.Data.CreatedBy,
                CreateTime = createTime,
                SignStatus = (sbyte)1,
                SignTime = DateTime.Now,
                DispatchStatus = (sbyte)1,
                DispatchTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.UserId,
                NickName = user.Name,
                UserName = user.Name,
                UserTel = user.Mobile,
                UserTelDes = user.Mobile,
                CreateBy = createBy,
                CreateTime = createTime
            };
            OrderCarDO orderCar = null;
            if (!string.IsNullOrEmpty(request.Data.CarNumber))
            {
                orderCar = new OrderCarDO()
                {
                    CarNumber = request.Data.CarNumber
                };

            }

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                if (orderCar != null)
                {
                    orderCar.OrderId = orderId;
                    await _orderCarRepository.InsertAsync(orderCar);
                }

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    packageOrProductDO.Number = 1;
                    packageOrProductDO.TotalNumber = num;
                    packageOrProductDO.Price = actualAmount;
                    packageOrProductDO.Amount = actualAmount;
                    packageOrProductDO.TotalAmount = actualAmount;
                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);
                    allInsertedProducts.Add(packageOrProductDO);
                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }
                #endregion

                ts.Complete();
            }

            #endregion


            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 获取订单确认页 自营产品适配出的数据，
        /// 2023-3目前只取套餐数据，暂时不做适配和补差价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetOrderConfirmPackageProductServicesResponse> GetOrderConfirmPackageProductServices(GetOrderConfirmPackageProductServicesRequest request, List<UpdateProductInfo> updateProductInfos = null)
        {
            var response = new GetOrderConfirmPackageProductServicesResponse();

            #region 尝试取套餐数据
            var tryPackageProductCodes = (from item in request.ProductInfos
                                          select item.Pid).ToList();
            var allProductCodes = tryPackageProductCodes;//所有产品编号（含套餐内产品）
            var parts = new List<BaoYangPartRequest>();//所有配件配件查询条件
            var packageProductSearchRequest = new PackageProductSearchRequest() { ProductCodes = tryPackageProductCodes };
            var getPackageProductsByCodesResult = await _productClient.GetPackageProductsByCodes(packageProductSearchRequest);//如果是套餐获取套餐产品集合
            _logger.Info($"GetOrderConfirmPackageProductServices packageProductSearchRequest={JsonConvert.SerializeObject(packageProductSearchRequest)} getPackageProductsByCodesResult={JsonConvert.SerializeObject(getPackageProductsByCodesResult)}");
            var packageProducts = getPackageProductsByCodesResult?.GetSuccessData();//套餐数据

            #region 更新产品信息

            var shopProductCodes = new List<string>();
            if (updateProductInfos?.Count() > 0)
            {
                _logger.Info($"GetOrderConfirmPackageProductServices updateProductInfos={JsonConvert.SerializeObject(updateProductInfos)} ");

                updateProductInfos?.ForEach(_ =>
                {
                    _.Child?.ForEach(single =>
                    {
                        var packageProduct = packageProducts.Where(item => item.Details.Any(t => t.ProjectId == single.OldPid)).FirstOrDefault();
                        if (packageProduct != null)
                        {
                            if (single.ProductOwnAttri == 1)
                            {
                                shopProductCodes.Add(single.Pid);
                            }
                            var detailProduct = packageProduct.Details.Where(item => item.ProjectId == single.OldPid).FirstOrDefault();
                            packageProduct.Details.Add(new Core.Model.Product.ProductPackageDetailDTO()
                            {
                                ProjectId = single.Pid,
                                ProjectType = detailProduct.ProjectType,
                                Quantity = detailProduct.Quantity
                            });
                            packageProduct.Details.Remove(detailProduct);
                        }
                    });
                });
            }

            #endregion

            #region 根据四滤配件号适配具体商品

            if (packageProducts != null && packageProducts.Any())
            {
                foreach (var item in packageProducts)
                {
                    foreach (var detail in item.Details)
                    {
                        switch (detail.ProjectType)
                        {
                            case 1:
                                if (!allProductCodes.Contains(detail.ProjectId))
                                {
                                    allProductCodes.Add(detail.ProjectId);
                                }
                                break;
                            case 2:
                                parts.Add(new BaoYangPartRequest()
                                {
                                    PartType = detail.ProjectId,
                                    BrandList = detail.ProjectBrands
                                });
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            //根据四滤配件号适配具体商品
            List<PartProductRefDTO> adaptiveProducts = null;
            //var saleProductId = _configuration["OrderConfig:ApolloErpJiYouPackage"];
            var isContainsSaleProduct = false;
            //isContainsSaleProduct = request.ProductInfos?.Select(_ => _.Pid == saleProductId)?.FirstOrDefault() ?? false;
            if (string.IsNullOrWhiteSpace(request.CarId) && isContainsSaleProduct)
            {
                //allProductCodes.Add(_configuration["OrderConfig:CommonJvPid"]);
            }
            else
            {
                if (parts.Any())
                {
                    var adaptiveProductByPartTypeAndCarIdRequest = new AdaptiveProductByPartTypeAndCarIdRequest()
                    {
                        CarId = request.CarId,
                        UserId = request.UserId,
                        ProvinceId = request.ProvinceId,
                        CityId = request.CityId,
                        BaoYangPart = parts
                    };
                    var partNotAdapterData = new List<string>(5);

                    var getAdaptiveProductByPartTypeAndCarIdResult = await _baoYangClient.GetAdaptiveProductByPartTypeAndCarId(adaptiveProductByPartTypeAndCarIdRequest);
                    _logger.Info($"GetOrderConfirmPackageProductServices adaptiveProductByPartTypeAndCarIdRequest={JsonConvert.SerializeObject(adaptiveProductByPartTypeAndCarIdRequest)} getAdaptiveProductByPartTypeAndCarIdResult={JsonConvert.SerializeObject(getAdaptiveProductByPartTypeAndCarIdResult)}");
                    adaptiveProducts = getAdaptiveProductByPartTypeAndCarIdResult.GetSuccessData();
                    if (adaptiveProducts != null && adaptiveProducts.Any())
                    {
                        foreach (var item in adaptiveProducts)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Pid))
                            {
                                allProductCodes.Add(item.Pid);
                            }
                            else
                            {
                                partNotAdapterData.Add(item.PartType);

                            }
                        }
                    }



                    if (partNotAdapterData?.Count() > 0)
                        throw new CustomException(CommonConstant.OilNotAdapterData);
                }
            }

            #region 更新产品信息
            if (updateProductInfos?.Count() > 0)
            {
                updateProductInfos?.ForEach(_ =>
                {
                    _.Child?.ForEach(single =>
                    {
                        if (allProductCodes.Exists(x => x == single.OldPid))
                        {
                            allProductCodes.Remove(single.OldPid);
                            allProductCodes.Add(single.Pid);
                            if (single.ProductOwnAttri == 1)
                            {
                                shopProductCodes.Add(single.Pid);
                            }
                        }
                        var patsType = parts.Select(x => x.PartType).ToList();
                        if (patsType?.Count() > 0)
                        {
                            var packageProduct = packageProducts.Where(item => item.Details.Any(t => patsType.Contains(t.ProjectId))).FirstOrDefault();
                            var detailProduct = packageProduct.Details.Where(item => patsType.Contains(item.ProjectId)).FirstOrDefault();
                            if (detailProduct != null)
                            {
                                detailProduct.ProjectId = single.Pid;
                            }
                        }

                    });
                });
            }

            #endregion

            #endregion

            #endregion

            #region 取所有商品详情数据
            var productDetailSearchRequest = new ProductDetailSearchRequest() { ProductCodes = allProductCodes };
            var getProductsByProductCodesResult = await _productClient.GetProductsByProductCodes(productDetailSearchRequest);
            _logger.Info($"GetOrderConfirmPackageProductServices productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} getProductsByProductCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
            var productDetails = getProductsByProductCodesResult?.GetSuccessData();//商品详情数据
            if (productDetails == null || !productDetails.Any())
            {
                _logger.Warn($"选购的商品：{string.Join(";", allProductCodes)}未找到");
                throw new CustomException(CommonConstant.ProductInfomationException);
            }

            #region 更新产品信息
            //说明里面放规格型号和原厂编码
            productDetails.ForEach(_ => _.Product.Description = $"{_.Product.PartNo} {_.Product.PartCode}");

            if (shopProductCodes?.Count() > 0)
            {
                var shopProducts = await _productClient.GetShopProductByCodes(new Client.Request.Product.GetShopProductByCodeRequest()
                {
                    ShopProductCodes = shopProductCodes
                });

                shopProducts?.Data?.ForEach(_ =>
                {
                    productDetails.Add(new ProductDetailDTO()
                    {
                        Product = new Core.Model.Product.ProductAllInfoDTO()
                        {
                            ProductCode = _.ProductCode,
                            Name = _.ProductName,
                            DisplayName = _.DisplayName,
                            Brand = string.Empty,
                            Description = $"{_.OeNumber} {_.Specification}",
                            ProductAttribute = 5, 
                            ChildCategoryCode = _.CategoryId.ToString(),
                            ChildCategoryId = (int)_.CategoryId,
                            Image1 = _.Icon.Replace("http://m.aerp.com.cn/", ""),
                            StandardPrice = _.StandardPrice,
                            SalesPrice = _.SalesPrice,
                            SettlementPrice = _.PurchaseCost,
                            Unit = _.Unit,
                            TaxRate = 0,
                            OnSale = 1
                        }
                    });
                });
            }

            #endregion

            #endregion

            #region 更新产品信息
            //把替换产品的价格信息更新到套餐产品中
            if (updateProductInfos?.Count() > 0)
            {
                updateProductInfos?.ForEach(_ =>
                {
                    _.Child?.ForEach(single =>
                    {
                        var packageProduct = packageProducts.Where(item => item.Details.Any(t => t.ProjectId == single.Pid)).FirstOrDefault();
                        if (packageProduct != null)
                        {
                            var detailProduct = packageProduct.Details.Where(item => item.ProjectId == single.Pid).FirstOrDefault();

                            var productDetail = productDetails.Where(item => item.Product.ProductCode == single.Pid).FirstOrDefault();
                            if (detailProduct != null && productDetail != null)
                            {
                                detailProduct.StandardPrice = productDetail.Product.StandardPrice;
                                detailProduct.SalesPrice = productDetail.Product.SalesPrice;
                            }
                        }
                    });
                });
            }

            #endregion

            #region 获取所有带出的安装服务产品详情数据
            var allRelatedServiceCodes = new List<string>();
            foreach (var item in productDetails)
            {
                if (item.Product.OnSale == 0)
                {
                    throw new CustomException(CommonConstant.ProductIsUnSale);
                }
                //由于产品配置问题，屏蔽掉带出服务
                //if (item.InstallationServices != null && item.InstallationServices.Any())
                //{
                //    foreach (var service in item.InstallationServices)
                //    {
                //        allRelatedServiceCodes.Add(service.ServiceId);
                //    }
                //}
            }


            #region  补录服务产品服务Pid
            //暂时不要
            //var installProducts = new List<InstallProductRequest>();
            //request?.ProductInfos?.ForEach(_ =>
            //{
            //    installProducts.Add(new InstallProductRequest()
            //    {
            //        InstallType = string.Empty,
            //        Num = _.Number,
            //        Pid = _.Pid
            //    });
            //});
            //VehicleRequest vehicleRequest = null;
            //if (!string.IsNullOrEmpty(request.CarId) && !string.IsNullOrEmpty(request.UserId))
            //{
            //    var vehicle = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest()
            //    {
            //        CarId = request.CarId,
            //        UserId = request.UserId
            //    });
            //    vehicleRequest = _mapper.Map<VehicleRequest>(vehicle.Data);
            //}

            //var getInstallServices = await _baoYangClient.GetInstallServiceByProduct(new InstallServiceByProductRequest()
            //{
            //    Products = installProducts,
            //    Vehicle = vehicleRequest
            //});

            //getInstallServices?.Data?.InstallService?.ForEach(_ =>
            //{
            //    allRelatedServiceCodes.Add(_.ServiceId);
            //});

            #endregion

            #region 服务加价  单独下服务 根据车型补差价

            var getAdditionalPriceData = new ApiResult<List<InstallServiceDetailVo>>();
            //暂时不要
            //var serviceProduct = request?.ProductInfos?.Where(_ => _.Pid.EndsWith("FU"))?.Select(_ => _.Pid)?.ToList();

            //if (serviceProduct?.Count > 0)
            //{
            //    getAdditionalPriceData = await _baoYangClient.GetAdditionalPriceByServiceId(new AdditionalPriceByServiceIdRequest()
            //    {
            //        ServiceId = serviceProduct,
            //        Vehicle = vehicleRequest
            //    });
            //}

            #endregion

            var serviceDetails = new List<ProductDetailDTO>();//带出的服务商品详情数据
            if (allRelatedServiceCodes.Any())
            {
                var serviceDetailSearchRequest = new ProductDetailSearchRequest() { ProductCodes = allRelatedServiceCodes };
                var getServicesByProductCodesResult = await _productClient.GetProductsByProductCodes(serviceDetailSearchRequest);
                _logger.Info($"GetOrderConfirmPackageProductServices serviceDetailSearchRequest={JsonConvert.SerializeObject(serviceDetailSearchRequest)} getServicesByProductCodesResult={JsonConvert.SerializeObject(getServicesByProductCodesResult)}");
                serviceDetails = getServicesByProductCodesResult?.GetSuccessData();
                if (serviceDetails == null || !serviceDetails.Any())
                {
                    _logger.Warn($"带出的服务：{string.Join(";", allRelatedServiceCodes)}未找到");
                    throw new CustomException(CommonConstant.ServiceInformationException);
                }
            }
            #endregion

            var products = new List<OrderConfirmPackageProductDTO>();//返回的选购套餐或单品集合
            var services = new List<OrderConfirmProductDTO>();//返回的选购服务集合

            #region 遍历选购的商品信息集合组装商品集合
            foreach (var productInfo in request.ProductInfos)
            {
                var productCode = productInfo.Pid;
                var findProduct = productDetails.Find(_ => _.Product.ProductCode == productCode);

                //移除198 首单价格
                //if (productCode == saleProductId)
                //{
                //    var checkFistSpecialProduct = await _orderRepository.CheckUserFirstOrderForSpecialProduct(request.UserId, saleProductId);
                //    if (checkFistSpecialProduct <= 0)
                //    {
                //        findProduct.Product.SalesPrice = Convert.ToDecimal(_configuration["OrderConfig:ApolloErpJiYouSalePrice"]);
                //    }
                //}

                if (findProduct == null)
                {
                    _logger.Warn($"选购的商品：{productCode}未找到");
                    throw new CustomException(CommonConstant.ProductInfomationException);
                }

                var getAddtionalProduct = getAdditionalPriceData?.Data?.Where(_ => _.ServiceId == findProduct.Product.ProductCode);

                var getAddtionalProductCount = getAddtionalProduct?.Count();

                var getAddionalPrice = getAddtionalProduct?.FirstOrDefault()?.Price ?? 0;

                //价格取前端传来的价格，txw,2023-4
                //var price = getAddtionalProductCount > 0 ? getAddionalPrice : findProduct.Product.SalesPrice;
                var price = productInfo.Price;

                //套餐或单品
                var product = new OrderConfirmPackageProductDTO()
                {

                    PackageOrProduct = new OrderConfirmProductDTO()
                    {
                        ProductId = findProduct.Product.ProductCode,
                        ProductName = findProduct.Product.Name,
                        DisplayName = findProduct.Product.DisplayName,
                        Brand = findProduct.Product.Brand,
                        Description = findProduct.Product.Description,
                        ProductAttribute = findProduct.Product.ProductAttribute,
                        MainCategoryCode = findProduct.Product.MainCategoryId.ToString(),
                        SecondCategoryCode = findProduct.Product.SecondCategoryId.ToString(),
                        CategoryCode = findProduct.Product.ChildCategoryId.ToString(),
                        Label = GetLabelByProductDetail(findProduct),
                        ImageUrl = findProduct.Product.Image1,
                        MarketingPrice = getAddtionalProductCount > 0 ? (getAddtionalProduct?.FirstOrDefault()?.MarketPrice ?? 0) : findProduct.Product.StandardPrice,
                        Price = price,
                        Unit = findProduct.Product.Unit,
                        Number = productInfo.Number,
                        TotalNumber = productInfo.Number,
                        Amount = (price) * productInfo.Number,
                        TotalAmount = (price) * productInfo.Number,
                        ActualPayAmount = (price) * productInfo.Number,
                        TaxRate = findProduct.Product.TaxRate,
                        TotalCostPrice = findProduct.Product.ProductAttribute == 2 ?
                            (getAddtionalProductCount > 0 ? getAddionalPrice : findProduct.Product.SettlementPrice) * productInfo.Number 
                            : findProduct.Product.SettlementPrice *productInfo.Number,
                        SettlementPrice = findProduct.Product.SettlementPrice,

                    },
                    PackageProducts = new List<OrderConfirmProductDTO>()
                };

                if (findProduct.Product.ProductAttribute == CommonConstant.PackageProduct)
                {
                    if (packageProducts != null && packageProducts.Any())
                    {
                        var findPackageProduct = packageProducts.Find(_ => _.PackageInfo.ProductCode == productCode);
                        if (findPackageProduct != null)
                        {
                            foreach (var detail in findPackageProduct.Details)
                            {
                                var detailPid = string.Empty;//明细商品ID
                                switch (detail.ProjectType)
                                {
                                    case 1:
                                        detailPid = detail.ProjectId;
                                        break;
                                    case 2:
                                        if (adaptiveProducts != null)
                                        {
                                            var findAdaptiveProduct = adaptiveProducts.Find(_ => _.PartType == detail.ProjectId);
                                            if (findAdaptiveProduct != null)
                                            {
                                                detailPid = findAdaptiveProduct.Pid;
                                            }
                                            else
                                            {
                                                detailPid = detail.ProjectId;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                var findDetail = productDetails.Find(_ => _.Product.ProductCode == detailPid);
                                if (findDetail == null)
                                {
                                    continue;
                                }
                                var salePrice = findDetail.Product.SalesPrice; //取实际产品的价格，不取套餐明细设置的价格 detail.SalesPrice 
                                //套餐明细
                                var packageProduct = new OrderConfirmProductDTO()
                                {
                                    ProductId = findDetail.Product.ProductCode,
                                    ProductName = findDetail.Product.Name,
                                    DisplayName = findDetail.Product.DisplayName,
                                    Brand = findDetail.Product.Brand,
                                    Description = findDetail.Product.Description,
                                    ProductAttribute = findDetail.Product.ProductAttribute,
                                    MainCategoryCode = findDetail.Product.MainCategoryId.ToString(),
                                    SecondCategoryCode = findDetail.Product.SecondCategoryId.ToString(),
                                    CategoryCode = findDetail.Product.ChildCategoryId.ToString(),
                                    Label = GetLabelByProductDetail(findDetail),
                                    ImageUrl = findDetail.Product.Image1,
                                    MarketingPrice = findDetail.Product.StandardPrice,// detail.StandardPrice,
                                    Price = salePrice, // detail.SalesPrice,
                                    Unit = findDetail.Product.Unit,
                                    Number = detail.Quantity,
                                    TotalNumber = detail.Quantity * productInfo.Number,
                                    Amount = salePrice * detail.Quantity,
                                    TotalAmount = salePrice * detail.Quantity * productInfo.Number,
                                    ActualPayAmount = salePrice * detail.Quantity * productInfo.Number,
                                    TaxRate = findDetail.Product.TaxRate,
                                    ParentPackageProductCode = findProduct.Product.ProductCode,
                                    TotalCostPrice = findDetail.Product.SettlementPrice * detail.Quantity * productInfo.Number,
                                    //(findDetail.Product.ProductAttribute == 2 || findDetail.Product.ProductAttribute == 4) ?
                                    //salePrice * detail.Quantity * productInfo.Number : 0
                                    SettlementPrice = findProduct.Product.SettlementPrice,
                                };
                                product.PackageProducts.Add(packageProduct);
                            }
                            //更新套餐的成本
                            product.PackageOrProduct.TotalCostPrice = product.PackageProducts.Sum(x => x.TotalCostPrice);
                        }
                    }
                }

                products.Add(product);
            }
            #endregion


            #region 找出并组装所有的单品带出的安装服务
            //由于产品配置问题，屏蔽掉带出服务
            //foreach (var item in productDetails)
            //{
            //    if (item.InstallationServices != null && item.InstallationServices.Any())
            //    {
            //        foreach (var installationService in item.InstallationServices)
            //        {
            //            if (serviceDetails == null)
            //            {
            //                continue;
            //            }
            //            var findInstallationServiceProduct = serviceDetails.Find(_ => _.Product.ProductCode == installationService.ServiceId);
            //            if (findInstallationServiceProduct == null)
            //            {
            //                continue;
            //            }
            //            var findSelectProductedInfo = request.ProductInfos.Find(_ => _.Pid == item.Product.ProductCode);
            //            if (findSelectProductedInfo == null)
            //            {
            //                continue;
            //            }
            //            //带出的套餐外安装服务
            //            var service = new OrderConfirmProductDTO()
            //            {
            //                ProductId = findInstallationServiceProduct.Product.ProductCode,
            //                ProductName = findInstallationServiceProduct.Product.Name,
            //                DisplayName = installationService.ServiceName,
            //                Brand = string.Empty,
            //                Description = findInstallationServiceProduct.Product.Description,
            //                ProductAttribute = findInstallationServiceProduct.Product.ProductAttribute,
            //                MainCategoryCode = findInstallationServiceProduct.Product.MainCategoryId.ToString(),
            //                SecondCategoryCode = findInstallationServiceProduct.Product.SecondCategoryId.ToString(),
            //                CategoryCode = findInstallationServiceProduct.Product.ChildCategoryId.ToString(),
            //                Label = GetLabelByProductDetail(findInstallationServiceProduct),
            //                ImageUrl = findInstallationServiceProduct.Product.Image1,
            //                MarketingPrice = installationService.ServicePrice,
            //                Price = installationService.ServicePrice,
            //                Unit = findInstallationServiceProduct.Product.Unit,
            //                Number = findSelectProductedInfo.Number,
            //                TotalNumber = findSelectProductedInfo.Number,
            //                Amount = installationService.ServicePrice * findSelectProductedInfo.Number,
            //                TotalAmount = installationService.ServicePrice * findSelectProductedInfo.Number,
            //                ActualPayAmount = installationService.ServicePrice * findSelectProductedInfo.Number,
            //                TaxRate = findInstallationServiceProduct.Product.TaxRate,
            //                TotalCostPrice = findInstallationServiceProduct.Product.ProductAttribute == 2 ?
            //                        installationService.ServicePrice * findSelectProductedInfo.Number : 0
            //            };

            //            var findIfExistService = services.Find(_ => _.ProductId == service.ProductId);
            //            if (findIfExistService == null)
            //            {
            //                service.ServeForProductCodes = $"{item.Product.ProductCode};";
            //                services.Add(service);
            //            }
            //            else
            //            {
            //                //同一个安装服务用于多个商品累计
            //                findIfExistService.Number += findSelectProductedInfo.Number;
            //                findIfExistService.TotalNumber += findSelectProductedInfo.Number;
            //                findIfExistService.Amount += installationService.ServicePrice * findSelectProductedInfo.Number;
            //                findIfExistService.TotalAmount += installationService.ServicePrice * findSelectProductedInfo.Number;
            //                if (findIfExistService.ServeForProductCodes != null)
            //                {
            //                    findIfExistService.ServeForProductCodes += $"{item.Product.ProductCode};";
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 补录服务 里面补录出的服务根据车型补录差价

            //serviceDetails?.ForEach(_ =>
            //{
            //    var getInstallService = getInstallServices?.Data?.ProductInstallService?.Where(item => item.ServiceId.Any(t => t == _.Product.ProductCode))?.FirstOrDefault();
            //    var installService = getInstallServices?.Data?.InstallService?.Where(item => item.ServiceId == _.Product.ProductCode)?.FirstOrDefault();
            //    if (getInstallService != null)
            //    {
            //        var product = products.Find(productItem => productItem.PackageOrProduct.ProductId == installService.ServiceId);
            //        if (product != null)
            //        {
            //            products.Remove(product);
            //        }
            //        //带出的套餐外安装服务
            //        var service = new OrderConfirmProductDTO()
            //        {
            //            ProductId = _.Product.ProductCode,
            //            ProductName = _.Product.Name,
            //            DisplayName = _.Product.Name,
            //            Brand = string.Empty,
            //            Description = _.Product.Description,
            //            ProductAttribute = _.Product.ProductAttribute,
            //            MainCategoryCode = _.Product.MainCategoryId.ToString(),
            //            SecondCategoryCode = _.Product.SecondCategoryId.ToString(),
            //            CategoryCode = _.Product.ChildCategoryId.ToString(),
            //            Label = GetLabelByProductDetail(_),
            //            ImageUrl = _.Product.Image1,
            //            MarketingPrice = installService?.MarketPrice ?? 0,
            //            Price = installService?.Price ?? 0,
            //            Unit = _.Product.Unit,
            //            Number = installService?.Count ?? 0,
            //            TotalNumber = installService?.Count ?? 0,
            //            Amount = installService.Count * installService.Price,
            //            TotalAmount = installService.Count * installService.Price,
            //            TaxRate = _.Product.TaxRate
            //        };

            //        var findIfExistService = services.Find(item => item.ProductId == service.ProductId);
            //        if (findIfExistService == null)
            //        {
            //            service.ServeForProductCodes = $"{getInstallService.ProductId};";
            //            services.Add(service);
            //        }
            //        else
            //        {
            //            //同一个安装服务用于多个商品累计
            //            findIfExistService.Number += installService?.Count ?? 0;
            //            findIfExistService.TotalNumber += installService?.Count ?? 0;
            //            findIfExistService.Amount += installService.Count * installService.Price;
            //            findIfExistService.TotalAmount += installService.Count * installService.Price;
            //            if (findIfExistService.ServeForProductCodes != null)
            //            {
            //                findIfExistService.ServeForProductCodes += $"{getInstallService.ProductId};";
            //            }
            //        }
            //    }

            //});

            #endregion

            response.Products = products;

            response.Services = services;

            return response;
        }

        /// <summary>
        /// 获取订单确认页 门店项目产品
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        private async Task<GetOrderConfirmPackageProductServicesResponse> GetShopProduct(GetOrderConfirmPackageProductServicesRequest request, List<GetPromotionActivitByProductCodeListResponse> activityProduct, sbyte productType = 0)
        {
            var response = new GetOrderConfirmPackageProductServicesResponse();
            var productCodes = request?.ProductInfos?.Select(_ => _.Pid)?.ToList();

            var shopProducts = await _productClient.GetShopProductByCodes(new Client.Request.Product.GetShopProductByCodeRequest()
            {
                ShopProductCodes = productCodes
            });

            if (shopProducts?.Data?.Count == 0)
            {
                throw new CustomException("项目产品不存在");
            }

            var products = new List<OrderConfirmPackageProductDTO>();//返回的选购套餐或单品集合
            shopProducts?.Data?.ForEach(_ =>
            {
                var product = productCodes?.Where(item => item == _.ProductCode);
                if (product != null && product.Any())
                {
                    var num = request?.ProductInfos?.Where(item => item.Pid == _.ProductCode)?.FirstOrDefault()?.Number ?? 0;
                    var activityItem = activityProduct?.Where(item => item.ProductCode == _.ProductCode)?.ToList();
                    var price = _.SalesPrice;
                    if (activityItem != null && activityItem.Any())
                    {
                        price = activityItem?.FirstOrDefault()?.PromotionPrice ?? 0;
                    }
                    decimal discountRate = _.DiscountRate;
                    decimal discountPrice = 0;

                    if (productType == ProductTypeEnum.DelegateOrder.ToSbyte())
                    {
                        discountPrice = Math.Floor(price * 100 * discountRate) / 100;
                        price = price - discountPrice;//超出分的金额直接进位
                    }

                    products.Add(new OrderConfirmPackageProductDTO()
                    {
                        PackageOrProduct = new OrderConfirmProductDTO()
                        {
                            ProductId = _.ProductCode,
                            ProductName = _?.ProductName,
                            DisplayName = _?.DisplayName,
                            Description = $"{_?.OeNumber} {_?.Specification}",
                            Brand = string.Empty,
                            ProductAttribute = 4,
                            ParentOrderPackagePid = 0,
                            ServeForOrderPids = string.Empty,
                            CategoryCode = _.CategoryId.ToString(),
                            ItemCode = string.Empty,
                            Label = string.Empty,
                            ImageUrl = _.Icon.Replace("http://m.aerp.com.cn/", ""),
                            PriceId = 0,
                            MarketingPrice = _.StandardPrice,
                            Price = price,
                            TotalCostPrice = _.PurchaseCost * num,
                            Number = 1,
                            Unit = _.Unit,
                            TotalNumber = num,
                            StockStatus = 0,
                            Amount = price,
                            TotalAmount = price * num,
                            TaxRate = 0,
                            ShareDiscountAmount = 0,
                            ActualPayAmount = price * num,
                            DiscountRate = discountRate,
                            OriginPrice = _.SalesPrice,
                            DiscountPrice = discountPrice
                        }
                    });
                }


            });
            response.Products = products;

            return response;
        }


        private async Task<GetOrderConfirmPackageProductServicesResponse> GetShopPurchaseProduct(GetOrderConfirmPackageProductServicesRequest request)
        {
            var response = new GetOrderConfirmPackageProductServicesResponse();
            var productCodes = request?.ProductInfos?.Select(_ => _.Pid)?.ToList();

            var shopProducts = await _productClient.GetShopProductByCodes(new Client.Request.Product.GetShopProductByCodeRequest()
            {
                ShopProductCodes = productCodes
            });
            if (shopProducts?.Data?.Count == 0)
            {
                throw new CustomException("外采产品商品不存在");
            }

            var products = new List<OrderConfirmPackageProductDTO>();//返回的选购套餐或单品集合
            shopProducts?.Data?.ForEach(_ =>
            {
                var product = request?.ProductInfos?.Where(item => item.Pid == _.ProductCode)?.FirstOrDefault();
                if (product != null )
                {
                    var num = product.Number;
                    //价格取前端传入的价格
                    //var price = _.SalesPrice;
                    var price = product.Price;

                    products.Add(new OrderConfirmPackageProductDTO()
                    {
                        PackageOrProduct = new OrderConfirmProductDTO()
                        {
                            ProductId = _.ProductCode,
                            ProductName = _?.ProductName,
                            DisplayName = _?.DisplayName,
                            Description = $"{_?.OeNumber} {_?.Specification}",
                            Brand = string.Empty,
                            ProductAttribute = 5,
                            ParentOrderPackagePid = 0,
                            ServeForOrderPids = string.Empty,
                            CategoryCode = _.CategoryId.ToString(),
                            ItemCode = string.Empty,
                            Label = string.Empty,
                            ImageUrl = _.Icon.Replace("http://m.aerp.com.cn/", ""),
                            PriceId = 0,
                            MarketingPrice = _.StandardPrice,
                            Price = price,
                            TotalCostPrice = _.PurchaseCost * num,
                            Number = 1,
                            Unit = _.Unit,
                            TotalNumber = num,
                            StockStatus = 0,
                            Amount = price,
                            TotalAmount = price * num,
                            TaxRate = 0,
                            ShareDiscountAmount = 0,
                            ActualPayAmount = price * num,
                        }
                    });
                }


            });
            response.Products = products;

            return response;
        }
        /// <summary>
        /// 根据商品明细获取标签
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        private string GetLabelByProductDetail(ProductDetailDTO productDetail)
        {
            var label = string.Empty;
            if (productDetail != null && productDetail.Attributevalues != null && productDetail.Attributevalues.Any())
            {
                var findAttributevalue = productDetail.Attributevalues.Find(_ => _.AttributeName == "BaseOilGrade");
                if (findAttributevalue != null)
                {
                    label = findAttributevalue.AttributeValue;
                }
            }
            return label;
        }

        /// <summary>
        /// 核销码订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitVerificationCodeOrder(ApiRequest<SubmitOrderRequest> request)
        {
            _logger.Info($"SubmitVerificationCodeOrder:{JsonConvert.SerializeObject(request)}");

            #region 基础验证

            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = request.Data.UserId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            var getUserVehicleByCarIdResult = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest()
            { UserId = request.Data.UserId, CarId = request.Data.CarId });
            var car = getUserVehicleByCarIdResult?.GetSuccessData();
            if (car == null)
                throw new CustomException(CommonConstant.CarInformationIsNull);

            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.ShopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);

            #endregion

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
            getPackageProductServicesRequest.CityId = shop.CityId;

            var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
            var products = getPackageProductServicesResponse.Products;
            var services = getPackageProductServicesResponse.Services;
            #endregion

            #region 验证服务是否在门店上架
            var allServiceProductIds = new List<string>();
            foreach (var item in products)
            {
                if (item.PackageOrProduct.ProductAttribute == 2)
                {
                    allServiceProductIds.Add(item.PackageOrProduct.ProductId);
                }
                if (item.PackageProducts != null && item.PackageProducts.Any())
                {
                    item.PackageProducts.ForEach(_ =>
                    {
                        if (_.ProductAttribute == 2)
                        {
                            allServiceProductIds.Add(_.ProductId);
                        }
                    });
                }
            }
            foreach (var item in services)
            {
                allServiceProductIds.Add(item.ProductId);
            }
            var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
            {
                ShopId = request.Data.ShopId,
                ProductIds = allServiceProductIds.ToList()
            });
            var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
            if (serviceCosts != null)
            {
                var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false);//服务上下架校验
                if (findOfflineServices != null && findOfflineServices.Any())
                {
                    throw new CustomException(CommonConstant.ServiceShopNotSupport);
                }
            }
            #endregion。

            #region 计算成本

            serviceCosts?.ForEach(_ =>
            {
                if (_.CostPrice > 0)
                {
                    var product = products?.SelectMany(item => item.PackageProducts)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (product != null)
                    {
                        product.TotalCostPrice = 0;
                    }
                    var service = services?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (service != null)
                    {
                        service.TotalCostPrice = 0;
                    }
                }
            });
            #endregion

            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            #endregion

            #region 核销验证

            bool isBuyVerificationOrder = false;//是否购买核销码产生的订单
            int orderVerificationCodeNum = 0;//核销码数量
            OrderVerificationCodeDO orderVerificationCodeDO = null;//单个核销码对象

            var verificationOrderProduct = products?.FirstOrDefault();
            var rules = await _verificationRuleRepository.GetRule(shop.Type, requestData.ShopId, verificationOrderProduct.PackageOrProduct.ProductId);
            if (rules != null && rules.Any())
            {
                isBuyVerificationOrder = true;
                requestData.ProduceType = 1;
            }
            else
            {
                throw new CustomException(CommonConstant.BeautyGuleException);
            }
            if (isBuyVerificationOrder)
            {
                if (products.Count != 1)
                {
                    throw new CustomException(CommonConstant.PackageCardOnlyCanBuyOne);
                }

                var rule = rules.FirstOrDefault();
                //有效期计算
                var startValidTime = new DateTime(1900, 1, 1);
                var endValidTime = new DateTime(1900, 1, 1);
                switch (rule.ValidStartType)
                {
                    case 1:
                        startValidTime = DateTime.Today;
                        switch (rule.ValidEndType)
                        {
                            case 1:
                                endValidTime = startValidTime.AddDays(rule.ValidDays);
                                break;
                            case 2:
                                endValidTime = rule.LatestUseDate;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        startValidTime = rule.EarliestUseDate;
                        switch (rule.ValidEndType)
                        {
                            case 1:
                                endValidTime = startValidTime.AddDays(rule.ValidDays);
                                break;
                            case 2:
                                endValidTime = rule.LatestUseDate;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                orderVerificationCodeDO = new OrderVerificationCodeDO()
                {
                    RuleId = rule.Id,
                    UserId = requestData.UserId,
                    UserPhone = user.UserTel,
                    Channel = VerticationChannel.RG.ToString(),
                    Status = 0,
                    StartValidTime = startValidTime,
                    EndValidTime = endValidTime
                };
                if (orderVerificationCodeDO.EndValidTime < DateTime.Today)
                {
                    throw new CustomException(CommonConstant.PackageExpireDate);
                }

                if (verificationOrderProduct != null && verificationOrderProduct.PackageOrProduct != null)
                {
                    if (verificationOrderProduct.PackageOrProduct.ProductAttribute == 1)
                    {
                        if (verificationOrderProduct.PackageOrProduct.Number != 1 || verificationOrderProduct.PackageProducts == null
                            || verificationOrderProduct.PackageProducts.Count != 1)
                        {
                            throw new CustomException(CommonConstant.PackageCardOnlyCanBuyOne);
                        }
                        else
                        {
                            var detailProduct = verificationOrderProduct?.PackageProducts?.FirstOrDefault();
                            if (detailProduct != null)
                            {
                                orderVerificationCodeNum = verificationOrderProduct.PackageOrProduct.Number * detailProduct.Number;
                                orderVerificationCodeDO.ProductName = detailProduct.ProductName;
                                orderVerificationCodeDO.MarketingPrice = detailProduct.MarketingPrice;
                            }
                        }
                    }
                    else
                    {
                        orderVerificationCodeNum = verificationOrderProduct.PackageOrProduct.Number;
                        orderVerificationCodeDO.ProductName = verificationOrderProduct.PackageOrProduct.ProductName;
                        orderVerificationCodeDO.MarketingPrice = verificationOrderProduct.PackageOrProduct.MarketingPrice;
                    }
                }
                else
                {
                    throw new CustomException(CommonConstant.VerticationCodeInformationExption);
                }
            }
            #endregion

            #region 组装待存储的数据
            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.UserName : request.Data.ReceiverName;
            telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;
            var createTime = DateTime.Now;//创建时间


            #region 用户为钻石会员 价格 折扣处理

            //钻石折扣
            // var payOrderPointValue = Convert.ToInt32(_configuration["OrderConfig:PayOrderPointValue"]);
            OrderDiscountDO orderDiscountDO = null;
            if ((getUserInfoResult?.Data?.MemberGrade ?? 0) == 50)//钻石折扣
            {
                var diamondsDiscountRate = Convert.ToDecimal(_configuration["OrderConfig:DiamondsDiscountRate"]);

                var diamondsDiscountAmount = Math.Ceiling((actualAmount * diamondsDiscountRate) * 100) / 100;//超出分的金额直接进位
                orderDiscountDO = new OrderDiscountDO()
                {
                    ActualAmount = actualAmount,
                    CreateBy = request.Data.CreatedBy ?? string.Empty,
                    CreateTime = DateTime.Now,
                    DiscountAmount = diamondsDiscountAmount,
                    DiscountRate = (float)diamondsDiscountRate,
                    DiscountType = 1,
                    Formula = "ActualAmount*DiscountRate",
                    OrderNo = string.Empty,
                    UpdateBy = string.Empty
                };


                actualAmount = diamondsDiscountAmount;
            }



            #region 组装订单主信息


            #endregion

            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = user?.UserName,
                UserPhone = user?.UserTel,
                ContactId = requestData.UserId,
                ContactName = createBy,
                ContactPhone = telephone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = shop.CompanyId,
                ShopId = requestData.ShopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = OrderSignStatusEnum.HaveSign.ToSbyte(),
                SignTime = DateTime.Now
            };

            #endregion

            #region 组装用户

            var orderUserDO = _mapper.Map<OrderUserDO>(user);
            orderUserDO.UserId = request.Data.UserId;

            #endregion

            #region 组装地址
            OrderAddressDO orderAddressDO = null;

            orderAddressDO = _mapper.Map<OrderAddressDO>(shop);
            orderAddressDO.AddressType = 1;
            orderAddressDO.ShopId = request.Data.ShopId;
            orderAddressDO.DetailAddress = shop.Address;

            if (orderAddressDO != null)
            {
                orderAddressDO.ReceiverName = createBy;
                orderAddressDO.ReceiverPhone = telephone;
                orderAddressDO.CreateBy = createBy;
                orderAddressDO.CreateTime = DateTime.Now;
            }
            #endregion

            #region 组装车型
            var orderCarDO = _mapper.Map<OrderCarDO>(car);
            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }

                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                // 存车辆
                orderCarDO.OrderId = orderId;
                orderCarDO.CreateBy = createBy;
                orderCarDO.CreateTime = createTime;
                await _orderCarRepository.InsertAsync(orderCarDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                //存储单独带出的服务
                var serviceProductDOs = new List<OrderProductDO>();
                foreach (var item in services)
                {
                    var serveForProductCodes = item.ServeForProductCodes.TrimEnd(';').Split(';');

                    var serveForProducts = allInsertedProducts.FindAll(_ => serveForProductCodes.Contains(_.ProductId));
                    var serveForOrderPids = new StringBuilder();
                    if (serveForProducts != null && serveForProducts.Any())
                    {
                        foreach (var serveForProduct in serveForProducts)
                        {
                            serveForOrderPids.Append($"{serveForProduct.Id};");
                        }
                    }
                    item.ServeForOrderPids = serveForOrderPids.ToString().TrimEnd(';');
                    var serviceProductDO = _mapper.Map<OrderProductDO>(item);
                    serviceProductDO.OrderId = orderId;
                    serviceProductDO.OrderNo = orderNo;
                    serviceProductDO.CreateBy = createBy;
                    serviceProductDO.CreateTime = createTime;
                }
                if (serviceProductDOs.Any())
                {
                    await _orderProductRepository.InsertBatchAsync(serviceProductDOs);
                    allInsertedProducts.Union(serviceProductDOs);
                }

                #endregion

                //存地址
                if (orderAddressDO != null)
                {
                    orderAddressDO.OrderId = orderId;
                    await _orderAddressRepository.InsertAsync(orderAddressDO);
                }

                //存折扣订单
                if (orderDiscountDO != null)
                {
                    orderDiscountDO.OrderNo = orderNo;
                    await _orderDiscountRepository.InsertAsync(orderDiscountDO);
                }

                if (request.Data.ArrivalId > 0)
                {
                    await _receiveClient.ArrivalRelateOrder(new ArrivalRelateOrderRequest()
                    {
                        ArrivalId = request.Data.ArrivalId,
                        OrderNo = orderNo,
                        UserId = request.Data.UserId
                    });
                }

                ts.Complete();
            }

            #endregion

            #region 发放核销码

            var orderVerificationCodeDOs = new List<OrderVerificationCodeDO>();
            orderVerificationCodeDO.OrderNo = orderNo;
            orderVerificationCodeDO.CreateBy = createBy;
            orderVerificationCodeDO.CreateTime = createTime;
            for (int i = 0; i < orderVerificationCodeNum; i++)
            {
                orderVerificationCodeDOs.Add(orderVerificationCodeDO);
            }
            await _orderVerificationCodeRepository.InsertBatchAsync(orderVerificationCodeDOs);

            var codes = await _orderVerificationCodeRepository.GetListByUserIdAndOrderNo(requestData.UserId, orderNo, true);
            //生成核销码并更新
            foreach (var item in codes)
            {
                var code = OrderHelper.GenVerificationCode(item.Id);
                item.Code = code;
                item.IsDeleted = 1;//注意：下单时预发放核销码先更新为软删除无效，待支付成功后再激活为有效可见（如果创建时软删除无法用Dapper更新Code）
                await _orderVerificationCodeRepository.UpdateAsync(item, new List<string> { "Code", "IsDeleted" });
            }

            #endregion

            //#region 通知订单中心进行信息同步

            //var syncOrderRequest = new SyncOrderRequest() { Order = _mapper.Map<MainOrderDTO>(orderDO) };
            //var orderProductDOs = await _orderProductRepository.GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            //syncOrderRequest.OrderProducts = _mapper.Map<List<MainOrderProductDTO>>(orderProductDOs?.ToList());
            //await _orderClient.SyncOrder(syncOrderRequest);

            //#endregion



            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 核销码使用提交订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            var result = Result.Failed<SubmitUseVerificationCodeOrderResponse>("下单失败");

            #region 基础检验
            var requestData = request.Data;
            var code = requestData.Code;
            if (string.IsNullOrWhiteSpace(code))
                throw new CustomException(CommonConstant.VerticationCodeIsNotNull);
            code = code.ToUpper();
            if (!code.StartsWith(VerticationCodePrexEnum.RGHX.ToString()))
                throw new CustomException(CommonConstant.VerticationCodeFormateIsMistake);
            var codeDO = await _orderVerificationCodeRepository.GetByCode(code);
            if (codeDO == null)
                throw new CustomException(CommonConstant.VerticationCodeIsNull);
            if (codeDO.Status > VerticationCodeStatusEnum.NotUse.ToSbyte())
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            if (!(codeDO.StartValidTime <= DateTime.Now && codeDO.EndValidTime.AddDays(1) > DateTime.Now))
            {
                codeDO.Status = VerticationCodeStatusEnum.Expired.ToSbyte();
                await _orderVerificationCodeRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            }

            var ruleId = codeDO.RuleId;
            var ruleDO = await _verificationRuleRepository.GetAsync(ruleId);
            if (ruleDO == null)
                throw new CustomException(CommonConstant.VerticationCodeRuleIsNotExist);
            if (ruleDO.IsValid == 0)
            {
                throw new CustomException(CommonConstant.VerticationCodeRuleInvalid);
            }
            var getShopByIdResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
            var shop = getShopByIdResult.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationException);

            bool isFitShop = false;
            if (ruleDO.IsByShopType == 1)
                isFitShop = (shop.Type & ruleDO.ShopType) == shop.Type;

            if (!isFitShop)
            {
                IEnumerable<VerificationRuleShopIdDO> ruleShopIdDOs = null;
                if (ruleDO.IsByShopId == 1)
                {
                    ruleShopIdDOs = await _verificationRuleShopIdRepository.GetListByRuleId(ruleId);
                    if (ruleShopIdDOs == null || !ruleShopIdDOs.Any())
                        throw new CustomException(CommonConstant.ShopInformationException);
                    var findShop = ruleShopIdDOs.ToList().Find(_ => _.ShopId == requestData.ShopId);
                    if (findShop != null)
                        isFitShop = true;
                }
            }
            if (!isFitShop)
                throw new CustomException(CommonConstant.ShopUnSupportThisService);

            var order = await _orderRepository.GetOrder(codeDO.OrderNo);
            var orderProducts = await _orderProductRepository.GetOrderProducts( order.Id );
            var findNonPackageProducts = orderProducts.FindAll(_ => _.ProductAttribute != ProductAttributeEnum.PackageProduct.ToSbyte());
            var orderProduct = findNonPackageProducts?.FirstOrDefault();
            if (orderProduct == null)
                throw new CustomException(CommonConstant.ProductInfomationException);

            var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
            {
                ShopId = requestData.ShopId,
                ProductIds = new List<string>() { orderProduct.ProductId }
            });
            var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
            if (serviceCosts != null)
            {
                var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false);//服务上下架校验
                if (findOfflineServices != null && findOfflineServices.Any())
                    throw new CustomException(CommonConstant.ServiceShopNotSupport);
            }

            var orderUser = await _orderUserRepository.GetOrderUser(order.Id);
            var orderCar = await _orderCarRepository.GetOrderCar(order.Id);
            var orderAddress = await _orderAddressRepository.GetOrderAddress(order.Id);
            if (orderUser == null || orderUser == null || orderAddress == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            #endregion

            #region 组装待存储数据

            var newOrderProduct = orderProduct;
            var productAmount = orderProduct.Price;//核销商品金额
            newOrderProduct.Id = 0;
            newOrderProduct.OrderId = 0;
            newOrderProduct.Amount = productAmount;
            newOrderProduct.TotalAmount = productAmount;
            newOrderProduct.TotalCostPrice = productAmount;
            newOrderProduct.ShareDiscountAmount = productAmount;
            newOrderProduct.ActualPayAmount = 0;
            newOrderProduct.Number = 1;
            newOrderProduct.TotalNumber = 1;
            newOrderProduct.ParentOrderPackagePid = 0;//核销商品所属父级套餐ID设置为0

            var newOrder = order;
            newOrder.Id = 0;
            newOrder.OrderNo = string.Empty;
            newOrder.OrderStatus = (sbyte)OrderStatusEnum.Completed;//核销使用直接完成
            newOrder.ProduceType = 2;
            newOrder.Channel = ChannelEnum.ApolloErpToShop.ToSbyte();
            newOrder.DeliveryStatus = DeliveryStatusEnum.HaveSend.ToSbyte();
            newOrder.SignStatus = OrderSignStatusEnum.HaveSign.ToSbyte();
            newOrder.SignTime = DateTime.Now;
            newOrder.DeliveryTime = DateTime.Now;
            newOrder.TerminalType = request.Data.TerminalType;
            newOrder.AppVersion = request.ApiVersion;
            newOrder.TotalProductNum = 0;
            newOrder.TotalProductAmount = 0;
            newOrder.ServiceFee = productAmount;
            newOrder.ServiceNum = 1;
            newOrder.DeliveryFee = 0;
            newOrder.TotalCouponAmount = productAmount;//TODO：暂时显示为优惠
            newOrder.ActualAmount = 0;
            newOrder.IsNeedDelivery = 0;
            newOrder.IsNeedInstall = 1;
            newOrder.PayStatus = PayStatusEnum.HavePay.ToSbyte();
            newOrder.PayTime = DateTime.Now;
            newOrder.MoneyArriveStatus = 1;
            newOrder.DispatchStatus = 1;
            newOrder.DispatchTime = DateTime.Now;
            newOrder.InstallStatus = 1;//核销订单自动安装
            newOrder.InstallTime = DateTime.Now;
            newOrder.ShopId = requestData.ShopId;


            var newOrderUser = orderUser;
            newOrderUser.Id = 0;
            newOrderUser.OrderId = 0;

            var newOrderCar = orderCar;
            newOrderCar.Id = 0;
            newOrderCar.OrderId = 0;

            var newOrderAddress = orderAddress;
            newOrderAddress.Id = 0;
            newOrderAddress.OrderId = 0;

            #endregion

            #region 计算成本

            serviceCosts?.ForEach(_ =>
            {
                if (_.CostPrice > 0)
                {
                    if (newOrderProduct.ProductId == _.PID)
                    {
                        newOrderProduct.TotalCostPrice = _.CostPrice;
                    }
                }
            });

            #endregion

            #region 事务开始提交订单

            var newOrderId = 0;
            var newOrderNo = string.Empty;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                newOrderId = await _orderRepository.InsertAsync(newOrder);
                newOrderNo = $"RGB{newOrderId.ToString().PadLeft(5, '0')}";//补0到5位
                newOrder.OrderNo = newOrderNo;

                newOrder.InstallCode = OrderHelper.GenInstallCode(newOrderId);//需要安装的订单生成安装码用于到店验证

                await _orderRepository.UpdateOrderNoAndInstallCode(newOrderId, newOrderNo, newOrder.InstallCode);

                newOrderProduct.OrderId = newOrderId;
                newOrderProduct.OrderNo = newOrderNo;
                await _orderProductRepository.InsertAsync(newOrderProduct);

                newOrderUser.OrderId = newOrderId;
                await _orderUserRepository.InsertAsync(newOrderUser);

                newOrderCar.OrderId = newOrderId;
                await _orderCarRepository.InsertAsync(newOrderCar);

                newOrderAddress.OrderId = newOrderId;
                await _orderAddressRepository.InsertAsync(newOrderAddress);



                ts.Complete();
            }

            #endregion

            #region  更新核销码使用状态和单号
            codeDO.Status = 1;
            codeDO.VerifyOrderNo = newOrderNo;
            codeDO.VerifyShopId = requestData.ShopId;
            codeDO.VerifyTime = DateTime.Now;
            codeDO.UpdateBy = requestData.ShopId.ToString();
            codeDO.UpdateTime = DateTime.Now;
            await _orderVerificationCodeRepository.UpdateAsync(codeDO,
                new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "VerifyTime", "UpdateBy", "UpdateTime" });

            #endregion

            //#region 通知订单中心进行信息同步

            //var syncOrderRequest = new SyncOrderRequest() { Order = _mapper.Map<MainOrderDTO>(newOrder) };
            //var orderProductDOs = await _orderProductRepository.GetListAsync("where order_id=@OrderId", new { OrderId = newOrderId });
            //syncOrderRequest.OrderProducts = _mapper.Map<List<MainOrderProductDTO>>(orderProductDOs?.ToList());
            //await _orderClient.SyncOrder(syncOrderRequest);

            //#endregion

            result = Result.Success(new SubmitUseVerificationCodeOrderResponse() { OrderNo = newOrderNo }, "下单成功");

            return result;
        }

        /// <summary>
        /// 修改订单派工记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderDispatch(ApiRequest<List<UpdateOrderDispatchRequest>> request)
        {
            var requrestOrderNos = new List<string>() { request.Data.FirstOrDefault().OrderNo };
            var getShopArrivalOrders = await _receiveClient.GetShopArrivalOrder(new GetShopArrivalOrderRequest()
            {
                OrderNos = requrestOrderNos
            });

            //var getOrderNos = getShopArrivalOrders?.Data?.Select(_ => _.OrderNo)?.ToList();
            //bool OrderNoIsEqual = requrestOrderNos.Equal_EveryItem(getOrderNos);
            //if (!OrderNoIsEqual)
            //{
            //    throw new CustomException("订单未关联到店记录");
            //}

            var result = new ApiResult() { Code = ResultCode.Failed, Message = "修改派工失败" };
            List<OrderDispatchDO> orderDispatchDO = _mapper.Map<List<OrderDispatchDO>>(request.Data);
            var insertRecord = 1;
            var isSuccess = await _orderDispatchRepository.DeleteOrderDispatch(request.Data.FirstOrDefault());
            if (isSuccess)
            {
                await _orderDispatchRepository.InsertBatchAsync(orderDispatchDO);
            }
            if (insertRecord > 0)
            {
                if (request.Data.FirstOrDefault().OrderNo?.StartsWith("RGB") ?? false)
                    await _orderRepository.UpdateOrderDispatchStatus(new List<string>() { request.Data.FirstOrDefault().OrderNo }, request.Data.FirstOrDefault().CreateBy);
                if (request.Data.FirstOrDefault().OrderNo?.StartsWith("RGC") ?? false)
                    await _orderCommandForCClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                    {
                        OrderNos = new List<string>() { request.Data.FirstOrDefault().OrderNo },
                        CreateBy = request.Data.FirstOrDefault().CreateBy
                    });

                await _orderClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                {
                    OrderNos = new List<string>() { request.Data.FirstOrDefault().OrderNo },
                    CreateBy = request.Data.FirstOrDefault().CreateBy
                });
                result = new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "修改派工成功"
                };
            }

            return result;
        }

        /// <summary>
        /// 取消派工
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrderDispatch(ApiRequest<CancelOrderDispatchRequest> request)
        {
            var result = new ApiResult() { Code = ResultCode.Failed, Message = "取消派工失败" };

            var requrestOrderNos = new List<string>() { request.Data.OrderNo };
            var getShopArrivalOrders = await _receiveClient.GetShopArrivalOrder(new GetShopArrivalOrderRequest()
            {
                OrderNos = requrestOrderNos
            });

            var getOrderNos = getShopArrivalOrders?.Data?.Select(_ => _.OrderNo)?.ToList();
            bool OrderNoIsEqual = requrestOrderNos.Equal_EveryItem(getOrderNos);
            if (!OrderNoIsEqual)
            {
                throw new CustomException("订单未关联到店记录");
            }

            UpdateOrderDispatchRequest updateOrderDispatchRequest = new UpdateOrderDispatchRequest()
            {
                OrderNo = request.Data.OrderNo,
                CreateBy = request.Data.CreateBy
            };

            var isSuccess = await _orderDispatchRepository.DeleteOrderDispatch(updateOrderDispatchRequest);
            if (isSuccess)
            {
                if (request.Data.OrderNo?.StartsWith("RGB") ?? false)
                    await _orderRepository.UpdateOrderDispatchStatus(new List<string>() { request.Data.OrderNo }, request.Data.CreateBy, 0);
                if (request.Data.OrderNo?.StartsWith("RGC") ?? false)
                    await _orderCommandForCClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                    {
                        OrderNos = new List<string>() { request.Data.OrderNo },
                        CreateBy = request.Data.CreateBy,
                        DispatchStatus = 0
                    });

                await _orderClient.UpdateOrderDispatchStatus(new UpdateOrderDispatchStatusRequest()
                {
                    OrderNos = new List<string>() { request.Data.OrderNo },
                    CreateBy = request.Data.CreateBy,
                    DispatchStatus = 0
                });
                result = new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "取消派工成功"
                };
            }
            return result;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.Data.OrderNo },
                //ShopId = request.Data.ShopId
            });
            _logger.Info($"CancelOrder:{JsonConvert.SerializeObject(getOrderBase)}");
            var orderInfo = getOrderBase?.Data?.FirstOrDefault();
            var orderNo = orderInfo?.OrderNo ?? string.Empty;
            if (string.IsNullOrEmpty(orderNo))
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = request.Data.OrderNo+ "，此订单不存在"
                };
            }

            orderInfo.UpdateBy = request.Data.CreateBy;

            var payStatus = orderInfo.PayStatus ;
            var reconciliationStatus = orderInfo.ReconciliationStatus ;
            var settleStatus = orderInfo.SettleStatus ;
            var stockStatus = orderInfo.StockStatus;
            var orderStatus = orderInfo.OrderStatus;

            //if (payStatus == PayStatusEnum.HavePay.ToSbyte())
            //    return new ApiResult()
            //    {
            //        Code = ResultCode.Failed,
            //        Message = "此订单已经付款完成，不允许取消订单"
            //    };


            //if (request.Data.OrderNo?.StartsWith("RGB") ?? false)
            var cancelResult = await _orderRepository.CancelOrder(orderNo, orderInfo.UpdateBy);
            //if (request.Data.OrderNo?.StartsWith("RGC") ?? false)
            //    await _orderCommandForCClient.CancelOrder(request);

            // var syncResult = await _orderClient.CancelOrder(request);

            var userCoupon = await _orderCouponRepository.GetOrderCoupon(orderInfo.Id);

            if ((userCoupon?.UserCouponId ?? 0) > 0)
            {
                var userCouponResult = await _couponClient.UpdateUserCouponStatusUnusedById(new UpdateUserCouponStatusReqByIdRequest()
                {
                    UserCouponId = userCoupon?.UserCouponId ?? 0,
                    UserId = request.Data.CreateBy ?? CommonConstant.User
                });

                if (userCouponResult.Code == ResultCode.Success)
                {
                    await _orderCouponRepository.DeleteOrderCoupon(userCoupon?.UserCouponId ?? 0, request.Data.CreateBy ?? CommonConstant.User);
                }
            }


            //还原库存,无批次，txw,2023-4
            if (orderStatus == OrderStatusEnum.Completed.ToInt())
            {
                var stockResponse = await _shopStockClient.OrderCancelReleaseStockNoBatch(new Core.Request.Stock.ReleaseStockRequest()
                {
                    QueueNo = orderNo,
                    CreateBy = orderInfo.UpdateBy ?? "System",
                    QueueStatus = string.Empty,
                    SourceType = string.Empty
                });
            }
            
            //还原库存,2已占库
            //if (stockStatus == 2)
            //{
            //    var stockResponse = await _shopStockClient.ReleaseStock(new Core.Request.Stock.ReleaseStockRequest()
            //    {
            //        QueueNo = request.Data.OrderNo,
            //        CreateBy = request.Data.CreateBy ?? "System",
            //        QueueStatus = string.Empty,
            //        SourceType = string.Empty
            //    });
            //    if (stockResponse?.Code == ResultCode.Success)
            //    {
            //        int.TryParse(Regex.Replace(request.Data.OrderNo, @"\D", ""), out var orderId);
            //        var products = await _orderProductRepository.GetOrderProducts(orderId);
            //        var findProductIds = products?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Select(_ => _.Id)?.ToList();
            //        if (findProductIds?.Count() > 0)
            //        {
            //            bool isUpdateProductStockStatusSuccess = await _orderProductRepository.UpdateProductStockStatus(orderId, 4, "OrderUseStockNotify", findProductIds);
            //        }
            //        bool isUpdateOrderStockStatusSuccess = await _orderRepository.UpdateOrderStockStatus(orderId, 4, "OrderUseStockNotify");
            //    }
            //}
            //2已对账: 删除对账记录
            if (reconciliationStatus == 2)
            {
                await _fmsClient.DeleteAccountCheck(new CalculationReconciliationFeeRequest()
                {
                    OrderNo = orderNo,
                    CreateBy = request.Data.CreateBy
                });
            }

            //月结对账支付订单
            if (orderInfo.ProduceType == BuyProductTypeEnum.MonthReconciliationOrder.ToSbyte())
            {
                await _delegateUserOrderRepository.ClearOrderRefInfo(orderNo, orderInfo.UpdateBy);
            }
           

            //更新员工绩效订单
            _employeePerformanceService.UpdateOrderPerformance(new BatchUpdateOrderRequest()
            {
                OrderNos = new List<string>() { orderNo },
                ShopId = orderInfo?.ShopId ?? 0,
                OrderDate = orderInfo?.OrderTime.ToString("D"),
                UpdateBy = orderInfo.UpdateBy
            });


            if (cancelResult)
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

        /// <summary>
        /// 支付成功回写
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> OrderPaySuccessNotify(OrderPaySuccessNotifyRequest request)
        {
            var result = Result.Failed("处理失败");

            var getBaseOrders = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.OrderNo }
            });
            var order = getBaseOrders?.Data?.FirstOrDefault();

            if (order.PayStatus == PayStatusEnum.HavePay.ToSbyte())
            {
                result = Result.Success("支付状态已经是已支付无需重复执行");
                return result;
            }
            if (order.MoneyArriveStatus == MoneyArriveStatusEnum.Arrive.ToSbyte())
            {
                result = Result.Success("到账状态已经是已到账无需重复执行");
                return result;
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var updatePayStatusRequest = _mapper.Map<UpdatePayStatusRequest>(request);
                updatePayStatusRequest.OrderNo = order.OrderNo;
                updatePayStatusRequest.PayStatus = 1;
                //var updatePayStatusResult = await _orderClient.UpdatePayStatus(updatePayStatusRequest);
                //if (!updatePayStatusResult.IsNotNullSuccess())
                //{
                //    throw new Exception("更新支付状态失败");
                //}

                // if (request.OrderNo?.StartsWith("RGB") ?? false)
                await _orderRepository.UpdatePayStatus(order.OrderNo, PayStatusEnum.HavePay.ToSbyte(),
                     request.PayTime, request.UpdateBy, request.PayMethod, request.PayChannel);
                //if (request.OrderNo?.StartsWith("RGC") ?? false)
                //    await _orderCommandForCClient.UpdatePayStatus(updatePayStatusRequest);

                //var updateMoneyArriveStatusResult = await _orderClient.UpdateMoneyArriveStatus(new UpdateMoneyArriveStatusRequest()
                //{ OrderNo = order.OrderNo, MoneyArriveStatus = MoneyArriveStatusEnum.Arrive.ToSbyte(), UpdateBy = request.UpdateBy });

                //if (!updateMoneyArriveStatusResult.IsNotNullSuccess())
                //{
                //    throw new Exception("更新到账状态失败");
                //}
                // if (request.OrderNo?.StartsWith("RGB") ?? false)
                if (request.PayMethod != 3) //月结
                    await _orderRepository.UpdateMoneyArriveStatus(order.OrderNo, MoneyArriveStatusEnum.Arrive.ToSbyte(), request.UpdateBy);

                ///为了兼容C端下单产生的上门安装订单
                if (order.OrderStatus < OrderStatusEnum.Confirmed.ToInt()) 
                    await _orderRepository.UpdateOrderConfirmStatus(order.OrderNo, request.UpdateBy);


                //if (request.OrderNo?.StartsWith("RGC") ?? false)
                //    await _orderCommandForCClient.UpdateMoneyArriveStatus(new UpdateMoneyArriveStatusRequest()
                //    { OrderNo = order.OrderNo, MoneyArriveStatus = MoneyArriveStatusEnum.Arrive.ToSbyte(), UpdateBy = request.UpdateBy });
                if (order.ProduceType == BuyProductTypeEnum.BuyVerticationCode.ToSbyte())
                {
                    //激活核销码
                    var isActiveByOrderNoSuccess = await _orderVerificationCodeRepository.ActiveByOrderNo(order.OrderNo);

                    if (!isActiveByOrderNoSuccess)
                    {
                        throw new Exception("激活核销码失败");
                    }
                }

                if (order.ProduceType == ProductTypeEnum.BuyPackageCard.ToSbyte())
                {
                    //激活核销码
                    var isActiveByOrderNoSuccess = await _orderPackageCardRepository.ActiveByOrderNo(order.OrderNo);
                    if (!isActiveByOrderNoSuccess)
                    {
                        throw new CustomException("激活核销码失败");
                    }

                    //移到下面，非购买套餐卡订单也可以赠券
                    //var orderProduct = (await _orderProductRepository.GetListAsync(" where is_deleted=0 and order_no=@OrderNo and parent_order_package_pid=0", new { OrderNo = order.OrderNo }, true))?.FirstOrDefault();

                    //await _couponClient.GiftCouponForOrder(new GiftCouponForOrderRequest()
                    //{
                    //    OrderNo = order.OrderNo,
                    //    Channel = AdaptChannelEnum.OnLine,
                    //    TerminalType = Client.Request.Coupon.CouponActivityChannel.MiniApp,
                    //    UserId = order.UserId,
                    //    ProductList = new List<OrderProductRequest>()
                    //        {
                    //            new OrderProductRequest()
                    //            {
                    //                CategoryCode=orderProduct?.CategoryCode,
                    //                Count=orderProduct?.TotalNumber??0,
                    //                Pid=orderProduct?.ProductId,
                    //                Price=orderProduct?.Price??0
                    //            }
                    //        }
                    //});
                }

                ts.Complete();
            }

            ////积分充值 移到安装状态更新时候处理，因为C端订单可能会提前支付
            ////var payOrderPointValue = Convert.ToInt32(configuration["OrderConfig:PayOrderPointValue"]);
            ////积分规则改成支付成功后按支付金额送积分，只取整数部分，1元=1积分
            //int payOrderPointValue = Convert.ToInt32(Math.Floor(order.ActualAmount));
            //if (payOrderPointValue > 0)
            //{
            //    int referrerPointValue = payOrderPointValue;

            //    var userInfo = await _userClient.GetUserInfo(new GetUserInfoRequest()
            //    {
            //        UserId = order.UserId
            //    });
                
            //    if ((userInfo?.Data?.MemberGrade ?? 0) == 50)//砖石会员 积分double
            //    {
            //        payOrderPointValue *= 2;
            //    }
            //    var operateUserPointRequest = new OperateUserPointRequest()
            //    {
            //        UserId = order.UserId,
            //        OperateType = UserPointOperateTypeEnum.PayOrder,
            //        PointValue = payOrderPointValue,
            //        SubmitBy = "系统",
            //        Remark = order.OrderNo
            //    };
            //    var operateUserPointResult = await _userClient.OperateUserPoint(operateUserPointRequest);
            //    _logger.Info($"OrderPaySuccessNotify 购买成功赠送积分 operateUserPointRequest={JsonConvert.SerializeObject(operateUserPointRequest)} operateUserPointResult={JsonConvert.SerializeObject(operateUserPointResult)}");

            //    if (userInfo?.Data?.Channel == ChannelType.Consumer && !string.IsNullOrEmpty(userInfo?.Data?.ReferrerUserId))
            //    {
            //        await _userClient.OperateUserPoint(new OperateUserPointRequest
            //        {
            //            UserId = userInfo?.Data?.ReferrerUserId,
            //            OperateType = UserPointOperateTypeEnum.ReferrerOrder,
            //            PointValue = referrerPointValue,
            //            SubmitBy = order.UserId,
            //            Remark = $"推荐订单：{order.OrderNo}"
            //        });
            //    }
            //}

            var orderProducts = await _orderClient.GetOrderProduct(new GetOrderProductRequest()
            {
                OrderNos = new List<string>(1) { request.OrderNo }
            });

            //购买产品赠优惠券
            var itemProducts = orderProducts?.Data?.Where(t => t.ParentOrderPackagePid == 0).ToList();
            if (itemProducts.Any())
            {
                var aProductList = new List<OrderProductRequest>();
                foreach (var orderProduct in itemProducts)
                {
                    aProductList.Add(
                                new OrderProductRequest()
                                {
                                    CategoryCode = orderProduct?.CategoryCode,
                                    Count = orderProduct?.TotalNumber ?? 0,
                                    Pid = orderProduct?.ProductId,
                                    Price = orderProduct?.Price ?? 0
                                });               

                }
                _couponClient.GiftCouponForOrder(new GiftCouponForOrderRequest()
                {
                    OrderNo = order.OrderNo,
                    Channel = AdaptChannelEnum.OffLine,
                    TerminalType = Client.Request.Coupon.CouponActivityChannel.AndroidApp,
                    UserId = order.UserId,
                    ProductList = aProductList
                });
            }


            //实物产品占库存
            var isContainsRealProduct = orderProducts?.Data?.Exists(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte() || _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte());

            if (isContainsRealProduct ?? false)
            {
                //txw20221206 简化库存取消批次和占库

                ///为了兼容C端下单产生的上门安装订单 付完钱之后才去占库，因为需要占门店库存，支付回调的时候调用了B端的回调接口
                //_shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
                //{
                //    QueueNo = order.OrderNo,
                //    CreateBy = request.UpdateBy,
                //    QueueStatus = "OrderService"
                //});

            }

            if (order.ProduceType == ProductTypeEnum.DelegateShopDepositOrder.ToSbyte())
            {
                _shopClient.UpdateShopDeposit(new UpdateShopDepositRequest()
                {
                    Amount = order?.ActualAmount ?? 0,
                    ShopId = order?.ShopId ?? 0,
                    UpdateBy = request.UpdateBy
                });
            }
            if (order.ProduceType == ProductTypeEnum.DelegateCompanyDepositOrder.ToSbyte())
            {
                _shopClient.OperateCompanyDeposit(new OperateCompanyDepositRequest()
                {
                    CompanyId = order?.ShopId ?? 0,
                    DepositAmount = order?.ActualAmount ?? 0,
                    Remark = string.Empty,
                    SubmitBy = order?.CreateBy,
                    OperateType = CompanyDepositOperationEnum.Payment
                });
            }

            result = Result.Success("处理成功");

            return result;
        }

        /// <summary>
        /// 合并支付收款完成后安装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> MergeOrderPayInstall(OrderPayInstallRequest request)
        {
            string mergNo = request.OrderNo;
            var getorders = await iPayClient.GetMergePaysByMergeOrder(mergNo);
            if (!getorders.IsNotNullSuccess() || getorders == null || getorders.Data == null || !getorders.Data.Any())
            {
                throw new CustomException("该合并支付单号未查到或还未支付成功");
            }

            string errMsg = string.Empty;
            foreach (var order in getorders.Data)
            {
                try
                {
                    request.OrderNo = order.OrderNo;
                    var oneResult = await OrderPayInstall(request);
                    if (!oneResult.IsNotNullSuccess())
                    {
                        errMsg += order.OrderNo + ",";
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"MergeOrderPayInstall异常，订单号:{order.OrderNo}", ex);
                }

            }

            if (errMsg != "")
            {
                _logger.Error($"对应合并支付单号{mergNo}的订单号:{errMsg}未成功安装");
                return Result.Failed($"对应合并支付单号{mergNo}的订单号:{errMsg}未成功安装，请手动完成安装");
            }
            else
            {
                return Result.Success("处理成功");
            }
        }


        /// <summary>
        /// 收款完成后安装
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> OrderPayInstall(OrderPayInstallRequest request)
        {
            var result = Result.Success("处理成功");

            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.OrderNo }
            });
            var orderInfo = getOrderBase?.Data?.FirstOrDefault();
            orderInfo.UpdateBy = request.UpdateBy;

            var payStatus = orderInfo?.PayStatus ?? PayStatusEnum.NotPay.ToSbyte();

            if (payStatus == PayStatusEnum.NotPay.ToSbyte())
            {
                OrderPaySuccessNotifyRequest orderPaySuccessNotifyRequest = _mapper.Map<OrderPaySuccessNotifyRequest>(request);

                if (request.PayTime.Year == 1)//为了0元订单 支付不传支付时间
                    orderPaySuccessNotifyRequest.PayTime = DateTime.Now;

                var orderPaySuccess = await OrderPaySuccessNotify(orderPaySuccessNotifyRequest);
                if (orderPaySuccess.Code != ResultCode.Success)
                    return orderPaySuccess;
            }

            var orderNo = orderInfo?.OrderNo ?? string.Empty;
            if (orderNo.StartsWith("RGC"))
            {

            }
            if (orderNo.StartsWith("RGB"))
            {
                // await _orderRepository.UpdateOrderInstallStatus(orderNo, request.UpdateBy);
            }
            await _orderClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
            {
                OrderNos = new List<string>() { orderNo },
                UpdateBy = request.UpdateBy,
                UpdateStatusType = UpdateOrderStatusTypeEnum.InstallStatus
            });

            await _orderRepository.UpdateOrderCompleteStatus(orderNo, request.UpdateBy);

            //订单安装后扣减库存，不区分RGC/RGB
            _shopStockClient.OrderInstallReduceStock(new Core.Request.Stock.OrderInstallReduceStockRequest
            {
                QueueNo = orderNo,
                CreateBy = request.UpdateBy,
                QueueStatus = string.Empty,
                SourceType = string.Empty
            });


            // 计算门店对账公司对账金额
            _fmsClient.CalculationReconciliationFee(new CalculationReconciliationFeeRequest()
            {
                OrderNo = orderNo,
                CreateBy = request.UpdateBy
            });

            //月结对账支付订单
            if (orderInfo?.ProduceType == BuyProductTypeEnum.MonthReconciliationOrder.ToSbyte())
            {
                UpdateMonthReconciliationRefOrder(orderNo, request.UpdateBy, request.PayChannel);
            }

            //更新员工绩效订单
            _employeePerformanceService.UpdateOrderPerformance(new BatchUpdateOrderRequest()
            {
                OrderNos = new List<string>() { orderNo },
                ShopId = orderInfo ?.ShopId ?? 0,
                OrderDate = orderInfo ?.OrderTime.ToString("D"),
                UpdateBy = request.UpdateBy
            });

            // 订单完结发放促销优惠卷-当分享人达到10人下保养订单时即发放指定优惠券
            _orderCommandForCClient.SharingPromotion(new SharingPromotionRequest()
            {
                UserId = orderInfo?.UserId,
                ActualAmount = orderInfo?.ActualAmount ?? 0,
                OrderNo = orderInfo?.OrderNo
            });

            //积分充值
            if (orderInfo.ActualAmount > 0)
            {
                UpdateUserPointByOrder(orderInfo);
            }

            return result;
        }

        /// <summary>
        /// 订单完成后更新用户积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task UpdateUserPointByOrder(OrderDTO request)
        {
            var order = request;
            var payOrderPointRate = Convert.ToDecimal(_configuration["OrderConfig:PayOrderPointRate"]);
            var payOrderPointRate40 = Convert.ToDecimal(_configuration["OrderConfig:PayOrderPointRate40"]);
            var payOrderPointRate50 = Convert.ToDecimal(_configuration["OrderConfig:PayOrderPointRate50"]);
            var payOrderPointRateReferrer = Convert.ToDecimal(_configuration["OrderConfig:PayOrderPointRateReferrer"]);
            //积分规则改成支付成功后按支付金额送积分，只取整数部分，1元=1积分*比率
            var amt = order.ActualAmount * payOrderPointRate;
            var amtReferrer = amt * payOrderPointRateReferrer;

            var userInfo = await userClient.GetUserInfo(new GetUserInfoRequest()
            {
                UserId = order.UserId
            });
            var userGrade = userInfo?.Data?.MemberGrade ?? 0;
            if (userGrade == 40)//铂金会员 积分
            {
                amt *= payOrderPointRate40;
            }
            else if (userGrade == 50)//砖石会员 积分double
            {
                amt *= payOrderPointRate50;
            }
            int payOrderPointValue = Convert.ToInt32(Math.Floor(amt));
           
            if (payOrderPointValue > 0)
            {
                var operateUserPointRequest = new OperateUserPointRequest()
                {
                    UserId = order.UserId,
                    OperateType = UserPointOperateTypeEnum.PayOrder,
                    PointValue = payOrderPointValue,
                    ReferrerNo = order.OrderNo,
                    SubmitBy = request.UpdateBy,
                    Remark = order.UserPhone
                };
                //var operateUserPointResult = await
                await _userClient.OperateUserPoint(operateUserPointRequest);
                //_logger.Info($"OrderPaySuccessNotify 购买成功赠送积分 operateUserPointRequest={JsonConvert.SerializeObject(operateUserPointRequest)} operateUserPointResult={JsonConvert.SerializeObject(operateUserPointResult)}");

                if (userInfo?.Data?.Channel == ChannelType.Consumer && !string.IsNullOrEmpty(userInfo?.Data?.ReferrerUserId))
                {
                    int referrerPointValue = Convert.ToInt32(Math.Floor(amtReferrer));
                    await _userClient.OperateUserPoint(new OperateUserPointRequest
                    {
                        UserId = userInfo?.Data?.ReferrerUserId,
                        OperateType = UserPointOperateTypeEnum.ReferrerOrder,
                        PointValue = referrerPointValue,
                        ReferrerNo = order.OrderNo,
                        SubmitBy = request.UpdateBy,
                        Remark = $"推荐订单：{order.UserPhone}"
                    });
                }
            }
        }

        /// <summary>
        /// 更新月度结算订单的后续操作
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="updateBy"></param>
        /// <param name="payChannel"></param>
        /// <returns></returns>
        private async Task UpdateMonthReconciliationRefOrder(string orderNo, string updateBy, sbyte payChannel)
        {

            var delegateUserOrderDOs = await _delegateUserOrderRepository.GetListAsync(" where is_deleted=0 and ref_order_no =@OrderNo", new { OrderNo = orderNo });

            if (delegateUserOrderDOs?.Count() > 0)
            {
                await _orderClient.BatchUpdatePayStatus(new BatchUpdatePayStatusRequest()
                {
                    OrderNo = delegateUserOrderDOs.Select(_ => _.OrderNo)?.ToList(),
                    PayMethod = PayMethodEnum.Month.ToInt(),
                    PayChannel = payChannel,
                    UpdateBy = updateBy
                });

                delegateUserOrderDOs?.ToList()?.ForEach(async _ =>
                {
                    await _fmsClient.CalculationReconciliationFee(new CalculationReconciliationFeeRequest()
                    {
                        OrderNo = _.OrderNo,
                        CreateBy = updateBy
                    });
                });
            }

        }


        /// <summary>
        ///  释放库存配合自动任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ReleaseShopStock(ReleaseShopStockRequest request)
        {
            var orderList = new List<OrderDTO>(200);
            var startDate = DateTime.Now.AddDays(-3).ToString();
            if (!string.IsNullOrWhiteSpace(request.StartDate))
            {
                startDate = request.StartDate;
            }
            var getNotPayOrders = await _orderClient.GetNotPayHaveStockOrder(new GetNotPayOrderRequest()
            {
                StartDate = DateTime.Now.AddDays(-3).ToString(),
                PageIndex = 1,
                PageSize = 1
            });

            var totalCount = getNotPayOrders?.Data?.TotalItems;

            for (var i = 0; i < (totalCount / 100) + 1; i++)
            {
                var getNotPayResponse = await _orderClient.GetNotPayHaveStockOrder(new GetNotPayOrderRequest()
                {
                    StartDate = DateTime.Now.AddDays(-3).ToString(),
                    PageIndex = i + 1,
                    PageSize = 100
                });
                if (getNotPayOrders?.Data?.Items?.Count() > 0)
                    orderList.AddRange(getNotPayOrders?.Data?.Items);
            }

            if (orderList.Count > 0)
                ExecuteReleaseStock(orderList.Select(_ => _.OrderNo).ToList());

            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "执行成功"
            };
        }

        private async Task ExecuteReleaseStock(List<string> orderNos)
        {
            //只兼容B端下单的订单
            //C,B 下单占用的库存不一样，所以释放的逻辑不一样
            if (orderNos.Count > 0)
            {
                var rgbOrders = new List<string>(orderNos.Count);
                orderNos?.ForEach(_ =>
                {
                    if (_.StartsWith("RGB"))
                    {
                        rgbOrders.Add(_);
                    }
                });
                var releaseStock = new ConcurrentBag<string>();

                while (true)
                {
                    if (releaseStock.Count == rgbOrders.Count)
                        break;
                    for (var i = 0; i < rgbOrders.Count; i++)
                    {
                        var stockResponse = await _shopStockClient.ReleaseStock(new Core.Request.Stock.ReleaseStockRequest()
                        {
                            QueueNo = rgbOrders[i],
                            CreateBy = "System",
                            QueueStatus = string.Empty,
                            SourceType = string.Empty
                        });
                        if (stockResponse?.Code == ResultCode.Success)
                        {
                            int.TryParse(Regex.Replace(rgbOrders[i], @"\D", ""), out var orderId);
                            var products = await _orderProductRepository.GetOrderProducts(orderId);
                            var findProductIds = products?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Select(_ => _.Id)?.ToList();
                            if (findProductIds?.Count() > 0)
                            {
                                bool isUpdateProductStockStatusSuccess = await _orderProductRepository.UpdateProductStockStatus(orderId, 4, "OrderUseStockNotify", findProductIds);
                                var updatProductStockStatusResult = await _orderClient.UpdateProductStockStatus(new UpdateProductStockStatusRequest()
                                {
                                    OrderNo = rgbOrders[i],
                                    StockStatus = 4,
                                    UpdateBy = "OrderUseStockNotify",
                                    OrderPids = findProductIds
                                });
                            }
                            bool isUpdateOrderStockStatusSuccess = await _orderRepository.UpdateOrderStockStatus(orderId, 4, "OrderUseStockNotify");
                            var updateOrderStockStatusResult = await _orderClient.UpdateOrderStockStatus(new UpdateOrderStockStatusRequest()
                            {
                                OrderNo = rgbOrders[i],
                                StockStatus = 4,
                                UpdateBy = "OrderUseStockNotify"
                            });
                            releaseStock.Add(rgbOrders[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新订单地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request)
        {
            request.Data.OrderNo = request.Data.OrderNo.Replace("RGB", "").Replace("RGC", "");
            var updateResponse = await _orderAddressRepository.UpdateOrderAddress(request.Data);



            if (updateResponse)
                return Result.Success("更新成功");
            return Result.Failed("更新失败");
        }

        public async Task<ApiResult> SaveOrderMachineFilter(ApiRequest<SaveOrderMachineFilterRequest> request)
        {
            var deletStatus = 0;
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.Data.OrderNo }
            });

            var repeatStatus = false;

            if (getOrderBase?.Data?.FirstOrDefault()?.StockStatus == 1 || getOrderBase?.Data?.FirstOrDefault()?.StockStatus == 0)
            {
                var getOrderProduct = await _orderClient.GetOrderProduct(new GetOrderProductRequest()
                {
                    OrderNos = new List<string>() { request.Data.OrderNo }
                });

                var pidCodes = getOrderProduct?.Data?.Select(_ => _.ProductId)?.ToList();

                var productCodes = await _productClient.GetProductsByProductCodes(new ProductDetailSearchRequest()
                {
                    ProductCodes = pidCodes
                });

                string lvProductCode = "BYFW-LQQ-OF-568";

                var lvQProducts = productCodes?.Data?.Select(_ => _.Product)?.Where(_ => _.ChildCategoryId == 69 && _.ProductCode != lvProductCode);

                if (pidCodes?.Contains(lvProductCode) ?? false)
                {
                    repeatStatus = true;
                }

                var ids = new List<long>(5);
                lvQProducts?.ToList()?.ForEach(item =>
                {
                    ids.AddRange(getOrderProduct?.Data?.Where(_ => _.ProductId == item?.ProductCode && (_.StockStatus == 1 || _.StockStatus == 0))?.Select(_ => _.Id)?.ToList());
                });

                if (ids != null && ids.Any())
                {
                    var waitingProducts = getOrderProduct?.Data?.Where(_ => ids.Contains(_.Id))?.ToList();

                    var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(waitingProducts);

                    var replaceProduct = await _productClient.GetProductsByProductCodes(new ProductDetailSearchRequest()
                    {
                        ProductCodes = new List<string>() { lvProductCode }
                    });

                    packageDetailDOs?.ForEach(_ =>
                    {
                        _.ProductId = lvProductCode;
                        _.ProductName = replaceProduct.Data?.FirstOrDefault()?.Product?.Name ?? string.Empty;
                        _.ImageUrl = replaceProduct.Data?.FirstOrDefault()?.Product?.Image1 ?? string.Empty;
                        _.Price = replaceProduct.Data?.FirstOrDefault()?.Product?.SalesPrice ?? 0;
                        _.CreateBy = request.Data.CreateBy ?? string.Empty;
                        _.UpdateBy = string.Empty;
                        _.CreateTime = DateTime.Now;
                        _.StockStatus = 2;
                        _.ProductAttribute = ProductAttributeEnum.DigitProduct.ToSbyte();
                    });

                    var stockingProductIds = getOrderProduct?.Data?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Where(_ => (_.StockStatus == 1 || _.StockStatus == 0))?.Select(_ => _.Id);

                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        deletStatus = await _orderProductRepository.DeleteOrderProducts(ids, request.Data.OrderNo, request.Data.CreateBy ?? string.Empty);

                        _orderProductRepository.InsertBatch(packageDetailDOs);

                        if (stockingProductIds?.Count() == ids?.Count())
                        {
                            await _orderRepository.UpdateOrderStockStatus(getOrderBase?.Data?.FirstOrDefault()?.Id ?? 0, 2, "OrderUseStockNotifyBySaveOrderMachineFilter");
                            await _orderRepository.UpdateOrderSignStatus(request.Data.OrderNo, "OrderUseStockNotifyBySaveOrderMachineFilter");
                        }

                        ts.Complete();
                    }
                }
            }

            if (deletStatus > 0)
            {
                return Result.Success("操作成功");
            }

            if (repeatStatus)
                return Result.Success("产品中机滤已经成功替换，不可以重复替换！");
            return Result.Failed("操作失败");
        }

        public async Task<ApiPagedResult<UserCouponVO>> GetUserCoupons(GetUserCouponsRequest request)
        {
            _logger.Info($"GetUserCoupons：{JsonConvert.SerializeObject(request)} ");
            List<long> userTrialOrderCoupons = new List<long>(5);

            if (request.ProductInfos?.Count > 0)
            {
                var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.ShopId });
                var shop = getShopResult?.GetSuccessData();

                #region 获取自营产品的信息

                var selfProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();
                var shopProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();

                var products = new List<OrderConfirmPackageProductDTO>(3);
                var services = new List<OrderConfirmProductDTO>(3);
                if (selfProducts?.Count() > 0)
                {
                    var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(new GetOrderConfirmPackageProductServicesRequest()
                    {
                        CarId = request.CarId,
                        UserId = request.UserId,
                        ProvinceId = getShopResult?.Data?.ProvinceId,
                        CityId = getShopResult?.Data?.CityId,
                        ProductInfos = selfProducts
                    });
                    _logger.Info($"GetOrderConfirmPackageProductServices：{JsonConvert.SerializeObject(getPackageProductServicesResponse)} ");
                    products = getPackageProductServicesResponse.Products;
                    services = getPackageProductServicesResponse.Services;
                }

                var calcCouponProducts = new List<Product>();//参与优惠计算的商品或服务集合

                decimal totalProductAmount = decimal.Zero;//商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
                decimal serviceFee = decimal.Zero;//服务费（指带出的安装服务，不含套餐内安装服务）

                products?.ForEach(_ =>
                {
                    totalProductAmount += _.PackageOrProduct.TotalAmount;
                    calcCouponProducts.Add(new Product()
                    {
                        Pid = _.PackageOrProduct.ProductId,
                        Category = _.PackageOrProduct.MainCategoryCode,
                        Brand = _.PackageOrProduct.Brand,
                        Total = _.PackageOrProduct.Number,
                        UnitPrice = _.PackageOrProduct.Price
                    });
                });

                services?.ForEach(_ =>
                {
                    serviceFee += _.TotalAmount;
                    calcCouponProducts.Add(new Product()
                    {
                        Pid = _.ProductId,
                        Category = _.MainCategoryCode,
                        Brand = _.Brand,
                        Total = _.TotalNumber,
                        UnitPrice = _.Price
                    });
                });

                #endregion

                #region 添加项目

                List<OrderProductDiscountDO> orderProductDiscountDOs = new List<OrderProductDiscountDO>(3);

                var promotionProducts = request?.ProductInfos?.Where(_ => !string.IsNullOrEmpty(_.ActivityId))?.ToList();

                List<GetPromotionActivitByProductCodeListResponse> promotionActivityData = new List<GetPromotionActivitByProductCodeListResponse>();
                if (promotionProducts?.Count() > 0)
                {
                    var promotionActivity = await _productClient.GetPromotionActivitByProductCodeList(new Core.Request.Product.GetPromotionActivitByProductCodeListRequest()
                    {
                        ProductCodeList = promotionProducts?.Select(_ => _.Pid)?.ToList(),
                        PromotionChannel = request?.PromotionChannel
                    });
                    promotionActivityData = promotionActivity?.Data;
                }

                if (shopProducts?.Count() > 0)
                {
                    var shopItems = await GetShopProduct(new GetOrderConfirmPackageProductServicesRequest()
                    {
                        CarId = request.CarId,
                        UserId = request.UserId,
                        ProvinceId = getShopResult?.Data?.ProvinceId,
                        CityId = getShopResult?.Data?.CityId,
                        ProductInfos = shopProducts
                    }, promotionActivityData, request.ProduceType);

                    shopItems?.Products?.ForEach(_ =>
                    {
                        totalProductAmount += _.PackageOrProduct.TotalAmount;
                        calcCouponProducts.Add(new Product()
                        {
                            Pid = _.PackageOrProduct.ProductId,
                            Category = _.PackageOrProduct.MainCategoryCode,
                            Brand = _.PackageOrProduct.Brand,
                            Total = _.PackageOrProduct.Number,
                            UnitPrice = _.PackageOrProduct.Price
                        });
                    });

                }


                #endregion

                decimal totalShouldPayAmount = totalProductAmount + serviceFee;//应付总价

                ShopRegion shopRegion = null;
                if (shop != null)
                {
                    shopRegion = new ShopRegion() { ProvinceId = shop.ProvinceId, CityId = shop.CityId, DistrictId = shop.DistrictId };
                }
                var orderApplicableCouponListReqDTO = new OrderApplicableCouponListReqDTO()
                {
                    UserId = request.UserId,
                    ShopId = request.ShopId,
                    ShopType = shop?.Type ?? 0,
                    ShopRegion = shopRegion,
                    TotalAmount = totalShouldPayAmount,
                    ProductList = calcCouponProducts
                };

                var getOrderApplicableCouponListResult = await _couponClient.GetOrderApplicableCouponList(orderApplicableCouponListReqDTO);
                var orderApplicableCouponEntityResDTO = getOrderApplicableCouponListResult.GetSuccessData();

                if (orderApplicableCouponEntityResDTO != null)
                {
                    if (orderApplicableCouponEntityResDTO.UserCouponId != null && orderApplicableCouponEntityResDTO.UserCouponId.Any())
                    {
                        userTrialOrderCoupons = orderApplicableCouponEntityResDTO.UserCouponId;
                    }
                }
            }

            var getUserCoupons = await _couponClient.GetUserCouponPageByUserId(new GetUserCouponPageByUserIdRequest()
            {
                UserId = request.UserId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            var userItems = getUserCoupons?.Data?.Items?.ToList();
            userItems?.ForEach(_ =>
            {
                if (userTrialOrderCoupons.Exists(item => item == _.UserCouponId))
                {
                    if (_.Status == UserCouponStatus.Unused)
                    {
                        _.OrderEnabled = true;
                    }
                }
            });
            _logger.Info($"getUserCoupons：{JsonConvert.SerializeObject(userItems)} ");

            return new ApiPagedResult<UserCouponVO>()
            {
                Code = ResultCode.Success,
                Data = new ApiPagedResultData<UserCouponVO>()
                {
                    Items = userItems?.OrderByDescending(_ => _.OrderEnabled)?.ToList(),
                    TotalItems = getUserCoupons?.Data?.TotalItems ?? 0
                }
            };

        }

        /// <summary>
        /// 提交套餐卡核销V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> UseBuyPackageOrderV2(ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            #region 基础检验

            var requestData = request.Data;
            var code = requestData.Code;
            if (string.IsNullOrWhiteSpace(code))
                throw new CustomException(CommonConstant.VerticationCodeIsNotNull);
            code = code.ToUpper();
            if (!code.StartsWith(VerticationCodePrexEnum.RGTC.ToString()))
                throw new CustomException(CommonConstant.VerticationCodeFormateIsMistake);
            var codeDO = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = code }, true)).FirstOrDefault();
            if (codeDO == null)
                throw new CustomException(CommonConstant.VerticationCodeIsNull);
            if (codeDO.Status > VerticationCodeStatusEnum.NotUse.ToSbyte())
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            if (!(codeDO.StartValidTime <= DateTime.Now && codeDO.EndValidTime.AddDays(1) > DateTime.Now))
            {
                codeDO.Status = VerticationCodeStatusEnum.Expired.ToSbyte();
                await _orderPackageCardRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            }

            var ruleId = codeDO.RuleId;
            var ruleDO = await _verificationRuleRepository.GetAsync(ruleId);
            if (ruleDO == null)
                throw new CustomException(CommonConstant.VerticationCodeRuleIsNotExist);
            if (ruleDO.IsValid == 0)
            {
                throw new CustomException(CommonConstant.VerticationCodeRuleInvalid);
            }

            var getShopByIdResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
            var shop = getShopByIdResult.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationException);

            var rules = await _verificationRuleRepository.GetRuleForPackageCard(shop?.Type ?? 0, requestData.ShopId, codeDO.RuleId);
            if (rules == null || !rules.Any())
            {
                throw new CustomException(CommonConstant.VerticationCodeNotUse);
            }

            #endregion

            #region 取所有商品详情数据
            var productDetailSearchRequest = new ProductDetailSearchRequest() { ProductCodes = new List<string>() { codeDO.ProductId } };
            var getProductsByProductCodesResult = await _productClient.GetProductsByProductCodes(productDetailSearchRequest);
            //_logger.Info($"UseBuyPackageOrderV2 productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} getProductsByProductCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
            var productDetails = getProductsByProductCodesResult?.GetSuccessData();//商品详情数据
            if (productDetails == null || !productDetails.Any())
            {
                _logger.Warn($"选购的商品：{codeDO.ProductId}未找到");
                throw new CustomException(CommonConstant.ProductInfomationException);
            }

            #endregion

            var orderType = OrderTypeEnum.Other.ToSbyte();
            var productInfo = productDetails.FirstOrDefault().Product;
            var mainCategoryId = productInfo.MainCategoryId.ToString() ;
            var secondCategoryId = productInfo.SecondCategoryId.ToString() ;
            if (mainCategoryId == "1" || secondCategoryId == "162" || secondCategoryId == "16")
            {
                orderType = OrderTypeEnum.Maintenance.ToSbyte();//保养
            }
            else if (mainCategoryId == "6" || mainCategoryId == "289" || secondCategoryId == "307")
            {
                orderType = OrderTypeEnum.Beauty.ToSbyte();//美容
            }
            else if (mainCategoryId == "2" || secondCategoryId == "300")
            {
                orderType = OrderTypeEnum.CarModification.ToSbyte();//维修改装
            }
            else if (mainCategoryId == "3" || mainCategoryId == "4" || secondCategoryId == "5")
            {
                orderType = OrderTypeEnum.Tire.ToSbyte();//轮胎
            }
            else if (mainCategoryId == "227" || secondCategoryId == "337")
            {
                orderType = OrderTypeEnum.SheetMetal.ToSbyte();//钣金喷漆
            }

            var submitOrderRequest = new SubmitOrderRequest()
            {
                CreatedBy = request.Data.CreateBy,
                Remark = request.Data.Remark,
                Channel = ChannelEnum.ApolloErpToShop.ToSbyte(),
                TerminalType = request.Data.TerminalType,
                OrderType = orderType,
                ProduceType = BuyProductTypeEnum.UsePackageCard.ToSbyte(),
                ShopId = request.Data.ShopId,
                UserId = codeDO.UserId,
                CarId = string.Empty,
                PayMethod = PayMethodEnum.None.ToSbyte(),
                PayChannel = PayMethodEnum.None.ToSbyte(),
                DeliveryType = 1,
                InstallType = 1,
                OtherCouponAmount = (codeDO.Price - codeDO.AvgPrice) * codeDO.Num,
                ProductInfos = new List<SelectedProductInfoDTO>()
                {
                    new SelectedProductInfoDTO()
                    {
                        Number=codeDO.Num,
                        Pid=codeDO.ProductId,
                        Price = codeDO.Price,
                        ProductOwnAttri = 0
                    }
                }
            };

            //result = Result.Success(new SubmitUseVerificationCodeOrderResponse() { OrderNo = orderNo }, "下单成功");

            var tempRequest = new ApiRequest<SubmitOrderRequest>();
            tempRequest.Data = submitOrderRequest;
            tempRequest.Channel = request.Channel;
            tempRequest.ApiVersion = request.ApiVersion;
            tempRequest.DeviceId = request.DeviceId;
            var result = await SubmitOrder(tempRequest);

            var newOrder = result.GetSuccessData();
            if (newOrder != null)
            {
                #region  更新核销码使用状态和单号
                codeDO.Status = 1;
                codeDO.VerifyOrderNo = newOrder.OrderNo;
                codeDO.VerifyShopId = requestData.ShopId;
                codeDO.VerifyTime = DateTime.Now;
                codeDO.UpdateBy = requestData.CreateBy;
                codeDO.UpdateTime = DateTime.Now;

                await _orderPackageCardRepository.UpdateAsync(codeDO,
                    new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "VerifyTime", "UpdateBy", "UpdateTime" });
                #endregion

                #region  关联最近一次到店
                var GetLastArrivalResult = await _receiveClient.GetLastArrival(new GetLastArrivalRequest()
                {
                    ShopId = requestData.ShopId,
                    Telephone = codeDO.UserPhone,
                    IsNoFinish = true
                });
                var lastArrival = GetLastArrivalResult.GetSuccessData();
                if (lastArrival != null)
                {
                    long.TryParse(lastArrival.ArrivalId, out var lastArrivalId);
                    if (lastArrivalId > 0)
                    {
                        await _receiveClient.ArrivalRelateOrder(new ArrivalRelateOrderRequest()
                        {
                            ArrivalId = lastArrivalId,
                            OrderNo = newOrder.OrderNo,
                            UserId = codeDO.UserId,
                            CreateBy = requestData.CreateBy
                        });
                    }
                }
                #endregion

            }

            return _mapper.Map<ApiResult<SubmitUseVerificationCodeOrderResponse>>(result);
        }

        /// <summary>
        /// 提交套餐卡核销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> UseBuyPackageOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            var result = Result.Failed<SubmitUseVerificationCodeOrderResponse>("下单失败");

            #region 基础检验
            var requestData = request.Data;
            var code = requestData.Code;
            if (string.IsNullOrWhiteSpace(code))
                throw new CustomException(CommonConstant.VerticationCodeIsNotNull);
            code = code.ToUpper();
            if (!code.StartsWith(VerticationCodePrexEnum.RGTC.ToString()))
                throw new CustomException(CommonConstant.VerticationCodeFormateIsMistake);
            var codeDO = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = code }, true)).FirstOrDefault();
            if (codeDO == null)
                throw new CustomException(CommonConstant.VerticationCodeIsNull);
            if (codeDO.Status > VerticationCodeStatusEnum.NotUse.ToSbyte())
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            if (!(codeDO.StartValidTime <= DateTime.Now && codeDO.EndValidTime.AddDays(1) > DateTime.Now))
            {
                codeDO.Status = VerticationCodeStatusEnum.Expired.ToSbyte();
                await _orderPackageCardRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                throw new CustomException(CommonConstant.VerticationCodeIsExpried);
            }

            var ruleId = codeDO.RuleId;
            var ruleDO = await _verificationRuleRepository.GetAsync(ruleId);
            if (ruleDO == null)
                throw new CustomException(CommonConstant.VerticationCodeRuleIsNotExist);
            if (ruleDO.IsValid == 0)
            {
                throw new CustomException(CommonConstant.VerticationCodeRuleInvalid);
            }

            var getShopByIdResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
            var shop = getShopByIdResult.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationException);

            var rules = await _verificationRuleRepository.GetRuleForPackageCard(shop?.Type ?? 0, requestData.ShopId, codeDO.RuleId);
            if (rules == null || !rules.Any())
            {
                throw new CustomException(CommonConstant.VerticationCodeNotUse);
            }

            var products = new List<OrderConfirmPackageProductDTO>();
            var services = new List<OrderConfirmProductDTO>();

            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(new GetOrderConfirmPackageProductServicesRequest()
            {
                CityId = shop.CityId,
                ProvinceId = shop.ProvinceId,
                CarId = string.Empty,
                ProductInfos = new List<SelectedProductInfoDTO>()
                    {
                        new SelectedProductInfoDTO()
                        {
                            Number=codeDO.Num,
                            Pid=codeDO.ProductId
                        }
                    }
            });

            var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

            products = getPackageProductServicesResponse.Products;
            services = getPackageProductServicesResponse.Services;
            if ((products == null || !products.Any()) && (services == null || !services.Any()))
            {
                throw new CustomException("商品或服务信息异常");
            }

            #region 获取全部服务费集合
            var allServiceProductIds = new List<string>();
            foreach (var item in products)
            {
                if (item.PackageOrProduct.ProductAttribute == 2)
                {
                    allServiceProductIds.Add(item.PackageOrProduct.ProductId);
                }
                if (item.PackageProducts != null && item.PackageProducts.Any())
                {
                    item.PackageProducts.ForEach(_ =>
                    {
                        if (_.ProductAttribute == 2)
                        {
                            allServiceProductIds.Add(_.ProductId);
                        }
                    });
                }
            }
            foreach (var item in services)
            {
                allServiceProductIds.Add(item.ProductId);
            }
            var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
            {
                ShopId = requestData.ShopId,
                ProductIds = allServiceProductIds.ToList()
            });
            var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
            if (serviceCosts != null)
            {
                var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false);//服务上下架校验
                if (findOfflineServices != null && findOfflineServices.Any())
                {
                    //2023-3-28 暂时不检查服务上下架
                    //throw new CustomException("所选门店暂不支持此服务，请重新选择服务门店");
                }
            }
            #endregion


            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = codeDO.UserId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
            {
                throw new CustomException(CommonConstant.UserInformationIsNull);
            }

            #endregion

            #region 服务产品计算成本
            //2023-3-28 不计算
            //serviceCosts?.ForEach(_ =>
            //{
            //    if (_.CostPrice > 0)
            //    {
            //        var product = products?.SelectMany(item => item.PackageProducts)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
            //        if (product != null)
            //        {
            //            product.TotalCostPrice = _.CostPrice * product.TotalNumber;
            //        }
            //        var packageProduct = products?.Select(item => item.PackageOrProduct)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
            //        if (packageProduct != null)
            //        {
            //            packageProduct.TotalCostPrice = _.CostPrice * packageProduct.TotalNumber;
            //        }
            //        var service = services?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
            //        if (service != null)
            //        {
            //            service.TotalCostPrice = _.CostPrice * service.TotalNumber;
            //        }
            //    }
            //});

            #endregion

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号
            var createTime = DateTime.Now;//创建时间

            var createBy = string.Empty;
            var telephone = string.Empty;

            var userName = string.Empty;

            if (!string.IsNullOrWhiteSpace(getUserInfoResult?.Data?.UserName))
            {
                userName = getUserInfoResult?.Data?.UserName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(getUserInfoResult?.Data?.NickName))
                {
                    userName = getUserInfoResult?.Data?.NickName;
                }
                else
                {
                    userName = getUserInfoResult?.Data?.UserTel;
                }
            }

            var contactName = userName;
            var contactPhone = getUserInfoResult?.Data?.UserTel;

            createBy = userName;

            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据
            var orderType = OrderTypeEnum.Other.ToSbyte();
            var mainCategoryId = products?.Select(item => item.PackageOrProduct)?.FirstOrDefault()?.MainCategoryCode ?? "";
            var secondCategoryId = products?.Select(item => item.PackageOrProduct)?.FirstOrDefault()?.SecondCategoryCode ?? "";
            if (mainCategoryId == "1" || secondCategoryId == "162" || secondCategoryId == "16")
            {
                orderType = OrderTypeEnum.Maintenance.ToSbyte();//保养
            }
            else if (mainCategoryId == "6" || mainCategoryId == "289" || secondCategoryId == "307")
            {
                orderType = OrderTypeEnum.Beauty.ToSbyte();//美容
            }
            else if (mainCategoryId == "2" || secondCategoryId == "300")
            {
                orderType = OrderTypeEnum.CarModification.ToSbyte();//改装
            }
            else if (mainCategoryId == "3" || mainCategoryId == "4" || secondCategoryId == "5")
            {
                orderType = OrderTypeEnum.Tire.ToSbyte();//轮胎
            }
            else if (mainCategoryId == "227" || secondCategoryId == "337")
            {
                orderType = OrderTypeEnum.SheetMetal.ToSbyte();//维修
            }

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = ChannelEnum.ApolloErpToShop.ToSbyte(),
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                //OrderType = OrderTypeEnum.Other.ToSbyte(),
                OrderType = orderType,
                ProduceType = BuyProductTypeEnum.UsePackageCard.ToSbyte(),
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = codeDO.UserId,
                UserName = userName,
                UserPhone = getUserInfoResult?.Data?.UserTel ?? string.Empty,
                ContactId = codeDO.UserId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = 0,
                PayMethod = PayMethodEnum.None.ToSbyte(),
                PayChannel = PayMethodEnum.None.ToSbyte(),
                PayTime = DateTime.Now,
                IsNeedInvoice = 0,
                IsNeedDelivery = 0,
                DeliveryType = 1,
                DeliveryMethod = 0,
                DeliveryStatus = 0,
                IsNeedInstall = 1,
                CompanyId = shop.CompanyId,
                ShopId = request.Data.ShopId,
                CreateBy = request.Data.CreateBy,
                CreateTime = createTime,
                SignStatus = 0,
                SignTime = DateTime.Now,
                Remark = string.Empty,
                InstallType = 1,
            };

            #endregion

            #region 组装用户

            var orderUserDO = _mapper.Map<OrderUserDO>(user);
            orderUserDO.UserId = codeDO.UserId;

            #endregion

            #region 组装地址
            OrderAddressDO orderAddressDO = null;

            orderAddressDO = _mapper.Map<OrderAddressDO>(shop);
            orderAddressDO.AddressType = 1;
            orderAddressDO.ShopId = request.Data.ShopId;
            orderAddressDO.DetailAddress = shop.Address;

            if (orderAddressDO != null)
            {
                orderAddressDO.ReceiverName = contactPhone;
                orderAddressDO.ReceiverPhone = contactPhone;
                orderAddressDO.CreateBy = createBy;
                orderAddressDO.CreateTime = DateTime.Now;
            }
            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);



                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                //存储单独带出的服务
                var serviceProductDOs = new List<OrderProductDO>();
                foreach (var item in services)
                {
                    var serveForProductCodes = item.ServeForProductCodes.TrimEnd(';').Split(';');

                    var serveForProducts = allInsertedProducts.FindAll(_ => serveForProductCodes.Contains(_.ProductId));
                    var serveForOrderPids = new StringBuilder();
                    if (serveForProducts != null && serveForProducts.Any())
                    {
                        foreach (var serveForProduct in serveForProducts)
                        {
                            serveForOrderPids.Append($"{serveForProduct.Id};");
                        }
                    }
                    item.ServeForOrderPids = serveForOrderPids.ToString().TrimEnd(';');
                    var serviceProductDO = _mapper.Map<OrderProductDO>(item);
                    serviceProductDO.OrderId = orderId;
                    serviceProductDO.OrderNo = orderNo;
                    serviceProductDO.CreateBy = createBy;
                    serviceProductDO.CreateTime = createTime;
                }
                if (serviceProductDOs.Any())
                {
                    await _orderProductRepository.InsertBatchAsync(serviceProductDOs);
                    allInsertedProducts.Union(serviceProductDOs);
                }

                #endregion

                //存地址
                if (orderAddressDO != null)
                {
                    orderAddressDO.OrderId = orderId;
                    await _orderAddressRepository.InsertAsync(orderAddressDO);
                }

                #region  更新核销码使用状态和单号
                codeDO.Status = 1;
                codeDO.VerifyOrderNo = orderNo;
                codeDO.VerifyShopId = requestData.ShopId;
                codeDO.VerifyTime = DateTime.Now;
                codeDO.UpdateBy = requestData.ShopId.ToString();
                codeDO.UpdateTime = DateTime.Now;

                await _orderPackageCardRepository.UpdateAsync(codeDO,
                    new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "VerifyTime", "UpdateBy", "UpdateTime" });
                #endregion

                ts.Complete();
            }

            #endregion

            #region 更新签收
            var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;


            products?.ForEach(item =>
            {
                if (item.PackageProducts != null)
                {
                    releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
                }
            });

            //txw20221206 简化库存取消批次和占库
            await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);

            //if (releaProductCount == 0)
            //{
            //    await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);
            //}
            //else
            //{
            //    #region 通知占库

            //    _shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
            //    {
            //        QueueNo = orderNo,
            //        CreateBy = createBy,
            //        QueueStatus = "OrderService"
            //    });

            //    #endregion
            //}

            #endregion

            result = Result.Success(new SubmitUseVerificationCodeOrderResponse() { OrderNo = orderNo }, "下单成功");

            return result;
        }

        /// <summary>
        /// 保存规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request)
        {
            DateTime earliestTime = DateTime.Now.Date;
            DateTime latestTime = DateTime.Now.Date;
            if (!string.IsNullOrEmpty(request.Data.EarliestUseDate))
            {
                var earliestRes = DateTime.TryParse(request.Data.EarliestUseDate, out earliestTime);
                if (!earliestRes)
                    return Result.Failed("最早使用日期格式不对");
            }
            if (!string.IsNullOrEmpty(request.Data.LatestUseDate))
            {
                var latestTimeRes = DateTime.TryParse(request.Data.LatestUseDate, out latestTime);
                if (!latestTimeRes)
                    return Result.Failed("最晚使用日期格式不对");
            }

            switch (request.Data.ValidStartType)
            {
                case 1:
                    earliestTime = DateTime.Now.Date;
                    break;
                default: break;
            }
            switch (request.Data.ValidEndType)
            {
                case 1:
                    latestTime = earliestTime.AddDays(request.Data.ValidDays);
                    break;
                default: break;
            }

            List<VerificationRuleShopIdDO> verificationRuleShopIdDOs = new List<VerificationRuleShopIdDO>();

            if (request.Data.Id > 0)
            {
                var rule = (await _verificationRuleRepository.GetListAsync(" where is_deleted=0 and id=@Id", new { Id = request.Data.Id }, true)).FirstOrDefault();
                rule.Name = request.Data.Name;
                rule.ShopType = request.Data.ShopType;
                rule.IsValid = (sbyte)request.Data.IsValid;
                rule.IsByShopType = (sbyte)request.Data.IsByShopType;
                rule.ShopType = (sbyte)request.Data.ShopType;
                rule.IsByShopId = (sbyte)request.Data.IsByShopId;
                rule.UseRuleDesc = request.Data.UseRuleDesc;
                rule.ValidStartType = (sbyte)request.Data.ValidStartType;
                rule.ValidEndType = (sbyte)request.Data.ValidEndType;
                rule.ValidDays = request.Data.ValidDays;
                rule.EarliestUseDate = earliestTime;
                rule.LatestUseDate = latestTime;
                rule.UpdateBy = request.Data.UpdateBy;
                rule.UpdateTime = DateTime.Now;
                await _verificationRuleRepository.UpdateAsync(rule, new List<string>{ "Name","IsValid","IsByShopType",
                   "ShopType","IsByShopId","UseRuleDesc","ValidStartType", "ValidEndType","ValidDays","EarliestUseDate","LatestUseDate","UpdateBy","UpdateTime" });

                if (request.Data.IsByShopType == 1)
                {
                    await _verificationRuleShopIdRepository.DeleteShopIds((int)rule.Id, request.Data.UpdateBy);
                }

                if (request.Data?.ShopIds?.Count > 0)
                {
                    await _verificationRuleShopIdRepository.DeleteShopIds((int)rule.Id, request.Data.UpdateBy);

                    request.Data?.ShopIds?.Distinct()?.ToList()?.ForEach(_ =>
                    {
                        verificationRuleShopIdDOs.Add(new VerificationRuleShopIdDO()
                        {
                            CreateBy = request.Data.UpdateBy,
                            RuleId = request.Data.Id,
                            CreateTime = DateTime.Now,
                            IsDeleted = 0,
                            ShopId = _
                        });
                    });
                }

            }
            else
            {
                request.Data.UpdateBy = request.Data.UpdateBy;
                request.Data.UpdateTime = DateTime.Now.ToString();
                VerificationRuleDO verificationRuleDO = _mapper.Map<VerificationRuleDO>(request.Data);
                verificationRuleDO.CreateBy = request.Data.UpdateBy;
                verificationRuleDO.CreateTime = DateTime.Now;
                verificationRuleDO.EarliestUseDate = earliestTime;
                verificationRuleDO.LatestUseDate = latestTime;
                verificationRuleDO.IsByPid = 1;
                var id = await _verificationRuleRepository.InsertAsync(verificationRuleDO);
                request.Data?.ShopIds?.ForEach(_ =>
                {
                    verificationRuleShopIdDOs.Add(new VerificationRuleShopIdDO()
                    {
                        CreateBy = request.Data.UpdateBy,
                        RuleId = id,
                        CreateTime = DateTime.Now,
                        IsDeleted = 0,
                        ShopId = _,

                    });
                });

            }

            if (verificationRuleShopIdDOs?.Count() > 0)
                await _verificationRuleShopIdRepository.InsertBatchAsync(verificationRuleShopIdDOs);

            return Result.Success("操作成功");
        }

        /// <summary>
        /// 保存核销码规则产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(SaveBeautiyOrPackageCardVerificationProductRequest request)
        {
            await _verificationRulePidRepository.DeleteRulePIds(request.RuleId, request.UpdateBy);

            var verificationRulePidDO = _mapper.Map<VerificationRulePidDO>(request);

            List<VerificationRulePidDO> pids = new List<VerificationRulePidDO>();

            var distinctPids = request.Pids?.Distinct()?.ToList();
            distinctPids?.ForEach(_ =>
            {
                pids.Add(new VerificationRulePidDO
                {
                    RuleId = request.RuleId,
                    Pid = _,
                    CreateBy = request.UpdateBy,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.UpdateBy,
                    UpdateTime = DateTime.Now

                });
            });
            if (distinctPids?.Count() > 0)
            {
                var chkPidExistList = await _verificationRulePidRepository.GetListAsync(" where is_deleted=0 and pid In @Pids", new { Pids = distinctPids }, true);

                if (chkPidExistList?.Count() > 0)
                    Result.Failed("此产品中已经配置上规则，请添加其他产品配置规则");
            }

            if (pids?.Count() > 0)
                await _verificationRulePidRepository.InsertBatchAsync(pids);
            return Result.Success("操作成功");
        }

        /// <summary>
        /// 快速下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitQuickOrder(ApiRequest<SubmitOrderRequest> request)
        {

            #region 校验门店用户

            long shopId = request.Data.ShopId;

            var getUserInfoResult = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.UserId
            });

            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = shopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);

            #endregion

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
            getPackageProductServicesRequest.CityId = shop.CityId;


            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();//自营产品


            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            var services = new List<OrderConfirmProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
                services = getPackageProductServicesResponse.Services;
            }

            #endregion

            #region 验证服务是否在门店上架
            var allServiceProductIds = new List<string>();
            foreach (var item in products)
            {
                if (item.PackageOrProduct.ProductAttribute == 2)
                {
                    allServiceProductIds.Add(item.PackageOrProduct.ProductId);
                }
                if (item.PackageProducts != null && item.PackageProducts.Any())
                {
                    item.PackageProducts.ForEach(_ =>
                    {
                        if (_.ProductAttribute == 2)
                        {
                            allServiceProductIds.Add(_.ProductId);
                        }
                    });
                }
            }
            foreach (var item in services)
            {
                allServiceProductIds.Add(item.ProductId);
            }
            var getShopServiceListWithPIDResult = await _shopClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
            {
                ShopId = shopId,
                ProductIds = allServiceProductIds.ToList()
            });
            var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
            if (serviceCosts != null)
            {
                var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false);//服务上下架校验
                if (findOfflineServices != null && findOfflineServices.Any())
                {
                    throw new CustomException(CommonConstant.ServiceShopNotSupport);
                }
            }
            #endregion

            #region 计算成本
            serviceCosts?.ForEach(_ =>
            {
                if (_.CostPrice > 0)
                {
                    var product = products?.SelectMany(item => item.PackageProducts)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (product != null)
                    {
                        product.TotalCostPrice = _.CostPrice * product.TotalNumber;
                    }
                    var packageProduct = products?.Select(item => item.PackageOrProduct)?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (packageProduct != null)
                    {
                        packageProduct.TotalCostPrice = _.CostPrice * packageProduct.TotalNumber;
                    }
                    var service = services?.Where(item => item.ProductId == _.PID)?.FirstOrDefault();
                    if (service != null)
                    {
                        service.TotalCostPrice = _.CostPrice * service.TotalNumber;
                    }
                }
            });

            #endregion


            #region 团购产品修改价格
            var getShopGroupProducts = await _shopClient.GetShopGrouponProduct(new GetShopGrouponProductRequest()
            {
                ShopId = shopId,
                Status = 1
            });

            products?.ForEach(_ =>
            {
                var getShopGroupProduct = getShopGroupProducts?.Data?.Where(t => t.ServiceId == _.PackageOrProduct.ProductId)?.FirstOrDefault();
                if (getShopGroupProduct != null)
                {
                    _.PackageOrProduct.Price = getShopGroupProduct.Price;
                    _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * getShopGroupProduct.Price;
                }
            });

            #endregion


            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号
            var createBy = string.IsNullOrWhiteSpace(request.Data.CreatedBy) ? string.Empty : request.Data.CreatedBy;
            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? string.Empty : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? string.Empty : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = OrderTypeEnum.Beauty.ToSbyte(),
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = contactName,
                UserPhone = contactPhone,
                ContactId = requestData.UserId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = shop.CompanyId,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = (selfProducts.Count() == 0) ? (sbyte)1 : (sbyte)0,
                SignTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户车型

            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.UserId,
                NickName = user.Name,
                UserName = user.Name,
                UserTel = user.Mobile,
                UserTelDes = user.Mobile,
                CreateBy = createBy,
                CreateTime = DateTime.Now
            };
            OrderCarDO orderCarDO = null;
            if (!string.IsNullOrEmpty(request.Data.CarNumber))
            {
                orderCarDO = new OrderCarDO()
                {
                    CarNumber = request.Data.CarNumber
                };
            }

            #endregion 

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                // 存车辆
                if (orderCarDO != null)
                {
                    orderCarDO.OrderId = orderId;
                    orderCarDO.CreateBy = createBy;
                    orderCarDO.CreateTime = createTime;
                    await _orderCarRepository.InsertAsync(orderCarDO);
                }


                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                //存储单独带出的服务
                var serviceProductDOs = new List<OrderProductDO>();
                foreach (var item in services)
                {
                    var serveForProductCodes = item.ServeForProductCodes.TrimEnd(';').Split(';');

                    var serveForProducts = allInsertedProducts.FindAll(_ => serveForProductCodes.Contains(_.ProductId));
                    var serveForOrderPids = new StringBuilder();
                    if (serveForProducts != null && serveForProducts.Any())
                    {
                        foreach (var serveForProduct in serveForProducts)
                        {
                            serveForOrderPids.Append($"{serveForProduct.Id};");
                        }
                    }
                    item.ServeForOrderPids = serveForOrderPids.ToString().TrimEnd(';');
                    var serviceProductDO = _mapper.Map<OrderProductDO>(item);
                    serviceProductDO.OrderId = orderId;
                    serviceProductDO.OrderNo = orderNo;
                    serviceProductDO.CreateBy = createBy;
                    serviceProductDO.CreateTime = createTime;
                }
                if (serviceProductDOs.Any())
                {
                    await _orderProductRepository.InsertBatchAsync(serviceProductDOs);
                    allInsertedProducts.Union(serviceProductDOs);
                }

                #endregion

                ts.Complete();
            }

            #endregion

            //创建下单人记录
            await _orderSaleRepository.InsertAsync(new OrderSaleDO()
            {
                OrderNo = orderNo,
                ShopId = request.Data.ShopId,
                TechId = request.Data.DelegateUserId,
                TechName = request.Data.DelegateUserName,
                Percent = 1,
                CreateBy = createBy,
                CreateTime = createTime
            });

            #region 更新签收
            var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
            products?.ForEach(item =>
            {
                if (item.PackageProducts != null)
                {
                    releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
                }
            });

            //txw20221206 简化库存取消批次和占库
            await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);

            //if (releaProductCount == 0)
            //{
            //    await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);
            //}
            //else
            //{
            //    #region 通知占库
            //    _shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
            //    {
            //        QueueNo = orderNo,
            //        CreateBy = createBy,
            //        QueueStatus = "OrderService"
            //    });

            //    #endregion
            //}

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 客户向小仓下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitCustomerToSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 校验门店用户

            long shopId = request.Data.ShopId;

            var getOwnUserInfo = await _userClient.GetUserInfoByUserTel(new GetUserInfoByUserTelRequest()
            {
                UserTel = request.Data.ReceiverPhone
            });

            var userId = getOwnUserInfo?.Data?.UserId;
            if (getOwnUserInfo?.Data == null)
            {
                var createUserResponse = await _userClient.CreateUser(new CreateUserRequest()
                {
                    UserTel = request.Data.ReceiverPhone,
                    UserName = request.Data.ReceiverName,
                    NickName = request.Data.ReceiverName,
                    Address = string.Empty,
                    SubmitBy = request.Data.CreatedBy
                });
                userId = createUserResponse?.Data;
            }

            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = userId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
                throw new CustomException(CommonConstant.UserInformationIsNull);

            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = shopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);

            #endregion

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
            getPackageProductServicesRequest.CityId = shop.CityId;


            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();//自营产品

            var shopPurchaseProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 2)?.ToList();//外采产品

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            var services = new List<OrderConfirmProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
                services = null;
            }

            #endregion


            #region 外采产品



            getPackageProductServicesRequest.ProductInfos = shopPurchaseProducts;

            if (shopPurchaseProducts?.Count() > 0)
            {
                var shopItems = await GetShopPurchaseProduct(getPackageProductServicesRequest);
                if (shopItems?.Products?.Count() > 0)
                {
                    products.AddRange(shopItems?.Products);
                }
            }

            #endregion

            #region 金额计算
            int num = 0;
            decimal actualAmount = 0;
            request.Data.ProductInfos?.ForEach(_ =>
            {
                num = _.Number;
                actualAmount += num * _.Price;
            });

            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号

            var createBy = string.Empty;
            var telephone = string.Empty;

            createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? string.Empty : request.Data.ReceiverName;
            telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? string.Empty : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = createBy,
                UserPhone = telephone,
                ContactId = requestData.UserId,
                ContactName = createBy,
                ContactPhone = telephone,
                TotalProductNum = num,
                TotalProductAmount = actualAmount,
                ServiceNum = 0,
                ServiceFee = 0,
                DeliveryFee = 0,
                TotalCouponAmount = 0,
                ActualAmount = actualAmount,
                PayMethod = request.Data.PayMethod,
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = 0,
                ShopId = shopId,
                CreateBy = request.Data.CreatedBy,
                CreateTime = createTime,
                SignStatus = 0,
                SignTime = DateTime.Now,
                DispatchStatus = (selfProducts.Count() == 0) ? (sbyte)1 : (sbyte)0,
                DispatchTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = userId,
                NickName = createBy,
                UserName = createBy,
                UserTel = telephone,
                UserTelDes = telephone,
                CreateBy = createBy,
                CreateTime = DateTime.Now
            };
            OrderCarDO orderCar = null;
            if (!string.IsNullOrEmpty(request.Data.CarNumber))
            {
                orderCar = new OrderCarDO()
                {
                    CarNumber = request.Data.CarNumber
                };

            }

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                if (orderCar != null)
                {
                    orderCar.OrderId = orderId;
                    await _orderCarRepository.InsertAsync(orderCar);
                }

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var product = request.Data?.ProductInfos?.Where(_ => _.Pid == item.PackageOrProduct.ProductId)?.FirstOrDefault();
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    packageOrProductDO.Price = product?.Price ?? 0;
                    packageOrProductDO.Amount = product?.Price ?? 0;
                    packageOrProductDO.TotalAmount = (product?.Price ?? 0) * product.Number;
                    packageOrProductDO.ActualPayAmount = (product?.Price ?? 0) * product.Number;
                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);
                    allInsertedProducts.Add(packageOrProductDO);
                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }
                #endregion

                ts.Complete();
            }

            #endregion

            #region 更新签收
            var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
            products?.ForEach(item =>
            {
                if (item.PackageProducts != null)
                {
                    releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
                }
            });

            //txw20221206 简化库存取消批次和占库
            await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);

            //if (releaProductCount == 0)
            //{
            //    await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);
            //}
            //else
            //{
            //    #region 通知占库
            //    _shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
            //    {
            //        QueueNo = orderNo,
            //        CreateBy = createBy,
            //        QueueStatus = "OrderService"
            //    });

            //    #endregion
            //}

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        /// <summary>
        /// 门店向小仓下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitShopToSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 校验门店用户

            long shopId = request.Data.ShopId;

            var getDelegateShop = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.DelegateUserShopId });

            var getDelegeateShopInfo = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.ShopId });

            var getDelegateShopData = getDelegeateShopInfo.GetSuccessData();
            if (getDelegateShopData == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);
            if (request.Data.DelegateUserShopId <= 0)
                throw new CustomException(CommonConstant.SmallWarehouseShopIsNotNull);

            #endregion

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            getPackageProductServicesRequest.ProvinceId = getDelegateShop?.Data?.ProvinceId;
            getPackageProductServicesRequest.CityId = getDelegateShop.Data?.CityId;

            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();//自营产品
            var shopPurchaseProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 2)?.ToList();//外采产品

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            var services = new List<OrderConfirmProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
            }

            #endregion

            #region 外采产品

            getPackageProductServicesRequest.ProductInfos = shopPurchaseProducts;

            if (shopPurchaseProducts?.Count() > 0)
            {
                var shopItems = await GetShopPurchaseProduct(getPackageProductServicesRequest);
                if (shopItems?.Products?.Count() > 0)
                {
                    products.AddRange(shopItems?.Products);
                }
            }

            #endregion

            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                if (item.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())
                {
                    totalProductNum += item.PackageOrProduct.TotalNumber;
                    totalProductAmount += item.PackageOrProduct.SettlementPrice * item.PackageOrProduct.TotalNumber;
                }
                if (item.PackageOrProduct.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte())
                {
                    totalProductNum += item.PackageOrProduct.TotalNumber;
                    totalProductAmount += item.PackageOrProduct.Price * item.PackageOrProduct.TotalNumber;
                }

            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号
            var createBy = string.IsNullOrWhiteSpace(request.Data.CreatedBy) ? string.Empty : request.Data.CreatedBy;

            var employee = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.DelegateUserId
            });

            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? request.Data.DelegateUserName : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? employee?.Data?.Mobile ?? string.Empty : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间



            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.DelegateUserId,
                NickName = getDelegateShop?.Data?.SimpleName ?? string.Empty,
                UserName = getDelegateShop?.Data?.SimpleName ?? string.Empty,
                UserTel = getDelegateShop?.Data?.OwnerPhone ?? string.Empty,
                UserTelDes = getDelegateShop?.Data?.OwnerPhone ?? string.Empty,
                CreateBy = createBy,
                CreateTime = DateTime.Now
            };


            #endregion


            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = request.Data.DelegateUserId,
                UserName = getDelegateShop?.Data?.SimpleName ?? string.Empty,
                UserPhone = getDelegateShop?.Data?.OwnerPhone ?? string.Empty,
                ContactId = requestData.UserId,
                ContactName = getDelegateShop?.Data?.SimpleName ?? string.Empty,
                ContactPhone = getDelegateShop?.Data?.OwnerPhone ?? string.Empty,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = PayMethodEnum.Month.ToSbyte(),
                PayChannel = request.Data.PayChannel,
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 1,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = getDelegateShop?.Data?.CompanyId ?? 0,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = (selfProducts.Count() == 0) ? (sbyte)1 : (sbyte)0,
                SignTime = DateTime.Now,
                Remark = request.Data.Remark ?? string.Empty,
                InstallType = request.Data.InstallType
            };

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    if (item.PackageOrProduct.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte())
                    {
                        packageOrProductDO.TotalAmount = item.PackageOrProduct.TotalNumber * item.PackageOrProduct.Price;
                        packageOrProductDO.Amount = item.PackageOrProduct.Price;
                        packageOrProductDO.Price = item.PackageOrProduct.Price;
                        packageOrProductDO.ActualPayAmount = item.PackageOrProduct.TotalNumber * item.PackageOrProduct.Price;
                    }
                    if (item.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())
                    {
                        packageOrProductDO.TotalAmount = item.PackageOrProduct.TotalNumber * item.PackageOrProduct.SettlementPrice;
                        packageOrProductDO.Amount = item.PackageOrProduct.SettlementPrice;
                        packageOrProductDO.Price = item.PackageOrProduct.SettlementPrice;
                        packageOrProductDO.ActualPayAmount = item.PackageOrProduct.TotalNumber * item.PackageOrProduct.SettlementPrice;
                    }

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                #endregion

                await _delegateUserOrderRepository.InsertAsync(new DelegateUserOrderDO()
                {
                    OrderNo = orderNo,
                    ShopId = request.Data.DelegateUserShopId,
                    ProductType = BuyProductTypeEnum.ShopToSamllWarehouseOrder.ToSbyte(),
                    CreateBy = request.Data.DelegateUserId,
                    UserId = request.Data.DelegateUserId,
                    UserName = request.Data.DelegateUserName,
                    CreateTime = DateTime.Now
                });


                ts.Complete();
            }

            #endregion

            #region 更新签收
            var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
            products?.ForEach(item =>
            {
                if (item.PackageProducts != null)
                {
                    releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Count() ?? 0;
                }
            });

            //txw20221206 简化库存取消批次和占库
            await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);

            //if (releaProductCount == 0)
            //{
            //    await _orderRepository.UpdateOrderSignStatus(orderNo, createBy);
            //}
            //else
            //{
            //    #region 通知占库
            //    _shopStockClient.OrderOccupyStock(new Core.Request.Stock.OrderOccupyStockRequest()
            //    {
            //        QueueNo = orderNo,
            //        CreateBy = createBy,
            //        QueueStatus = "OrderService"
            //    });

            //    #endregion
            //}

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        public async Task<ApiResult> SaveShopSmallWarehouseOrderStatus(SaveShopSmallWarehouseOrderStatusRequest request)
        {
            var result = Result.Success("处理成功");
            //if (request.OrderNo.StartsWith("RGB"))
            //{
            //    // await _orderRepository.UpdateOrderInstallStatus(orderNo, request.UpdateBy);
            //    _shopStockClient.OrderInstallReduceStock(new Core.Request.Stock.OrderInstallReduceStockRequest
            //    {
            //        QueueNo = request.OrderNo,
            //        CreateBy = request.UpdateBy,
            //        QueueStatus = string.Empty,
            //        SourceType = string.Empty
            //    });
            //}
            //await _orderClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
            //{
            //    OrderNos = new List<string>() { request.OrderNo },
            //    UpdateBy = request.UpdateBy,
            //    UpdateStatusType = UpdateOrderStatusTypeEnum.InstallStatus
            //});

            //await _orderRepository.UpdateOrderCompleteStatus(request.OrderNo, request.UpdateBy);

            //_fmsClient.CalculationReconciliationFee(new CalculationReconciliationFeeRequest()
            //{
            //    OrderNo = request.OrderNo,
            //    CreateBy = request.UpdateBy
            //});

            return result;
        }

        public async Task<ApiResult<SubmitOrderResponse>> SubmitMonthSamllWarehouseOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 校验门店用户

            long shopId = request.Data.ShopId;

            var getDelegateShop = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.DelegateUserShopId });

            var getDelegeateShopInfo = await _shopClient.GetShopById(new GetShopRequest() { ShopId = request.Data.ShopId });

            var getDelegateShopData = getDelegeateShopInfo.GetSuccessData();
            if (getDelegateShopData == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);
            if (request.Data.DelegateUserShopId <= 0)
                throw new CustomException(CommonConstant.SmallWarehouseShopIsNotNull);

            #endregion

            #region 获取自营产品的信息

            var requestData = request.Data;
            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            getPackageProductServicesRequest.ProvinceId = getDelegateShop?.Data?.ProvinceId;
            getPackageProductServicesRequest.CityId = getDelegateShop.Data?.CityId;
            // getPackageProductServicesRequest.ProductInfos
            var productCode = _configuration["MonthReconciliationProduct:ProductCode"];
            getPackageProductServicesRequest.ProductInfos = new List<SelectedProductInfoDTO>()
            {
                new SelectedProductInfoDTO()
                {
                    Pid=productCode,
                    ProductOwnAttri=0,
                }
            };
            var selfProducts = getPackageProductServicesRequest?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();//自营产品

            getPackageProductServicesRequest.ProductInfos = selfProducts;

            var products = new List<OrderConfirmPackageProductDTO>(3);
            var services = new List<OrderConfirmProductDTO>(3);
            if (selfProducts?.Count() > 0)
            {
                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                products = getPackageProductServicesResponse.Products;
            }

            #endregion

            #region 获取订单集合
            if (request?.Data?.OrderNos?.Count() == 0)
                throw new CustomException(CommonConstant.RefOrderIsNotNull);
            var orderInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.Data.OrderNos
            });
            #endregion

            #region 金额计算

            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            var sumAmount = orderInfos?.Data?.Sum(_ => _.ActualAmount) ?? 0;
            totalProductAmount = sumAmount;
            totalProductNum = 1;

            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额

            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位

            if (actualAmount < 0)
                actualAmount = 0;
            #endregion

            #region 组装待存储的数据

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号
            var createBy = string.IsNullOrWhiteSpace(request.Data.CreatedBy) ? string.Empty : request.Data.CreatedBy;

            var employee = await _shopClient.GetEmployeeInfo(new EmployeeInfoRequest()
            {
                EmployeeId = request.Data.DelegateUserId
            });

            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? request.Data.DelegateUserName : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? employee?.Data?.Mobile ?? string.Empty : request.Data.ReceiverPhone;

            var createTime = DateTime.Now;//创建时间

            #region 组装用户

            // var orderUserDO = _mapper.Map<OrderUserDO>(user);
            var orderUserDO = new OrderUserDO()
            {
                UserId = request.Data.DelegateUserId,
                NickName = createBy,
                UserName = createBy,
                UserTel = contactPhone,
                UserTelDes = contactPhone,
                CreateBy = createBy,
                CreateTime = DateTime.Now
            };

            #endregion

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = request.Data.Channel,
                TerminalType = request.Data.TerminalType,
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = request.Data.DelegateUserId,
                UserName = contactName,
                UserPhone = contactPhone,
                ContactId = requestData.UserId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = 0,
                PayChannel = 0,
                IsNeedInvoice = 0,
                IsNeedDelivery = 1,
                DeliveryType = 1,
                DeliveryMethod = 2,
                DeliveryStatus = 1,
                IsNeedInstall = 0,
                CompanyId = getDelegateShop?.Data?.CompanyId ?? 0,
                ShopId = shopId,
                CreateBy = createBy,
                CreateTime = createTime,
                SignStatus = 1,
                SignTime = DateTime.Now,
                Remark = string.Empty,
                InstallType = 3
            };

            #endregion

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                if (orderDO.IsNeedInstall == 1)
                {
                    orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                }
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception(CommonConstant.OrderSubmitFailure);
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存商品

                // 存储套餐或单品（服务）
                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;

                    packageOrProductDO.TotalAmount = actualAmount;
                    packageOrProductDO.Amount = actualAmount;
                    packageOrProductDO.Price = actualAmount;
                    packageOrProductDO.ActualPayAmount = actualAmount;
                    packageOrProductDO.Number = 1;
                    packageOrProductDO.TotalNumber = 1;

                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    allInsertedProducts.Add(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        if (packageDetailDOs != null && packageDetailDOs.Any())
                        {
                            await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                }

                #endregion

                await _delegateUserOrderRepository.InsertAsync(new DelegateUserOrderDO()
                {
                    OrderNo = orderNo,
                    ShopId = request.Data.DelegateUserShopId,
                    ProductType = BuyProductTypeEnum.MonthReconciliationOrder.ToSbyte(),
                    CreateBy = request.Data.DelegateUserId,
                    UserId = request.Data.DelegateUserId,
                    UserName = request.Data.DelegateUserName,
                    CreateTime = DateTime.Now
                });

                await _delegateUserOrderRepository.UpdateOrderRefInfo(request.Data.OrderNos, orderNo, request.Data.DelegateUserName);

                ts.Complete();
            }

            #endregion

            return Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, CommonConstant.OrderSubmitSuccess);
        }

        public async Task<ApiResult<SubmitOrderResponse>> SubmitBuyPackageCard(ApiRequest<SubmitOrderRequest> request)
        {
            var result = Result.Failed<SubmitOrderResponse>("下单失败");

            var requestData = request.Data;

            //if (request.Data.UserCouponId > 0)
            //{
            //    var checkUserCoupon = await _couponClient.CheckUserCouponValidity(new Client.Request.Coupon.CheckUserCouponValidityRequest()
            //    {
            //        UserCouponId = request.Data.UserCouponId,
            //        UserId = request.Data.UserId
            //    });

            //    if ((checkUserCoupon?.Data ?? false) == false)
            //        throw new CustomException(checkUserCoupon?.Message ?? "优惠卷异常");
            //}


            #region 准备外部数据
            var getUserInfoResult = await _userClient.GetUserInfo(new GetUserInfoRequest() { UserId = requestData.UserId });
            var user = getUserInfoResult?.GetSuccessData();
            if (user == null)
            {
                throw new CustomException("用户信息异常");
            }

            var getShopResult = await _shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
            var shop = getShopResult?.GetSuccessData();
            if (shop == null)
                throw new CustomException(CommonConstant.ShopInformationIsNull);

            //ShopDTO shop = null;

            //var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = 4458 });
            //shop = getShopResult?.GetSuccessData();
            //if (shop == null)
            //{
            //    throw new CustomException("门店信息异常");
            //}

            #endregion

            var selfProducts = requestData?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();

            var products = new List<OrderConfirmPackageProductDTO>();
            var services = new List<OrderConfirmProductDTO>();

            var getPackageProductServicesRequest = _mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

            #region 根据选择的Pid集合获取套餐、单品集合和服务集合

            if (selfProducts?.Count > 0)
            {
                getPackageProductServicesRequest.ProductInfos = selfProducts;

                var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

                products = getPackageProductServicesResponse.Products;
                services = getPackageProductServicesResponse.Services;
                if ((products == null || !products.Any()) && (services == null || !services.Any()))
                {
                    throw new CustomException("商品或服务信息异常");
                }
            }

            #endregion

            var verificationOrderProduct = products?.FirstOrDefault();
            var rules = await _verificationRuleRepository.GetRuleForPackage(verificationOrderProduct.PackageOrProduct.ProductId);
            if (rules == null || !rules.Any())
            {
                throw new CustomException("套餐卡购规则没有配置");

            }
            if (products.Count != 1 || products?.Select(_ => _.PackageOrProduct)?.FirstOrDefault()?.TotalNumber != 1)
            {
                throw new CustomException("套餐卡单次仅可购买一份");
            }

            var rule = rules.FirstOrDefault();
            //有效期计算
            var startValidTime = new DateTime(1900, 1, 1);
            var endValidTime = new DateTime(1900, 1, 1);
            switch (rule.ValidStartType)
            {
                case 1:
                    startValidTime = DateTime.Today;
                    switch (rule.ValidEndType)
                    {
                        case 1:
                            endValidTime = startValidTime.AddDays(rule.ValidDays);
                            break;
                        case 2:
                            endValidTime = rule.LatestUseDate;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    startValidTime = rule.EarliestUseDate;
                    switch (rule.ValidEndType)
                    {
                        case 1:
                            endValidTime = startValidTime.AddDays(rule.ValidDays);
                            break;
                        case 2:
                            endValidTime = rule.LatestUseDate;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (endValidTime < DateTime.Today)
            {
                throw new CustomException("套餐卡配置的结束日期信息小于今天");
            }

            #region 金额计算
            int totalProductNum = 0;//商品总数（单品或套餐，不含单服务和套餐明细数量）
            decimal totalProductAmount = decimal.Zero;//商品总价（单品或套餐，不含单服务）
            int serviceNum = 0;//服务数量（单服务）
            decimal serviceFee = decimal.Zero;//服务费（单服务）
            decimal deliveryFee = decimal.Zero;//运费

            foreach (var item in products)
            {
                totalProductNum += item.PackageOrProduct.TotalNumber;
                totalProductAmount += item.PackageOrProduct.TotalAmount;
            }
            foreach (var item in services)
            {
                serviceNum += item.TotalNumber;
                serviceFee += item.TotalAmount;
            }
            decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
            decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

            decimal totalCouponAmount = decimal.Zero;//总优惠金额
            var userCouponId = requestData.UserCouponId;//用户选择的优惠券ID

            UserCouponEntityCustomResponse userCoupon = null;//用户优惠券信息
            if (userCouponId > 0)
            {
                //获取优惠券具体信息
                var getUserCouponEntityCustomByIdResult = await _couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdRequest() { UserCouponId = userCouponId });
                userCoupon = getUserCouponEntityCustomByIdResult.GetSuccessData();
                if (userCoupon != null)
                {
                    totalCouponAmount += userCoupon.Value;
                }
            }


            decimal actualAmountWithOutDeliveryFee = totalShouldPayAmountWithOutDeliveryFee - totalCouponAmount;//不含运费实付
            actualAmountWithOutDeliveryFee = Math.Ceiling(actualAmountWithOutDeliveryFee * 100) / 100;//超出分的金额直接进位
            decimal actualAmount = totalShouldPayAmount - totalCouponAmount;//实付
            actualAmount = Math.Ceiling(actualAmount * 100) / 100;//超出分的金额直接进位
            #endregion

            long orderId = 0;//订单主键
            var orderNo = string.Empty;//订单号
            var createTime = DateTime.Now;//创建时间

            var createBy = string.Empty;
            var telephone = string.Empty;


            var userName = string.Empty;

            if (!string.IsNullOrWhiteSpace(getUserInfoResult?.Data?.UserName))
            {
                userName = getUserInfoResult?.Data?.UserName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(getUserInfoResult?.Data?.NickName))
                {
                    userName = getUserInfoResult?.Data?.NickName;
                }
                else
                {
                    userName = getUserInfoResult?.Data?.UserTel;
                }
            }

            var contactName = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? userName : request.Data.ReceiverName;
            var contactPhone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;


            if (string.IsNullOrEmpty(request.Data.CreatedBy))
            {
                createBy = userName;
            }
            else
            {
                createBy = request.Data.CreatedBy;
            }

            #region 组装订单主信息
            var orderDO = new OrderDO()
            {
                Channel = ChannelEnum.ApolloErpToShop.ToSbyte(),
                ShopId = request.Data.ShopId,//购买套餐卡固定门店
                TerminalType = 2,//TODO: 可判断校验request.Channel
                AppVersion = request.ApiVersion ?? string.Empty,
                OrderType = requestData.OrderType,
                ProduceType = requestData.ProduceType,
                OrderStatus = OrderStatusEnum.Confirmed.ToSbyte(),
                OrderTime = createTime,
                UserId = requestData.UserId,
                UserName = userName,
                UserPhone = getUserInfoResult?.Data?.UserTel ?? string.Empty,
                ContactId = requestData.UserId,
                ContactName = contactName,
                ContactPhone = contactPhone,
                TotalProductNum = totalProductNum,
                TotalProductAmount = totalProductAmount,
                ServiceNum = serviceNum,
                ServiceFee = serviceFee,
                DeliveryFee = deliveryFee,
                TotalCouponAmount = totalCouponAmount,
                ActualAmount = actualAmount,
                PayMethod = (sbyte)(request.Data.PayMethod),
                PayChannel = (sbyte)(request.Data.PayMethod),
                IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                IsNeedDelivery = 0,
                DeliveryType = requestData.DeliveryType,
                DeliveryMethod = 2,
                IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                CompanyId = getShopResult?.Data?.CompanyId ?? 0,
                CreateBy = createBy,
                InstallType = requestData.InstallType,
                CreateTime = createTime,
                SignStatus = OrderSignStatusEnum.HaveSign.ToSbyte(),
                SignTime = DateTime.Now,

            };
            #endregion

            #region 组装用户
            var orderUserDO = _mapper.Map<OrderUserDO>(user);
            orderUserDO.UserId = requestData.UserId;
            orderUserDO.UserName = userName;
            orderUserDO.UserTel = getUserInfoResult?.Data?.UserTel ?? string.Empty;
            var historyOrderCount = await _orderRepository.RecordCountAsync("where user_id=@UserId", new { requestData.UserId });
            if (historyOrderCount == 0)
            {
                orderUserDO.IsFirstOrder = 1;//首单
            }
            #endregion

            #region 开启事务存储数据
            var packageName = string.Empty;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //存订单主信息
                orderId = await _orderRepository.InsertAsync(orderDO);

                //业务更改全部占门店库存
                orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                orderDO.OrderNo = orderNo;
                await _orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                if (orderId <= 0)
                {
                    throw new Exception("下单失败");
                }

                //存用户
                orderUserDO.OrderId = orderId;
                orderUserDO.CreateBy = createBy;
                orderUserDO.CreateTime = createTime;
                await _orderUserRepository.InsertAsync(orderUserDO);

                #region 存储套餐或单品（服务）

                var allInsertedProducts = new List<OrderProductDO>();
                foreach (var item in products)
                {
                    var packageOrProductDO = _mapper.Map<OrderProductDO>(item.PackageOrProduct);
                    packageOrProductDO.OrderId = orderId;
                    packageOrProductDO.OrderNo = orderNo;
                    packageOrProductDO.CreateBy = createBy;
                    packageOrProductDO.CreateTime = createTime;
                    packageName = packageOrProductDO.ProductName;
                    var packageOrProductId = await _orderProductRepository.InsertAsync(packageOrProductDO);

                    //如果有套餐明细批量存储
                    if (item.PackageProducts != null && item.PackageProducts.Any())
                    {
                        var packageDetailDOs = _mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                        decimal allDetailProductTotalAmount = 0;//所有套餐明细商品总金额
                        packageDetailDOs.ForEach(_ =>
                        {
                            allDetailProductTotalAmount += _.Amount;
                        });

                        packageDetailDOs.ForEach(_ =>
                        {
                            _.ParentOrderPackagePid = packageOrProductId;
                            _.OrderId = orderId;
                            _.OrderNo = orderNo;
                            _.CreateBy = createBy;
                            _.CreateTime = createTime;
                        });
                        await _orderProductRepository.InsertBatchAsync(packageDetailDOs);
                        allInsertedProducts.AddRange(packageDetailDOs);
                    }
                }

                #endregion

                #region 使用并存优惠券
                if (userCouponId > 0 && userCoupon != null)
                {
                    //调用优惠券更新使用状态
                    var updateUserCouponStatusReqByIdDTO = new UpdateUserCouponStatusReqByIdRequest()
                    {
                        UserId = requestData.UserId,
                        UserCouponId = userCouponId
                    };
                    var updateUserCouponStatusUsedByIdResult = await _couponClient.UpdateUserCouponStatusUsedById(updateUserCouponStatusReqByIdDTO);
                    _logger.Info($"UpdateUserCouponStatusUsedById updateUserCouponStatusReqByIdDTO={JsonConvert.SerializeObject(updateUserCouponStatusReqByIdDTO)} updateUserCouponStatusUsedByIdResult={JsonConvert.SerializeObject(updateUserCouponStatusUsedByIdResult)}");
                    bool isUpdateUserCouponStatusUsedSuccess = updateUserCouponStatusUsedByIdResult.IsNotNullSuccess() && updateUserCouponStatusUsedByIdResult.GetSuccessData() == true;
                    if (!isUpdateUserCouponStatusUsedSuccess)
                    {
                        throw new Exception("优惠券使用失败");
                    }
                    //组装存储订单优惠券
                    var orderCoupon = new OrderCouponDO()
                    {
                        OrderId = orderId,
                        UserCouponId = userCouponId,
                        CouponRuleId = userCoupon.CouponRuleId,
                        CouponName = userCoupon.DisplayName,
                        CouponAmount = userCoupon.Value,
                        CreateBy = createBy,
                        CreateTime = createTime
                    };
                    await _orderCouponRepository.InsertAsync(orderCoupon);

                }
                #endregion

                #region 发放套餐卡明细

                List<OrderPackageCardDO> orderPackageCardDOs = new List<OrderPackageCardDO>(5);
                allInsertedProducts?.ForEach(_ =>
                {
                    for (var i = 0; i < _.TotalNumber; i++)
                    {
                        orderPackageCardDOs.Add(new OrderPackageCardDO()
                        {
                            RuleId = rule.Id,
                            UserId = request.Data.UserId,
                            UserPhone = getUserInfoResult?.Data?.UserTel ?? string.Empty,
                            SourceOrderNo = orderNo,
                            ProductName = _.ProductName,
                            ProductId = _.ProductId,
                            Price = _.Price,
                            BasePrice = actualAmount,
                            Num = 1,
                            Code = string.Empty,
                            Channel = ChannelEnum.ApolloErpToShop.GetDescription(),
                            Status = 0,
                            StartValidTime = startValidTime,
                            EndValidTime = endValidTime,
                            CreateBy = userName,
                            CreateTime = DateTime.Now,
                            PackageName = packageName
                        });
                    }
                });
                if (orderPackageCardDOs.Count > 0)
                {
                    int currentIndex = 1;
                    decimal sumAvePrice = 0;
                    decimal sumPrice = orderPackageCardDOs.Sum(_ => _.Price);
                    if (sumPrice > 0)
                    {
                        orderPackageCardDOs.ForEach(_ =>
                        {
                            decimal avePrice = 0;
                            if (orderPackageCardDOs.Count == currentIndex++)
                            {
                                avePrice = _.BasePrice - sumAvePrice;
                                if (avePrice < 0)
                                {
                                    avePrice = 0;
                                }
                            }
                            else
                            {
                                avePrice = Math.Round((_.Price / sumPrice) * _.BasePrice, 2);
                                sumAvePrice += avePrice;
                            }
                            _.AvgPrice = avePrice;
                        });
                    }
                    await _orderPackageCardRepository.InsertBatchAsync(orderPackageCardDOs);

                    var codes = await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and user_id=@UserId and source_order_no=@OrderNo", new { UserId = request.Data.UserId, OrderNo = orderNo }, true);
                    //生成核销码并更新
                    foreach (var item in codes)
                    {
                        var code = OrderHelper.GenPackageCode(item.Id);
                        item.Code = code;
                        item.IsDeleted = 1;//注意：下单时预发放核销码先更新为软删除无效，待支付成功后再激活为有效可见（如果创建时软删除无法用Dapper更新Code）
                        await _orderVerificationCodeRepository.UpdateAsync(item, new List<string> { "Code", "IsDeleted" });
                    }
                }

                #endregion

                ts.Complete();
            }
            #endregion

            result = Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, "下单成功");

            return result;
        }

        /// <summary>
        /// 美团导流核销订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitMeiTuanOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 获取订单集合
            if (request?.Data?.OrderNos?.Count() == 0)
                throw new CustomException(CommonConstant.RefOrderIsNotNull);
            var orderInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.Data.OrderNos
            });
            #endregion

            var order = orderInfos?.Data?.FirstOrDefault();

            if (order.OrderStatus == OrderStatusEnum.Confirmed.ToSbyte() && order.DispatchStatus == 1)
            {
                await OrderPayInstall(new OrderPayInstallRequest()
                {
                    OrderNo = order.OrderNo,
                    PayChannel = request.Data.PayChannel, //PayChannelEnum.MeiTuan.ToSbyte(),//美团核销
                    PayMethod = 2,
                    UpdateBy = request.Data.CreatedBy,
                    PayTime = DateTime.Now
                });

                //order.ProduceType = BuyProductTypeEnum.MeiTuanOrder.ToSbyte();
                
                await _orderRepository.UpdateOrderChannel(order.OrderNo,ChannelEnum.MeiTuan.ToInt());

                //await _orderRepository.UpdateOrderCompleteStatus(new BatchUpdateCompleteStatusRequest()
                //{
                //    OrderNo = new List<string>(1) { order.OrderNo },
                //    UpdateBy = request.Data.CreatedBy
                //});

                await _orderRelationRepository.InsertAsync(new OderRelationDO()
                {
                    OrderNo = order?.OrderNo,
                    Refer = "第三方核销",
                    ReferOrder = request?.Data?.MeiTuanOrder,
                    ReferTransferNo = request?.Data?.MeiTuanTransferOrder,
                    CreateBy = request.Data.CreatedBy,
                    CreateTime = DateTime.Now
                });

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Success,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "核销成功！"
                };
            }
            else
            {

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Failed,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "订单没有确认或没有完成派工不能完成核销操作！"
                };

            }


        }

        /// <summary>
        /// 大客户订单核销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitDakehuOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 获取订单集合
            if (request?.Data?.OrderNos?.Count() == 0)
                throw new CustomException(CommonConstant.RefOrderIsNotNull);
            var orderInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.Data.OrderNos
            });
            #endregion

            var order = orderInfos?.Data?.FirstOrDefault();

            if (order.OrderStatus == OrderStatusEnum.Confirmed.ToSbyte() && order.DispatchStatus == 1)
            {
                await OrderPayInstall(new OrderPayInstallRequest()
                {
                    OrderNo = order.OrderNo,
                    PayChannel = request.Data.PayChannel, //PayChannelEnum.DaiZhiFu.ToSbyte(),//代支付
                    PayMethod = request.Data.PayMethod, //3, //月结
                    UpdateBy = request.Data.CreatedBy,
                    PayTime = DateTime.Now
                });

                //order.ProduceType = BuyProductTypeEnum.MeiTuanOrder.ToSbyte();

                await _orderRepository.UpdateOrderChannel(order.OrderNo, ChannelEnum.Dakehu.ToInt());

                //await _orderRepository.UpdateOrderCompleteStatus(new BatchUpdateCompleteStatusRequest()
                //{
                //    OrderNo = new List<string>(1) { order.OrderNo },
                //    UpdateBy = request.Data.CreatedBy
                //});

                await _orderRelationRepository.InsertAsync(new OderRelationDO()
                {
                    OrderNo = order?.OrderNo,
                    Refer = "月结",
                    ReferOrder = request?.Data?.MeiTuanOrder,
                    ReferTransferNo = request?.Data?.MeiTuanTransferOrder,
                    CreateBy = request.Data.CreatedBy,
                    CreateTime = DateTime.Now
                });

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Success,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "核销成功！"
                };
            }
            else
            {

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Failed,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "订单没有确认或没有完成派工不能完成核销操作！"
                };

            }


        }

        /// <summary>
        /// 订单代支付提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitDaiZhiFuOrder(ApiRequest<SubmitOrderRequest> request)
        {
            #region 获取订单集合
            if (request?.Data?.OrderNos?.Count() == 0)
                throw new CustomException(CommonConstant.RefOrderIsNotNull);
            var orderInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.Data.OrderNos
            });
            #endregion

            var order = orderInfos?.Data?.FirstOrDefault();

            if (order.OrderStatus == OrderStatusEnum.Confirmed.ToSbyte() && order.DispatchStatus == 1)
            {
                await OrderPayInstall(new OrderPayInstallRequest()
                {
                    OrderNo = order.OrderNo,
                    PayChannel = PayChannelEnum.DaiZhiFu.ToSbyte(),//代支付
                    PayMethod = 2,
                    UpdateBy = request.Data.CreatedBy,
                    PayTime = DateTime.Now
                });

                if (!string.IsNullOrEmpty(request?.Data?.Remark))
                {
                    await _orderRepository.UpdateOrderRemark(order.OrderNo, request?.Data?.Remark);
                }
                //await _orderRepository.UpdateOrderCompleteStatus(new BatchUpdateCompleteStatusRequest()
                //{
                //    OrderNo = new List<string>(1) { order.OrderNo },
                //    UpdateBy = request.Data.CreatedBy
                //});

                if (!string.IsNullOrEmpty(request?.Data?.MeiTuanTransferOrder))
                {
                    await _orderRelationRepository.InsertAsync(new OderRelationDO()
                    {
                        OrderNo = order?.OrderNo,
                        Refer = "代支付",
                        ReferOrder = request?.Data?.MeiTuanTransferOrder
                    });
                }

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Success,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "核销成功！"
                };
            }
            else
            {

                return new ApiResult<SubmitOrderResponse>()
                {
                    Code = ResultCode.Failed,
                    Data = new SubmitOrderResponse()
                    {
                        OrderNo = request?.Data?.OrderNos?.FirstOrDefault()
                    },
                    Message = "订单没有确认或没有完成派工不能完成代支付操作！"
                };

            }


        }

    }
}

