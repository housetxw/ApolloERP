using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Clients.Order;
using Ae.ShopOrder.Service.Client.Clients.OrderForC;
using Ae.ShopOrder.Service.Client.Clients.ShopServer;
using Ae.ShopOrder.Service.Client.Clients.PayServer;
using Ae.ShopOrder.Service.Client.Clients.Receive;
using Ae.ShopOrder.Service.Client.Clients.WMS;
using Ae.ShopOrder.Service.Client.Model.Order;
using Ae.ShopOrder.Service.Client.Model.Shop;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Client.Request.OrderForC;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Client.Request.Pay;
using Ae.ShopOrder.Service.Client.Request.Receive;
using Ae.ShopOrder.Service.Client.Request.WMS;
using Ae.ShopOrder.Service.Common.Exceptions;
using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Common.Template;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using Ae.ShopOrder.Service.Client.Clients.Stock;
using Ae.ShopOrder.Service.Client.Clients.Coupon;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Request.Product;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Dapper;

namespace Ae.ShopOrder.Service.Imp.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IOrderQueryForCClient _orderQueryClient;
        private readonly IOrderNotAdapterRepository orderNotAdapterRepo;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderClient _orderClient;
        private readonly IApolloErpWMSClient _ApolloErpWmsClient;
        private readonly IShopOrderRepository shopOrderRepo;
        private readonly IOrderAddressRepository orderAddressRepo;
        private readonly IOrderUserRepository _orderUserRepository;
        private readonly IOrderCarRepository orderCarRepo;
        private readonly IPayClient _payClient;
        private readonly IOrderProductRepository orderProductRepo;
        private readonly IOrderLogRepository _orderLogRepository;
        private readonly IShopClient shopClient;
        private readonly IReceiveClient receiveClient;
        private readonly IOrderDispatchRepository _orderDispatchRepository;
        private readonly IOrderCarRepository _orderCarRepository;
        private readonly IOrderVerificationCodeRepository _orderVerificationCodeRepository;
        private readonly IVerificationRuleRepository _verificationRuleRepository;
        private readonly IVerificationRuleShopIdRepository _verificationRuleShopIdRepository;
        private readonly IOrderCouponRepository _orderCouponRepository;

        private readonly IShopStockClient _shopStockClient;
        private readonly IFranchisesConfigRepository _franchisesConfigRepository;
        private readonly ICouponClient _couponClient;
        private readonly IInsuranceCompanyRepository _insuranceCompanyRepository;
        private readonly IOrderInsuranceCompanyRepository _orderInsuranceCompanyRepository;
        private readonly IVerificationRulePidRepository _verificationRulePidRepository;
        private readonly IDelegateUserOrderRepository _delegateUserOrderRepository;
        private readonly IOrderRelationRepository _orderRelationRepository;
        private readonly IOrderPackageCardRepository _orderPackageCardRepository;

        public OrderQueryService(IOrderQueryForCClient orderQueryClient, IMapper mapper,
            ApolloErpLogger<OrderQueryService> logger, IOrderRepository orderRepository, IOrderClient orderClient, IApolloErpWMSClient ApolloErpWmsClient,
            IShopOrderRepository shopOrderRepo, IOrderAddressRepository orderAddressRepo, IOrderUserRepository orderUserRepository,
            IOrderCarRepository orderCarRepo, IPayClient payClient, IOrderProductRepository orderProductRepo, IOrderLogRepository orderLogRepository,
            IOrderNotAdapterRepository orderNotAdapterRepo,
            IShopClient shopClient, IOrderPackageCardRepository orderPackageCardRepository,
            IReceiveClient receiveClient, IOrderDispatchRepository orderDispatchRepository, IOrderCarRepository orderCarRepository, IOrderVerificationCodeRepository orderVerificationCodeRepository, 
            IVerificationRuleRepository verificationRuleRepository, IVerificationRuleShopIdRepository verificationRuleShopIdRepository, IShopStockClient shopStockClient, IFranchisesConfigRepository franchisesConfigRepository, 
            IOrderCouponRepository orderCouponRepository, ICouponClient couponClient, IInsuranceCompanyRepository insuranceCompanyRepository, IOrderInsuranceCompanyRepository orderInsuranceCompanyRepository,
            IVerificationRulePidRepository verificationRulePidRepository, IDelegateUserOrderRepository delegateUserOrderRepository, IOrderRelationRepository orderRelationRepository)
        {
            _orderQueryClient = orderQueryClient;
            this.mapper = mapper;
            this.logger = logger;
            _orderRepository = orderRepository;
            _orderClient = orderClient;
            _ApolloErpWmsClient = ApolloErpWmsClient;

            this.shopOrderRepo = shopOrderRepo;
            this.orderAddressRepo = orderAddressRepo;
            _orderUserRepository = orderUserRepository;
            this.orderProductRepo = orderProductRepo;
            this.orderCarRepo = orderCarRepo;
            _payClient = payClient;
            _orderLogRepository = orderLogRepository;
            this.orderNotAdapterRepo = orderNotAdapterRepo;
            this.shopClient = shopClient;
            this.receiveClient = receiveClient;

            _orderDispatchRepository = orderDispatchRepository;
            _orderCarRepository = orderCarRepository;
            _orderVerificationCodeRepository = orderVerificationCodeRepository;
            _verificationRuleRepository = verificationRuleRepository;
            _verificationRuleShopIdRepository = verificationRuleShopIdRepository;
            _shopStockClient = shopStockClient;
            _franchisesConfigRepository = franchisesConfigRepository;
            _orderCouponRepository = orderCouponRepository;
            _couponClient = couponClient;
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _orderInsuranceCompanyRepository = orderInsuranceCompanyRepository;
            _verificationRulePidRepository = verificationRulePidRepository;
            _delegateUserOrderRepository = delegateUserOrderRepository;
            _orderRelationRepository = orderRelationRepository;
            _orderPackageCardRepository = orderPackageCardRepository;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 得到车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCar(GetOrderCarRequest request)
        {
            var mappingClientRequest = mapper.Map<GetOrderCarClientRequest>(request);

            var getOrderCarsResponse = await _orderCarRepository.GetOrderCars(request.OrderIds);

            //  var getOrderCarsResponse = await _orderQueryClient.GetOrderCar(mappingClientRequest);


            var orderCars = mapper.Map<List<OrderCarDTO>>(getOrderCarsResponse);
            return new ApiResult<List<OrderCarDTO>>()
            {
                Code = ResultCode.Success,
                Data = orderCars
            };
        }

        /// <summary>
        /// 得到订单不适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderNotAdapterInfoResponse>> GetOrderNotAdapter(GetOrderNotAdapterRequest request)
        {
            var getOrderNotAdapterDalResponse = await orderNotAdapterRepo.GetOrderNotAdapter(request);
            if (getOrderNotAdapterDalResponse != null)
            {
                var getOrderNotAdapterInfoResponse =
                    JsonConvert.DeserializeObject<GetOrderNotAdapterInfoResponse>(getOrderNotAdapterDalResponse.Content);
                getOrderNotAdapterInfoResponse.IsHistory = true;
                return new ApiResult<GetOrderNotAdapterInfoResponse>()
                {
                    Code = ResultCode.Success,
                    Data = getOrderNotAdapterInfoResponse
                };
            }

            var initResponse = JsonConvert.DeserializeObject<GetOrderNotAdapterInfoResponse>(OrderNotAdapterTemplate.InitTemplate);

            var orderId = Regex.Replace(request.OrderNo.Trim(), @"\D", "");
            long.TryParse(orderId, out var newOrderId);
            var orderIds = new List<long>();
            orderIds.Add(newOrderId);
            var orderProductResponse = await _orderClient.GetOrderProduct(new GetOrderProductRequest()
            {
                OrderNos = new List<string>() { request.OrderNo }
            });
            if (orderProductResponse.Code == ResultCode.Success && (orderProductResponse.Data != null && orderProductResponse.Data.Any()))
            {
                initResponse.OrderNo = request.OrderNo;
                initResponse.IsHistory = false;
                initResponse.ProductList = new List<ProductNotAdapterVO>();
                orderProductResponse?.Data?.ForEach(x =>
              {
                  if (x.ProductAttribute == ProductAttributeEnum.RealProduct.ToInt())
                  {
                      initResponse.ProductList.Add(
                          GetProductNotAdapterVO(x));
                  }
              });
            }


            return new ApiResult<GetOrderNotAdapterInfoResponse>()
            {
                Code = ResultCode.Success,
                Data = initResponse
            };

        }


        /// <summary>
        /// 形成不适配
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        private ProductNotAdapterVO GetProductNotAdapterVO(OrderProductDTO orderProduct)
        {
            return new ProductNotAdapterVO()
            {
                Name = orderProduct.ProductName,
                Pid = orderProduct.ProductId,
                Remark = string.Empty,
                HandleModeList = GetHandleModelListVO(),
                ProductImgList = GetProductImgListVos()
            };

        }

        private List<HandleModeListVO> GetHandleModelListVO()
        {
            return new List<HandleModeListVO>()
            {
                new HandleModeListVO()
                {
                    Name = HandleModelListEnum.OrderCancel.GetDescription(),
                    Code = HandleModelListEnum.OrderCancel.ToString(),
                    Check = false
                },
                new HandleModeListVO()
                {
                    Name = HandleModelListEnum.ShopProductReplace.GetDescription(),
                    Code = HandleModelListEnum.ShopProductReplace.ToString(),
                    Check = false
                },
                new HandleModeListVO()
                {
                    Name = HandleModelListEnum.Other.GetDescription(),
                    Code = HandleModelListEnum.Other.ToString(),
                    Check = false
                },
            };
        }

        private List<ProductImgListVO> GetProductImgListVos()
        {
            return new List<ProductImgListVO>()
            {
                new ProductImgListVO()
                {
                    Name = ProductListEnum.New.GetDescription(),
                    Code = ProductListEnum.New.GetEnumDescription(),
                    ExampleImgUrl = "",
                    ImgList = new List<ImgVo>()
                    {
                        new ImgVo()
                        {
                            Url = ""
                        }
                    }
                },
                new ProductImgListVO()
                {
                    Name = ProductListEnum.Old.GetDescription(),
                    Code = ProductListEnum.Old.GetEnumDescription(),
                    ExampleImgUrl = "",
                    ImgList = new List<ImgVo>()
                    {
                        new ImgVo()
                        {
                            Url = ""
                        }
                    }
                },
                new ProductImgListVO()
                {
                    Name = ProductListEnum.NewOldComparison.GetDescription(),
                    Code = ProductListEnum.NewOldComparison.GetEnumDescription(),
                    ExampleImgUrl = "",
                    ImgList = new List<ImgVo>()
                    {
                        new ImgVo()
                        {
                            Url = ""
                        }
                    }
                }
            };
        }

        /// <summary>
        /// 得到移库单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetWareHouseTransferResponse>> GetWarehouseTransferAllTask(GetWareHouseTransferRequest request)
        {
            var clientResponse = mapper.Map<GetWareHouseTransferClientRequest>(request);
            var getWarehouseTransferAllTask = await _ApolloErpWmsClient.GetWarehouseTransferAllTask(clientResponse);
            ApiResult<GetWareHouseTransferResponse> response =
                mapper.Map<ApiResult<GetWareHouseTransferResponse>>(getWarehouseTransferAllTask);
            return response;
        }

        /// <summary>
        /// 批量的查询物流接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>> GetBatchWarehouseTransferPackages(GetBatchWarehouseTransferPackagesRequest request)
        {
            return await _ApolloErpWmsClient.GetBatchWarehouseTransferPackages(request);
        }




        /// <summary>
        /// 获取BOSS订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss(GetOrderDetailForBossRequest request)
        {
            var result = Result.Failed<GetOrderDetailForBossResponse>("加载异常，请重试");
            try
            {
                var response = new GetOrderDetailForBossResponse();

                var order = await shopOrderRepo.GetOrder(request.OrderNo);

                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                response.OrderInfo = mapper.Map<OrderDetailForBossOrderInfoDto>(order);

                response.FinanceInfo = mapper.Map<OrderDetailForBossFinanceInfoDto>(order);
                response.AmountInfo = mapper.Map<OrderDetailForBossAmountInfoDto>(order);

                var user = await _orderUserRepository.GetOrderUser(order.Id);
                if (user == null)
                {
                    // throw new CustomException("用户信息异常");
                }
                response.UserInfo = mapper.Map<OrderDetailForBossUserInfoDto>(user);

                if (response.UserInfo != null)
                {
                    var rReq = new GetReserveInfoByReserveIdOrOrderNum
                    {
                        OrderNumbers = new List<string>()
                    };
                    rReq.OrderNumbers.Add(order.OrderNo);
                    response.ReserverInfo = new OrderDetailForBossReserverInfoDto();
                    response.ReserverInfo.ReserveId = (await receiveClient.GetReserveInfoByReserveIdOrOrderNum(rReq))?
                            .FirstOrDefault(f => f.OrderNo.Equals(order.OrderNo))?.ReserveId
                            ?? default;
                    response.ReserverInfo.ReserverTime = (await receiveClient.GetShopReserveDO(new ReserveDetailRequest() { ReserveId = response.ReserverInfo.ReserveId }))?.Data?.ReserveTime.ToString() ?? string.Empty;

                }

                if (response.OrderInfo?.ShopId > 0)
                {
                    var shopInfo = await shopClient.GetShopById(new GetShopRequest()
                    {
                        ShopId = response.OrderInfo.ShopId
                    });
                    response.OrderInfo.ShopName = shopInfo?.Data?.SimpleName;
                }

                var orderDispatchs = await _orderDispatchRepository.GetOrderDispatch(new GetOrderDispatchRequest()
                {
                    OrderNos = new List<string>(1) { order.OrderNo }
                });

                var refOrder = await (_orderRelationRepository.GetListAsync(" where is_deleted=0 and order_no = @OrderNO", new { OrderNO = order.OrderNo }));
                response.OrderInfo.RefOrderNo = refOrder?.FirstOrDefault()?.ReferOrder ?? "";

                if (orderDispatchs != null && orderDispatchs?.Count > 0)
                {
                    var orderDispatchInfos = mapper.Map<List<OrderDispatchDTO>>(orderDispatchs);
                    response.OrderDispatchInfo = orderDispatchInfos?.FirstOrDefault();
                    var techName = "";
                    orderDispatchInfos?.ForEach(_ =>
                    {
                        techName += _.TechName +"-" + _.Percent.ToString() + "、";
                    });
                    if (response.OrderDispatchInfo != null)
                    {
                        response.OrderDispatchInfo.TechName = techName.Length > 0 ? techName.Substring(0, techName.Length - 1) : "";
                    }
                }

                var address = await orderAddressRepo.GetOrderAddress(order.Id);
                if (address == null)
                {
                    // throw new CustomException("地址信息异常");
                }
                if (address != null)
                {
                    response.UserInfo.DisplayContactAddress = $"{address.Province}{address.City}{address.District}{address.DetailAddress}";
                    response.UserInfo.ProvinceId = address.ProvinceId;
                    response.UserInfo.CityId = address.CityId;
                    response.UserInfo.DistrictId = address.DistrictId;
                }

                if (response.UserInfo != null)
                {
                    response.UserInfo.UserName = order.UserName;
                    response.UserInfo.UserTel = order.UserPhone;
                }


                var car = await orderCarRepo.GetOrderCar(order.Id);
                //if (car == null)
                //{
                //    throw new CustomException("车辆信息异常");
                //}
                if (car != null)
                {
                    response.UserInfo.CarId = car?.CarId;
                    response.UserInfo.CarNumber = car?.CarNumber;
                    response.UserInfo.DisplayCarName = $"{car?.Brand} {car?.Vehicle} {car?.Nian} {car?.PaiLiang} {car?.SalesName}";//格式：品牌Brand 车系Vehicle 年份Nian 排量PaiLiang 款型SalesName
                    response.UserInfo.TotalMileage = car?.TotalMileage ?? 0;
                    response.UserInfo.VinCode = car?.VinCode;
                }

                var getPaysByOrderNoResult = await _payClient.GetPaysByOrderNo(new GetPaysByOrderNoRequest() { OrderNo = request.OrderNo });
                var pays = getPaysByOrderNoResult.GetSuccessData();
                if (pays != null && pays.Any())
                {
                    var pay = pays?.Where(_ => _.Status == 2)?.FirstOrDefault();
                    if (pay != null)
                    {
                        response.FinanceInfo.PayNo = pay.PayNo;
                        response.FinanceInfo.TradeNo =
                            !string.IsNullOrEmpty(pay.TradeNo) ? pay.TradeNo : pay.PlatformTradeNo;
                        response.FinanceInfo.BuyerAccount = pay.BuyerAccount;
                    }
                }
                else
                {
                    if(order.Channel== ChannelEnum.MeiTuan.ToInt())
                    {
                        response.FinanceInfo.PayChannel = 4;
                        response.FinanceInfo.TradeNo = refOrder?.FirstOrDefault()?.ReferTransferNo;
                    }
                }

                var products = await orderProductRepo.GetOrderProducts(order.Id);
                if (products == null || !products.Any())
                {
                    throw new CustomException("商品信息异常");
                }
                var productList = new List<OrderDetailForBossPackageProductDto>();
                foreach (var item in products)
                {
                    if (item.ParentOrderPackagePid == 0)
                    {
                        var product = mapper.Map<OrderDetailForBossPackageProductDto>(item);
                        var childProducts = products.Where(_ => _.ParentOrderPackagePid == item.Id)?.ToList();
                        if (childProducts != null && childProducts.Any())
                            product.Children.AddRange(mapper.Map<List<OrderDetailForBossProductDto>>(childProducts));
                        productList.Add(product);
                    }
                }
                var pids = products?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToSbyte())?.Select(_ => _.ProductId)?.ToList();

                if (pids != null && pids.Any() && (order.StockStatus == 0 || order.StockStatus == 1))
                {

                    var stockProducts = await _shopStockClient.GetTransferProductStocks(new Core.Request.Stock.GetShopProductStocksRequest()
                    {
                        Pids = pids,
                        ShopId = order.ShopId
                    });
                    stockProducts?.Data?.ForEach(_ =>
                    {
                        var product = productList?.Where(item => item.ProductId == _.ProductId)?.FirstOrDefault();
                        if (product != null)
                            product.EnoughStock = (_.StockNum - product.TotalNumber) > 0;

                        var childProduct = productList?.SelectMany(item => item.Children)?.Where(item => item.ProductId == _.ProductId)?.FirstOrDefault();
                        if (childProduct != null)
                            childProduct.EnoughStock = (_.StockNum - childProduct.TotalNumber) > 0;
                    });
                }

                productList?.ForEach(_ =>
                {
                    if (_.ProductAttribute != ProductAttributeEnum.PackageProduct.ToSbyte() && _.ProductAttribute != ProductAttributeEnum.RealProduct.ToSbyte())
                    {
                        _.EnoughStock = true;
                    }
                    _?.Children?.ForEach(item =>
                    {
                        if (item.ProductAttribute != ProductAttributeEnum.PackageProduct.ToSbyte() && item.ProductAttribute != ProductAttributeEnum.RealProduct.ToSbyte())
                        {
                            item.EnoughStock = true;
                        }
                    });
                    if (_.ProductAttribute == ProductAttributeEnum.PackageProduct.ToInt())
                    {
                        bool packStockIsEnough = true;
                        _?.Children?.ForEach(item =>
                        {
                            if (!item.EnoughStock)
                            {
                                packStockIsEnough = false;
                            }
                        });
                        _.EnoughStock = packStockIsEnough;
                    }
                });

                response.ProductInfo = new OrderDetailForBossProductInfoDto() { Products = productList };

                var orderCoupon = await _orderCouponRepository.GetOrderCoupon(order.Id);

                if (orderCoupon != null)
                {
                    var couponInfo = await _couponClient.GetUserCouponEntityCustomById(new Client.Request.Coupon.UserCouponEntityReqByIdRequest()
                    {
                        UserCouponId = orderCoupon.UserCouponId
                    });

                    var orderCouponInfo = mapper.Map<OrderCouponInformationDto>(couponInfo.Data);

                    response.OrderCouponInfo = orderCouponInfo;
                }

                var insuranceCompanyDOs = await _orderInsuranceCompanyRepository.GetListAsync(" where is_deleted=0 and order_no=@OrderNo", new { OrderNo = request.OrderNo }, true);

                var insuranceCompanyData = mapper.Map<OrderInsuranceCompanyDTO>(insuranceCompanyDOs.FirstOrDefault());

                if (insuranceCompanyDOs != null && insuranceCompanyDOs.Any())
                    response.OrderInsuranceCompanyInfo = insuranceCompanyData;

                result = Result.Success(response, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderDetailForBossEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 订单日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListRequest request)
        {
            var result = Result.Success(new List<OrderLogDTO>(), 0);
            try
            {
                if (!request.OrderNo.StartsWith("RGB"))
                {
                    throw new CustomException("订单号异常");
                }
                var order = await shopOrderRepo.GetOrder(request.OrderNo);
                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                var orderId = order.Id;
                var pageList = await _orderLogRepository.GetListPagedAsync(request.PageIndex, request.PageSize, "where order_id = @OrderId", "id desc", new { OrderId = orderId });
                if (pageList != null)
                {
                    var response = mapper.Map<List<OrderLogDTO>>(pageList.Items);
                    result = Result.Success(response, pageList.TotalItems);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderLogListEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetailForMiniApp(GetOrderDetailRequest req)
        {
            var res = Result.Failed<GetOrderDetailResponse>("加载异常，请重试");
            try
            {
                var resp = new GetOrderDetailResponse();

                //摘要信息
                var order = await shopOrderRepo.GetOrder(req.OrderNo);
                if (order == null)
                {
                    res = Result.Failed<GetOrderDetailResponse>(ResultCode.SUCCESS_NOT_EXIST);
                    return res;
                }
                var (sum, subSum) = GetOrderSummary(order);
                resp.Summary = sum;
                resp.SubSummary = subSum;

                //配送信息（个人地址或门店地址）
                resp.IsNeedDelivery = order.IsNeedDelivery;
                resp.DeliveryType = order.DeliveryType;
                resp.UserName = order.UserName;
                resp.UserPhone = order.UserPhone;
                ShopDTO shop = null;
                if (order.ShopId > 0)
                {
                    //查询门店详情
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = order.ShopId });
                    shop = getShopResult?.GetSuccessData();
                }
                var address = await orderAddressRepo.GetOrderAddress(order.Id);
                resp.ShopId = order.ShopId;
                if (shop != null)
                {
                    resp.ShopName = shop.SimpleName;
                    resp.ShopAddress = $"{shop.Province}{shop.City}{shop.District}{shop.Address}";
                    resp.Longitude = shop.Longitude;
                    resp.Latitude = shop.Latitude;
                    resp.ShopPhone = shop.Mobile;
                }
                if (address != null)
                {
                    if (order.ProduceType == ProductTypeEnum.DoorToDoor.ToSbyte())
                    {

                        resp.ShopAddress = $"{address?.Province}{address?.City}{address?.District}{address?.DetailAddress}";
                        resp.Longitude = 0;
                        resp.Latitude = 0;
                        resp.ShopPhone = string.Empty;
                    }
                    resp.ReceiverName = address.ReceiverName;
                    resp.ReceiverPhone = address.ReceiverPhone;
                }

                //车辆信息
                var car = await orderCarRepo.GetOrderCar(order.Id);
                if (car != null)
                {
                    resp.DisplayVehicleName = $"{car.Brand} - {car.Vehicle}";
                    resp.DisplayModelName = $"{car.Nian} - {car.PaiLiang} - {car.SalesName}";
                    resp.CarId = car.CarId;
                }

                resp.ContactName = order.ContactName;
                resp.ContactPhone = order.ContactPhone;

                //商品信息
                var getOrderDetailPackageProductServicesResponse = await GetOrderDetailPackageProductServices(order.Id);
                resp.Products = getOrderDetailPackageProductServicesResponse.Products;
                resp.Services = getOrderDetailPackageProductServicesResponse.Services;

                //主信息
                var rReq = new GetReserveInfoByReserveIdOrOrderNum
                {
                    OrderNumbers = new List<string>()
                };
                rReq.OrderNumbers.Add(order.OrderNo);
                resp.ReserveId = (await receiveClient.GetReserveInfoByReserveIdOrOrderNum(rReq))?
                                 .FirstOrDefault(f => f.OrderNo.Equals(order.OrderNo))?.ReserveId
                                 ?? default;
                resp.ReserverTime = (await receiveClient.GetShopReserveDO(new ReserveDetailRequest() { ReserveId = resp.ReserveId }))?.Data?.ReserveTime.ToString() ?? string.Empty;

                var orderDispatchDO = (await _orderDispatchRepository.GetOrderDispatch(new GetOrderDispatchRequest()
                {
                    OrderNos = new List<string>(1) { order.OrderNo }
                })).FirstOrDefault();

                resp.OrderDispatch = mapper.Map<OrderDispatchDTO>(orderDispatchDO);

                if (resp.OrderDispatch != null)
                {
                    var getTechnicians = await shopClient.GetTechnicianPage(new GetTechnicianPageRequest()
                    {
                        EmployeeId = resp.OrderDispatch?.TechId ?? string.Empty,
                        ShopId = order.ShopId,
                        PageIndex = 1,
                        PageSize = 1
                    });
                    resp.OrderDispatch.Telephone = getTechnicians?.FirstOrDefault()?.Mobile ?? string.Empty;
                }

                resp.OrderNo = order.OrderNo;
                resp.OrderChannel = (ChannelEnum)order.Channel;
                resp.OrderType = (OrderTypeEnum)order.OrderType;
                resp.OrderTime = order.OrderTime;
                resp.DisplayOrderStatus = ((OrderStatusEnum)order.OrderStatus).GetDescription();
                switch (resp.OrderChannel)
                {
                    case ChannelEnum.None:
                        break;
                    case ChannelEnum.ApolloErpToC:
                        if (order.TerminalType == 1)
                        {
                            resp.DisplayOrderChannel = "微信小程序";
                        }
                        else if (order.TerminalType == 2)
                        {
                            resp.DisplayOrderChannel = "合作店";
                        }
                        break;
                    case ChannelEnum.ApolloErpToShop:
                        resp.DisplayOrderChannel = "合作店";
                        break;
                    default:
                        break;
                }
                resp.DisplayServiceCategory = ((OrderTypeEnum)order.OrderType).GetDescription();
                switch (order.PayChannel)
                {
                    case 1:
                        resp.DisplayPayMethod = "微信支付";
                        break;
                    case 2:
                        resp.DisplayPayMethod = "支付宝";
                        break;
                    default:
                        if (order.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
                        {
                            resp.DisplayPayMethod = "上门支付";
                        }
                        else
                        {
                            resp.DisplayPayMethod = "到店支付";
                        }
                        break;
                }
                //resp.DisplayDeliveryMethod = order.DeliveryType == 1 ? "送货到店" : "送货到家";

                resp.DisplayDeliveryMethod = string.Empty;

                //金额信息
                resp.ProductAmount = order.TotalProductAmount;
                resp.ServiceAmount = order.ServiceFee;
                resp.DeliveryFee = order.DeliveryFee;
                resp.TotalCouponAmount = order.TotalCouponAmount;
                resp.ActualAmount = order.ActualAmount;

                //操作按钮
                resp.OrderUserOperations = GetOrderUserOperations(order);

                //安装码
                ShopConfigDTO shopConfig = null;
                if (!string.IsNullOrWhiteSpace(order.InstallCode) && order.ProduceType != ProductTypeEnum.DoorToDoor.ToSbyte())
                {
                    var orderInstallCodeInfo = new OrderInstallCodeInfoDTO();
                    if (order.ShopId > 0)
                    {
                        var getShopConfigByShopIdResult = await shopClient.GetShopConfigByShopId(new GetShopRequest() { ShopId = order.ShopId });
                        shopConfig = getShopConfigByShopIdResult.GetSuccessData();
                        if (shopConfig != null)
                        {
                            orderInstallCodeInfo = new OrderInstallCodeInfoDTO()
                            {
                                StartUseBussinessTime = shopConfig.StartWorkTime.ToString("HH:mm"),
                                EndUseBussinessTime = shopConfig.EndWorkTime.ToString("HH:mm"),
                                ShopId = order.ShopId,
                                ShopName = shop.SimpleName,
                                ShopAddress = $"{shop.Province}{shop.City}{shop.District}{shop.Address}"
                            };
                        }
                    }
                    orderInstallCodeInfo.Code = order.InstallCode;
                    resp.OrderInstallCodeInfos = new List<OrderInstallCodeInfoDTO>();
                    resp.OrderInstallCodeInfos.Add(orderInstallCodeInfo);
                }

                //核销码
                var orderVerificationCodes = await _orderVerificationCodeRepository.GetListByUserIdAndOrderNo(req.UserId, req.OrderNo);
                if (orderVerificationCodes != null && orderVerificationCodes.Any())
                {
                    var orderVerificationCode = orderVerificationCodes.OrderBy(_ => _.Status).FirstOrDefault();
                    if (orderVerificationCode != null)
                    {
                        resp.OrderVerificationCodeInfos = new List<OrderVerificationCodeInfoDTO>()
                        {
                            new OrderVerificationCodeInfoDTO()
                            {
                                Code=orderVerificationCode.Code,
                                Status=orderVerificationCode.Status,
                                VerificationRuleInfo=await GetVerificationRuleInfo(orderVerificationCode)
                            }
                        };
                    }
                }

                resp.ProductType = order.ProduceType;

                if (order.ProduceType == ProductTypeEnum.DoorToDoor.ToSbyte())
                {
                    resp.DisplayOrderType = $"上门{((OrderTypeEnum)order.OrderType).GetDescription()}服务";
                }
                else
                {
                    resp.DisplayOrderType = $"配送到门店";
                }

                var getShopArrivalVideos = await receiveClient.GetShopArrivalVideoForOrder(new GetShopArrivalVideoForOrderRequest()
                {
                    OrderNo = req.OrderNo
                });

                resp.OrderVideos = getShopArrivalVideos?.Data;

                res = Result.Success(resp, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderDetailEx", ex);
            }
            return res;
        }

        /// <summary>
        /// 根据订单号获取订单详情页套餐单品服务信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<GetOrderDetailPackageProductServicesResponse> GetOrderDetailPackageProductServices(long orderId)
        {
            var response = new GetOrderDetailPackageProductServicesResponse();
            var responseProducts = new List<OrderDetailPackageProductDTO>();
            var responseServices = new List<OrderDetailProductDTO>();

            var products = await orderProductRepo.GetOrderProducts(orderId);
            if (products == null || !products.Any())
            {
                return response;
            }

            var packageProducts = products.FindAll(_ => _.ProductAttribute == 1);//套餐集合

            var packageChildrenProducts = products.FindAll(_ => _.ParentOrderPackagePid > 0);//套餐子商品集合
            var singleProducts = products.FindAll(_ => _.ProductAttribute == 0 && _.ParentOrderPackagePid == 0);//单品集合
            var singelServiceProducts = products.FindAll(_ => _.ProductAttribute == 2 && _.ParentOrderPackagePid == 0);//单服务集合

            var shopPurchase = products.FindAll(_ => _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToSbyte());//外采

            if (packageProducts != null && packageProducts.Any()
                && packageChildrenProducts != null && packageChildrenProducts.Any())
            {
                foreach (var item in packageProducts)
                {
                    var responseProduct = new OrderDetailPackageProductDTO()
                    {
                        PackageOrProduct = mapper.Map<OrderDetailProductDTO>(item)
                    };
                    var findChildrenProducts = packageChildrenProducts.FindAll(_ => _.ParentOrderPackagePid == item.Id);
                    if (findChildrenProducts != null && findChildrenProducts.Any())
                    {
                        responseProduct.PackageProducts = mapper.Map<List<OrderDetailProductDTO>>(findChildrenProducts);
                    }
                    responseProducts.Add(responseProduct);
                }
            }

            if (singleProducts != null && singleProducts.Any())
            {
                foreach (var item in singleProducts)
                {
                    var responseProduct = new OrderDetailPackageProductDTO()
                    {
                        PackageOrProduct = mapper.Map<OrderDetailProductDTO>(item)
                    };
                    responseProducts.Add(responseProduct);
                }
            }

            if (shopPurchase != null && shopPurchase.Any())
            {
                foreach (var item in shopPurchase)
                {
                    var responseProduct = new OrderDetailPackageProductDTO()
                    {
                        PackageOrProduct = mapper.Map<OrderDetailProductDTO>(item)
                    };
                    responseProducts.Add(responseProduct);
                }
            }


            if (singelServiceProducts != null && singelServiceProducts.Any())
            {
                foreach (var item in singelServiceProducts)
                {
                    var responseService = mapper.Map<OrderDetailProductDTO>(item);
                    responseServices.Add(responseService);
                }
            }

            response.Products = responseProducts;
            response.Services = responseServices;

            return response;
        }

        /// <summary>
        /// 根据订单核销码信息获取使用规则信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private async Task<VerificationRuleInfoDTO> GetVerificationRuleInfo(OrderVerificationCodeDO code)
        {
            var rule = await _verificationRuleRepository.GetAsync(code.RuleId);
            if (rule == null)
            {
                return null;
            }

            var shopIds = new List<long>();
            if (rule.IsByShopId == 1)
            {
                var ruleShopIds = await _verificationRuleShopIdRepository.GetListAsync("where rule_id=@RuleId", new { code.RuleId });
                if (ruleShopIds != null)
                {
                    foreach (var item in ruleShopIds)
                    {
                        if (!shopIds.Contains(item.ShopId))
                        {
                            shopIds.Add(item.ShopId);
                        }
                    }
                }
            }
            if (rule.IsByShopType == 1)
            {
                var getShopListResult = await shopClient.GetShopListAsync(new GetShopListRequest()
                {
                    ShopType = rule.ShopType,
                    PageIndex = 1,
                    PageSize = int.MaxValue
                });
                var shopList = getShopListResult.GetSuccessData();
                if (shopList != null)
                {
                    foreach (var item in shopList.Items)
                    {
                        if (!shopIds.Contains(item.Id))
                        {
                            shopIds.Add(item.Id);
                        }
                    }
                }
            }
            var ruleInfo = new VerificationRuleInfoDTO()
            {
                DisplayCodeName = "核销码",
                EndValidTime = code.EndValidTime,
                ShopIds = shopIds
            };

            return ruleInfo;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        /// <summary>
        /// 获取订单摘要信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static (string sum, string subSum) GetOrderSummary(OrderDO order)
        {
            var sum = string.Empty;
            var subSum = string.Empty;

            switch ((OrderStatusEnum)order.OrderStatus)
            {
                case OrderStatusEnum.Submitted:
                    //if (order.PayStatus == 0)
                    //{
                    //    if (order.PayMethod == 1)
                    //    {
                    //        sum = "等待客人支付";
                    //        var timeLeft = order.OrderTime.AddHours(24) - DateTime.Now;
                    //        subSum = $"超过{timeLeft.Hours}小时{timeLeft.Minutes}分钟订单将自动取消";
                    //    }
                    //}
                    //else
                    //{
                    //    sum = "等待客服审核";
                    //    subSum = "未审核通过订单将取消";
                    //}

                    if (order.PayMethod == 1)
                    {
                        sum = "等待客人支付";
                        var timeLeft = order.OrderTime.AddHours(24) - DateTime.Now;
                        subSum = $"超过{timeLeft.Hours}小时{timeLeft.Minutes}分钟订单将自动取消";
                    }
                    else
                    {
                        sum = "等待客服审核";
                        subSum = "未审核通过订单将取消";
                    }
                    break;
                case OrderStatusEnum.Confirmed:
                    if (order.ProduceType == 1)
                    {
                        sum = "订单待安装";
                        subSum = "等待客人开车前往门店体验服务";
                    }
                    else if (order.ProduceType == ProductTypeEnum.DoorToDoor.ToSbyte())
                    {
                        sum = "订单待收货";
                        subSum = "等待技师开车前往约定地点体验服务";
                    }
                    else if (order.DeliveryType == 1)
                    {
                        if (order.DeliveryStatus == 1)
                        {
                            if (order.SignStatus == 0)
                            {
                                sum = "订单待收货";
                                subSum = "货品到店后会及时通知客人";
                            }
                            else
                            {
                                sum = "订单待安装";
                                subSum = "等待客人开车前往门店体验服务";
                            }
                        }
                        else
                        {
                            sum = "等待仓库发货";
                            subSum = "到货后会及时和客人预约到店服务时间";
                        }
                    }//TODO: 暂无到家，待开放配送到家需要新增文案
                    break;
                case OrderStatusEnum.Completed:
                    if (order.CommentStatus == 0)
                    {
                        sum = "订单已完成";
                        subSum = "等待客人点评";
                    }
                    else
                    {
                        sum = "订单已点评";
                        subSum = "期待可以再次为您提供服务";
                    }
                    break;
                case OrderStatusEnum.Canceled:
                    sum = "订单已取消";
                    subSum = "期待可以再次为您提供服务";
                    break;
                default:
                    break;
            }

            //switch ((OrderStatusEnum)order.OrderStatus)
            //{
            //    case OrderStatusEnum.Confirmed:
            //        sum = "订单待安装";
            //        subSum = "等待客人开车前往门店体验服务";
            //        break;
            //    case OrderStatusEnum.Completed:
            //        sum = "订单已完成";
            //        subSum = "等待客人开车前往门店体验服务";
            //        break;
            //    case OrderStatusEnum.Canceled:
            //        sum = "订单已取消";
            //        subSum = "期待可以再次为您提供服务";
            //        break;
            //    default:
            //        break;
            //}

            return (sum, subSum);
        }

        /// <summary>
        /// 获取订单用户可操作按钮
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private List<OrderUserOperationDTO> GetOrderUserOperations(OrderDO order)
        {
            var list = new List<OrderUserOperationDTO>();

            switch ((OrderStatusEnum)order.OrderStatus)
            {
                case OrderStatusEnum.Submitted:
                    if (order.PayStatus == 0)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.CancelOrder, IsImportance = false, Sort = 1 });
                        if (order.PayMethod == 1)
                        {
                            list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.PayOrder, IsImportance = true, Sort = 2 });
                        }
                    }
                    if (order.ReserveStatus == 0)
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    break;
                case OrderStatusEnum.Confirmed:
                    //if (order.ProduceType == 1)
                    //{
                    //    list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    //}
                    //else if (order.DeliveryType == 1)
                    //{
                    //    if (order.DeliveryStatus == 1)
                    //    {
                    //        if (order.SignStatus == 0)
                    //        {
                    //            //list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ConfirmReceive, IsImportance = true, Sort = 1 });
                    //        }
                    //        else
                    //        {
                    //            list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    //        }
                    //    }
                    //}
                    if (order.ProduceType == 1)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    }
                    else
                    {
                        if (order.ReserveStatus == 0)
                            list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    }
                    break;
                case OrderStatusEnum.Completed:
                    if (order.CommentStatus == 0)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.CommentOrder, IsImportance = true, Sort = 1 });
                    }
                    else if (order.CommentStatus == 1)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.AppendComment, IsImportance = true, Sort = 1 });
                    }
                    //if (list.Any())
                    //{
                    //    list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.BuyAgain, IsImportance = true, Sort = 2 });
                    //}
                    //else
                    //{
                    //    list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.BuyAgain, IsImportance = true, Sort = 1 });
                    //}
                    break;
                case OrderStatusEnum.Canceled:
                    //list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.BuyAgain, IsImportance = true, Sort = 1 });
                    break;
                default:
                    break;
            }
            if (order.OrderStatus < (sbyte)OrderStatusEnum.Canceled && order.IsOccurReverse == 1)
            {
                if (order.PayStatus == 1)
                {
                    if (order.RefundStatus == 0)
                    {
                        list = new List<OrderUserOperationDTO>();
                    }
                }
            }

            //switch ((OrderStatusEnum)order.OrderStatus)
            //{
            //    case OrderStatusEnum.Submitted:
            //        break;
            //    case OrderStatusEnum.Confirmed:
            //        if (order.PayStatus == Convert.ToInt32(PayStatusEnum.NotPay))
            //        {
            //            list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.AgainReserve, Sort = 4 });
            //            list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.CheckReserve, Sort = 3 });
            //            list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.CancelOrder, Sort = 2 });
            //            list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.PayOrder, Sort = 1 });
            //        }
            //        else if (order.PayStatus == Convert.ToInt32(PayStatusEnum.HavePay))
            //        {
            //            list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.CheckReserve, Sort = 1 });
            //        }
            //        break;
            //    case OrderStatusEnum.Completed:
            //        list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.CheckReserve, Sort = 1 });
            //        break;
            //    case OrderStatusEnum.Canceled:
            //        list.Add(new OrderUserOperationDTO { Function = OrderUserOperationEnum.AgainReserve, Sort = 1 });
            //        break;
            //}

            return list;
        }


        /// <summary>
        /// 得到派工订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request)
        {
            var getDispatchOrders = await _orderDispatchRepository.GetOrderDispatch(request);

            var orderDispatchDTO = mapper.Map<List<OrderDispatchDTO>>(getDispatchOrders);

            return new ApiResult<List<OrderDispatchDTO>>()
            {
                Code = ResultCode.Success,
                Data = orderDispatchDTO
            };
        }

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians(GetDispatchTechniciansRequest request)
        {
            var getTechnicians = await shopClient.GetShopEmployeeByJobIdPage(new Core.Request.Arrival.GetShopEmployeeByJobIdPageRequest()
            {
                EmployeeId = request.UserId,
                ShopId = request.ShopId,
                JobId = new List<int> { 1,2, 3,4,5,6,7,8,9 },
                PageIndex = 1,
                PageSize = 100
            });
            var getDispatchTechnicians = new List<GetDispatchTechniciansResponse>();
            getTechnicians?.ForEach(_ =>
            {
                int.TryParse(_.Level, out int level);

                getDispatchTechnicians.Add(new GetDispatchTechniciansResponse()
                {
                    UserId = _.Id,
                    Name = _.Name,
                    Level = _.JobName.Equals("店长") ? "店长" :
                    _.JobName + "-" + _.WorkKindName + "-" + ((TechnicianLevelEnum)level).GetDescription()
                }); ;
            });
            return new ApiResult<List<GetDispatchTechniciansResponse>>()
            {
                Code = ResultCode.Success,
                Data = getDispatchTechnicians
            };
        }

        /// <summary>
        /// 判断是否唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayControl(CheckIsNeedPayControlRequest request)
        {
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.OrderNo.FirstOrDefault() }
                //ShopId = request.ShopId
            });

            if ((getOrderBase?.Data?.Count ?? 0) == 0)
            {
                return new ApiResult<bool>()
                {
                    Code = ResultCode.Failed,
                    Data = true,
                    Message = "订单不存在"
                };
            }

            var checkIsNeedPay = getOrderBase?.Data?.Exists(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt() 
                && _.ActualAmount > 0 && _.ProduceType != BuyProductTypeEnum.UsePackageCard.ToSbyte()) ?? false;

            return new ApiResult<bool>()
            {
                Code = ResultCode.Success,
                Data = checkIsNeedPay
            };
        }

        /// <summary>
        /// 判断是否唤醒合并支付收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayMergeControl(CheckIsNeedPayMergeControlRequest request)
        {
            if (request == null || request.OrderNo == null || request.OrderNo.Count <= 1)
            {
                return Result.Failed<bool>("参数不能为空或必须选择1个以上订单进行合并支付");
            }

            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.OrderNo
            });

            if ((getOrderBase?.Data?.Count ?? 0) == 0)
            {
                return new ApiResult<bool>()
                {
                    Code = ResultCode.Failed,
                    Data = false,
                    Message = "订单不存在"
                };
            }
            string payOrderNo = "";
            decimal actualAmountSum = 0;
            foreach (var i in getOrderBase.Data)
            {
                if (i.PayStatus != PayStatusEnum.NotPay.ToInt() || i.SignStatus != 1 || i.DispatchStatus != 1)
                {
                    payOrderNo = payOrderNo + i.OrderNo + ";";
                }
                actualAmountSum = actualAmountSum + i.ActualAmount;
            }
            if (payOrderNo != "")
            {
                return Result.Failed<bool>($"订单:{payOrderNo}不符合支付条件，请检查是否已签收已派工");
            }
            if (actualAmountSum == 0)
            {
                return Result.Failed<bool>("支付总金额为0元，不需要唤起收银台");
            }

            return new ApiResult<bool>()
            {
                Code = ResultCode.Success,
                Data = true
            };
        }

        /// <summary>
        /// 支付列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayListResponse>> PayList(PayRequest request)
        {
            var requestOrderNos = new List<string>() { request.OrderNo };
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = requestOrderNos,
                ShopId = request.ShopId
            });

            var getOrderProducts = await _orderClient.GetOrderProduct(new GetOrderProductRequest()
            {
                OrderNos = requestOrderNos
            });

            PayListResponse payListResponse = new PayListResponse();
            payListResponse.Items = new List<PayListVo>();
            var products = new List<PayListProductVo>();
            getOrderBase?.Data.ForEach(item =>
            {
                payListResponse.Items.Add(new PayListVo()
                {
                    OrderNo = item.OrderNo,
                    PayStatus = item?.PayStatus == PayStatusEnum.HavePay.ToInt() ? true : false,
                    ShowOrderStatus = item.PayStatus == PayStatusEnum.HavePay.ToInt() ?
                    PayStatusEnum.HavePay.GetDescription() : PayStatusEnum.NotPay.GetDescription(),
                    OrderType = item.OrderType.ToString(),

                });
            });

            getOrderProducts?.Data?.ForEach(item =>
            {
                if (item.ParentOrderPackagePid == 0)
                {
                    products.Add(new PayListProductVo()
                    {
                        Name = item.ProductName,
                        Num = item.TotalNumber,
                        Price = $"￥{item.Price.ToString("F2")}"
                    });
                }

            });

            payListResponse.SumPrice = getOrderBase?.Data.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";

            payListResponse.WaitingPayPrice = getOrderBase?.Data?.
                Where(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt())?.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";


            return new ApiResult<PayListResponse>()
            {
                Code = ResultCode.Success,
                Data = payListResponse
            };



        }

        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(GetOrderDetailStaticReportRequest request)
        {
            ApiPagedResult<GetOrderDetailStaticReportResponse> result =
                new ApiPagedResult<GetOrderDetailStaticReportResponse>()
                {
                    Code = ResultCode.Success,
                    Data = new ApiPagedResultData<GetOrderDetailStaticReportResponse>()
                    {
                        TotalItems = 0,
                        Items = new List<GetOrderDetailStaticReportResponse>(20)
                    }
                };

            var getOrders = await _orderRepository.GetOrderStaticReport(request);

            getOrders?.Items?.ToList()?.ForEach(_ =>
            {
                result.Data.Items.Add(new GetOrderDetailStaticReportResponse()
                {
                    OrderId = _.Id,
                    OrderTime = _.OrderTime.ToString("yyyy-MM-dd"),
                    OrderNo = _.OrderNo,
                    OrderStatus = ((OrderStatusEnum)_.OrderStatus).GetDescription(),
                    ContactName = string.IsNullOrEmpty(_.UserName) ? _.ContactName : _.UserName,
                    ContactPhone = string.IsNullOrEmpty(_.UserPhone) ? _.ContactPhone : _.UserPhone,
                    ShopId = _.ShopId,
                    CheckName = "",
                    OrderAmount = (_.TotalProductAmount + _.ServiceFee).ToString("C"),
                    ActualAmount = _.ActualAmount.ToString("C"),
                    DiscountContent = _.TotalCouponAmount > 0 ? $"优惠卷金额:{ _.TotalCouponAmount}" : string.Empty,
                    CarNumber = "",
                    TechName = "",
                    DispatchTime = ""

                });
            });

            if (result.Data.Items?.Count > 0)
            {
                var getOrderDispatch = await _orderDispatchRepository.GetOrderDispatch(new GetOrderDispatchRequest()
                {
                    OrderNos = result.Data.Items?.Select(_ => _.OrderNo)?.ToList()
                });
                GetOrderDetailStaticReportResponse getOrderDetail = new GetOrderDetailStaticReportResponse();
                if (getOrderDispatch?.Count > 0)
                {
                    getOrderDispatch?.ForEach(_ =>
                        {
                            getOrderDetail = result.Data.Items.Where(item => _.OrderNo == item.OrderNo)?.FirstOrDefault();
                            if (getOrderDetail != null)
                            {
                                getOrderDetail.TechName = _.TechName;
                                getOrderDetail.DispatchTime = _.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        });
                }

                var orderIds = result.Data.Items?.Select(_ => _.OrderId)?.ToList();
                var getOrderCars = await _orderCarRepository.GetOrderCars(orderIds);

                getOrderCars?.ForEach(_ =>
                {
                    getOrderDetail = result.Data.Items.Where(item => _.OrderId == item.OrderId)?.FirstOrDefault();
                    getOrderDetail.CarNumber = _.CarNumber;

                });

                var shops = await shopClient.GetShopListAsync(new GetShopListRequest()
                {
                    ShopIds = result.Data.Items?.Select(_ => _.ShopId)?.ToList(),
                    PageIndex = 1,
                    PageSize = request.PageSize
                });

                result.Data.Items?.ToList()?.ForEach(item =>
                    {
                        item.ShopName = shops.Data.Items.Where(_ => _.Id == item.ShopId)?.FirstOrDefault()?.SimpleName;
                    });
            }

            result.Data.TotalItems = getOrders?.TotalItems ?? 0;
            return result;

        }

        public async Task<ApiResult<List<FranchisesConfigDTO>>> GetFranchisesConfig(GetFranchisesConfigRequest request)
        {
            var dalResponse = await _franchisesConfigRepository.GetFranchises(request);

            var data = mapper.Map<List<FranchisesConfigDTO>>(dalResponse);

            return Result.Success(data);
        }

        public async Task<ApiPagedResult<GetOrdersForOfficeResponse>> GetOrdersForOffice(GetOrdersForOfficeRequest request)
        {
            var dalOrderResponse = await _orderRepository.GetOrdersForOffice(request);

            ApiPagedResultData<GetOrdersForOfficeResponse> apiPagedResultData = mapper.Map<ApiPagedResultData<GetOrdersForOfficeResponse>>(dalOrderResponse);

            if (dalOrderResponse?.Items?.Count > 0)
            {
                var getOrderProduct = await _orderClient.GetOrderProduct(new GetOrderProductRequest()
                {
                    OrderNos = dalOrderResponse?.Items?.Select(_ => _.OrderNo)?.ToList()
                });

                apiPagedResultData?.Items?.ToList()?.ForEach(_ =>
                {
                    var products = getOrderProduct?.Data?.Where(item => item.OrderNo == _.OrderNo);
                    _.Goods = mapper.Map<List<GetOrdersForOfficeProductDTO>>(products);
                    _.Goods?.ForEach(item =>
                    {
                        item.ImageUrl = "https://m.ApolloErp.cn//" + item.ImageUrl;
                    });
                });


            }
            ApiPagedResult<GetOrdersForOfficeResponse> response = new ApiPagedResult<GetOrdersForOfficeResponse>()
            {
                Code = ResultCode.Success,
                Data = apiPagedResultData
            };

            return response;
        }

        public async Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request)
        {
            var insuranceCompanyDOs = await _orderInsuranceCompanyRepository.GetListAsync(" where is_deleted=0 and order_no=@OrderNo", new { OrderNo = request.OrderNo }, true);

            var data = mapper.Map<OrderInsuranceCompanyDTO>(insuranceCompanyDOs.FirstOrDefault());

            return Result.Success(data);

        }

        public async Task<ApiResult<List<InsuranceCompanyDTO>>> GetInsuranceCompanyList(GetShopInfoRequest request)
        {
            long companyId = 0;

            var param = new DynamicParameters();
            string sqlWhere = string.Empty;

            if (request.ShopId > 0)
            {
                //查询门店详情
                var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = request.ShopId });
                companyId = getShopResult?.GetSuccessData()?.CompanyId??0;
                if (companyId >= 0)
                {
                    param.Add("@company_id", companyId);
                    sqlWhere += " and company_id = @company_id";
                }
            }
            var insuranceCompanyDOs = await _insuranceCompanyRepository.GetListAsync(" where is_deleted=0 "+ sqlWhere, param, true);

            var data = mapper.Map<List<InsuranceCompanyDTO>>(insuranceCompanyDOs);

            return Result.Success(data);
        }

        public async Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request)
        {
            var dalResponse = await _verificationRuleRepository.GetVerificationRule(request);

            ApiPagedResultData<VerificationRuleDTO> response = mapper.Map<ApiPagedResultData<VerificationRuleDTO>>(dalResponse);

            var ruleIds = dalResponse?.Items?.Select(_ => _.Id)?.ToList();
            if (ruleIds?.Count() > 0)
            {
                var shopIds = await _verificationRuleShopIdRepository.GetListAsync(" where is_deleted=0 and rule_id in @RuleIds", new { RuleIds = ruleIds }, true);

                var products = await _verificationRulePidRepository.GetListAsync(" where is_deleted=0 and rule_id in @RuleIds", new { RuleIds = ruleIds }, true);

                response?.Items?.ToList()?.ForEach(_ =>
                {
                    _.ShopIds =
                    string.Join(",", shopIds?.Where(item => item.RuleId == _.Id)?.Select(item => item.ShopId)?.ToArray());

                    _.Pids = string.Join(",", products?.Where(item => item.RuleId == _.Id)?.Select(item => item.Pid)?.ToArray());
                });
            }

            return new ApiPagedResult<VerificationRuleDTO>()
            {
                Code = ResultCode.Success,
                Data = response
            };
        }

        public async Task<ApiResult<List<GetShopInfoByRefSmallWarehouseIdResponse>>> GetShopInfoByRefSmallWarehouseId(GetShopInfoByRefSmallWarehouseIdRequest request)
        {
            var getShopOccpupy = await _ApolloErpWmsClient.GetShopOccupyMappingsByRelationId(new GetShopOccupyMappingsByRelationIdRequest()
            {
                RelationShopId = request.SmallWarehouseId
            });

            var getShopInfo = getShopOccpupy?.Data?.Select(_ =>
            {
                return new GetShopInfoByRefSmallWarehouseIdResponse()
                {
                    ShopId = _.ShopId,
                    ShopName = _.ShopName
                };
            });

            return Result.Success(getShopInfo?.ToList());
        }

        public async Task<ApiResult<List<GetWaitingPaySmallWarehouseOrderResponse>>> GetWaitingPaySmallWarehouseOrder(GetWaitingPaySmallWarehouseOrderRequest request)
        {
            var startDate = DateTime.Now.AddMonths(-1).AddDays(1 - DateTime.Now.Day).Date;
            var endDate = DateTime.Now.AddMonths(-1).AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            if (!string.IsNullOrEmpty(request.ReconciliationDate))
            {
                DateTime.TryParse(request.ReconciliationDate, out var reconciliationDateTime);

                if (reconciliationDateTime.Month > DateTime.Now.Month && reconciliationDateTime.Year >= DateTime.Now.Year)
                {
                    return Result.Success(default(List<GetWaitingPaySmallWarehouseOrderResponse>));
                }

                startDate = reconciliationDateTime.AddDays(1 - reconciliationDateTime.Day);

                endDate = reconciliationDateTime.AddDays(1 - reconciliationDateTime.Day).AddMonths(1).AddDays(-1);
            }


            var delegateUserOrderDOs = await _delegateUserOrderRepository.GetListAsync(" where is_deleted=0 and LENGTH(trim(ref_order_no))=0 and shop_id=@ShopId and create_time >= @StartTime AND create_time<=@EndTime", new { ShopId = request.ShopId, StartTime = startDate, EndTime = endDate });


            var orderNos = delegateUserOrderDOs?.Select(_ => _.OrderNo)?.ToList();


            if (orderNos?.Count() > 0)
            {
                var orderInfos = await _orderRepository.GetListAsync(" where is_deleted=0 and order_no in @OrderNos and order_status=@OrderStatus", new { OrderNos = orderNos, OrderStatus = OrderStatusEnum.Completed.ToInt() });

                var getWatingPaySmallhouseOrder = orderInfos?.Select(_ =>
                 {
                     return new GetWaitingPaySmallWarehouseOrderResponse()
                     {
                         ActualAmount = _.ActualAmount,
                         OrderNo = _.OrderNo,
                         CreateTime = _.CreateTime.ToString("yyyy-MM-dd")
                     };
                 });

                return Result.Success(getWatingPaySmallhouseOrder?.ToList());
            }

            return Result.Success(default(List<GetWaitingPaySmallWarehouseOrderResponse>));
        }

        public async Task<ApiResult<List<GetRefDelegateOrdersResponse>>> GetRefDelegateOrders(GetRefDelegateOrdersRequest request)
        {
            var delegateUserOrderDOs = await _delegateUserOrderRepository.GetListAsync(" where is_deleted=0 and ref_order_no =@OrderNo", new { OrderNo = request.OrderNo });

            var orders = delegateUserOrderDOs?.Select(_ =>
             {
                 return new GetRefDelegateOrdersResponse()
                 {
                     OrderNo = _.OrderNo
                 };
             });

            return Result.Success(orders?.ToList());
        }

        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            List<GetOrderOutProductsProfitResponse> data = new List<GetOrderOutProductsProfitResponse>();
            var getOrderOutProducts = await _orderRepository.GetOrderOutProductsProfit(request.Data);
            if (getOrderOutProducts?.Items?.Count > 0)
            {
                data = getOrderOutProducts?.Items.ToList();
            }

            //var getOrderOutProducts = await _orderRepository.GetOrderOutProducts(request.Data);

            //if (getOrderOutProducts?.Items?.Count > 0)
            //{
            //    var getOrderOccupyShopProductPurchaseInfo = await _ApolloErpWmsClient.GetOrderOccupyShopProductPurchaseInfo(
            //        new GetOrderOccupyShopProductPurchaseInfoReqeust()
            //        {
            //            OrderNos = getOrderOutProducts?.Items.Select(_ => _.OrderNo)?.ToList(),

            //        });


            //    var shops =
            //        await shopClient.GetShopListAsync(new GetShopListRequest() { ShopIds = getOrderOutProducts?.Items?.Select(_ => _.ShopId)?.ToList(), PageIndex = request.Data.PageIndex, PageSize = request.Data.PageSize });
            //    GetOrderOutProductResponse getOrderOutProductsResponse = default(GetOrderOutProductResponse);
            //    getOrderOccupyShopProductPurchaseInfo?.Data?.ForEach(_ =>
            //    {
            //        getOrderOutProductsResponse = getOrderOutProducts.Items
            //            .Where(item => item.ProductId == _.ProductCode)?.FirstOrDefault();
            //        if (getOrderOutProductsResponse != null)
            //        {
            //            data.Add(new GetOrderOutProductsProfitResponse()
            //            {
            //                ShopId = getOrderOutProductsResponse?.ShopId ?? 0,
            //                SimpleName = shops?.Data?.Items?.Where(shopItem => shopItem.Id == (getOrderOutProductsResponse?.ShopId ?? 0))?.FirstOrDefault()?.SimpleName,
            //                ProductId = _.ProductCode,
            //                ProductName = _.ProductName,
            //                OeNumber = _.OleNumber,
            //                CategoryCode = _.CategoryName,
            //                VenderShortName = _.VenderShortName,
            //                PurchaseOrder = _.PurchaseOrder,
            //                SaleOrder = _.OrderNo,
            //                CarNumber = getOrderOutProductsResponse?.CarNumber,
            //                CarInfo = getOrderOutProductsResponse?.CarInfo,
            //                PurchasePrice = _.PurchasePrice,
            //                SalePrice = _.SalePrice,
            //                SaleOrderPrice = (getOrderOutProductsResponse?.Amount ?? 0) * getOrderOutProductsResponse.TotalNumber,
            //                TotalNumber = getOrderOutProductsResponse.TotalNumber,
            //                PurchaseTotalPrice = getOrderOutProductsResponse.TotalNumber * _.PurchasePrice,
            //                ActualAmount = (getOrderOutProductsResponse?.Amount ?? 0) * getOrderOutProductsResponse.TotalNumber,
            //                CreateTime = getOrderOutProductsResponse?.CreateTime
            //            });
            //        }
            //    });
            //}


            return new ApiPagedResult<GetOrderOutProductsProfitResponse>()
            {
                Code = ResultCode.Success,
                Data = new ApiPagedResultData<GetOrderOutProductsProfitResponse>()
                {
                    Items = data,
                    TotalItems = getOrderOutProducts?.TotalItems ?? 0
                }
            };
        }

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request)
        {
            var getOrderProducts = await orderProductRepo.GetOrderProductsReport(request.Data);

            ApiPagedResultData<OrderProductNewDTO> data = mapper.Map<ApiPagedResultData<OrderProductNewDTO>>(getOrderProducts);


            var shops =
                await shopClient.GetShopListAsync(new GetShopListRequest() { ShopIds = getOrderProducts?.Items?.Select(_ => _.ShopId)?.ToList(), PageIndex = 1, PageSize = request.Data.PageSize });

            data?.Items?.ToList()?.ForEach(_ =>
            {
                _.SimpleName = shops?.Data?.Items?.Where(shopItem => shopItem.Id == (_?.ShopId ?? 0))?.FirstOrDefault()?.SimpleName;
            });

            return new ApiPagedResult<OrderProductNewDTO>()
            {
                Code = ResultCode.Success,
                Data = data
            };

        }

        /// <summary>
        /// 经营月报报表查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList(GetShopSalesMonthRequest request)
        {
            List<ShopSalesMonthResponse> response = new List<ShopSalesMonthResponse>();

            List<ShopOrderDO> dalList = await _orderRepository.GetShopOrderList(request);
            DateTime startDateTime = Convert.ToDateTime(request.StartDate);
            DateTime endDateTime = Convert.ToDateTime(request.EndDate);
            if (dalList == null || dalList.Count() == 0)
            {
                return Result.Success(response);
            }
            while (startDateTime <= endDateTime)
            {
                DateTime curretDate = startDateTime;
                startDateTime = startDateTime.AddDays(1);
                ShopSalesMonthResponse shopSale = new ShopSalesMonthResponse();
                shopSale.InstallDate = curretDate.ToString("yyyy-MM-dd");
                var curretData = dalList.Where(t => t.InstallTime == curretDate);
                List<ShopSaleMonthVO> shopSaleList = new List<ShopSaleMonthVO>();

                if (curretData == null || curretData.Count() == 0)
                {
                    shopSale.ShopSaleMonthList = new List<ShopSaleMonthVO>();
                    response.Add(shopSale);
                    continue;
                }
                foreach (int type in Enum.GetValues(typeof(OrderTypeEnum)))
                {
                    ShopSaleMonthVO sale = new ShopSaleMonthVO();
                    sale.Type = type;
                    //总体
                    if (type == 0)
                    {
                        //求客户数
                        sale.CustomerNum = curretData.Select(t => t.UserId).Distinct().Count();
                        //订单数
                        sale.OrderNum = curretData.Count();
                        //订单总金额
                        sale.OrderSumMoney = curretData.Sum(t => t.ActualAmount);

                        //不含套餐卡销售金额
                        var allMoney = curretData.Where(t => t.OrderType != 7).Sum(t => t.ActualAmount);
                        //均值客户数，不含套餐卡和0元订单
                        var cNum = curretData.Where(t => t.OrderType != 7 && t.ActualAmount >0).Select(t => t.UserId).Distinct().Count();
                        //均值订单数，不含套餐卡和0元订单
                        var oNum = curretData.Where(t => t.OrderType != 7 && t.ActualAmount > 0).Count();

                        //客户均金额
                        sale.CustomerAvgMoney = cNum == 0 ? 0 : (decimal)(Math.Round((decimal)(allMoney / cNum), 2));
                        //订单均金额
                        sale.OrderAvgMoney = oNum == 0 ? 0 : (decimal)(Math.Round((decimal)(allMoney / oNum), 2));
                    }
                    else
                    {
                        var curretDataType = curretData.Where(t => t.OrderType == type);
                        if (curretDataType != null && curretDataType.Count() != 0)
                        {
                            //求客户数
                            sale.CustomerNum = curretDataType.Select(t => t.UserId).Distinct().Count();
                            //订单数
                            sale.OrderNum = curretDataType.Where(t => t.OrderType == type).Count();
                            //订单总金额
                            sale.OrderSumMoney = curretDataType.Where(t => t.OrderType == type).Sum(t => t.ActualAmount);
                            //客户均金额
                            sale.CustomerAvgMoney = sale.CustomerNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.CustomerNum), 2));
                            //订单均金额
                            sale.OrderAvgMoney = sale.OrderNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.OrderNum), 2));
                        }
                    }
                    shopSaleList.Add(sale);
                }
                shopSale.ShopSaleMonthList = shopSaleList;
                response.Add(shopSale);
            }
            return Result.Success(response);
        }

        /// <summary>
        /// 得到套餐卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderPackageCardDTO>>> GetOrderPackageCard(GetOrderPackageCardRequest request)
        {
            var getOrderPackageCard = await _orderPackageCardRepository.GetOrderPackageCard(request);

            var orderPackageCardDTO = mapper.Map<List<OrderPackageCardDTO>>(getOrderPackageCard);

            return new ApiResult<List<OrderPackageCardDTO>>()
            {
                Code = ResultCode.Success,
                Data = orderPackageCardDTO
            };
        }

    }

}
