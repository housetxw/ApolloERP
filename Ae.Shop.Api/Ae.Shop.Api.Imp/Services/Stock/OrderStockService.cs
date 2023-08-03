using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Dal.Model;
using System.Linq;
using Ae.Shop.Api.Client.Clients.Order;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Model.Order;
using Dapper;
namespace Ae.Shop.Api.Imp.Services
{
    public class OrderStockService : IOrderStockService
    {
        #region
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<OrderStockService> _logger;
        private readonly IOccupyItemRepository _occupyItemRepository;
        private readonly IOccupyQueueRepository _occupyQueueRepository;
        private readonly IIdentityService _identityService;
        private readonly IOrderClient _orderClient;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly IStockInoutRepository _stockInoutRepository;
        private readonly IStockInoutItemRepository _stockInoutItemRepository;
        private readonly IStockManageService _stockManageService;
        private readonly IInventoryFlowItemRepository _inventoryFlowItemRepository;
        private readonly IOccupyStockLogRepository _occupyStockLogRepository;
        private readonly IHomeCareService _homeCareService;
        private readonly IWmsClient _wmsClient;

        public OrderStockService(IMapper mapper, ApolloErpLogger<OrderStockService> logger,
           IOccupyItemRepository occupyItemRepository, IOccupyQueueRepository occupyQueueRepository,
           IIdentityService identityService,
           IOrderClient orderClient,
           IShopMangeClient shopMangeClient, IStockInoutRepository stockInoutRepository,
           IStockInoutItemRepository stockInoutItemRepository,
           IStockManageService stockManageService,
           IInventoryFlowItemRepository inventoryFlowItemRepository,
           IOccupyStockLogRepository occupyStockLogRepository, IHomeCareService homeCareService,
           IWmsClient wmsClient)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._occupyItemRepository = occupyItemRepository;
            this._occupyQueueRepository = occupyQueueRepository;
            this._identityService = identityService;
            this._orderClient = orderClient;
            this._shopMangeClient = shopMangeClient;
            this._stockInoutRepository = stockInoutRepository;
            this._stockInoutItemRepository = stockInoutItemRepository;
            this._stockManageService = stockManageService;
            this._inventoryFlowItemRepository = inventoryFlowItemRepository;
            this._occupyStockLogRepository = occupyStockLogRepository;
            this._homeCareService = homeCareService;
            this._wmsClient = wmsClient;
        }

        #endregion

        /// <summary>
        /// 门店获取占库记录
        /// </summary>
        /// <returns></returns>
        public async Task<ApiPagedResult<OccupyQueueDTO>> GetOrderQueues(GetOrderQueueRequest request)
        {
            var res = new ApiPagedResult<OccupyQueueDTO>();
            try
            {
                var param = new DynamicParameters();
                var condtions = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(request.QueueStatus))
                {
                    condtions.Append(" and status=@status");
                    param.Add("@status", request.QueueStatus);
                }
                if (!string.IsNullOrWhiteSpace(request.QueueNo))
                {
                    condtions.Append(" and source_no=@source_no");
                    param.Add("@source_no", request.QueueNo);
                }

                var result = await _occupyQueueRepository.GetListPagedAsync(request.PageIndex, request.PageSize, " where is_deleted=0 " + condtions.ToString(), "id desc", param);
                var vo = _mapper.Map<ApiPagedResultData<OccupyQueueDTO>>(result);

                res.Data = vo;
                res.Code = ResultCode.Success;
                res.Message = "查询成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetOrderQueues_Error", ex);
            }
            return res;
        }

        #region 订单安装扣减锁定库存

        /// <summary>
        /// 订单安装扣减锁定库存
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderInstallReduceStock(OrderStockRequest request)
        {
            _logger.Info($"OrderInstallReduceStock_Data:{JsonConvert.SerializeObject(request)}");
            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? _identityService.GetUserName() : request.CreateBy;
            request.CreateBy = createBy;

            var res = new ApiResult<string>();
            try
            {
                await _stockManageService.OrderInstallReduceStock(request);

                //上门养车的门店才扣减记录

                //通知上门养车记录
                await _homeCareService.OrderInstallNotify(new HomeCareOrderDTO
                {
                    OrderNo = request.QueueNo,
                    CreateBy = createBy
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderOccupyStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 订单检查库存重新出库
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderRepeatReduceStock(OrderStockRequest request)
        {
            _logger.Info($"OrderRepeatReduceStock:{JsonConvert.SerializeObject(request)}");
            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? _identityService.GetUserName() : request.CreateBy;
            request.CreateBy = createBy;

            var res = new ApiResult<string>();
            try
            {
                await _stockManageService.OrderRepeatReduceStock(request);

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderRepeatReduceStock Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        #endregion

        #region 订单占用库存

        /// <summary>
        /// 订单占用库存
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderOccupyStock(OrderStockRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.QueueNo))
                {
                    res = await SendOrderQueue(request); //生成占库记录
                    if (res.Code != ResultCode.Success)
                    {
                        return res;
                    }

                    var result = await _occupyQueueRepository.GetOrderQueues(new OccupyQueueDO
                    {
                        SourceNo = request.QueueNo
                    });

                    if (result.Any())
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = request.QueueNo,
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"OrderOccupyStock_开始执行订单【{request.QueueNo}】的占库流程!",
                            LogLevel = LogLevelEnum.Info.ToString()
                        });
                        //单个订单的占用
                        var stockRes = await OrderStockMain(result.First());
                        if (stockRes.Code == ResultCode.Success)
                        {
                            res.Code = ResultCode.Success;
                            res.Message = $"占库结束,返回消息:{stockRes.Message}";
                        }
                        else
                        {
                            res.Code = ResultCode.Exception;
                            res.Message = $"占库发生异常,返回消息:{stockRes.Message}";
                        }
                    }
                    else
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = request.QueueNo,
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"OrderOccupyStock_订单【{request.QueueNo}】添加占库通知记录后查询为空!",
                            LogLevel = LogLevelEnum.Error.ToString()
                        });
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(request.QueueStatus))
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = "-1",
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"OrderOccupyStock_开始执行状态【{request.QueueStatus}】订单的占库流程!",
                            LogLevel = LogLevelEnum.Info.ToString()
                        });

                        await BeginOrderStock(request.QueueStatus);
                        res.Code = ResultCode.Success;
                        res.Message = "占库结束!";
                    }
                    else
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = "-1",
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"OrderOccupyStock_开始执行状态【新建/未占库】订单的占库流程!",
                            LogLevel = LogLevelEnum.Info.ToString()
                        });

                        //开始循环未占用的记录  频率快
                        await BeginOrderStock(OrderQueueStatusEnum.New.ToString());

                        //开始循环未占用成功的记录 频率慢
                        await BeginOrderStock(OrderQueueStatusEnum.UnOccupy.ToString());
                        res.Code = ResultCode.Success;
                        res.Message = "占库结束!";
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderOccupyStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> BeginOrderStock(string queueStatus)
        {
            var res = new ApiResult<string>();

            var queueList = await _occupyQueueRepository.GetOrderQueues(new OccupyQueueDO
            {
                Status = queueStatus
            });
            if (queueList.Any())
            {
                foreach (var item in queueList)
                {
                    if (item.Status == OrderQueueStatusEnum.UnOccupy.ToString())
                    {
                        if (DateTime.Now.Subtract(item.UpdateTime).TotalMinutes > 3)
                        {
                            await OrderStockMain(item);
                        }
                    }
                    else if (item.Status == OrderQueueStatusEnum.New.ToString())
                    {
                        await OrderStockMain(item);
                    }
                }
            }
            else
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = "-1",
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"BeginOrderStock_暂无状态【{queueStatus}】的占库通知记录!",
                    LogLevel = LogLevelEnum.Warning.ToString()
                });
            }
            return res;
        }

        /// <summary>
        /// 占库核心入口
        /// </summary>
        /// <param name="queueDO"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderStockMain(OccupyQueueDO queueDO)
        {
            await CreateOrderStockLog(new OccupyStockLogDO
            {
                ObjectNo = queueDO.SourceNo,
                Type = OrderQueueTypeEnum.OrderService.ToString(),
                Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】,状态【{queueDO.Status}】开始执行占库主逻辑!",
                LogLevel = LogLevelEnum.Info.ToString()
            });

            var res = new ApiResult<string>
            {
                Code = ResultCode.Exception
            };
            var orderInfoResponse = await _orderClient.QueryUseStockOrderDetail(new QueryUseStockOrderDetailClientRequest { OrderNo = queueDO.SourceNo });

            //订单产品存在铺货+外采的SKU
            var orderCheckRes = await CheckOrderValid(orderInfoResponse, queueDO);
            if (orderCheckRes.Code != ResultCode.Success)
            {
                return orderCheckRes;
            }

            var orderInfo = orderInfoResponse.Data;
            try
            {
                #region 更新占库通知的处理状态
                await _occupyQueueRepository.UpdateOrderProcessStatus(new OccupyQueueDO
                {
                    Id = queueDO.Id,
                    IsProcessing = OrderQueueProcessStatusEnum.Processing.ToString(),
                    UpdateBy = "占库系统"
                });

                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的处理状态更新为【处理中】!",
                    BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}",
                    LogLevel = LogLevelEnum.Info.ToString()
                });
                #endregion

                //TODO 根据订单的区域获取默认仓和备选仓的记录(暂时没有)


                //TODO 判断几个仓库的库存是否充足 一个一个判断 可能会涉及到拆单(暂时没有)

                //获取所有实物产品的占用情况
                WarehouseProductOccupyStockDTO warehouseProductOccupy = await HandleOrderOccupyStock(orderInfo);


                //TODO 占用结束后 根据订单的占库状态决定是否需要拆单/创建采购单(暂无)

                #region  占用结束流程

                //存在订单产品未占用
                if (!warehouseProductOccupy.IsOccupy)
                {
                    //订单占用需要释放掉  后续没占上库存的产品做处理
                    //更新占库通知的状态
                    await _occupyQueueRepository.UpdateOrderQueueStatus(new OccupyQueueDO
                    {
                        Id = queueDO.Id,
                        UpdateBy = "系统",
                        UpdateTime = DateTime.Now,
                        Status = OrderQueueStatusEnum.UnOccupy.ToString()
                    });

                    res.Message = $"订单【{orderInfo.OrderNo}】的产品占用失败!";
                }
                else
                {
                    var orderOccupyDetails = warehouseProductOccupy.ProductStocks;
                    await CreateOrderStockLog(new OccupyStockLogDO
                    {
                        ObjectNo = queueDO.SourceNo,
                        Type = OrderQueueTypeEnum.OrderService.ToString(),
                        Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的产品全部占用成功!",
                        BeforeValue = $"占库明细:{JsonConvert.SerializeObject(orderOccupyDetails)}",
                        LogLevel = LogLevelEnum.Info.ToString()
                    });

                    await CreateOrderStockLog(new OccupyStockLogDO
                    {
                        ObjectNo = queueDO.SourceNo,
                        Type = OrderQueueTypeEnum.OrderService.ToString(),
                        Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】回写订单数据,更新成本和占库状态!",
                        BeforeValue = $"占库明细:{JsonConvert.SerializeObject(orderOccupyDetails)}",
                        LogLevel = LogLevelEnum.Info.ToString()
                    });

                    //订单产品都占上了库存
                    #region 回写订单占库状态
                    var stockNotifyRequest = new OrderUseStockNotifyClientRequest()
                    {
                        OrderNo = orderInfo.OrderNo,
                        UseStockDetails = new List<OrderUseStockDetailProductDTO>()
                    };

                    foreach (var item in orderOccupyDetails)
                    {
                        var stockDetailProductDTO = new OrderUseStockDetailProductDTO()
                        {
                            Batches = new List<OrderUseStockDetailBatchDTO>(),
                            Id = item.Id,
                            ProductId = item.ProductId,
                            StockStatus = 2
                        };
                        var stockDetailBatchList = new List<OrderUseStockDetailBatchDTO>();
                        //一个产品具体占用的批次
                        if (item.StockDetailDTOs.Any())
                        {
                            foreach (var detailBatch in item.StockDetailDTOs)
                            {
                                var stockDetailBatchDTO = new OrderUseStockDetailBatchDTO()
                                {
                                    BatchNo = detailBatch.BatchId.ToString(),
                                    CostPrice = detailBatch.Cost,
                                    Number = detailBatch.OccupyNum
                                };
                                stockDetailBatchList.Add(stockDetailBatchDTO);
                            }
                            stockDetailProductDTO.Batches = stockDetailBatchList;
                        }
                        stockNotifyRequest.UseStockDetails.Add(stockDetailProductDTO);
                    }
                    await _orderClient.OrderUseStockNotify(stockNotifyRequest);

                    #endregion


                    //更新库存占用的is_valid

                    //await orderStockRepository.UpdateOrderStockValid(new OrderStockDO
                    //{
                    //    OrderNo = orderInfo.OrderNo,
                    //    IsValid = 2,
                    //    Remark = "库存充足,自动创建出库任务,修改占库有效值!",
                    //    UpdateBy = "系统"
                    //});

                    //更新占库通知的状态
                    await _occupyQueueRepository.UpdateOrderQueueStatus(new OccupyQueueDO
                    {
                        Id = queueDO.Id,
                        Status = OrderQueueStatusEnum.Occupy.ToString(),
                        UpdateTime = DateTime.Now,
                        UpdateBy = "系统"
                    });
                    res.Message = $"订单【{orderInfo.OrderNo}】的产品全部占用成功!";
                }

                #endregion
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的占库流程发送异常,错误信息:{ex.ToString()}!",
                    LogLevel = LogLevelEnum.Critical.ToString()
                });
            }
            finally
            {
                #region 更新占库通知的处理状态

                await _occupyQueueRepository.UpdateOrderProcessStatus(new OccupyQueueDO
                {
                    Id = queueDO.Id,
                    IsProcessing = OrderQueueProcessStatusEnum.Processed.ToString(),
                    UpdateBy = "占库系统",
                    UpdateTime = DateTime.Now
                });

                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的处理状态更新为【已处理】,结束占库流程!",
                    LogLevel = LogLevelEnum.Info.ToString()
                });
                #endregion
            }
            return res;
        }

        //循环订单产品做占用
        public async Task<WarehouseProductOccupyStockDTO> HandleOrderOccupyStock(QueryUseStockOrderDetailResponse orderInfo)
        {
            var productOccupyStockDTO = new WarehouseProductOccupyStockDTO();

            long shopId = orderInfo.ShopId;
            var shopInfoRes = await _shopMangeClient.GetShopById(new GetShopClientRequest
            {
                ShopId = shopId
            });
            ShopDTO shopInfo = null;
            if (shopInfoRes.Code == ResultCode.Success)
            {
                shopInfo = shopInfoRes.Data;
            }

            var shopOccupy = new ShopOccupyDTO
            {
                LocationId = shopId,
                LocationName = shopInfo != null ? shopInfo.SimpleName : string.Empty
            };

            var occupyQueueRes = await _occupyQueueRepository.GetListAsync(" where is_deleted =0 and source_no =@source_no", new { source_no = orderInfo.OrderNo });

            OccupyQueueDO occupyQueue = null;
            if (occupyQueueRes != null && occupyQueueRes.Any())
            {
                occupyQueue = occupyQueueRes.First();
            }
            try
            {
                productOccupyStockDTO = await OrderProductOccupy(orderInfo, occupyQueue, shopOccupy);
                //20210226 先占用门店的库存，如果没有，再占用 前置仓的库存
                //20210312 注释掉
                //if (!productOccupyStockDTO.IsOccupy)
                //{
                //    //关联的前置仓
                //    var relationShopInfoRes = await _wmsClient.GetShopOccupyMappingInfo(new GetShopOccupyMappingRequest
                //    {
                //        ShopId = shopId
                //    });
                //    if (relationShopInfoRes!=null && relationShopInfoRes.Code == ResultCode.Success)
                //    {
                //        var relationShopInfo = relationShopInfoRes.Data;
                //        shopOccupy = new ShopOccupyDTO
                //        {
                //            LocationId = relationShopInfo.RelationShopId,
                //            LocationName = relationShopInfo.RelationShopName
                //        };
                //        productOccupyStockDTO = await OrderProductOccupy(orderInfo, occupyQueue, shopOccupy);
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger.Error($"HandleOrderOccupyStock_订单号【{orderInfo.OrderNo}】占库发送异常!", ex);
            }
            return productOccupyStockDTO;
        }

        public async Task<WarehouseProductOccupyStockDTO> OrderProductOccupy(QueryUseStockOrderDetailResponse orderInfo,
            OccupyQueueDO occupyQueue, ShopOccupyDTO shopOccupy)
        {
            var productOccupyStockDTO = new WarehouseProductOccupyStockDTO();
            var orderOccupyDetails = new List<OrderProductStockDTO>();

            #region  订单产品逐个占库
            foreach (var item in orderInfo.Products)
            {
                if (item.PackageOrProduct.ProductAttribute == 1)
                {
                    if (!item.PackageProducts.Any())
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = orderInfo.OrderNo,
                            ObjectProductId = item.PackageOrProduct.Id,
                            ProductId = item.PackageOrProduct.ProductId,
                            ProductName = item.PackageOrProduct.ProductName,
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"HandleOrderOccupyStock_订单【{orderInfo.OrderNo}】," +
                            $"订单子单号【{item.PackageOrProduct.Id}】的套装子产品为空!",
                            LogLevel = LogLevelEnum.Error.ToString()
                        });
                    }
                    else
                    {
                        //套装下的子产品做判断
                        foreach (var packageProduct in item.PackageProducts)
                        {
                            if (packageProduct.ProductAttribute == 0 ||
                               packageProduct.ProductAttribute == 5)
                            {
                                packageProduct.OrderNo = orderInfo.OrderNo;
                                //套装产品*套装子产品的数量
                                // int productNumber = packageProduct.Number * item.PackageOrProduct.Number;

                                int productNumber = packageProduct.TotalNumber;

                                var occupyResult = await OrderOccupyStock(packageProduct, shopOccupy, productNumber, occupyQueue);
                                orderOccupyDetails.Add(new OrderProductStockDTO
                                {
                                    OrderNo = orderInfo.OrderNo,
                                    IsOccupy = occupyResult.IsOccupy,
                                    IsPackageProduct = 1,
                                    Number = productNumber,
                                    Id = packageProduct.Id,
                                    ProductId = packageProduct.ProductId,
                                    ProductName = packageProduct.ProductName,
                                    PackageId = item.PackageOrProduct.Id,
                                    StockDetailDTOs = occupyResult.OccupyStockList
                                });
                            }
                        }
                    }
                }
                else if (item.PackageOrProduct.ProductAttribute == 0 ||
                    item.PackageOrProduct.ProductAttribute == 5)
                {
                    item.PackageOrProduct.OrderNo = orderInfo.OrderNo;

                    int productNumber = item.PackageOrProduct.TotalNumber;

                    //普通产品的占用
                    var occupyResult = await OrderOccupyStock(item.PackageOrProduct, shopOccupy, productNumber, occupyQueue);
                    orderOccupyDetails.Add(new OrderProductStockDTO
                    {
                        OrderNo = orderInfo.OrderNo,
                        IsOccupy = occupyResult.IsOccupy,
                        IsPackageProduct = 0,
                        Number = productNumber,
                        Id = item.PackageOrProduct.Id,
                        ProductId = item.PackageOrProduct.ProductId,
                        ProductName = item.PackageOrProduct.ProductName,
                        StockDetailDTOs = occupyResult.OccupyStockList
                    });
                }
            }

            #endregion

            if (orderOccupyDetails.Any())
            {
                productOccupyStockDTO.ProductStocks = orderOccupyDetails;

                //全部占用
                if (orderOccupyDetails.All(r => r.IsOccupy))
                {
                    productOccupyStockDTO.IsOccupy = true;
                }
                else
                {
                    //部分占用 释放库存
                    var occupyItems = await _occupyItemRepository.GetListAsync(" where source_inventory_no=@source_inventory_no and is_deleted=0 and is_valid=1", new { source_inventory_no = orderInfo.OrderNo });

                    var flowItemIds = occupyItems.Select(r => r.InventoryId);

                    if (flowItemIds.Any())
                    {
                        //释放库存流水
                        await _inventoryFlowItemRepository.ReleaseInventoryFlowItem(new InventoryFlowItemDO
                        {
                            UpdateBy = "释放系统",
                            StockIds = flowItemIds.ToList()
                        });
                    }

                    //释放订单占库记录
                    await _occupyItemRepository.ReleaseOccupyItemStatus(new OccupyItemDO
                    {
                        UpdateBy = "释放系统",
                        SourceInventoryNo = orderInfo.OrderNo
                    });

                    var remark = new StringBuilder();
                    //有库存的和订单产品比较
                    foreach (var item in orderOccupyDetails.Where(r => !r.IsOccupy))
                    {
                        remark.AppendLine($"【{item.ProductName}】的库存不足");
                    }

                    //添加日志记录
                    await _occupyQueueRepository.UpdateOrderQueueRemark(new OccupyQueueDO
                    {
                        SourceNo = orderInfo.OrderNo,
                        UpdateBy = "系统",
                        Remark = remark.ToString()
                    });
                }
            }
            return productOccupyStockDTO;
        }

        /// <summary>
        /// 订单产品占用核心接口
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task<OrderOccupyStockResponse> OrderOccupyStock(UseStockOrderDetailProductDTO productDTO,
          ShopOccupyDTO shopOccupy, decimal productNumber, OccupyQueueDO occupyQueue)
        {
            var res = new OrderOccupyStockResponse();

            //开始准备占用【XX】仓库存

            #region 暂时不用
            //查询汇总库存
            //var stockLocationSummaryResult =await stockLocationRepository.GetStockLocationSummaryDOs(new StockLocationSummaryDO
            //{
            //    LocationId = wareHouseId,
            //    ProductId = productDTO.ProductId,
            //    LocationType = LocationTypeEnum.Warehouse.ToString()
            //});

            ////做初步的判断 这个数据可能并不准确
            //if (stockLocationSummaryResult.Any()) { 

            //}
            #endregion

            //TODO 后期这里要加锁 LocationId+ProductId

            try
            {
                #region 获取可用库存&占用批次
                // 
                var availableStockLocationRes = await _stockManageService.GetAvailableBatchs(new GetAvailableBatchsRequest
                {
                    ShopId = shopOccupy.LocationId,
                    ProductId = productDTO.ProductId
                });

                if (availableStockLocationRes.Code == ResultCode.Success)
                {
                    var availableStockLocations = availableStockLocationRes.Data;

                    //有可用 但不代表够占用
                    if (availableStockLocations != null &&
                        availableStockLocations.Any())
                    {
                        //现在不允许超占 而且也没有考虑到安全库存

                        //总可用库存
                        int totalAvailableNum = availableStockLocations.Sum(r => r.AvailableNum);

                        int productNum = Convert.ToInt32(productNumber);

                        if (totalAvailableNum >= productNum)
                        {
                            //locationId +productId+batchId +ownerId +num 先进先出的占用规则
                            foreach (var item in availableStockLocations.Where(r => r.AvailableNum > 0))
                            {
                                if (productNum > 0)
                                {
                                    var stockType = item.ProductAttrType == ProductAttrTypeEnum.良品.ToString() ? 1 : 2;

                                    if (item.AvailableNum >= productNum)
                                    {
                                        //添加批次库存流水记录
                                        var flowItemResult = await _inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                        {
                                            SourceNo = productDTO.OrderNo,
                                            LocationId = shopOccupy.LocationId,
                                            LocationName = shopOccupy.LocationName,
                                            BatchNo = item.Id.ToString(),
                                            ProductId = productDTO.ProductId,
                                            ProductName = productDTO.ProductName,
                                            BusinessCategory = 4,
                                            BeforeOccupyNum = 0,
                                            AfterOccupyNum = productNum,
                                            ChangeOccupyNum = productNum,
                                            CreateBy = "占库系统",
                                            CreateTime = DateTime.Now
                                        });

                                        //添加订单占用
                                        await CreateOrderStock(flowItemResult, productNum, productDTO,
                                            Convert.ToInt32(productNumber), shopOccupy, occupyQueue, item);

                                        res.OccupyStockList.Add(new OrderProductStockDetailDTO
                                        {
                                            WeekYear = item.WeekYear,
                                            BatchId = Convert.ToInt32(item.Id),
                                            Cost = item.Cost,
                                            OccupyNum = productNum,
                                            OwnerId = Convert.ToInt32(item.OwnId),
                                            StockType = Convert.ToSByte(stockType),
                                            StockId = flowItemResult
                                        });
                                        break;
                                    }
                                    else
                                    {
                                        //扣除后继续占用
                                        productNum -= item.AvailableNum;

                                        var flowItemResult = await _inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                        {
                                            SourceNo = productDTO.OrderNo,
                                            LocationId = shopOccupy.LocationId,
                                            LocationName = shopOccupy.LocationName,
                                            BatchNo = item.Id.ToString(),
                                            ProductId = productDTO.ProductId,
                                            ProductName = productDTO.ProductName,
                                            BusinessCategory = 4,
                                            BeforeOccupyNum = 0,
                                            AfterOccupyNum = item.AvailableNum,
                                            ChangeOccupyNum = item.AvailableNum,
                                            CreateBy = "占库系统",
                                            CreateTime = DateTime.Now
                                        });

                                        await CreateOrderStock(flowItemResult, item.AvailableNum, productDTO,
                                            Convert.ToInt32(productNumber), shopOccupy, occupyQueue, item);

                                        res.OccupyStockList.Add(new OrderProductStockDetailDTO
                                        {
                                            WeekYear = item.WeekYear,
                                            BatchId = Convert.ToInt32(item.Id),
                                            Cost = item.Cost,
                                            OccupyNum = item.AvailableNum,
                                            OwnerId = Convert.ToInt32(item.OwnId),
                                            StockType = Convert.ToSByte(stockType),
                                            StockId = flowItemResult
                                        });
                                        continue;
                                    }
                                }
                            }
                            res.IsOccupy = true;
                        }
                        else
                        {
                            await CreateOrderStockLog(new OccupyStockLogDO
                            {
                                ObjectNo = productDTO.OrderNo,
                                ObjectProductId = productDTO.Id,
                                ProductId = productDTO.ProductId,
                                ProductName = productDTO.ProductName,
                                Type = OrderQueueTypeEnum.OrderService.ToString(),
                                Remark = $"OrderOccupyStock_订单【{productDTO.OrderNo}】," +
                                    $"订单子单号【{ productDTO.Id}】的产品【{productDTO.ProductId}】在【{shopOccupy.LocationName}】无充足库存," +
                                    $"总可用库存【{totalAvailableNum}】,产品数量【{productNumber}】!",
                                BeforeValue = $"门店信息:{JsonConvert.SerializeObject(shopOccupy)}",

                                LogLevel = LogLevelEnum.Info.ToString()
                            });
                        }
                    }
                    else
                    {
                        await CreateOrderStockLog(new OccupyStockLogDO
                        {
                            ObjectNo = productDTO.OrderNo,
                            ObjectProductId = productDTO.Id,
                            ProductId = productDTO.ProductId,
                            ProductName = productDTO.ProductName,
                            Type = OrderQueueTypeEnum.OrderService.ToString(),
                            Remark = $"订单【{productDTO.OrderNo}】," +
                            $"订单子单号【{ productDTO.Id}】的产品【{productDTO.ProductId}】在门店【{shopOccupy.LocationName}】对应的库存表无记录!",
                            BeforeValue = $"门店信息:{JsonConvert.SerializeObject(shopOccupy)}",
                            AfterValue = $"产品信息:{JsonConvert.SerializeObject(productDTO)}",
                            LogLevel = LogLevelEnum.Warning.ToString()
                        });

                    }
                }
                #endregion

                //TODO 要做Check 防止超占 如果超占 需要释放订单占用
                #region 超占判断逻辑
                #endregion
            }
            catch (Exception ex)
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = productDTO.OrderNo,
                    ObjectProductId = productDTO.Id,
                    ProductId = productDTO.ProductId,
                    ProductName = productDTO.ProductName,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderOccupyStock_订单【{productDTO.OrderNo}】," +
                           $"订单子单号【{ productDTO.Id}】的产品【{productDTO.ProductId}】在门店【{shopOccupy.LocationName}】占库发生异常,ex:{ex.ToString()}",
                    BeforeValue = $"仓库配置:{JsonConvert.SerializeObject(shopOccupy)}",
                    LogLevel = LogLevelEnum.Critical.ToString()
                });
                _logger.Error($"OrderOccupyStock_订单【{productDTO.OrderNo}】," +
                           $"订单子单号【{ productDTO.Id}】的产品【{productDTO.ProductId}】在门店【{shopOccupy.LocationName}】占库发生异常", ex);
            }

            return res;
        }

        /// <summary>
        /// 创建订单占用
        /// </summary>
        /// <param name="stockLocation"></param>
        /// <param name="occupyNum"></param>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task<int> CreateOrderStock(long flowItemId, int occupyNum,
                                                UseStockOrderDetailProductDTO productDTO,
                                                int totalOccupyNum, ShopOccupyDTO purchaseWarehouse,
                                                OccupyQueueDO occupyQueue, InventoryBatchDTO inventoryBatch)
        {
            try
            {
                //添加占用记录
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = productDTO.OrderNo,
                    ObjectProductId = productDTO.Id,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    ProductId = productDTO.ProductId,
                    ProductName = productDTO.ProductName,
                    Remark = $"CreateOrderStock_订单【{productDTO.OrderNo}】," +
                         $"订单子单号【{ productDTO.Id}】的产品" +
                         $"【{productDTO.ProductId}】占用仓库【{purchaseWarehouse.LocationName}】的批次【{inventoryBatch.Id}】数量【{occupyNum}】!",
                    BeforeValue = $"仓库配置:{JsonConvert.SerializeObject(purchaseWarehouse)}",
                    LogLevel = LogLevelEnum.Info.ToString()
                });


                int orderId = 0;
                int.TryParse(productDTO.OrderNo.Replace("RGB", ""), out orderId);

                int stockType = inventoryBatch.ProductAttrType == ProductAttrTypeEnum.良品.ToString() ? 1 : 2;
                var occupyItem = new OccupyItemDO
                {
                    OccupyQueueId = occupyQueue.Id,
                    SourceInventoryId = orderId,
                    SourceInventoryNo = productDTO.OrderNo,
                    OrderProductId = productDTO.Id,
                    InventoryId = flowItemId,
                    ProductId = productDTO.ProductId,
                    ProductName = productDTO.ProductName,
                    Num = occupyNum,
                    BatchNo = inventoryBatch.Id.ToString(),
                    RefBatchNo = inventoryBatch.RefBatchNo,
                    OccupyType = OccupyTypeEnum.Order.ToString(),
                    IsValid = 1,
                    Remark = $"门店【{purchaseWarehouse.LocationName}】批次【{inventoryBatch.Id}】" +
                    $"的可用库存量【{inventoryBatch.AvailableNum}】,本次占用量:【{occupyNum}】,总占用量:【{totalOccupyNum}】",
                    LocationId = inventoryBatch.ShopId,
                    OwnId = inventoryBatch.OwnId,
                    Cost = inventoryBatch.Cost,
                    StockType = Convert.ToSByte(stockType),
                    CreateBy = "占库系统",
                    CreateTime = DateTime.Now
                };
                await _occupyItemRepository.InsertAsync(occupyItem);
            }
            catch (Exception ex)
            {
                _logger.Error($"订单【{productDTO.OrderNo}】创建订单占用发送异常", ex);
            }
            return 1;
        }


        public async Task<ApiResult<string>> CheckOrderValid(ApiResult<QueryUseStockOrderDetailResponse> orderInfoResponse, OccupyQueueDO queueDO)
        {
            var res = new ApiResult<string>
            {
                Code = ResultCode.Exception
            };

            #region 订单前置校验

            if (orderInfoResponse.Code != ResultCode.Success)
            {
                res.Message = $"订单号【{queueDO.SourceNo}】查询订单信息异常!";
                return res;
            }

            if (orderInfoResponse.Data == null)
            {
                res.Message = $"订单号【{queueDO.SourceNo}】查询订单信息为空!";
                return res;
            }
            var orderInfo = orderInfoResponse.Data;

            //订单状态
            if (orderInfo.OrderStatus == Client.Enums.OrderStatusEnum.Canceled)
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的状态为已取消!",
                    LogLevel = LogLevelEnum.Warning.ToString(),
                    BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}",
                });
                res.Message = $"订单【{queueDO.SourceNo}】的状态为已取消!";
                return res;
            }

            //订单产品校验 
            if (!orderInfo.Products.Any())
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的产品为空!",
                    LogLevel = LogLevelEnum.Warning.ToString(),
                    BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}"
                });
                res.Message = $"订单【{queueDO.SourceNo}】的产品为空!";
                return res;
            }
            //订单未支付
            //if (orderInfo.PayStatus != 1)
            //{
            //    await CreateOrderStockLog(new OccupyStockLogDO
            //    {
            //        ObjectNo = queueDO.SourceNo,
            //        Type = OrderQueueTypeEnum.OrderService.ToString(),
            //        Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的支付状态为【未支付】!",
            //        LogLevel = LogLevelEnum.Warning.ToString(),
            //        BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}"
            //    });
            //    res.Message = $"订单【{queueDO.SourceNo}】的支付状态为【未支付】!";
            //    return res;
            //}
            //订单占库
            if (orderInfo.StockStatus == 2)
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的占库状态为【已占库】!",
                    LogLevel = LogLevelEnum.Warning.ToString(),
                    BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}",
                });
                res.Message = $"订单【{queueDO.SourceNo}】的占库状态为【已占库】!";
                return res;
            }
            //用户地址不为空
            //if (orderInfo.OrderAddress == null)
            //{
            //    await CreateOrderStockLog(new OccupyStockLogDO
            //    {
            //        ObjectNo = queueDO.SourceNo,
            //        Type = OrderQueueTypeEnum.OrderService.ToString(),
            //        Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】的地址为空!",
            //        LogLevel = LogLevelEnum.Warning.ToString(),
            //        BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}",
            //    });
            //    res.Message = $"订单【{queueDO.SourceNo}】的地址为空";
            //    return res;
            //}
            //订单必须都是实物产品
            if (!orderInfo.Products.Any(r => r.PackageOrProduct.ProductAttribute == 0 ||
            r.PackageOrProduct.ProductAttribute == 1 || r.PackageOrProduct.ProductAttribute == 5))
            {
                await CreateOrderStockLog(new OccupyStockLogDO
                {
                    ObjectNo = queueDO.SourceNo,
                    Type = OrderQueueTypeEnum.OrderService.ToString(),
                    Remark = $"OrderStockMain_订单【{queueDO.SourceNo}】无实物产品!",
                    LogLevel = LogLevelEnum.Warning.ToString(),
                    BeforeValue = $"订单信息:{JsonConvert.SerializeObject(orderInfo)}",
                });
                res.Message = $"订单【{queueDO.SourceNo}】无实物产品";
                return res;
            }

            res.Code = ResultCode.Success;
            #endregion

            return res;
        }



        public async Task<ApiResult<string>> SendOrderQueue(OrderStockRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var model = new OccupyQueueDTO
                {
                    SourceNo = request.QueueNo,
                    QueueType = OrderQueueTypeEnum.OrderService.ToString(),
                    IsProcessing = OrderQueueProcessStatusEnum.Processed.ToString(),
                    Status = OrderQueueStatusEnum.New.ToString(),
                    CreateBy = "占库系统",
                    CreateTime = DateTime.Now
                };

                var vo = _mapper.Map<OccupyQueueDO>(model);

                var orderQueueRes = await _occupyQueueRepository.GetListAsync(" where is_deleted=0 and source_no=@source_no", new { source_no = request.QueueNo });
                if (orderQueueRes != null && orderQueueRes.Any())
                {
                    var orderQueueResult = orderQueueRes.First();
                    if (orderQueueResult.Status != OrderQueueStatusEnum.Occupy.ToString())
                    {
                        await _occupyQueueRepository.UpdateOrderQueue(new OccupyQueueDO
                        {
                            UpdateBy = model.CreateBy,
                            IsProcessing = OrderQueueProcessStatusEnum.Processed.ToString(),
                            Status = OrderQueueStatusEnum.New.ToString(),
                            SourceNo = request.QueueNo
                        });
                        res.Code = ResultCode.Success;
                    }
                }
                else
                {
                    var result = await _occupyQueueRepository.InsertAsync(vo);
                    if (result <= 0)
                    {
                        _logger.Error($"订单号{request.QueueNo}创建占库通知异常!");
                        res.Code = ResultCode.Exception;
                    }
                    res.Code = ResultCode.Success;
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                res.Message = $"订单号【{request.QueueNo}】新增占库通知异常,Error:{ex.ToString()}";
                _logger.Error($"订单号【{request.QueueNo}】新增占库通知异常", ex);
            }
            return res;
        }

        public async Task<int> CreateOrderStockLog(OccupyStockLogDO stockLogDO)
        {
            stockLogDO.CreateBy = "占库系统";
            stockLogDO.CreateTime = DateTime.Now;
            try
            {
                //前期入表，后期入ELK
                return await _occupyStockLogRepository.InsertAsync(stockLogDO);
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateOrderStockLog发送异常,Data:{JsonConvert.SerializeObject(stockLogDO)},错误信息:{ex.ToString()}");
            }
            return -1;
        }

        public async Task<ApiResult<string>> CancelOrderQueue(GetOrderQueueRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var orderQueue = await _occupyQueueRepository.GetAsync(request.Id);
                if (orderQueue != null)
                {
                    if (orderQueue.Status != OrderQueueStatusEnum.Occupy.ToString()
                        && orderQueue.Status != OrderQueueStatusEnum.Cancel.ToString())
                    {
                        await _occupyQueueRepository.UpdateOrderQueueStatus(new OccupyQueueDO
                        {
                            Id = request.Id,
                            UpdateBy = _identityService.GetUserName(),
                            UpdateTime = DateTime.Now,
                            Status = OrderQueueStatusEnum.Cancel.ToString()
                        });
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Success,
                            Message = "操作成功!"
                        };
                    }
                    else
                    {
                        var msg = string.Empty;
                        if (orderQueue.Status == OrderQueueStatusEnum.Cancel.ToString())
                        {
                            msg = "占库已取消";
                        }
                        else if (orderQueue.Status == OrderQueueStatusEnum.Occupy.ToString())
                        {
                            msg = "订单已占库";
                        }
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Exception,
                            Message = msg
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"CancelOrderQueue_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> ReOccupyStock(GetOrderQueueRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var orderQueue = await _occupyQueueRepository.GetAsync(request.Id);
                if (orderQueue.Status != OrderQueueStatusEnum.Occupy.ToString()
                       && orderQueue.Status != OrderQueueStatusEnum.Cancel.ToString())
                {
                    if (orderQueue.IsProcessing == OrderQueueProcessStatusEnum.Processing.ToString())
                    {
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Exception,
                            Message = "订单正在占库中....."
                        };
                    }
                    else
                    {
                        //调用占库接口
                        await OrderOccupyStock(new OrderStockRequest
                        {
                            CreateBy = _identityService.GetUserName(),
                            QueueNo = orderQueue.SourceNo
                        });

                        return new ApiResult<string>
                        {
                            Code = ResultCode.Success,
                            Message = "占库结束！"
                        };
                    }
                }
                else
                {
                    var msg = string.Empty;
                    if (orderQueue.Status == OrderQueueStatusEnum.Cancel.ToString())
                    {
                        msg = "占库已取消";
                    }
                    else if (orderQueue.Status == OrderQueueStatusEnum.Occupy.ToString())
                    {
                        msg = "订单已占库";
                    }
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = msg
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"ReOccupyStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<OccupyStockLogDTO>>> GetOccupyStockLogs(OccupyStockLogDTO request)
        {
            var res = new ApiResult<List<OccupyStockLogDTO>>();
            try
            {
                //日志记录过多，查不到精准的信息   先查top 50的
                var result = await _occupyStockLogRepository.GetListAsync(" where object_no=@object_no order by id desc limit 0,50 ", new
                {
                    object_no = request.ObjectNo
                });

                var vo = _mapper.Map<List<OccupyStockLogDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetOccupyStockLogs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        #endregion

        #region 释放库存

        /// <summary>
        /// 释放库存(安装了释放  占用未安装释放)
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ReleaseStock(OrderStockRequest request)
        {
            _logger.Info($"ReleaseStock_Data:{JsonConvert.SerializeObject(request)}");

            var res = new ApiResult<string>();
            try
            {
                //1.获取订单安装出库的记录 做入库

                var orderInfoRes = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest
                {
                    OrderNos = new List<string> { request.QueueNo }
                });

                OrderDTO orderInfo = null;
                if (orderInfoRes.Code == ResultCode.Success)
                {
                    orderInfo = orderInfoRes.Data?.FirstOrDefault();
                }

                //获取订单的占用记录 做出库
                var stockInoutRes = await _stockInoutRepository.GetListAsync(" where source_inventory_no =@source_inventory_no and is_deleted=0 and source_type=@source_type", new { source_inventory_no = request.QueueNo, source_type = StockOutTypeEnum.订单安装.ToString() });
                //订单已经安装了
                if (stockInoutRes != null && stockInoutRes.Any())
                {
                    var stockInout = stockInoutRes.First();
                    var stockInoutItems = await _stockInoutItemRepository.GetListAsync(" where inout_id =@inout_id and is_deleted=0", new { inout_id = stockInout.Id });

                    stockInout.CreateBy = request.CreateBy;
                    stockInout.SourceType = StockInTypeEnum.出库作废.ToString();
                    stockInout.Status = StockInOutStatusEnum.已收货.ToString();

                    var stockInoutDTO = _mapper.Map<StockInOutDTO>(stockInout);
                    var stockInoutItemDTO = _mapper.Map<List<StockInoutItemDTO>>(stockInoutItems);
                    stockInoutDTO.StockItems = stockInoutItemDTO;

                    await _stockManageService.OrderCancelReleaseStock(stockInoutDTO);

                    //将原订单安装的单据作废掉
                    await _stockInoutRepository.DeleteStockInout(new StockInoutDO
                    {
                        Id = stockInout.Id
                    });
                }
                else
                {
                    //订单未安装，仅占用
                    var orderStocks = await _occupyItemRepository.GetListAsync(" where source_inventory_no=@source_inventory_no and is_deleted=0 and is_valid=1", new { source_inventory_no = request.QueueNo });
                    if (orderStocks != null && orderStocks.Any())
                    {
                        //将订单占用释放掉
                        await _occupyItemRepository.UpdateOccupyItemValid(new OccupyItemDO
                        {
                            UpdateBy = request.CreateBy,
                            IsValid = 3,
                            SourceInventoryNo = request.QueueNo
                        });
                        var flowItemIds = orderStocks.Select(r => r.InventoryId).ToList();
                        //将库存占用释放掉
                        await _inventoryFlowItemRepository.ReleaseInventoryFlowItem(new InventoryFlowItemDO
                        {
                            UpdateBy = request.CreateBy,
                            StockIds = flowItemIds
                        });
                    }
                    else
                    {
                        //判断占库通知的状态
                        var orderQueueRes = await _occupyQueueRepository.GetListAsync(" where source_no=@source_no and is_deleted=0", new { source_no = request.QueueNo });
                        var orderQueue = orderQueueRes?.FirstOrDefault();
                        if (orderQueue != null && orderQueue.Status != OrderQueueStatusEnum.Occupy.ToString()
                             && orderQueue.Status != OrderQueueStatusEnum.Cancel.ToString())
                        {
                            await _occupyQueueRepository.CancelOrderQueue(new OccupyQueueDO
                            {
                                SourceNo = request.QueueNo,
                                UpdateBy = request.CreateBy ?? "System",
                                UpdateTime = DateTime.Now,
                                Status = OrderQueueStatusEnum.Cancel.ToString()
                            });
                            return new ApiResult<string>
                            {
                                Code = ResultCode.Success,
                                Message = "操作成功!"
                            };
                        }
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderOccupyStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<OccupyItemDTO>>> GetOccupyItems(OccupyItemDTO request)
        {
            var res = new ApiResult<List<OccupyItemDTO>>();
            try
            {
                var occupyItemRes = await _occupyItemRepository.GetListAsync(" where is_deleted=0 and source_inventory_no=@source_inventory_no and is_valid in (1,2)", new
                {
                    source_inventory_no = request.SourceInventoryNo
                });
                if (occupyItemRes != null && occupyItemRes.Any())
                {
                    var vo = _mapper.Map<List<OccupyItemDTO>>(occupyItemRes);
                    res.Data = vo;
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetOccupyItems_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }
        #endregion
    }
}
