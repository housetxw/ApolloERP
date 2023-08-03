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
using Newtonsoft.Json;
using System.Linq;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Request;
using ApolloErp.Login.Auth;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Imp.Helpers;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Dal.Model.WMS;

namespace Ae.Shop.Api.Imp.Services
{
    public class TransferService : ITransferService
    {
        #region  
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<TransferService> _logger;
        private readonly IAllotTaskRepository _allotTaskRepository;
        private readonly IAllotProductRepository _allotProductRepository;
        private readonly IIdentityService _identityService;
        private readonly IStockManageService _stockManageService;
        private readonly IShopManageService _shopManageService;
        private readonly IShopMangeClient shopMangeClient;
        private readonly IWmsClient wmsClient;

        public TransferService(
            IMapper mapper,
            ApolloErpLogger<TransferService> logger,
            IAllotProductRepository allotProductRepository, IAllotTaskRepository allotTaskRepository,
                                  IStockManageService stockManageService, IWmsClient wmsClient,
                                  IShopManageService shopManageService, IShopMangeClient shopMangeClient,
            IIdentityService identityService
            )
        {
            this._mapper = mapper;
            this._logger = logger;
            this._allotTaskRepository = allotTaskRepository;
            this._allotProductRepository = allotProductRepository;
            this._identityService = identityService;
            this._stockManageService = stockManageService;
            this._shopManageService = shopManageService;
            this.shopMangeClient = shopMangeClient;
            this.wmsClient = wmsClient;
        }
        #endregion
        /// <summary>
        /// 创建调拨单，直接更新库存，无批次
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateAllotTaskAndUpdateStock(AllotTaskDTO request)
        {
            var res = new ApiResult<string>();
            string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? _identityService.GetUserName() : request.CreateBy;
            //var createBy = _identityService.GetUserName() ?? request.CreateBy;

            var shopInfo = await _stockManageService.GetShopById();
            request.LocationId = shopInfo?.ShopId ?? 0;
            request.LocationName = shopInfo?.SimpleName ?? "";

            if (request.SourceWarehouse <= 0)
            {
                request.SourceWarehouse = shopInfo?.ShopId ?? 0;
                request.SourceWarehouseName = shopInfo?.SimpleName ?? "";
            }
            else if (string.IsNullOrWhiteSpace(request.SourceWarehouseName))
            {
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = request.SourceWarehouse
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    request.SourceWarehouseName = shopResult.Data.SimpleName ?? "";
                }

            }

            try
            {
                var allotTask = new AllotTaskDO
                {
                    LocationId = request.LocationId,
                    LocationName = request.LocationName,
                    IsAudit = 1,
                    AuditUser = "SYS",
                    AuditRemark = "自动审核",
                    AuditTime = DateTime.Now,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    Operator = request.Operator,
                    OperatorTime = request.OperatorTime,
                    Remark = request.Remark,
                    TaskStatus = AllotTaskStatusEnum.全部出库.ToString(),
                    SourceWarehouse = request.SourceWarehouse,
                    SourceWarehouseName = request.SourceWarehouseName,
                    TargetWarehouse = request.TargetWarehouse,
                    TargetWarehouseName = request.TargetWarehouseName,
                    SourceType = request.SourceType,
                    TargetType = request.TargetType
                };
                var taskResult = await _allotTaskRepository.InsertAsync(allotTask);

                var taskNo = "DBD" + taskResult.ToString().PadLeft(5, '0');
                await _allotTaskRepository.UpdateAllotTaskNo(new AllotTaskDO
                {
                    Id = Convert.ToInt64(taskResult),
                    TaskNo = taskNo
                });


                if (request.Products.Any())
                {
                    //组装库存更新数据
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = request.TargetWarehouse,
                        LocationName = request.TargetWarehouseName,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceInventoryNo = taskResult.ToString(),
                        SourceType = StockInTypeEnum.调拨入库.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = $"从{request.SourceWarehouseName}调入"
                    };
                    foreach (var item in request.Products)
                    {
                        if (item.Num <= 0)
                        {
                            continue;
                        }
                        var allotProduct = await _allotProductRepository.InsertAsync(new AllotProductDO
                        {
                            TaskId = Convert.ToInt64(taskResult),
                            IsDeleted = 0,
                            BatchId = item.BatchId,
                            CostPrice = item.CostPrice,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            Num = item.Num,
                            OwnerId = item.OwnerId,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Remark = item.Remark,
                            Unit = item.Unit,
                            RefBatchId = item.RefBatchId,
                            StockId = item.StockId
                        });

                        stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ReleationId = Convert.ToInt64(allotProduct),
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Num = item.Num,
                            GoodNum = item.Num,
                            ActualNum = item.Num,
                            UomText = item.Unit,
                            Cost = item.CostPrice,
                            CreateBy = createBy,
                            Status = StockInOutStatusEnum.已收货.ToString()
                        });


                    }
                    //更新目标门店入库
                    await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                    stockInoutRequest.LocationId = request.SourceWarehouse;
                    stockInoutRequest.LocationName = request.SourceWarehouseName;
                    stockInoutRequest.OperationType = StockOperateTypeEnum.出库.ToString();
                    stockInoutRequest.SourceInventoryNo = taskResult.ToString();
                    stockInoutRequest.SourceType = StockOutTypeEnum.调拨出库.ToString();
                    stockInoutRequest.Status = StockInOutStatusEnum.已出库.ToString();
                    stockInoutRequest.CreateBy = createBy;
                    stockInoutRequest.Operator = createBy;
                    stockInoutRequest.OperateTime = DateTime.Now;
                    stockInoutRequest.Remark = $"调出到{request.TargetWarehouseName}";

                    //更新原门店出库
                    await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                }


                res.Data = taskResult.ToString();

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateAllotTaskAndUpdateStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> CreateAllotTask(AllotTaskDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //var createBy = _identityService.GetUserName() ?? request.CreateBy;
                string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? _identityService.GetUserName() : request.CreateBy;
                if (request.SourceWarehouse <= 0 )
                {
                    var shopInfo = await _stockManageService.GetShopById();
                    request.SourceWarehouse = shopInfo?.ShopId ?? 0;
                    request.SourceWarehouseName = shopInfo?.SimpleName ?? "";
                }
                else if(string.IsNullOrWhiteSpace(request.SourceWarehouseName))
                {
                    var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                    {
                        ShopId = request.SourceWarehouse
                    });
                    if (shopResult.Code == ResultCode.Success)
                    {
                        request.SourceWarehouseName = shopResult.Data.SimpleName ?? "";
                    }
                  
                }
                //request.SourceType = "Shop";
                //request.TargetType = "Shop";

                var allotTask = new AllotTaskDO
                {
                    IsAudit = 1,
                    AuditUser = "SYS",
                    AuditRemark = "自动审核",
                    AuditTime = DateTime.Now,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    Operator = request.Operator,
                    OperatorTime = request.OperatorTime,
                    Remark = request.Remark,
                    TaskStatus = AllotTaskStatusEnum.全部出库.ToString(),
                    SourceWarehouse = request.SourceWarehouse,
                    SourceWarehouseName = request.SourceWarehouseName,
                    TargetWarehouse = request.TargetWarehouse,
                    TargetWarehouseName = request.TargetWarehouseName,
                    SourceType = request.SourceType,
                    TargetType = request.TargetType
                };
                var taskResult = await _allotTaskRepository.InsertAsync(allotTask);

                var taskNo = "DBD" + taskResult.ToString().PadLeft(6, '0');
                await _allotTaskRepository.UpdateAllotTaskNo(new AllotTaskDO
                {
                    Id = Convert.ToInt64(taskResult),
                    TaskNo = taskNo
                });


                if (request.Products.Any())
                {
                    //组装库存更新数据
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = request.TargetWarehouse,
                        LocationName = request.TargetWarehouseName,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceInventoryNo = taskResult.ToString(),
                        SourceType = StockInTypeEnum.调拨入库.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = $"从{request.SourceWarehouseName}调入"
                    };
                    foreach (var item in request.Products)
                    {
                        if (item.Num <= 0)
                        {
                            continue;
                        }
                        var allotProduct = await _allotProductRepository.InsertAsync(new AllotProductDO
                        {
                            TaskId = Convert.ToInt64(taskResult),
                            IsDeleted = 0,
                            BatchId = item.BatchId,
                            CostPrice = item.CostPrice,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            Num = item.Num,
                            OwnerId = item.OwnerId,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Remark = item.Remark,
                            Unit = item.Unit,
                            RefBatchId = item.RefBatchId,
                            StockId = item.StockId
                        });

                        stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ReleationId = Convert.ToInt64(allotProduct),
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Num = item.Num,
                            GoodNum = item.Num,
                            ActualNum = item.Num,
                            UomText = item.Unit,
                            Cost = item.CostPrice,
                            CreateBy = createBy,
                            Status = StockInOutStatusEnum.已收货.ToString()
                        });


                    }
                    //更新目标门店入库
                    await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                    stockInoutRequest.LocationId = request.SourceWarehouse;
                    stockInoutRequest.LocationName = request.SourceWarehouseName;
                    stockInoutRequest.OperationType = StockOperateTypeEnum.出库.ToString();
                    stockInoutRequest.SourceInventoryNo = taskResult.ToString();
                    stockInoutRequest.SourceType = StockOutTypeEnum.调拨出库.ToString();
                    stockInoutRequest.Status = StockInOutStatusEnum.已出库.ToString();
                    stockInoutRequest.CreateBy = createBy;
                    stockInoutRequest.Operator = createBy;
                    stockInoutRequest.OperateTime = DateTime.Now;
                    stockInoutRequest.Remark = $"调出到{request.TargetWarehouseName}";

                    //更新原门店出库
                    await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                }


                res.Data = taskResult.ToString();

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiPagedResult<AllotTaskDTO>> GetAllotPageData(AllotPageRequest request)
        {
            _logger.Info(JsonConvert.SerializeObject(request));
            var res = new ApiPagedResult<AllotTaskDTO>();
            request.ShopId = Convert.ToInt64(_identityService.GetOrganizationId());
            try
            {
                if (request.Times != null && request.Times.Any())
                {
                    request.StartTime = Convert.ToDateTime(request.Times[0]);
                    request.StartTime = Convert.ToDateTime(request.Times[1]);
                }
                if (!string.IsNullOrWhiteSpace(request.SourceWarehouse))
                {
                    var source = request.SourceWarehouse;
                    int index = source.IndexOf('-');
                    request.SourceWarehouse = source.Substring(0, index);
                }

                if (!string.IsNullOrWhiteSpace(request.TargetWarehouse))
                {
                    var target = request.TargetWarehouse;
                    int index = target.IndexOf('-');
                    request.TargetWarehouse = target.Substring(0, index);
                }
                //_logger.Info(JsonConvert.SerializeObject(request));
                var result = await _allotTaskRepository.GetAllotPageData(request);
                var vo = _mapper.Map<ApiPagedResultData<AllotTaskDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetAllotPageData_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        public async Task<ApiResult<AllotTaskDTO>> GetAllotTaskDetail(AllotPageRequest request)
        {
            var res = new ApiResult<AllotTaskDTO>();
            try
            {
                var task = await _allotTaskRepository.GetListAsync(" where id =@id", new { id = request.TaskId });
                var taskVo = _mapper.Map<AllotTaskDTO>(task.FirstOrDefault());

                var products = await _allotProductRepository.GetListAsync(" where task_id=@task_id and is_deleted=0", new { task_id = request.TaskId });
                var productsVo = _mapper.Map<List<AllotProductDTO>>(products);

                taskVo.Products = productsVo;

                res.Data = taskVo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                //_logger.Error($"GetAllotTaskDetail_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<BasicInfoDTO>>> GetAllotTaskStatus()
        {
            var result = await EnumHelper.GetBasicInfoDTOs<AllotTaskStatusEnum>();
            return new ApiResult<List<BasicInfoDTO>>
            {
                Data = result,
                Code = ResultCode.Success
            };
        }


        //public async Task<ApiResult<string>> UpdateAllotTaskStatus(AllotTaskDTO request)
        //{
        //    var res = new ApiResult<string>();
        //    try
        //    {
        //        await _allotTaskRepository.UpdateAllotTaskStatus(new AllotTaskDO
        //        {
        //            TaskNo = request.TaskNo,
        //            UpdateBy = request.UpdateBy,
        //            TaskStatus = request.TaskStatus
        //        });

        //        var allotTaskRes = await _allotTaskRepository.GetListAsync(" where task_no=@task_no", new { task_no = request.TaskNo });

        //        var allotTask = allotTaskRes.FirstOrDefault();

        //        await _asnRepository.CreateWmsLog(new WmsLogDO
        //        {
        //            LogLevel = LogLevelEnum.Info.ToString(),
        //            CreateBy = request.UpdateBy,
        //            CreateTime = DateTime.Now,
        //            ObjectId = allotTask.Id,
        //            ObjectType = WMSLogType.AllotTask.ToString(),
        //            Remark = $"调拨任务【{allotTask.TaskNo}】{request.TaskStatus}"
        //        });

        //        res.Code = ResultCode.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Code = ResultCode.Exception;
        //        _logger.Error($"UpdateAllotTaskStatus_Error Data:{JsonConvert.SerializeObject(request)}", ex);
        //        throw;
        //    }
        //    return res;
        //}

        /// <summary>
        /// 获取源仓和门店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GroupSelectDTO>>> GetSourceWarehouses(GetShopInfoRequest request)
        {
            var res = new ApiResult<List<GroupSelectDTO>>();
            try
            {
                var groups = new List<GroupSelectDTO>();

                var orgType = _identityService.GetOrgType();
                if (orgType == "0") //公司
                {
                    long companyId = Convert.ToInt64(_identityService.GetOrganizationId());
            
                    var resultWareHouse = await shopMangeClient.GetShopWareHouseListAsync(new GetShopListRequest() { CompanyId = companyId });
                    _logger.Info($"SVC:{JsonConvert.SerializeObject(resultWareHouse)}");


                    if (resultWareHouse.Code == ResultCode.Success)
                    {
                        if (resultWareHouse.Data != null && resultWareHouse.Data.Any())
                        {
                            var selectDTO = new GroupSelectDTO
                            {
                                Label = "---仓库---"
                            };
                            var basicLists = new List<BasicInfoDTO>();
                            foreach (var item in resultWareHouse.Data)
                            {
                                basicLists.Add(new BasicInfoDTO
                                {
                                    Key = item.Id.ToString() + $"-{OwnerType.Warehouse.ToString()}",
                                    Value = item.SimpleName
                                });
                            }
                            selectDTO.Options = basicLists;
                            groups.Add(selectDTO);
                        }
                    }

                }

                var result = await _shopManageService.GetCompanyShopList(new GetShopListRequest() { SimpleName = request.SimpleName, ShopTypes = new List<int> { 2 } });
                _logger.Info($"SVC:{JsonConvert.SerializeObject(result)}");

                if (result.Code == ResultCode.Success)
                {
                    if (result.Data.Items != null && result.Data.Items.Any())
                    {
                        var selectDTO = new GroupSelectDTO
                        {
                            Label = "---门店---"
                        };
                        var basicLists = new List<BasicInfoDTO>();
                        foreach (var item in result.Data.Items)
                        {
                            basicLists.Add(new BasicInfoDTO
                            {
                                Key = item.Id.ToString() + $"-{OwnerType.Shop.ToString()}",
                                Value = item.SimpleName
                            });
                        }
                        selectDTO.Options = basicLists;
                        groups.Add(selectDTO);
                    }
                }
                
                res.Data = groups;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetSourceWarehouses_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
    }
}
