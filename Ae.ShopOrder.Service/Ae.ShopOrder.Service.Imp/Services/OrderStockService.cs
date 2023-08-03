using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Clients.Order;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Model.Stock;
using Ae.ShopOrder.Service.Core.Request.OrderForC;
using Ae.ShopOrder.Service.Core.Request.Stock;
using Ae.ShopOrder.Service.Core.Response.Stock;
using Ae.ShopOrder.Service.Dal.Repository;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ae.ShopOrder.Service.Imp.Services
{
    public class OrderStockService : IOrderStockService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderStockService> logger;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IOrderAddressRepository orderAddressRepository;
        private readonly IOrderQueryService orderQueryService;
        private readonly IOrderClient orderClient;

        public OrderStockService(IMapper mapper, ApolloErpLogger<OrderStockService> logger, IOrderRepository orderRepository, 
            IOrderProductRepository orderProductRepository, IOrderAddressRepository orderAddressRepository, IOrderClient orderCommandClient, IOrderQueryService orderQueryService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderAddressRepository = orderAddressRepository;
            this.orderClient = orderCommandClient;
            this.orderQueryService = orderQueryService;
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
                    if (item.ProductAttribute == 0||item.ProductAttribute==5)
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
                            //var updatProductStockStatusResult = await orderClient.UpdateProductStockStatus(new UpdateProductStockStatusRequest()
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

                    bool isUpdateOrderStockStatusSuccess = await orderRepository.UpdateOrderStockStatus(order.Id, orderStockStatus, "OrderUseStockNotify");
                    //var updateOrderStockStatusResult = await orderClient.UpdateOrderStockStatus(new UpdateOrderStockStatusRequest()
                    //{
                    //    OrderNo = order.OrderNo,
                    //    StockStatus = orderStockStatus,
                    //    UpdateBy = "OrderUseStockNotify"
                    //});
                    if (orderStockStatus == 2)
                    {
                        await orderRepository.UpdateOrderSignStatus(order.OrderNo, "OrderUseStockNotify");
                        //await orderClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                        //{
                        //    OrderNos = new List<string>() { order.OrderNo },
                        //    UpdateBy = "OrderUseStockNotify",
                        //    UpdateStatusType = UpdateOrderStatusTypeEnum.SignStatus
                        //});
                    }
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
                var order = await orderRepository.GetOrder(orderNo);
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
                    ShopId = order.ShopId,
                    Products = mapper.Map<List<UseStockOrderDetailPackageProductDTO>>(products),
                    ProduceType=order.ProduceType
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

        public async Task<ApiResult<QueryOrderDetailUseStockResponse>> QueryOrderRealProductDetail(QueryUseStockOrderDetailRequest request)
        {
            var result = Result.Failed<QueryOrderDetailUseStockResponse>();
            try
            {
                var orderNo = request.OrderNo;
                var order = await orderRepository.GetOrder(orderNo);
                if (order == null)
                {
                    result = Result.Failed<QueryOrderDetailUseStockResponse>(ResultCode.ILLEGAL_OPERATION, "订单不存在");
                    return result;
                }
                //商品信息

                var products = await orderProductRepository.GetOrderProducts(order.Id);
                if (products == null || !products.Any())
                {
                    result = Result.Failed<QueryOrderDetailUseStockResponse>(ResultCode.SUCCESS_NOT_EXIST, "商品不存在");
                    return result;
                }

                var response = new QueryOrderDetailUseStockResponse()
                {
                    OrderNo = order.OrderNo,
                    OrderChannel = (ChannelEnum)order.Channel,
                    OrderStatus = (OrderStatusEnum)order.OrderStatus,
                    PayStatus = order.PayStatus,
                    StockStatus = order.StockStatus,
                    ShopId = order.ShopId,
                    Products = mapper.Map<List<UseStockOrderDetailProductDTO>>(products.Where(t => t.ProductAttribute == 0 ||
                        t.ProductAttribute == 5)),//实物或外采
                    ProduceType = order.ProduceType
                };

                result = Result.Success(response, "查询成功");
            }
            catch (Exception ex)
            {
                logger.Error($"QueryOrderRealProductDetail_err", ex);
                result = Result.Exception<QueryOrderDetailUseStockResponse>();
            }
            return result;
        }
    }
}
