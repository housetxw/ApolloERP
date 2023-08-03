using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Dapper;
using ApolloErp.Login.Auth;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Dal.Repositorys.ShopPurchase;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Client.Model;
using System.Transactions;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Dal.Repositorys.WMS;
using Ae.Shop.Api.Client.Clients.Order;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Dal.Repositorys.HomeCare;
using Ae.Shop.Api.Imp.Helpers;
using Ae.Shop.Api.Dal.Repositorys.Company;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Client.Clients.Product;
using Ae.Shop.Api.Client.Model.ShopManage;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Common.Extension;
using Dao = Ae.Shop.Api.Dal.Repositorys;
using DAL = Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Client.Response;

namespace Ae.Shop.Api.Imp.Services
{
    public class StockManageService : IStockManageService
    {
        #region 初始化函数
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<StockManageService> _logger;
        private readonly IStockManageRepository _stockManageRepository;
        private readonly IInventoryBatchFlowRepository _inventoryBatchFlowRepository;


        private readonly IInventoryFlowItemRepository inventoryFlowItemRepository;

        private readonly IStockInoutRepository stockInoutRepository;
        private readonly IStockInoutItemRepository stockInoutItemRepository;

        private readonly IIdentityService identityService;
        private readonly IInventoryBatchRepository inventoryBatchRepository;
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly IPurchaseOrderProductRepository purchaseOrderProductRepository;

        private readonly IShopMangeClient shopMangeClient;
        private readonly IOccupyItemRepository _occupyItemRepository;
        private readonly IWmsClient wmsClient;
        private readonly IBatchRepository _batchRepository;
        private readonly IAllotTaskRepository _allotTaskRepository;
        private readonly IOrderClient _orderClient;
        private readonly IHomeCareProductRepository _homeCareProductRepository;
        private readonly IHomeCareRecordRepository _homeCareRecordRepository;
        private readonly IHomeCareOrderRepository _homeCareOrderRepository;
        private readonly IOrderDispatchRepository _orderDispatchRepository;
        private readonly IShopWmsLogRepository _shopWmsLogRepository;
        private readonly IPurchaseClient _purchaseClient;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductClient _productClient;
        private readonly IPurchaseOrderLogRepository purchaseOrderLogRepository;
        private readonly Dao.IShopRepository shopRepository;

        public StockManageService(IMapper mapper, ApolloErpLogger<StockManageService> logger,
           IStockManageRepository stockManageRepository,
           IInventoryBatchFlowRepository inventoryBatchFlowRepository,
           IInventoryFlowItemRepository inventoryFlowItemRepository,
           IStockInoutRepository stockInoutRepository,
           IStockInoutItemRepository stockInoutItemRepository,
           IIdentityService identityService, IInventoryBatchRepository inventoryBatchRepository,
           IPurchaseOrderRepository purchaseOrderRepository,
           IPurchaseOrderProductRepository purchaseOrderProductRepository, IShopMangeClient shopMangeClient,
           IOccupyItemRepository occupyItemRepository,
           IWmsClient wmsClient, IBatchRepository batchRepository,
           IAllotTaskRepository allotTaskRepository, IOrderClient orderClient,
           IHomeCareProductRepository homeCareProductRepository,
           IHomeCareRecordRepository homeCareRecordRepository,
           IHomeCareOrderRepository homeCareOrderRepository,
            IOrderDispatchRepository orderDispatchRepository,
            IShopWmsLogRepository shopWmsLogRepository,
            IPurchaseClient purchaseClient, ICompanyRepository companyRepository,
            IProductClient productClient, IPurchaseOrderLogRepository purchaseOrderLogRepository,
            Dao.IShopRepository _shopRepository)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._stockManageRepository = stockManageRepository;
            this._inventoryBatchFlowRepository = inventoryBatchFlowRepository;
            this.inventoryFlowItemRepository = inventoryFlowItemRepository;

            this.stockInoutRepository = stockInoutRepository;
            this.stockInoutItemRepository = stockInoutItemRepository;
            this.identityService = identityService;
            this.inventoryBatchRepository = inventoryBatchRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.purchaseOrderProductRepository = purchaseOrderProductRepository;

            this.shopMangeClient = shopMangeClient;
            this._occupyItemRepository = occupyItemRepository;
            this.wmsClient = wmsClient;
            this._batchRepository = batchRepository;
            this._allotTaskRepository = allotTaskRepository;
            this._orderClient = orderClient;
            this._homeCareProductRepository = homeCareProductRepository;
            this._homeCareRecordRepository = homeCareRecordRepository;
            this._homeCareOrderRepository = homeCareOrderRepository;
            this._orderDispatchRepository = orderDispatchRepository;
            this._shopWmsLogRepository = shopWmsLogRepository;
            this._purchaseClient = purchaseClient;
            this._companyRepository = companyRepository;
            this._productClient = productClient;
            this.purchaseOrderLogRepository = purchaseOrderLogRepository;

            this.shopRepository = _shopRepository;

        }

        #endregion

        /// <summary>
        /// 查询出入库记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<StockInOutDTO>> GetStockInOutPageData(ShopBatchFlowRequest request)
        {
            if ( request.ShopId <= 0)
            {
                var shopInfo = await GetShopById();
                request.ShopId = shopInfo?.ShopId ?? 0;
            }

            var res = new ApiPagedResult<StockInOutDTO>();
            try
            {
                if (request.Times != null && request.Times.Any())
                {
                    request.StartTime = Convert.ToDateTime(request.Times[0]);
                    request.EndTime = Convert.ToDateTime(request.Times[1]);
                }
                var result = await stockInoutRepository.GetStockInOutPageData(request);

                var stockData = new ApiPagedResultData<StockInOutDTO>();
                stockData.TotalItems = result.TotalItems;
                stockData.Items = new List<StockInOutDTO>();

                if (result.Items != null && result.Items.Any())
                {
                    foreach (var item in result.Items.GroupBy(r => r.StockId))
                    {
                        var stockItem = item.First();
                        stockData.Items.Add(new StockInOutDTO
                        {
                            CreateBy = stockItem.CreateBy,
                            CreateTime = stockItem.CreateTime,
                            SourceInventoryNo = stockItem.SourceInventoryNo,
                            LocationName = stockItem.LocationName,
                            StockId = stockItem.StockId,
                            Type = stockItem.OperationType + "-" + stockItem.SourceType,
                            SourceType = stockItem.SourceType,
                            OperationType = stockItem.OperationType,
                            Status = stockItem.Status,
                            Remark = stockItem.Remark
                        });

                    }
                }
                res.Message = "查询成功!";
                res.Code = ResultCode.Success;
                res.Data = stockData;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStockInOutPageData_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 门店库存查看
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<InventoryDTO>> GetShopStockList(ShopStockRequest request)
        {
            var res = new ApiPagedResult<InventoryDTO>();
            try
            {
                var shopId = Convert.ToInt64(identityService.GetOrganizationId());
                //从Boss过来的允许查多个门店的库存
                if (shopId != 1 && request.LocationId <= 0)
                {
                    request.LocationId = shopId;
                }

                //_logger.Info($"GetShopStockList,{JsonConvert.SerializeObject(request)}");
                var result = await _stockManageRepository.GetInventoryPageData(request);
                var vo = _mapper.Map<ApiPagedResultData<InventoryDTO>>(result);

                res.Code = ResultCode.Success;
                res.Data = vo;
                res.Message = "查询成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetShopStockList_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 查询门店产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductAllInfoVo>>> GetShopProducts(ProductSearchRequest request)
        {
            var res = new ApiResult<List<ProductAllInfoVo>>();
            try
            {
                //var conditons = new StringBuilder();
                //var param = new DynamicParameters();
                if (request.PurchaseType == 1)
                {
                    request.IsOnSale = 1;
                }
                // request.IsOnSale = 1; //必须是上架的

                var result = await SearchProduct(request);

                res.Code = ResultCode.Success;
                res.Data = result.Items;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 搜索商品区分 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSearchResponse> SearchProduct(ProductSearchRequest request)
        {
            if (request == null)
            {
                return null;
            }
            var result = new ProductSearchResponse();
            if (request.PurchaseType == 1) // 查询总部商品
            {
                var para = _mapper.Map<ProductSearchClientRequest>(request);
                para.ProductAttributes = new List<string>() { "0" };// 只查询实物产品
                para.ClassType = 2;
                if (request.IsHaoCai == 1)
                {
                    para.MainCategoryId = 321; //耗材
                }
                para.IsDeleted = 0;
                var product = await _productClient.SearchProduct(para);
                if (product != null && product.TotalItems > 0)
                {
                    result.Items = product.Items.Select(t => new ProductAllInfoVo()
                    {
                        CategoryName = t.MainCategoryName + "/" + t.SecondCategoryName + "/" + t.ChildCategoryName,
                        IsOnSale = t.OnSale,
                        ProductCode = t.ProductCode,
                        ProductName = t.Name,
                        SalesPrice = t.SalesPrice,
                        StandardPrice = t.StandardPrice,
                        CostPrice = t.SettlementPrice,
                        Unit = t.Unit ?? "个"
                    }).ToList();
                    result.TotalItems = product.TotalItems;
                }
            }
            else if (request.PurchaseType == 2)// 非总部
            {

                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);

                var para = _mapper.Map<ShopProductSearchClientRequest>(request);
                para.ShopId = shopId;
                para.IsDeleted = 0;
                // para.AuditStatus = 1;  //审核通过
                //para.IsStoreoutside = 1;  //是外采商品
                para.IsDisabled = 0;
                var pageShopProduct = await _productClient.SearchShopProduct(para);
                if (pageShopProduct != null && pageShopProduct.Data.TotalItems > 0)
                {
                    //var serviceTypeDtos = await shopMangeClient.GetShopServiceTypeAsync(new Client.Request.ShopManage.ShopServiceTypeClientRequest()
                    //{ ShopId = shopId });
                    result.Items = pageShopProduct.Data.Items.Select(t => new ProductAllInfoVo()
                    {
                        //CategoryName = ConverToShopServiceType(serviceTypeDtos, t.CategoryId),
                        CategoryName = t.MainCategoryName + "/" + t.SecondCategoryName + "/" + t.ChildCategoryName,
                        IsOnSale = t.OnSale,
                        ProductCode = t.ProductCode,
                        ProductName = t.ProductName,
                        SalesPrice = t.SalesPrice,
                        StandardPrice = t.StandardPrice,
                        CostPrice = t.PurchasePrice,
                        Unit = "个"
                    }).ToList();
                    result.TotalItems = pageShopProduct.Data.TotalItems;
                }
            }
            return result;
        }

        /// <summary>
        /// 转换门店项目类型
        /// </summary>
        /// <param name="serviceTypeDtos"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private string ConverToShopServiceType(List<ShopServiceTypeDto> serviceTypeDtos, int categoryId)
        {
            var serviceType = serviceTypeDtos?.Where(c => c.ServiceTypeId == categoryId)?.FirstOrDefault();
            if (serviceType != null)
            {
                return serviceType.DisplayName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 创建入库任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateInStockTask(StockInOutDTO request)
        {
            var res = new ApiResult<string>();

            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? identityService.GetUserName() : request.CreateBy;
            long locationId = 0;
            string locationName = string.Empty;
            if (request.LocationId > 0)
            {
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = request.LocationId
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    locationId = request.LocationId;
                    locationName = shopResult.Data.SimpleName;
                }
            }
            else
            {
                var shopInfo = await GetShopById();
                locationId = shopInfo?.ShopId ?? 0;
                locationName = shopInfo?.SimpleName ?? "";
            }
            try
            {
                var status = string.Empty;
                PurchaseOrderDO purchaseOrderDO = null;

                //修改入库单状态
                if (request.SourceType == StockInTypeEnum.采购入库.ToString())
                {
                    purchaseOrderDO = await purchaseOrderRepository.GetAsync(request.SourceInventoryNo);
                    if (request.StockItems.Any(r => r.DiffNum > 0))
                    {
                        status = StockInOutStatusEnum.部分收货.ToString();
                    }
                    else if (request.StockItems.Any(r => r.Num != r.ActualNum))
                    {
                        status = StockInOutStatusEnum.部分收货.ToString();
                    }
                    else
                    {
                        status = StockInOutStatusEnum.已收货.ToString();
                    }
                }
                else if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                    || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                {
                    status = StockInOutStatusEnum.已收货.ToString();
                    purchaseOrderDO = new PurchaseOrderDO();
                }



                //查询当前登录用户  所在的门店 创建记录
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = createBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = locationId,
                    LocationName = locationName, //TODO
                    OperateTime = request.OperateTime,
                    OperationType = StockOperateTypeEnum.入库.ToString(),
                    Operator = string.Empty,
                    SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = status   //判断有无差异数目
                });

                //txw 20221206 
                //var transferDTO = new InventoryTransferDTO
                //{
                //    LocationId = locationId,
                //    LocationName = locationName,
                //    SupplierId = purchaseOrderDO.VenderId,
                //    SupplierName = purchaseOrderDO.VenderShortName,
                //    StockInOutId = stockId
                //};

                //await fullFlowLogRepository.InsertAsync(new FullFlowLogDO
                //{
                //    SourceInventoryNo=!string.IsNullOrWhiteSpace(request.SourceInventoryNo)? request.SourceInventoryNo: stockId.ToString(),
                //    IsDeleted =0,
                //    CreateBy = identityService.GetUserName(),
                //    CreateTime = DateTime.Now,
                //    FlowType = request.SourceType,
                //    FlowTypeText = request.SourceType,
                //    Content=""
                //});

                //txw 20221206 
                //if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                //    || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                //{
                //    await stockInoutRepository.GenerateStockNo(new StockInoutDO
                //    {
                //        Id = stockId,
                //        SourceInventoryNo = stockId.ToString()
                //    });
                //}

                if (request.StockItems.Any())
                {
                    foreach (var item in request.StockItems)
                    {
                        item.CreateBy = createBy;

                        #region 判断子单的状态
                        var tempStatus = string.Empty;
                        if (request.SourceType == StockInTypeEnum.采购入库.ToString())
                        {
                            if (item.DiffNum > 0 || item.ActualNum < item.Num)
                            {
                                tempStatus = StockInOutStatusEnum.部分收货.ToString();
                            }
                            else
                            {
                                tempStatus = StockInOutStatusEnum.已收货.ToString();
                            }
                        }
                        else if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                            || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                        {
                            tempStatus = StockInOutStatusEnum.已收货.ToString();
                        }
                        #endregion

                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            ActualNum = item.ActualNum,
                            IsDeleted = 0,
                            BadNum = item.BadNum,
                            BatchNo = string.Empty,
                            Cost = item.Cost,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            GoodNum = item.GoodNum,
                            InoutId = stockId,
                            Num = item.Num,
                            ProductId = item.ProductId.Trim(),
                            ProductName = item.ProductName,
                            ReleationId = item.ReleationId,  //其他入库  无关联,
                            Status = tempStatus, //判断有误差异数据
                            SupplierId = purchaseOrderDO != null ? purchaseOrderDO.VenderId : 0, //其他入库  无关联,
                            SupplierName = purchaseOrderDO != null ? purchaseOrderDO.VenderShortName : string.Empty,
                            UomText = item.UomText
                        });

                        //txw 20221206 
                        //其他入库的入库单自动生成关联单号
                        //if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                        //    || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                        //{
                        //    await this.stockInoutItemRepository.UpdateStockInoutRelationId(new StockInoutItemDO
                        //    {
                        //        Id = relationId,
                        //        ReleationId = relationId
                        //    });
                        //}

                        //txw 20221206 去掉批次，只更新库存
                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", locationId);
                        stockParams.Add("@product_id", item.ProductId.Trim());
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);
                        var inventoryData = inventoryResult?.FirstOrDefault();

                        if (inventoryData != null)
                        {
                            inventoryData.TotalNum = inventoryData.TotalNum + item.GoodNum;
                            inventoryData.TotalCost = inventoryData.TotalCost + item.GoodNum * item.Cost;
                            inventoryData.CanUseNum = inventoryData.CanUseNum + item.GoodNum;
                            inventoryData.UomText = item.UomText;
                            inventoryData.UpdateTime = DateTime.Now;
                            inventoryData.UpdateBy = createBy;
                            //更新表记录
                            await _stockManageRepository.UpdateAsync(inventoryData, new List<string> { "TotalNum", "TotalCost", "CanUseNum", "UomText", "UpdateTime", "UpdateBy" });

                            //await _stockManageRepository.UpdateInventoryData(new InventoryDO
                            //{
                            //    TotalNum = inventoryData.TotalNum + item.GoodNum,
                            //    TotalCost = inventoryData.TotalCost + item.GoodNum * item.Cost,
                            //    CanUseNum = inventoryData.CanUseNum + item.GoodNum,
                            //    UomText = item.UomText,
                            //    LocationId = locationId,
                            //    ProductId = item.ProductId.Trim(),
                            //    Id = inventoryData.Id,
                            //    UpdateBy = createBy
                            //});
                        }
                        else
                        {
                            //新增记录
                            await _stockManageRepository.InsertAsync(new InventoryDO
                            {
                                LocationId = locationId,
                                LocationName = locationName,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName,
                                SourceType = OwnerType.Shop.ToString(),
                                TotalCost = item.GoodNum * item.Cost,
                                TotalNum = item.GoodNum,
                                CanUseNum = item.GoodNum,
                                UomText = item.UomText,
                                IsDeleted = 0,
                                CreateBy = createBy,
                                CreateTime = DateTime.Now
                            });
                        }
                        //transferDTO.RelationId = relationId;
                        //transferDTO.CreateBy = createBy;
                        //var syncResult = await SyncInventoryDataIn(item, request, transferDTO);

                        //if (syncResult.Code == ResultCode.Success)
                        //{
                        //    //回写入库产品批次
                        //    await stockInoutItemRepository.UpdateStockInoutBatch(new StockInoutItemDO
                        //    {
                        //        Id = relationId,
                        //        BatchNo = syncResult.Data,
                        //        UpdateType = 1
                        //    });

                        //    res.Code = ResultCode.Success;
                        //}
                        //else
                        //{
                        //    //清除入库产品记录
                        //    await stockInoutItemRepository.DeleteStockInoutItem(new StockInoutItemDO { Id = relationId });

                        //    await stockInoutRepository.DeleteStockInout(new StockInoutDO { Id = stockId });

                        //    res.Code = ResultCode.Exception;
                        //    res.Message = "入库发生异常!";
                        //    return res;
                        //}
                    }

                    #region 回写采购单状态
                    if (request.SourceType == StockInTypeEnum.采购入库.ToString())
                    {
                        int purchaseOrderStatus = 0;

                        var stockInouts = await stockInoutRepository.GetListAsync(" where source_inventory_no =@purchaseId and source_type='采购入库' and is_deleted=0 ", new { purchaseId = request.SourceInventoryNo });
                        var inoutIds = stockInouts.Select(r => r.Id);

                        var stockInoutProducts = await stockInoutItemRepository.GetListAsync(" where inout_id in @inoutIds and is_deleted =0", new { inoutIds = inoutIds });

                        bool isInstock = true;
                        //判断采购下的所有产品是否都入库了
                        foreach (var item in stockInoutProducts.GroupBy(r => r.ReleationId))
                        {
                            var purchaseOrderProduct = await purchaseOrderProductRepository.GetAsync(item.Key);
                            if (purchaseOrderProduct != null)
                            {
                                //采购数量 和已入数量
                                if (purchaseOrderProduct.Num <= item.Sum(r => r.ActualNum))
                                {
                                    continue;
                                }
                                else
                                {
                                    isInstock = false;
                                    break;
                                }
                            }
                        }

                        //5.部分收货  6.全部收货
                        purchaseOrderStatus = isInstock ? 6 : 5;
                        await UpdateShopPurchaseStatusById(Convert.ToInt32(request.SourceInventoryNo), purchaseOrderStatus, createBy);
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"CreateStockTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 修改采购单状态
        /// </summary>
        /// <param name="purchaseOrderId">采购单号</param>
        /// <param name="status">采购单状态(1新建 2待发货 3已取消 4已发货 5部分收货 6已收货 )  </param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateShopPurchaseStatusById(int purchaseOrderId, int status, string createBy)
        {

            var res = new ApiResult<bool>();
            try
            {
                var purchaseOrderDO = await purchaseOrderRepository.GetAsync(purchaseOrderId);
                if (purchaseOrderDO == null || purchaseOrderDO.Id == 0)
                {
                    throw new CustomException("采购单不存在！");
                }
                var updateFile = new List<string>() { "Status", "UpdateBy", "UpdateTime" };
                purchaseOrderDO.UpdateTime = DateTime.Now;
                purchaseOrderDO.UpdateBy = createBy;
                purchaseOrderDO.Status = status;
                await purchaseOrderRepository.UpdateAsync(purchaseOrderDO, updateFile);

                var statusText = ((PurchaseStatusEnum)status).GetEnumDescription();
                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = purchaseOrderId.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = $"purchaseOrderId:{purchaseOrderId},status:{status}",
                    AfterValue = string.Empty,
                    Remark = "采购单" + (!string.IsNullOrWhiteSpace(statusText) ? statusText : string.Empty),
                    CreateBy = createBy,
                    CreateTime = DateTime.Now
                });

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateShopPurchaseStatusById_Error purchaseOrderId:{purchaseOrderId},status:{status}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<int> InsertPurchaseLog(PurchaseOrderLogDO request)
        {
            try
            {
                await purchaseOrderLogRepository.InsertAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error($"InsertPurchaseLog_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return 1;
        }

        /// <summary>
        /// 正向操作  外采入库专用！！！
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> SyncInventoryDataIn(StockInoutItemDTO item, StockInOutDTO request, InventoryTransferDTO transferDTO)
        {
            var res = new ApiResult<string>();
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //1.初始化库存汇总表
                    var stockParams = new DynamicParameters();
                    stockParams.Add("@location_id", transferDTO.LocationId);
                    stockParams.Add("@product_id", item.ProductId);
                    var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);
                    var inventoryData = inventoryResult?.FirstOrDefault();
                    
                    if (inventoryData == null)
                    {
                        //新增记录
                        var inventoryId =  await _stockManageRepository.InsertAsync(new InventoryDO
                        {
                            LocationId = transferDTO.LocationId,
                            LocationName = transferDTO.LocationName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            SourceType = OwnerType.Shop.ToString(),
                            TotalCost = 0, // item.GoodNum * item.Cost,
                            TotalNum = 0,// item.GoodNum,
                            CanUseNum = 0,// item.GoodNum,
                            IsDeleted = 0,
                            CreateBy = transferDTO.CreateBy,
                            CreateTime = DateTime.Now
                        });
                        inventoryData = await _stockManageRepository.GetAsync(inventoryId);
                    }

                    //2.生成批次记录
                    var batchResult = await inventoryBatchRepository.InsertAsync(new InventoryBatchDO
                    {
                        ShopId = transferDTO.LocationId,
                        InventoryId = inventoryData.Id,
                        SourceInventoryId = item.ReleationId > 0 ? item.ReleationId : transferDTO.RelationId,
                        SourceInventoryNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : transferDTO.StockInOutId.ToString(),
                        ProductId = item.ProductId,
                        ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                        ProductName = item.ProductName,
                        IsDeleted = 0,
                        Amount = item.GoodNum * item.Cost,
                        Cost = item.Cost,
                        SupplierId = transferDTO.SupplierId.ToString(),
                        SupplierName = transferDTO.SupplierName,
                        TotalNum = item.GoodNum,
                        CanUseNum = item.GoodNum,
                        CreateBy = transferDTO.CreateBy,
                        CreateTime = DateTime.Now,
                        OwnId = transferDTO.LocationId,
                        OwnType = OwnerType.Shop.ToString()
                    });

                    //3.生成批次流水记录
                    await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                    {
                        InventoryId = inventoryData.Id,
                        ShopId = transferDTO.LocationId,
                        ShopName = transferDTO.LocationName,
                        SourceId = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : transferDTO.StockInOutId.ToString(),
                        IsDeleted = 0,
                        Cost = item.Cost,
                        BatchNo = batchResult,
                        SourceInventoryNo = item.ReleationId > 0 ? item.ReleationId.ToString() : transferDTO.RelationId.ToString(),
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceType = request.SourceType,
                        ProductId = item.ProductId,
                        ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                        ProductName = item.ProductName,
                        CreateBy = transferDTO.CreateBy,
                        CreateTime = DateTime.Now,
                        BeforeTotalNum = 0,
                        AfterTotalNum = item.GoodNum,
                        BeforeCanUseNum = 0,
                        AfterCanUseNum = item.GoodNum,
                        Amount = item.GoodNum * item.Cost,
                        CurrentCanUseNum = item.GoodNum,
                        SupplierId = transferDTO.SupplierId.ToString(),
                        SupplierName = transferDTO.SupplierName,
                        OwnId = transferDTO.LocationId,
                        OwnType = OwnerType.Shop.ToString()
                    });

                    //4.生成库存流水记录
                    await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                    {
                        InventoryId = inventoryData.Id,
                        SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : transferDTO.StockInOutId.ToString(),
                        SourceInventoryNo = item.ReleationId > 0 ? item.ReleationId.ToString() : transferDTO.RelationId.ToString(),
                        LocationId = transferDTO.LocationId,
                        LocationName = transferDTO.LocationName,
                        IsDeleted = 0,
                        CreateBy = transferDTO.CreateBy,
                        CreateTime = DateTime.Now,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        BatchNo = batchResult.ToString(),
                        BusinessCategory = 1,
                        BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                        AfterTotalNum = (inventoryData?.TotalNum ?? 0) + item.GoodNum,
                        ChangeTotalNum = item.GoodNum,
                        BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                        AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) + item.GoodNum,
                        ChangeCanUseNum = item.GoodNum
                    });
                    //5.维护库存汇总表
                    if (inventoryData != null)
                    {
                        //更新表记录
                        await _stockManageRepository.UpdateInventoryData(new InventoryDO
                        {
                            TotalNum = inventoryData.TotalNum + item.GoodNum,
                            TotalCost = inventoryData.TotalCost + item.GoodNum * item.Cost,
                            CanUseNum = inventoryData.CanUseNum + item.GoodNum,
                            LocationId = transferDTO.LocationId,
                            ProductId = item.ProductId,
                            Id = inventoryData.Id,
                            UpdateBy = transferDTO.CreateBy
                        });
                    }
                    else
                    {
                        //新增记录
                        await _stockManageRepository.InsertAsync(new InventoryDO
                        {
                            LocationId = transferDTO.LocationId,
                            LocationName = transferDTO.LocationName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            SourceType = OwnerType.Shop.ToString(),
                            TotalCost = item.GoodNum * item.Cost,
                            TotalNum = item.GoodNum,
                            CanUseNum = item.GoodNum,
                            IsDeleted = 0,
                            CreateBy = transferDTO.CreateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                    res.Data = batchResult.ToString();
                    res.Code = ResultCode.Success;

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SyncInventoryDataIn_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 铺货商品盘盈操作
        /// </summary>
        /// <param name="item"></param>
        /// <param name="request"></param>
        /// <param name="locationId"></param>
        /// <param name="locationName"></param>
        /// <param name="purchaseOrderDO"></param>
        /// <param name="relationId"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SyncInventoryDataInForTransfer(StockInoutItemDTO item, StockInOutDTO request,
             InventoryTransferDTO transferDTO)
        {
            var res = new ApiResult<string>();
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //2.生成批次记录
                    var batchResult = await inventoryBatchRepository.InsertAsync(new InventoryBatchDO
                    {
                        ShopId = transferDTO.LocationId,
                        SourceInventoryId = item.ReleationId > 0 ? item.ReleationId : transferDTO.RelationId,
                        ProductId = item.ProductId,
                        ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                        ProductName = item.ProductName,
                        IsDeleted = 0,
                        Amount = item.GoodNum * item.Cost,
                        Cost = item.Cost,
                        SupplierId = transferDTO.SupplierId.ToString(),
                        SupplierName = transferDTO.SupplierName,
                        TotalNum = item.GoodNum,
                        CanUseNum = item.GoodNum,
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        OwnId = -1,
                        OwnType = OwnerType.Company.ToString(),
                        RefBatchNo = transferDTO.RefBatchNo.ToString()
                    });

                    //3.生成批次流水记录
                    await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                    {
                        ShopId = transferDTO.LocationId,
                        ShopName = transferDTO.LocationName,
                        SourceId = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : transferDTO.StockInOutId.ToString(),
                        IsDeleted = 0,
                        Cost = item.Cost,
                        BatchNo = batchResult,
                        SourceInventoryNo = item.ReleationId > 0 ? item.ReleationId.ToString() : transferDTO.RelationId.ToString(),
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceType = request.SourceType,
                        ProductId = item.ProductId,
                        ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                        ProductName = item.ProductName,
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        BeforeTotalNum = 0,
                        AfterTotalNum = item.GoodNum,
                        BeforeCanUseNum = 0,
                        AfterCanUseNum = item.GoodNum,
                        Amount = item.GoodNum * item.Cost,
                        CurrentCanUseNum = item.GoodNum,
                        SupplierId = transferDTO.SupplierId.ToString(),
                        SupplierName = transferDTO.SupplierName,
                        OwnId = -1,
                        OwnType = OwnerType.Company.ToString()
                    });

                    var stockParams = new DynamicParameters();
                    stockParams.Add("@location_id", transferDTO.LocationId);
                    stockParams.Add("@product_id", item.ProductId);
                    var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                    var inventoryData = inventoryResult?.FirstOrDefault();

                    //4.生成库存流水记录
                    await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                    {
                        SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : transferDTO.StockInOutId.ToString(),
                        LocationId = transferDTO.LocationId,
                        LocationName = transferDTO.LocationName,
                        IsDeleted = 0,
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        BatchNo = batchResult.ToString(),
                        BusinessCategory = 1,
                        BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                        AfterTotalNum = (inventoryData?.TotalNum ?? 0) + item.GoodNum,
                        ChangeTotalNum = item.GoodNum,
                        BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                        AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) + item.GoodNum,
                        ChangeCanUseNum = item.GoodNum
                    });
                    //5.维护库存汇总表
                    if (inventoryData != null)
                    {
                        //更新表记录
                        await _stockManageRepository.UpdateInventoryData(new InventoryDO
                        {
                            TotalNum = inventoryData.TotalNum + item.GoodNum,
                            TotalCost = inventoryData.TotalCost + item.GoodNum * item.Cost,
                            CanUseNum = inventoryData.CanUseNum + item.GoodNum,
                            LocationId = transferDTO.LocationId,
                            ProductId = item.ProductId,
                            UpdateBy = identityService.GetUserName()
                        });
                    }
                    else
                    {
                        //新增记录
                        await _stockManageRepository.InsertAsync(new InventoryDO
                        {
                            LocationId = transferDTO.LocationId,
                            LocationName = transferDTO.LocationName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            SourceType = OwnerType.Shop.ToString(),
                            TotalCost = item.GoodNum * item.Cost,
                            TotalNum = item.GoodNum,
                            CanUseNum = item.GoodNum,
                            IsDeleted = 0,
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now
                        });
                    }
                    res.Data = batchResult.ToString();
                    res.Code = ResultCode.Success;

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SyncInventoryDataIn_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        /// <summary>
        /// 查询产品出入库记录  --库存明细使用!!!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<StockInOutTempDTO>>> GetStockInoutItemsByPid(GetShopStockRequest request)
        {
            var res = new ApiResult<List<StockInOutTempDTO>>();

            if (request.ShopId <= 0)
            {
                var shopId = Convert.ToInt64(identityService.GetOrganizationId());
                request.ShopId = shopId;
            }

            //var shopInfo = await GetShopById();
            //request.ShopId = shopInfo?.ShopId ?? 7;
            try
            {
                var result = await stockInoutItemRepository.GetStockInoutItemsByPID(request);

                result.ForEach(t => t.Type = t.SourceType+"-"+t.OperationType+"-"+t.Status);

                res.Data = result?.OrderByDescending(r => r.CreateTime).ToList();
              
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStockInoutItemsByPid_err Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 查询产品库存记录  --出库使用!!!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<StockInoutItemDTO>>> GetStockInoutItems(GetStockInoutItemRequest request)
        {

            var res = new ApiResult<List<StockInoutItemDTO>>();

            var shopInfo = await GetShopById();
            request.ShopId = shopInfo?.ShopId ?? 7;
            try
            {
                if (request.ObjectType == StockOutTypeEnum.采购退货.ToString()
                     && !string.IsNullOrWhiteSpace(request.ObjectId))
                {
                    var result = await stockInoutItemRepository.GetStockInoutItemsByRelationIdAndType(request);

                    //需要扣减批次的数量

                    var vo = _mapper.Map<List<StockInoutItemDTO>>(result);
                    res.Data = vo?.OrderBy(r => r.ProductId).ToList();
                }
                else if (request.ObjectType == StockOutTypeEnum.其他出库.ToString() ||
                         request.ObjectType == StockOutTypeEnum.货损出库.ToString() ||
                         request.ObjectType == StockOutTypeEnum.订单安装.ToString())
                {
                    //根据前端选择的产品  查询产品在门店可用库存信息
                    var vo = new List<StockInoutItemDTO>();
                    var result = await this.inventoryBatchRepository.GetInventoryBatchs(request);
                    if (result.Any())
                    {
                        //组装数据
                        foreach (var item in result)
                        {
                            vo.Add(new StockInoutItemDTO
                            {
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                BatchNo = item.Id.ToString(), //关联批次
                                AvailableNum = Convert.ToInt32(item.CanUseNum)
                            });
                        }
                    }
                    res.Data = vo?.OrderBy(r => r.ProductId).ToList();
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStockInoutItems_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
        /// <summary>
        /// 查询明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<StockInOutDTO>> GetStockInOutInfo(StockInOutDTO request)
        {
            var res = new ApiResult<StockInOutDTO>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", request.StockId);
                //param.Add("@source_inventory_no", request.SourceInventoryNo);
                //param.Add("@operation_type", request.OperationType);
                var stockInOutInfos = await stockInoutRepository.GetListAsync(" where id =@id and is_deleted=0", param);

                var stockInOut = stockInOutInfos?.FirstOrDefault();

                if (stockInOut != null)
                {
                    var stockInOutItems = await stockInoutItemRepository.GetListAsync(" where inout_id =@stockId and is_deleted=0", new { stockId = stockInOut.Id });

                    var vo = _mapper.Map<StockInOutDTO>(stockInOut);

                    var items = _mapper.Map<List<StockInoutItemDTO>>(stockInOutItems);

                    //重新计算差异数
                    foreach (var item in items)
                    {
                        item.DiffNum = item.Num - item.ActualNum - item.BadNum;
                        item.GoodNum = item.ActualNum;
                    }

                    vo.StockItems = items;

                    res.Data = vo;
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStockInOutInfo_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 创建出库任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateOutStockTask(StockInOutDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await GetShopById();
            long locationId = shopInfo?.ShopId ?? 7;
            string locationName = shopInfo?.SimpleName ?? "测试门店";

            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? identityService.GetUserName() : request.CreateBy;
            //需要Check 订单
            if (request.SourceType == StockOutTypeEnum.订单安装.ToString())
            {
                bool isValid = false;
                if (!string.IsNullOrWhiteSpace(request.SourceInventoryNo))
                {
                    var orderInfoRes = await this._orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest
                    {
                        ShopId = locationId,
                        OrderNos = new List<string> { request.SourceInventoryNo }
                    });
                    if (orderInfoRes.Code == ResultCode.Success)
                    {
                        if (orderInfoRes.Data != null && orderInfoRes.Data.Any())
                        {
                            isValid = true;
                        }
                    }
                }

                if (!isValid)
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = "订单号不存在，请重新输入!"
                    };
                }
            }

            //查询关联的批次信息  将库存扣减掉
            //var batchIds = request.StockItems.Select(r => Convert.ToInt64(r.BatchNo)).ToList();
            //var batchInfos = await inventoryBatchRepository.GetInventoryBatchByIds(batchIds);

            //var batchInfoDic = batchInfos.ToDictionary(r => r.Id, r => r);

            try
            {
                // request.CreateBy = this.identityService.GetUserName();
                //查询当前登录用户  所在的门店 创建记录
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = createBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = locationId,
                    LocationName = locationName, //TODO
                    OperateTime = request.OperateTime,
                    OperationType = StockOperateTypeEnum.出库.ToString(),
                    Operator = request.Operator,
                    Remark = request.Remark,
                    SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                });

                if (string.IsNullOrWhiteSpace(request.SourceInventoryNo))
                {
                    await stockInoutRepository.GenerateStockNo(new StockInoutDO
                    {
                        Id = stockId,
                        SourceInventoryNo = stockId.ToString()
                    });
                }

                if (request.StockItems.Any())
                {
                    foreach (var item in request.StockItems)
                    {
                        if (item.Num <= 0)
                        {
                            continue;
                        }
                        var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            IsDeleted = 0,
                            BatchNo = item.BatchNo,
                            Cost = batchInfo.Cost,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            InoutId = stockId,
                            Num = item.Num,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ReleationId = item.ReleationId > 0 ? item.ReleationId : 0,  //其他入库  无关联,
                            Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                            SupplierId = Convert.ToInt64(batchInfo.SupplierId), //其他入库  无关联,
                            SupplierName = batchInfo.SupplierName,
                            UomText = "个"
                        });
                        if (item.ReleationId > 0)
                        {
                            await this.stockInoutItemRepository.UpdateStockInoutRelationId(new StockInoutItemDO
                            {
                                Id = relationId,
                                ReleationId = relationId
                            });
                        }

                        //var shopInfo = await shopMangeClient.GetShopById(new GetShopClientRequest
                        //{
                        //    ShopId = batchInfo.ShopId
                        //});

                        #region  扣减库存
                        //2.扣减批次信息
                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                        {
                            Id = batchInfo.Id,
                            UpdateBy = createBy,
                            CanUseNum = batchInfo.CanUseNum - item.Num
                            // TotalNum = batchInfo.TotalNum - item.Num
                        });

                        //获取原入库批次流信息
                        var inventoryBatchFlows = await _inventoryBatchFlowRepository.GetListAsync(" where batch_no =@batch_no and is_deleted=0", new { batch_no = item.BatchNo });
                        var inventoryBatchFlowResult = inventoryBatchFlows.FirstOrDefault(r => r.OperationType == StockOperateTypeEnum.入库.ToString());


                        //3.生成批次流水信息
                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                        {
                            ShopId = batchInfo.ShopId,
                            ShopName = inventoryBatchFlowResult.ShopName,
                            SourceId = inventoryBatchFlowResult.SourceId,
                            SourceInventoryNo = inventoryBatchFlowResult.SourceInventoryNo,
                            OperationType = StockOperateTypeEnum.出库.ToString(),
                            SourceType = request.SourceType,
                            ProductId = inventoryBatchFlowResult.ProductId,
                            ProductName = inventoryBatchFlowResult.ProductName,
                            BatchNo = Convert.ToInt64(item.BatchNo),
                            Price = inventoryBatchFlowResult.Price,
                            Amount = inventoryBatchFlowResult.Cost * item.Num,
                            Cost = inventoryBatchFlowResult.Cost,
                            //BeforeTotalNum = batchInfo.TotalNum,
                            //AfterTotalNum = batchInfo.TotalNum - item.Num,
                            //CurrentTotalNum = item.Num,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum - item.Num,
                            CurrentCanUseNum = batchInfo.CanUseNum - item.Num,
                            OwnId = batchInfo.OwnId,
                            OwnType = batchInfo.OwnType,
                            SupplierId = batchInfo.SupplierId,
                            SupplierName = batchInfo.SupplierName,
                            IsDeleted = 0,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now
                        });


                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", locationId);
                        stockParams.Add("@product_id", item.ProductId);
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                        var inventoryData = inventoryResult?.FirstOrDefault();

                        //4.生成库存流水信息 扣减
                        await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                        {
                            SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : stockId.ToString(),
                            LocationId = locationId,
                            LocationName = locationName,
                            BatchNo = item.BatchNo,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            BusinessCategory = 2,
                            BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                            AfterTotalNum = (inventoryData?.TotalNum ?? 0) - item.Num,
                            ChangeTotalNum = item.Num,
                            BeforeCanUseNum = inventoryData?.CanUseNum ?? 0,
                            AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) - item.Num,
                            ChangeCanUseNum = item.Num,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now
                        });

                        //5.扣减库存汇总表
                        if (inventoryData != null)
                        {
                            await _stockManageRepository.UpdateInventoryData(new InventoryDO
                            {
                                LocationId = locationId,
                                ProductId = item.ProductId,
                                UpdateBy = createBy,
                                TotalNum = inventoryData.TotalNum - item.Num,
                                CanUseNum = inventoryData.CanUseNum - item.Num,
                                TotalCost = inventoryData.TotalCost - (item.Num * batchInfo.Cost)
                            });
                        }
                        #endregion
                    }

                    //回写采购单的状态
                    if (request.SourceType == StockOutTypeEnum.采购退货.ToString())
                    {
                        var purchaseOrderProducts = await purchaseOrderProductRepository.GetListAsync(" where po_id =@po_id and is_deleted =0 and status<>3", new { po_id = request.SourceInventoryNo });
                        //var param = new DynamicParameters();
                        //param.Add("@source_inventory_no", request.SourceInventoryNo);
                        //param.Add("@source_type", StockOutTypeEnum.采购入库.ToString());

                        //var stockInOuts = await stockInoutRepository.GetListAsync(" where source_inventory_no =@source_inventory_no and is_deleted=0 and source_type =@source_type", param);
                        if (purchaseOrderProducts != null && purchaseOrderProducts.Any())
                        {
                            //var stockInOut = stockInOuts.First();

                            //var stockInoutItems = await stockInoutItemRepository.GetStockInoutItemsByStockId(new StockInoutItemDO
                            //{
                            //    InoutId = stockInOut.Id
                            //});
                            bool isAllOut = true;
                            foreach (var item in purchaseOrderProducts)
                            {
                                var stockInOutItems = request.StockItems.Where(r => r.ReleationId == item.Id);
                                if (stockInOutItems != null && stockInOutItems.Any())
                                {
                                    if (item.Num <= stockInOutItems.Sum(r => r.Num))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        isAllOut = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    isAllOut = false;
                                    break;
                                }
                            }
                            var status = isAllOut ? 4 : 5;
                            await UpdateShopPurchaseStatusById(int.Parse(request.SourceInventoryNo), status, createBy);
                        }

                    }
                    else if (request.SourceType == StockOutTypeEnum.订单安装.ToString())
                    {
                        //回写领料数量
                        await OrderInstallNotifyForStockOut(request);
                    }
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateStockTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 查询采购单入库情况
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<StockInoutItemDTO>>> GetPurchaseOrderProducts(StockInOutDTO request)
        {
            var res = new ApiResult<List<StockInoutItemDTO>>();
            try
            {
                //采购单是否已经入过库
                var param = new DynamicParameters();
                param.Add("@source_inventory_no", request.SourceInventoryNo);
                //param.Add("@operation_type", StockOperateTypeEnum.入库.ToString());
                param.Add("@source_type", StockInTypeEnum.采购入库.ToString());
                var stockInouts = await stockInoutRepository.GetListAsync(" where source_inventory_no =@source_inventory_no and is_deleted =0 and source_type=@source_type", param);

                var vo = new List<StockInoutItemDTO>();

                if (stockInouts != null && stockInouts.Any())
                {
                    //记录都需要做汇总
                    var inoutIds = stockInouts.Select(r => r.Id);
                    var stockItems = await stockInoutItemRepository.GetListAsync($" where inout_id in ({string.Join(",", inoutIds)}) and is_deleted=0");
                    foreach (var item in stockItems.GroupBy(r => r.ReleationId))
                    {
                        var stockItem = item.FirstOrDefault();

                        vo.Add(new StockInoutItemDTO
                        {
                            ProductId = stockItem.ProductId,
                            ProductName = stockItem.ProductName,
                            Num = stockItem.Num,
                            ActualNum = item.Sum(r => r.ActualNum),
                            //BadNum = item.Sum(r => r.BadNum),
                            Cost = stockItem.Cost,
                            DiffNum = stockItem.Num - item.Sum(r => r.ActualNum) - item.Sum(r => r.BadNum),
                            ReleationId = stockItem.ReleationId
                        });
                    }
                }
                else
                {
                    //采购单必须是发货中/部分收货状态才能入库
                    var purchaseOrderRes = await purchaseOrderRepository.GetListAsync(" where id =@id and is_deleted=0 and (status<>3 and status<>6) ", new { id = request.SourceInventoryNo });

                    var purchaseOrder = purchaseOrderRes?.FirstOrDefault();
                    if (purchaseOrder != null)
                    {
                        var result = await purchaseOrderProductRepository.GetListAsync(" where po_id =@poid and is_deleted=0 and status <>3", new { poid = request.SourceInventoryNo });

                        if (result != null && result.Any())
                        {
                            foreach (var item in result)
                            {
                                vo.Add(new StockInoutItemDTO
                                {
                                    ProductId = item.ProductCode,
                                    ProductName = item.ProductName,
                                    Num = item.Num,
                                    ReleationId = item.Id,
                                    Cost = item.PurchaseCost,
                                    DiffNum = item.Num
                                });
                            }
                        }
                    }
                }
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetPurchaseOrderProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 外部使用接口(批次记录已存在)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateInStockTaskCommon(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                if (!request.StockItems.Any())
                {
                    res.Message = "入库明细不能为空!";
                    res.Code = ResultCode.Exception;
                    return res;
                }

                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    SourceInventoryNo = request.SourceInventoryNo,
                    IsDeleted = 0,
                    CreateBy = request.CreateBy,
                    CreateTime = DateTime.Now,
                    LocationId = request.LocationId,
                    LocationName = request.LocationName,
                    OperateTime = DateTime.Now.AddDays(2),
                    Operator = request.Operator,
                    OperationType = request.OperationType,
                    SourceType = request.SourceType,
                    Status = StockInOutStatusEnum.新建.ToString(),
                    Remark = request.Remark
                });

                foreach (var item in request.StockItems)
                {
                    var inventoryBatchRes = await inventoryBatchRepository.GetListAsync(" where id = @id and is_deleted=0", new { id = item.BatchNo });
                    var inventoryBatch = inventoryBatchRes.FirstOrDefault();
                    if (inventoryBatch != null)
                    {
                        await stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            IsDeleted = 0,
                            Num = item.Num,
                            ActualNum = item.ActualNum,
                            GoodNum = item.GoodNum,
                            Cost = inventoryBatch.Cost,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            InoutId = stockId,
                            BatchNo = item.BatchNo,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ReleationId = item.ReleationId,
                            SupplierId = Convert.ToInt64(inventoryBatch.SupplierId),
                            SupplierName = inventoryBatch.SupplierName,
                            Status = StockInOutStatusEnum.新建.ToString()
                        });
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateInStockTaskCommon_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 盘亏出库 库存扭转
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateStorageOutStockTask(StockInOutDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await GetShopById();
            long locationId = shopInfo?.ShopId ?? 7;
            string locationName = shopInfo?.SimpleName ?? "测试门店";

            try
            {
                //只有一条记录！！
                if (request.StockItems.Any())
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var stockProduct = request.StockItems.FirstOrDefault();

                        var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@locationId and product_id = @productId and is_deleted =0 and can_use_num>0",
                            new { locationId = locationId, productId = stockProduct.ProductId });
                        //判断每个批次的可用库存 -占用库存-锁定库存

                        if (inventoryBatchs.Any())
                        {
                            var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                            {
                                CreateBy = this.identityService.GetUserName(),
                                IsDeleted = 0,
                                CreateTime = DateTime.Now,
                                LocationId = locationId,
                                LocationName = locationName, //TODO
                                OperateTime = request.OperateTime,
                                OperationType = StockOperateTypeEnum.出库.ToString(),
                                Operator = request.Operator,
                                Remark = request.Remark,
                                SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                                SourceType = request.SourceType,
                                Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                            });

                            inventoryBatchs = inventoryBatchs.OrderBy(r => r.Id); //先进先出原则
                            var batchIds = inventoryBatchs.Select(r => r.Id);

                            var inventoryItems = await inventoryFlowItemRepository.GetListAsync(" where location_id =@locationId and batch_no in @batchNos and is_deleted =0 ", new { locationId = locationId, batchNos = batchIds });
                            var inventoryItemDic = new Dictionary<string, List<InventoryFlowItemDO>>();

                            foreach (var item in inventoryItems)
                            {
                                if (inventoryItemDic.ContainsKey(item.BatchNo))
                                {
                                    inventoryItemDic[item.BatchNo].Add(item);
                                }
                                else
                                {
                                    inventoryItemDic[item.BatchNo] = new List<InventoryFlowItemDO>();
                                    inventoryItemDic[item.BatchNo].Add(item);
                                }
                            }

                        TODO: //批次的总库存是不动
                              //取批次的可用库存 - 修改后的 占用库存 和锁定库存
                            long diffNum = stockProduct.Num;

                            var tempStockOutProducts = new List<StockOutProductDTO>();

                            //获取需要扣减的批次和数量  先进先出的扣
                            foreach (var item in inventoryBatchs)
                            {
                                if (diffNum > 0)
                                {
                                    if (inventoryItemDic.ContainsKey(item.Id.ToString()))
                                    {
                                        inventoryItems = inventoryItemDic[item.Id.ToString()];
                                        long sumUnAvailableNum = inventoryItems.Sum(r => r.AfterOccupyNum) + inventoryItems.Sum(r => r.AfterLockNum);

                                        long availableNum = item.CanUseNum - sumUnAvailableNum;

                                        if (diffNum > availableNum)
                                        {
                                            tempStockOutProducts.Add(new StockOutProductDTO
                                            {
                                                BatchNo = item.Id,
                                                Num = availableNum
                                            });
                                            diffNum -= availableNum;
                                            //这个批次的数据全部扣减掉
                                            continue;
                                        }
                                        else
                                        {
                                            tempStockOutProducts.Add(new StockOutProductDTO
                                            {
                                                BatchNo = item.Id,
                                                Num = diffNum
                                            });
                                            //部分扣掉  把差异的库存给扣掉
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (item.CanUseNum > diffNum)
                                        {
                                            tempStockOutProducts.Add(new StockOutProductDTO
                                            {
                                                BatchNo = item.Id,
                                                Num = diffNum
                                            });
                                            //部分扣掉
                                            break;
                                        }
                                        else
                                        {
                                            tempStockOutProducts.Add(new StockOutProductDTO
                                            {
                                                BatchNo = item.Id,
                                                Num = item.CanUseNum
                                            });

                                            diffNum -= item.CanUseNum;
                                            //这个批次的数据全部扣减掉
                                            continue;
                                        }
                                    }
                                }
                            }

                            if (tempStockOutProducts.Any())
                            {
                                #region  获取产品总库存记录
                                var stockParams = new DynamicParameters();
                                stockParams.Add("@location_id", locationId);
                                stockParams.Add("@product_id", stockProduct.ProductId);
                                var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                                var inventoryData = inventoryResult?.FirstOrDefault();

                                var totalNum = inventoryData.TotalNum;
                                var canUseNum = inventoryData.CanUseNum;
                                var totalCost = inventoryData.TotalCost;
                                #endregion

                                foreach (var item in tempStockOutProducts)
                                {
                                    var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                                    //出入库产品
                                    var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                                    {
                                        IsDeleted = 0,
                                        BatchNo = item.BatchNo.ToString(),
                                        Cost = batchInfo.Cost,
                                        CreateBy = this.identityService.GetUserName(),
                                        CreateTime = DateTime.Now,
                                        InoutId = stockId,
                                        Num = Convert.ToInt32(item.Num),
                                        ProductId = batchInfo.ProductId,
                                        ProductName = batchInfo.ProductName,
                                        ReleationId = stockProduct.ReleationId,  //其他入库  无关联,
                                        Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                                        SupplierId = Convert.ToInt64(batchInfo.SupplierId), //其他入库  无关联,
                                        SupplierName = batchInfo.SupplierName,
                                        UomText = "个"
                                    });

                                    //2.扣减批次信息
                                    await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                                    {
                                        Id = batchInfo.Id,
                                        UpdateBy = identityService.GetUserName(),
                                        CanUseNum = batchInfo.CanUseNum - item.Num
                                        // TotalNum = batchInfo.TotalNum - item.Num
                                    });

                                    //获取原入库批次流信息
                                    var inventoryBatchFlows = await _inventoryBatchFlowRepository.GetListAsync(" where batch_no =@batch_no and is_deleted=0", new { batch_no = item.BatchNo });
                                    var inventoryBatchFlowResult = inventoryBatchFlows.FirstOrDefault(r => r.OperationType == StockOperateTypeEnum.入库.ToString());

                                    //3.生成批次流水信息
                                    await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                                    {
                                        ShopId = batchInfo.ShopId,
                                        ShopName = inventoryBatchFlowResult.ShopName,
                                        SourceId = inventoryBatchFlowResult.SourceId,
                                        SourceInventoryNo = inventoryBatchFlowResult.SourceInventoryNo,
                                        OperationType = StockOperateTypeEnum.出库.ToString(),
                                        SourceType = request.SourceType,
                                        ProductId = inventoryBatchFlowResult.ProductId,
                                        ProductName = inventoryBatchFlowResult.ProductName,
                                        BatchNo = Convert.ToInt64(item.BatchNo),
                                        Price = inventoryBatchFlowResult.Price,
                                        Amount = inventoryBatchFlowResult.Cost * item.Num,
                                        Cost = inventoryBatchFlowResult.Cost,
                                        //BeforeTotalNum = batchInfo.TotalNum,
                                        //AfterTotalNum = batchInfo.TotalNum - item.Num,
                                        //CurrentTotalNum = item.Num,
                                        BeforeCanUseNum = batchInfo.CanUseNum,
                                        AfterCanUseNum = batchInfo.CanUseNum - item.Num,
                                        CurrentCanUseNum = batchInfo.CanUseNum - item.Num,
                                        OwnId = batchInfo.OwnId,
                                        OwnType = batchInfo.OwnType,
                                        SupplierId = batchInfo.SupplierId,
                                        SupplierName = batchInfo.SupplierName,
                                        IsDeleted = 0,
                                        CreateBy = identityService.GetUserName(),
                                        CreateTime = DateTime.Now
                                    });


                                    //4.生成库存流水信息 扣减
                                    await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                    {
                                        SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : stockId.ToString(),
                                        LocationId = locationId,
                                        LocationName = locationName,
                                        BatchNo = item.BatchNo.ToString(),
                                        ProductId = inventoryBatchFlowResult.ProductId,
                                        ProductName = inventoryBatchFlowResult.ProductName,
                                        BusinessCategory = 2,
                                        BeforeTotalNum = totalNum,//总数量要扣掉
                                        AfterTotalNum = totalNum - item.Num,
                                        ChangeTotalNum = item.Num,
                                        BeforeCanUseNum = canUseNum,
                                        AfterCanUseNum = canUseNum - item.Num,
                                        ChangeCanUseNum = item.Num,
                                        CreateBy = identityService.GetUserName() ?? "System",
                                        CreateTime = DateTime.Now
                                    });

                                    //扣减产品的总库存量和成本
                                    totalNum -= item.Num;
                                    canUseNum -= item.Num;
                                    totalCost -= item.Num * batchInfo.Cost;
                                }

                                //将这个产品占用的批次
                                await _stockManageRepository.UpdateInventoryData(new InventoryDO
                                {
                                    LocationId = locationId,
                                    ProductId = stockProduct.ProductId,
                                    UpdateBy = identityService.GetUserName(),
                                    TotalNum = totalNum,
                                    CanUseNum = canUseNum,
                                    TotalCost = totalCost
                                });
                            }
                            res.Code = ResultCode.Success;
                        }
                        else
                        {
                            res.Code = ResultCode.Exception;
                            res.Message = $"产品【{stockProduct.ProductName}】盘亏异常,无可用库存扣减!";
                        }

                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateStorageOutStockTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ShopSimpleDTO> GetShopById()
        {
            var orgType = identityService.GetOrgType();
            var shopId = Convert.ToInt64(identityService.GetOrganizationId());
            if (orgType == "1")  //门店
            {
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = shopId
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    var shopSimpleInfo = new ShopSimpleDTO
                    {
                        ShopId = shopId,
                        SimpleName = shopResult.Data.SimpleName
                    };
                    return shopSimpleInfo;
                }
            }
            else if (orgType == "0") //公司
            {
                var companyInfo = await _companyRepository.GetAsync(shopId);
                if (companyInfo != null)
                {
                    var shopSimpleInfo = new ShopSimpleDTO
                    {
                        ShopId = shopId,
                        SimpleName = companyInfo.SimpleName
                    };
                    return shopSimpleInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 订单取消 释放库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderCancelReleaseStock(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //1.生成出入库记录
                // request.CreateBy = this.identityService.GetUserName();
                //查询当前登录用户  所在的门店 创建记录
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = request.CreateBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = request.LocationId,
                    LocationName = request.LocationName, //TODO
                    OperateTime = request.OperateTime,
                    OperationType = StockOperateTypeEnum.入库.ToString(),
                    Operator = string.Empty,
                    SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = request.Status  //判断有无差异数目
                });

                if (request.StockItems.Any())
                {
                    foreach (var item in request.StockItems)
                    {
                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            ActualNum = item.Num,
                            IsDeleted = 0,
                            BadNum = 0,
                            BatchNo = item.BatchNo,
                            Cost = item.Cost,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            GoodNum = item.Num,
                            InoutId = stockId,
                            Num = item.Num,
                            ProductId = item.ProductId.Trim(),
                            ProductName = item.ProductName,
                            ReleationId = item.ReleationId,  //其他入库  无关联,
                            Status = request.Status, //判断有误差异数据
                            SupplierId = item.SupplierId, //其他入库  无关联,
                            SupplierName = item.SupplierName,
                            UomText = "个"
                        });
                        var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                        //2.批次记录更新
                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                        {
                            UpdateBy = request.CreateBy,
                            Id = Convert.ToInt64(item.BatchNo),
                            CanUseNum = batchInfo.CanUseNum + item.Num
                        });

                        //3.批次流水更新
                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                        {
                            ShopId = request.LocationId,
                            ShopName = request.LocationName,
                            SourceId = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : stockId.ToString(),
                            IsDeleted = 0,
                            Cost = item.Cost,
                            BatchNo = Convert.ToInt64(item.BatchNo),
                            SourceInventoryNo = item.ReleationId > 0 ? item.ReleationId.ToString() : relationId.ToString(),
                            OperationType = StockOperateTypeEnum.入库.ToString(),
                            SourceType = request.SourceType,
                            ProductId = item.ProductId,
                            ProductAttrType = batchInfo.ProductAttrType,
                            ProductName = item.ProductName,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            BeforeTotalNum = batchInfo.TotalNum, //TODO  修改数量 原批次上加
                            AfterTotalNum = batchInfo.TotalNum,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum + item.GoodNum,
                            Amount = (batchInfo.CanUseNum + item.GoodNum) * item.Cost,
                            CurrentCanUseNum = batchInfo.CanUseNum + item.GoodNum,
                            SupplierId = item.SupplierId.ToString(),
                            SupplierName = item.SupplierName,
                            OwnId = batchInfo.OwnId,
                            OwnType = batchInfo.OwnType
                        });

                        //4.库存流水更新
                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", request.LocationId);
                        stockParams.Add("@product_id", item.ProductId);
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                        var inventoryData = inventoryResult?.FirstOrDefault();

                        //4.生成库存流水记录
                        await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                        {
                            SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : stockId.ToString(),
                            LocationId = request.LocationId,
                            LocationName = request.LocationName,
                            IsDeleted = 0,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            BatchNo = item.BatchNo,
                            BusinessCategory = 1,
                            BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                            AfterTotalNum = (inventoryData?.TotalNum ?? 0) + item.GoodNum,
                            ChangeTotalNum = item.GoodNum,
                            BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                            AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) + item.GoodNum,
                            ChangeCanUseNum = item.GoodNum
                        });
                        //5.维护库存汇总表
                        if (inventoryData != null)
                        {
                            //更新表记录
                            await _stockManageRepository.UpdateInventoryData(new InventoryDO
                            {
                                TotalNum = inventoryData.TotalNum + item.GoodNum,
                                TotalCost = inventoryData.TotalCost + item.GoodNum * item.Cost,
                                CanUseNum = inventoryData.CanUseNum + item.GoodNum,
                                LocationId = request.LocationId,
                                ProductId = item.ProductId,
                                UpdateBy = request.CreateBy
                            });
                        }
                    }
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderCancelReleaseStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 查询门店可用库存记录
        /// </summary>
        /// <param name="inventoryBatch"></param>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<InventoryBatchDTO>>> GetAvailableBatchs(GetAvailableBatchsRequest request)
        {
            var res = new ApiResult<List<InventoryBatchDTO>>();

            try
            {
                //只占用铺货的库存
                //20200925 改动：去掉货主限制
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id =@product_id and is_deleted=0",
                    new
                    {
                        shop_Id = request.ShopId,
                        product_id = request.ProductId
                    });

                if (inventoryBatchs.Any())
                {
                    var batchNos = inventoryBatchs.Select(r => r.Id);

                    //查询批次占用
                    var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = request.ProductId, location_id = request.ShopId });
                    var inventoryItemDic = new Dictionary<string, List<InventoryFlowItemDO>>();

                    foreach (var item in inventoryFlowItems)
                    {
                        if (inventoryItemDic.ContainsKey(item.BatchNo))
                        {
                            inventoryItemDic[item.BatchNo].Add(item);
                        }
                        else
                        {
                            inventoryItemDic[item.BatchNo] = new List<InventoryFlowItemDO>();
                            inventoryItemDic[item.BatchNo].Add(item);
                        }
                    }

                    //获取批次可用库存
                    foreach (var item in inventoryBatchs)
                    {
                        if (inventoryItemDic.ContainsKey(item.Id.ToString()))
                        {
                            var inventoryItems = inventoryItemDic[item.Id.ToString()];
                            long sumUnAvailableNum = inventoryItems.Sum(r => r.AfterOccupyNum) + inventoryItems.Sum(r => r.AfterLockNum);
                            item.AvailableNum = item.CanUseNum - sumUnAvailableNum;
                        }
                        else
                        {
                            item.AvailableNum = item.CanUseNum;
                        }
                    }
                    res.Data = _mapper.Map<List<InventoryBatchDTO>>(inventoryBatchs);
                }
                else
                {
                    res.Data = new List<InventoryBatchDTO>();
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetAvailableBatchs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 忽略货主概念
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<InventoryBatchDTO>>> GetShopAvailableBatchs(GetAvailableBatchsRequest request)
        {
            var res = new ApiResult<List<InventoryBatchDTO>>();
            try
            {
                //只占用铺货的库存
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id =@product_id and is_deleted=0",
                    new
                    {
                        shop_Id = request.ShopId,
                        product_id = request.ProductId
                    });

                if (inventoryBatchs.Any())
                {
                    var batchNos = inventoryBatchs.Select(r => r.Id);

                    //查询批次占用
                    var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = request.ProductId, location_id = request.ShopId });
                    var inventoryItemDic = new Dictionary<string, List<InventoryFlowItemDO>>();

                    foreach (var item in inventoryFlowItems)
                    {
                        if (inventoryItemDic.ContainsKey(item.BatchNo))
                        {
                            inventoryItemDic[item.BatchNo].Add(item);
                        }
                        else
                        {
                            inventoryItemDic[item.BatchNo] = new List<InventoryFlowItemDO>();
                            inventoryItemDic[item.BatchNo].Add(item);
                        }
                    }

                    //获取批次可用库存
                    foreach (var item in inventoryBatchs)
                    {
                        if (inventoryItemDic.ContainsKey(item.Id.ToString()))
                        {
                            var inventoryItems = inventoryItemDic[item.Id.ToString()];
                            long sumUnAvailableNum = inventoryItems.Sum(r => r.AfterOccupyNum) + inventoryItems.Sum(r => r.AfterLockNum);
                            item.AvailableNum = item.CanUseNum - sumUnAvailableNum;
                        }
                        else
                        {
                            item.AvailableNum = item.CanUseNum;
                        }
                    }
                    res.Data = _mapper.Map<List<InventoryBatchDTO>>(inventoryBatchs);
                }
                else
                {
                    res.Data = new List<InventoryBatchDTO>();
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetShopAvailableBatchs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 订单安装扣减占用库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderInstallReduceStock(OrderStockRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var occupyItems = await _occupyItemRepository.GetListAsync(" where source_inventory_no=@source_inventory_no and is_deleted=0 and is_valid=1",
                new
                {
                    source_inventory_no = request.QueueNo
                });

                if (occupyItems.Any())
                {
                    var orderItem = occupyItems.First();
                    ShopSimpleDTO shopSimpleInfo = null;
                    var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                    {
                        ShopId = orderItem.LocationId
                    });
                    if (shopResult.Code == ResultCode.Success)
                    {
                        shopSimpleInfo = new ShopSimpleDTO
                        {
                            ShopId = orderItem.LocationId,
                            SimpleName = shopResult.Data.SimpleName
                        };
                    }

                    //1.生成出库任务
                    var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                    {
                        CreateBy = request.CreateBy,
                        IsDeleted = 0,
                        CreateTime = DateTime.Now,
                        LocationId = orderItem.LocationId,
                        LocationName = shopSimpleInfo != null ? shopSimpleInfo.SimpleName : string.Empty, //TODO
                        OperateTime = DateTime.Now,
                        OperationType = StockOperateTypeEnum.出库.ToString(),
                        Operator = string.Empty,
                        SourceInventoryNo = request.QueueNo, //如果是空的 需要手动生成
                        SourceType = StockOutTypeEnum.订单安装.ToString(),
                        Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                    });

                    foreach (var item in occupyItems)
                    {
                        int num = Convert.ToInt32(item.Num);

                        var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            ActualNum = num,
                            IsDeleted = 0,
                            BadNum = 0,
                            BatchNo = item.BatchNo,
                            Cost = item.Cost,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            GoodNum = num,
                            InoutId = stockId,
                            Num = num,
                            ProductId = item.ProductId.Trim(),
                            ProductName = item.ProductName,
                            ReleationId = item.OrderProductId,  //其他入库  无关联,
                            Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                            SupplierId = batchInfo != null ? Convert.ToInt64(batchInfo.SupplierId) : 0, //其他入库  无关联,
                            SupplierName = batchInfo != null ? batchInfo.SupplierName : string.Empty,
                            UomText = "个"
                        });

                        //2.扣除批次可用数量
                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                        {
                            Id = batchInfo.Id,
                            UpdateBy = request.CreateBy,
                            CanUseNum = batchInfo.CanUseNum - item.Num
                        });

                        //3.生成批次流水记录
                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                        {
                            ShopId = item.LocationId,
                            ShopName = shopSimpleInfo != null ? shopSimpleInfo.SimpleName : string.Empty,
                            SourceId = request.QueueNo,
                            IsDeleted = 0,
                            Cost = item.Cost,
                            BatchNo = batchInfo.Id,
                            SourceInventoryNo = item.OrderProductId.ToString(),
                            OperationType = StockOperateTypeEnum.出库.ToString(),
                            SourceType = StockOutTypeEnum.订单安装.ToString(),
                            ProductId = item.ProductId,
                            ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                            ProductName = item.ProductName,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now,
                            BeforeTotalNum = 0,
                            AfterTotalNum = batchInfo.TotalNum,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum - item.Num,
                            Amount = item.Num * item.Cost,
                            CurrentCanUseNum = batchInfo.CanUseNum - item.Num,
                            SupplierId = batchInfo.SupplierId,
                            SupplierName = batchInfo.SupplierName,
                            OwnId = batchInfo.OwnId,
                            OwnType = batchInfo.OwnType
                        });

                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", orderItem.LocationId);
                        stockParams.Add("@product_id", item.ProductId);
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                        var inventoryData = inventoryResult?.FirstOrDefault();
                        //4.扣除锁定数量
                        await inventoryFlowItemRepository.UpdateInventoryFlowItemOccupy(new InventoryFlowItemDO
                        {
                            UpdateBy = request.CreateBy,
                            StockIds = new List<long> { item.InventoryId },
                            BeforeTotalNum = inventoryData.TotalNum,
                            AfterTotalNum = inventoryData.TotalNum - item.Num,
                            ChangeTotalNum = item.Num,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum - item.Num,
                            ChangeCanUseNum = item.Num
                        });

                        //5.扣除总库存
                        await _stockManageRepository.UpdateInventoryData(new InventoryDO
                        {
                            UpdateBy = request.CreateBy,
                            LocationId = shopSimpleInfo.ShopId,
                            ProductId = item.ProductId,
                            TotalCost = inventoryData.TotalCost - batchInfo.Cost * item.Num,
                            TotalNum = inventoryData.TotalNum - item.Num,
                            CanUseNum = inventoryData.CanUseNum - item.Num
                        });
                    }

                    await _occupyItemRepository.UpdateOccupyItemValid(new OccupyItemDO
                    {
                        UpdateBy = request.CreateBy,
                        SourceInventoryNo = request.QueueNo,
                        IsValid = 2
                    });

                    //把这部分库存 出到目标门店
                    //var orderInfo = await _orderClient.QueryUseStockOrderDetail(new QueryUseStockOrderDetailClientRequest
                    //{
                    //    OrderNo = request.QueueNo
                    //});
                    //if (orderInfo.Code == ResultCode.Success
                    //    && orderInfo.Data != null)
                    //{
                    //    if (orderInfo.Data.ProduceType == 2)
                    //    {
                    //        var stockInOut = new StockInOutDTO
                    //        {
                    //            CreateBy = request.CreateBy,
                    //            OperateTime = DateTime.Now,
                    //            SourceType = StockInTypeEnum.其他入库.ToString(),
                    //            LocationId = request.TargetShopId
                    //        };

                    //        foreach (var item in occupyItems)
                    //        {
                    //            stockInOut.StockItems.Add(new StockInoutItemDTO
                    //            {
                    //                Num = Convert.ToInt32(item.Num),
                    //                ActualNum = Convert.ToInt32(item.Num),
                    //                ProductId = item.ProductId,
                    //                ProductName = item.ProductName,
                    //                CreateBy = request.CreateBy,
                    //                GoodNum = Convert.ToInt32(item.Num)
                    //            });
                    //        }
                    //        await CreateInStockTask(stockInOut);
                    //    }
                    //}
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderInstallReduceStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 检查订单扣减库存,不论是否占用都扣减，但不重复扣
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderRepeatReduceStock(OrderStockRequest request)
        {
            if (string.IsNullOrEmpty(request.OperationType)) { request.OperationType = StockOperateTypeEnum.出库.ToString(); }
            if (string.IsNullOrEmpty(request.SourceType)) { request.SourceType = StockOutTypeEnum.订单安装.ToString(); }

            var res = new ApiResult<string>();
            try
            {
                QueryOrderDetailUseStockResponse orderInfo = null;
                //是否有实物产品
                var orderResult = await _orderClient.QueryOrderRealProductDetail(new QueryUseStockOrderDetailClientRequest
                {
                    OrderNo = request.QueueNo
                });
                if (orderResult.Code == ResultCode.Success
                    && orderResult.Data != null && orderResult.Data.Products != null && orderResult.Data.Products.Any())
                {
                    //订单有实物产品时继续操作
                    orderInfo = orderResult.Data;
                }
                else
                {
                    //无实物产品直接退出
                    res.Code = ResultCode.Success;
                    return res;
                }


                //出库记录
                var stockInout = await stockInoutRepository.GetListAsync(@" where source_inventory_no=@source_inventory_no and is_deleted=0 
                    and operation_type = @operation_type and source_type = @source_type ",
                new
                {
                    source_inventory_no = request.QueueNo,
                    operation_type = request.OperationType,
                    source_type = request.SourceType
                });

                //已经出库则直接退出
                if (stockInout != null && stockInout.Any())
                {
                    res.Code = ResultCode.Success;
                    return res;
                }


                ShopSimpleDTO shopSimpleInfo = null;
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = orderInfo.ShopId
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    shopSimpleInfo = new ShopSimpleDTO
                    {
                        ShopId = orderInfo.ShopId,
                        SimpleName = shopResult.Data.SimpleName
                    };
                }

                //1.生成出库任务
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = request.CreateBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = orderInfo.ShopId,
                    LocationName = shopSimpleInfo != null ? shopSimpleInfo.SimpleName : string.Empty, //TODO
                    OperateTime = DateTime.Now,
                    OperationType = request.OperationType,
                    Operator = string.Empty,
                    SourceInventoryNo = request.QueueNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                });

                foreach (var item in orderInfo.Products)
                {
                    int num = Convert.ToInt32(item.TotalNumber);

                    //先不查可用批次，以后再说
                    //var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                    var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                    {
                        ActualNum = num,
                        IsDeleted = 0,
                        BadNum = 0,
                        BatchNo = string.Empty,
                        Cost = 0, // item.Cost,
                        CreateBy = request.CreateBy,
                        CreateTime = DateTime.Now,
                        GoodNum = num,
                        InoutId = stockId,
                        Num = num,
                        ProductId = item.ProductId.Trim(),
                        ProductName = item.ProductName,
                        ReleationId = item.Id,  //其他入库  无关联,
                        Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                        SupplierId = 0, //batchInfo != null ? Convert.ToInt64(batchInfo.SupplierId) : 0, //其他入库  无关联,
                        SupplierName =string.Empty, //string.Empty, batchInfo != null ? batchInfo.SupplierName : string.Empty,
                        UomText = item.Unit
                    });


                    var stockParams = new DynamicParameters();
                    stockParams.Add("@location_id", orderInfo.ShopId);
                    stockParams.Add("@product_id", item.ProductId);
                    var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                    var inventoryData = inventoryResult?.FirstOrDefault();

                    //库存扣减标记
                    int iKey = (request.OperationType == StockOperateTypeEnum.出库.ToString()) ? -1 : 1;

                    //5.维护库存汇总表
                    if (inventoryData != null)
                    {
                        //更新表记录
                        await _stockManageRepository.UpdateInventoryData(new InventoryDO
                        {
                            UpdateBy = request.CreateBy,
                            LocationId = shopSimpleInfo.ShopId,
                            ProductId = item.ProductId,
                            Id = inventoryData.Id,
                            TotalCost = 0, // inventoryData.TotalCost - batchInfo.Cost * item.Num,
                            TotalNum = inventoryData.TotalNum + iKey * num,
                            CanUseNum = inventoryData.CanUseNum + iKey * num
                        });
                    }
                    else
                    {
                        //新增记录
                        await _stockManageRepository.InsertAsync(new InventoryDO
                        {
                            LocationId = shopSimpleInfo.ShopId,
                            LocationName = shopSimpleInfo.SimpleName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            SourceType = item.ProductAttribute == 0? OwnerType.Company.ToString() : OwnerType.Shop.ToString(),
                            TotalCost = 0,// actualNum * transferProductInfo.Cost,
                            TotalNum = num * iKey,
                            CanUseNum = num * iKey,
                            UomText = item.Unit,
                            IsDeleted = 0,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                  
                }

            
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderRepeatReduceStock Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 铺货同步库存记录
        /// </summary>
        /// <param name="packageDO"></param>
        /// <param name="productInfo"></param>
        /// <param name="transferProduct"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> TransferSyncInventoryData(object transferProduct,
            ShopSimpleDTO shopInfo, ProductTransferDTO productTransfer)
        {
            var res = new ApiResult<string>();
            try
            {
                string createBy = productTransfer.CreateBy;

                WarehouseTransferProductDO transferProductInfo = transferProduct as WarehouseTransferProductDO;
                //需要生成新批次

                int actualNum = productTransfer.ActualNum;

                //ShopDTO shopInfo = shopDTO as ShopDTO;
                var status = productTransfer.IsDamage ? StockInOutStatusEnum.部分收货.ToString() :
                                    StockInOutStatusEnum.已收货.ToString();

                var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                {
                    ActualNum = actualNum,
                    IsDeleted = 0,
                    BadNum = 0,
                    Cost = transferProductInfo.Cost,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    GoodNum = actualNum,
                    InoutId = productTransfer.InOutId,
                    Num = actualNum,
                    ProductId = transferProductInfo.ProductId.Trim(),
                    ProductName = transferProductInfo.ProductName,
                    ReleationId = transferProductInfo.Id,  //其他入库  无关联,
                    Status = status, //判断有误差异数据
                    SupplierId = transferProductInfo.VenderId, //其他入库  无关联,
                    SupplierName = transferProductInfo.VenderShortName,
                    UomText = "个"
                });

                var batchResult = await inventoryBatchRepository.InsertAsync(new InventoryBatchDO
                {
                    ShopId = shopInfo.ShopId,
                    SourceInventoryId = transferProductInfo.Id,
                    ProductId = transferProductInfo.ProductId,
                    ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                    ProductName = transferProductInfo.ProductName,
                    IsDeleted = 0,
                    Amount = actualNum * transferProductInfo.Cost,
                    Cost = transferProductInfo.Cost,
                    SupplierId = transferProductInfo.VenderId.ToString(),
                    SupplierName = transferProductInfo.VenderShortName,
                    TotalNum = actualNum,
                    CanUseNum = actualNum,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    OwnId = -1,
                    OwnType = OwnerType.Company.ToString(),
                    RefBatchNo = transferProductInfo.BatchId.ToString()  //关联仓库的批次！
                });

                await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                {
                    ShopId = shopInfo.ShopId,
                    ShopName = shopInfo.SimpleName,
                    SourceId = transferProductInfo.TransferId.ToString(),
                    IsDeleted = 0,
                    Cost = transferProductInfo.Cost,
                    BatchNo = batchResult,
                    SourceInventoryNo = transferProductInfo.Id.ToString(),
                    OperationType = StockOperateTypeEnum.入库.ToString(),
                    SourceType = StockInTypeEnum.铺货入库.ToString(),
                    ProductId = transferProductInfo.ProductId,
                    ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                    ProductName = transferProductInfo.ProductName,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    BeforeTotalNum = 0,
                    AfterTotalNum = actualNum,
                    BeforeCanUseNum = 0,
                    AfterCanUseNum = actualNum,
                    Amount = actualNum * transferProductInfo.Cost,
                    CurrentCanUseNum = actualNum,
                    SupplierId = transferProductInfo.VenderId.ToString(),
                    SupplierName = transferProductInfo.VenderShortName,
                    OwnId = -1,
                    OwnType = OwnerType.Company.ToString()
                });


                var stockParams = new DynamicParameters();
                stockParams.Add("@location_id", shopInfo.ShopId);
                stockParams.Add("@product_id", transferProductInfo.ProductId);
                var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                var inventoryData = inventoryResult?.FirstOrDefault();

                //4.生成库存流水记录
                await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                {
                    SourceNo = transferProductInfo.TransferId.ToString(),
                    LocationId = shopInfo.ShopId,
                    LocationName = shopInfo.SimpleName,
                    IsDeleted = 0,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    ProductId = transferProductInfo.ProductId,
                    ProductName = transferProductInfo.ProductName,
                    BatchNo = batchResult.ToString(),
                    BusinessCategory = 1,
                    BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                    AfterTotalNum = (inventoryData?.TotalNum ?? 0) + actualNum,
                    ChangeTotalNum = actualNum,
                    BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                    AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) + actualNum,
                    ChangeCanUseNum = actualNum
                });
                //5.维护库存汇总表
                if (inventoryData != null)
                {
                    //更新表记录
                    await _stockManageRepository.UpdateInventoryData(new InventoryDO
                    {
                        TotalNum = inventoryData.TotalNum + actualNum,
                        TotalCost = inventoryData.TotalCost + actualNum * transferProductInfo.Cost,
                        CanUseNum = inventoryData.CanUseNum + actualNum,
                        LocationId = shopInfo.ShopId,
                        ProductId = transferProductInfo.ProductId,
                        Id = inventoryData.Id,
                        UpdateBy = createBy
                    });
                }
                else
                {
                    //新增记录
                    await _stockManageRepository.InsertAsync(new InventoryDO
                    {
                        LocationId = shopInfo.ShopId,
                        LocationName = shopInfo.SimpleName,
                        ProductId = transferProductInfo.ProductId,
                        ProductName = transferProductInfo.ProductName,
                        SourceType = OwnerType.Company.ToString(),
                        TotalCost = actualNum * transferProductInfo.Cost,
                        TotalNum = actualNum,
                        CanUseNum = actualNum,
                        IsDeleted = 0,
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    });
                }

                //回写批次
                await stockInoutItemRepository.UpdateStockInoutBatch(new StockInoutItemDO
                {
                    Id = relationId,
                    BatchNo = batchResult.ToString(),
                    UpdateType = 1
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"TransferSyncInventoryData_Error productInfo:{JsonConvert.SerializeObject(transferProduct)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<ProductStockDTO>>> GetProductStocks(List<ProductStockDTO> request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var orgType = identityService.GetOrgType();
                if (orgType == "1")
                {
                    var productStocks = new List<ProductStockDTO>();
                    var shopId = Convert.ToInt64(identityService.GetOrganizationId());

                    //只占用铺货的库存
                    var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id IN @product_ids and is_deleted=0 and own_type='Company'",
                        new
                        {
                            shop_Id = shopId,
                            product_ids = request.Select(r => r.ProductId)
                        });

                    if (inventoryBatchs.Any())
                    {
                        foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                        {
                            var batchNos = item.ToList().Select(r => r.Id);
                            //查询批次占用
                            var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key, location_id = shopId });
                            var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                            productStocks.Add(new ProductStockDTO
                            {
                                CategoryName = request.FirstOrDefault(r => r.ProductId == item.Key)?.CategoryName,
                                ProductId = item.Key,
                                ProductName = request.FirstOrDefault(r => r.ProductId == item.Key)?.ProductName,
                                StockNum = item.Sum(r => r.CanUseNum) - sumUnAvailabelNum
                            });
                        }

                        res.Data = productStocks;
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 获取门店铺货产品库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockResponse>>> GetShopProductStocks(GetShopProductStocksRequest request)
        {
            var res = new ApiResult<List<ProductStockResponse>>();
            try
            {
                var productStocks = new List<ProductStockResponse>();
                //只占用铺货的库存
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id IN @product_ids and is_deleted=0",
                    new
                    {
                        shop_Id = request.ShopId,
                        product_ids = request.Pids
                    });

                if (inventoryBatchs.Any())
                {
                    foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                    {
                        var batchNos = item.ToList().Select(r => r.Id);
                        //查询批次占用
                        var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key, location_id = request.ShopId });
                        //  var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);

                        foreach (var batchItem in item.ToList())
                        {
                            //每个产品的批次的占用量
                            var sumUnAvailabelNum = inventoryFlowItems.Where(r => r.BatchNo == batchItem.Id.ToString()).Sum(r => r.AfterOccupyNum) +
                                inventoryFlowItems.Where(r => r.BatchNo == batchItem.Id.ToString()).Sum(r => r.AfterLockNum);

                            productStocks.Add(new ProductStockResponse
                            {
                                AvailableNum = Convert.ToInt32(batchItem.CanUseNum - sumUnAvailabelNum),
                                //铺货到门店的产品库存 /门店 盘盈平台产品 生成的库存
                                BatchId = !string.IsNullOrWhiteSpace(batchItem.RefBatchNo) ? Convert.ToInt64(batchItem.RefBatchNo) : batchItem.Id,
                                RefBatchId = batchItem.Id,
                                CostPrice = batchItem.Cost,
                                Location = string.Empty,
                                LocationId = batchItem.ShopId,
                                OwnerId = Convert.ToInt32(batchItem.OwnId),
                                OwnerName = batchItem.OwnType,
                                ProductId = batchItem.ProductId,
                                ProductName = batchItem.ProductName,
                                StockType = batchItem.ProductAttrType == "良品" ? Convert.ToSByte(1) : Convert.ToSByte(2),
                                StockUnit = string.Empty
                            });
                        }
                    }
                    res.Data = productStocks;
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 出库任务发出
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> StockInOutTaskDelivery(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var stockInout = await stockInoutRepository.GetAsync(request.StockId);

                //Check 每一个Sku的库存可用量
                var inOutProducts = await stockInoutItemRepository.GetListAsync(" where inout_id=@inoutId and is_deleted=0", new { inoutId = request.StockId });
                if (inOutProducts != null && inOutProducts.Any())
                {
                    bool isEnough = true;
                    string msg = string.Empty;
                    //判断库存是否充足
                    foreach (var item in inOutProducts)
                    {
                        var productStockRes = await GetShopProductStocks(new GetShopProductStocksRequest
                        {
                            ShopId = stockInout.LocationId,
                            Pids = new List<string> { item.ProductId }
                        });
                        var productStocks = productStockRes.Data;

                        if (productStocks != null && productStocks.Any())
                        {
                            //判断可用库存是否充足                              
                            var targetStock = productStocks.FirstOrDefault(r => r.RefBatchId == Convert.ToInt64(item.BatchNo));
                            if (targetStock != null && targetStock.AvailableNum - item.Num >= 0)
                            {
                                continue;
                            }
                            else
                            {
                                //系统需要给出提示
                                isEnough = false;
                                msg = $"产品【{item.ProductName}】的调拨数量【{item.Num}】,可用库存【{targetStock?.AvailableNum}】！";
                                break;
                            }
                        }
                    }

                    if (!isEnough)
                    {
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Success,
                            Message = msg
                        };
                    }

                    //1.更改出库任务的状态

                    await stockInoutRepository.UpdateStockInoutStatus(new StockInoutDO
                    {
                        Id = stockInout.Id,
                        UpdateBy = identityService.GetUserName(),
                        Status = StockInOutStatusEnum.已出库.ToString()
                    });

                    //2.扣减库存  需要生成相关的记录  直接把该批次的库存扣减掉

                    //3.生成批次流水记录

                    foreach (var item in inOutProducts)
                    {
                        var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                        //出掉批次的库存
                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                        {
                            Id = batchInfo.Id,
                            UpdateBy = identityService.GetUserName(),
                            CanUseNum = batchInfo.CanUseNum - item.Num
                            // TotalNum = batchInfo.TotalNum - item.Num
                        });

                        //需要记录数据
                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                        {
                            ShopId = stockInout.LocationId,
                            ShopName = stockInout.LocationName,
                            SourceId = request.SourceInventoryNo,
                            IsDeleted = 0,
                            Cost = item.Cost,
                            BatchNo = Convert.ToInt64(item.BatchNo),
                            SourceInventoryNo = item.ReleationId.ToString(),
                            OperationType = StockOperateTypeEnum.出库.ToString(),
                            SourceType = stockInout.SourceType,
                            ProductId = item.ProductId,
                            ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                            ProductName = item.ProductName,
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            BeforeTotalNum = 0,
                            AfterTotalNum = item.GoodNum,
                            BeforeCanUseNum = 0,
                            AfterCanUseNum = item.GoodNum,
                            Amount = item.GoodNum * item.Cost,
                            CurrentCanUseNum = item.GoodNum,
                            SupplierId = batchInfo.SupplierId,
                            SupplierName = batchInfo.SupplierName,
                            OwnId = batchInfo.OwnId,
                            OwnType = batchInfo.OwnType
                        });

                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", stockInout.LocationId);
                        stockParams.Add("@product_id", item.ProductId);
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                        var inventoryData = inventoryResult?.FirstOrDefault();

                        //4.生成库存流水记录
                        await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                        {
                            SourceNo = stockInout.SourceInventoryNo,
                            LocationId = stockInout.LocationId,
                            LocationName = stockInout.LocationName,
                            IsDeleted = 0,
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            BatchNo = item.BatchNo,
                            BusinessCategory = 1,
                            BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                            AfterTotalNum = (inventoryData?.TotalNum ?? 0) - item.GoodNum,
                            ChangeTotalNum = item.GoodNum,
                            BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                            AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) - item.GoodNum,
                            ChangeCanUseNum = item.GoodNum
                        });
                        //5.维护库存汇总表
                        if (inventoryData != null)
                        {
                            //更新表记录
                            await _stockManageRepository.UpdateInventoryData(new InventoryDO
                            {
                                TotalNum = inventoryData.TotalNum - item.GoodNum,
                                TotalCost = inventoryData.TotalCost - (item.GoodNum * item.Cost),
                                CanUseNum = inventoryData.CanUseNum - item.GoodNum,
                                LocationId = stockInout.LocationId,
                                ProductId = item.ProductId,
                                Id = inventoryData.Id,
                                UpdateBy = identityService.GetUserName()
                            });
                        }
                    }

                    //3.回写调拨任务
                    await wmsClient.UpdateAllotTaskStatus(new AllotTaskRequest
                    {
                        TaskNo = stockInout.SourceInventoryNo,
                        TaskStatus = "全部出库",
                        UpdateBy = identityService.GetUserName(),

                    });

                    var stockLocationRequest = new BatchCreateStockLocationClientRequest();

                    //查询调拨单信息
                    var orginTaskInfoRes = await _allotTaskRepository.GetListAsync(" where task_no=@task_no", new { task_no = stockInout.SourceInventoryNo });
                    var orginTaskInfo = orginTaskInfoRes.FirstOrDefault();

                    //如果调拨单 目标仓 是仓库
                    if (orginTaskInfo.TargetType == "Warehouse")
                    {
                        #region 创建大仓的库存
                        //直接把该批次的库存将到原库存中
                        foreach (var item in inOutProducts)
                        {
                            var inventoryBatch = await inventoryBatchRepository.GetAsync(item.BatchNo);
                            if (inventoryBatch != null)
                            {
                                var orginBatchInfo = await _batchRepository.GetAsync(inventoryBatch.RefBatchNo);
                                stockLocationRequest.StockLocations.Add(new StockLocationClientDTO
                                {
                                    LocationId = orginTaskInfo.TargetWarehouse,
                                    Location = orginTaskInfo.TargetWarehouseName,
                                    LocationType = 1,
                                    ProductId = orginBatchInfo.ProductId,
                                    ProductName = orginBatchInfo.ProductName,
                                    Num = item.GoodNum,
                                    BatchId = orginBatchInfo.Id,
                                    CostPrice = orginBatchInfo.Cost,
                                    TotalCost = orginBatchInfo.Cost * item.GoodNum,
                                    WeekYear = orginBatchInfo.WeekYear,
                                    OwnerId = orginBatchInfo.OwnerId,
                                    StockType = 1,
                                    CreateBy = identityService.GetUserName(),
                                    CreateTime = DateTime.Now,
                                    Remark = $"门店【{stockInout.LocationName}】->仓库【{orginTaskInfo.TargetWarehouseName}】新增的库存【{item.Num}】"
                                });
                            }
                        }

                        var stockLocationRes = await wmsClient.BatchCreateStockLocation(stockLocationRequest);
                        #endregion

                        #region 创建门店->仓库的库存扭转
                        var createStockTranctionRequest = new BatchCreateStockTranctionClientRequest();

                        if (stockLocationRes.Code == ResultCode.Success
                            && stockLocationRes.Data != null)
                        {
                            var createStockLocationResponse = stockLocationRes.Data.StockLocations;
                            foreach (var item in createStockLocationResponse)
                            {
                                createStockTranctionRequest.StockTranctions.Add(new StockTranctionClientDTO
                                {
                                    TransferObjectNo = orginTaskInfo.TaskNo,
                                    TransferType = "调拨单",
                                    TransferFrom = orginTaskInfo.SourceWarehouse,
                                    TransferFromName = orginTaskInfo.SourceWarehouseName,
                                    TransferTo = orginTaskInfo.TargetWarehouse,
                                    TransferToName = orginTaskInfo.TargetWarehouseName,
                                    FromStockId = -1000,
                                    ToStockId = item.Id,
                                    Num = item.Num,
                                    ProductId = item.ProductId,
                                    ProductName = item.ProductName,
                                    TransferOperator = identityService.GetUserName(),
                                    TransferTime = DateTime.Now,
                                    CreateBy = identityService.GetUserName(),
                                    CreateTime = DateTime.Now
                                });
                            }
                        }
                        //新建仓库->在途的流转记录
                        await wmsClient.BatchCreateStockTranction(createStockTranctionRequest);
                        #endregion

                        await _allotTaskRepository.CreateWmsLog(new WmsLogDO
                        {
                            LogLevel = LogLevelEnum.Info.ToString(),
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            ObjectId = orginTaskInfo.Id,
                            ObjectType = "AllotTask",
                            Remark = $"调拨任务【{orginTaskInfo.TaskNo}】的产品库存从源仓【{orginTaskInfo.SourceWarehouseName}】扣减，追加到目标仓【{orginTaskInfo.TargetWarehouseName}】"
                        });
                    }
                    //如果调拨单 目标仓 是门店
                    else if (orginTaskInfo.TargetType == "Shop")
                    {
                        var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                        {
                            CreateBy = identityService.GetUserName(),
                            IsDeleted = 0,
                            CreateTime = DateTime.Now,
                            LocationId = orginTaskInfo.TargetWarehouse,
                            LocationName = orginTaskInfo.TargetWarehouseName, //TODO
                            OperateTime = DateTime.Now,
                            OperationType = StockOperateTypeEnum.入库.ToString(),
                            Operator = string.Empty,
                            SourceInventoryNo = request.StockId.ToString(), //如果是空的 需要手动生成
                            SourceType = StockInTypeEnum.调拨入库.ToString(),
                            Status = StockInOutStatusEnum.已收货.ToString()  //判断有无差异数目
                        });

                        foreach (var item in inOutProducts)
                        {
                            //生成入库任务
                            var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                            {
                                ActualNum = item.Num,
                                IsDeleted = 0,
                                BadNum = 0,
                                Cost = item.Cost,
                                CreateBy = identityService.GetUserName(),
                                CreateTime = DateTime.Now,
                                GoodNum = item.Num,
                                InoutId = stockId,
                                Num = item.Num,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName,
                                ReleationId = item.Id,  //其他入库  无关联,
                                Status = StockInOutStatusEnum.已收货.ToString(), //判断有误差异数据
                                SupplierId = item.SupplierId, //其他入库  无关联,
                                SupplierName = item.SupplierName,
                                UomText = "个"
                            });


                            var orginBatchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);
                            var batchResult = await inventoryBatchRepository.InsertAsync(new InventoryBatchDO
                            {
                                ShopId = orginTaskInfo.TargetWarehouse,
                                SourceInventoryId = stockId,
                                ProductId = item.ProductId,
                                ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                                ProductName = item.ProductName,
                                IsDeleted = 0,
                                Amount = item.Num * item.Cost,
                                Cost = item.Cost,
                                SupplierId = item.SupplierId.ToString(),
                                SupplierName = item.SupplierName,
                                TotalNum = item.Num,
                                CanUseNum = item.Num,
                                CreateBy = identityService.GetUserName(),
                                CreateTime = DateTime.Now,
                                OwnId = -1,
                                OwnType = OwnerType.Company.ToString(),
                                RefBatchNo = orginBatchInfo.RefBatchNo  //关联仓库的批次！
                            });

                            //加到新的记录中
                            await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                            {
                                ShopId = orginTaskInfo.TargetWarehouse,
                                ShopName = orginTaskInfo.TargetWarehouseName,
                                SourceId = stockId.ToString(),
                                IsDeleted = 0,
                                Cost = item.Cost,
                                BatchNo = batchResult,
                                SourceInventoryNo = item.Id.ToString(),
                                OperationType = StockOperateTypeEnum.入库.ToString(),
                                SourceType = StockInTypeEnum.调拨入库.ToString(),
                                ProductId = item.ProductId,
                                ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                                ProductName = item.ProductName,
                                CreateBy = identityService.GetUserName(),
                                CreateTime = DateTime.Now,
                                BeforeTotalNum = 0,
                                AfterTotalNum = item.Num,
                                BeforeCanUseNum = 0,
                                AfterCanUseNum = item.Num,
                                Amount = item.Num * item.Cost,
                                CurrentCanUseNum = item.Num,
                                SupplierId = item.SupplierId.ToString(),
                                SupplierName = item.SupplierName,
                                OwnId = -1,
                                OwnType = OwnerType.Company.ToString()
                            });

                            var stockParams = new DynamicParameters();
                            stockParams.Add("@location_id", orginTaskInfo.TargetWarehouse);
                            stockParams.Add("@product_id", item.ProductId);
                            var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                            var inventoryData = inventoryResult?.FirstOrDefault();

                            //4.生成库存流水记录
                            await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                            {
                                SourceNo = stockId.ToString(),
                                LocationId = orginTaskInfo.TargetWarehouse,
                                LocationName = orginTaskInfo.TargetWarehouseName,
                                IsDeleted = 0,
                                CreateBy = identityService.GetUserName(),
                                CreateTime = DateTime.Now,
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                BatchNo = batchResult.ToString(),
                                BusinessCategory = 1,
                                BeforeTotalNum = inventoryData?.TotalNum ?? 0,
                                AfterTotalNum = (inventoryData?.TotalNum ?? 0) + item.Num,
                                ChangeTotalNum = item.Num,
                                BeforeCanUseNum = (inventoryData?.CanUseNum ?? 0),
                                AfterCanUseNum = (inventoryData?.CanUseNum ?? 0) + item.Num,
                                ChangeCanUseNum = item.Num
                            });
                            //5.维护库存汇总表
                            if (inventoryData != null)
                            {
                                //更新表记录
                                await _stockManageRepository.UpdateInventoryData(new InventoryDO
                                {
                                    TotalNum = inventoryData.TotalNum + item.Num,
                                    TotalCost = inventoryData.TotalCost + item.Num * item.Cost,
                                    CanUseNum = inventoryData.CanUseNum + item.Num,
                                    LocationId = orginTaskInfo.TargetWarehouse,
                                    ProductId = item.ProductId,
                                    Id = inventoryData.Id,
                                    UpdateBy = identityService.GetUserName()
                                });
                            }
                            else
                            {
                                //新增记录
                                await _stockManageRepository.InsertAsync(new InventoryDO
                                {
                                    LocationId = orginTaskInfo.TargetWarehouse,
                                    LocationName = orginTaskInfo.TargetWarehouseName,
                                    ProductId = item.ProductId,
                                    ProductName = item.ProductName,
                                    SourceType = OwnerType.Company.ToString(),
                                    TotalCost = item.Num * item.Cost,
                                    TotalNum = item.Num,
                                    CanUseNum = item.Num,
                                    IsDeleted = 0,
                                    CreateBy = identityService.GetUserName(),
                                    CreateTime = DateTime.Now
                                });
                            }

                            //回写批次
                            await stockInoutItemRepository.UpdateStockInoutBatch(new StockInoutItemDO
                            {
                                Id = relationId,
                                BatchNo = batchResult.ToString(),
                                UpdateType = 1
                            });
                        }

                        await _allotTaskRepository.CreateWmsLog(new WmsLogDO
                        {
                            LogLevel = LogLevelEnum.Info.ToString(),
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            ObjectId = orginTaskInfo.Id,
                            ObjectType = "AllotTask",
                            Remark = $"调拨任务【{orginTaskInfo.TaskNo}】的产品库存从源仓【{orginTaskInfo.SourceWarehouseName}】扣减，追加到目标仓【{orginTaskInfo.TargetWarehouseName}】"
                        });

                    }
                    res.Code = ResultCode.Success;
                    res.Message = "success";
                }
                else
                {
                    res.Code = ResultCode.Exception;
                    res.Message = $"出入库产品为空!";
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"StockInOutTaskDelivery_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> CancelStockInoutTask(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var stockInoutTask = await stockInoutRepository.GetAsync(request.StockId);
                //取消出库任务
                await stockInoutRepository.UpdateStockInoutStatus(new StockInoutDO
                {
                    Id = request.StockId,
                    UpdateBy = identityService.GetUserName(),
                    Status = "已取消"
                });


                await wmsClient.UpdateAllotTaskStatus(new AllotTaskRequest
                {
                    TaskNo = stockInoutTask.SourceInventoryNo,
                    UpdateBy = identityService.GetUserName(),
                    TaskStatus = "已取消"
                });
                //取消调拨任务
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CancelStockInoutTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<ProductStockDTO>>> GetProductStocksForOut(List<ProductStockDTO> request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var orgType = identityService.GetOrgType();
                if (orgType == "1")
                {
                    var productStocks = new List<ProductStockDTO>();
                    var shopId = Convert.ToInt64(identityService.GetOrganizationId());

                    //查询外采商品的库存
                    var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id IN @product_ids and is_deleted=0 and own_id=@own_id",
                        new
                        {
                            shop_Id = shopId,
                            product_ids = request.Select(r => r.ProductId),
                            own_id = shopId
                        });

                    if (inventoryBatchs.Any())
                    {
                        foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                        {
                            var batchNos = item.ToList().Select(r => r.Id);
                            //查询批次占用
                            var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", 
                                new { batchNos = batchNos, product_id = item.Key, location_id = shopId });
                            var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                            productStocks.Add(new ProductStockDTO
                            {
                                CategoryName = request.FirstOrDefault(r => r.ProductId == item.Key)?.CategoryName,
                                ProductId = item.Key,
                                ProductName = request.FirstOrDefault(r => r.ProductId == item.Key)?.ProductName,
                                CostPrice = item.Where(x => x.CanUseNum > 0)?.Select(x => x.Cost).Max()??0,                                
                                //FirstOrDefault(r => r.CanUseNum > 0,).Cost,
                                StockNum = item.Sum(r => r.CanUseNum) - sumUnAvailabelNum
                            });
                        }

                        res.Data = productStocks;
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetProductStocksForOut_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;

        }

        /// <summary>
        /// 回写技师外采领料记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> OrderInstallNotifyForStockOut(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //要回写领取任务表的安装值
                var orderInfo = await _orderClient.QueryUseStockOrderDetail(new QueryUseStockOrderDetailClientRequest
                {
                    OrderNo = request.SourceInventoryNo
                });
                if (orderInfo.Code == ResultCode.Success
                    && orderInfo.Data != null)
                {
                    var shopType = 0;

                    var shopId = orderInfo.Data.ShopId;

                    var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                    {
                        ShopId = shopId
                    });
                    if (shopResult != null && shopResult.Code == ResultCode.Success)
                    {
                        shopType = shopResult.Data.Type;
                    }

                    if (shopType != 4)
                    {
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Success,
                            Message = "订单安装门店类型不是上门养车!"
                        };
                    }

                    #region 获取安装订单的产品和数量

                    var realProducts = new List<InstallOrderProdcutDTO>();

                    foreach (var r in request.StockItems)
                    {
                        realProducts.Add(new InstallOrderProdcutDTO
                        {
                            Num = r.Num,
                            OrderNo = request.SourceInventoryNo,
                            ProductId = r.ProductId,
                            ProductName = r.ProductName
                        });
                    };

                    #endregion

                    //获取订单派工的技师
                    var orderDispatchRes = await _orderDispatchRepository.GetListAsync(" where order_no=@order_no and is_deleted=0", new { order_no = request.SourceInventoryNo });

                    var orderDispatchInfo = orderDispatchRes.FirstOrDefault();

                    if (orderDispatchInfo != null)
                    {
                        var statusList = new List<string> { HomeCareStatusEnum.未完结.ToString() };

                        //获取技师安装的产品数据
                        var homeProducts = await _homeCareProductRepository.GetHomeCareProductsByTechId(new HomeCareRecordDO
                        {
                            TechId = orderDispatchInfo.TechId,
                            ShopId = orderDispatchInfo.ShopId,
                            StatusList = statusList
                        });

                        if (!homeProducts.Any())
                        {
                            return new ApiResult<string> { Code = ResultCode.Exception, Message = $"【{orderDispatchInfo.TechName}】暂无养车记录需同步！" };
                        }

                        foreach (var item in realProducts)
                        {
                            var filterProducts = homeProducts?.Where(r => r.ProductId == item.ProductId)?.OrderBy(r => r.CreateTime);
                            if (filterProducts != null && filterProducts.Any())
                            {
                                var orginNum = item.Num;

                                //按照优先级来扣
                                foreach (var filterProduct in filterProducts)
                                {
                                    //TODO:数量待确定
                                    if (filterProduct.ReceiveNum >= orginNum)
                                    {
                                        var status = string.Empty;

                                        if (filterProduct.LessNum > 0 || filterProduct.ExceptionNum > 0)
                                        {
                                            if (filterProduct.ReceiveNum > orginNum)
                                            {
                                                status = HomeCareStatusEnum.未完结.ToString();
                                            }
                                            else
                                            {
                                                status = HomeCareStatusEnum.异常完结.ToString();
                                            }
                                        }
                                        else
                                        {
                                            if (filterProduct.ReceiveNum > orginNum)
                                            {
                                                status = HomeCareStatusEnum.未完结.ToString();
                                            }
                                            else
                                            {
                                                status = HomeCareStatusEnum.正常完结.ToString();
                                            }
                                        }
                                        await _homeCareProductRepository.UpdateProductInstallNum(new HomeCareProductDO
                                        {
                                            Id = filterProduct.Id,
                                            InstallNum = orginNum,
                                            UpdateBy = request.CreateBy,
                                            ReceiveNum = orginNum,
                                            Status = status,
                                            Remark = $" 更新安装数量:{orginNum} "
                                        });

                                        await SyncHomeCareRecordStatus(filterProduct.RecordId, request.CreateBy);

                                        await _homeCareOrderRepository.InsertAsync(new HomeCareOrderDO
                                        {
                                            CreateBy = request.CreateBy,
                                            IsDeleted = 0,
                                            CreateTime = DateTime.Now,
                                            HomeProductId = filterProduct.Id,
                                            Num = orginNum,
                                            OrderNo = item.OrderNo,
                                            ProductId = filterProduct.ProductId,
                                            ProductName = filterProduct.ProductName,
                                            Remark = $"【{item.OrderNo}】的产品【{filterProduct.ProductId}】安装数量【{orginNum}】"
                                        });
                                        break;
                                    }
                                    else
                                    {
                                        var status = string.Empty;
                                        if (filterProduct.LessNum > 0 || filterProduct.ExceptionNum > 0)
                                        {
                                            status = HomeCareStatusEnum.异常完结.ToString();
                                        }
                                        else
                                        {
                                            status = HomeCareStatusEnum.正常完结.ToString();
                                        }

                                        await _homeCareProductRepository.UpdateProductInstallNum(new HomeCareProductDO
                                        {
                                            Id = filterProduct.Id,
                                            InstallNum = filterProduct.ReceiveNum,
                                            ReceiveNum = filterProduct.ReceiveNum,
                                            UpdateBy = request.CreateBy,
                                            Status = status,
                                            Remark = $" 更新安装数量:{filterProduct.ReceiveNum} "
                                        });

                                        await SyncHomeCareRecordStatus(filterProduct.RecordId, request.CreateBy);

                                        orginNum = orginNum - filterProduct.ReceiveNum;

                                        await _homeCareOrderRepository.InsertAsync(new HomeCareOrderDO
                                        {
                                            CreateBy = request.CreateBy,
                                            IsDeleted = 0,
                                            CreateTime = DateTime.Now,
                                            HomeProductId = filterProduct.Id,
                                            Num = filterProduct.ReceiveNum,
                                            OrderNo = item.OrderNo,
                                            ProductId = filterProduct.ProductId,
                                            ProductName = filterProduct.ProductName,
                                            Remark = $"【{item.OrderNo}】的产品【{filterProduct.ProductId}】安装数量【{(filterProduct.ReceiveNum)}】"
                                        });
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Success,
                            Message = "获取订单派工技师为空!"
                        };
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"OrderInstallNotify_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 同步领料状态
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SyncHomeCareRecordStatus(long recordId, string updateBy)
        {
            var res = new ApiResult<string>();
            try
            {
                var products = await _homeCareProductRepository.GetListAsync(" where record_id=@record_id and is_deleted=0", new { record_id = recordId });
                if (!products.Any(r => r.Status == HomeCareStatusEnum.未完结.ToString()))
                {
                    var status = string.Empty;
                    if (products.Any(r => r.Status == HomeCareStatusEnum.异常完结.ToString()))
                    {
                        status = HomeCareStatusEnum.异常完结.ToString();
                    }
                    else
                    {
                        status = HomeCareStatusEnum.正常完结.ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        await _homeCareRecordRepository.UpdateHomeCareRecordStatus(new HomeCareRecordDO
                        {
                            Id = recordId,
                            Status = status,
                            UpdateBy = updateBy
                        });
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SyncHomeCareRecordStatus_Error Data:", ex);
            }
            return res;
        }

        /// <summary>
        /// 订单详情查询库存接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetTransferProductStocks(GetShopProductStocksRequest request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var productStocks = new List<ProductStockDTO>();
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id IN @product_ids and is_deleted=0",
                    new
                    {
                        shop_Id = request.ShopId,
                        product_ids = request.Pids
                    });

                if (inventoryBatchs.Any())
                {
                    foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                    {
                        var batchNos = item.ToList().Select(r => r.Id);
                        //查询批次占用
                        var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key, location_id = request.ShopId });
                        var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                        productStocks.Add(new ProductStockDTO
                        {
                            ProductId = item.Key,
                            StockNum = item.Sum(r => r.CanUseNum) - sumUnAvailabelNum
                        });
                    }
                    res.Data = productStocks;
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;

        }

        /// <summary>
        /// 返回门店铺货sku库存
        /// StockNum:库存总量  Num：可用库存
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetAllProductStocks()
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var shop = await GetShopById();
                //只查询铺货的库存
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and is_deleted=0 and own_type='Company'",
                    new
                    {
                        shop_Id = shop.ShopId
                    });

                var productStocks = new List<ProductStockDTO>();

                if (inventoryBatchs.Any())
                {
                    foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                    {
                        var batchNos = item.ToList().Select(r => r.Id);
                        //查询批次占用
                        var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key, location_id = shop.ShopId });
                        var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                        productStocks.Add(new ProductStockDTO
                        {
                            ProductId = item.Key,
                            StockNum = item.Sum(r => r.CanUseNum),
                            ProductName = item.First().ProductName,
                            Num = Convert.ToInt32(item.Sum(r => r.CanUseNum) - sumUnAvailabelNum)
                        });
                    }
                    res.Data = productStocks;
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAllProductStocks_Error", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 查看库存批次流水记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<InventoryBatchDTO>>> GetInventoryBatchs(GetShopStockRequest request)
        {
            var res = new ApiResult<List<InventoryBatchDTO>>();
            try
            {
                if (request.ShopId <= 0)
                {
                    var shopInfo = await GetShopById();
                    request.ShopId = shopInfo.ShopId;
                }

                var param = new DynamicParameters();
                param.Add("@shop_Id", request.ShopId);
                param.Add("@product_id", request.ProductId);

                var inventoryBatchRes = await inventoryBatchRepository.GetListAsync(" where shop_Id=@shop_Id and product_id=@product_id and is_deleted =0", param);
                //获取每个批次的锁定库存
                if (inventoryBatchRes.Any())
                {
                    var inventoryBatchs = _mapper.Map<List<InventoryBatchDTO>>(inventoryBatchRes);
                    var batchNos = inventoryBatchs.Select(r => r.Id);

                    var param1 = new DynamicParameters();
                    param1.Add("@shop_Id", request.ShopId);
                    param1.Add("@product_id", request.ProductId);
                    param1.Add("@batchNos", batchNos);
                    var inventoryBatchFlowRes = await inventoryFlowItemRepository.GetListAsync("where location_id=@shop_id and product_id=@product_id and batch_no in @batchNos and is_deleted=0", param1);

                    foreach (var item in inventoryBatchs)
                    {
                        var inventoryBatchFlows = inventoryBatchFlowRes?.Where(r => r.BatchNo == item.Id.ToString());
                        if (inventoryBatchFlows != null && inventoryBatchFlows.Any())
                        {
                            long sumUnAvailableNum = inventoryBatchFlows.Sum(r => r.AfterOccupyNum) + inventoryBatchFlows.Sum(r => r.AfterLockNum);
                            item.AvailableNum = Convert.ToInt32(item.CanUseNum - sumUnAvailableNum);
                            item.OccupyNum = Convert.ToInt32(sumUnAvailableNum);
                        }
                    }
                    res.Data = inventoryBatchs;
                }
                res.Code = ResultCode.Success;

            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<InventoryFlowItemDTO>>> GetInventoryFlowItems(GetShopStockRequest request)
        {
            var res = new ApiResult<List<InventoryFlowItemDTO>>();
            try
            {
                if (request.ShopId <= 0)
                {
                    var shopInfo = await GetShopById();
                    request.ShopId = shopInfo.ShopId;
                }

                var param = new DynamicParameters();
                param.Add("@location_id", request.ShopId);
                param.Add("@product_id", request.ProductId);
                param.Add("@batch_no", request.BatchNo);

                var inventoryFlowItemRes = await inventoryFlowItemRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and batch_no=@batch_no and is_deleted =0", param);
                if (inventoryFlowItemRes != null)
                {
                    var inventoryFlowItems = _mapper.Map<List<InventoryFlowItemDTO>>(inventoryFlowItemRes);
                    inventoryFlowItems = inventoryFlowItems.Where(r => r.ChangeTotalNum > 0 || r.AfterOccupyNum > 0 || r.AfterLockNum > 0).ToList();

                    //获取这个批次的使用记录
                    foreach (var item in inventoryFlowItems)
                    {
                        //库存量比较 
                        if (item.AfterOccupyNum > 0 || item.AfterLockNum > 0)
                        {
                            item.Type = "占用";
                        }
                        else if (item.BeforeTotalNum > item.AfterTotalNum)
                        {
                            item.Type = "扣减";
                        }
                        else
                        {
                            item.Type = "增加";
                        }
                    }

                    return new ApiResult<List<InventoryFlowItemDTO>>
                    {
                        Code = ResultCode.Success,
                        Data = inventoryFlowItems
                    };
                }
                else
                {
                    return new ApiResult<List<InventoryFlowItemDTO>>
                    {
                        Code = ResultCode.Success,
                        Message = "暂无库存流水记录！"
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<BasicInfoDTO>>> GetStockInTypes()
        {
            var result = await EnumHelper.GetBasicInfoDTOs<StockInTypeEnum>();
            return new ApiResult<List<BasicInfoDTO>>
            {
                Code = ResultCode.Success,
                Data = result
            };
        }

        public async Task<ApiResult<List<BasicInfoDTO>>> GetStockOutTypes()
        {
            var result = await EnumHelper.GetBasicInfoDTOs<StockOutTypeEnum>();
            return new ApiResult<List<BasicInfoDTO>>
            {
                Code = ResultCode.Success,
                Data = result
            };
        }

        public async Task<ApiResult<List<ShopWmsLogDTO>>> GetShopWmsLogs(ShopWmsLogDTO request)
        {
            var res = new ApiResult<List<ShopWmsLogDTO>>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@object_id", request.ObjectId);
                param.Add("@object_type", request.ObjectType);

                var result = await _shopWmsLogRepository.GetListAsync(" where object_id =@object_id and object_type=@object_type", param);

                var vo = _mapper.Map<List<ShopWmsLogDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopWmsLogs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 外部使用接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetAvailableStocks(GetProductStocksRequest request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var param = new DynamicParameters();
                var conditions = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(request.ProductId))
                {
                    var productId = request.ProductId;
                    var productNames = request.ProductId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    if (productNames.Any())
                    {
                        string productName = string.Join("%", productNames);
                        conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                        param.Add("@productId", productId);
                    }
                }

                if (request.Times != null && request.Times.Any())
                {
                    conditions.Append(" and create_time>=@StartTime");
                    param.Add("@StartTime", request.Times[0]);

                    conditions.Append(" and create_time<=@EndTime");
                    param.Add("@EndTime", request.Times[1]);
                }
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where is_deleted=0 " + conditions.ToString(), param);

                var productStocks = new List<ProductStockDTO>();

                if (inventoryBatchs.Any())
                {
                    var distinctShops = inventoryBatchs.Select(r => r.ShopId).Distinct();

                    var shopDic = new Dictionary<long, ShopDTO>();
                    foreach (var item in distinctShops)
                    {
                        var shopInfo = await shopRepository.GetAsync(item);
                        shopDic.Add(item, new ShopDTO
                        {
                            SimpleName = shopInfo.SimpleName,
                        });
                    }

                    var batchNos1 = inventoryBatchs.Select(r => r.Id).ToList();

                    var inventoryFlowItems = await inventoryFlowItemRepository.SelectTargetValues(distinctShops.ToList(), batchNos1);

                    int jl = 0;

                    //一个门店+SKU 分组
                    foreach (var item in inventoryBatchs.GroupBy(r => new { r.ProductId, r.ShopId }))
                    {
                        var batchNos = item.ToList().Select(r => r.Id.ToString()).ToList();
                        //查询批次占用
                        // var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and product_id=@product_id and location_id=@location_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key.ProductId, location_id = item.Key.ShopId });

                        IEnumerable<InventoryFlowItemDO> filterFlowItems = null;

                        List<InventoryFlowItemDO> finalFlowItems = new List<InventoryFlowItemDO>();
                        if (batchNos.Count > 1)
                        {
                            filterFlowItems = inventoryFlowItems.Where(r => r.LocationId == item.Key.ShopId &&
                              r.ProductId == item.Key.ProductId);

                            if (filterFlowItems != null && filterFlowItems.Any())
                            {
                                foreach (var filterItem in filterFlowItems)
                                {
                                    if (batchNos.Contains(filterItem.BatchNo))
                                    {
                                        finalFlowItems.Add(filterItem);
                                    }
                                }
                            }

                        }
                        else
                        {
                            finalFlowItems = inventoryFlowItems.Where(r => r.LocationId == item.Key.ShopId &&
                                         r.ProductId == item.Key.ProductId && r.BatchNo == batchNos.First().ToString()).ToList();
                        }
                        if (finalFlowItems != null && finalFlowItems.Any())
                        {
                            //需要安装门店分组
                            var sumUnAvailabelNum = finalFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);

                            productStocks.Add(new ProductStockDTO
                            {
                                ProductId = item.Key.ProductId,
                                LocationName = shopDic[item.Key.ShopId].SimpleName,
                                StockNum = item.Sum(r => r.CanUseNum),
                                ProductName = item.First().ProductName,
                                Num = Convert.ToInt32(item.Sum(r => r.CanUseNum) - sumUnAvailabelNum),
                                LocationId = item.Key.ShopId
                            });
                        }
                    }
                    res.Data = productStocks;
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<ProductStockDTO>>> GetOccupyStocks(GetProductStocksRequest request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var productStocks = new List<ProductStockDTO>();
                var param = new DynamicParameters();
                var conditions = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(request.ProductId))
                {
                    var productId = request.ProductId;
                    var productNames = request.ProductId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    if (productNames.Any())
                    {
                        string productName = string.Join("%", productNames);
                        conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                        param.Add("@productId", productId);
                    }
                }

                if (request.Times != null && request.Times.Any())
                {
                    conditions.Append(" and create_time>=@StartTime");
                    param.Add("@StartTime", request.Times[0]);

                    conditions.Append(" and create_time<=@EndTime");
                    param.Add("@EndTime", request.Times[1]);
                }
                //if (request.LocationId > 0)
                //{
                //    conditions.Append(" and location_id=@location_id");
                //    param.Add("@location_id", request.LocationId);
                //}

                var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and (after_occupy_num>0 or after_lock_num>0) " + conditions.ToString(), param);
                foreach (var item in inventoryFlowItems.GroupBy(r => new { r.ProductId, r.LocationId }))
                {
                    productStocks.Add(new ProductStockDTO
                    {
                        LocationId = item.Key.LocationId,
                        ProductId = item.Key.ProductId,
                        LocationName = item.First().LocationName,
                        ProductName = item.First().ProductName,
                        Num = Convert.ToInt32(item.Sum(r => r.AfterOccupyNum) + item.Sum(r => r.AfterLockNum))
                    });
                }
                res.Data = productStocks;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetOccupyStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStocks(GetSupplierStockRequest request)
        {
            var res = new ApiResult<List<SupplierProductStockDTO>>();
            try
            {
                var productStocks = new List<SupplierProductStockDTO>();
                var organizationId = identityService.GetOrganizationId();
                var companyRes = await _companyRepository.GetAsync(organizationId);
                if (companyRes == null)
                {
                    return new ApiResult<List<SupplierProductStockDTO>>
                    {
                        Code = ResultCode.Exception,
                        Message = "无所属公司!"
                    };
                }
                if (companyRes?.VenderId <= 0)
                {
                    return new ApiResult<List<SupplierProductStockDTO>>
                    {
                        Code = ResultCode.Exception,
                        Message = "未配置关联供应商!"
                    };
                }
                var venderRes = await _purchaseClient.GetVenders(new GetSupplierPurchaseRequest { }); //不需要传递参数

                var vender = venderRes.FirstOrDefault(r => r.Id == companyRes?.VenderId);

                var productStockDic = new Dictionary<string, SupplierProductStockDTO>();

                //获取供应商Id 
                //采购量
                var purchaseProducts = await _purchaseClient.GetSupplierPurchaseProducts(new GetSupplierPurchaseRequest
                {
                    VenderId = vender.Id
                });
                if (purchaseProducts.Code == ResultCode.Success
                    && purchaseProducts.Data != null && purchaseProducts.Data.Any())
                {
                    foreach (var item in purchaseProducts.Data)
                    {
                        if (productStockDic.ContainsKey(item.ProductId.Trim()))
                        {
                            productStockDic[item.ProductId.Trim()].PurchaseNum += item.PurchaseNum;
                        }
                        else
                        {
                            productStockDic[item.ProductId.Trim()] = new SupplierProductStockDTO
                            {
                                PurchaseNum = item.PurchaseNum,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName
                            };
                        }
                    }
                }

                //可用量(仓库+门店的汇总)
                var warehouseProductStockRes = await wmsClient.GetSupplierProductStocks(new GetSupplierStockRequest
                {
                    VenderId = vender.Id,
                    VenderShortName = vender.VenderShortName
                });
                var warehouseProductStocks = warehouseProductStockRes?.Data;

                //门店的可用库存
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync("where is_deleted=0 and supplier_id=@supplier_id and  supplier_name=@supplier_name and can_use_num>0", new { supplier_id = vender.Id, supplier_name = vender.VenderShortName });


                ////需要减去占用的库存
                //var distinctBatchIds = inventoryBatchs?.Select(r => r.Id).ToList();
                var shopProductStocks = new List<SupplierProductStockDTO>();

                //var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where batch_no in @batch_nos and is_deleted=0", new { batch_nos = distinctBatchIds });
                //foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                //{
                //    var targetFlowItems = inventoryFlowItems.Where(r => r.ProductId == item.Key);
                //    //可用的库存量
                //    long stockNum = item.Sum(r => r.CanUseNum) - targetFlowItems.Sum(r => r.AfterOccupyNum) - targetFlowItems.Sum(r => r.AfterLockNum);
                //    shopProductStocks.Add(new SupplierProductStockDTO
                //    {
                //        ProductId = item.Key,
                //        ProductName = item.First().ProductName,
                //        StockNum = Convert.ToInt32(stockNum)
                //    });
                //}

                foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId.Trim()))
                {
                    shopProductStocks.Add(new SupplierProductStockDTO
                    {
                        ProductId = item.Key.Trim(),
                        ProductName = item.First().ProductName,
                        StockNum = Convert.ToInt32(item.Sum(r => r.CanUseNum))
                    });
                }

                if (warehouseProductStocks.Any())
                {
                    foreach (var item in warehouseProductStocks)
                    {
                        if (productStockDic.ContainsKey(item.ProductId.Trim()))
                        {
                            productStockDic[item.ProductId.Trim()].StockNum += item.StockNum;
                        }
                        else
                        {
                            productStockDic[item.ProductId.Trim()] = new SupplierProductStockDTO
                            {
                                StockNum = item.StockNum,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName
                            };
                        }
                    }
                }

                if (shopProductStocks.Any())
                {
                    foreach (var item in shopProductStocks)
                    {
                        if (productStockDic.ContainsKey(item.ProductId.Trim()))
                        {
                            productStockDic[item.ProductId.Trim()].StockNum += item.StockNum;
                        }
                        else
                        {
                            productStockDic[item.ProductId.Trim()] = new SupplierProductStockDTO
                            {
                                StockNum = item.StockNum,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName
                            };
                        }
                    }
                }

                //销售量(门店安装的)
                var saleProducts = await stockInoutRepository.GetSupplierSaleProducts(new GetSupplierStockRequest
                {
                    VenderId = vender.Id,
                    VenderShortName = vender.VenderShortName
                });

                if (saleProducts.Any())
                {
                    foreach (var item in saleProducts)
                    {
                        if (productStockDic.ContainsKey(item.ProductId.Trim()))
                        {
                            productStockDic[item.ProductId.Trim()].SaleNum += item.SaleNum;
                        }
                        else
                        {
                            productStockDic[item.ProductId.Trim()] = new SupplierProductStockDTO
                            {
                                SaleNum = item.SaleNum,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName
                            };
                        }
                    }
                }

                foreach (var item in productStockDic.Values)
                {
                    item.OtherNum = item.PurchaseNum - item.SaleNum - item.StockNum;
                    productStocks.Add(item);
                }
                res.Data = productStocks;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetSupplierProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// sku 查询门店的库存+销售量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStockDetails(GetSupplierStockRequest request)
        {
            var res = new ApiResult<List<SupplierProductStockDTO>>();
            try
            {
                var productStocks = new List<SupplierProductStockDTO>();
                request.ProductId = request.ProductId.Trim();
                var organizationId = identityService.GetOrganizationId();
                var companyRes = await _companyRepository.GetAsync(organizationId);
                if (companyRes == null)
                {
                    return new ApiResult<List<SupplierProductStockDTO>>
                    {
                        Code = ResultCode.Exception,
                        Message = "无所属公司!"
                    };
                }
                if (companyRes?.VenderId <= 0)
                {
                    return new ApiResult<List<SupplierProductStockDTO>>
                    {
                        Code = ResultCode.Exception,
                        Message = "未配置关联供应商!"
                    };
                }
                var venderRes = await _purchaseClient.GetVenders(new GetSupplierPurchaseRequest { }); //不需要传递参数

                var vender = venderRes.FirstOrDefault(r => r.Id == companyRes?.VenderId);

                var inventoryBatchs = await inventoryBatchRepository.GetListAsync("where is_deleted=0 and supplier_id=@supplier_id and  supplier_name=@supplier_name and can_use_num>0 and product_id=@product_id", new { supplier_id = vender.Id, supplier_name = vender.VenderShortName, product_id = request.ProductId });

                var productStockDic = new Dictionary<long, SupplierProductStockDTO>();
                if (inventoryBatchs != null && inventoryBatchs.Any())
                {
                    var distinctShops = inventoryBatchs.Select(r => r.ShopId).Distinct();
                    var shopDic = new Dictionary<long, ShopDTO>();
                    foreach (var item in distinctShops)
                    {
                        var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                        {
                            ShopId = item
                        });
                        if (shopResult.IsNotNullSuccess())
                        {
                            shopDic.Add(item, shopResult.Data);
                        }
                    }

                    foreach (var item in inventoryBatchs)
                    {
                        if (productStockDic.ContainsKey(item.ShopId))
                        {
                            productStockDic[item.ShopId].StockNum += Convert.ToInt32(item.CanUseNum);
                        }
                        else
                        {
                            productStockDic[item.ShopId] = new SupplierProductStockDTO
                            {
                                LocationId = item.ShopId,
                                LocationName = shopDic[item.ShopId].SimpleName,
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                StockNum = Convert.ToInt32(item.CanUseNum)
                            };
                        }
                    }
                }

                //销售量(门店安装的)
                var saleProducts = await stockInoutRepository.GetSupplierSaleProducts(new GetSupplierStockRequest
                {
                    VenderId = vender.Id,
                    VenderShortName = vender.VenderShortName,
                    ProductId = request.ProductId
                });
                if (saleProducts.Any())
                {
                    foreach (var item in saleProducts)
                    {
                        if (productStockDic.ContainsKey(item.LocationId))
                        {
                            productStockDic[item.LocationId].SaleNum += item.SaleNum;
                        }
                        else
                        {
                            productStockDic[item.LocationId] = new SupplierProductStockDTO
                            {
                                LocationId = item.LocationId,
                                LocationName = item.LocationName,
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                SaleNum = item.SaleNum
                            };
                        }
                    }
                }

                foreach (var item in productStockDic.Values)
                {
                    productStocks.Add(item);
                }
                res.Data = productStocks;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetSupplierProductStockDetails_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 门店铺货产品盘盈入库！！！
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateInStockTaskForTransfer(StockInOutDTO request)
        {
            var res = new ApiResult<string>();
            var shopInfo = await GetShopById();
            long locationId = shopInfo?.ShopId ?? 7;
            string locationName = shopInfo?.SimpleName ?? "测试门店";
            try
            {
                var productId = request.StockItems.FirstOrDefault().ProductId;
                //用原来Sku上的供应商信息
                var transferDTO = new InventoryTransferDTO();
                decimal cost = 0;
                var inventoryBatchs = await inventoryBatchRepository.GetListAsync("where shop_Id=@shop_Id and product_id=@product_id and is_deleted=0 and supplier_name<>''", new { shop_Id = locationId, product_id = productId });
                if (inventoryBatchs != null && inventoryBatchs.Any())
                {
                    transferDTO.SupplierId = Convert.ToInt64(inventoryBatchs.FirstOrDefault().SupplierId);
                    transferDTO.SupplierName = inventoryBatchs.FirstOrDefault().SupplierName;
                    transferDTO.RefBatchNo = Convert.ToInt64(inventoryBatchs.FirstOrDefault().RefBatchNo);
                    cost = inventoryBatchs.FirstOrDefault().Cost;
                }

                request.CreateBy = this.identityService.GetUserName();
                //查询当前登录用户  所在的门店 创建记录
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = this.identityService.GetUserName(),
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = locationId,
                    LocationName = locationName, //TODO
                    OperateTime = request.OperateTime,
                    OperationType = StockOperateTypeEnum.入库.ToString(),
                    Operator = request.Operator,
                    SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = request.Status,   //判断有无差异数目
                    Remark = request.Remark
                });

                if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                   || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                {
                    await stockInoutRepository.GenerateStockNo(new StockInoutDO
                    {
                        Id = stockId,
                        SourceInventoryNo = stockId.ToString()
                    });
                }

                if (request.StockItems.Any())
                {
                    foreach (var item in request.StockItems)
                    {
                        item.Cost = cost;
                        item.CreateBy = this.identityService.GetUserName();
                        var tempStatus = StockInOutStatusEnum.已收货.ToString();
                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            ActualNum = item.ActualNum,
                            IsDeleted = 0,
                            BadNum = item.BadNum,
                            BatchNo = string.Empty,
                            Cost = item.Cost,
                            CreateBy = this.identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            GoodNum = item.GoodNum,
                            InoutId = stockId,
                            Num = item.Num,
                            ProductId = item.ProductId.Trim(),
                            ProductName = item.ProductName,
                            ReleationId = item.ReleationId,  //其他入库  无关联,
                            Status = tempStatus, //判断有误差异数据
                            SupplierId = transferDTO.SupplierId, //其他入库  无关联,
                            SupplierName = transferDTO.SupplierName,
                            UomText = "个"

                        });

                        //其他入库的入库单自动生成关联单号
                        if (request.SourceType == StockInTypeEnum.其他入库.ToString()
                            || request.SourceType == StockInTypeEnum.盘盈入库.ToString())
                        {
                            await this.stockInoutItemRepository.UpdateStockInoutRelationId(new StockInoutItemDO
                            {
                                Id = relationId,
                                ReleationId = relationId
                            });
                        }

                        transferDTO.LocationId = locationId;
                        transferDTO.LocationName = locationName;
                        transferDTO.StockInOutId = stockId;
                        transferDTO.RelationId = relationId;

                        var syncResult = await SyncInventoryDataInForTransfer(item, request, transferDTO);

                        if (syncResult.Code == ResultCode.Success)
                        {
                            //回写入库产品批次
                            await stockInoutItemRepository.UpdateStockInoutBatch(new StockInoutItemDO
                            {
                                Id = relationId,
                                BatchNo = syncResult.Data,
                                UpdateType = 1
                            });

                        }
                        else
                        {
                            //清除入库产品记录
                            await stockInoutItemRepository.DeleteStockInoutItem(new StockInoutItemDO { Id = relationId });

                            await stockInoutRepository.DeleteStockInout(new StockInoutDO { Id = stockId });

                            res.Code = ResultCode.Exception;
                            res.Message = "入库发生异常!";
                            return res;
                        }
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateInStockTaskForTransfer_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 门店产品库存查询（只查非0库存）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopOutPurchaseStocks(GetBatchProductStockRequest request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            res.Data = new List<ProductStockDTO>();
            try
            {
                //库存汇总表,只查非0
                var stockParams = new DynamicParameters();
                var condition = " where location_id =@location_id and is_deleted=0 and can_use_num <> 0 ";
                stockParams.Add("@location_id", request.LocationId);
                if (request.ProductIds != null && request.ProductIds.Any())
                {
                    stockParams.Add("@product_ids", request.ProductIds);
                    condition = condition + " and product_id in @product_ids";
                }
                var inventoryResult = await _stockManageRepository.GetListAsync(condition, stockParams);
                if (inventoryResult != null && inventoryResult.Any())
                {
                    foreach (var item in inventoryResult.OrderByDescending(t => t.Id))
                    {
                        res.Data.Add(new ProductStockDTO
                        {
                            InventoryId = item.Id,
                            LocationId = item.LocationId,
                            LocationName = item.LocationName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            StockNum = item.TotalNum,
                            Num = Convert.ToInt32(item.CanUseNum),
                            UomText = item.UomText
                        });
                    }
                }



                //IEnumerable<InventoryBatchDO> inventoryBatchs = null;
                //if (request.ProductIds.Any())
                //{
                //    //查询外采产品的库存
                //    inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id in @product_ids and is_deleted=0",
                //        new
                //        {
                //            shop_Id = request.LocationId,
                //            product_ids = request.ProductIds
                //        });
                //}
                //else
                //{
                //    inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and is_deleted=0 and can_use_num>0",
                //   new
                //   {
                //       shop_Id = request.LocationId
                //   });
                //}


                //var productStocks = new List<ProductStockDTO>();

                //if (inventoryBatchs != null && inventoryBatchs.Any())
                //{
                //    foreach (var item in inventoryBatchs.GroupBy(r => r.ProductId))
                //    {
                //        var inventoryId = inventoryResult?.Where(t => t.ProductId == item.Key)?.FirstOrDefault()?.Id??0;

                //        var batchNos = item.ToList().Select(r => r.Id);
                //        //查询批次占用
                //        var inventoryFlowItems = await inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.Key, location_id = request.LocationId });
                //        var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                //        productStocks.Add(new ProductStockDTO
                //        {
                //            InventoryId = inventoryId,
                //            ProductId = item.Key,
                //            StockNum = item.Sum(r => r.CanUseNum),
                //            ProductName = item.First().ProductName,
                //            Num = Convert.ToInt32(item.Sum(r => r.CanUseNum) - sumUnAvailabelNum)
                //        });
                //    }
                //    res.Data = productStocks;
                //}
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopOutPurchaseStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 通用门店库存查询（无批次）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopAllProductStocks(GetProductStockRequest request)
        {
            _logger.Info(JsonConvert.SerializeObject(request));
            var res = new ApiResult<List<ProductStockDTO>>();
            res.Data = new List<ProductStockDTO>();
            try
            {
                //库存汇总表
                var stockParams = new DynamicParameters();
                var condition = " where location_id =@location_id and is_deleted=0  ";
                stockParams.Add("@location_id", request.LocationId);

                if (request.ProductIds != null && request.ProductIds.Any())
                {
                    stockParams.Add("@product_ids", request.ProductIds);
                    condition += " and product_id in @product_ids";
                }

                if (!string.IsNullOrEmpty(request.SourceType))
                {
                    stockParams.Add("@source_type", request.SourceType);
                    condition += " and source_type = @source_type";
                }

                if (!string.IsNullOrEmpty(request.SearchKey))
                {
                    stockParams.Add("@key", $"%{request.SearchKey}%");
                    stockParams.Add("@productCode", request.SearchKey);
                    condition += " and ( product_name like @key or product_id = @productCode )";
                }

                if (request.NumType == 1)
                {
                    condition += " and can_use_num > 0";
                }
                else if (request.NumType == -1)
                {
                    condition += " and can_use_num < 0";
                }
                else if (request.NumType == 2)
                {
                    condition += " and can_use_num <> 0";
                }

                var inventoryResult = await _stockManageRepository.GetListAsync(condition, stockParams);
                if (inventoryResult != null && inventoryResult.Any())
                {
                    foreach (var item in inventoryResult.OrderByDescending(t => t.Id))
                    {
                        res.Data.Add(new ProductStockDTO
                        {
                            InventoryId = item.Id,
                            LocationId = item.LocationId,
                            LocationName = item.LocationName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            StockNum = item.TotalNum,
                            Num = Convert.ToInt32(item.CanUseNum),
                            ActualNum = Convert.ToInt32(item.CanUseNum),
                            UomText = item.UomText
                        });
                    }
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopAllProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> BatchCreateStockOutTask(StockInOutDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await GetShopById();
            long locationId = shopInfo?.ShopId ?? 7;
            string locationName = shopInfo?.SimpleName ?? "测试门店";

            try
            {
                //只有一条记录！！
                if (request.StockItems.Any())
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                        {
                            CreateBy = this.identityService.GetUserName(),
                            IsDeleted = 0,
                            CreateTime = DateTime.Now,
                            LocationId = locationId,
                            LocationName = locationName, //TODO
                            OperateTime = request.OperateTime,
                            OperationType = StockOperateTypeEnum.出库.ToString(),
                            Operator = request.Operator,
                            Remark = request.Remark,
                            SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                            SourceType = request.SourceType,
                            Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                        });



                        foreach (var stockProduct in request.StockItems)
                        {
                            //var stockProduct = request.StockItems.FirstOrDefault();

                            var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@locationId and product_id = @productId and is_deleted =0 and can_use_num>0",
                                new { locationId = locationId, productId = stockProduct.ProductId });
                            //判断每个批次的可用库存 -占用库存-锁定库存

                            if (inventoryBatchs.Any())
                            {
                                inventoryBatchs = inventoryBatchs.OrderBy(r => r.Id); //先进先出原则
                                var batchIds = inventoryBatchs.Select(r => r.Id);

                                var inventoryItems = await inventoryFlowItemRepository.GetListAsync(" where location_id =@locationId and batch_no in @batchNos and is_deleted =0 ", new { locationId = locationId, batchNos = batchIds });
                                var inventoryItemDic = new Dictionary<string, List<InventoryFlowItemDO>>();

                                foreach (var item in inventoryItems)
                                {
                                    if (inventoryItemDic.ContainsKey(item.BatchNo))
                                    {
                                        inventoryItemDic[item.BatchNo].Add(item);
                                    }
                                    else
                                    {
                                        inventoryItemDic[item.BatchNo] = new List<InventoryFlowItemDO>();
                                        inventoryItemDic[item.BatchNo].Add(item);
                                    }
                                }

                            TODO: //批次的总库存是不动
                                  //取批次的可用库存 - 修改后的 占用库存 和锁定库存
                                long diffNum = stockProduct.Num;

                                var tempStockOutProducts = new List<StockOutProductDTO>();

                                //获取需要扣减的批次和数量  先进先出的扣
                                foreach (var item in inventoryBatchs)
                                {
                                    if (diffNum > 0)
                                    {
                                        if (inventoryItemDic.ContainsKey(item.Id.ToString()))
                                        {
                                            inventoryItems = inventoryItemDic[item.Id.ToString()];
                                            long sumUnAvailableNum = inventoryItems.Sum(r => r.AfterOccupyNum) + inventoryItems.Sum(r => r.AfterLockNum);

                                            long availableNum = item.CanUseNum - sumUnAvailableNum;

                                            if (diffNum > availableNum)
                                            {
                                                tempStockOutProducts.Add(new StockOutProductDTO
                                                {
                                                    BatchNo = item.Id,
                                                    Num = availableNum
                                                });
                                                diffNum -= availableNum;
                                                //这个批次的数据全部扣减掉
                                                continue;
                                            }
                                            else
                                            {
                                                tempStockOutProducts.Add(new StockOutProductDTO
                                                {
                                                    BatchNo = item.Id,
                                                    Num = diffNum
                                                });
                                                //部分扣掉  把差异的库存给扣掉
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (item.CanUseNum > diffNum)
                                            {
                                                tempStockOutProducts.Add(new StockOutProductDTO
                                                {
                                                    BatchNo = item.Id,
                                                    Num = diffNum
                                                });
                                                //部分扣掉
                                                break;
                                            }
                                            else
                                            {
                                                tempStockOutProducts.Add(new StockOutProductDTO
                                                {
                                                    BatchNo = item.Id,
                                                    Num = item.CanUseNum
                                                });

                                                diffNum -= item.CanUseNum;
                                                //这个批次的数据全部扣减掉
                                                continue;
                                            }
                                        }
                                    }
                                }

                                if (tempStockOutProducts.Any())
                                {
                                    #region  获取产品总库存记录
                                    var stockParams = new DynamicParameters();
                                    stockParams.Add("@location_id", locationId);
                                    stockParams.Add("@product_id", stockProduct.ProductId);
                                    var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                                    var inventoryData = inventoryResult?.FirstOrDefault();

                                    var totalNum = inventoryData.TotalNum;
                                    var canUseNum = inventoryData.CanUseNum;
                                    var totalCost = inventoryData.TotalCost;
                                    #endregion

                                    foreach (var item in tempStockOutProducts)
                                    {
                                        var batchInfo = await inventoryBatchRepository.GetAsync(item.BatchNo);

                                        //出入库产品
                                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                                        {
                                            IsDeleted = 0,
                                            BatchNo = item.BatchNo.ToString(),
                                            Cost = batchInfo.Cost,
                                            CreateBy = this.identityService.GetUserName(),
                                            CreateTime = DateTime.Now,
                                            InoutId = stockId,
                                            Num = Convert.ToInt32(item.Num),
                                            ProductId = batchInfo.ProductId,
                                            ProductName = batchInfo.ProductName,
                                            ReleationId = stockProduct.ReleationId,  //其他入库  无关联,
                                            Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                                            SupplierId = Convert.ToInt64(batchInfo.SupplierId), //其他入库  无关联,
                                            SupplierName = batchInfo.SupplierName,
                                            UomText = "个"
                                        });

                                        //2.扣减批次信息
                                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                                        {
                                            Id = batchInfo.Id,
                                            UpdateBy = identityService.GetUserName(),
                                            CanUseNum = batchInfo.CanUseNum - item.Num
                                            // TotalNum = batchInfo.TotalNum - item.Num
                                        });

                                        //获取原入库批次流信息
                                        var inventoryBatchFlows = await _inventoryBatchFlowRepository.GetListAsync(" where batch_no =@batch_no and is_deleted=0", new { batch_no = item.BatchNo });
                                        var inventoryBatchFlowResult = inventoryBatchFlows.FirstOrDefault(r => r.OperationType == StockOperateTypeEnum.入库.ToString());

                                        //3.生成批次流水信息
                                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                                        {
                                            ShopId = batchInfo.ShopId,
                                            ShopName = inventoryBatchFlowResult.ShopName,
                                            SourceId = inventoryBatchFlowResult.SourceId,
                                            SourceInventoryNo = inventoryBatchFlowResult.SourceInventoryNo,
                                            OperationType = StockOperateTypeEnum.出库.ToString(),
                                            SourceType = request.SourceType,
                                            ProductId = inventoryBatchFlowResult.ProductId,
                                            ProductName = inventoryBatchFlowResult.ProductName,
                                            BatchNo = Convert.ToInt64(item.BatchNo),
                                            Price = inventoryBatchFlowResult.Price,
                                            Amount = inventoryBatchFlowResult.Cost * item.Num,
                                            Cost = inventoryBatchFlowResult.Cost,
                                            //BeforeTotalNum = batchInfo.TotalNum,
                                            //AfterTotalNum = batchInfo.TotalNum - item.Num,
                                            //CurrentTotalNum = item.Num,
                                            BeforeCanUseNum = batchInfo.CanUseNum,
                                            AfterCanUseNum = batchInfo.CanUseNum - item.Num,
                                            CurrentCanUseNum = batchInfo.CanUseNum - item.Num,
                                            OwnId = batchInfo.OwnId,
                                            OwnType = batchInfo.OwnType,
                                            SupplierId = batchInfo.SupplierId,
                                            SupplierName = batchInfo.SupplierName,
                                            IsDeleted = 0,
                                            CreateBy = identityService.GetUserName(),
                                            CreateTime = DateTime.Now
                                        });


                                        //4.生成库存流水信息 扣减
                                        await inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                        {
                                            SourceNo = !string.IsNullOrWhiteSpace(request.SourceInventoryNo) ? request.SourceInventoryNo : stockId.ToString(),
                                            LocationId = locationId,
                                            LocationName = locationName,
                                            BatchNo = item.BatchNo.ToString(),
                                            ProductId = inventoryBatchFlowResult.ProductId,
                                            ProductName = inventoryBatchFlowResult.ProductName,
                                            BusinessCategory = 2,
                                            BeforeTotalNum = totalNum,//总数量要扣掉
                                            AfterTotalNum = totalNum - item.Num,
                                            ChangeTotalNum = item.Num,
                                            BeforeCanUseNum = canUseNum,
                                            AfterCanUseNum = canUseNum - item.Num,
                                            ChangeCanUseNum = item.Num,
                                            CreateBy = identityService.GetUserName() ?? "System",
                                            CreateTime = DateTime.Now
                                        });

                                        //扣减产品的总库存量和成本
                                        totalNum -= item.Num;
                                        canUseNum -= item.Num;
                                        totalCost -= item.Num * batchInfo.Cost;
                                    }

                                    //将这个产品占用的批次
                                    await _stockManageRepository.UpdateInventoryData(new InventoryDO
                                    {
                                        LocationId = locationId,
                                        ProductId = stockProduct.ProductId,
                                        UpdateBy = identityService.GetUserName(),
                                        TotalNum = totalNum,
                                        CanUseNum = canUseNum,
                                        TotalCost = totalCost
                                    });
                                }
                                res.Code = ResultCode.Success;
                            }
                            else
                            {
                                res.Code = ResultCode.Exception;
                                res.Message = $"产品【{stockProduct.ProductName}】领料异常,无可用库存扣减!";
                            }
                        }

                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"BatchCreateStockOutTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 创建出入库任务并更新库存（无批次）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> CreateInOutStockAndUpdateInventory(StockInOutDTO request)
        {
            var res = new ApiResult<long>();

            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? identityService.GetUserName() : request.CreateBy;
            long locationId = 0;
            string locationName = string.Empty;
            if (request.LocationId > 0 && !string.IsNullOrEmpty(request.LocationName))
            {
                locationId = request.LocationId;
                locationName = request.LocationName;
            }
            else if (request.LocationId > 0)
            {
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = request.LocationId
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    locationId = request.LocationId;
                    locationName = shopResult.Data.SimpleName;
                }
            }
            else
            {
                var shopInfo = await GetShopById();
                locationId = shopInfo?.ShopId ?? 0;
                locationName = shopInfo?.SimpleName ?? string.Empty;
            }
            try
            {
                //创建记录
                var stockId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = createBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = locationId,
                    LocationName = locationName, //TODO
                    OperateTime = request.OperateTime,
                    OperationType = request.OperationType,
                    Operator = request.Operator,
                    SourceInventoryNo = request.SourceInventoryNo, //如果是空的 需要手动生成
                    SourceType = request.SourceType,
                    Status = request.Status // StockInOutStatusEnum.已收货.ToString()   //判断有无差异数目
                });


                if (request.StockItems.Any())
                {
                    foreach (var item in request.StockItems)
                    {
                        item.CreateBy = createBy;

                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            InoutId = stockId,
                            ProductId = item.ProductId.Trim(),
                            ProductName = item.ProductName,
                            Num = item.Num,
                            ActualNum = item.ActualNum,
                            GoodNum = item.GoodNum,
                            BadNum = item.BadNum,
                            BatchNo = string.Empty,
                            Cost = item.Cost,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            ReleationId = item.ReleationId,  //其他入库  无关联,
                            Status = item.Status, // StockInOutStatusEnum.已收货.ToString(), //判断有误差异数据
                            SupplierId = item.SupplierId,
                            SupplierName = item.SupplierName,
                            UomText = item.UomText,
                            IsDeleted = 0
                        });

                        //txw 20221206 去掉批次，只更新库存
                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", locationId);
                        stockParams.Add("@product_id", item.ProductId.Trim());
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);
                        var inventoryData = inventoryResult?.FirstOrDefault();
                        int numType = request.OperationType == StockOperateTypeEnum.入库.ToString() ? 1 : -1;

                        if (inventoryData != null)
                        {
                            inventoryData.TotalNum += item.ActualNum * numType;
                            inventoryData.TotalCost += item.ActualNum * item.Cost * numType;
                            inventoryData.CanUseNum += item.ActualNum * numType;
                            inventoryData.UomText = string.IsNullOrWhiteSpace(item.UomText) ? inventoryData.UomText: item.UomText ;
                            inventoryData.UpdateTime = DateTime.Now;
                            inventoryData.UpdateBy = createBy;
                            //更新表记录
                            await _stockManageRepository.UpdateAsync(inventoryData, new List<string> { "TotalNum", "TotalCost", "CanUseNum", "UomText", "UpdateTime", "UpdateBy" });

                        }
                        else
                        {
                            //新增记录
                            await _stockManageRepository.InsertAsync(new InventoryDO
                            {
                                LocationId = locationId,
                                LocationName = locationName,
                                ProductId = item.ProductId.Trim(),
                                ProductName = item.ProductName,
                                SourceType = item.ProductId.StartsWith("MD-")? OwnerType.Shop.ToString() : OwnerType.Company.ToString(),
                                TotalCost = item.ActualNum * item.Cost,
                                TotalNum = item.ActualNum,
                                CanUseNum = item.ActualNum,
                                UomText = item.UomText,
                                IsDeleted = 0,
                                CreateBy = createBy,
                                CreateTime = DateTime.Now
                            });
                        }
                        
                    }

                }
                res.Data = stockId;
                res.Message = "操作成功!";
                res.Code = ResultCode.Success;

            }
            catch (Exception ex)
            {
                _logger.Error($"CreateInOutStockAndUpdateInventory_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

    }
}
