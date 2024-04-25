using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Common.QrCode;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Clients;
using Ae.ConsumerOrder.Service.Client.Clients.OrderServer;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.BaoYang;
using Ae.ConsumerOrder.Service.Common.Constant;
using Ae.ConsumerOrder.Service.Common.Exceptions;
using Ae.ConsumerOrder.Service.Common.Extension;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Core.Response.OrderQuery;
using Ae.ConsumerOrder.Service.Dal.Model;
using Ae.ConsumerOrder.Service.Dal.Repository;
using Ae.ConsumerOrder.Service.Dal.Repository.OrderVerificationCode;

namespace Ae.ConsumerOrder.Service.Imp.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderLogRepository orderLogRepository;
        private readonly IOrderAddressRepository orderAddressRepository;
        private readonly IOrderCarRepository orderCarRepository;
        private readonly IOrderUserRepository orderUserRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IOrderVerificationCodeRepository orderVerificationCodeRepository;
        private readonly IVerificationRuleRepository verificationRuleRepository;
        private readonly IVerificationRuleShopIdRepository verificationRuleShopIdRepository;
        private readonly IProductSearchClient productSearchClient;
        private readonly IShopClient shopClient;
        private readonly IUserClient userClient;
        private readonly IBaoYangClient baoYangClient;
        private readonly ICouponClient couponClient;
        private readonly IPayClient payClient;
        private readonly IOrderQueryClient _orderQueryClient;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IOrderPackageCardRepository _orderPackageCardRepository;
        private readonly IVehicleClient _vehicleClient;

        public OrderQueryService(IMapper mapper, ApolloErpLogger<OrderQueryService> logger, IOrderRepository orderRepository,
            IOrderLogRepository orderLogRepository, IOrderAddressRepository orderAddressRepository, IOrderCarRepository orderCarRepository,
            IOrderUserRepository orderUserRepository, IOrderProductRepository orderProductRepository,
            IOrderVerificationCodeRepository orderVerificationCodeRepository,
            IVerificationRuleRepository verificationRuleRepository, IVerificationRuleShopIdRepository verificationRuleShopIdRepository,
            IProductSearchClient productSearchClient, IShopClient shopClient, IUserClient userClient, IBaoYangClient baoYangClient,
            ICouponClient couponClient, IPayClient payClient, IOrderQueryClient orderQueryClient, Microsoft.Extensions.Configuration.IConfiguration configuration, IOrderPackageCardRepository orderPackageCardRepository, IVehicleClient vehicleClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.orderLogRepository = orderLogRepository;
            this.orderAddressRepository = orderAddressRepository;
            this.orderCarRepository = orderCarRepository;
            this.orderUserRepository = orderUserRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderVerificationCodeRepository = orderVerificationCodeRepository;
            this.verificationRuleRepository = verificationRuleRepository;
            this.verificationRuleShopIdRepository = verificationRuleShopIdRepository;
            this.productSearchClient = productSearchClient;
            this.shopClient = shopClient;
            this.userClient = userClient;
            this.baoYangClient = baoYangClient;
            this.couponClient = couponClient;
            this.payClient = payClient;
            _orderQueryClient = orderQueryClient;
            _configuration = configuration;
            _orderPackageCardRepository = orderPackageCardRepository;
            _vehicleClient = vehicleClient;
        }

        #region 提取的订单公共方法
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
        /// 获取订单确认页套餐单品服务信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetOrderConfirmPackageProductServicesResponse> GetOrderConfirmPackageProductServices(GetOrderConfirmPackageProductServicesRequest request)
        {
            var response = new GetOrderConfirmPackageProductServicesResponse();

            #region 尝试取套餐数据
            var tryPackageProductCodes = (from item in request.ProductInfos
                                          select item.Pid).ToList();
            var allProductCodes = tryPackageProductCodes;//所有产品编号（含套餐内产品）
            var parts = new List<BaoYangPartRequest>();//所有配件配件查询条件
            var packageProductSearchRequest = new PackageProductSearchRequest() { ProductCodes = tryPackageProductCodes };
            var getPackageProductsByCodesResult = await productSearchClient.GetPackageProductsByCodes(packageProductSearchRequest);//如果是套餐获取套餐产品集合
            //logger.Info($"GetOrderConfirmPackageProductServices packageProductSearchRequest={JsonConvert.SerializeObject(packageProductSearchRequest)} getPackageProductsByCodesResult={JsonConvert.SerializeObject(getPackageProductsByCodesResult)}");
            var packageProducts = getPackageProductsByCodesResult?.GetSuccessData();//套餐数据
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
            var saleProductId = _configuration["OrderConfig:ApolloErpJiYouPackage"];
            var isContainsSaleProduct = request.ProductInfos?.Select(_ => _.Pid == saleProductId)?.FirstOrDefault() ?? false;
            if (string.IsNullOrWhiteSpace(request.CarId) && isContainsSaleProduct)
            {
                allProductCodes.Add(_configuration["OrderConfig:CommonJvPid"]);
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
                    var getAdaptiveProductByPartTypeAndCarIdResult = await baoYangClient.GetAdaptiveProductByPartTypeAndCarId(adaptiveProductByPartTypeAndCarIdRequest);
                    //logger.Info($"GetOrderConfirmPackageProductServices adaptiveProductByPartTypeAndCarIdRequest={JsonConvert.SerializeObject(adaptiveProductByPartTypeAndCarIdRequest)} getAdaptiveProductByPartTypeAndCarIdResult={JsonConvert.SerializeObject(getAdaptiveProductByPartTypeAndCarIdResult)}");
                    adaptiveProducts = getAdaptiveProductByPartTypeAndCarIdResult.GetSuccessData();
                    if (adaptiveProducts != null && adaptiveProducts.Any())
                    {
                        foreach (var item in adaptiveProducts)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Pid))
                            {
                                allProductCodes.Add(item.Pid);
                            }
                        }
                    }
                }
            }

            #endregion



            #region 取所有商品详情数据
            var productDetailSearchRequest = new ProductDetailSearchRequest() { ProductCodes = allProductCodes };
            var getProductsByProductCodesResult = await productSearchClient.GetProductsByProductCodes(productDetailSearchRequest);
            //logger.Info($"GetOrderConfirmPackageProductServices productDetailSearchRequest={JsonConvert.SerializeObject(productDetailSearchRequest)} getProductsByProductCodesResult={JsonConvert.SerializeObject(getProductsByProductCodesResult)}");
            var productDetails = getProductsByProductCodesResult?.GetSuccessData();//商品详情数据
            if (productDetails == null || !productDetails.Any())
            {
                logger.Warn($"选购的商品：{string.Join(";", allProductCodes)}未找到");
                throw new CustomException("商品信息异常");
            }
            #endregion

            #region 获取所有带出的安装服务产品详情数据
            var allRelatedServiceCodes = new List<string>();
            foreach (var item in productDetails)
            {
                if (item.Product.OnSale == 0)
                {
                    throw new CustomException($"选购的商品已下架");
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

            var installProducts = new List<InstallProductRequest>();
            request?.ProductInfos?.ForEach(_ =>
            {
                installProducts.Add(new InstallProductRequest()
                {
                    InstallType = string.Empty,
                    Num = _.Number,
                    Pid = _.Pid
                });
            });
            VehicleRequest vehicleRequest = null;
            if (!string.IsNullOrEmpty(request.CarId) && !string.IsNullOrEmpty(request.UserId))
            {
                var vehicle = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdRequest()
                {
                    CarId = request.CarId,
                    UserId = request.UserId
                });
                vehicleRequest = mapper.Map<VehicleRequest>(vehicle.Data);
            }

            var getInstallServices = await baoYangClient.GetInstallServiceByProduct(new InstallServiceByProductRequest()
            {
                Products = installProducts,
                Vehicle = vehicleRequest
            });

            getInstallServices?.Data?.InstallService?.ForEach(_ =>
            {
                allRelatedServiceCodes.Add(_.ServiceId);
            });

            #endregion
            var serviceDetails = new List<ProductDetailDTO>();//带出的服务商品详情数据
            if (allRelatedServiceCodes.Any())
            {
                var serviceDetailSearchRequest = new ProductDetailSearchRequest() { ProductCodes = allRelatedServiceCodes };
                var getServicesByProductCodesResult = await productSearchClient.GetProductsByProductCodes(serviceDetailSearchRequest);
                //logger.Info($"GetOrderConfirmPackageProductServices serviceDetailSearchRequest={JsonConvert.SerializeObject(serviceDetailSearchRequest)} getServicesByProductCodesResult={JsonConvert.SerializeObject(getServicesByProductCodesResult)}");
                serviceDetails = getServicesByProductCodesResult?.GetSuccessData();
                if (serviceDetails == null || !serviceDetails.Any())
                {
                    logger.Warn($"带出的服务：{string.Join(";", allRelatedServiceCodes)}未找到");
                    throw new CustomException($"服务信息异常");
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

                if (productCode == saleProductId)
                {
                    var checkFistSpecialProduct = await orderRepository.CheckUserFirstOrderForSpecialProduct(request.UserId, saleProductId);
                    if (checkFistSpecialProduct <= 0)
                    {
                        //移除198 首单价格
                        // findProduct.Product.SalesPrice = Convert.ToDecimal(_configuration["OrderConfig:ApolloErpJiYouSalePrice"]);
                    }
                }
                // var isContainsSaleProduct = request.ProductInfos?.Select(_ => _.Pid == saleProductId)?.FirstOrDefault() ?? false;

                if (findProduct == null)
                {
                    logger.Warn($"选购的商品：{productCode}未找到");
                    throw new CustomException($"商品信息异常");
                }
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
                        MarketingPrice = findProduct.Product.StandardPrice,
                        Price = findProduct.Product.SalesPrice,
                        Unit = findProduct.Product.Unit,
                        Number = productInfo.Number,
                        TotalNumber = productInfo.Number,
                        Amount = findProduct.Product.SalesPrice * productInfo.Number,
                        TotalAmount = findProduct.Product.SalesPrice * productInfo.Number,
                        TaxRate = findProduct.Product.TaxRate,
                        ActualPayAmount = findProduct.Product.SalesPrice * productInfo.Number,
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
                                        }
                                        else
                                        {
                                            if (string.IsNullOrWhiteSpace(request.CarId) && isContainsSaleProduct)
                                            {
                                                detailPid = _configuration["OrderConfig:CommonJvPid"];
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
                                    MarketingPrice = detail.StandardPrice,
                                    Price = detail.SalesPrice,
                                    Unit = findDetail.Product.Unit,
                                    Number = detail.Quantity,
                                    TotalNumber = detail.Quantity * productInfo.Number,
                                    Amount = detail.SalesPrice * detail.Quantity,
                                    TotalAmount = detail.SalesPrice * detail.Quantity * productInfo.Number,
                                    TaxRate = findDetail.Product.TaxRate,
                                    ParentPackageProductCode = findProduct.Product.ProductCode,
                                    ActualPayAmount = detail.SalesPrice * detail.Quantity * productInfo.Number,
                                };
                                product.PackageProducts.Add(packageProduct);//  59  BaseOilGrade  基础油级别  机油
                            }
                        }
                    }
                }

                products.Add(product);
            }
            #endregion

            #region 找出并组装所有的单品带出的安装服务
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
            //                TaxRate = findInstallationServiceProduct.Product.TaxRate
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


            #region 补录产品服务

            serviceDetails?.ForEach(_ =>
            {
                var getInstallService = getInstallServices?.Data?.ProductInstallService?.Where(item => item.ServiceId.Any(t => t == _.Product.ProductCode))?.FirstOrDefault();

                var installService = getInstallServices?.Data?.InstallService?.Where(item => item.ServiceId == _.Product.ProductCode)?.FirstOrDefault();
                if (getInstallService != null)
                {
                    //带出的套餐外安装服务
                    var service = new OrderConfirmProductDTO()
                    {
                        ProductId = _.Product.ProductCode,
                        ProductName = _.Product.Name,
                        DisplayName = _.Product.Name,
                        Brand = string.Empty,
                        Description = _.Product.Description,
                        ProductAttribute = _.Product.ProductAttribute,
                        MainCategoryCode = _.Product.MainCategoryId.ToString(),
                        SecondCategoryCode = _.Product.SecondCategoryId.ToString(),
                        CategoryCode = _.Product.ChildCategoryId.ToString(),
                        Label = GetLabelByProductDetail(_),
                        ImageUrl = _.Product.Image1,
                        MarketingPrice = installService?.MarketPrice ?? 0,
                        Price = installService?.Price ?? 0,
                        Unit = _.Product.Unit,
                        Number = installService?.Count ?? 0,
                        TotalNumber = installService?.Count ?? 0,
                        Amount = installService.Count * installService.Price,
                        TotalAmount = installService.Count * installService.Price,
                        TaxRate = _.Product.TaxRate
                    };

                    var findIfExistService = services.Find(item => item.ProductId == service.ProductId);
                    if (findIfExistService == null)
                    {
                        service.ServeForProductCodes = $"{getInstallService.ProductId};";
                        services.Add(service);
                    }
                    else
                    {
                        //同一个安装服务用于多个商品累计
                        findIfExistService.Number += installService?.Count ?? 0;
                        findIfExistService.TotalNumber += installService?.Count ?? 0;
                        findIfExistService.Amount += installService.Count * installService.Price;
                        findIfExistService.TotalAmount += installService.Count * installService.Price;
                        if (findIfExistService.ServeForProductCodes != null)
                        {
                            findIfExistService.ServeForProductCodes += $"{getInstallService.ProductId};";
                        }
                    }
                }

            });

            #endregion
            response.Products = products;
            response.Services = services;
            //logger.Info($"GetOrderConfirmPackageProductServices response.Products={JsonConvert.SerializeObject(products)} response.Services={JsonConvert.SerializeObject(services)}");

            return response;
        }

        /// <summary>
        /// 获取订单摘要信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static Tuple<string, string> GetOrderSummary(OrderDO order)
        {
            string summary = string.Empty;
            string subSummary = string.Empty;

            switch ((OrderStatusEnum)order.OrderStatus)
            {
                case OrderStatusEnum.Submitted:
                    if (order.PayStatus == 0)
                    {
                        if (order.PayMethod == 1)
                        {
                            summary = "等待客人支付";
                            var timeLeft = order.OrderTime.AddHours(24) - DateTime.Now;
                            subSummary = $"超过{timeLeft.Hours}小时{timeLeft.Minutes}分钟订单将自动取消";
                        }
                    }
                    else
                    {
                        summary = "等待客服审核";
                        subSummary = "未审核通过订单将取消";
                    }
                    break;
                case OrderStatusEnum.Confirmed:
                    if (order.ProduceType == 1)
                    {
                        summary = "订单待安装";
                        subSummary = "等待客人开车前往门店体验服务";
                    }
                    else if (order.DeliveryType == 1)
                    {
                        if (order.DeliveryStatus == 1)
                        {
                            if (order.SignStatus == 0)
                            {
                                summary = "订单待收货";
                                subSummary = "货品到店后会及时通知客人";
                            }
                            else
                            {
                                summary = "订单待安装";
                                subSummary = "等待客人开车前往门店体验服务";
                            }
                        }
                        else
                        {
                            summary = "等待仓库发货";
                            subSummary = "到货后会及时和客人预约到店服务时间";
                        }
                    }//TODO: 暂无到家，待开放配送到家需要新增文案
                    break;
                case OrderStatusEnum.Completed:
                    if (order.CommentStatus == 0)
                    {
                        summary = "订单已完成";
                        subSummary = "等待客人点评";
                    }
                    else
                    {
                        summary = "订单已点评";
                        subSummary = "期待可以再次为您提供服务";
                    }
                    break;
                case OrderStatusEnum.Canceled:
                    summary = "订单已取消";
                    subSummary = "期待可以再次为您提供服务";
                    break;
                default:
                    break;
            }

            return new Tuple<string, string>(summary, subSummary);
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
                    break;
                case OrderStatusEnum.Confirmed:
                    if (order.ProduceType == 1)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    }
                    else if (order.DeliveryType == 1)
                    {
                        if (order.DeliveryStatus == 1)
                        {
                            if (order.SignStatus == 0)
                            {
                                //list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ConfirmReceive, IsImportance = true, Sort = 1 });
                            }
                            else
                            {
                                list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                            }
                        }
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
            if (order.OrderStatus < (sbyte)OrderStatusEnum.Canceled
                && order.IsOccurReverse == 1 && order.ReverseStatus != (sbyte)ReverseStatusEnum.Canceled)
            {
                if (order.PayStatus == 1)
                {
                    if (order.RefundStatus == 0)
                    {
                        list = new List<OrderUserOperationDTO>();
                    }
                }
            }
            return list;
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

            var products = await orderProductRepository.GetOrderProducts(orderId);
            if (products == null || !products.Any())
            {
                return response;
            }

            var packageProducts = products.FindAll(_ => _.ProductAttribute == 1);//套餐集合
            var packageChildrenProducts = products.FindAll(_ => _.ParentOrderPackagePid > 0);//套餐子商品集合
            var singleProducts = products.FindAll(_ => _.ProductAttribute == 0 && _.ParentOrderPackagePid == 0);//单品集合
            var singelServiceProducts = products.FindAll(_ => _.ProductAttribute == 2 && _.ParentOrderPackagePid == 0);//单服务集合

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
            var rule = await verificationRuleRepository.GetAsync(code.RuleId);
            if (rule == null)
            {
                return null;
            }

            var shopIds = new List<long>();
            if (rule.IsByShopId == 1)
            {
                var ruleShopIds = await verificationRuleShopIdRepository.GetListAsync("where rule_id=@RuleId", new { code.RuleId });
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
        #endregion

        public async Task<List<OrderCarDTO>> GetOrderCars(GetOrderCarsRequest request)
        {
            if (request.OrderIds != null && request.OrderIds.Any())
            {
                var getOrderCarsResponse = await orderCarRepository.GetOrderCars(request.OrderIds);

                var orderCars = mapper.Map<List<OrderCarDTO>>(getOrderCarsResponse);
                return orderCars;
            }

            return null;
        }

        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(GetOrderConfirmRequest request)
        {
            var result = Result.Failed<GetOrderConfirmResponse>("加载异常，请重试");
            var sw = Stopwatch.StartNew();
            var dict = new Dictionary<string, long>();
            var response = new GetOrderConfirmResponse()
            {
                DeliveryType = 1,//本期只上线配送到店
                Payment = 1,
                IsNeedInvoice = false
            };

            try
            {

                #region 准备外部数据
                //根据userid获取姓名地址信息
                var userId = request.UserId;
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = userId });
                sw.Stop();
                dict.Add("根据userid获取姓名地址信息", sw.ElapsedMilliseconds);
                sw.Restart();
                var user = getUserInfoResult.GetSuccessData();
                //var getUserAddressResult = await userClient.GetUserAddress(new UserAddressRequest() { UserId = userId });
                //var userAddress = getUserAddressResult.GetSuccessData();
                if (user != null)
                {
                    var receiverName = string.IsNullOrEmpty(user.UserName) ? (user.NickName ?? string.Empty) : user.UserName;
                    switch (response.DeliveryType)
                    {
                        case 1:
                            response.ReceiverName = receiverName;
                            response.ReceiverPhone = user.UserTel;
                            break;
                        case 2:
                            if (user.DefaultAddress != null)
                            {
                                var defaultAddress = user.DefaultAddress;
                                response.ReceiverName = string.IsNullOrWhiteSpace(defaultAddress.UserName) ? receiverName : defaultAddress.UserName;
                                response.ReceiverPhone = defaultAddress.MobileNumber;
                                response.ReceiverAddressId = defaultAddress.AddressId;
                                response.FullReceiverAddress = $"{defaultAddress.Province}{defaultAddress.City}{defaultAddress.District}{defaultAddress.AddressLine}";
                            }
                            break;
                        default:
                            break;
                    }

                }
                //查询门店
                ShopDTO shop = null;
                if (request.ShopId > 0)
                {
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = request.ShopId });
                    sw.Stop();
                    dict.Add("查询门店", sw.ElapsedMilliseconds);
                    sw.Restart();
                    shop = getShopResult?.GetSuccessData();
                }
                #endregion

                var selfProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();
                var shopProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();

                response.Products = new List<OrderConfirmPackageProductDTO>();
                response.Services = new List<OrderConfirmProductDTO>();

                #region 根据选择的Pid集合获取套餐、单品集合和服务集合

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(request);
                if (selfProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = selfProducts;
                    var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                    sw.Stop();
                    dict.Add("根据选择的Pid集合获取套餐、单品集合和服务集合", sw.ElapsedMilliseconds);
                    sw.Restart();
                    response.Products = getPackageProductServicesResponse.Products;
                    response.Services = getPackageProductServicesResponse.Services;
                    if ((response.Products == null || !response.Products.Any()) && (response.Services == null || !response.Services.Any()))
                    {
                        result = Result.Failed<GetOrderConfirmResponse>("商品信息异常");
                        return result;
                    }
                }

                #endregion

                #region 添加项目

                if (shopProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = shopProducts;

                    if (shopProducts?.Count() > 0)
                    {
                        var shopItems = await GetShopProduct(getPackageProductServicesRequest, null);
                        if (shopItems?.Products?.Count() > 0)
                        {
                            response.Products.AddRange(shopItems?.Products);
                        }
                    }
                }

                #endregion


                #region 修改价格

              
                if (ProductTypeEnum.SeckillOrder.ToSbyte() == request.ProduceType)
                {
                    var activeFlashProducts = await productSearchClient.GetActiveFlashSaleConfigs();
                    response.Products?.ForEach(_ =>
                    {
                        var singleProduct = activeFlashProducts?.Data?.Where(item => item.ProductId == _?.PackageOrProduct?.ProductId)?.FirstOrDefault();
                        if (singleProduct != null)
                        {
                            _.PackageOrProduct.Price = singleProduct.Price;
                            _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * singleProduct.Price;
                        }
                    });
                }
                else if (ProductTypeEnum.UsePackageCard.ToSbyte() == request.ProduceType)
                {
                    response.Products?.ForEach(_ =>
                    {
                        _.PackageOrProduct.Price = 0;
                        _.PackageOrProduct.TotalAmount = 0;
                    });
                }
                else
                {
                    ///美容团购修改价格支持门店价格
                    var shopGroupProducts = await shopClient.GetShopGrouponProduct(new GetShopGrouponProductRequest() { ShopId = request.ShopId, Status = 1 });

                    response.Products?.ForEach(_ =>
                    {
                        var singleProduct = shopGroupProducts?.Data?.Where(item => item.ServiceId == _?.PackageOrProduct?.ProductId)?.FirstOrDefault();
                        if (singleProduct != null)
                        {
                            _.PackageOrProduct.Price = singleProduct.Price;
                            _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * singleProduct.Price;
                        }
                    });
                }


                #endregion

                #region 金额计算
                int totalProductNum = 0;//商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
                decimal totalProductAmount = decimal.Zero;//商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
                int serviceNum = 0;//服务数量（指带出的安装服务，不含套餐内安装服务）
                decimal serviceFee = decimal.Zero;//服务费（指带出的安装服务，不含套餐内安装服务）
                decimal deliveryFee = decimal.Zero;//运费
                var calcCouponProducts = new List<Product>();//参与优惠计算的商品或服务集合

                foreach (var item in response.Products)
                {
                    totalProductNum += item.PackageOrProduct.TotalNumber;
                    totalProductAmount += item.PackageOrProduct.TotalAmount;
                    var calcCouponProduct = new Product()
                    {
                        Pid = item.PackageOrProduct.ProductId,
                        Category = item.PackageOrProduct.MainCategoryCode,
                        Brand = item.PackageOrProduct.Brand,
                        Total = item.PackageOrProduct.TotalNumber,
                        UnitPrice = item.PackageOrProduct.Price
                    };
                    calcCouponProducts.Add(calcCouponProduct);
                }

                foreach (var item in response.Services)
                {
                    serviceNum += item.TotalNumber;
                    serviceFee += item.TotalAmount;
                    var calcCouponProduct = new Product()
                    {
                        Pid = item.ProductId,
                        Category = item.MainCategoryCode,
                        Brand = item.Brand,
                        Total = item.TotalNumber,
                        UnitPrice = item.Price
                    };
                    calcCouponProducts.Add(calcCouponProduct);
                }
                decimal totalShouldPayAmount = totalProductAmount + serviceFee + deliveryFee;//应付总价

                #region 获取可用优惠信息
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
                    PayMethod = (PayMethod)response.Payment,
                    TotalAmount = totalShouldPayAmount,
                    ProductList = calcCouponProducts
                };
                var getOrderApplicableCouponListResult = await couponClient.GetOrderApplicableCouponList(orderApplicableCouponListReqDTO);
                sw.Stop();
                dict.Add("获取可用优惠信息", sw.ElapsedMilliseconds);
                sw.Restart();
                //logger.Info($"GetOrderApplicableCouponList orderApplicableCouponListReqDTO={JsonConvert.SerializeObject(orderApplicableCouponListReqDTO)} getOrderApplicableCouponListResult={JsonConvert.SerializeObject(getOrderApplicableCouponListResult)}");
                var orderApplicableCouponEntityResDTO = getOrderApplicableCouponListResult.GetSuccessData();
                if (orderApplicableCouponEntityResDTO != null)
                {
                    if (orderApplicableCouponEntityResDTO.OrderApplicableCoupon != null)
                    {
                        response.UserCouponId = orderApplicableCouponEntityResDTO.OrderApplicableCoupon.UserCouponId;
                        response.UserCouponDisplayName = orderApplicableCouponEntityResDTO.OrderApplicableCoupon.DisplayName;
                    }
                    if (orderApplicableCouponEntityResDTO.UserCouponId != null && orderApplicableCouponEntityResDTO.UserCouponId.Any())
                    {
                        response.AllApplicableCoupons = orderApplicableCouponEntityResDTO.UserCouponId;
                    }
                }

                decimal totalCouponAmount = decimal.Zero;//总优惠金额
                long userCouponId = 0;//当前参与计算的用户优惠券ID
                if (response.UserCouponId > 0)
                {
                    userCouponId = response.UserCouponId;
                }
                if (userCouponId > 0)
                {
                    //获取优惠券具体信息
                    var getUserCouponEntityCustomByIdResult = await couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdDTO() { UserCouponId = response.UserCouponId });
                    var userCoupon = getUserCouponEntityCustomByIdResult.GetSuccessData();
                    if (userCoupon != null)
                    {
                        totalCouponAmount += userCoupon.Value;
                    }
                }
                #endregion



                decimal actualAmount = totalShouldPayAmount - totalCouponAmount;

                if ((user?.MemberGrade ?? 0) == 50)//钻石折扣
                {
                    var diamondsDiscountRate = Convert.ToDecimal(_configuration["OrderConfig:DiamondsDiscountRate"]);


                    var diamondsDiscountAmount = Math.Ceiling((actualAmount * diamondsDiscountRate) * 100) / 100;//超出分的金额直接进位
                    response.DiamondsDiscountAmount = actualAmount - (diamondsDiscountAmount);

                    actualAmount = diamondsDiscountAmount;

                }

                response.TotalProductAmount = totalProductAmount;
                response.ServiceFee = serviceFee;
                response.DeliveryFee = deliveryFee;
                response.TotalCouponAmount = totalCouponAmount;
                response.ActualAmount = actualAmount;

                response.FreeShippingRuleDesc = string.Empty;
                #endregion
                sw.Stop();
                dict.Add("返回", sw.ElapsedMilliseconds);


                response.Products?.ForEach(_ =>
                {

                    var productOwnAttri = request.ProductInfos?.Where(item => item.Pid == _.PackageOrProduct.ProductId)?.FirstOrDefault()?.ProductOwnAttri;

                    _.PackageOrProduct.ProductOwnAttri = productOwnAttri ?? 0;
                });
                result = Result.Success(response, "加载成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderConfirmEx", ex);
            }
            finally
            {
                logger.Info($"GetOrderConfirm request={JsonConvert.SerializeObject(request)} response={JsonConvert.SerializeObject(result)} dict={JsonConvert.SerializeObject(dict)}");
            }
            return result;
        }

        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail(GetOrderDetailRequest request)
        {
            var result = Result.Failed<GetOrderDetailResponse>("加载异常，请重试");
            try
            {
                var response = new GetOrderDetailResponse();

                //摘要信息
                var order = await orderRepository.GetOrder(request.UserId, request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<GetOrderDetailResponse>(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                var summaryTuple = GetOrderSummary(order);
                response.Summary = summaryTuple.Item1;
                response.SubSummary = summaryTuple.Item2;

                //配送信息（个人地址或门店地址）
                response.IsNeedDelivery = order.IsNeedDelivery;
                response.DeliveryType = order.DeliveryType;
                ShopDTO shop = null;
                if (order.ShopId > 0)
                {
                    //查询门店详情
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = order.ShopId });
                    shop = getShopResult?.GetSuccessData();
                }
                var address = await orderAddressRepository.GetOrderAddress(order.Id);
                if (order.DeliveryType == 1)
                {
                    response.ShopId = order.ShopId;
                    if (shop != null)
                    {
                        response.ShopName = shop.SimpleName;
                        response.ShopAddress = $"{shop.Province}{shop.City}{shop.District}{shop.Address}";
                    }
                }
                if (address != null)
                {
                    response.ReceiverName = address.ReceiverName;
                    response.ReceiverPhone = address.ReceiverPhone;
                }

                //车辆信息
                var car = await orderCarRepository.GetOrderCar(order.Id);
                if (car != null)
                {
                    response.DisplayVehicleName = $"{car.Brand} - {car.Vehicle}";
                    response.DisplayModelName = $"{car.Nian} - {car.PaiLiang} - {car.SalesName}";
                }

                //商品信息
                var getOrderDetailPackageProductServicesResponse = await GetOrderDetailPackageProductServices(order.Id);
                response.Products = getOrderDetailPackageProductServicesResponse.Products;
                response.Services = getOrderDetailPackageProductServicesResponse.Services;

                //主信息
                response.OrderId = order.Id;
                response.OrderNo = order.OrderNo;
                response.OrderChannel = (ChannelEnum)order.Channel;
                response.OrderType = (OrderTypeEnum)order.OrderType;
                response.OrderTime = order.OrderTime;
                response.DisplayOrderStatus = GetDisplayOrderStatus(order);
                switch (response.OrderChannel)
                {
                    case ChannelEnum.None:
                        break;
                    case ChannelEnum.ApolloErpToC:
                        if (order.TerminalType == 1)
                        {
                            response.DisplayOrderChannel = "微信小程序";
                        }
                        else if (order.TerminalType == 2)
                        {
                            response.DisplayOrderChannel = "合作店";
                        }
                        break;
                    case ChannelEnum.ApolloErpToShop:
                        response.DisplayOrderChannel = "合作店";
                        break;
                    default:
                        break;
                }
                response.DisplayServiceCategory = ((OrderTypeEnum)order.OrderType).GetDescription();
                switch (order.PayChannel)
                {
                    case 1:
                        response.DisplayPayMethod = "微信支付";
                        break;
                    case 2:
                        response.DisplayPayMethod = "支付宝";
                        break;
                    default:
                        response.DisplayPayMethod = string.Empty;
                        break;
                }
                response.DisplayDeliveryMethod = order.DeliveryType == 1 ? "送货到店" : "送货到家";

                //金额信息
                response.ProductAmount = order.TotalProductAmount;
                response.ServiceAmount = order.ServiceFee;
                response.DeliveryFee = order.DeliveryFee;
                response.TotalCouponAmount = order.TotalCouponAmount;
                response.ActualAmount = order.ActualAmount;

                //操作按钮
                response.OrderUserOperations = GetOrderUserOperations(order);

                //安装码
                ShopConfigDTO shopConfig = null;
                if (!string.IsNullOrWhiteSpace(order.InstallCode))
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
                    response.OrderInstallCodeInfos = new List<OrderInstallCodeInfoDTO>();
                    response.OrderInstallCodeInfos.Add(orderInstallCodeInfo);
                }

                //核销码
                var orderVerificationCodes = await orderVerificationCodeRepository.GetListByUserIdAndOrderNo(request.UserId, request.OrderNo);
                if (orderVerificationCodes != null && orderVerificationCodes.Any())
                {
                    var orderVerificationCode = orderVerificationCodes.OrderBy(_ => _.Status).FirstOrDefault();
                    if (orderVerificationCode != null)
                    {
                        response.OrderVerificationCodeInfos = new List<OrderVerificationCodeInfoDTO>()
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

                result = Result.Success(response, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderDetailEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取订单确认页 门店项目产品
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        public async Task<GetOrderConfirmPackageProductServicesResponse> GetShopProduct(GetOrderConfirmPackageProductServicesRequest request, List<GetPromotionActivitByProductCodeListResponse> activityProduct)
        {
            var response = new GetOrderConfirmPackageProductServicesResponse();
            var productCodes = request?.ProductInfos?.Select(_ => _.Pid)?.ToList();

            var shopProducts = await productSearchClient.GetShopProductByCodes(new GetShopProductByCodeRequest()
            {
                ShopProductCodes = productCodes
            });

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

                    products.Add(new OrderConfirmPackageProductDTO()
                    {
                        PackageOrProduct = new OrderConfirmProductDTO()
                        {
                            ProductId = _.ProductCode,
                            ProductName = _?.ProductName,
                            DisplayName = _?.DisplayName,
                            Description = _?.Description,
                            Brand = string.Empty,
                            ProductAttribute = 4,
                            ParentOrderPackagePid = 0,
                            ServeForOrderPids = string.Empty,
                            CategoryCode = _.CategoryId.ToString(),
                            ItemCode = string.Empty,
                            Label = string.Empty,
                            ImageUrl = _.Icon.Replace("http://m.aerp.com.cn/", ""),
                            PriceId = 0,
                            MarketingPrice = price,
                            Price = price,
                            TotalCostPrice = price * num,
                            Number = 1,
                            TotalNumber = num,
                            StockStatus = 0,
                            Amount = price,
                            TotalAmount = price * num,
                            TaxRate = 0,
                            ShareDiscountAmount = 0,
                            ActualPayAmount = 0,
                        }
                    });
                }


            });
            response.Products = products;

            return response;
        }

        public async Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountRequest request)
        {
            var result = Result.Failed<TrialCalcOrderAmountResponse>("金额计算异常");
            var sw = Stopwatch.StartNew();
            var dict = new Dictionary<string, long>();
            try
            {
                var response = new TrialCalcOrderAmountResponse();

                #region 准备外部数据
                //查询门店
                ShopDTO shop = null;
                if (request.ShopId > 0)
                {
                    var getShopResult = await shopClient.GetShopById(new GetShopRequest() { ShopId = request.ShopId });
                    shop = getShopResult?.GetSuccessData();
                    sw.Stop();
                    dict.Add("查询门店", sw.ElapsedMilliseconds);
                    sw.Restart();
                }
                #endregion

                var selfProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 0)?.ToList();
                var shopProducts = request?.ProductInfos?.Where(_ => _.ProductOwnAttri == 1)?.ToList();

                var products = new List<OrderConfirmPackageProductDTO>();
                var services = new List<OrderConfirmProductDTO>();

                var getPackageProductServicesRequest = mapper.Map<GetOrderConfirmPackageProductServicesRequest>(request);
                #region 根据选择的Pid集合获取套餐、单品集合和服务集合

                if (selfProducts?.Count > 0)
                {

                    getPackageProductServicesRequest.ProductInfos = selfProducts;

                    var getPackageProductServicesResponse = await GetOrderConfirmPackageProductServices(getPackageProductServicesRequest);
                    sw.Stop();
                    dict.Add("根据选择的Pid集合获取套餐、单品集合和服务集合", sw.ElapsedMilliseconds);
                    sw.Restart();
                    products = getPackageProductServicesResponse.Products;
                    services = getPackageProductServicesResponse.Services;
                    if ((products == null || !products.Any()) && (services == null || !services.Any()))
                    {
                        result = Result.Failed<TrialCalcOrderAmountResponse>("商品信息异常");
                        return result;
                    }
                }

                #endregion

                #region 添加项目

                if (shopProducts?.Count > 0)
                {
                    getPackageProductServicesRequest.ProductInfos = shopProducts;

                    if (shopProducts?.Count() > 0)
                    {
                        var shopItems = await GetShopProduct(getPackageProductServicesRequest, null);
                        if (shopItems?.Products?.Count() > 0)
                        {
                            products.AddRange(shopItems?.Products);
                        }
                    }
                }

                #endregion

                #region 修改价格

                if (ProductTypeEnum.SeckillOrder.ToSbyte() == request.ProduceType)
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
                else if (ProductTypeEnum.UsePackageCard.ToSbyte() == request.ProduceType)
                {
                    products?.ForEach(_ =>
                    {
                        _.PackageOrProduct.Price = 0;
                        _.PackageOrProduct.TotalAmount = 0;
                    });
                }
                else
                {
                    ///美容团购修改价格支持门店价格
                    var shopGroupProducts = await shopClient.GetShopGrouponProduct(new GetShopGrouponProductRequest() { ShopId = request.ShopId, Status = 1 });

                    products?.ForEach(_ =>
                    {
                        var singleProduct = shopGroupProducts?.Data?.Where(item => item.ServiceId == _?.PackageOrProduct?.ProductId)?.FirstOrDefault();
                        if (singleProduct != null)
                        {
                            _.PackageOrProduct.Price = singleProduct.Price;
                            _.PackageOrProduct.TotalAmount = _.PackageOrProduct.TotalNumber * singleProduct.Price;
                        }
                    });
                }


                #endregion

                #region 金额计算
                int totalProductNum = 0;//商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
                decimal totalProductAmount = decimal.Zero;//商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
                int serviceNum = 0;//服务数量（指带出的安装服务，不含套餐内安装服务）
                decimal serviceFee = decimal.Zero;//服务费（指带出的安装服务，不含套餐内安装服务）
                decimal deliveryFee = decimal.Zero;//运费
                var calcCouponProducts = new List<Product>();//参与优惠计算的商品或服务集合

                foreach (var item in products)
                {
                    totalProductNum += item.PackageOrProduct.TotalNumber;
                    totalProductAmount += item.PackageOrProduct.TotalAmount;
                    var calcCouponProduct = new Product()
                    {
                        Pid = item.PackageOrProduct.ProductId,
                        Category = item.PackageOrProduct.MainCategoryCode,
                        Brand = item.PackageOrProduct.Brand,
                        Total = item.PackageOrProduct.Number,
                        UnitPrice = item.PackageOrProduct.Price
                    };
                    calcCouponProducts.Add(calcCouponProduct);
                }
                foreach (var item in services)
                {
                    serviceNum += item.TotalNumber;
                    serviceFee += item.TotalAmount;
                    var calcCouponProduct = new Product()
                    {
                        Pid = item.ProductId,
                        Category = item.MainCategoryCode,
                        Brand = item.Brand,
                        Total = item.TotalNumber,
                        UnitPrice = item.Price
                    };
                    calcCouponProducts.Add(calcCouponProduct);
                }
                decimal totalShouldPayAmount = totalProductAmount + serviceFee + deliveryFee;//应付总价

                #region 获取可用的优惠信息
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
                    PayMethod = (PayMethod)request.Payment,
                    TotalAmount = totalShouldPayAmount,
                    ProductList = calcCouponProducts
                };
                var getOrderApplicableCouponListResult = await couponClient.GetOrderApplicableCouponList(orderApplicableCouponListReqDTO);
                sw.Stop();
                dict.Add("获取可用的优惠信息", sw.ElapsedMilliseconds);
                sw.Restart();
                //logger.Info($"GetOrderApplicableCouponList orderApplicableCouponListReqDTO={JsonConvert.SerializeObject(orderApplicableCouponListReqDTO)} getOrderApplicableCouponListResult={JsonConvert.SerializeObject(getOrderApplicableCouponListResult)}");
                var orderApplicableCouponEntityResDTO = getOrderApplicableCouponListResult.GetSuccessData();
                if (orderApplicableCouponEntityResDTO != null)
                {
                    if (orderApplicableCouponEntityResDTO.OrderApplicableCoupon != null)
                    {
                        response.UserCouponId = orderApplicableCouponEntityResDTO.OrderApplicableCoupon.UserCouponId;
                        response.UserCouponDisplayName = orderApplicableCouponEntityResDTO.OrderApplicableCoupon.DisplayName;
                    }
                    if (orderApplicableCouponEntityResDTO.UserCouponId != null && orderApplicableCouponEntityResDTO.UserCouponId.Any())
                    {
                        response.AllApplicableCoupons = orderApplicableCouponEntityResDTO.UserCouponId;
                    }
                }

                decimal totalCouponAmount = decimal.Zero;//总优惠金额
                long userCouponId = 0;//当前参与计算的用户优惠券ID
                if (response.UserCouponId > 0)
                {
                    if (request.UserCouponId >= 0 && request.UserCouponId != response.UserCouponId)
                    {
                        userCouponId = request.UserCouponId;//如果客户选择了非推荐优惠券，以用户选择为准
                    }
                    else
                    {
                        userCouponId = response.UserCouponId;
                    }
                }
                if (userCouponId > 0)
                {
                    //获取优惠券具体信息
                    var getUserCouponEntityCustomByIdResult = await couponClient.GetUserCouponEntityCustomById(new UserCouponEntityReqByIdDTO() { UserCouponId = userCouponId });
                    var userCoupon = getUserCouponEntityCustomByIdResult.GetSuccessData();
                    if (userCoupon != null)
                    {
                        totalCouponAmount += userCoupon.Value;
                    }
                }
                #endregion

                decimal actualAmount = totalShouldPayAmount - totalCouponAmount;

                var userId = request.UserId;
                var getUserInfoResult = await userClient.GetUserInfo(new GetUserInfoRequest() { UserId = userId });
                var user = getUserInfoResult.GetSuccessData();
                if ((user?.MemberGrade ?? 0) == 50)//钻石折扣
                {
                    var diamondsDiscountRate = Convert.ToDecimal(_configuration["OrderConfig:DiamondsDiscountRate"]);
                    var diamondsDiscountAmount = Math.Ceiling((actualAmount * diamondsDiscountRate) * 100) / 100;//超出分的金额直接进位
                    response.DiamondsDiscountAmount = actualAmount - (diamondsDiscountAmount);
                    actualAmount = diamondsDiscountAmount;
                }

                response.TotalProductAmount = totalProductAmount;
                response.ServiceFee = serviceFee;
                response.DeliveryFee = deliveryFee;
                response.TotalCouponAmount = totalCouponAmount;
                response.ActualAmount = actualAmount;

                response.FreeShippingRuleDesc = string.Empty;
                #endregion
                sw.Stop();
                dict.Add("返回", sw.ElapsedMilliseconds);

                result = Result.Success(response, "计算成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("TrialCalcOrderAmountEx", ex);
            }
            finally
            {
                logger.Info($"TrialCalcOrderAmount request={JsonConvert.SerializeObject(request)} response={JsonConvert.SerializeObject(result)} dict={JsonConvert.SerializeObject(dict)}");
            }
            return result;
        }

        public async Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosRequest request)
        {
            var result = Result.Failed<GetOrderVerificationCodeInfosResponse>("加载异常，请重试");
            try
            {
                var list = await orderVerificationCodeRepository.GetListByUserIdAndOrderNo(request.UserId, request.OrderNo);
                if (list == null || !list.Any())
                {
                    return result;
                }
                var response = new GetOrderVerificationCodeInfosResponse()
                {
                    OrderVerificationCodeBaseInfos = new List<OrderVerificationCodeBaseInfoDTO>()
                };
                for (int i = 0; i < list.Count; i++)
                {
                    var code = list[i];
                    if (i == 0)
                    {
                        VerificationRuleInfoDTO ruleInfo = await GetVerificationRuleInfo(code);
                        response.VerificationRuleInfo = ruleInfo;
                    }
                    response.OrderVerificationCodeBaseInfos.Add(new OrderVerificationCodeBaseInfoDTO()
                    {
                        Code = code.Code,
                        Status = code.Status
                    });
                }
                result = Result.Success(response, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"GetOrderVerificationCodeInfosEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoRequest request)
        {
            var result = Result.Failed<OrderCarDTO>("查询失败");
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<OrderCarDTO>(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                var car = await orderCarRepository.GetOrderCar(order.Id);
                if (car == null)
                {
                    result = Result.Failed<OrderCarDTO>(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                var carDTO = mapper.Map<OrderCarDTO>(car);
                result = Result.Success(carDTO, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"GetCarByOrderNoEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<OrderUserDTO>> GetUserByOrderNo(GetUserByOrderNoRequest request)
        {
            var result = Result.Failed<OrderUserDTO>("查询失败");
            try
            {
                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    result = Result.Failed<OrderUserDTO>(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                var user = await orderUserRepository.GetOrderUser(order.Id);
                if (user == null)
                {
                    result = Result.Failed<OrderUserDTO>(ResultCode.SUCCESS_NOT_EXIST);
                    return result;
                }
                var userDTO = mapper.Map<OrderUserDTO>(user);
                result = Result.Success(userDTO, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"GetUserByOrderNoEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<ScanCodeResponse>> ScanCode(ScanCodeRequest request)
        {
            var result = Result.Failed<ScanCodeResponse>("扫码异常");
            try
            {
                var code = request.Code;
                if (string.IsNullOrWhiteSpace(code) || code.Length < 5)
                {
                    throw new CustomException("核销码或安装码格式不正确");
                }
                var channelString = request.ChannelKey;
                if (string.IsNullOrWhiteSpace(channelString))
                {
                    channelString = code.Substring(0, 2);
                }
                channelString = channelString.ToUpper();
                if (channelString != "RG")
                {
                    throw new CustomException("渠道错误");
                }
                var typeString = code.Substring(2, 2).ToUpper();
                if (typeString != "HX" && typeString != "AZ" && typeString != "TC")
                {
                    throw new CustomException("核销码或安装码类型不正确");
                }
                sbyte type = 0;
                switch (typeString)
                {
                    case "HX": type = 1; break;
                    case "AZ": type = 2; break;
                    case "TC": type = 3; break;
                    default:
                        break;
                }

                var response = new ScanCodeResponse()
                {
                    Channel = channelString,
                    Type = type
                };
                if (typeString == "HX")
                {
                    var codeDO = await orderVerificationCodeRepository.GetByCode(code);
                    if (codeDO == null)
                    {
                        throw new CustomException("核销码不存在");
                    }
                    if (codeDO.Status != 0)
                    {
                        throw new CustomException("核销码已使用或已过期");
                    }
                    if (codeDO.EndValidTime < DateTime.Now || codeDO.StartValidTime > DateTime.Now)
                    {
                        codeDO.Status = 2;
                        await orderVerificationCodeRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                        throw new CustomException("核销码使用时间已过期");
                    }

                    result = Result.Success(response, "扫码成功");
                }
                else if (typeString == "AZ")
                {

                    var orderDO = await _orderQueryClient.GetOrderBaseInfo(new Core.Request.OrderQuery.GetOrderBaseInfoClientRequest()
                    {
                        InstallCodes = new List<string>(1) { request.Code }
                    });
                    if (orderDO?.Data?.Count() == 0)
                    {
                        result = Result.Failed<ScanCodeResponse>("安装码信息异常");
                        return result;
                    }
                    if (orderDO?.Data?.FirstOrDefault()?.ShopId != request.ShopId)
                    {
                        result = Result.Failed<ScanCodeResponse>("非本门店订单，若需安装请让客户联系总部修改安装门店");
                        return result;
                    }

                    response.OrderNo = orderDO?.Data?.FirstOrDefault()?.OrderNo;
                    result = Result.Success(response, "扫码成功");
                }
                else if (typeString == "TC")
                {
                    var codeDO = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = request.Code }, true)).FirstOrDefault();
                    if (codeDO == null)
                    {
                        throw new CustomException("核销码不存在");
                    }
                    if (codeDO.Status != 0)
                    {
                        throw new CustomException("核销码已使用或已过期");
                    }
                    if (codeDO.EndValidTime < DateTime.Now || codeDO.StartValidTime > DateTime.Now)
                    {
                        codeDO.Status = 2;
                        await _orderPackageCardRepository.UpdateAsync(codeDO, new List<string>() { "Status" });
                        throw new CustomException("核销码使用时间已过期");
                    }

                    result = Result.Success(response, "扫码成功");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"ScanCodeEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetVerificationCodeOrderListResponse>> GetVerificationCodeOrderList(GetVerificationCodeOrderListRequest request)
        {
            var result = new ApiPagedResult<GetVerificationCodeOrderListResponse>() { Code = ResultCode.Failed, Message = "加载异常，请重试" };
            try
            {
                var pageList = await orderVerificationCodeRepository.GetListPagedByCondition(request);
                if (pageList != null && pageList.TotalItems > 0)
                {
                    result = new ApiPagedResult<GetVerificationCodeOrderListResponse>()
                    {
                        Code = ResultCode.Success,
                        Message = "查询成功",
                        Data = new ApiPagedResultData<GetVerificationCodeOrderListResponse>()
                        {
                            TotalItems = pageList.TotalItems,
                            Items = mapper.Map<List<GetVerificationCodeOrderListResponse>>(pageList.Items)
                        }
                    };
                }
                else
                {
                    result = new ApiPagedResult<GetVerificationCodeOrderListResponse>()
                    {
                        Code = ResultCode.Success,
                        Message = "查询成功",
                        Data = new ApiPagedResultData<GetVerificationCodeOrderListResponse>()
                        {
                            TotalItems = 0,
                            Items = new List<GetVerificationCodeOrderListResponse>()
                        }
                    };
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"GetVerificationCodeOrderListEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<GetVerificationCodeOrderDetailResponse>> GetVerificationCodeOrderDetail(GetVerificationCodeOrderDetailRequest request)
        {
            var result = Result.Failed<GetVerificationCodeOrderDetailResponse>("加载异常，请重试");
            try
            {
                if (request.Code.Contains("HX"))
                {
                    var model = await orderVerificationCodeRepository.GetByCode(request.Code);

                    if (model == null)
                    {
                        return result;
                    }
                    var response = mapper.Map<GetVerificationCodeOrderDetailResponse>(model);
                    result = Result.Success(response, "查询成功");
                }
                else if (request.Code.Contains("TC"))
                {
                    var model = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = request.Code }, true)).FirstOrDefault();

                    if (model == null)
                    {
                        return result;
                    }


                    var response = new GetVerificationCodeOrderDetailResponse()
                    {
                        Code = model.Code,
                        Status = model.Status,
                        UserPhone = model.UserPhone,
                        EndValidTime = model.EndValidTime,
                        ProductName = model.ProductName,
                        MarketingPrice = model.Price,
                        Number = model.Num,
                        Remark = model.Remark
                    };
                    result = Result.Success(response, "查询成功");
                }


            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"GetVerificationCodeOrderDetailEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss(GetOrderDetailForBossRequest request)
        {
            var result = Result.Failed<GetOrderDetailForBossResponse>("加载异常，请重试");
            try
            {
                var response = new GetOrderDetailForBossResponse();

                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                response.OrderInfo = mapper.Map<OrderDetailForBossOrderInfoDTO>(order);
                response.FinanceInfo = mapper.Map<OrderDetailForBossFinanceInfoDTO>(order);
                response.AmountInfo = mapper.Map<OrderDetailForBossAmountInfoDTO>(order);

                var user = await orderUserRepository.GetOrderUser(order.Id);
                if (user == null)
                {
                    throw new CustomException("用户信息异常");
                }
                response.UserInfo = mapper.Map<OrderDetailForBossUserInfoDTO>(user);
                var address = await orderAddressRepository.GetOrderAddress(order.Id);
                if (address == null)
                {
                    throw new CustomException("地址信息异常");
                }
                response.UserInfo.DisplayContactAddress = $"{address.Province}{address.City}{address.District}{address.DetailAddress}";
                var car = await orderCarRepository.GetOrderCar(order.Id);
                if (car == null)
                {
                    throw new CustomException("车辆信息异常");
                }
                response.UserInfo.CarId = car.CarId;
                response.UserInfo.CarNumber = car.CarNumber;
                response.UserInfo.DisplayCarName = $"{car.Brand} {car.Vehicle} {car.Nian} {car.PaiLiang} {car.SalesName}";//格式：品牌Brand 车系Vehicle 年份Nian 排量PaiLiang 款型SalesName
                response.UserInfo.TotalMileage = car.TotalMileage;

                var getPaysByOrderNoResult = await payClient.GetPaysByOrderNo(new GetPaysByOrderNoRequest() { OrderNo = request.OrderNo });
                var pays = getPaysByOrderNoResult.GetSuccessData();
                if (pays != null && pays.Any())
                {
                    var pay = pays.FirstOrDefault();
                    response.FinanceInfo.PayNo = pay.PayNo;
                    response.FinanceInfo.TradeNo = pay.TradeNo;
                    response.FinanceInfo.BuyerAccount = pay.BuyerAccount;
                }

                var products = await orderProductRepository.GetOrderProducts(order.Id);
                if (products == null || !products.Any())
                {
                    throw new CustomException("商品信息异常");
                }
                var productList = new List<OrderDetailForBossPackageProductDTO>();
                foreach (var item in products)
                {
                    if (item.ProductAttribute == 1)
                    {
                        var packageProduct = mapper.Map<OrderDetailForBossPackageProductDTO>(item);
                        var packageProducts = products.FindAll(_ => _.ParentOrderPackagePid == item.Id);
                        if (packageProducts != null && packageProducts.Any())
                        {
                            packageProduct.Children = mapper.Map<List<OrderDetailForBossProductDTO>>(packageProducts);
                            productList.Add(packageProduct);
                        }
                    }
                    else if (item.ParentOrderPackagePid == 0)
                    {
                        var product = mapper.Map<OrderDetailForBossPackageProductDTO>(item);
                        productList.Add(product);
                    }
                }
                response.ProductInfo = new OrderDetailForBossProductInfoDTO() { Products = productList };

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

        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListRequest request)
        {
            var result = Result.Success(new List<OrderLogDTO>(), 0);
            try
            {
                if (!request.OrderNo.StartsWith("RGC"))
                {
                    throw new CustomException("订单号异常");
                }
                var order = await orderRepository.GetOrder(request.OrderNo);
                if (order == null)
                {
                    throw new CustomException("订单信息异常");
                }
                var orderId = order.Id;
                var pageList = await orderLogRepository.GetListPagedAsync(request.PageIndex, request.PageSize, "where order_id = @OrderId", "id desc", new { OrderId = orderId });
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

        /// <summary>
        /// 得到订单服务方式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request)
        {
            ApiResult<GetOrderServiceTypeResponse> result;

            if (request.ProductType == ProductTypeEnum.DoorToDoor.ToInt())
            {
                result = Result.Success(new GetOrderServiceTypeResponse
                {
                    DoorToDoor = true,
                    ArrivalToShop = true
                    //GetOrderServiceType(OrderServiceTypeEnum.DoorToDoor,true),
                    // GetOrderServiceType(OrderServiceTypeEnum.ArrivalToShop,true),
                });
            }
            else
            {
                if (request.OrderType == OrderTypeEnum.Maintenance.ToInt())
                {
                    result = Result.Success(new GetOrderServiceTypeResponse
                    {
                        DoorToDoor = true,
                        ArrivalToShop = true
                        //GetOrderServiceType(OrderServiceTypeEnum.DoorToDoor,true),
                        // GetOrderServiceType(OrderServiceTypeEnum.ArrivalToShop,true),
                    });
                }
                else
                {
                    result = Result.Success(new GetOrderServiceTypeResponse
                    {
                        DoorToDoor = false,
                        ArrivalToShop = true
                        //GetOrderServiceType(OrderServiceTypeEnum.DoorToDoor,true),
                        // GetOrderServiceType(OrderServiceTypeEnum.ArrivalToShop,true),
                    });
                }
            }

            //var checkDoorProductResponse = await productSearchClient.CheckDoorProduct(new CheckDoorProductRequest()
            //{
            //    PidList = request.Pids
            //});
            //var isDoorToDoor = checkDoorProductResponse?.Data?.Any(_ => _.IsDoorProduct == true) ?? false;

            //result = new ApiResult<GetOrderServiceTypeResponse>()
            //{
            //    Code = ResultCode.Success,
            //    Data = new GetOrderServiceTypeResponse()
            //    {
            //        DoorToDoor = isDoorToDoor,
            //        ArrivalToShop = true
            //    }
            //};
            return await Task.FromResult(result);

        }

        public async Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request)
        {
            var orderPackageCode = (await _orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code", new { Code = request.Code }, true)).FirstOrDefault();

            return new ApiResult<GetPackageVerificationCodeDetailResponse>()
            {
                Code = ResultCode.Success,
                Data = new GetPackageVerificationCodeDetailResponse()
                {
                    ProductId = orderPackageCode?.ProductId,
                    ProductName = orderPackageCode?.ProductName,
                    StartValidTime = orderPackageCode?.StartValidTime ?? DateTime.Now,
                    EndValidTime = orderPackageCode?.EndValidTime ?? DateTime.Now,
                    Status = orderPackageCode?.Status ?? 0,
                    Code = orderPackageCode.Code,
                    QRCodeBase64String = $"data:image/jpg;base64,{QrCodeCreateHelper.GetQRCode(orderPackageCode.Code)}"
                }
            };
        }

        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request)
        {
            ApiResult<GetOrderServiceTypeResponse> result;

            var checkDoorProductResponse = await productSearchClient.CheckDoorProduct(new CheckDoorProductRequest()
            {
                PidList = request.Pids
            });
            var isDoorToDoor = checkDoorProductResponse?.Data?.Any(_ => _.IsDoorProduct == true) ?? false;

            result = new ApiResult<GetOrderServiceTypeResponse>()
            {
                Code = ResultCode.Success,
                Data = new GetOrderServiceTypeResponse()
                {
                    DoorToDoor = isDoorToDoor,
                    ArrivalToShop = true
                }
            };
            return await Task.FromResult(result);
        }

        ///// <summary>
        /////订单服务方式
        ///// </summary>
        ///// <param name="orderServiceTypeEnum"></param>
        ///// <param name="isOptional"></param>
        ///// <returns></returns>
        //private GetOrderServiceTypeResponse GetOrderServiceType(OrderServiceTypeEnum orderServiceTypeEnum,
        //    bool isOptional)
        //{
        //    return new GetOrderServiceTypeResponse()
        //    {
        //        Code = orderServiceTypeEnum.ToString(),
        //        Text = orderServiceTypeEnum.GetDescription(),
        //        IsOptional = isOptional
        //    };
        //}
    }
}
