using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Repository.ConsumerOrder;
using Ae.Order.Service.Dal.Repository.OrderProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Client.Request.Receive;
using Ae.Order.Service.Common.Extension;

namespace Ae.Order.Service.Imp.Services
{
    public class OrderQueryForMiniAppService : IOrderQueryForMiniAppService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryForMiniAppService> logger;
        private readonly IOrderForMiniAppRepository orderForMiniAppRepository;
        private readonly IOrderProductRepository orderProductRepository;
        private readonly IReceiveClient receiveClient;

        public OrderQueryForMiniAppService(IMapper mapper, ApolloErpLogger<OrderQueryForMiniAppService> logger,
            IOrderForMiniAppRepository orderForMiniAppRepository,
            IOrderProductRepository orderProductRepository,
            IReceiveClient receiveClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderForMiniAppRepository = orderForMiniAppRepository;
            this.orderProductRepository = orderProductRepository;
            this.receiveClient = receiveClient;
        }

        public async Task<ApiResult<int>> GetOrderCountForMiniApp(GetOrderCountForMiniAppRequest request)
        {
            var result = Result.Failed<int>("加载异常，请重试");
            try
            {
                var count = await orderForMiniAppRepository.GetOrderCountForMiniApp(request);
                result = Result.Success(count, "查询成功");
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderCountForMiniAppEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetOrderListForMiniAppResponse>> GetOrderListForMiniApp(GetOrderListForMiniAppRequest request)
        {
            var result = new ApiPagedResult<GetOrderListForMiniAppResponse>
            {
                Code = ResultCode.Success,
                Message = "查询成功",
                Data = new ApiPagedResultData<GetOrderListForMiniAppResponse> { Items = new List<GetOrderListForMiniAppResponse>() }
            };

            try
            {
                var orderPage = await orderForMiniAppRepository.GetOrderListForMiniApp(request);
                if (orderPage == null || orderPage.TotalItems <= 0)
                {
                    return result;
                }

                result.Data = new ApiPagedResultData<GetOrderListForMiniAppResponse>()
                {
                    TotalItems = orderPage.TotalItems,
                    Items = new List<GetOrderListForMiniAppResponse>()
                };

                if (orderPage.Items.Count <= 0) return result;

                var orderIds = orderPage.Items.Select(s => s.Id);
                var prodList = (await orderProductRepository.GetOrderProductsByOrderIds(orderIds)).ToList();
                if (!prodList.Any())
                {
                    return result;
                }

                var reserveOrderDic = GetReserveOrderInfoDic(orderPage.Items.Select(s => s.OrderNo).ToList());
                foreach (var item in orderPage.Items)
                {
                    var rowData = new GetOrderListForMiniAppResponse
                    {
                        OrderNo = item.OrderNo,
                        ReserveId = reserveOrderDic.ContainsKey(item.OrderNo) ? reserveOrderDic[item.OrderNo] : default,
                        Channel = (ChannelEnum)item.Channel,
                        ListTotalProductNum = item.TotalProductNum + item.ServiceNum,
                        DisplayOrderStatus = GetDisplayOrderStatus(item),
                        OrderUserOperations = GetOrderUserOperations(item, request.MiniAppType),
                        ActualAmount = item.ActualAmount
                    };

                    var prods = prodList.FindAll(p => p.OrderId == item.Id && p.ParentOrderPackagePid == 0);
                    if (prods.Count > 0)
                    {
                        rowData.IsCollapseShowProducts = prods.Count > 1;
                        rowData.Products = mapper.Map<List<OrderDetailProductDTO>>(prods);
                    }

                    result.Data.Items.Add(rowData);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderListForMiniAppEx", ex);
                result.Message = "加载异常，请重试";
            }

            return result;
        }

        public async Task<ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>> GetEachStatusOrderCountForMiniApp(GetEachStatusOrderCountForMiniAppRequest request)
        {
            var result = Result.Failed<List<GetEachStatusOrderCountForMiniAppResponse>>("加载异常，请重试");
            try
            {
                var list = new List<GetEachStatusOrderCountForMiniAppResponse>();
                for (sbyte i = 0; i < 6; i++)
                {
                    var getOrderCountForMiniAppRequest = new GetOrderCountForMiniAppRequest()
                    {
                        UserId = request.UserId,
                        OrderListStatus = i
                    };
                    var count = await orderForMiniAppRepository.GetOrderCountForMiniApp(getOrderCountForMiniAppRequest);
                    list.Add(new GetEachStatusOrderCountForMiniAppResponse() { OrderListStatus = i, Count = count });
                }
                result = Result.Success(list, "查询成功");
            }
            catch (Exception ex)
            {
                logger.Error("GetEachOrderCountForMiniAppEx", ex);
            }
            return result;
        }

        public Dictionary<string, long> GetReserveOrderInfoDic(List<string> orderList)
        {
            var res = new Dictionary<string, long>();

            var req = new GetReserveInfoByReserveIdOrOrderNum
            {
                OrderNumbers = orderList
            };

            var reserveTask = receiveClient.GetReserveInfoByReserveIdOrOrderNum(req);
            Task.WaitAll(reserveTask);

            reserveTask.Result.ForEach(f =>
            {
                if (!res.ContainsKey(f.OrderNo))
                {
                    res.Add(f.OrderNo, f.ReserveId);
                }
            });

            return res;
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
                    //if (order.ProduceType == 1)
                    //{
                    //    displayOrderStatus = "待安装";
                    //}
                    //else if (order.DeliveryType == 1)
                    //{
                    //if (order.DeliveryStatus == 1)
                    //{
                    //    if (order.SignStatus == 0)
                    //    {
                    //        displayOrderStatus = "待收货";
                    //    }
                    //    else
                    //    {
                    //        displayOrderStatus = "待安装";
                    //    }
                    //}
                    //else
                    //{
                    //    displayOrderStatus = "待发货";
                    //}
                    if (order.SignStatus == 0)
                    {
                        displayOrderStatus = "待收货";
                    }
                    else
                    {
                        displayOrderStatus = "待安装";
                    }
                    //}
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
            if (order.OrderStatus < (sbyte)OrderStatusEnum.Canceled && order.IsOccurReverse == 1 && order.ReverseStatus != (sbyte)ReverseStatusEnum.Canceled)
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
        private List<OrderUserOperationDTO> GetOrderUserOperations(OrderDO order, sbyte miniAppType)
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
                    if (order.ProduceType == 1)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    }
                    else
                    {
                        if (order.ReserveStatus == 0)
                            list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.ApplyReserve, IsImportance = true, Sort = 1 });
                    }
                    if (order.PayStatus == 0)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.CancelOrder, IsImportance = false, Sort = 1 });
                        if (order.PayMethod == 1)
                        {
                            list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.PayOrder, IsImportance = true, Sort = 2 });
                        }
                    }
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
                    break;
                case OrderStatusEnum.Completed:
                    if (order.CommentStatus == 0)
                    {
                        list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.CommentOrder, IsImportance = true, Sort = 1 });
                    }
                    //else if (order.CommentStatus == 1)
                    //{
                    //    list.Add(new OrderUserOperationDTO() { Function = OrderUserOperationEnum.AppendComment, IsImportance = true, Sort = 1 });
                    //}
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

        public async Task<ApiResult<List<GetOrderListForMiniAppResponse>>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request)
        {
            var result = Result.Failed(ResultCode.Failed, "查询异常，请重试", new List<GetOrderListForMiniAppResponse>());

            try
            {
                var items = new List<GetOrderListForMiniAppResponse>();
                var list = await orderForMiniAppRepository.BatchGetOrderListForMiniApp(request);

                if (list == null || list.Count <= 0)
                {
                    result = Result.Success(new List<GetOrderListForMiniAppResponse>(), "查询成功");
                    return result;
                }

                var orderIds = list.Select(s => s.Id);
                var prodList = (await orderProductRepository.GetOrderProductsByOrderIds(orderIds)).ToList();
                if (!prodList.Any())
                {
                    return result;
                }

                var reserveOrderDic = GetReserveOrderInfoDic(list.Select(s => s.OrderNo).ToList());
                foreach (var item in list)
                {
                    var rowData = new GetOrderListForMiniAppResponse
                    {
                        OrderNo = item.OrderNo,
                        ShopId = item.ShopId,
                        ReserveId = reserveOrderDic.ContainsKey(item.OrderNo) ? reserveOrderDic[item.OrderNo] : default,
                        Channel = (ChannelEnum)item.Channel,
                        ListTotalProductNum = item.TotalProductNum + item.ServiceNum,
                        DisplayOrderStatus = GetDisplayOrderStatus(item),
                        OrderUserOperations = GetOrderUserOperations(item, request.MiniAppType),
                        ActualAmount = item.ActualAmount
                    };

                    var prods = prodList.FindAll(p => p.OrderId == item.Id);
                    if (prods.Count > 0)
                    {
                        rowData.IsCollapseShowProducts = prods.Count > 1;
                        rowData.Products = mapper.Map<List<OrderDetailProductDTO>>(prods);
                    }

                    items.Add(rowData);
                }

                result = Result.Success(items, "查询成功");
            }
            catch (Exception ex)
            {
                logger.Error("BatchGetOrderListForMiniAppEx", ex);
            }

            return result;
        }

    }
}
