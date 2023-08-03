using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Login.Auth;
using Newtonsoft.Json;
using Dapper;
using System.Linq;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Client.Clients;

namespace Ae.Shop.Api.Imp.Services.Stock
{
    public class StoragePlanService : IStoragePlanService
    {
        #region 初始化函数
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<StoragePlanService> _logger;
        private readonly IStoragePlanRepository _storagePlanRepository;
        private readonly IStoragePlanProductRepository storagePlanProductRepository;
        private readonly IStockManageRepository stockManageRepository;
        private IIdentityService identityService;
        private IStockManageService stockManageService;
        private readonly IShopWmsLogRepository _shopWmsLogRepository;
        private readonly IShopPurchaseService shopPurchaseService;
        private readonly IInventoryBatchRepository _inventoryBatchRepository;

        public StoragePlanService(IMapper _mapper,
                                  ApolloErpLogger<StoragePlanService> _logger,
                                  IStoragePlanRepository _storagePlanRepository,
                                  IStoragePlanProductRepository storagePlanProductRepository,
                                  IIdentityService identityService, IStockManageRepository stockManageRepository,
                                  IStockManageService stockManageService,
                                  IShopPurchaseService shopPurchaseService,
                                  IShopWmsLogRepository shopWmsLogRepository,
                                  IInventoryBatchRepository inventoryBatchRepository)
        {
            this._mapper = _mapper;
            this._logger = _logger;
            this._storagePlanRepository = _storagePlanRepository;
            this.storagePlanProductRepository = storagePlanProductRepository;
            this.identityService = identityService;
            this.stockManageRepository = stockManageRepository;
            this.stockManageService = stockManageService;
            this.shopPurchaseService = shopPurchaseService;
            this._shopWmsLogRepository = shopWmsLogRepository;
            this._inventoryBatchRepository = inventoryBatchRepository;
        }

        /// <summary>
        /// 撤销盘点记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CancelStoragePlanProduct(StoragePlanProductDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                await storagePlanProductRepository.CancelStoragePlanProduct(new StoragePlanProductDO
                {
                    Id = request.Id,
                    Status = StorageTypeEnum.新建.ToString(),
                    UpdateBy = identityService.GetUserName()
                });
                var storagePlan = await storagePlanProductRepository.GetAsync(request.Id);
                await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                {
                    LogLevel = LogLevelEnum.Info.ToString(),
                    CreateBy = identityService.GetUserName(),
                    CreateTime = DateTime.Now,
                    ObjectId = storagePlan.PlanId,
                    ObjectType = ShopWmsLogEnum.Storage.ToString(),
                    Remark = $"任务【{storagePlan.PlanId}】的产品【{storagePlan.ProductName}】撤销盘库"
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CancelStoragePlanProduct_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        #endregion

        public async Task<ApiResult<string>> CreateStoragePlan(StoragePlanDTO request)
        {
            var res = new ApiResult<string>();

            if (request.ShopId <= 0)
            {
                var shopInfo = await stockManageService.GetShopById();
                request.ShopId = shopInfo?.ShopId ?? 0;
                request.ShopName = shopInfo?.SimpleName ?? "";
            }

            try
            {
                var tempProducts = new List<TempProductDTO>();
                //如果是全盘 需要拉取现有的产品做处理
                if (request.Type == StorageStatusEnum.全盘.ToString())
                {
                    var rgProductStocksRes = await stockManageService.GetShopAllProductStocks(new GetProductStockRequest
                    {
                        LocationId = request.ShopId,
                        SourceType = request.SourceType,
                        NumType = 2  //所有非零库存
                    });
                    if (rgProductStocksRes.Code == ResultCode.Success)
                    {
                        var rgProductStocks = rgProductStocksRes.Data;
                        if (rgProductStocks.Any())
                        {
                            rgProductStocks.ForEach(r =>
                            {
                                tempProducts.Add(new TempProductDTO
                                {
                                    ProductId = r.ProductId,
                                    ProductName = r.ProductName,
                                    Unit = r.UomText,
                                    ProductType = request.SourceType
                                });
                            });
                        }
                    }
                }
                else
                {
                    if (request.StoragePlans.Any())
                    {
                        request.StoragePlans.ForEach(r =>
                        {
                            tempProducts.Add(new TempProductDTO { ProductId = r.ProductId, ProductName = r.ProductName, ProductType = r.ProductType, Unit = r.Unit });
                        });

                    }
                }


                if (tempProducts.Any())
                {
                    await CreateStoragePlanCommon(request, tempProducts);
                }
                else
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = $"{request.ShopName}暂无产品需要盘点!"
                    };
                }
                res.Code = ResultCode.Success;

            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateStoragePlan_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateStoragePlan_delete(StoragePlanDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await stockManageService.GetShopById();
            long locationId = shopInfo?.ShopId ?? 0;
            string locationName = shopInfo?.SimpleName ?? "";
            try
            {
                //如果是全盘 需要拉取现有的产品做处理
                if (request.Type == StorageStatusEnum.全盘.ToString())
                {
                    //这个门店所有的上架产品做盘点(总部+非总部)
                    //门店铺货的+外采的
                    //门店铺货的产品 库存为0   外采的sku 全部盘点
                    var tempProducts = new List<TempProductDTO>();

                    if (request.SourceType == "Company")
                    {
                        var rgProductStocksRes = await stockManageService.GetAllProductStocks();
                        if (rgProductStocksRes.Code != ResultCode.Success)
                        {
                            return new ApiResult<string>
                            {
                                Code = ResultCode.Exception,
                                Message = "铺货库存查询异常!"
                            };
                        }
                        else
                        {
                        TODO: //只盘点库存大于0的！！！
                            var rgProductStocks = rgProductStocksRes.Data;
                            if (rgProductStocks.Any(r => r.StockNum > 0))
                            {
                                rgProductStocks.ForEach(r =>
                                {
                                    tempProducts.Add(new TempProductDTO
                                    {
                                        ProductId = r.ProductId,
                                        ProductName = r.ProductName,
                                        ProductType = ProductTypeEnum.总部产品.ToString()
                                    });
                                });
                            }
                            else
                            {
                                return new ApiResult<string>
                                {
                                    Code = ResultCode.Exception,
                                    Message = $"{locationName}暂无铺货产品需要盘点!"
                                };
                            }
                        }
                    }
                    else if (request.SourceType == "Shop")
                    {
                        //外采的所有sku
                        var shopProducts = await shopPurchaseService.SearchProduct(new ProductSearchRequest
                        {
                            IsOnSale = 1,
                            PurchaseType = 2,
                            PageIndex = 1,
                            PageSize = 10000000
                        });
                        if (shopProducts.Items != null &&
                            shopProducts.Items.Any())
                        {
                            shopProducts.Items.ForEach(r =>
                            {
                                tempProducts.Add(new TempProductDTO
                                {
                                    ProductId = r.ProductCode,
                                    ProductName = r.ProductName,
                                    ProductType = ProductTypeEnum.其他产品.ToString()
                                });
                            });
                        }
                        else
                        {
                            return new ApiResult<string>
                            {
                                Code = ResultCode.Exception,
                                Message = $"{locationName}暂无外采产品需要盘点!"
                            };
                        }
                    }
                    if (tempProducts.Any())
                    {
                        await CreateStoragePlanCommon(request, tempProducts);
                    }
                    else
                    {
                        var sourceType = request.SourceType == "Company" ? "总部" : "外采";
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Exception,
                            Message = $"{locationName}暂无{sourceType}产品需要盘点!"
                        };
                    }

                }
                else if (request.Type == StorageStatusEnum.指定产品.ToString())
                {
                    if (request.SourceType == "Company")
                    {
                        var productId = request.StoragePlans.FirstOrDefault()?.ProductId;
                        var shopId = Convert.ToInt64(identityService.GetOrganizationId());
                        //先需要Check 该SKU是否在门店存在铺货记录
                        var inventoryBatchs = await _inventoryBatchRepository.GetListAsync("where shop_Id=@shop_Id and product_id=@product_id and is_deleted=0", new { shop_Id = shopId, product_id = productId });
                        if (inventoryBatchs != null && inventoryBatchs.Any())
                        {
                            if (request.StoragePlans.Any())
                            {
                                var tempProducts = new List<TempProductDTO>();
                                request.StoragePlans.ForEach(r =>
                                {
                                    tempProducts.Add(new TempProductDTO { ProductId = r.ProductId, ProductName = r.ProductName, ProductType = r.ProductType });
                                });

                                await CreateStoragePlanCommon(request, tempProducts);
                            }
                        }
                        else
                        {
                            return new ApiResult<string>
                            {
                                Code = ResultCode.Exception,
                                Message = "该产品未在门店铺货，无法做盘点操作!"
                            };
                        }
                    }
                    else if (request.SourceType == "Shop")
                    {
                        if (request.StoragePlans.Any())
                        {
                            var tempProducts = new List<TempProductDTO>();
                            request.StoragePlans.ForEach(r =>
                            {
                                tempProducts.Add(new TempProductDTO { ProductId = r.ProductId, ProductName = r.ProductName, ProductType = r.ProductType });
                            });

                            await CreateStoragePlanCommon(request, tempProducts);
                        }
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateStoragePlan_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> CreateStoragePlanCommon(StoragePlanDTO request, List<TempProductDTO> products)
        {
            var res = new ApiResult<string>();
            if (request.ShopId <= 0)
            {
                var shopInfo = await stockManageService.GetShopById();
                request.ShopId = shopInfo?.ShopId ?? 0;
                request.ShopName = shopInfo?.SimpleName ?? "";
            }
            var createBy = identityService.GetUserName();

            try
            {
                if (!products.Any())
                {
                    res.Code = ResultCode.Exception;
                    return res;
                }

                var planNo = DateTime.Now.ToString("yyyyMMdd");

                var planId = await _storagePlanRepository.InsertAsync(new StoragePlanDO
                {
                    ShopId = request.ShopId,
                    ShopName = request.ShopName,
                    WarehouseId = request.WarehouseId > 0 ? request.WarehouseId : request.ShopId,
                    WarehouseName = string.IsNullOrWhiteSpace( request.WarehouseName)? request.ShopName : request.WarehouseName,
                    PlanNo = planNo,
                    PlanName = request.PlanName,
                    SourceType = request.SourceType,
                    Type = request.Type,
                    Status = StorageTypeEnum.新建.ToString(),
                    PlanTime = request.PlanTime,
                    IsDeleted = 0,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now
                });

                foreach (var item in products)
                {
                    await storagePlanProductRepository.InsertAsync(new StoragePlanProductDO
                    {
                        PlanId = planId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Unit = item.Unit,
                        ProductType = item.ProductType,
                        //盘点总库存
                        SystemNum = 0,
                        Status = StorageTypeEnum.新建.ToString(),
                        IsDeleted = 0,
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    });
                }
                string productSourceType = request.SourceType == "Company" ? "平台产品" : "外采产品";
               
                await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                {
                    LogLevel = LogLevelEnum.Info.ToString(),
                    CreateBy = identityService.GetUserName(),
                    CreateTime = DateTime.Now,
                    ObjectId = planId,
                    ObjectType = ShopWmsLogEnum.Storage.ToString(),
                    Remark = $"创建【{productSourceType}】的【{ request.Type}】盘点任务"
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateStoragePlanCommon_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 处理盘库差异,不考虑批次
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DealStorageProduct(StockDiffDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await stockManageService.GetShopById();
            var shopId = shopInfo?.ShopId ?? 0;
            var shopName = shopInfo?.SimpleName ?? string.Empty;

            long locationId = request.WarehouseId;
            string locationName = request.WarehouseName;
            if (request.WarehouseId <= 0)
            {
                locationId = shopId;
                locationName = shopName;
            }
            
            var createBy = identityService.GetUserName();

            try
            {
                var updateStatus = string.Empty;
                if (request.DealType == DealTypeEnum.差异确认通过.ToString())
                {
                    //var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);
                    var actualNum = System.Math.Abs(request.DiffNum);

                    //盘盈
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = locationId,
                        LocationName = locationName,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceInventoryNo = request.PlanId.ToString(),
                        SourceType = StockInTypeEnum.盘盈入库.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = "盘盈入库"
                    };
                    stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                    {
                        ReleationId = request.Id,
                        ProductId = request.ProductId,
                        ProductName = request.ProductName,
                        Num = actualNum,
                        GoodNum = actualNum,
                        ActualNum = actualNum,
                        UomText = request.Unit,
                        Cost = 0,
                        CreateBy = createBy,
                        Status = StockInOutStatusEnum.已收货.ToString()
                    });
                    
                    //盘盈
                    if (request.DiffNum > 0)
                    {
                        
                    }
                    //盘亏
                    else if (request.DiffNum < 0)
                    {
                        stockInoutRequest.OperationType = StockOperateTypeEnum.出库.ToString();
                        stockInoutRequest.SourceType = StockOutTypeEnum.盘亏出库.ToString();
                        stockInoutRequest.Status = StockInOutStatusEnum.已出库.ToString();
                        stockInoutRequest.Remark = "盘亏出库";
                        stockInoutRequest.StockItems.FirstOrDefault().Status = StockInOutStatusEnum.已出库.ToString();

                    }

                    var resResult = await stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);
                    if (resResult.Code == ResultCode.Success)
                    {
                        updateStatus = StorageTypeEnum.盘点完成.ToString();
                        res.Data = resResult.Data.ToString();
                    }
                    else
                    {
                        res.Message = "盘点差异处理异常!";
                        res.Code = ResultCode.Exception;
                        return res;
                    }

                }
                else if (request.DealType == DealTypeEnum.要求重盘此产品.ToString())
                {
                    //生成新的盘库计划
                    var planNo = DateTime.Now.ToString("yyyyMMdd");
                    //var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);

                    var planId = await _storagePlanRepository.InsertAsync(new StoragePlanDO
                    {
                        ShopId = shopId,
                        ShopName = shopName,
                        WarehouseId = locationId,
                        WarehouseName = locationName,
                        PlanNo = planNo,
                        PlanName = $"【重盘】{request.ProductName}",
                        Type = StorageStatusEnum.指定产品.ToString(),
                        SourceType = request.ProductType == "平台产品" ? "Company" : "Shop",
                        Status = StorageTypeEnum.新建.ToString(),
                        PlanTime = request.PlanTime.Value,
                        IsDeleted = 0,
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    });

                    await storagePlanProductRepository.InsertAsync(new StoragePlanProductDO
                    {
                        PlanId = planId,
                        SourceId = request.Id, //关联源单号
                        ProductId = request.ProductId,
                        ProductName = request.ProductName,
                        Unit = request.Unit,
                        ProductType = request.ProductType,
                        //盘点总库存
                        SystemNum = 0,
                        Status = StorageTypeEnum.新建.ToString(),
                        IsDeleted = 0,
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    });
                    //原计划关闭“无效”
                    updateStatus = StorageTypeEnum.无效.ToString();

                }

                if (!string.IsNullOrWhiteSpace(updateStatus))
                {
                    //var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);

                    //更新盘库产品 状态
                    await storagePlanProductRepository.DealStorageProduct(new StoragePlanProductDO
                    {
                        Id = request.Id,
                        UpdateBy = createBy,
                        Status = updateStatus,
                        DealBy = createBy,
                        DealRemark = request.DealRemark,
                        DealType = request.DealType
                    });
                }

                //更新盘库任务的状态
                var storageProducts = await storagePlanProductRepository.GetListAsync(" where plan_id =@planId and is_deleted=0 ", new { planId = request.PlanId });
                //盘库计划中有异常的数据都已经处理
                if (!storageProducts.Any(r => r.Status == StorageTypeEnum.差异处理中.ToString()))
                {
                    await _storagePlanRepository.UpdateStoragePlanStatus(new StoragePlanDO
                    {
                        UpdateBy = createBy,
                        Id = request.PlanId,
                        UpdateType = 2,
                        Status = StorageTypeEnum.盘点完成.ToString()
                    });

                    await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                    {
                        LogLevel = LogLevelEnum.Info.ToString(),
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        ObjectId = request.PlanId,
                        ObjectType = ShopWmsLogEnum.Storage.ToString(),
                        Remark = $"盘点单【{request.PlanId}】{StorageTypeEnum.盘点完成.ToString()}"
                    });
                }


                res.Message = "操作成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"DealStorageProduct_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 处理盘库差异(作废)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DealStorageProduct_delete(StockDiffDTO request)
        {
            var res = new ApiResult<string>();

            var shopInfo = await stockManageService.GetShopById();
            long locationId = shopInfo?.ShopId ?? 7;
            string locationName = shopInfo?.SimpleName ?? "测试门店";

            try
            {
                var status = string.Empty;
                if (request.DealType == DealTypeEnum.差异确认通过.ToString())
                {
                    //库存要平掉 
                    //1.盘亏
                    // 1.创建盘亏出库单 2.库存的成本需要扣除 3.扣除按照先进先出的批次扣(获取成本) 

                    //2.盘盈
                    //1.创建盘盈入库单 2.成本为0  3.新生成一个批次记录关联

                    //3.盘库任务状态“盘点完成”

                    var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);

                    //盘盈
                    if (request.DiffNum > 0)
                    {
                        var storagePlan = await _storagePlanRepository.GetAsync(request.PlanId);
                        if (storagePlan.SourceType == OwnerType.Company.ToString())
                        {
                            //铺货商品的盘盈
                            //这种SKU 存在过铺货记录
                            var stockInoutRequest = new StockInOutDTO
                            {
                                OperateTime = DateTime.Now,
                                SourceType = StockInTypeEnum.盘盈入库.ToString(),
                                Operator = identityService.GetUserName(),
                                Status = StockInOutStatusEnum.已收货.ToString(),
                                Remark = $"【{storagePlan.ShopName}】的铺货商品盘盈入库"
                            };

                            stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                            {
                                ProductId = storageProduct.ProductId,
                                ProductName = storageProduct.ProductName,
                                Num = request.DiffNum,
                                GoodNum = request.DiffNum,
                                ActualNum = request.DiffNum,
                                UomText = storageProduct.Unit,
                                Cost = 0,
                                Status = StockInOutStatusEnum.已收货.ToString()
                            });
                            //新建盘盈入库单 
                            res = await stockManageService.CreateInStockTaskForTransfer(stockInoutRequest);
                            if (res.Code == ResultCode.Success)
                            {
                                status = StorageTypeEnum.盘点完成.ToString();
                            }
                            else
                            {
                                res.Message = "盘盈入库异常!";
                                return res;
                            }
                        }
                        else if (storagePlan.SourceType == OwnerType.Shop.ToString())
                        {
                            //外采SKU的盘点
                            var stockInoutRequest = new StockInOutDTO
                            {
                                OperateTime = DateTime.Now,
                                SourceType = StockInTypeEnum.盘盈入库.ToString()
                            };

                            stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                            {
                                ProductId = storageProduct.ProductId,
                                ProductName = storageProduct.ProductName,
                                Num = request.DiffNum,
                                GoodNum = request.DiffNum,
                                ActualNum = request.DiffNum,
                                Cost = 0
                            });
                            //新建盘盈入库单 
                            res = await stockManageService.CreateInStockTask(stockInoutRequest);
                            if (res.Code == ResultCode.Success)
                            {
                                status = StorageTypeEnum.盘点完成.ToString();
                            }
                            else
                            {
                                res.Message = "盘盈入库异常!";
                                return res;
                            }
                        }
                        await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                        {
                            LogLevel = LogLevelEnum.Info.ToString(),
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            ObjectId = storageProduct.PlanId,
                            ObjectType = ShopWmsLogEnum.Storage.ToString(),
                            Remark = $"盘库任务【{storageProduct.PlanId}】的产品【{ storageProduct.ProductName}】差异确认通过，自动做【盘盈】"
                        });
                    }
                    //盘亏
                    else if (request.DiffNum < 0)
                    {
                        //库存的扣减按照先进先出的原则    要排除掉锁定库存+占用库存！！！
                        var stockOutRequest = new StockInOutDTO
                        {
                            OperateTime = DateTime.Now,
                            Operator = "系统自动处理",
                            Remark = $"【{request.ProductName}】盘亏出库",
                            SourceType = StockOutTypeEnum.盘亏出库.ToString(),
                            SourceInventoryNo = request.PlanId.ToString()

                        };
                        stockOutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ProductId = request.ProductId,
                            ProductName = request.ProductName,
                            Num = Math.Abs(request.DiffNum),
                            Cost = 0,
                            ReleationId = request.Id
                        });

                        res = await stockManageService.CreateStorageOutStockTask(stockOutRequest);
                        if (res.Code == ResultCode.Success)
                        {
                            status = StorageTypeEnum.盘点完成.ToString();
                        }
                        else
                        {
                            res.Message = "盘亏出库异常!";
                            res.Code = ResultCode.Exception;
                            return res;
                        }

                        await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                        {
                            LogLevel = LogLevelEnum.Info.ToString(),
                            CreateBy = identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            ObjectId = storageProduct.PlanId,
                            ObjectType = ShopWmsLogEnum.Storage.ToString(),
                            Remark = $"盘库任务【{storageProduct.PlanId}】的产品【{ storageProduct.ProductName}】差异确认通过，自动做【盘亏】"
                        });
                    }
                }
                else if (request.DealType == DealTypeEnum.要求重盘此产品.ToString())
                {
                    //生成新的盘库计划

                    var planNo = DateTime.Now.ToString("yyyyMMdd");
                    var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);

                    var planId = await _storagePlanRepository.InsertAsync(new StoragePlanDO
                    {
                        ShopId = locationId,
                        ShopName = locationName,
                        PlanNo = planNo,
                        PlanName = $"【重盘】{request.ProductName}",
                        Type = StorageStatusEnum.指定产品.ToString(),
                        SourceType = storageProduct?.ProductType == "总部产品" ? "Company" : "Shop",
                        Status = StorageTypeEnum.新建.ToString(),
                        PlanTime = request.PlanTime.Value,
                        IsDeleted = 0,
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now
                    });

                    await storagePlanProductRepository.InsertAsync(new StoragePlanProductDO
                    {
                        PlanId = planId,
                        SourceId = request.Id, //关联源单号
                        ProductId = request.ProductId,
                        ProductName = request.ProductName,
                        Unit = request.Unit,
                        ProductType = request.ProductType,
                        //盘点总库存
                        SystemNum = 0,
                        Status = StorageTypeEnum.新建.ToString(),
                        IsDeleted = 0,
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now
                    });
                    //原计划关闭“无效”
                    status = StorageTypeEnum.无效.ToString();

                    await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                    {
                        LogLevel = LogLevelEnum.Info.ToString(),
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        ObjectId = storageProduct.PlanId,
                        ObjectType = ShopWmsLogEnum.Storage.ToString(),
                        Remark = $"盘库任务【{storageProduct.PlanId}】的产品【{ storageProduct.ProductName}】差异确认不通过，自动创建新的重盘任务【{planId}】"
                    });
                }

                if (!string.IsNullOrWhiteSpace(status))
                {
                    var storageProduct = await storagePlanProductRepository.GetAsync(request.Id);

                    //更新盘库产品 状态
                    await storagePlanProductRepository.DealStorageProduct(new StoragePlanProductDO
                    {
                        Id = request.Id,
                        UpdateBy = identityService.GetUserName(),
                        Status = status,
                        DealBy = identityService.GetUserName(),
                        DealRemark = request.DealRemark,
                        DealType = request.DealType
                    });
                }

                //更新盘库任务的状态
                var storageProducts = await storagePlanProductRepository.GetListAsync(" where plan_id =@planId and is_deleted=0 ", new { planId = request.PlanId });
                //盘库计划中有异常的数据都已经处理
                if (!storageProducts.Any(r => r.Status == StorageTypeEnum.差异处理中.ToString()))
                {
                    await _storagePlanRepository.UpdateStoragePlanStatus(new StoragePlanDO
                    {
                        UpdateBy = identityService.GetUserName(),
                        Id = request.PlanId,
                        UpdateType = 2,
                        Status = StorageTypeEnum.盘点完成.ToString()
                    });

                    await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                    {
                        LogLevel = LogLevelEnum.Info.ToString(),
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        ObjectId = request.PlanId,
                        ObjectType = ShopWmsLogEnum.Storage.ToString(),
                        Remark = $"盘库任务【{request.PlanId}】{StorageTypeEnum.盘点完成.ToString()}"
                    });
                }
                res.Message = "操作成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"DealStorageProduct_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 盘库差异详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<StockDiffDTO>> GetStockDiffDetail(StoragePlanProductDTO request)
        {
            var res = new ApiResult<StockDiffDTO>();
            try
            {
                var result = await storagePlanProductRepository.GetStockDiffDetail(new StoragePlanProductDO
                {
                    Id = request.Id
                });

                var vo = _mapper.Map<StockDiffDTO>(result);
                int planType = -1;
                if (result.PlanType == StorageStatusEnum.指定产品.ToString())
                {
                    planType = 1;
                }
                else if (result.PlanType == StorageStatusEnum.全盘.ToString())
                {
                    planType = 2;
                }
                vo.PlanType1 = planType;
                if (!string.IsNullOrWhiteSpace(vo.DealType))
                {
                    int dealType = -1; ;
                    if (vo.DealType == DealTypeEnum.要求重盘此产品.ToString())
                    {
                        dealType = 1;
                    }
                    else if (vo.DealType == DealTypeEnum.差异确认通过.ToString())
                    {
                        dealType = 2;
                    }

                    vo.DealType1 = dealType;
                }
                else
                {
                    vo.DealType1 = 1;
                }
                vo.OperateBy = vo.OperateBy + " " + vo.OperateTime.ToString("yyyy-MM-dd hh:mm");

                if (!string.IsNullOrWhiteSpace(vo.DealType) && vo.DealType == DealTypeEnum.要求重盘此产品.ToString())
                {
                    var planProduct = await storagePlanProductRepository.GetListAsync(" where source_id =@source_id and is_deleted=0 ", new { source_id = request.Id });
                    var sourcePlanProduct = planProduct.FirstOrDefault();

                    var storagePlan = await _storagePlanRepository.GetAsync(sourcePlanProduct.PlanId);
                    vo.PlanTime = storagePlan.PlanTime;
                }
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetStockDiffDetail_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 库存差异
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<StockDiffDTO>> GetStockDiffs(StockDiffRequest request)
        {
            var res = new ApiPagedResult<StockDiffDTO>();

            var shopInfo = await stockManageService.GetShopById();
            request.ShopId = shopInfo?.ShopId ?? 0;

            try
            {
                if (request.Times != null && request.Times.Any())
                {
                    var times = request.Times;

                    request.StartTime = Convert.ToDateTime(times[0]);
                    request.EndTime = Convert.ToDateTime(times[1]);
                }
                var result = await storagePlanProductRepository.GetStockDiffs(request);

                var vo = _mapper.Map<ApiPagedResultData<StockDiffDTO>>(result);

                res.Data = vo;
                res.Message = "查询成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStockDiffs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiPagedResult<StoragePlanProductDTO>> GetStoragePlanProducts(GetStoragePlanProductsRequest request)
        {
            var res = new ApiPagedResult<StoragePlanProductDTO>();
            try
            {
                long locationId = request.WarehouseId;
                if (request.WarehouseId <= 0)
                {
                    var shopInfo = await stockManageService.GetShopById();
                    locationId = shopInfo?.ShopId ?? 0;
                }

                var param1 = new DynamicParameters();
                param1.Add("@plan_id", request.PlanId);
                var condition = new StringBuilder();
                if (request.Status == StorageTypeEnum.新建.ToString())
                {
                    param1.Add("@status", request.Status);
                    condition.Append(" and status=@status ");
                }
                else
                {
                    param1.Add("@status", StorageTypeEnum.新建.ToString());
                    condition.Append(" and status<>@status ");
                }
                var result = await storagePlanProductRepository.GetListPagedAsync(request.PageIndex, request.PageSize,
                    " where plan_id = @plan_id and is_deleted =0 " + condition.ToString(), "id desc", param1);

                var vo = _mapper.Map<ApiPagedResultData<StoragePlanProductDTO>>(result);

                if (request.Status == StorageTypeEnum.新建.ToString())
                {
                    var productStockDic = new Dictionary<string, InventoryDO>();
                    //判断产品是否做过盘点 如果做过 系统数量用表中的  如果未盘点 用库存表的
                    if (result.Items != null && result.Items.Any())
                    {
                        var productIds = result.Items.Where(r => r.Status == StorageTypeEnum.新建.ToString()).Select(r => r.ProductId);
                        var param = new DynamicParameters();
                        param.Add("@location_id", locationId);
                        param.Add("@product_ids", productIds);

                        //获取这类产品的库存
                        var productStocks = await this.stockManageRepository.GetListAsync(" where location_id =@location_id and is_deleted=0 and product_id in @product_ids", param);
                        productStockDic = productStocks.ToDictionary(r => r.ProductId, r => r);

                        foreach (var item in vo.Items)
                        {
                            item.SystemNum = productStockDic.ContainsKey(item.ProductId) ? Convert.ToInt32(productStockDic[item.ProductId].TotalNum) : 0;
                        }
                    }
                }

                res.Data = vo;

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetStoragePlanProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiPagedResult<StoragePlanDTO>> GetStoragePlans(GetStoragePlanRequest request)
        {
            var res = new ApiPagedResult<StoragePlanDTO>();
            try
            {
                var shopId = Convert.ToInt64(identityService.GetOrganizationId());
                request.ShopId = shopId;

                var param = new DynamicParameters();
                param.Add("@shop_id", request.ShopId);
                var conditions = new StringBuilder(" where is_deleted =0 and shop_id =@shop_id");

                DateTime? startTime = null;
                DateTime? endTime = null;
                var dateTime = new DateTime(2019, 10, 1);
                if (request.Times != null && request.Times.Any())
                {
                    startTime = Convert.ToDateTime(request.Times[0]);
                    endTime = Convert.ToDateTime(request.Times[1]);
                }

                if (!string.IsNullOrWhiteSpace(request.Status))
                {
                    conditions.Append(" and status=@status");
                    param.Add("@status", request.Status);
                }

                if (!string.IsNullOrWhiteSpace(request.Type))
                {
                    conditions.Append(" and type=@Type");
                    param.Add("@Type", request.Type);
                }
                if (request.WarehouseId > 0)
                {
                    conditions.Append(" and warehouse_id=@WarehouseId");
                    param.Add("@WarehouseId", request.Type);
                }

                if (startTime.HasValue &&
                 startTime.Value.Subtract(dateTime).TotalDays > 0)
                {
                    conditions.Append(" and plan_time>=@StartTime");
                    param.Add("@StartTime", startTime);
                }

                if (endTime.HasValue &&
                  endTime.Value.Subtract(dateTime).TotalDays > 0)
                {
                    conditions.Append(" and plan_time<=@EndTime");
                    param.Add("@EndTime", endTime);
                }

                if (!string.IsNullOrWhiteSpace(request.SourceType))
                {
                    conditions.Append(" and source_type<=@source_type");
                    param.Add("@source_type", request.SourceType);
                }

                var result = await _storagePlanRepository.GetListPagedAsync(request.PageIndex, request.PageSize,
                    conditions.ToString(), "id desc", param);

                var vo = _mapper.Map<ApiPagedResultData<StoragePlanDTO>>(result);

                res.Code = ResultCode.Success;
                res.Data = vo;

            }
            catch (Exception ex)
            {
                _logger.Error($"GetStoragePlans_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 更新盘点数量
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateStoragePlanProduct(UpdateStoragePlanRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                if (!request.Products.Any())
                {
                    res.Code = ResultCode.Exception;
                    res.Message = "盘点产品不能为空!";
                    return res;
                }
                foreach (var item in request.Products)
                {
                    await storagePlanProductRepository.UpdateProductStorageNum(new StoragePlanProductDO
                    {
                        Id = item.Id,
                        UpdateBy = identityService.GetUserName(),
                        Status = StorageTypeEnum.盘点中.ToString(),
                        SystemNum = item.SystemNum,
                        StorageNum = item.StorageNum,
                        DiffNum = item.StorageNum - item.SystemNum
                    });
                }
                var planId = request.Products.FirstOrDefault().PlanId;
                if (planId > 0)
                {
                    await _storagePlanRepository.UpdateStoragePlanStatus(new StoragePlanDO
                    {
                        Id = planId,
                        UpdateBy = identityService.GetUserName(),
                        Status = StorageTypeEnum.盘点中.ToString(),
                        UpdateType = 1
                    });

                    //await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                    //{
                    //    LogLevel = LogLevelEnum.Info.ToString(),
                    //    CreateBy = identityService.GetUserName(),
                    //    CreateTime = DateTime.Now,
                    //    ObjectId = planId,
                    //    ObjectType = ShopWmsLogEnum.Storage.ToString(),
                    //    Remark = $"创建【{productSourceType}】的【{ request.Type}】盘点任务"
                    //});
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdateStoragePlanProduct_Error", ex);
            }
            return res;
        }

        /// <summary>
        /// 盘点完成 更新状态
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateStoragePlanProductStatus(UpdateStoragePlanRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                long planId = 0;
                if (request.Products.Any())
                {
                    foreach (var item in request.Products)
                    {
                        await storagePlanProductRepository.UpdateProductStorageNum(new StoragePlanProductDO
                        {
                            Id = item.Id,
                            UpdateBy = identityService.GetUserName(),
                            Status = StorageTypeEnum.盘点中.ToString(),
                            StorageNum = item.StorageNum,
                            SystemNum = item.SystemNum,
                            DiffNum = item.StorageNum - item.SystemNum
                        });
                    }
                    planId = request.Products.FirstOrDefault().PlanId;
                }
                else
                {
                    planId = request.PlanId;
                }

                var planProducts = await storagePlanProductRepository.GetListAsync(" where plan_id =@planId and is_deleted =0", new { planId = planId });

                //判断是否还有新建状态的盘点产品
                //case:盘点产品有3页记录  只盘点了1页 就完成了

                if (planProducts.Any(r => r.Status == StorageTypeEnum.新建.ToString()))
                {
                    await _storagePlanRepository.UpdateStoragePlanStatus(new StoragePlanDO
                    {
                        Id = planId,
                        UpdateBy = identityService.GetUserName(),
                        Status = StorageTypeEnum.盘点中.ToString(),
                        UpdateType = 1
                    });

                    res.Code = ResultCode.Success;
                    res.Message = "还有待盘点产品!";
                }
                else
                {
                    var storageStatus = string.Empty;
                    var isFinish = false;
                    if (planProducts.Any(r => r.DiffNum != 0))
                    {
                        storageStatus = StorageTypeEnum.差异处理中.ToString();
                    }
                    else
                    {
                        storageStatus = StorageTypeEnum.盘点完成.ToString();
                        isFinish = true;
                    }

                    //更新盘库任务的状态
                    await _storagePlanRepository.UpdateStoragePlanStatus(new StoragePlanDO
                    {
                        Id = planId,
                        UpdateBy = identityService.GetUserName(),
                        Status = storageStatus,
                        UpdateType = isFinish ? 2 : 1
                    });

                    //判断哪些的差异数是大于0的
                    //更新盘库产品的状态
                    foreach (var item in planProducts)
                    {
                        if (item.DiffNum > 0 || item.DiffNum < 0)
                        {
                            await storagePlanProductRepository.UpdateStoragePlanStatus(new StoragePlanProductDO
                            {
                                Id = item.Id,
                                UpdateBy = identityService.GetUserName(),
                                Status = StorageTypeEnum.差异处理中.ToString()
                            });
                        }
                        else
                        {
                            await storagePlanProductRepository.UpdateStoragePlanStatus(new StoragePlanProductDO
                            {
                                Id = item.Id,
                                UpdateBy = identityService.GetUserName(),
                                Status = StorageTypeEnum.盘点完成.ToString()
                            });
                        }
                    }

                    await _shopWmsLogRepository.InsertAsync(new ShopWmsLogDO
                    {
                        LogLevel = LogLevelEnum.Info.ToString(),
                        CreateBy = identityService.GetUserName(),
                        CreateTime = DateTime.Now,
                        ObjectId = planId,
                        ObjectType = ShopWmsLogEnum.Storage.ToString(),
                        Remark = $"盘库任务【{planId}】 【{storageStatus}】"
                    });

                    res.Message = string.Empty;
                    res.Code = ResultCode.Success;
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdateStoragePlanProductStatus_Error", ex);
            }
            return res;
        }

        public async Task<ApiResult<StoragePlanDTO>> GetStoragePlan(StoragePlanDTO request)
        {
            var res = new ApiResult<StoragePlanDTO>();
            try
            {

                var planRes = await _storagePlanRepository.GetAsync(request.Id);
                var vo = _mapper.Map<StoragePlanDTO>(planRes);

                vo.PlanName = $"{vo.PlanNo} {vo.PlanName}";
                vo.SourceType = vo.SourceType == "Company" ? "平台商品" : "外采商品";
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetStoragePlan_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }


    }
}
