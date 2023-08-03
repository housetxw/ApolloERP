using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Clients.Order;
using Ae.ShopOrder.Service.Client.Clients.ShopServer;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Common.Exceptions;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
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
    public class EmployeePerformanceService : IEmployeePerformanceService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<EmployeePerformanceService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IOrderDispatchRepository _orderDispatchRepository;
        private readonly IEmployeePerformanceOrderRepository _employeePerformanceOrderRepository;
        private readonly IOrderQueryService _orderQueryService;
        private readonly IOrderClient _orderClient;
        private readonly IShopClient _shopClient;

        public EmployeePerformanceService(IMapper mapper, ApolloErpLogger<EmployeePerformanceService> logger, IOrderRepository orderRepository, IOrderDispatchRepository orderDispatchRepository,
            IOrderProductRepository orderProductRepository,  IOrderClient orderCommandClient, IShopClient shopClient, IEmployeePerformanceOrderRepository employeePerformanceOrderRepository,
            IOrderQueryService orderQueryService)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._orderRepository = orderRepository;
            this._orderProductRepository = orderProductRepository;
            this._orderDispatchRepository = orderDispatchRepository;
            this._employeePerformanceOrderRepository = employeePerformanceOrderRepository;
            this._orderClient = orderCommandClient;
            this._shopClient = shopClient;
            this._orderQueryService = orderQueryService;
        }

        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeePerformanceOrderDTO>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request)
        {
            _logger.Info($"GetEmployeePerformanceOrderList Data:{JsonConvert.SerializeObject(request)}");
            var result = await _employeePerformanceOrderRepository.GetEmployeePerformanceOrderList(request);
            return _mapper.Map<List<EmployeePerformanceOrderDTO>>(result);

        }

        /// <summary>
        /// 更新订单绩效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderPerformance(BatchUpdateOrderRequest request)
        {
            _logger.Info($"UpdateOrderPerformance：{JsonConvert.SerializeObject(request)} ");
            var result = Result.Success("处理成功");
            try
            {
                if (request.ShopId == 0)
                {
                    throw new CustomException("门店未选择");
                }

                //取绩效配置
                var getShopPerformanceConfigs = await _shopClient.GetShopPerformanceConfig(new GetShopRequest()
                {
                    ShopId = request.ShopId
                });
                var shopPerformanceConfigs = getShopPerformanceConfigs.Data;
                if (shopPerformanceConfigs?.Count() == 0 )
                {
                    throw new CustomException("门店未配置绩效");
                }

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

                //所有订单，包含取消的
                var orderList = await _orderRepository.GetSimpleOrderList(new GetOrderListRequest()
                {
                    ShopId = request.ShopId,
                    OrderNos = request.OrderNos,
                    StartDate = startDateTime,
                    EndDate = endDateTime
                });
                _logger.Info($"orderList：{JsonConvert.SerializeObject(orderList)} ");

                var orderNos = new List<string>();

                if (orderList != null && orderList.Any())
                {
                    orderNos = orderList.Select(x => x.OrderNo).ToList();
                }
                else
                {
                    return result;
                }

                //先删除原所有绩效
                var delCount = await _employeePerformanceOrderRepository.DeleteShopPerformanceConfig(new BatchUpdateCompleteStatusRequest()
                {
                    ShopId = request.ShopId,
                    UpdateBy = request.UpdateBy,
                    OrderNo = orderNos
                });

                //已完成订单
                var orders = orderList.Where(x => x.OrderStatus == 30).ToList();

                if (orders != null && orders.Any())
                {
                    orderNos = orders.Select(x => x.OrderNo).ToList();
                }
                else
                {
                    return result;
                }

                //取订单派工
                var dispatchOrders = await _orderDispatchRepository.GetOrderDispatch(new GetOrderDispatchRequest() { 
                    OrderNos = orderNos,
                    ShopId = request.ShopId
                });
                //取订单明细
                var orderProducts = await _orderProductRepository.GetOrderProducts(new GetOrderBaseInfoRequest() { 
                    OrderNos = orderNos,
                    ShopId = (Int32) request.ShopId
                });

                if (shopPerformanceConfigs?.Count() == 0 || dispatchOrders?.Count() == 0 || orderProducts?.Count() == 0)
                {
                    return result;
                }
                foreach (var item in shopPerformanceConfigs)
                { 
                    item.TypeValueList = item.TypeValue.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                foreach (var product in orderProducts)
                {
                    //套餐只按整体计算，不算套餐明细
                    if (product.ParentOrderPackagePid > 0) { continue; }

                    var orderInfo = orders.Where(_ => _.OrderNo == product.OrderNo)?.FirstOrDefault();

                    foreach (var dispatch in dispatchOrders.Where(_ => _.OrderNo == product.OrderNo)?.ToList())
                    {
                        var epo = new EmployeePerformanceOrderDO()
                        {
                            ShopId = request.ShopId,
                            EmployeeId = dispatch.TechId,
                            EmployeeName = dispatch.TechName,
                            OrderNo = dispatch.OrderNo,
                            InstallTime = orderInfo?.InstallTime?? new DateTime(1900, 1, 1),
                            OrderType = orderInfo?.OrderType??0,
                            ProduceType = orderInfo?.ProduceType??0,
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            TotalNumber = product.TotalNumber,
                            TotalAmount = product.TotalAmount,
                            ShareDiscountAmount = product.ShareDiscountAmount,
                            ActualPayAmount = product.ActualPayAmount,
                            TotalCostPrice = product.TotalCostPrice,
                            SettlementAmount = product.SettlementAmount,
                            Maoli = product.ActualPayAmount - product.TotalCostPrice,
                            Percent = dispatch.Percent,

                            IsDeleted = 0,
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now
                        };
                        //计算绩效
                        decimal amount = ((epo.Maoli > epo.SettlementAmount || epo.SettlementAmount == 0) ? epo.Maoli : epo.SettlementAmount) * dispatch.Percent;
                        decimal number = (epo.TotalNumber) * dispatch.Percent;

                        var config = new ShopPerformanceConfigDTO() { Id = 0};
                        //先算单品
                        if (shopPerformanceConfigs.Where(_=>_.PerformanceType == 1 && _.TypeValueList.Contains(product.ProductId)).Count() > 0)
                        {
                            config = shopPerformanceConfigs.Where(_ => _.PerformanceType == 1 && _.TypeValueList.Contains(product.ProductId)).FirstOrDefault();
                        }//再算品类
                        else if (shopPerformanceConfigs.Where(_ => _.PerformanceType == 2 && _.TypeValueList.Contains(product.CategoryCode)).Count() > 0)
                        {
                            config = shopPerformanceConfigs.Where(_ => _.PerformanceType == 2 && _.TypeValueList.Contains(product.CategoryCode)).FirstOrDefault();
                        }//最后按默认算
                        else if (shopPerformanceConfigs.Where(_ => _.PerformanceType == 0).Count() > 0)
                        {
                            config = shopPerformanceConfigs.Where(_ => _.PerformanceType == 0).FirstOrDefault();
                        }

                        decimal installPoint = 0;
                        if (config.Id > 0)
                        {
                            if (config.ConfigType == 1)
                            {
                                installPoint = amount * config.ConfigPoint;
                                epo.TabType = 3;
                            }
                            else if (config.ConfigType == 2)
                            {
                                installPoint = number * config.ConfigPoint;
                                epo.TabType = 2;
                            }
                            epo.PerformanceType = config.PerformanceType;
                            epo.TypeValue = config.TypeValue;
                            epo.ConfigType = config.ConfigType;
                            epo.ConfigPoint = config.ConfigPoint;
                        }
                        epo.InstallPoint = installPoint;
                        epo.SalePoint = 0;
                        epo.TotalPoint = installPoint;

                        if (epo.ProduceType == 14)
                        {
                            epo.TabType = 1;
                        }

                        await _employeePerformanceOrderRepository.InsertAsync(epo);
                    }

                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Exception;
                result.Message = ex.Message;
                _logger.Error($"UpdateOrderPerformance Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return result;
        }

    }
}
