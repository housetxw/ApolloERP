using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using AutoMapper.Configuration;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Message.Sms;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Clients;
using Ae.ConsumerOrder.Service.Client.Clients.ActivityServer;
using Ae.ConsumerOrder.Service.Client.Clients.ReceiveServer;
using Ae.ConsumerOrder.Service.Client.Clients.StockServer;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Order;
using Ae.ConsumerOrder.Service.Client.Model.Vehicle;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.BaoYang;
using Ae.ConsumerOrder.Service.Client.Request.Coupon;
using Ae.ConsumerOrder.Service.Client.Request.Stock;
using Ae.ConsumerOrder.Service.Common.Constant;
using Ae.ConsumerOrder.Service.Common.Exceptions;
using Ae.ConsumerOrder.Service.Common.Extension;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Dal.Model;
using Ae.ConsumerOrder.Service.Dal.Model.Order;
using Ae.ConsumerOrder.Service.Dal.Model.OrderVerificationCode;
using Ae.ConsumerOrder.Service.Dal.Repository;
using Ae.ConsumerOrder.Service.Dal.Repository.Order;
using Ae.ConsumerOrder.Service.Dal.Repository.OrderVerificationCode;
using Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion;
using Ae.ConsumerOrder.Service.Imp.Helpers;

namespace Ae.ConsumerOrder.Service.Imp.Services
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommandService> logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository productRepository;
        private readonly IOrderAddressRepository orderAddressRepository;
        private readonly IOrderCarRepository orderCarRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IOrderUserRepository orderUserRepository;
        private readonly IOrderCouponRepository orderCouponRepository;
        private readonly IOrderVerificationCodeRepository orderVerificationCodeRepository;
        private readonly IVerificationRuleRepository verificationRuleRepository;
        private readonly IVerificationRuleShopIdRepository verificationRuleShopIdRepository;
        private readonly IVerificationRulePidRepository verificationRulePidRepository;
        private readonly IOrderQueryService orderQueryService;
        private readonly IOrderStockService orderStockService;
        private readonly IShopClient shopClient;
        private readonly IUserClient userClient;
        private readonly IVehicleClient vehicleClient;
        private readonly IOrderCommandClient orderCommandClient;
        private readonly IShopServerClient shopServerClient;
        private readonly ICouponClient couponClient;
        private readonly IProductSearchClient productSearchClient;
        private readonly IReverseClient reverseClient;
        private readonly SmsClient smsClient;
        private readonly IReceiveClient _receiveClient;
        private readonly IOrderDiscountRepository _orderDiscountRepository;
        private readonly IActivityClient _activityClient;
        private readonly IOrderUserShareDetailRepository _orderUserShareDetailRepository;
        private readonly IOrderPackageCardRepository _orderPackageCardRepository;
        private readonly IOrderUserShareRepository _orderUserShareRepository;
        private readonly IShopStockClient _shopStockClient;
        private readonly IBaoYangClient _baoYangClient;



        public OrderCommandService(IMapper mapper, ApolloErpLogger<OrderCommandService> logger, Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOrderRepository orderRepository, IOrderProductRepository productRepository, IOrderAddressRepository orderAddressRepository,
            IOrderCarRepository orderCarRepository, IOrderProductRepository orderProductRepository, IOrderUserRepository orderUserRepository,
            IOrderCouponRepository orderCouponRepository, IOrderVerificationCodeRepository orderVerificationCodeRepository,
            IVerificationRuleRepository verificationRuleRepository, IVerificationRuleShopIdRepository verificationRuleShopIdRepository,
            IVerificationRulePidRepository verificationRulePidRepository, IOrderQueryService orderQueryService, IOrderStockService orderStockService,
            IShopClient shopClient, IUserClient userClient, IVehicleClient vehicleClient, IOrderCommandClient orderCommandClient,
            IShopServerClient shopServerClient, ICouponClient couponClient, IProductSearchClient productSearchClient,
            IReverseClient reverseClient, SmsClient smsClient, IReceiveClient receiveClient,
            IOrderDiscountRepository orderDiscountRepository, IActivityClient activityClient, IOrderUserShareDetailRepository orderUserShareDetailRepository, IOrderUserShareRepository orderUserShareRepository, IOrderPackageCardRepository orderPackageCardRepository, IShopStockClient shopStockClient, IBaoYangClient baoYangClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.configuration = configuration;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderAddressRepository = orderAddressRepository;
            this.orderCarRepository = orderCarRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderUserRepository = orderUserRepository;
            this.orderCouponRepository = orderCouponRepository;
            this.orderVerificationCodeRepository = orderVerificationCodeRepository;
            this.verificationRuleRepository = verificationRuleRepository;
            this.verificationRuleShopIdRepository = verificationRuleShopIdRepository;
            this.verificationRulePidRepository = verificationRulePidRepository;
            this.orderQueryService = orderQueryService;
            this.orderStockService = orderStockService;
            this.shopClient = shopClient;
            this.userClient = userClient;
            this.vehicleClient = vehicleClient;
            this.orderCommandClient = orderCommandClient;
            this.shopServerClient = shopServerClient;
            this.couponClient = couponClient;
            this.productSearchClient = productSearchClient;
            this.reverseClient = reverseClient;
            this.smsClient = smsClient;
            _receiveClient = receiveClient;
            _orderDiscountRepository = orderDiscountRepository;
            _activityClient = activityClient;
            _orderUserShareDetailRepository = orderUserShareDetailRepository;
            _orderPackageCardRepository = orderPackageCardRepository;
            _orderUserShareRepository = orderUserShareRepository;
            _shopStockClient = shopStockClient;
            _baoYangClient = baoYangClient;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request)
        {
            var result = Result.Failed<SubmitOrderResponse>("下单失败");
            try
            {
                var requestData = request.Data;

                //if (requestData.DeliveryType != 1)
                //{
                //    throw new CustomException("配送类型不正确");
                //}

                //if (requestData.Payment != 1)
                //{
                //    throw new CustomException("支付方式不正确");
                //}

                var isCovertSuccess = DateTime.TryParse(request.Data.ReserverTime, out var reserverTime);

                if (isCovertSuccess)
                {
                    var IsSatisfyReserver = await _receiveClient.CheckTheDayReserveWithUserCarId(new Client.Request.Receive.CheckReserveTimeRequest()
                    {
                        CarId = request.Data.CarId,
                        ReserveTime = reserverTime,
                        ShopId = request.Data.ShopId,
                        UserId = request.Data.UserId
                    });
                    if (IsSatisfyReserver?.Data ?? false)
                        throw new CustomException("当天时间已有预约,不可以再次预约");
                }

                if (request.Data.UserCouponId > 0)
                {
                    var checkUserCoupon = await couponClient.CheckUserCouponValidity(new Client.Request.Coupon.CheckUserCouponValidityRequest()
                    {
                        UserCouponId = request.Data.UserCouponId,
                        UserId = request.Data.UserId
                    });

                    if ((checkUserCoupon?.Data ?? false) == false)
                        throw new CustomException(checkUserCoupon?.Message ?? "优惠卷异常");
                }


                #region 准备外部数据
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = requestData.UserId });
                var user = getUserInfoResult?.GetSuccessData();
                if (user == null)
                {
                    throw new CustomException("用户信息异常");
                }

                if (request.Data.ProduceType == ProductTypeEnum.VipCard.ToInt())
                {
                    if ((user?.MemberGrade ?? 0) == 50)//钻石会员 
                    {
                        throw new CustomException("钻石会员，无法再次购买会员卡！");
                    }
                }

                UserVehicleDTO car = null;
                if (!string.IsNullOrWhiteSpace(request.Data.CarId))
                {
                    var getUserVehicleByCarIdResult = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest() { UserId = requestData.UserId, CarId = requestData.CarId });
                    car = getUserVehicleByCarIdResult?.GetSuccessData();
                    if (car == null)
                    {
                        throw new CustomException("车辆信息异常");
                    }
                }

                ShopDTO shop = null;
                if (requestData.DeliveryType == 1)
                {
                    if (requestData.ShopId <= 0)
                    {
                        throw new CustomException("请选择服务门店");
                    }
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
                    shop = getShopResult?.GetSuccessData();
                    if (shop == null)
                    {
                        throw new CustomException("门店信息异常");
                    }
                }
                #endregion

                if (request.Data.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
                {
                    request.Data.ProductInfos.Add(new SelectedProductInfoDTO()
                    {
                        Pid = "BYFW-SMFW-SSMFW-2-FU",
                        Number = 1
                    });
                }

                var selfProducts = requestData?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();
                var shopProducts = requestData?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();

                var products = new List<OrderConfirmPackageProductDTO>();
                var services = new List<OrderConfirmProductDTO>();

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
                #region 根据选择的Pid集合获取套餐、单品集合和服务集合

                if (selfProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = selfProducts;

                    switch (requestData.DeliveryType)
                    {
                        case 1:
                            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
                            getPackageProductServicesRequest.CityId = shop.CityId;
                            break;
                        case 2:
                            if (user.DefaultAddress != null)
                            {
                                getPackageProductServicesRequest.ProvinceId = user.DefaultAddress.ProvinceId;
                                getPackageProductServicesRequest.CityId = user.DefaultAddress.CityId;
                            }
                            break;
                        default:
                            break;
                    }
                    var getPackageProductServicesResponse = await orderQueryService.GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

                    //logger.Info($"SubmitOrder getPackageProductServicesResponse={JsonConvert.SerializeObject(getPackageProductServicesResponse)}  ");

                    products = getPackageProductServicesResponse.Products;
                    services = getPackageProductServicesResponse.Services;
                    //logger.Info($"SubmitOrder products={JsonConvert.SerializeObject(products)}, services={JsonConvert.SerializeObject(services)}  ");

                    if ((products == null || !products.Any()) && (services == null || !services.Any()))
                    {
                        throw new CustomException("商品或服务信息异常");
                    }
                }


                #endregion

                #region 添加项目

                if (shopProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = shopProducts;

                    if (shopProducts?.Count() > 0)
                    {
                        var shopItems = await orderQueryService.GetShopProduct(getPackageProductServicesRequest, null);
                        if (shopItems?.Products?.Count() > 0)
                        {
                            products.AddRange(shopItems?.Products);
                        }
                    }

                }

                #endregion

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
                var getShopServiceListWithPIDResult = await shopServerClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
                {
                    ShopId = requestData.ShopId,
                    ProductIds = allServiceProductIds.ToList()
                });
                var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
                if (serviceCosts != null)
                {
                    var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false && (_.PID != "MF-XC-PX-2-FU" && _.PID != "FW-BY-JC-2-FU"));//服务上下架校验
                    if (findOfflineServices != null && findOfflineServices.Any())
                    {
                        throw new CustomException("所选门店暂不支持此服务，请重新选择服务门店");
                    }
                }
                #endregion

                #region 如果是购买核销码下单特殊处理
                bool isBuyVerificationOrder = false;//是否购买核销码产生的订单
                int orderVerificationCodeNum = 0;//核销码数量
                OrderVerificationCodeDO orderVerificationCodeDO = null;//单个核销码对象


                var verificationOrderProduct = products?.FirstOrDefault();
                var rules = await verificationRuleRepository.GetRule(shop?.Type ?? 0, requestData.ShopId, verificationOrderProduct.PackageOrProduct.ProductId);
                if (rules != null && rules.Any())
                {
                    isBuyVerificationOrder = true;
                    requestData.ProduceType = 1;
                }
                else
                {
                    if (request.Data.OrderType == OrderTypeEnum.Beauty.ToInt())
                    {
                        throw new CustomException("美容团购规则没有配置");
                    }
                }
                if (isBuyVerificationOrder)
                {
                    if (products.Count != 1)
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
                    orderVerificationCodeDO = new OrderVerificationCodeDO()
                    {
                        RuleId = rule.Id,
                        UserId = requestData.UserId,
                        UserPhone = user.UserTel,
                        Channel = "RG",
                        Status = 0,
                        StartValidTime = startValidTime,
                        EndValidTime = endValidTime
                    };
                    if (orderVerificationCodeDO.EndValidTime < DateTime.Today)
                    {
                        throw new CustomException("套餐卡信息异常");
                    }

                    if (verificationOrderProduct != null && verificationOrderProduct.PackageOrProduct != null)
                    {
                        if (verificationOrderProduct.PackageOrProduct.ProductAttribute == 1)
                        {
                            if (verificationOrderProduct.PackageOrProduct.Number != 1 || verificationOrderProduct.PackageProducts == null || verificationOrderProduct.PackageProducts.Count != 1)
                            {
                                throw new CustomException("套餐卡单次仅可购买一份");
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
                        throw new CustomException("套餐卡商品信息异常");
                    }
                }
                #endregion

                #region 团购产品修改价格

                var getShopGroupProducts = await shopClient.GetShopGrouponProduct(new GetShopGrouponProductRequest()
                {
                    ShopId = requestData.ShopId,
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
                var userCouponId = requestData.UserCouponId;//用户选择的优惠券ID
                UserCouponEntityCustomDTO userCoupon = null;//用户优惠券信息
                if (userCouponId > 0)
                {
                    //获取优惠券具体信息
                    var getUserCouponEntityCustomByIdResult = await couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdDTO() { UserCouponId = userCouponId });
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

                long orderId = 0;//订单主键
                var orderNo = string.Empty;//订单号
                var createTime = DateTime.Now;//创建时间

                var createBy = string.Empty;
                var telephone = string.Empty;

                // createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.UserName : request.Data.ReceiverName;
                // telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;

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

                createBy = userName;

                //if (string.IsNullOrEmpty(createBy))
                //{
                //    createBy = string.IsNullOrEmpty(shop.OwnerName) ? (shop.Head ?? string.Empty) : shop.OwnerName ?? shop.Contact;//创建人
                //}
                //if (string.IsNullOrEmpty(telephone))
                //{
                //    telephone = shop?.Telephone ?? string.Empty;
                //}
                #region 组装待存储的数据

                #region 用户为钻石会员 价格 折扣处理

                //钻石折扣
                //var payOrderPointRate = Convert.ToDecimal(configuration["OrderConfig:PayOrderPointRate"]);
                OrderDiscountDO orderDiscountDO = null;
                if ((getUserInfoResult?.Data?.MemberGrade ?? 0) == 50)//钻石折扣
                {
                    var diamondsDiscountRate = Convert.ToDecimal(configuration["OrderConfig:DiamondsDiscountRate"]);


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


                #endregion

                #region 组装订单主信息
                var orderDO = new OrderDO()
                {
                    Channel = 1,
                    TerminalType = 1,//TODO: 可判断校验request.Channel
                    AppVersion = request.ApiVersion ?? string.Empty,
                    OrderType = requestData.OrderType,
                    ProduceType = requestData.ProduceType,
                    OrderStatus = 10,
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
                    PayMethod = (sbyte)(requestData.Payment == 2 ? 2 : 1),
                    PayChannel = (sbyte)(requestData.Payment == 2 ? 0 : 1),
                    IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                    IsNeedDelivery = 1,
                    DeliveryType = requestData.DeliveryType,
                    DeliveryMethod = 2,
                    IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                    CompanyId = shop.CompanyId,
                    ShopId = requestData.ShopId,
                    CreateBy = createBy,
                    InstallType = requestData.InstallType,
                    CreateTime = createTime
                };
                if (isBuyVerificationOrder)
                {
                    orderDO.IsNeedDelivery = 0;//套餐卡核销码订单无需配送
                }
                #endregion

                #region 组装用户
                user.BirthDay = string.IsNullOrEmpty(user.BirthDay) ? "1900-01-01" : user.BirthDay;
                var orderUserDO = mapper.Map<OrderUserDO>(user);
                orderUserDO.UserId = requestData.UserId;
                orderUserDO.UserName = createBy;
                orderUserDO.UserTel = telephone;
                var historyOrderCount = await orderRepository.RecordCountAsync("where user_id=@UserId", new { requestData.UserId });
                if (historyOrderCount == 0)
                {
                    orderUserDO.IsFirstOrder = 1;//首单
                }
                #endregion

                #region 组装地址
                OrderAddressDO orderAddressDO = null;
                if (requestData.DeliveryType == 1)
                {
                    orderAddressDO = mapper.Map<OrderAddressDO>(shop);
                    orderAddressDO.AddressType = 1;
                    orderAddressDO.ShopId = requestData.ShopId;
                    orderAddressDO.DetailAddress = shop.Address;
                }

                if (requestData.ReceiverAddressId > 0)
                {
                    var getUserAddressResult = await userClient.GetUserAddress(new UserAddressRequest() { UserId = requestData.UserId });
                    var userAddresses = getUserAddressResult?.GetSuccessData();
                    if (userAddresses != null)
                    {
                        var address = userAddresses.Find(_ => _.AddressId == requestData.ReceiverAddressId);
                        orderAddressDO = mapper.Map<OrderAddressDO>(address);
                        orderAddressDO.AddressType = 2;
                        orderAddressDO.AddressId = requestData.ReceiverAddressId;
                        orderAddressDO.Label = address.AddressTag;
                        orderAddressDO.IsDefault = (sbyte)(address.DefaultAddress ? 1 : 0);
                        orderAddressDO.DetailAddress = address.AddressLine;
                    }
                }

                //else if (requestData.DeliveryType == 2)
                //{
                //    if (requestData.ReceiverAddressId > 0)
                //    {
                //        var getUserAddressResult = await userClient.GetUserAddress(new UserAddressRequest() { UserId = requestData.UserId });
                //        var userAddresses = getUserAddressResult?.GetSuccessData();
                //        if (userAddresses != null)
                //        {
                //            var address = userAddresses.Find(_ => _.AddressId == requestData.ReceiverAddressId);
                //            orderAddressDO = mapper.Map<OrderAddressDO>(address);
                //            orderAddressDO.AddressType = 2;
                //            orderAddressDO.AddressId = requestData.ReceiverAddressId;
                //            orderAddressDO.Label = address.AddressTag;
                //            orderAddressDO.IsDefault = (sbyte)(address.DefaultAddress ? 1 : 0);
                //            orderAddressDO.DetailAddress = address.AddressLine;
                //        }
                //    }
                //}
                if (orderAddressDO != null)
                {
                    orderAddressDO.ReceiverName = contactName;
                    orderAddressDO.ReceiverPhone = contactPhone;
                    orderAddressDO.CreateBy = createBy;
                    orderAddressDO.CreateTime = createTime;
                }
                #endregion

                #endregion

                //logger.Info($"SubmitOrder begin TransactionScope orderDO={JsonConvert.SerializeObject(orderDO)} ");

                #region 开启事务存储数据
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //存订单主信息
                    orderId = await orderRepository.InsertAsync(orderDO);
                    if (orderId <= 0)
                    {
                        throw new Exception($"下单失败:orderRepository.InsertAsync(orderDO={JsonConvert.SerializeObject(orderDO)})");
                    }
                    //if (request.Data.InstallType == 2)
                    //{
                    //    orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    //}
                    //else
                    //{
                    //    orderNo = $"RGC{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    //}

                    //业务更改全部占门店库存
                    orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    orderDO.OrderNo = orderNo;
                    if (orderDO.IsNeedInstall == 1 && !isBuyVerificationOrder)
                    {
                        orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                    }
                    var updateResult = await orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                    if (!updateResult)
                    {
                        throw new Exception("下单失败:UpdateOrderNoAndInstallCode");
                    }

                    //存用户
                    orderUserDO.OrderId = orderId;
                    orderUserDO.CreateBy = createBy;
                    orderUserDO.CreateTime = createTime;
                    await orderUserRepository.InsertAsync(orderUserDO);

                    #region 存车辆
                    if (car != null)
                    {
                        var orderCarDO = mapper.Map<OrderCarDO>(car);
                        orderCarDO.OrderId = orderId;
                        orderCarDO.CreateBy = createBy;
                        orderCarDO.CreateTime = createTime;
                        await orderCarRepository.InsertAsync(orderCarDO);
                    }

                    #endregion

                    #region 存商品

                    int accumulateSharedProductIndex = 0;//当前分摊商品序号
                    int accumulateSharedServiceIndex = 0;//当前分摊服务序号
                    int needShareProductNum = 0;//需要分摊的商品数（只分摊非零的）
                    int needShareServiceNum = 0;//需要分摊的服务数（只分摊非零的）
                    var findNonZeroProducts = products.FindAll(_ => _.PackageOrProduct.TotalAmount != decimal.Zero);//非零商品
                    if (findNonZeroProducts != null)
                    {
                        needShareProductNum += findNonZeroProducts.Count;
                    }
                    var findNonZeroServices = services.FindAll(_ => _.TotalAmount != decimal.Zero);//非零服务
                    if (findNonZeroServices != null)
                    {
                        needShareServiceNum += findNonZeroServices.Count;
                    }
                    decimal accumulateSharedDiscountAmount = 0;//累计已分摊优惠金额
                    decimal accumulateSharedActualPayAmount = 0;//累计已分摊实付金额

                    #region 存储套餐或单品（服务）
                    var allInsertedProducts = new List<OrderProductDO>();
                    foreach (var item in products)
                    {
                        var packageOrProductDO = mapper.Map<OrderProductDO>(item.PackageOrProduct);
                        packageOrProductDO.OrderId = orderId;
                        packageOrProductDO.OrderNo = orderNo;
                        packageOrProductDO.CreateBy = createBy;
                        packageOrProductDO.CreateTime = createTime;

                        if (item.PackageOrProduct.ProductAttribute == 2 && serviceCosts != null)
                        {
                            if (request.Data.ProduceType != 1)
                            {
                                packageOrProductDO.TotalCostPrice = packageOrProductDO.Price * item.PackageOrProduct.TotalNumber;
                            }
                            else
                            {
                                packageOrProductDO.TotalCostPrice = 0;
                            }

                            var findServiceCost = serviceCosts.Find(c => c.PID == item.PackageOrProduct.ProductId);
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    packageOrProductDO.TotalCostPrice = findServiceCost.CostPrice * item.PackageOrProduct.TotalNumber;
                                }
                            }
                        }
                        //if (item.PackageOrProduct.TotalAmount != decimal.Zero)
                        //{
                        //   // accumulateSharedProductIndex++;
                        //    //if (accumulateSharedProductIndex == needShareProductNum && needShareServiceNum == 0)
                        //    //{
                        //    //    //最后一个使用余下值
                        //    //    packageOrProductDO.ShareDiscountAmount = totalCouponAmount - accumulateSharedDiscountAmount;
                        //    //    packageOrProductDO.ActualPayAmount = actualAmountWithOutDeliveryFee - accumulateSharedActualPayAmount;
                        //    //}
                        //    //else
                        //    //{
                        //    //    //按比例简单计算分摊优惠金额和分摊实付金额
                        //    //    packageOrProductDO.ShareDiscountAmount = decimal.Round(totalCouponAmount * (packageOrProductDO.TotalAmount / totalShouldPayAmountWithOutDeliveryFee), 2, MidpointRounding.AwayFromZero);
                        //    //    packageOrProductDO.ActualPayAmount = decimal.Round(actualAmountWithOutDeliveryFee * (packageOrProductDO.TotalAmount / totalShouldPayAmountWithOutDeliveryFee), 2, MidpointRounding.AwayFromZero);
                        //    //}
                        //    //accumulateSharedDiscountAmount += packageOrProductDO.ShareDiscountAmount;
                        //    //accumulateSharedActualPayAmount += packageOrProductDO.ActualPayAmount;
                        //}
                        var packageOrProductId = await productRepository.InsertAsync(packageOrProductDO);
                        allInsertedProducts.Add(packageOrProductDO);
                        //如果有套餐明细批量存储
                        if (item.PackageProducts != null && item.PackageProducts.Any())
                        {
                            var packageDetailDOs = mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                            decimal allDetailProductTotalAmount = 0;//所有套餐明细商品总金额
                            packageDetailDOs.ForEach(_ =>
                            {
                                allDetailProductTotalAmount += _.Amount;
                            });
                            int accumulateSharedDetailIndex = 0;//当前分摊明细序号
                            int needShareDetailNum = 0;//需要分摊的明细数（只分摊非零的）
                            var findNonZeroDetails = packageDetailDOs.FindAll(_ => _.TotalAmount != decimal.Zero);//非零商品
                            if (findNonZeroDetails != null)
                            {
                                needShareDetailNum += findNonZeroDetails.Count;
                            }
                            decimal detailAccumulateSharedDiscountAmount = 0;//明细累计已分摊优惠金额
                            decimal detailAccumulateSharedActualPayAmount = 0;//明细累计已分摊实付金额
                            packageDetailDOs.ForEach(_ =>
                            {
                                _.ParentOrderPackagePid = packageOrProductId;
                                _.OrderId = orderId;
                                _.OrderNo = orderNo;
                                _.CreateBy = createBy;
                                _.CreateTime = createTime;
                                if (_.ProductAttribute == 2 && serviceCosts != null)
                                {
                                    var findServiceCost = serviceCosts.Find(c => c.PID == _.ProductId);
                                    if (request.Data.ProduceType != 1)
                                    {
                                        _.TotalCostPrice = _.Price * _.TotalNumber;
                                    }
                                    else
                                    {
                                        _.TotalCostPrice = 0;
                                    }
                                    if (findServiceCost != null)
                                    {
                                        if (request.Data.ProduceType != 1)
                                        {
                                            _.TotalCostPrice = findServiceCost.CostPrice * _.TotalNumber;
                                        }

                                    }
                                }
                                //if (_.TotalAmount != decimal.Zero)
                                //{
                                //    accumulateSharedDetailIndex++;
                                //    if (accumulateSharedDetailIndex == needShareDetailNum)
                                //    {
                                //        //最后一个使用余下值
                                //        _.ShareDiscountAmount = packageOrProductDO.ShareDiscountAmount - detailAccumulateSharedDiscountAmount;
                                //        _.ActualPayAmount = packageOrProductDO.ActualPayAmount - detailAccumulateSharedActualPayAmount;
                                //    }
                                //    else
                                //    {
                                //        //按比例简单计算分摊优惠金额和分摊实付金额
                                //        _.ShareDiscountAmount = decimal.Round(packageOrProductDO.ShareDiscountAmount * (_.TotalAmount / allDetailProductTotalAmount), 2, MidpointRounding.AwayFromZero);
                                //        _.ActualPayAmount = decimal.Round(packageOrProductDO.ActualPayAmount * (_.TotalAmount / allDetailProductTotalAmount), 2, MidpointRounding.AwayFromZero);
                                //    }
                                //    detailAccumulateSharedDiscountAmount += _.ShareDiscountAmount;
                                //    detailAccumulateSharedActualPayAmount += _.ActualPayAmount;
                                //}
                            });
                            await productRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                    #endregion

                    #region 存储单独带出的服务
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
                        var serviceProductDO = mapper.Map<OrderProductDO>(item);
                        serviceProductDO.OrderId = orderId;
                        serviceProductDO.OrderNo = orderNo;
                        serviceProductDO.CreateBy = createBy;
                        serviceProductDO.CreateTime = createTime;
                        if (serviceCosts != null)
                        {
                            var findServiceCost = serviceCosts.Find(c => c.PID == serviceProductDO.ProductId);
                            if (request.Data.ProduceType != 1)
                            {
                                serviceProductDO.TotalCostPrice = serviceProductDO.TotalNumber * serviceProductDO.Price;
                            }
                            else
                            {
                                serviceProductDO.TotalCostPrice = 0;
                            }
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    serviceProductDO.TotalCostPrice = findServiceCost.CostPrice * serviceProductDO.TotalNumber;
                                }

                            }
                        }
                        //if (item.TotalAmount != decimal.Zero)
                        //{
                        //    accumulateSharedServiceIndex++;
                        //    if (accumulateSharedServiceIndex == needShareServiceNum)
                        //    {
                        //        //最后一个使用余下值
                        //        serviceProductDO.ShareDiscountAmount = totalCouponAmount - accumulateSharedDiscountAmount;
                        //        serviceProductDO.ActualPayAmount = actualAmountWithOutDeliveryFee - accumulateSharedActualPayAmount;
                        //    }
                        //    else
                        //    {
                        //        //按比例简单计算分摊优惠金额和分摊实付金额
                        //        serviceProductDO.ShareDiscountAmount = decimal.Round(totalCouponAmount * (serviceProductDO.TotalAmount / totalShouldPayAmountWithOutDeliveryFee), 2, MidpointRounding.AwayFromZero);
                        //        serviceProductDO.ActualPayAmount = decimal.Round(actualAmountWithOutDeliveryFee * (serviceProductDO.TotalAmount / totalShouldPayAmountWithOutDeliveryFee), 2, MidpointRounding.AwayFromZero);
                        //    }
                        //    accumulateSharedDiscountAmount += serviceProductDO.ShareDiscountAmount;
                        //    accumulateSharedActualPayAmount += serviceProductDO.ActualPayAmount;
                        //}
                        serviceProductDOs.Add(serviceProductDO);
                    }
                    if (serviceProductDOs.Any())
                    {
                        await productRepository.InsertBatchAsync(serviceProductDOs);
                        allInsertedProducts.Union(serviceProductDOs);
                    }
                    #endregion

                    #endregion

                    //存地址
                    if (orderAddressDO != null)
                    {
                        orderAddressDO.OrderId = orderId;
                        await orderAddressRepository.InsertAsync(orderAddressDO);
                    }

                    if (orderDiscountDO != null)
                    {
                        orderDiscountDO.OrderNo = orderNo;
                        await _orderDiscountRepository.InsertAsync(orderDiscountDO);
                    }

                    #region 使用并存优惠券
                    if (userCouponId > 0 && userCoupon != null)
                    {


                        //调用优惠券更新使用状态
                        var updateUserCouponStatusReqByIdDTO = new UpdateUserCouponStatusReqByIdDTO()
                        {
                            UserId = requestData.UserId,
                            UserCouponId = userCouponId
                        };
                        var updateUserCouponStatusUsedByIdResult = await couponClient.UpdateUserCouponStatusUsedById(updateUserCouponStatusReqByIdDTO);
                        //logger.Info($"UpdateUserCouponStatusUsedById updateUserCouponStatusReqByIdDTO={JsonConvert.SerializeObject(updateUserCouponStatusReqByIdDTO)} updateUserCouponStatusUsedByIdResult={JsonConvert.SerializeObject(updateUserCouponStatusUsedByIdResult)}");
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
                        await orderCouponRepository.InsertAsync(orderCoupon);

                    }
                    #endregion


                    ts.Complete();
                }
                #endregion

                #region 如有存储发放核销码
                if (isBuyVerificationOrder && orderVerificationCodeNum > 0 && orderVerificationCodeDO != null)
                {
                    var orderVerificationCodeDOs = new List<OrderVerificationCodeDO>();
                    orderVerificationCodeDO.OrderNo = orderNo;
                    orderVerificationCodeDO.CreateBy = createBy;
                    orderVerificationCodeDO.CreateTime = createTime;
                    for (int i = 0; i < orderVerificationCodeNum; i++)
                    {
                        orderVerificationCodeDOs.Add(orderVerificationCodeDO);
                    }
                    await orderVerificationCodeRepository.InsertBatchAsync(orderVerificationCodeDOs);
                    var codes = await orderVerificationCodeRepository.GetListByUserIdAndOrderNo(requestData.UserId, orderNo, true);
                    //生成核销码并更新
                    foreach (var item in codes)
                    {
                        var code = OrderHelper.GenVerificationCode(item.Id);
                        item.Code = code;
                        item.IsDeleted = 1;//注意：下单时预发放核销码先更新为软删除无效，待支付成功后再激活为有效可见（如果创建时软删除无法用Dapper更新Code）
                        await orderVerificationCodeRepository.UpdateAsync(item, new List<string> { "Code", "IsDeleted" });
                    }
                }
                #endregion

                //通知订单中心进行信息同步
                //var syncOrderRequest = new SyncOrderRequest() { Order = mapper.Map<MainOrderDTO>(orderDO) };
                //var orderProductDOs = await orderProductRepository.GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
                //syncOrderRequest.OrderProducts = mapper.Map<List<MainOrderProductDTO>>(orderProductDOs);
                //await orderCommandClient.SyncOrder(syncOrderRequest);


                if (isCovertSuccess)
                {
                    await _receiveClient.AddShopReserveV3(new Client.Request.Receive.AddReserveV3Request()
                    {
                        UserId = request.Data.UserId,
                        Year = reserverTime.Year,
                        Month = reserverTime.Month,
                        Day = reserverTime.Day,
                        ReserveTime = $"{reserverTime.Hour}:00",
                        OrderNo = orderNo
                    });
                }

                #region 更新签收
                var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == 0)?.Count() ?? 0;

                products?.ForEach(item =>
                {
                    if (item.PackageProducts != null)
                    {
                        releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == 0)?.Count() ?? 0;
                    }
                });
                if (releaProductCount == 0)
                {
                    await orderRepository.UpdateOrderSignStatus(orderNo, createBy);
                }

                #endregion

                result = Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, "下单成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("SubmitOrderEx", ex);
            }
            finally
            {
                logger.Info($"SubmitOrder request={JsonConvert.SerializeObject(request)} response={JsonConvert.SerializeObject(result)}");
            }
            return result;
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
            try
            {
                ProxyGenerator proxyGenerator = new ProxyGenerator();
                int.TryParse(Regex.Replace(request?.OrderNos?.FirstOrDefault(), @"\D", ""), out var orderId);

                switch (request.UpdateStatusType)
                {
                    case UpdateOrderStatusTypeEnum.SignStatus:
                        {
                            IOrderRepository orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                            {
                                OrderId = orderId,
                                CreateBy = request.UpdateBy,
                                UpdateBy = request.UpdateBy,
                                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                Type2 = OrderLoggerTypeEnum.Sign.ToString(),
                                Filter1 = request?.OrderNos?.FirstOrDefault(),
                                Filter2 = string.Empty,
                                Summary = "更新签收状态"
                            }));
                            dalUpdateOrderStatus = await orderLoggerRepository.UpdateOrderSignStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
                            if (dalUpdateOrderStatus > 0)
                            {
                                var getUpdateOrderStatus = await orderCommandClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                                {
                                    OrderNos = request?.OrderNos,
                                    UpdateBy = request.UpdateBy,
                                    UpdateStatusType = UpdateOrderStatusTypeEnum.SignStatus
                                });
                                if (getUpdateOrderStatus.Code != ResultCode.Success)
                                {
                                    logger.Error($"orderCommandClient UpdateOrderMainStatus  { request?.OrderNos?.FirstOrDefault()} 签收状态 更新失败");
                                }
                            }

                            break;
                        }
                    case UpdateOrderStatusTypeEnum.InstallStatus:
                        {
                            IOrderRepository orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                            {
                                OrderId = orderId,
                                CreateBy = request.UpdateBy,
                                UpdateBy = request.UpdateBy,
                                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                Type2 = OrderLoggerTypeEnum.Install.ToString(),
                                Filter1 = request?.OrderNos?.FirstOrDefault(),
                                Filter2 = string.Empty,
                                Summary = "更新安装状态"
                            }));
                            dalUpdateOrderStatus = await orderLoggerRepository.UpdateOrderInstallStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
                            if (dalUpdateOrderStatus > 0)
                            {
                                var getUpdateOrderStatus = await orderCommandClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                                {
                                    OrderNos = request?.OrderNos,
                                    UpdateBy = request.UpdateBy,
                                    UpdateStatusType = UpdateOrderStatusTypeEnum.InstallStatus
                                });
                                if (getUpdateOrderStatus.Code != ResultCode.Success)
                                {
                                    logger.Error($"orderCommandClient UpdateOrderMainStatus {  request?.OrderNos?.FirstOrDefault()} 安装状态 更新失败");
                                }
                            }
                            var orderNo = request?.OrderNos?.FirstOrDefault();
                            _ = InstalledOrderCommentRemind(orderNo);
                            break;
                        }
                    case UpdateOrderStatusTypeEnum.DeliveryStatus:
                        {
                            IOrderRepository orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                            {
                                OrderId = orderId,
                                CreateBy = request.UpdateBy,
                                UpdateBy = request.UpdateBy,
                                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                Type2 = OrderLoggerTypeEnum.Delivery.ToString(),
                                Filter1 = request?.OrderNos?.FirstOrDefault(),
                                Filter2 = string.Empty,
                                Summary = "更新配送状态"
                            }));
                            dalUpdateOrderStatus = await orderLoggerRepository.UpdateOrderDeliveryStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy);
                            if (dalUpdateOrderStatus > 0)
                            {
                                var getUpdateOrderStatus = await orderCommandClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                                {
                                    OrderNos = request?.OrderNos,
                                    UpdateBy = request.UpdateBy,
                                    UpdateStatusType = UpdateOrderStatusTypeEnum.DeliveryStatus
                                });
                                if (getUpdateOrderStatus.Code != ResultCode.Success)
                                {
                                    logger.Error($"orderCommandClient UpdateOrderMainStatus  { request?.OrderNos?.FirstOrDefault()} 配送状态 更新失败");
                                }
                            }

                            break;
                        }
                    case UpdateOrderStatusTypeEnum.CustomerComment:
                    case UpdateOrderStatusTypeEnum.CustomerAppendComment:
                        {

                            IOrderRepository orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                            {
                                OrderId = orderId,
                                CreateBy = request.UpdateBy,
                                UpdateBy = request.UpdateBy,
                                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                Type2 = OrderLoggerTypeEnum.Comment.ToString(),
                                Filter1 = request?.OrderNos?.FirstOrDefault(),
                                Filter2 = string.Empty,
                                Summary = "更新追评状态"
                            }));

                            int commentStatus = 0;
                            UpdateOrderStatusTypeEnum updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.Null;
                            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerComment)
                            {
                                updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.CustomerComment;
                                commentStatus = CustomerCommentStatusEnum.CustomerComment.ToInt();
                            }

                            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.CustomerAppendComment)
                            {
                                updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.CustomerAppendComment;
                                commentStatus = CustomerCommentStatusEnum.CustomerAppendComment.ToInt();
                            }

                            dalUpdateOrderStatus = await orderLoggerRepository.UpdateOrderCommentStatus(request?.OrderNos?.FirstOrDefault(), request.UpdateBy, commentStatus);
                            if (dalUpdateOrderStatus > 0)
                            {


                                var getUpdateOrderStatus = await orderCommandClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                                {
                                    OrderNos = request?.OrderNos,
                                    UpdateBy = request.UpdateBy,
                                    UpdateStatusType = updateOrderStatusTypeEnum
                                });
                                if (getUpdateOrderStatus.Code != ResultCode.Success)
                                {
                                    logger.Error($"orderCommandClient UpdateOrderMainStatus  { request?.OrderNos?.FirstOrDefault()} 配送状态 更新失败");
                                }
                            }

                            break;

                        }
                    case UpdateOrderStatusTypeEnum.NoReconciliation:
                    case UpdateOrderStatusTypeEnum.ExceptionReconciliation:
                    case UpdateOrderStatusTypeEnum.HaveReconciliation:
                        {
                            var updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.NoReconciliation;
                            int reconciliationStatus = 0;
                            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.NoReconciliation)
                            {
                                reconciliationStatus = ReconciliationStatusEnum.NoReconciliation.ToInt();
                                updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.NoReconciliation;
                            }

                            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.ExceptionReconciliation)
                            {
                                reconciliationStatus = ReconciliationStatusEnum.ExceptionReconciliation.ToInt();
                                updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.ExceptionReconciliation;
                            }

                            if (request.UpdateStatusType == UpdateOrderStatusTypeEnum.HaveReconciliation)
                            {
                                reconciliationStatus = ReconciliationStatusEnum.HaveReconciliation.ToInt();
                                updateOrderStatusTypeEnum = UpdateOrderStatusTypeEnum.HaveReconciliation;
                            }

                            IOrderRepository orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                            {
                                OrderId = orderId,
                                CreateBy = request.UpdateBy,
                                UpdateBy = request.UpdateBy,
                                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                                Type2 = OrderLoggerTypeEnum.Reconciliation.ToString(),
                                Filter1 = request?.OrderNos?.FirstOrDefault(),
                                Filter2 = string.Empty,
                                Summary = "更新对账状态"
                            }));

                            dalUpdateOrderStatus = await orderLoggerRepository.UpdateOrderReconciliationStatus(request.OrderNos, request.UpdateBy, reconciliationStatus);

                            if (dalUpdateOrderStatus > 0)
                            {
                                var getUpdateOrderStatus = await orderCommandClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                                {
                                    OrderNos = request.OrderNos,
                                    UpdateBy = request.UpdateBy,
                                    UpdateStatusType = updateOrderStatusTypeEnum
                                });
                                if (getUpdateOrderStatus.Code != ResultCode.Success)
                                {
                                    logger.Error($"orderCommandClient UpdateOrderMainStatus {string.Join(",", request.OrderNos?.ToArray())} 对账状态 更新失败");
                                }
                            }

                            break;
                        }
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("UpdateOrderStatusEx", ex);
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

        /// <summary>
        /// 审核确认订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CheckOrder(CheckOrderRequest request)
        {
            var result = Result.Failed("审核失败");
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo, true);
                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                //if (order.PayStatus != 1)
                //{
                //    throw new CustomException("订单未支付不可以审核");
                //}
                if (order.OrderStatus >= (sbyte)OrderStatusEnum.Confirmed)
                {
                    result = Result.Success("当前订单状态无需审核");
                    return result;
                }
                if (request.IsCheckPass)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //审核确认并通知占库
                        var proxyGenerator = new ProxyGenerator();
                        var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                        {
                            OrderId = order.Id,
                            CreateBy = request.UpdateBy,
                            UpdateBy = request.UpdateBy,
                            Type1 = OrderLoggerTypeEnum.Order.ToString(),
                            Type2 = OrderLoggerTypeEnum.Check.ToString(),
                            Filter1 = order.OrderNo,
                            Filter2 = string.Empty,
                            Summary = "审核确认并通知占库"
                        }));

                        var isUpdateOrderStatusSuccess = await orderLoggerRepository.UpdateOrderMainStatus(order.Id, (sbyte)OrderStatusEnum.Confirmed, request.UpdateBy);

                        if ((order.ProduceType == ProductTypeEnum.VipCard.ToInt() || order.ProduceType == ProductTypeEnum.BuyVerticationCode.ToInt() || order.ProduceType == ProductTypeEnum.BuyPackageCard.ToInt())&&order.PayStatus==PayStatusEnum.HavePay.ToSbyte())
                        {
                            await orderLoggerRepository.UpdateOrderMainStatus(order.Id, (sbyte)OrderStatusEnum.Completed, request.UpdateBy);

                        }

                        //if (!sendOrderUseStockResult.IsNotNullSuccess())
                        //{
                        //    throw new Exception($"sendOrderUseStockResult={JsonConvert.SerializeObject(sendOrderUseStockResult)}");
                        //}

                        //通知主订单系统修改相应状态
                        //var updateOrderMainStatusResult = await orderCommandClient.UpdateOrderMainStatus(new UpdateOrderMainStatusRequest()
                        //{
                        //    OrderNo = request.OrderNo,
                        //    OrderStatus = (sbyte)OrderStatusEnum.Confirmed,
                        //    UpdateBy = request.UpdateBy
                        //});
                        //logger.Info($"CheckOrder OrderNo={request.OrderNo} updateOrderMainStatusResult={JsonConvert.SerializeObject(updateOrderMainStatusResult)}");
                        //if (!updateOrderMainStatusResult.IsNotNullSuccess())
                        //{
                        //    throw new Exception($"updateOrderMainStatusResult={JsonConvert.SerializeObject(updateOrderMainStatusResult)}");
                        //}

                        ts.Complete();
                    }

                    // logger.Info($"CheckOrder OrderNo={request.OrderNo} isUpdateOrderStatusSuccess={isUpdateOrderStatusSuccess}");
                    var sendOrderUseStockResult = await orderStockService.SendOrderUseStock(new SendOrderUseStockRequest() { OrderNo = request.OrderNo, UpdateBy = request.UpdateBy });
                    // logger.Info($"CheckOrder OrderNo={request.OrderNo} sendOrderUseStockResult={JsonConvert.SerializeObject(sendOrderUseStockResult)}");


                    if (order.ProduceType == ProductTypeEnum.OfficeOrder.ToInt())
                    {
                        await orderRepository.UpdateOrderSignStatus(request.OrderNo, request.UpdateBy);
                    }


                    if (order.ProduceType == ProductTypeEnum.VipCard.ToInt() || order.ProduceType == ProductTypeEnum.BuyVerticationCode.ToInt())
                    {
                        SharingPromotion(new SharingPromotionRequest()
                        {
                            UserId = order.UserId,
                            ActualAmount = order.ActualAmount,
                            OrderNo = order.OrderNo
                        });
                    }

                    result = Result.Success("审核成功");
                }
                else
                {
                    //TODO: 不通过取消订单
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"CheckOrderEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 订单支付成功通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> OrderPaySuccessNotify(OrderPaySuccessNotifyRequest request)
        {
            var result = Result.Failed("处理失败");
            try
            {
                //修改支付状态和到账状态并同步
                var order = await orderRepository.GetOrder(request.OrderNo, true);
                if (order.PayStatus == 1)
                {
                    result = Result.Success("支付状态已经是已支付无需重复执行");
                    return result;
                }
                if (order.MoneyArriveStatus == 1)
                {
                    result = Result.Success("到账状态已经是已到账无需重复执行");
                    return result;
                }

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var updatePayStatusRequest = mapper.Map<UpdatePayStatusRequest>(request);
                    updatePayStatusRequest.OrderNo = order.OrderNo;
                    updatePayStatusRequest.PayStatus = 1;
                    //var updatePayStatusResult = await orderCommandClient.UpdatePayStatus(updatePayStatusRequest);
                    //if (!updatePayStatusResult.IsNotNullSuccess())
                    //{
                    //    throw new CustomException("更新支付状态失败");
                    //}
                    var proxyGenerator = new ProxyGenerator();
                    var orderLoggerRepositoryPayStatus = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                    {
                        OrderId = order.Id,
                        CreateBy = request.UpdateBy,
                        UpdateBy = request.UpdateBy,
                        Type1 = OrderLoggerTypeEnum.Order.ToString(),
                        Type2 = OrderLoggerTypeEnum.PayStatus.ToString(),
                        Filter1 = order.OrderNo,
                        Filter2 = string.Empty,
                        Summary = "修改订单支付状态"
                    }));
                    var isUpdatePayStatusSuccess = await orderLoggerRepositoryPayStatus.UpdatePayStatus(order.Id, 1, request.PayTime, request.UpdateBy, request.PayMethod, request.PayChannel);
                    //var updateMoneyArriveStatusResult = await orderCommandClient.UpdateMoneyArriveStatus(new UpdateMoneyArriveStatusRequest() { OrderNo = order.OrderNo, MoneyArriveStatus = 1, UpdateBy = request.UpdateBy });
                    //if (!updateMoneyArriveStatusResult.IsNotNullSuccess())
                    //{
                    //    throw new CustomException("更新到账状态失败");
                    //}
                    var orderLoggerRepositoryMoneyArriveStatus = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                    {
                        OrderId = order.Id,
                        CreateBy = request.UpdateBy,
                        UpdateBy = request.UpdateBy,
                        Type1 = OrderLoggerTypeEnum.Order.ToString(),
                        Type2 = OrderLoggerTypeEnum.MoneyArriveStatus.ToString(),
                        Filter1 = order.OrderNo,
                        Filter2 = string.Empty,
                        Summary = "修改支付到账状态"
                    }));
                    var isUpdateMoneyArriveStatusSuccess = await orderRepository.UpdateMoneyArriveStatus(order.Id, 1, request.UpdateBy);
                    if (order.ProduceType == 1)
                    {
                        //激活核销码
                        var isActiveByOrderNoSuccess = await orderVerificationCodeRepository.ActiveByOrderNo(order.OrderNo);
                        if (!isActiveByOrderNoSuccess)
                        {
                            throw new CustomException("激活核销码失败");
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

                        await orderRepository.UpdateOrderCompleteStatus(new UpdateCompleteStatusRequest()
                        {
                            OrderNo = order.OrderNo,
                            UpdateBy = request.UpdateBy
                        });


                        //移到下面，非购买套餐卡订单也可以赠券
                        //var orderProduct = (await orderProductRepository.GetListAsync(" where is_deleted=0 and order_no=@OrderNo and parent_order_package_pid=0", new { OrderNo = order.OrderNo }, true))?.FirstOrDefault();

                        //await couponClient.GiftCouponForOrder(new GiftCouponForOrderRequest()
                        //{
                        //    OrderNo = order.OrderNo,
                        //    Channel = AdaptChannelEnum.OnLine,
                        //    TerminalType = Client.Request.Coupon.CouponActivityChannel.MiniApp,
                        //    UserId = order.UserId,
                        //    ProductList = new List<OrderProductRequest>()
                        //    {
                        //        new OrderProductRequest()
                        //        {
                        //            CategoryCode=orderProduct?.CategoryCode,
                        //            Count=orderProduct?.TotalNumber??0,
                        //            Pid=orderProduct?.ProductId,
                        //            Price=orderProduct?.Price??0
                        //        }
                        //    }
                        //});
                    }

                    ts.Complete();
                }

                //自动审核占库并同步相应状态
                await CheckOrder(new CheckOrderRequest() { OrderNo = request.OrderNo, CheckType = 1, IsCheckPass = true, UpdateBy = "OrderPaySuccessNotify" });

                #region 积分充值
                if (request.PayAmount > 0)
                {
                    var payOrderPointRate = Convert.ToDecimal(configuration["OrderConfig:PayOrderPointRate"]);
                    var payOrderPointRate40 = Convert.ToDecimal(configuration["OrderConfig:PayOrderPointRate40"]);
                    var payOrderPointRate50 = Convert.ToDecimal(configuration["OrderConfig:PayOrderPointRate50"]);
                    var payOrderPointRateReferrer = Convert.ToDecimal(configuration["OrderConfig:PayOrderPointRateReferrer"]);
                    //积分规则改成支付成功后按支付金额送积分，只取整数部分，1元=1积分*比率
                    var amt = request.PayAmount * payOrderPointRate;
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
                        var operateUserPointResult = await userClient.OperateUserPoint(operateUserPointRequest);
                        //logger.Info($"OrderPaySuccessNotify 购买成功赠送积分 operateUserPointRequest={JsonConvert.SerializeObject(operateUserPointRequest)} operateUserPointResult={JsonConvert.SerializeObject(operateUserPointResult)}");

                        if (userInfo?.Data?.Channel == ChannelType.Consumer && !string.IsNullOrEmpty(userInfo?.Data?.ReferrerUserId))
                        {
                            int referrerPointValue = Convert.ToInt32(Math.Floor(amtReferrer));
                            await userClient.OperateUserPoint(new OperateUserPointRequest
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
                #endregion

                #region 更新会员成长值
                int payOrderGrowthValue = Convert.ToInt32(Math.Floor(request.PayAmount));
                if (payOrderGrowthValue > 0)
                {
                    await userClient.OperateUserGrowthValue(new OperateUserGrowthValueRequest()
                    {
                        UserId = order.UserId,
                        GrowthValue = payOrderGrowthValue,
                        OperateType = UserGrowthOperateTypeEnum.UserPurchase,
                        ReferrerNo = order.OrderNo,
                        Remark = order.UserPhone,
                        SubmitBy = order.CreateBy
                    });
                }

                #endregion

                //钻石会员发放指定优惠券，先去掉，后续改为配置。
                //var addUserCouponResult = await couponClient.AddUserCouponForDiamondMemeber(new Client.Request.Coupon.AddUserCouponForDiamondMemeberRequest()
                //{
                //    UserId = order.UserId,
                //    UserIP = string.Empty
                //});

                var orderProducts = await orderProductRepository.GetOrderProducts(order.Id);
               
                //购买产品赠优惠券
                var itemProducts = orderProducts?.Where(t => t.ParentOrderPackagePid == 0).ToList();
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
                    couponClient.GiftCouponForOrder(new GiftCouponForOrderRequest()
                    {
                        OrderNo = order.OrderNo,
                        Channel = AdaptChannelEnum.OnLine,
                        TerminalType = Client.Request.Coupon.CouponActivityChannel.MiniApp,
                        UserId = order.UserId,
                        ProductList = aProductList
                    });
                }


                result = Result.Success("处理成功");
            }
            catch (CustomException cex)
            {
                logger.Warn("OrderPaySuccessNotifyCex", cex);
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"OrderPaySuccessNotifyEx", ex);
                result = Result.Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 提交使用核销码产生的订单（即ProduceType=2使用核销码产生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            var result = Result.Failed<SubmitUseVerificationCodeOrderResponse>("下单失败");
            try
            {
                var requestData = request.Data;
                var code = requestData.Code;
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new CustomException("核销码不可为空");
                }
                code = code.ToUpper();
                if (!code.StartsWith("RGHX"))
                {
                    throw new CustomException("核销码格式不正确");
                }
                var codeDO = await orderVerificationCodeRepository.GetByCode(code);
                if (codeDO == null)
                {
                    throw new CustomException("核销码不存在");
                }
                if (codeDO.Status > 0)
                {
                    throw new CustomException("核销码已使用或已过期");
                }
                if (!(codeDO.StartValidTime <= DateTime.Now && codeDO.EndValidTime.AddDays(1) > DateTime.Now))
                {
                    codeDO.Status = 2;
                    await orderVerificationCodeRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                    throw new CustomException("核销码使用时间已过期");
                }

                var ruleId = codeDO.RuleId;
                var ruleDO = await verificationRuleRepository.GetAsync(ruleId);
                if (ruleDO == null)
                {
                    throw new CustomException("核销码规则不存在");
                }
                if (ruleDO.IsValid == 0)
                {
                    throw new CustomException("核销码规则已无效");
                }

                IEnumerable<VerificationRulePidDO> rulePidDOs = null;
                if (ruleDO.IsByPid == 1)
                {
                    rulePidDOs = await verificationRulePidRepository.GetListByRuleId(ruleId);
                }

                var getShopByIdResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
                var shop = getShopByIdResult.GetSuccessData();
                if (shop == null)
                {
                    throw new CustomException("门店信息异常");
                }

                bool isFitShop = false;
                if (ruleDO.IsByShopType == 1)
                {
                    //如果指定门店类型验证是否满足
                    isFitShop = (shop.Type & ruleDO.ShopType) == shop.Type;
                }
                if (!isFitShop)
                {
                    IEnumerable<VerificationRuleShopIdDO> ruleShopIdDOs = null;
                    if (ruleDO.IsByShopId == 1)
                    {
                        ruleShopIdDOs = await verificationRuleShopIdRepository.GetListByRuleId(ruleId);
                        if (ruleShopIdDOs == null || !ruleShopIdDOs.Any())
                        {
                            throw new CustomException("门店信息异常");
                        }
                        var findShop = ruleShopIdDOs.ToList().Find(_ => _.ShopId == requestData.ShopId);
                        if (findShop != null)
                        {
                            isFitShop = true;
                        }
                    }
                }
                if (!isFitShop)
                {
                    throw new CustomException("当前门店不支持此服务");
                }
                var order = await orderRepository.GetOrder(codeDO.OrderNo);

                var orderProducts = await orderProductRepository.GetOrderProducts(order.Id);
                var findNonPackageProducts = orderProducts.FindAll(_ => _.ProductAttribute != 1);
                var orderProduct = findNonPackageProducts?.FirstOrDefault();
                if (orderProduct == null)
                {
                    throw new CustomException("商品信息异常");
                }
                var getShopServiceListWithPIDResult = await shopServerClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
                {
                    ShopId = requestData.ShopId,
                    ProductIds = new List<string>() { orderProduct.ProductId }
                });
                var serviceCosts = getShopServiceListWithPIDResult?.GetSuccessData();
                if (serviceCosts != null)
                {
                    var findOfflineServices = serviceCosts.FindAll(_ => _.IsOnline == false);//服务上下架校验
                    if (findOfflineServices != null && findOfflineServices.Any())
                    {
                        throw new CustomException("所选门店暂不支持此服务，请重新选择服务门店");
                    }
                }

                var orderUser = await orderUserRepository.GetOrderUser(order.Id);
                var orderCar = await orderCarRepository.GetOrderCar(order.Id);
                var orderAddress = await orderAddressRepository.GetOrderAddress(order.Id);
                if (orderUser == null || orderUser == null || orderAddress == null)
                {
                    throw new CustomException("用户信息异常");
                }

                var newOrderProduct = orderProduct;//注意引用类型指针！
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
                newOrder.Channel = 1;
                newOrder.DeliveryStatus = (sbyte)DeliveryStatusEnum.HaveSend.ToInt();
                newOrder.SignStatus = (sbyte)SignStatusEnum.HaveSign.ToInt();
                newOrder.SignTime = DateTime.Now;
                newOrder.DeliveryTime = DateTime.Now;
                newOrder.TerminalType = 2;
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
                newOrder.PayStatus = 1;
                newOrder.PayTime = DateTime.Now;
                newOrder.MoneyArriveStatus = 1;
                newOrder.DispatchStatus = 1;
                newOrder.DispatchTime = DateTime.Now;
                newOrder.InstallStatus = 1;//核销订单自动安装
                newOrder.InstallTime = DateTime.Now;
                newOrder.ShopId = requestData.ShopId;

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

                var newOrderUser = orderUser;
                newOrderUser.Id = 0;
                newOrderUser.OrderId = 0;

                var newOrderCar = orderCar;
                newOrderCar.Id = 0;
                newOrderCar.OrderId = 0;

                var newOrderAddress = orderAddress;
                newOrderAddress.Id = 0;
                newOrderAddress.OrderId = 0;

                var newOrderId = 0;
                var newOrderNo = string.Empty;
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    newOrderId = await orderRepository.InsertAsync(newOrder);
                    newOrderNo = $"RGC{newOrderId.ToString().PadLeft(5, '0')}";//补0到5位
                    newOrder.OrderNo = newOrderNo;
                    if (newOrder.IsNeedInstall == 1)
                    {
                        newOrder.InstallCode = OrderHelper.GenInstallCode(newOrderId);//需要安装的订单生成安装码用于到店验证
                    }
                    await orderRepository.UpdateOrderNoAndInstallCode(newOrderId, newOrderNo, newOrder.InstallCode);

                    newOrderProduct.OrderId = newOrderId;
                    newOrderProduct.OrderNo = newOrderNo;
                    await orderProductRepository.InsertAsync(newOrderProduct);

                    newOrderUser.OrderId = newOrderId;
                    await orderUserRepository.InsertAsync(newOrderUser);

                    newOrderCar.OrderId = newOrderId;
                    await orderCarRepository.InsertAsync(newOrderCar);

                    newOrderAddress.OrderId = newOrderId;
                    await orderAddressRepository.InsertAsync(newOrderAddress);

                    ts.Complete();
                }


                #region  更新核销码使用状态和单号
                codeDO.Status = 1;
                codeDO.VerifyOrderNo = newOrderNo;
                codeDO.VerifyShopId = requestData.ShopId;
                codeDO.VerifyTime = DateTime.Now;
                codeDO.UpdateBy = requestData.ShopId.ToString();
                codeDO.UpdateTime = DateTime.Now;
                await orderVerificationCodeRepository.UpdateAsync(codeDO,
                    new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "VerifyTime", "UpdateBy", "UpdateTime" });
                #endregion


                //通知订单中心进行信息同步
                var syncOrderRequest = new SyncOrderRequest() { Order = mapper.Map<MainOrderDTO>(newOrder) };
                var orderProductDOs = await orderProductRepository.GetListAsync("where order_id=@OrderId", new { OrderId = newOrderId });
                syncOrderRequest.OrderProducts = mapper.Map<List<MainOrderProductDTO>>(orderProductDOs);
                await orderCommandClient.SyncOrder(syncOrderRequest);

                _ = InstalledOrderCommentRemind(newOrderNo);

                result = Result.Success(new SubmitUseVerificationCodeOrderResponse() { OrderNo = newOrderNo }, "下单成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"SubmitUseVerificationCodeOrderEx", ex);
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
            var result = Result.Failed("处理失败");
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var proxyGenerator = new ProxyGenerator();
                    var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
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
                                var orderLoggerRepositoryOrderStatus = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
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
                    var orderReverseNotifyResult = await orderCommandClient.OrderReverseNotify(request);
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
                logger.Error("OrderReverseNotifyEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 再次购买
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<BuyAgainResponse>> BuyAgain(ApiRequest<BuyAgainRequest> request)
        {
            var result = Result.Failed<BuyAgainResponse>("下单失败");
            try
            {
                var requstData = request.Data;
                var orderNo = requstData.OrderNo;
                var userId = requstData.UserId;
                if (!orderNo.ToUpper().StartsWith("RGC"))
                {
                    result = Result.Failed<BuyAgainResponse>("渠道错误");
                    return result;
                }
                var order = await orderRepository.GetOrder(userId, orderNo);
                if (order == null)
                {
                    result = Result.Failed<BuyAgainResponse>("原订单信息异常");
                    return result;
                }

                #region 准备再次购买自动提交订单请求参数
                var submitOrderRequestData = mapper.Map<SubmitOrderRequest>(order);
                var car = await orderCarRepository.GetOrderCar(order.Id);
                submitOrderRequestData.CarId = car.CarId;
                var address = await orderAddressRepository.GetOrderAddress(order.Id);
                submitOrderRequestData.ReceiverName = address.ReceiverName;
                submitOrderRequestData.ReceiverPhone = address.ReceiverPhone;
                submitOrderRequestData.ReceiverAddressId = address.AddressId;
                submitOrderRequestData.ProduceType = 3;
                switch (order.PayMethod)
                {
                    case 1:
                        if (order.PayChannel == 1)
                        {
                            submitOrderRequestData.Payment = 1;
                        }
                        break;
                    case 2:
                        submitOrderRequestData.Payment = 2;
                        break;
                    default:
                        break;
                }
                submitOrderRequestData.ProductInfos = new List<SelectedProductInfoDTO>();
                var products = await orderProductRepository.GetOrderProducts(order.Id);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        if (item.ParentOrderPackagePid == 0 || string.IsNullOrWhiteSpace(item.ServeForOrderPids))
                        {
                            submitOrderRequestData.ProductInfos.Add(new SelectedProductInfoDTO()
                            {
                                Pid = item.ProductId,
                                Number = item.Number
                            });
                        }
                    }
                }
                #endregion

                var submitOrderRequest = new ApiRequest<SubmitOrderRequest>()
                {
                    Channel = request.Channel,
                    ApiVersion = request.ApiVersion,
                    DeviceId = request.DeviceId,
                    Data = submitOrderRequestData
                };
                var submitOrderResult = await SubmitOrder(submitOrderRequest);
                if (submitOrderResult.IsNotNullSuccess())
                {
                    result = Result.Success(new BuyAgainResponse() { OrderNo = submitOrderResult?.Data?.OrderNo }, "下单成功");
                }
                else
                {
                    result = Result.Failed<BuyAgainResponse>(submitOrderResult.Code, submitOrderResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("BuyAgainEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 追加下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderRequest> request)
        {
            var result = Result.Failed<AppendSubmitOrderResponse>("下单失败");
            try
            {
                var requstData = request.Data;
                var orderNo = requstData.OrderNo;
                var appendForProductId = requstData.AppendForProductId;
                if (!orderNo.ToUpper().StartsWith("RGC"))
                {
                    throw new CustomException("渠道错误");
                }
                var order = await orderRepository.GetOrder(orderNo);
                if (order == null)
                {
                    throw new CustomException("原订单信息异常");
                }

                #region 准备再次购买自动提交订单请求参数
                var submitOrderRequestData = mapper.Map<SubmitOrderRequest>(order);
                var car = await orderCarRepository.GetOrderCar(order.Id);
                submitOrderRequestData.CarId = car.CarId;
                var address = await orderAddressRepository.GetOrderAddress(order.Id);
                submitOrderRequestData.ReceiverName = address.ReceiverName;
                submitOrderRequestData.ReceiverPhone = address.ReceiverPhone;
                submitOrderRequestData.ReceiverAddressId = address.AddressId;
                submitOrderRequestData.ProduceType = 4;
                switch (order.PayMethod)
                {
                    case 1:
                        if (order.PayChannel == 1)
                        {
                            submitOrderRequestData.Payment = 1;
                        }
                        break;
                    case 2:
                        submitOrderRequestData.Payment = 2;
                        break;
                    default:
                        break;
                }
                submitOrderRequestData.ProductInfos = new List<SelectedProductInfoDTO>();
                var productDetailSearchRequest = new ProductDetailSearchRequest()
                {
                    ProductCodes = new List<string>() { appendForProductId }
                };
                var getProductsByProductCodesResult = await productSearchClient.GetProductsByProductCodes(productDetailSearchRequest);
                //logger.Info($"AppendSubmitOrder 查询关联商品 orderNo={orderNo} productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} getProductsByProductCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
                var product = getProductsByProductCodesResult.GetSuccessData()?.FirstOrDefault();
                if (product == null)
                {
                    throw new CustomException("原商品已下架");
                }
                var associatedGood = product.Attributevalues?.Find(_ => _.AttributeName == "AssociatedGood");
                if (associatedGood == null || string.IsNullOrWhiteSpace(associatedGood.AttributeValue))
                {
                    throw new CustomException("追加商品信息异常，请检查属性配置");
                }
                var associatedGoodPid = associatedGood.AttributeValue;
                submitOrderRequestData.ProductInfos.Add(new SelectedProductInfoDTO()
                {
                    Pid = associatedGoodPid,
                    Number = requstData.Number
                });
                #endregion

                var submitOrderRequest = new ApiRequest<SubmitOrderRequest>()
                {
                    Channel = request.Channel,
                    ApiVersion = request.ApiVersion,
                    DeviceId = request.DeviceId,
                    Data = submitOrderRequestData
                };
                var submitOrderResult = await SubmitOrder(submitOrderRequest);
                if (submitOrderResult.IsNotNullSuccess())
                {
                    result = Result.Success(new AppendSubmitOrderResponse() { OrderNo = submitOrderResult?.Data?.OrderNo }, "下单成功");
                }
                else
                {
                    result = Result.Failed<AppendSubmitOrderResponse>(submitOrderResult.Code, submitOrderResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("AppendSubmitOrderEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 更新订单的成本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderProductCost(UpdateOrderProductRequest request)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            int.TryParse(Regex.Replace(request?.OrderNo, @"\D", ""), out var orderId);
            var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderRepository>(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
            {
                OrderId = orderId,
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = OrderLoggerTypeEnum.Order.ToString(),
                Type2 = OrderLoggerTypeEnum.UpdateCost.ToString(),
                Filter1 = request?.OrderNo,
                Filter2 = string.Empty,
                Summary = "更新订单的成本",
                IsRecordOrderProduct = true
            }));

            var updateOrderProductCost = await orderLoggerRepository.UpdateOrderProductCost(request);

            if (updateOrderProductCost > 0)
            {
                var updateProductTotalCostPriceResponse = await orderCommandClient.UpdateProductTotalCostPrice(new UpdateProductTotalCostPriceRequest()
                {
                    OrderNo = request.OrderNo,
                    OrderPid = request.OrderProductId,
                    TotalCostPrice = request.Cost,
                    UpdateBy = request.UpdateBy,
                    Remark = request.Remark
                });
                if (updateProductTotalCostPriceResponse.Code != ResultCode.Success)
                {
                    logger.Error($"orderClient UpdateProductTotalCostPrice {request.OrderNo}-{request.Pid}  {CommonConstant.UpdateProductCostFailure}");
                }
            }

            if (updateOrderProductCost > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.UpdateProductCostSuccess
                };
            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = CommonConstant.UpdateProductCostFailure
            };
        }

        /// <summary>
        /// 催促订单付款
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> UrgeOrderPayment()
        {
            var result = Result.Failed("执行异常");
            try
            {
                var list = await orderRepository.GetUnpaidOrders();
                if (list == null || !list.Any())
                {
                    result = Result.Success("执行成功，没有需要催促付款的订单");
                    return result;
                }
                result = Result.Success("任务请求已收到并执行中...请勿重复提交");
                _ = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            //通知订单付款
                            if (!item.UserName.Contains("测试"))
                            {
                                var sms = new SmsParameter
                                {
                                    PhoneNumbers = item.UserPhone,
                                    SignName = "总部",
                                    TemplateCode = "SMS_180955393",
                                    TemplateParam = JsonConvert.SerializeObject(new { order = item.OrderNo })
                                };
                                var sendSmsResult = smsClient.SendSms(sms);
                                var isSendSuccess = sendSmsResult.Code.Equals("10000");
                                logger.Info($"UrgeOrderPayment orderNo={item.OrderNo} 催促订单付款短信发送结果：{isSendSuccess}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("RunUrgeOrderPaymentEx", ex);
                    }
                }, TaskCreationOptions.LongRunning);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("UrgeOrderPaymentEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 自动取消超时订单
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> AutoCancelTimeOutOrder()
        {
            var result = Result.Failed("执行异常");
            try
            {
                var list = await orderRepository.GetTimeOutUncanceledOrders();
                if (list == null || !list.Any())
                {
                    result = Result.Success("执行成功，没有需要自动取消的超时订单");
                    return result;
                }
                result = Result.Success("任务请求已收到并执行中...请勿重复提交");
                _ = Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        foreach (var item in list)
                        {
                            var createReverseOrderForCancelRequest = new CreateReverseOrderForCancelRequest()
                            {
                                OrderNo = item.OrderNo,
                                OperatorType = ReverseOperatorTypeEnum.CustomerService,
                                OperatorName = "系统",
                                ReasonId = 4,
                                Instruction = "超时系统自动取消"
                            };
                            var createReverseOrderForCancelResult = await reverseClient.CreateReverseOrderForCancel(createReverseOrderForCancelRequest);
                            if (createReverseOrderForCancelResult.IsNotNullSuccess())
                            {
                                //订单自动取消通知
                                if (!item.UserName.Contains("测试"))
                                {
                                    var sms = new SmsParameter
                                    {
                                        PhoneNumbers = item.UserPhone,
                                        SignName = "总部",
                                        TemplateCode = "SMS_180955388",
                                        TemplateParam = JsonConvert.SerializeObject(new { odrder = item.OrderNo })
                                    };
                                    var sendSmsResult = smsClient.SendSms(sms);
                                    var isSendSuccess = sendSmsResult.Code.Equals("10000");
                                    logger.Info($"AutoCancelTimeOutOrder orderNo={item.OrderNo} 自动取消超时订单成功，短信发送结果：{isSendSuccess}");
                                }
                            }
                            else
                            {
                                logger.Warn($"AutoCancelTimeOutOrder orderNo={item.OrderNo} 自动取消超时订单失败：createReverseOrderForCancelRequest={JsonConvert.SerializeObject(createReverseOrderForCancelRequest)} createReverseOrderForCancelResult={JsonConvert.SerializeObject(createReverseOrderForCancelResult)}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("RunAutoCancelTimeOutOrderEx", ex);
                    }
                }, TaskCreationOptions.LongRunning);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("AutoCancelTimeOutOrderEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 安装完成订单评价提醒
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> InstalledOrderCommentRemind(string orderNo)
        {
            var result = Result.Failed("执行失败");
            try
            {
                return Result.Success("评价功能暂未上线无需发送提醒");//TODO: 评价功能暂未上线无需发送提醒
                if (string.IsNullOrWhiteSpace(orderNo))
                {
                    throw new CustomException("订单号异常");
                }
                var order = await orderRepository.GetOrder(orderNo);
                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                //提醒订单评价
                if (!order.UserName.Contains("测试"))
                {
                    var sms = new SmsParameter
                    {
                        PhoneNumbers = order.UserPhone,
                        SignName = "总部",
                        TemplateCode = "SMS_180955384",
                        TemplateParam = JsonConvert.SerializeObject(new { order = order.OrderNo })
                    };
                    var sendSmsResult = smsClient.SendSms(sms);
                    var isSendSuccess = sendSmsResult.Code.Equals("10000");
                    logger.Info($"InstalledOrderCommentRemind orderNo={order.OrderNo} 安装完成订单评价提醒短信发送结果：{isSendSuccess}");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("InstalledOrderCommentRemindEx", ex);
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
                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed(ResultCode.SUCCESS_NOT_EXIST, "订单不存在");
                    return result;
                }
                bool isSuccess = false;
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //修改c端订单预约状态
                    var proxyGenerator = new ProxyGenerator();
                    var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                    {
                        OrderId = order.Id,
                        CreateBy = "系统",
                        UpdateBy = "系统",
                        Type1 = OrderLoggerTypeEnum.Order.ToString(),
                        Type2 = OrderLoggerTypeEnum.ReserveStatus.ToString(),
                        Filter1 = order.OrderNo,
                        Filter2 = string.Empty,
                        Summary = "更新预约状态"
                    }));
                    isSuccess = await orderLoggerRepository.UpdateOrderReserveStatus(order.Id, request.ReserveStatus, request.ReserveTime);

                    //修改订单主表预约状态
                    //var updateOrderReserveStatusResult = await orderCommandClient.UpdateOrderReserveStatus(new UpdateOrderReserveStatusClientRequest()
                    //{
                    //    OrderNo = request.OrderNo,
                    //    ReserveStatus = request.ReserveStatus,
                    //    ReserveTime = request.ReserveTime
                    //});
                    //logger.Info($"UpdateOrderReserveStatus OrderNo={request.OrderNo} updateOrderReserveStatusResult={JsonConvert.SerializeObject(updateOrderReserveStatusResult)}");
                    //if (!updateOrderReserveStatusResult.IsNotNullSuccess())
                    //{
                    //    throw new Exception($"updateOrderReserveStatusResult={JsonConvert.SerializeObject(updateOrderReserveStatusResult)}");
                    //}

                    ts.Complete();
                }

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
        /// 修改派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request)
        {
            var result = await orderRepository.UpdateOrderDispatchStatus(request.OrderNos, request.CreateBy, request.DispatchStatus);
            if (result)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "修改派工成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "修改派工失败"
            };
        }

        public async Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {

            var isSuccess = await orderRepository.CancelOrder(request.Data.OrderNo, request.Data.CreateBy);

            var orderPackages = await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and verify_order_no=@OrderNo", new { OrderNo = request.Data.OrderNo }, true);

            if (orderPackages != null && orderPackages.Any())
            {
                var orderPackage = orderPackages.First();
                orderPackage.Status = 0;
                orderPackage.UpdateBy = request.Data.CreateBy;
                orderPackage.UpdateTime = DateTime.Now;
                orderPackage.VerifyOrderNo = string.Empty;
                orderPackage.VerifyShopId = 0;
                await _orderPackageCardRepository.UpdateAsync(orderPackage, new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "UpdateBy", "UpdateTime" });
            }

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

        public async Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo);
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

        public async Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request)
        {
            var result = Result.Failed();
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo);
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


        /// <summary>
        /// 订单完结发放促销优惠卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SharingPromotion(SharingPromotionRequest request)
        {
            var sharingAmount = Convert.ToDecimal(configuration["SharingPromotion:Amount"]);
            if (request.ActualAmount >= sharingAmount)
            {
                var getUserRefers = await _activityClient.GetUserRefers(new Client.Request.Activity.GetUserRefersRequest()
                {
                    UserId = request.UserId
                });

                if (!string.IsNullOrWhiteSpace(getUserRefers?.Data?.ReferrerUserId))
                {
                    var getOrderUserShareDetail = await _orderUserShareDetailRepository.GetOrderUserShareDetailDoByUserId(getUserRefers?.Data.ReferrerUserId, request.UserId);

                    if (getOrderUserShareDetail == null)
                    {
                        var orderUserShareDO = await _orderUserShareRepository.GetOrderUserShare(getUserRefers?.Data?.ReferrerUserId);
                        if (orderUserShareDO == null)
                        {
                            await _orderUserShareRepository.InsertAsync(new Dal.Model.SharingPromotin.OrderUserShareDO()
                            {
                                UserId = getUserRefers?.Data?.ReferrerUserId,
                                UserName = getUserRefers?.Data?.ReferrenUserName,
                                CreateBy = request.UserId,
                                CreateTime = DateTime.Now,
                                ShareValidSumOrderNum = 1,
                                ExchangedCouponNum = 0,
                                ExchangeSumOrderNum = 1
                            });
                        }
                        else
                        {
                            await _orderUserShareRepository.UpdateOrderUserShareNum(getUserRefers?.Data?.ReferrerUserId, 1, 1, 0);
                        }


                        await _orderUserShareDetailRepository.InsertAsync(new Dal.Model.SharingPromotin.OrderUserShareDetailDO()
                        {
                            CouponId = 0,
                            CreateBy = request.UserId,
                            CreateTime = DateTime.Now,
                            SourceUserId = getUserRefers?.Data?.ReferrerUserId,
                            DestinationUserId = request.UserId,
                            OrderNo = request.OrderNo,
                            ExchangeStatus = 0,
                            UpdateBy = request.UserId,
                            UpdateTime = DateTime.Now
                        });
                    }

                    var getOrderUserShare = await _orderUserShareRepository.GetOrderUserShare(getUserRefers?.Data?.ReferrerUserId);
                    var exchangeSumOrderNum = getOrderUserShare?.ExchangeSumOrderNum ?? 0;

                    var sharingNum = Convert.ToInt32(configuration["SharingPromotion:SharingNum"]);
                    if (exchangeSumOrderNum >= sharingNum)
                    {
                        var getOrderUnUseShareDetail = await _orderUserShareDetailRepository.GetOrderUserShareDetail(new Core.Request.SharingPromotion.GetSharingOrdersRequest()
                        {
                            UserId = getUserRefers?.Data?.ReferrerUserId,
                            ExchangeStatus = true,
                            PageIndex = 1,
                            PageSize = sharingNum
                        });

                        var orderNos = getOrderUnUseShareDetail?.Items?.Select(_ => _.OrderNo)?.ToList();


                        var couponResponse = await couponClient.AddRecommendCourteousCoupon(new AddRecommendCourteousCouponRequest()
                        {
                            UserId = getUserRefers?.Data?.ReferrerUserId
                        });

                        await _orderUserShareDetailRepository.UpdateOrderUserShareDetailStatus(orderNos, couponResponse?.Data ?? 0);


                        await _orderUserShareRepository.UpdateOrderUserShareNum(getUserRefers?.Data?.ReferrerUserId, 0, -sharingNum, 1);

                    }
                }

            }

            return await Task.FromResult(new ApiResult()
            {
                Code = ResultCode.Success
            });
        }

        /// <summary>
        /// 购买套餐卡订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SubmitBuyPackageOrder(ApiRequest<SubmitOrderRequest> request)
        {
            var result = Result.Failed<SubmitOrderResponse>("下单失败");
            try
            {
                var requestData = request.Data;

                if (request.Data.UserCouponId > 0)
                {
                    var checkUserCoupon = await couponClient.CheckUserCouponValidity(new Client.Request.Coupon.CheckUserCouponValidityRequest()
                    {
                        UserCouponId = request.Data.UserCouponId,
                        UserId = request.Data.UserId
                    });

                    if ((checkUserCoupon?.Data ?? false) == false)
                        throw new CustomException(checkUserCoupon?.Message ?? "优惠卷异常");
                }


                #region 准备外部数据
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = requestData.UserId });
                var user = getUserInfoResult?.GetSuccessData();
                if (user == null)
                {
                    throw new CustomException("用户信息异常");
                }

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

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);

                #region 根据选择的Pid集合获取套餐、单品集合和服务集合

                if (selfProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = selfProducts;

                    var getPackageProductServicesResponse = await orderQueryService.GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

                    products = getPackageProductServicesResponse.Products;
                    services = getPackageProductServicesResponse.Services;
                    if ((products == null || !products.Any()) && (services == null || !services.Any()))
                    {
                        throw new CustomException("商品或服务信息异常");
                    }
                }

                #endregion

                var verificationOrderProduct = products?.FirstOrDefault();
                var rules = await verificationRuleRepository.GetRuleForPackage(verificationOrderProduct.PackageOrProduct.ProductId);
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
                UserCouponEntityCustomDTO userCoupon = null;//用户优惠券信息
                if (userCouponId > 0)
                {
                    //获取优惠券具体信息
                    var getUserCouponEntityCustomByIdResult = await couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdDTO() { UserCouponId = userCouponId });
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

                createBy = userName;

                #region 组装订单主信息
                var orderDO = new OrderDO()
                {
                    Channel = ChannelEnum.ApolloErpToC.ToSbyte(),
                    ShopId = 0,//购买套餐卡固定门店
                    TerminalType = 1,//TODO: 可判断校验request.Channel
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
                    PayMethod = (sbyte)(requestData.Payment == 2 ? 2 : 1),
                    PayChannel = (sbyte)(requestData.Payment == 2 ? 0 : 1),
                    IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                    IsNeedDelivery = 0,
                    DeliveryType = requestData.DeliveryType,
                    DeliveryMethod = 2,
                    IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                    CompanyId = 0,
                    CreateBy = createBy,
                    InstallType = requestData.InstallType,
                    CreateTime = createTime,
                    SignStatus = SignStatusEnum.HaveSign.ToSbyte(),
                    SignTime = DateTime.Now,

                };
                #endregion

                #region 组装用户
                var orderUserDO = mapper.Map<OrderUserDO>(user);
                orderUserDO.UserId = requestData.UserId;
                orderUserDO.UserName = createBy;
                orderUserDO.UserTel = telephone;
                var historyOrderCount = await orderRepository.RecordCountAsync("where user_id=@UserId", new { requestData.UserId });
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
                    orderId = await orderRepository.InsertAsync(orderDO);

                    //业务更改全部占门店库存
                    orderNo = $"RGC{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    orderDO.OrderNo = orderNo;
                    await orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                    if (orderId <= 0)
                    {
                        throw new Exception("下单失败");
                    }

                    //存用户
                    orderUserDO.OrderId = orderId;
                    orderUserDO.CreateBy = createBy;
                    orderUserDO.CreateTime = createTime;
                    await orderUserRepository.InsertAsync(orderUserDO);

                    #region 存储套餐或单品（服务）

                    var allInsertedProducts = new List<OrderProductDO>();
                    foreach (var item in products)
                    {
                        var packageOrProductDO = mapper.Map<OrderProductDO>(item.PackageOrProduct);
                        packageOrProductDO.OrderId = orderId;
                        packageOrProductDO.OrderNo = orderNo;
                        packageOrProductDO.CreateBy = createBy;
                        packageOrProductDO.CreateTime = createTime;
                        packageName = packageOrProductDO.ProductName;
                        var packageOrProductId = await productRepository.InsertAsync(packageOrProductDO);

                        //如果有套餐明细批量存储
                        if (item.PackageProducts != null && item.PackageProducts.Any())
                        {
                            var packageDetailDOs = mapper.Map<List<OrderProductDO>>(item.PackageProducts);
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
                            await productRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.AddRange(packageDetailDOs);
                        }
                    }

                    #endregion

                    #region 使用并存优惠券
                    if (userCouponId > 0 && userCoupon != null)
                    {

                        //调用优惠券更新使用状态
                        var updateUserCouponStatusReqByIdDTO = new UpdateUserCouponStatusReqByIdDTO()
                        {
                            UserId = requestData.UserId,
                            UserCouponId = userCouponId
                        };
                        var updateUserCouponStatusUsedByIdResult = await couponClient.UpdateUserCouponStatusUsedById(updateUserCouponStatusReqByIdDTO);
                        logger.Info($"UpdateUserCouponStatusUsedById updateUserCouponStatusReqByIdDTO={JsonConvert.SerializeObject(updateUserCouponStatusReqByIdDTO)} updateUserCouponStatusUsedByIdResult={JsonConvert.SerializeObject(updateUserCouponStatusUsedByIdResult)}");
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
                        await orderCouponRepository.InsertAsync(orderCoupon);

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
                                BasePrice=actualAmount,
                                Price = _.Price,
                                Num = 1,
                                Code = string.Empty,
                                Channel = ChannelEnum.ApolloErpToC.GetDescription(),
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
                            await orderVerificationCodeRepository.UpdateAsync(item, new List<string> { "Code", "IsDeleted" });
                        }
                    }

                    #endregion

                    ts.Complete();
                }
                #endregion

                result = Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, "下单成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("SubmitOrderEx", ex);
            }
            finally
            {
                logger.Info($"SubmitOrder request={JsonConvert.SerializeObject(request)} response={JsonConvert.SerializeObject(result)}");
            }
            return result;
        }

        /// <summary>
        /// 使用套餐卡订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> UseBuyPackageOrder(ApiRequest<SubmitOrderRequest> request)
        {

            var result = Result.Failed<SubmitOrderResponse>("下单失败");
            try
            {
                var requestData = request.Data;
                var code = requestData.Code;

                var isCovertSuccess = DateTime.TryParse(request.Data.ReserverTime, out var reserverTime);

                if (isCovertSuccess)
                {
                    var IsSatisfyReserver = await _receiveClient.CheckTheDayReserveWithUserCarId(new Client.Request.Receive.CheckReserveTimeRequest()
                    {
                        CarId = request.Data.CarId,
                        ReserveTime = reserverTime,
                        ShopId = request.Data.ShopId,
                        UserId = request.Data.UserId
                    });
                    if (IsSatisfyReserver?.Data ?? false)
                        throw new CustomException("当天时间已有预约,不可以再次预约");
                }

                #region 准备外部数据
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = requestData.UserId });
                var user = getUserInfoResult?.GetSuccessData();
                if (user == null)
                {
                    throw new CustomException("用户信息异常");
                }

                UserVehicleDTO car = null;
                if (!string.IsNullOrWhiteSpace(request.Data.CarId))
                {
                    var getUserVehicleByCarIdResult = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest() { UserId = requestData.UserId, CarId = requestData.CarId });
                    car = getUserVehicleByCarIdResult?.GetSuccessData();
                    if (car == null)
                    {
                        throw new CustomException("车辆信息异常");
                    }
                }

                var getShopByIdResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
                var shop = getShopByIdResult.GetSuccessData();
                if (shop == null)
                {
                    throw new CustomException("门店信息异常");
                }

                #endregion

                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new CustomException("核销码不可为空");
                }
                code = code.ToUpper();
                if (!code.StartsWith("RGTC"))
                {
                    throw new CustomException("核销码格式不正确");
                }
                var codeDO = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = code }, true)).FirstOrDefault();
                if (codeDO == null)
                {
                    throw new CustomException("核销码不存在");
                }
                if (codeDO.Status > 0)
                {
                    throw new CustomException("核销码已使用或已过期");
                }
                if (!(codeDO.StartValidTime <= DateTime.Now && codeDO.EndValidTime.AddDays(1) > DateTime.Now))
                {
                    codeDO.Status = 2;
                    await _orderPackageCardRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                    throw new CustomException("核销码使用时间已过期");
                }

                var ruleId = codeDO.RuleId;


                //var ruleDO = await verificationRuleRepository.GetAsync(ruleId);
                //if (ruleDO == null)
                //{
                //    throw new CustomException("核销码规则不存在");
                //}
                //if (ruleDO.IsValid == 0)
                //{
                //    throw new CustomException("核销码规则已无效");
                //}

                var rules = await verificationRuleRepository.GetRuleForPackageCard(shop?.Type ?? 0, requestData.ShopId, codeDO.RuleId);
                if (rules == null || !rules.Any())
                {
                    throw new CustomException("此核销码不能再此门店使用!");
                }

                var products = new List<OrderConfirmPackageProductDTO>();
                var services = new List<OrderConfirmProductDTO>();

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(new GetOrderConfirmPackageProductServicesRequest()
                {
                    CityId = shop.CityId,
                    ProvinceId = shop.ProvinceId,
                    CarId = request.Data.CarId,
                    ProductInfos = new List<SelectedProductInfoDTO>()
                    {
                        new SelectedProductInfoDTO()
                        {
                            Number=codeDO.Num,
                            Pid=codeDO.ProductId
                        }
                    }
                });

                var getPackageProductServicesResponse = await orderQueryService.GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

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
                var getShopServiceListWithPIDResult = await shopServerClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
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
                        throw new CustomException("所选门店暂不支持此服务，请重新选择服务门店");
                    }
                }
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
                decimal actualAmount = decimal.Zero;//实付
                #endregion


                #region 组装订单主信息
                var orderDO = new OrderDO()
                {
                    Channel = ChannelEnum.ApolloErpToC.ToSbyte(),
                    TerminalType = 1,//TODO: 可判断校验request.Channel
                    AppVersion = request.ApiVersion ?? string.Empty,
                    OrderType = requestData.OrderType,
                    ProduceType = ProductTypeEnum.UsePackageCard.ToSbyte(),
                    OrderStatus = 20,
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
                    PayMethod = (sbyte)(requestData.Payment == 2 ? 2 : 1),
                    PayChannel = (sbyte)(requestData.Payment == 2 ? 0 : 1),
                    IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                    IsNeedDelivery = 1,
                    DeliveryType = requestData.DeliveryType,
                    DeliveryMethod = 2,
                    IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                    CompanyId = shop.CompanyId,
                    ShopId = requestData.ShopId,
                    CreateBy = createBy,
                    InstallType = requestData.InstallType,
                    CreateTime = createTime,
                    PayStatus = PayStatusEnum.HavePay.ToSbyte(),
                    PayTime = DateTime.Now
                };

                #endregion

                #region 组装用户
                var orderUserDO = mapper.Map<OrderUserDO>(user);
                orderUserDO.UserId = requestData.UserId;
                orderUserDO.UserName = createBy;
                orderUserDO.UserTel = telephone;
                var historyOrderCount = await orderRepository.RecordCountAsync("where user_id=@UserId", new { requestData.UserId });
                if (historyOrderCount == 0)
                {
                    orderUserDO.IsFirstOrder = 1;//首单
                }
                #endregion

                #region 组装地址
                OrderAddressDO orderAddressDO = null;
                if (requestData.DeliveryType == 1)
                {
                    orderAddressDO = mapper.Map<OrderAddressDO>(shop);
                    orderAddressDO.AddressType = 1;
                    orderAddressDO.ShopId = requestData.ShopId;
                    orderAddressDO.DetailAddress = shop.Address;
                }
                if (orderAddressDO != null)
                {
                    orderAddressDO.ReceiverName = contactName;
                    orderAddressDO.ReceiverPhone = contactPhone;
                    orderAddressDO.CreateBy = createBy;
                    orderAddressDO.CreateTime = createTime;
                }
                #endregion

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //存订单主信息
                    orderId = await orderRepository.InsertAsync(orderDO);


                    //业务更改全部占门店库存
                    orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    orderDO.OrderNo = orderNo;
                    if (orderDO.IsNeedInstall == 1)
                    {
                        orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                    }
                    await orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                    if (orderId <= 0)
                    {
                        throw new Exception("下单失败");
                    }

                    //存用户
                    orderUserDO.OrderId = orderId;
                    orderUserDO.CreateBy = createBy;
                    orderUserDO.CreateTime = createTime;
                    await orderUserRepository.InsertAsync(orderUserDO);

                    if (car != null)
                    {
                        var orderCarDO = mapper.Map<OrderCarDO>(car);
                        orderCarDO.OrderId = orderId;
                        orderCarDO.CreateBy = createBy;
                        orderCarDO.CreateTime = createTime;
                        await orderCarRepository.InsertAsync(orderCarDO);
                    }

                    var allInsertedProducts = new List<OrderProductDO>();
                    foreach (var item in products)
                    {
                        var packageOrProductDO = mapper.Map<OrderProductDO>(item.PackageOrProduct);
                        packageOrProductDO.OrderId = orderId;
                        packageOrProductDO.OrderNo = orderNo;
                        packageOrProductDO.CreateBy = createBy;
                        packageOrProductDO.CreateTime = createTime;

                        if (item.PackageOrProduct.ProductAttribute == 2 && serviceCosts != null)
                        {
                            if (request.Data.ProduceType != 1)
                            {
                                packageOrProductDO.TotalCostPrice = packageOrProductDO.Price * item.PackageOrProduct.TotalNumber;
                            }
                            else
                            {
                                packageOrProductDO.TotalCostPrice = 0;
                            }

                            var findServiceCost = serviceCosts.Find(c => c.PID == item.PackageOrProduct.ProductId);
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    packageOrProductDO.TotalCostPrice = findServiceCost.CostPrice * item.PackageOrProduct.TotalNumber;
                                }
                            }
                        }

                        var packageOrProductId = await productRepository.InsertAsync(packageOrProductDO);
                        allInsertedProducts.Add(packageOrProductDO);
                        //如果有套餐明细批量存储
                        if (item.PackageProducts != null && item.PackageProducts.Any())
                        {
                            var packageDetailDOs = mapper.Map<List<OrderProductDO>>(item.PackageProducts);

                            packageDetailDOs.ForEach(_ =>
                            {
                                _.ParentOrderPackagePid = packageOrProductId;
                                _.OrderId = orderId;
                                _.OrderNo = orderNo;
                                _.CreateBy = createBy;
                                _.CreateTime = createTime;
                                if (_.ProductAttribute == 2 && serviceCosts != null)
                                {
                                    var findServiceCost = serviceCosts.Find(c => c.PID == _.ProductId);
                                    if (request.Data.ProduceType != 1)
                                    {
                                        _.TotalCostPrice = _.Price * _.TotalNumber;
                                    }
                                    else
                                    {
                                        _.TotalCostPrice = 0;
                                    }
                                    if (findServiceCost != null)
                                    {
                                        if (request.Data.ProduceType != 1)
                                        {
                                            _.TotalCostPrice = findServiceCost.CostPrice * _.TotalNumber;
                                        }

                                    }
                                }

                            });
                            await productRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }



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
                        var serviceProductDO = mapper.Map<OrderProductDO>(item);
                        serviceProductDO.OrderId = orderId;
                        serviceProductDO.OrderNo = orderNo;
                        serviceProductDO.CreateBy = createBy;
                        serviceProductDO.CreateTime = createTime;
                        if (serviceCosts != null)
                        {
                            var findServiceCost = serviceCosts.Find(c => c.PID == serviceProductDO.ProductId);
                            if (request.Data.ProduceType != 1)
                            {
                                serviceProductDO.TotalCostPrice = serviceProductDO.TotalNumber * serviceProductDO.Price;
                            }
                            else
                            {
                                serviceProductDO.TotalCostPrice = 0;
                            }
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    serviceProductDO.TotalCostPrice = findServiceCost.CostPrice * serviceProductDO.TotalNumber;
                                }

                            }
                        }

                        serviceProductDOs.Add(serviceProductDO);
                    }
                    if (serviceProductDOs.Any())
                    {
                        await productRepository.InsertBatchAsync(serviceProductDOs);
                        allInsertedProducts.Union(serviceProductDOs);
                    }



                    //存地址
                    if (orderAddressDO != null)
                    {
                        orderAddressDO.OrderId = orderId;
                        await orderAddressRepository.InsertAsync(orderAddressDO);
                    }


                    codeDO.Status = 1;
                    codeDO.VerifyOrderNo = orderNo;
                    codeDO.VerifyShopId = requestData.ShopId;
                    codeDO.VerifyTime = DateTime.Now;
                    codeDO.UpdateBy = requestData.ShopId.ToString();
                    codeDO.UpdateTime = DateTime.Now;

                    await _orderPackageCardRepository.UpdateAsync(codeDO,
                        new List<string>() { "Status", "VerifyOrderNo", "VerifyShopId", "VerifyTime", "UpdateBy", "UpdateTime" });


                    ts.Complete();
                }

                if (isCovertSuccess)
                {
                    await _receiveClient.AddShopReserveV3(new Client.Request.Receive.AddReserveV3Request()
                    {
                        UserId = request.Data.UserId,
                        Year = reserverTime.Year,
                        Month = reserverTime.Month,
                        Day = reserverTime.Day,
                        ReserveTime = $"{reserverTime.Hour}:00",
                        OrderNo = orderNo
                    });
                }


                var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == 0)?.Count() ?? 0;

                products?.ForEach(item =>
                {
                    if (item.PackageProducts != null)
                    {
                        releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == 0)?.Count() ?? 0;
                    }
                });
                if (releaProductCount == 0)
                {
                    await orderRepository.UpdateOrderSignStatus(orderNo, createBy);
                }





                if (releaProductCount > 0)
                {
                    ///为了兼容C端下单产生的上门安装订单 付完钱之后才去占库，因为需要占门店库存，支付回调的时候调用了B端的回调接口
                    _shopStockClient.OrderOccupyStock(new OrderOccupyStockRequest()
                    {
                        QueueNo = orderNo,
                        CreateBy = createBy,
                        QueueStatus = "OrderService"
                    });
                }

                result = Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, "下单成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"SubmitUseVerificationCodeOrderEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 秒杀订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitOrderResponse>> SecKillOrder(ApiRequest<SubmitOrderRequest> request)
        {
            var result = Result.Failed<SubmitOrderResponse>("下单失败");
            try
            {
                var requestData = request.Data;

                var isCovertSuccess = DateTime.TryParse(request.Data.ReserverTime, out var reserverTime);

                if (isCovertSuccess)
                {
                    var IsSatisfyReserver = await _receiveClient.CheckTheDayReserveWithUserCarId(new Client.Request.Receive.CheckReserveTimeRequest()
                    {
                        CarId = request.Data.CarId,
                        ReserveTime = reserverTime,
                        ShopId = request.Data.ShopId,
                        UserId = request.Data.UserId
                    });
                    if (IsSatisfyReserver?.Data ?? false)
                        throw new CustomException("当天时间已有预约,不可以再次预约");
                }

                if (request.Data.UserCouponId > 0)
                {
                    var checkUserCoupon = await couponClient.CheckUserCouponValidity(new Client.Request.Coupon.CheckUserCouponValidityRequest()
                    {
                        UserCouponId = request.Data.UserCouponId,
                        UserId = request.Data.UserId
                    });

                    if ((checkUserCoupon?.Data ?? false) == false)
                        throw new CustomException(checkUserCoupon?.Message ?? "优惠卷异常");
                }


                #region 准备外部数据
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = requestData.UserId });
                var user = getUserInfoResult?.GetSuccessData();
                if (user == null)
                {
                    throw new CustomException("用户信息异常");
                }

                UserVehicleDTO car = null;
                if (!string.IsNullOrWhiteSpace(request.Data.CarId))
                {
                    var getUserVehicleByCarIdResult = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest() { UserId = requestData.UserId, CarId = requestData.CarId });
                    car = getUserVehicleByCarIdResult?.GetSuccessData();
                    if (car == null)
                    {
                        throw new CustomException("车辆信息异常");
                    }
                }

                ShopDTO shop = null;
                if (requestData.DeliveryType == 1)
                {
                    if (requestData.ShopId <= 0)
                    {
                        throw new CustomException("请选择服务门店");
                    }
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = requestData.ShopId });
                    shop = getShopResult?.GetSuccessData();
                    if (shop == null)
                    {
                        throw new CustomException("门店信息异常");
                    }
                }
                #endregion


                var selfProducts = requestData?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();
                var shopProducts = requestData?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();

                var getFlashSaleProduct = await productSearchClient.GetFlashSaleProduct(new Client.Model.Product.FlashSaleConfigDTO()
                {
                    ProductId = selfProducts?.FirstOrDefault()?.Pid
                });

                if (getFlashSaleProduct.Data == null)
                {
                    throw new CustomException("此秒杀已结束");
                }

                var products = new List<OrderConfirmPackageProductDTO>();
                var services = new List<OrderConfirmProductDTO>();

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(requestData);
                #region 根据选择的Pid集合获取套餐、单品集合和服务集合

                if (selfProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = selfProducts;

                    switch (requestData.DeliveryType)
                    {
                        case 1:
                            getPackageProductServicesRequest.ProvinceId = shop.ProvinceId;
                            getPackageProductServicesRequest.CityId = shop.CityId;
                            break;
                        case 2:
                            if (user.DefaultAddress != null)
                            {
                                getPackageProductServicesRequest.ProvinceId = user.DefaultAddress.ProvinceId;
                                getPackageProductServicesRequest.CityId = user.DefaultAddress.CityId;
                            }
                            break;
                        default:
                            break;
                    }
                    var getPackageProductServicesResponse = await orderQueryService.GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);

                    products = getPackageProductServicesResponse.Products;
                    services = getPackageProductServicesResponse.Services;
                    if ((products == null || !products.Any()) && (services == null || !services.Any()))
                    {
                        throw new CustomException("商品或服务信息异常");
                    }
                }

                #endregion

                #region 添加项目

                if (shopProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = shopProducts;

                    if (shopProducts?.Count() > 0)
                    {
                        var shopItems = await orderQueryService.GetShopProduct(getPackageProductServicesRequest, null);
                        if (shopItems?.Products?.Count() > 0)
                        {
                            products.AddRange(shopItems?.Products);
                        }
                    }

                }

                #endregion

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
                var getShopServiceListWithPIDResult = await shopServerClient.GetShopServiceListWithPID(new GetShopServiceListWithPIDRequest()
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
                        throw new CustomException("所选门店暂不支持此服务，请重新选择服务门店");
                    }
                }
                #endregion


                #region 修改价格

                if (ProductTypeEnum.SeckillOrder.ToSbyte() == request.Data.ProduceType)
                {
                    var activeFlashProducts = await productSearchClient.GetActiveFlashSaleConfigs();
                    products?.ForEach(_ =>
                    {
                        var singleProduct = activeFlashProducts?.Data?.Where(item => item.ProductId == _?.PackageOrProduct?.ProductId)?.FirstOrDefault();
                        if (singleProduct != null)
                        {
                            _.PackageOrProduct.Price = singleProduct.Price;
                            _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * singleProduct.Price;
                        }
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
                foreach (var item in services)
                {
                    serviceNum += item.TotalNumber;
                    serviceFee += item.TotalAmount;
                }
                decimal totalShouldPayAmountWithOutDeliveryFee = totalProductAmount + serviceFee;//不含运费应付总价
                decimal totalShouldPayAmount = totalShouldPayAmountWithOutDeliveryFee + deliveryFee;//应付总价

                decimal totalCouponAmount = decimal.Zero;//总优惠金额
                var userCouponId = requestData.UserCouponId;//用户选择的优惠券ID
                UserCouponEntityCustomDTO userCoupon = null;//用户优惠券信息
                if (userCouponId > 0)
                {
                    //获取优惠券具体信息
                    var getUserCouponEntityCustomByIdResult = await couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdDTO() { UserCouponId = userCouponId });
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

                // createBy = string.IsNullOrWhiteSpace(request.Data.ReceiverName) ? user.UserName : request.Data.ReceiverName;
                // telephone = string.IsNullOrWhiteSpace(request.Data.ReceiverPhone) ? user.UserTel : request.Data.ReceiverPhone;

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

                createBy = userName;


                #region 组装待存储的数据


                #region 组装订单主信息
                var orderDO = new OrderDO()
                {
                    Channel = 1,
                    TerminalType = 1,//TODO: 可判断校验request.Channel
                    AppVersion = request.ApiVersion ?? string.Empty,
                    OrderType = requestData.OrderType,
                    ProduceType = requestData.ProduceType,
                    OrderStatus = 10,
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
                    PayMethod = (sbyte)(requestData.Payment == 2 ? 2 : 1),
                    PayChannel = (sbyte)(requestData.Payment == 2 ? 0 : 1),
                    IsNeedInvoice = (sbyte)(requestData.IsNeedInvoice ? 1 : 0),
                    IsNeedDelivery = 1,
                    DeliveryType = requestData.DeliveryType,
                    DeliveryMethod = 2,
                    IsNeedInstall = (sbyte)(requestData.DeliveryType == 1 ? 1 : 0),
                    CompanyId = shop.CompanyId,
                    ShopId = requestData.ShopId,
                    CreateBy = createBy,
                    InstallType = requestData.InstallType,
                    CreateTime = createTime
                };

                #endregion

                #region 组装用户
                var orderUserDO = mapper.Map<OrderUserDO>(user);
                orderUserDO.UserId = requestData.UserId;
                orderUserDO.UserName = createBy;
                orderUserDO.UserTel = telephone;
                var historyOrderCount = await orderRepository.RecordCountAsync("where user_id=@UserId", new { requestData.UserId });
                if (historyOrderCount == 0)
                {
                    orderUserDO.IsFirstOrder = 1;//首单
                }
                #endregion

                #region 组装地址
                OrderAddressDO orderAddressDO = null;
                if (requestData.DeliveryType == 1)
                {
                    orderAddressDO = mapper.Map<OrderAddressDO>(shop);
                    orderAddressDO.AddressType = 1;
                    orderAddressDO.ShopId = requestData.ShopId;
                    orderAddressDO.DetailAddress = shop.Address;
                }

                if (requestData.ReceiverAddressId > 0)
                {
                    var getUserAddressResult = await userClient.GetUserAddress(new UserAddressRequest() { UserId = requestData.UserId });
                    var userAddresses = getUserAddressResult?.GetSuccessData();
                    if (userAddresses != null)
                    {
                        var address = userAddresses.Find(_ => _.AddressId == requestData.ReceiverAddressId);
                        orderAddressDO = mapper.Map<OrderAddressDO>(address);
                        orderAddressDO.AddressType = 2;
                        orderAddressDO.AddressId = requestData.ReceiverAddressId;
                        orderAddressDO.Label = address.AddressTag;
                        orderAddressDO.IsDefault = (sbyte)(address.DefaultAddress ? 1 : 0);
                        orderAddressDO.DetailAddress = address.AddressLine;
                    }
                }


                if (orderAddressDO != null)
                {
                    orderAddressDO.ReceiverName = contactName;
                    orderAddressDO.ReceiverPhone = contactPhone;
                    orderAddressDO.CreateBy = createBy;
                    orderAddressDO.CreateTime = createTime;
                }
                #endregion

                #endregion

                #region 开启事务存储数据
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //存订单主信息
                    orderId = await orderRepository.InsertAsync(orderDO);


                    //业务更改全部占门店库存
                    orderNo = $"RGB{orderId.ToString().PadLeft(5, '0')}";//补0到5位
                    orderDO.OrderNo = orderNo;
                    if (orderDO.IsNeedInstall == 1)
                    {
                        orderDO.InstallCode = OrderHelper.GenInstallCode(orderId);//需要安装且非购买核销码的订单生成安装码用于到店验证
                    }
                    await orderRepository.UpdateOrderNoAndInstallCode(orderId, orderNo, orderDO.InstallCode);
                    if (orderId <= 0)
                    {
                        throw new Exception("下单失败");
                    }

                    //存用户
                    orderUserDO.OrderId = orderId;
                    orderUserDO.CreateBy = createBy;
                    orderUserDO.CreateTime = createTime;
                    await orderUserRepository.InsertAsync(orderUserDO);

                    #region 存车辆
                    if (car != null)
                    {
                        var orderCarDO = mapper.Map<OrderCarDO>(car);
                        orderCarDO.OrderId = orderId;
                        orderCarDO.CreateBy = createBy;
                        orderCarDO.CreateTime = createTime;
                        await orderCarRepository.InsertAsync(orderCarDO);
                    }

                    #endregion

                    #region 存商品

                    #region 存储套餐或单品（服务）
                    var allInsertedProducts = new List<OrderProductDO>();
                    foreach (var item in products)
                    {
                        var packageOrProductDO = mapper.Map<OrderProductDO>(item.PackageOrProduct);
                        packageOrProductDO.OrderId = orderId;
                        packageOrProductDO.OrderNo = orderNo;
                        packageOrProductDO.CreateBy = createBy;
                        packageOrProductDO.CreateTime = createTime;

                        if (item.PackageOrProduct.ProductAttribute == 2 && serviceCosts != null)
                        {
                            if (request.Data.ProduceType != 1)
                            {
                                packageOrProductDO.TotalCostPrice = packageOrProductDO.Price * item.PackageOrProduct.TotalNumber;
                            }
                            else
                            {
                                packageOrProductDO.TotalCostPrice = 0;
                            }

                            var findServiceCost = serviceCosts.Find(c => c.PID == item.PackageOrProduct.ProductId);
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    packageOrProductDO.TotalCostPrice = findServiceCost.CostPrice * item.PackageOrProduct.TotalNumber;
                                }
                            }
                        }

                        var packageOrProductId = await productRepository.InsertAsync(packageOrProductDO);
                        allInsertedProducts.Add(packageOrProductDO);
                        //如果有套餐明细批量存储
                        if (item.PackageProducts != null && item.PackageProducts.Any())
                        {
                            var packageDetailDOs = mapper.Map<List<OrderProductDO>>(item.PackageProducts);
                            packageDetailDOs.ForEach(_ =>
                            {
                                _.ParentOrderPackagePid = packageOrProductId;
                                _.OrderId = orderId;
                                _.OrderNo = orderNo;
                                _.CreateBy = createBy;
                                _.CreateTime = createTime;
                                if (_.ProductAttribute == 2 && serviceCosts != null)
                                {
                                    var findServiceCost = serviceCosts.Find(c => c.PID == _.ProductId);
                                    if (request.Data.ProduceType != 1)
                                    {
                                        _.TotalCostPrice = _.Price * _.TotalNumber;
                                    }
                                    else
                                    {
                                        _.TotalCostPrice = 0;
                                    }
                                    if (findServiceCost != null)
                                    {
                                        if (request.Data.ProduceType != 1)
                                        {
                                            _.TotalCostPrice = findServiceCost.CostPrice * _.TotalNumber;
                                        }

                                    }
                                }
                            });
                            await productRepository.InsertBatchAsync(packageDetailDOs);
                            allInsertedProducts.Union(packageDetailDOs);
                        }
                    }
                    #endregion

                    #region 存储单独带出的服务
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
                        var serviceProductDO = mapper.Map<OrderProductDO>(item);
                        serviceProductDO.OrderId = orderId;
                        serviceProductDO.OrderNo = orderNo;
                        serviceProductDO.CreateBy = createBy;
                        serviceProductDO.CreateTime = createTime;
                        if (serviceCosts != null)
                        {
                            var findServiceCost = serviceCosts.Find(c => c.PID == serviceProductDO.ProductId);
                            if (request.Data.ProduceType != 1)
                            {
                                serviceProductDO.TotalCostPrice = serviceProductDO.TotalNumber * serviceProductDO.Price;
                            }
                            else
                            {
                                serviceProductDO.TotalCostPrice = 0;
                            }
                            if (findServiceCost != null)
                            {
                                if (request.Data.ProduceType != 1)
                                {
                                    serviceProductDO.TotalCostPrice = findServiceCost.CostPrice * serviceProductDO.TotalNumber;
                                }

                            }
                        }
                        serviceProductDOs.Add(serviceProductDO);
                    }
                    if (serviceProductDOs.Any())
                    {
                        await productRepository.InsertBatchAsync(serviceProductDOs);
                        allInsertedProducts.Union(serviceProductDOs);
                    }
                    #endregion

                    #endregion

                    //存地址
                    if (orderAddressDO != null)
                    {
                        orderAddressDO.OrderId = orderId;
                        await orderAddressRepository.InsertAsync(orderAddressDO);
                    }

                    #region 使用并存优惠券
                    if (userCouponId > 0 && userCoupon != null)
                    {


                        //调用优惠券更新使用状态
                        var updateUserCouponStatusReqByIdDTO = new UpdateUserCouponStatusReqByIdDTO()
                        {
                            UserId = requestData.UserId,
                            UserCouponId = userCouponId
                        };
                        var updateUserCouponStatusUsedByIdResult = await couponClient.UpdateUserCouponStatusUsedById(updateUserCouponStatusReqByIdDTO);
                        logger.Info($"UpdateUserCouponStatusUsedById updateUserCouponStatusReqByIdDTO={JsonConvert.SerializeObject(updateUserCouponStatusReqByIdDTO)} updateUserCouponStatusUsedByIdResult={JsonConvert.SerializeObject(updateUserCouponStatusUsedByIdResult)}");
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
                        await orderCouponRepository.InsertAsync(orderCoupon);

                    }
                    #endregion


                    ts.Complete();
                }
                #endregion

                if (isCovertSuccess)
                {
                    await _receiveClient.AddShopReserveV3(new Client.Request.Receive.AddReserveV3Request()
                    {
                        UserId = request.Data.UserId,
                        Year = reserverTime.Year,
                        Month = reserverTime.Month,
                        Day = reserverTime.Day,
                        ReserveTime = $"{reserverTime.Hour}:00",
                        OrderNo = orderNo
                    });
                }

                #region 更新秒杀产品数量

                getFlashSaleProduct.Data.SaleNum = selfProducts.FirstOrDefault().Number;
                var updateFlash = await productSearchClient.UpdateFlashSaleConfigNum(getFlashSaleProduct.Data);

                #endregion

                #region 更新签收
                var releaProductCount = products?.Where(_ => _.PackageOrProduct.ProductAttribute == 0)?.Count() ?? 0;

                products?.ForEach(item =>
                {
                    if (item.PackageProducts != null)
                    {
                        releaProductCount += item.PackageProducts?.Where(_ => _.ProductAttribute == 0)?.Count() ?? 0;
                    }
                });
                if (releaProductCount == 0)
                {
                    await orderRepository.UpdateOrderSignStatus(orderNo, createBy);
                }

                #endregion

                result = Result.Success(new SubmitOrderResponse() { OrderNo = orderNo }, "下单成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("SubmitOrderEx", ex);
            }
            finally
            {
                logger.Info($"SubmitOrder request={JsonConvert.SerializeObject(request)} response={JsonConvert.SerializeObject(result)}");
            }
            return result;
        }
    }
}
