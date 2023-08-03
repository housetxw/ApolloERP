using AutoMapper;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Clients;
using Ae.ConsumerOrder.Service.Client.Clients.StockServer;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Common.Exceptions;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Dal.Repository;
using Ae.ConsumerOrder.Service.Imp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Ae.ConsumerOrder.Service.Imp.Services
{
    public class OrderStockService : IOrderStockService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderStockService> logger;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IOrderAddressRepository orderAddressRepository;
        private readonly IOrderQueryService orderQueryService;
        private readonly IStockClient stockClient;
        private readonly IOrderCommandClient orderCommandClient;
        private readonly IShopStockClient _shopStockClient;

        public OrderStockService(IMapper mapper, ApolloErpLogger<OrderStockService> logger, IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository, IOrderAddressRepository orderAddressRepository,
            IStockClient stockClient, IOrderCommandClient orderCommandClient, IOrderQueryService orderQueryService, IShopStockClient shopStockClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderAddressRepository = orderAddressRepository;
            this.stockClient = stockClient;
            this.orderCommandClient = orderCommandClient;
            this.orderQueryService = orderQueryService;
            _shopStockClient = shopStockClient;
        }

        public async Task<ApiResult> OrderUseStockNotify(OrderUseStockNotifyRequest request)
        {
            var result = Result.Failed();
            var orderNo = string.Empty;
            try
            {
                orderNo = request.OrderNo;
                var useStockDetails = request.UseStockDetails;
                if (string.IsNullOrWhiteSpace(orderNo) || useStockDetails == null || !useStockDetails.Any())
                {
                    result = Result.Failed(ResultCode.PARAM_ERROR);
                }

                //查询订单商品列表
                var order = await orderRepository.GetOrder(orderNo);
                var products = await orderProductRepository.GetOrderProducts(order.Id);
                var needStockPids = new List<long>();
                foreach (var item in products)
                {
                    if (item.ProductAttribute == 0)
                    {
                        needStockPids.Add(item.Id);
                    }
                }
                var stockedPids = new List<long>();
                var releasedPids = new List<long>();
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //更新商品库存状态 商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
                    foreach (var item in useStockDetails)
                    {
                        var findProduct = products.Find(_ => _.Id == item.Id);
                        if (findProduct != null)
                        {
                            if (item.StockStatus == 2)
                            {
                                stockedPids.Add(findProduct.Id);
                            }
                            else if (item.StockStatus == 4)
                            {
                                stockedPids.Add(findProduct.Id);
                            }

                            bool isUpdateProductStockStatusSuccess = await orderProductRepository.UpdateProductStockStatus(order.Id, item.StockStatus, "OrderUseStockNotify", new List<long>() { findProduct.Id });
                            //var updatProductStockStatusResult = await orderCommandClient.UpdateProductStockStatus(new UpdateProductStockStatusRequest()
                            //{
                            //    OrderNo = order.OrderNo,
                            //    StockStatus = item.StockStatus,
                            //    UpdateBy = "OrderUseStockNotify",
                            //    OrderPids = new List<long>() { findProduct.Id }
                            //});
                            //logger.Info($"OrderUseStockNotify OrderNo={order.OrderNo} OrderPid={findProduct.Id} 更新商品库存状态 isUpdateProductStockStatusSuccess={isUpdateProductStockStatusSuccess} updatProductStockStatusResult={JsonConvert.SerializeObject(updatProductStockStatusResult)}");

                            //if (!updatProductStockStatusResult.IsNotNullSuccess())
                            //{
                            //    throw new Exception("UpdateProductStockStatus同步失败");
                            //}

                            ////占库成功更新成本价
                            //if (item.StockStatus == 2)
                            //{
                            //    if (item.Batches != null && item.Batches.Any())
                            //    {
                            //        var totalCostPrice = decimal.Zero;
                            //        foreach (var batch in item.Batches)
                            //        {
                            //            totalCostPrice += batch.CostPrice * batch.Number;
                            //        }
                            //        bool isUpdateProductTotalCostPriceSuccess = await orderProductRepository.UpdateProductTotalCostPrice(order.Id, findProduct.Id, totalCostPrice, "OrderUseStockNotify");
                            //        var updateProductTotalCostPriceResult = await orderCommandClient.UpdateProductTotalCostPrice(new UpdateProductTotalCostPriceRequest()
                            //        {
                            //            OrderNo = order.OrderNo,
                            //            OrderPid = findProduct.Id,
                            //            TotalCostPrice = totalCostPrice,
                            //            UpdateBy = "OrderUseStockNotify"
                            //        });
                            //        logger.Info($"OrderUseStockNotify OrderNo={order.OrderNo} OrderPid={findProduct.Id} 占库成功更新成本价 isUpdateProductTotalCostPriceSuccess={isUpdateProductTotalCostPriceSuccess} updateProductTotalCostPriceResult={JsonConvert.SerializeObject(updateProductTotalCostPriceResult)}");
                            //    }
                            //}
                        }
                    }
                    //更新订单库存状态 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
                    sbyte orderStockStatus = order.StockStatus;
                    if (needStockPids.Count == stockedPids.Count)
                    {
                        orderStockStatus = 2;//全部占库成功
                    }
                    else if (needStockPids.Count == releasedPids.Count)
                    {
                        orderStockStatus = 4;//全部释放成功
                    }

                    var proxyGenerator = new ProxyGenerator();
                    var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                    {
                        OrderId = order.Id,
                        CreateBy = "系统",
                        UpdateBy = "系统",
                        Type1 = OrderLoggerTypeEnum.Order.ToString(),
                        Type2 = OrderLoggerTypeEnum.UseStock.ToString(),
                        Filter1 = order.OrderNo,
                        Filter2 = string.Empty,
                        Summary = "占库通知"
                    }));
                    bool isUpdateOrderStockStatusSuccess = await orderLoggerRepository.UpdateOrderStockStatus(order.Id, orderStockStatus, "OrderUseStockNotify");

                    if (orderStockStatus == 2)
                    {
                        await orderRepository.UpdateOrderSignStatus(order.OrderNo, "OrderUseStockNotify");
                    }
                    //var updateOrderStockStatusResult = await orderCommandClient.UpdateOrderStockStatus(new UpdateOrderStockStatusRequest()
                    //{
                    //    OrderNo = order.OrderNo,
                    //    StockStatus = orderStockStatus,
                    //    UpdateBy = "OrderUseStockNotify"
                    //});
                    //logger.Info($"OrderUseStockNotify OrderNo={order.OrderNo} 更新订单库存状态 isUpdateOrderStockStatusSuccess={isUpdateOrderStockStatusSuccess} updateOrderStockStatusResult={JsonConvert.SerializeObject(updateOrderStockStatusResult)}");

                    //if (!updateOrderStockStatusResult.IsNotNullSuccess())
                    //{
                    //    throw new Exception("UpdateOrderStockStatus同步失败");
                    //}

                    ts.Complete();
                }

                result = Result.Success("处理成功");
            }
            catch (Exception ex)
            {
                logger.Error($"OrderUseStockNotifyEx", ex);
                result = Result.Exception(ex.Message);
            }
            finally
            {
                logger.Info($"OrderUseStockNotify orderNo={orderNo} request={JsonConvert.SerializeObject(request)} result={JsonConvert.SerializeObject(result)}");
            }
            return result;
        }

        public async Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail(QueryUseStockOrderDetailRequest request)
        {
            var result = Result.Failed<QueryUseStockOrderDetailResponse>();
            try
            {
                var orderNo = request.OrderNo;
                var order = await orderRepository.GetOrder(orderNo,true);
                if (order == null)
                {
                    result = Result.Failed<QueryUseStockOrderDetailResponse>(ResultCode.ILLEGAL_OPERATION, "订单不存在");
                    return result;
                }
                //商品信息
                var getOrderDetailPackageProductServicesResponse = await orderQueryService.GetOrderDetailPackageProductServices(order.Id);
                var products = getOrderDetailPackageProductServicesResponse.Products;
                if (products == null || !products.Any())
                {
                    result = Result.Failed<QueryUseStockOrderDetailResponse>(ResultCode.SUCCESS_NOT_EXIST, "商品不存在");
                    return result;
                }
                var response = new QueryUseStockOrderDetailResponse()
                {
                    OrderNo = order.OrderNo,
                    OrderChannel = (ChannelEnum)order.Channel,
                    OrderStatus = (OrderStatusEnum)order.OrderStatus,
                    PayStatus = order.PayStatus,
                    StockStatus = order.StockStatus,
                    Products = mapper.Map<List<UseStockOrderDetailPackageProductDTO>>(products)
                };
                var orderAddress = await orderAddressRepository.GetOrderAddress(order.Id);
                if (orderAddress != null)
                {
                    response.OrderAddress = mapper.Map<OrderAddressDTO>(orderAddress);
                }
                result = Result.Success(response, "查询成功");
            }
            catch (Exception ex)
            {
                logger.Error($"QueryUseStockOrderDetailEx", ex);
                result = Result.Exception<QueryUseStockOrderDetailResponse>();
            }
            return result;
        }

        public async Task<ApiResult> SendOrderReleaseStock(SendOrderReleaseStockRequest request)
        {
            var result = Result.Failed("发送失败");
            try
            {
                var clientRequest = mapper.Map<SendOrderReleaseStockClientRequest>(request);
                var clientResult = await stockClient.SendOrderReleaseStock(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success("发送成功");
                }
                else
                {
                    logger.Warn($"SendOrderReleaseStock OrderNo={request.OrderNo} request={JsonConvert.SerializeObject(request)} clientResult={JsonConvert.SerializeObject(clientResult)}");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"SendOrderReleaseStockEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> SendOrderUseStock(SendOrderUseStockRequest request)
        {
            var result = Result.Failed("发送失败");
            try
            {
                var clientRequest = mapper.Map<SendOrderUseStockClientRequest>(request);
                var order = await orderRepository.GetOrder(request.OrderNo);
                var clientResult = default(ApiResult);
                //if (order.InstallType == 2)
                //{
                //    clientResult = await _shopStockClient.OrderOccupyStock(new Client.Request.Stock.OrderOccupyStockRequest()
                //    {
                //        QueueNo = request.OrderNo,
                //        CreateBy = order.CreateBy,
                //        QueueStatus = "OrderService"
                //    });
                //}
                //else
                //{
                //    clientResult = await stockClient.SendOrderUseStock(clientRequest);
                //}

                //业务需要全部占门店库存
                clientResult = await _shopStockClient.OrderOccupyStock(new Client.Request.Stock.OrderOccupyStockRequest()
                {
                    QueueNo = request.OrderNo,
                    CreateBy = order.CreateBy,
                    QueueStatus = "OrderService"
                });


                // var clientResult = await stockClient.SendOrderUseStock(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    // var order = await orderRepository.GetOrder(request.OrderNo);
                    if (order == null)
                    {
                        result = Result.Failed("订单不存在");
                        return result;
                    }
                    if (order.ProduceType == 1)
                    {
                        result = Result.Success("套餐卡核销码订单无需占库");
                        return result;
                    }

                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        //更新占库状态为占库中
                        var proxyGenerator = new ProxyGenerator();
                        var orderLoggerRepository = proxyGenerator.CreateInterfaceProxyWithTarget(orderRepository, new OrderLoggerInterceptor(new OrderLoggerRequest()
                        {
                            OrderId = order.Id,
                            CreateBy = request.UpdateBy,
                            UpdateBy = request.UpdateBy,
                            Type1 = OrderLoggerTypeEnum.Order.ToString(),
                            Type2 = OrderLoggerTypeEnum.UseStock.ToString(),
                            Filter1 = order.OrderNo,
                            Filter2 = string.Empty,
                            Summary = "发起占库"
                        }));
                        bool isUpdateOrderStockStatusSuccess = await orderLoggerRepository.UpdateOrderStockStatus(order.Id, 1, request.UpdateBy);
                        bool isUpdateProductStockStatusSuccess = await orderProductRepository.UpdateProductStockStatus(order.Id, 1, request.UpdateBy);
                        logger.Info($"SendOrderUseStock OrderNo={request.OrderNo} isUpdateOrderStockStatusSuccess={isUpdateOrderStockStatusSuccess} isUpdateProductStockStatusSuccess={isUpdateProductStockStatusSuccess}");

                        //通知主订单系统修改相应状态
                        //var updateOrderStockStatusResult = await orderCommandClient.UpdateOrderStockStatus(new UpdateOrderStockStatusRequest()
                        //{
                        //    OrderNo = request.OrderNo,
                        //    StockStatus = 1,
                        //    UpdateBy = request.UpdateBy
                        //});
                        //var updateProductStockStatusResult = await orderCommandClient.UpdateProductStockStatus(new UpdateProductStockStatusRequest()
                        //{
                        //    OrderNo = request.OrderNo,
                        //    StockStatus = 1,
                        //    UpdateBy = request.UpdateBy,
                        //    OrderPids = null
                        //});
                        //logger.Info($"SendOrderUseStock OrderNo={request.OrderNo} updateOrderStockStatusResult={JsonConvert.SerializeObject(updateOrderStockStatusResult)} updateProductStockStatusResult={JsonConvert.SerializeObject(updateProductStockStatusResult)}");
                        //if (!updateOrderStockStatusResult.IsNotNullSuccess() || !updateProductStockStatusResult.IsNotNullSuccess())
                        //{
                        //    throw new Exception($"发起占库后通知主订单系统修改相应状态失败 OrderNo={request.OrderNo} updateOrderStockStatusResult={JsonConvert.SerializeObject(updateOrderStockStatusResult)} updateProductStockStatusResult={JsonConvert.SerializeObject(updateProductStockStatusResult)} ");
                        //}

                        ts.Complete();
                    }

                    result = Result.Success("发送成功");
                }
                else
                {
                    logger.Info($"调用库存服务发起占库失败clientResult={JsonConvert.SerializeObject(clientResult)}");
                   // throw new Exception($"调用库存服务发起占库失败clientResult={JsonConvert.SerializeObject(clientResult)}");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"SendOrderUseStockEx", ex);
            }
            return result;
        }
    }
}
