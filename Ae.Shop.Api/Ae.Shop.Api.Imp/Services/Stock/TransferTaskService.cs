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
using Ae.Shop.Api.Dal.Model;
using System.Linq;
using Ae.Shop.Api.Core.Enums;
using System.Transactions;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Client.Clients.Product;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Api.Core.Request.ShopWms;
using Ae.Shop.Api.Dal.Repositorys.Company;

namespace Ae.Shop.Api.Imp.Services
{
    public class TransferTaskService : ITransferTaskService
    {
        #region
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<TransferTaskService> _logger;
        private readonly ITransferTaskRepository _transferTaskRepository;
        private readonly IIdentityService _identityService;
        private readonly IInventoryExceptionFileRepository _inventoryExceptionFileRepository;
        private readonly IInventoryExceptionRecordRepository _inventoryExceptionRecordRepository;
        private readonly IInventoryPackageRecordRepository _inventoryPackageRecordRepository;
        private readonly IStockManageService _stockManageService;
        private readonly IStockInoutRepository _stockInoutRepository;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly IReceiveRepository _receiveRepository;
        private readonly IProductPackageRelationRepository _productPackageRelationRepository;
        private readonly IWmsClient _wmsClient;
        private readonly IProductClient _productClient;
        private readonly IConfiguration _configuration;
        private readonly ICompanyRepository _companyRepository;



        public TransferTaskService(IMapper mapper, ApolloErpLogger<TransferTaskService> logger,
            ITransferTaskRepository transferTaskRepository,
            IIdentityService identityService, IInventoryExceptionFileRepository inventoryExceptionFileRepository,
            IInventoryExceptionRecordRepository inventoryExceptionRecordRepository,
            IInventoryPackageRecordRepository inventoryPackageRecordRepository,
            IStockManageService stockManageService, IStockInoutRepository stockInoutRepository,
            IShopMangeClient shopMangeClient, IReceiveRepository receiveRepository,
            IProductPackageRelationRepository productPackageRelationRepository,
            IWmsClient wmsClient, IProductClient productClient,
            IConfiguration configuration, ICompanyRepository companyRepository)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._transferTaskRepository = transferTaskRepository;
            this._identityService = identityService;
            this._inventoryExceptionFileRepository = inventoryExceptionFileRepository;
            this._inventoryExceptionRecordRepository = inventoryExceptionRecordRepository;
            this._inventoryPackageRecordRepository = inventoryPackageRecordRepository;
            this._stockManageService = stockManageService;
            this._stockInoutRepository = stockInoutRepository;
            this._shopMangeClient = shopMangeClient;
            this._receiveRepository = receiveRepository;
            this._productPackageRelationRepository = productPackageRelationRepository;
            this._wmsClient = wmsClient;
            this._productClient = productClient;
            this._configuration = configuration;
            this._companyRepository = companyRepository;
        }
        #endregion


        /// <summary>
        /// 清点入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CheckInStock(CheckInStockRequest request)
        {
        TODO: //InventoryPackageRecord 记录每个包裹里面收货产品明细
              //InventoryExceptionRecord 记录每个包裹里面收货异常产品明细  这块数据后续会特殊处理 因此分开处理
              //按照包裹收货后 需要做拆分判断

            var res = new ApiResult<string>();
            try
            {
                _logger.Info($"CheckInStock 门店{request.ShopId}包裹清点 Data:{JsonConvert.SerializeObject(request)}");

                #region 
                //可能传入的包裹单号 并非订单/铺货所有的
                //因此判断签收状态时需要Check
                if (request.PackageProducts.Any())
                {
                    var packageNos = request.PackageProducts.Select(r => r.PackageNo).ToList();

                    var packageProduct = request.PackageProducts.First();
                    var packageRes = await _transferTaskRepository.GetListAsync(" where delivery_code =@delivery_code", new
                    {
                        delivery_code = packageProduct.PackageNo
                    });

                    var shopInfoRes = await _shopMangeClient.GetShopById(new GetShopClientRequest { ShopId = request.ShopId });
                    ShopDTO shopInfo = null;
                    if (shopInfoRes.Code == ResultCode.Success)
                    {
                        shopInfo = shopInfoRes.Data;
                    }

                    WarehouseTransferPackageDO packageInfo = null;
                    if (packageRes.Any())
                    {
                        packageInfo = packageRes.First();
                        var warehouseTransferProductRequest = new UpdateWarehouseTransferProductRequest
                        {
                            TransferId = packageInfo.TransferId,
                            TransferType = packageInfo.TransferType,
                            UpdateBy = request.Operator
                        };

                        //清点包裹下的所有记录
                        var checkPackageProducts = await _productPackageRelationRepository.GetListAsync(" where 1=1 and is_deleted=0 and package_no in @packageNos", new { packageNos = packageNos });

                        var shopSimpleInfo = new ShopSimpleDTO
                        {
                            ShopId = request.ShopId,
                            SimpleName = shopInfo != null ? shopInfo.SimpleName : string.Empty
                        };

                        //目前只有一个包裹
                        foreach (var packageProductItem in request.PackageProducts)
                        {
                            //获取这个包裹下的所有商品
                            var inPackageProducts = checkPackageProducts.Where(r => r.PackageNo == packageProductItem.PackageNo);

                            //如果包裹下产品有货损的 入库状态为 部分收货
                            string status = packageProductItem.Products.Any(r => r.IsDamage) ? StockInOutStatusEnum.部分收货.ToString() :
                                StockInOutStatusEnum.已收货.ToString();

                            //以每个包裹为单位 生成出库任务
                            var stockId = await _stockInoutRepository.InsertAsync(new StockInoutDO
                            {
                                CreateBy = request.Operator,
                                IsDeleted = 0,
                                CreateTime = DateTime.Now,
                                LocationId = request.ShopId,
                                LocationName = shopInfo != null ? shopInfo.SimpleName : string.Empty, //TODO
                                OperateTime = DateTime.Now,
                                OperationType = StockOperateTypeEnum.入库.ToString(),
                                Operator = string.Empty,
                                SourceInventoryNo = packageInfo.TransferId, //如果是空的 需要手动生成
                                SourceType = StockInTypeEnum.铺货入库.ToString(),
                                Status = status  //判断有无差异数目
                            });

                            //包裹下的每个产品
                            foreach (var productItem in packageProductItem.Products)
                            {
                                string productName = inPackageProducts.FirstOrDefault(r => r.ProductId == productItem.ProductId)?.ProductName;

                                //这个包裹下 该产品的所有记录
                                var packageSameProducts = inPackageProducts.Where(r => r.ProductId == productItem.ProductId);

                                int sumDamageNum = productItem.DamageNum;

                                int sumActualNum = productItem.ActualNum;

                                //原始数量
                                int orginNum = packageSameProducts.Sum(r => r.Num);

                                #region  良品做记录
                                if (sumActualNum > orginNum)
                                {
                                    // 多的入差异
                                    var recordResult = await _inventoryExceptionRecordRepository.InsertAsync(new InventoryExceptionRecordDO
                                    {
                                        CreateTime = DateTime.Now,
                                        IsDeleted = 0,
                                        CreateBy = request.Operator,
                                        ExceptionType = ExceptionFileTypeEnum.多发.ToString(),
                                        Num = sumActualNum - orginNum,
                                        PackageNo = packageProductItem.PackageNo,
                                        ProductId = productItem.ProductId,
                                        ProductName = productName,
                                        Status = ExceptionRecordStatusEnum.新建.ToString(),
                                        Remark = $"包裹号:{packageProductItem.PackageNo}的产品{productItem.ProductId}应收:{orginNum},实收:{sumActualNum}",
                                        TransferType = packageInfo.TransferType,
                                        TransferId = packageInfo.TransferId,
                                        TransferProductId = 0
                                    });
                                    //把商品全部入进去

                                    //将无异常的商品做入库
                                    await _inventoryPackageRecordRepository.InsertAsync(new InventoryPackageRecordDO
                                    {
                                        IsDeleted = 0,
                                        BadNum = sumDamageNum,
                                        CreateBy = request.Operator,
                                        CreateTime = DateTime.Now,
                                        Num = orginNum,
                                        PackageNo = packageProductItem.PackageNo,
                                        ProductId = productItem.ProductId,
                                        ProductName = productName,
                                        TransferId = packageInfo.TransferId,
                                        TransferProductId = 0,
                                        TransferType = packageInfo.TransferType,
                                        Remark = $"包裹号:{packageProductItem.PackageNo}的产品{productItem.ProductId}应收:{orginNum},实收:{sumActualNum}",
                                    });

                                    foreach (var item in packageSameProducts)
                                    {
                                        await _stockManageService.TransferSyncInventoryData(item, shopSimpleInfo, new ProductTransferDTO
                                        {
                                            InOutId = stockId,
                                            IsDamage = false,
                                            ActualNum = item.Num,
                                            CreateBy = request.Operator
                                        });

                                        //回写出库产品表收货数量
                                        await _transferTaskRepository.UpdateProductPackageRelation(new ProductPackageRelationDO
                                        {
                                            UpdateBy = request.Operator,
                                            ReceiveNum = item.Num,
                                            TransferProductId = item.TransferProductId,
                                            PackageNo = packageProductItem.PackageNo
                                        });
                                        //回写包裹产品表收货数量
                                        //await _transferTaskRepository.UpdateWarehouseTransferProduct(new WarehouseTransferProductDO
                                        //{
                                        //    UpdateBy = request.Operator,
                                        //    Id = item.TransferProductId,
                                        //    ReceiveNum = item.Num
                                        //});

                                        warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                                        {
                                            TransferProductId = item.TransferProductId,
                                            ReceiveNum = item.Num
                                        });
                                    }

                                }
                                else if (sumActualNum < orginNum)
                                {
                                    //分批一点一点的入
                                    var newOrginNum = sumActualNum;

                                    //按照后入先出的规则入到门店
                                    foreach (var item in packageSameProducts.OrderByDescending(r => r.BatchId))
                                    {
                                        var transferProductRes = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                                        {
                                            Id = item.TransferProductId
                                        });
                                        var transferProduct = transferProductRes.FirstOrDefault();

                                        if (newOrginNum > 0)
                                        {
                                            if (item.Num > newOrginNum)
                                            {
                                                //只能入 newOrginNum 多的库存
                                                //商品入库
                                                await _stockManageService.TransferSyncInventoryData(transferProduct, shopSimpleInfo, new ProductTransferDTO
                                                {
                                                    InOutId = stockId,
                                                    IsDamage = true,
                                                    ActualNum = newOrginNum,
                                                    CreateBy = request.Operator
                                                });

                                                //回写出库产品表收货数量
                                                await _transferTaskRepository.UpdateProductPackageRelation(new ProductPackageRelationDO
                                                {
                                                    UpdateBy = request.Operator,
                                                    ReceiveNum = newOrginNum,
                                                    TransferProductId = item.TransferProductId,
                                                    PackageNo = packageProductItem.PackageNo
                                                });
                                                //回写包裹产品表收货数量
                                                //await _transferTaskRepository.UpdateWarehouseTransferProduct(new WarehouseTransferProductDO
                                                //{
                                                //    UpdateBy = request.Operator,
                                                //    Id = item.TransferProductId,
                                                //    ReceiveNum = newOrginNum
                                                //});

                                                warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                                                {
                                                    TransferProductId = item.TransferProductId,
                                                    ReceiveNum = item.Num
                                                });
                                                break;
                                            }
                                            else
                                            {
                                                newOrginNum -= item.Num;
                                                await _stockManageService.TransferSyncInventoryData(transferProduct, shopSimpleInfo, new ProductTransferDTO
                                                {
                                                    InOutId = stockId,
                                                    IsDamage = false,
                                                    ActualNum = item.Num,
                                                    CreateBy = request.Operator
                                                });

                                                //回写出库产品表收货数量
                                                await _transferTaskRepository.UpdateProductPackageRelation(new ProductPackageRelationDO
                                                {
                                                    UpdateBy = request.Operator,
                                                    ReceiveNum = item.Num,
                                                    TransferProductId = item.TransferProductId,
                                                    PackageNo = packageProductItem.PackageNo
                                                });
                                                //回写包裹产品表收货数量
                                                //await _transferTaskRepository.UpdateWarehouseTransferProduct(new WarehouseTransferProductDO
                                                //{
                                                //    UpdateBy = request.Operator,
                                                //    Id = item.TransferProductId,
                                                //    ReceiveNum = item.Num
                                                //});

                                                warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                                                {
                                                    TransferProductId = item.TransferProductId,
                                                    ReceiveNum = item.Num
                                                });

                                                continue; ;
                                            }
                                        }
                                    }

                                    //记录漏发的差异
                                    var recordResult = await _inventoryExceptionRecordRepository.InsertAsync(new InventoryExceptionRecordDO
                                    {
                                        CreateTime = DateTime.Now,
                                        IsDeleted = 0,
                                        CreateBy = request.Operator,
                                        ExceptionType = ExceptionFileTypeEnum.漏发.ToString(),
                                        Num = orginNum - sumActualNum,
                                        PackageNo = packageProductItem.PackageNo,
                                        ProductId = productItem.ProductId,
                                        ProductName = productName,
                                        Status = ExceptionRecordStatusEnum.新建.ToString(),
                                        Remark = $"包裹号:{packageProductItem.PackageNo}的产品{productItem.ProductId}应收:{orginNum},实收:{sumActualNum}",
                                        TransferType = packageInfo.TransferType,
                                        TransferId = packageInfo.TransferId,
                                        TransferProductId = 0
                                    });
                                }
                                else
                                {
                                    //实收数量=入库数
                                    await _inventoryPackageRecordRepository.InsertAsync(new InventoryPackageRecordDO
                                    {
                                        IsDeleted = 0,
                                        BadNum = 0,
                                        CreateBy = request.Operator,
                                        CreateTime = DateTime.Now,
                                        Num = orginNum,
                                        PackageNo = packageProductItem.PackageNo,
                                        ProductId = productItem.ProductId,
                                        ProductName = productName,
                                        TransferId = packageInfo.TransferId,
                                        TransferProductId = 0,
                                        TransferType = packageInfo.TransferType,
                                        Remark = $"包裹号:{packageProductItem.PackageNo}的产品{productItem.ProductId}应收:{orginNum},实收:{sumActualNum}",
                                    });

                                    foreach (var item in packageSameProducts)
                                    {
                                        var transferProductRes = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                                        {
                                            Id = item.TransferProductId
                                        });
                                        var transferProduct = transferProductRes.FirstOrDefault();

                                        await _stockManageService.TransferSyncInventoryData(transferProduct, shopSimpleInfo, new ProductTransferDTO
                                        {
                                            InOutId = stockId,
                                            IsDamage = false,
                                            ActualNum = item.Num,
                                            CreateBy = request.Operator
                                        });

                                        //回写出库产品表收货数量
                                        await _transferTaskRepository.UpdateProductPackageRelation(new ProductPackageRelationDO
                                        {
                                            UpdateBy = request.Operator,
                                            ReceiveNum = item.Num,
                                            TransferProductId = item.TransferProductId,
                                            PackageNo = packageProductItem.PackageNo
                                        });
                                        //回写包裹产品表收货数量
                                        //await _transferTaskRepository.UpdateWarehouseTransferProduct(new WarehouseTransferProductDO
                                        //{
                                        //    UpdateBy = request.Operator,
                                        //    Id = item.TransferProductId,
                                        //    ReceiveNum = item.Num
                                        //});
                                        warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                                        {
                                            TransferProductId = item.TransferProductId,
                                            ReceiveNum = item.Num
                                        });
                                    }

                                }
                                #endregion

                                #region 处理货损商品

                                if (productItem.IsDamage)
                                {
                                    // 新增货损记录
                                    var recordResult = await _inventoryExceptionRecordRepository.InsertAsync(new InventoryExceptionRecordDO
                                    {
                                        CreateTime = DateTime.Now,
                                        IsDeleted = 0,
                                        CreateBy = request.Operator,
                                        ExceptionType = ExceptionFileTypeEnum.货损.ToString(),
                                        Num = productItem.DamageNum,
                                        PackageNo = packageProductItem.PackageNo,
                                        ProductId = productItem.ProductId,
                                        ProductName = productName,
                                        Status = ExceptionRecordStatusEnum.新建.ToString(),
                                        Remark = productItem.DamageRemark,
                                        TransferType = packageInfo.TransferType,
                                        TransferId = packageInfo.TransferId,
                                        TransferProductId = 0
                                    });

                                    if (productItem.FileList.Any())
                                    {
                                        productItem.FileList.ForEach(async k =>
                                        {
                                            await _inventoryExceptionFileRepository.InsertAsync(new InventoryExceptionFileDO
                                            {
                                                CreateBy = request.Operator,
                                                CreateTime = DateTime.Now,
                                                FileName = string.Empty,
                                                FileUrl = k,
                                                IsActive = 1,
                                                RelationId = recordResult,
                                                RelationType = ExceptionFileTypeEnum.货损.ToString()
                                            });
                                        });
                                    }
                                }
                                #endregion
                            }
                        }

                        //回写包裹表状态
                        await _transferTaskRepository.UpdatePackageStatus(new WarehouseTransferPackageDO
                        {
                            UpdateBy = request.Operator,
                            Status = PackageStatusEnum.已清点.ToString(),
                            PackageNos = packageNos
                        });

                        //回写包裹产品表状态
                        await _transferTaskRepository.UpdateProductPackageRelationStatus(new ProductPackageRelationDO
                        {
                            Status = PackageStatusEnum.已清点.ToString(),
                            PackageNos = packageNos,
                            UpdateBy = request.Operator
                        });

                        //回写出库主表状态
                        var allPackages = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                        {
                            TransferId = packageInfo.TransferId,
                            TransferType = packageInfo.TransferType
                        });

                        //如果铺货的包裹全部都清掉完成
                        if (allPackages.All(r => r.Status == PackageStatusEnum.已清点.ToString()))
                        {
                            await _transferTaskRepository.UpdateWarehouseTransferStatus(new WarehouseTransferTaskDO
                            {
                                TransferId = packageInfo.TransferId,
                                TransferType = packageInfo.TransferType,
                                TaskStatus = "已收货",
                                UpdateBy = request.Operator
                            });
                        }
                        if (warehouseTransferProductRequest.TransferType == SignType.铺货.ToString())
                        {
                            if (warehouseTransferProductRequest.TransferList.Any())
                            {
                                await _wmsClient.UpdateWarehouseTransferProductReceiveNum(warehouseTransferProductRequest);
                            }
                        }
                    }

                    res.Code = ResultCode.Success;
                }
                else
                {
                    res.Code = ResultCode.Exception;
                    res.Message = "请选择需要清点的包裹!";
                }
                #endregion
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CheckInStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        /// <summary>
        /// 获取今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TodaySignPackageApiPagedResult<GetTodayReceivePackageDTO>> GetTodayReceivePackage(GetTodayReceivePackageRequest request)
        {
            try
            {
                var signPackages = await _receiveRepository.GetTodaySignList(request);

                var packageVo = _mapper.Map<TodaySignPackageApiPagedResult<GetTodayReceivePackageDTO>>(signPackages);

                if (packageVo.Items != null && packageVo.Items.Any())
                {
                    packageVo.TitleNum = packageVo.TotalItems;

                    var packages = packageVo.Items;

                    //需要判断签收的包裹是否已经清点
                    var orginPackages = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                    {
                        Status = PackageStatusEnum.已清点.ToString(),
                        PackageNos = packages.Select(r => r.PackageNo).ToList()
                    });
                    foreach (var item in packages)
                    {
                        var status = string.Empty;
                        if (item.RelationNo.IndexOf("RGC") != -1)
                        {
                            item.Status = PackageStatusEnum.已签收.ToString();
                            item.IsShow = false;
                        }
                        else if (item.RelationNo.IndexOf("PHD") != -1)
                        {
                            var orginPackage = orginPackages.FirstOrDefault(r => r.DeliveryCode == item.PackageNo);

                            status = orginPackage != null ? "-" + PackageStatusEnum.已清点.ToString() :
                                "-" + PackageStatusEnum.未清点.ToString();

                            item.Status = PackageStatusEnum.已签收.ToString() + status;
                            item.IsShow = orginPackage != null ? false : true;
                        }
                    }
                }
                return packageVo;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetTodayReceivePackage_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return null;
        }


        /// <summary>
        /// 签收并清点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ReceivePackageDTO>> SignInStock(SignInStockRequest request)
        {
            var res = new ApiResult<ReceivePackageDTO>();
            try
            {
                if (!request.IsOnlyInventory)
                {
                    //先把这几个包裹做签收  需要更新包裹表的状态
                    var signList = await _receiveRepository.GetSignList(request.PackageNos, request.ShopId);
                    if (signList.Any())
                    {
                        var codeMessage = new StringBuilder();
                        signList?.Select(x => x.PackageNo)?.ToList()?.ForEach(x =>
                        {
                            codeMessage.AppendLine($"{x} {CommonConstant.PackageHaveSign}");
                        });

                        return new ApiResult<ReceivePackageDTO>()
                        {
                            Code = ResultCode.Failed,
                            Message = codeMessage.ToString()
                        };
                    }
                    else
                    {
                        //获取该包裹关联的所有的包裹数据
                        var packagesRes = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                        {
                            PackageNos = request.PackageNos
                        });

                        if (packagesRes.Any())
                        {
                            var packageItem = packagesRes.FirstOrDefault();

                            //获取所有的包裹
                            var allPackages = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                            {
                                TransferId = packageItem.TransferId,
                                TransferType = packageItem.TransferType
                            });

                            var transferTask = await _transferTaskRepository.GetWarehouseTransferTask(new WarehouseTransferTaskDO
                            {
                                TransferId = packageItem.TransferId,
                                TransferType = packageItem.TransferType
                            });


                            if (allPackages.Any())
                            {
                                var notExistPackage = new List<string>();

                                request.PackageNos?.ForEach(x =>
                                {
                                    var packageNoObj = allPackages
                                        ?.Where(_ => _.DeliveryCode.Trim() == x.Trim())?.ToList();
                                    if (packageNoObj == null || !packageNoObj.Any())
                                    {
                                        notExistPackage.Add(x);
                                    }
                                });

                                if (notExistPackage.Any())
                                {
                                    var codeMessage = new StringBuilder();
                                    notExistPackage?.ForEach(x =>
                                    {
                                        codeMessage.AppendLine($"{x} 包裹号不存在;");
                                    });
                                    return new ApiResult<ReceivePackageDTO>()
                                    {
                                        Code = ResultCode.Failed,
                                        Message = codeMessage.ToString()
                                    };
                                }
                                if (request.ShopId != transferTask.TargetWarehouse)
                                {
                                    return new ApiResult<ReceivePackageDTO>()
                                    {
                                        Code = ResultCode.Failed,
                                        Message = CommonConstant.PackageIsNotShop
                                    };
                                }
                                var packageNos = allPackages.Select(x => x.DeliveryCode) ?? new List<string>();


                                var signDO = new SignDO()
                                {
                                    RelationNo = packageItem.TransferId,
                                    SourceType = GetSignType(packageItem.TransferType),
                                    Num = allPackages?.Count ?? 0,
                                    HaveSignNum = request.PackageNos.Count,
                                    SignStatus = 2, //包裹部分签收 
                                    ShopId = request.ShopId,
                                    PackageNos = String.Join("、", packageNos),
                                    Remark = string.Empty,
                                    CreateBy = request.UpdateBy,
                                    CreateTime = DateTime.Now,
                                    UpdateBy = request.UpdateBy,
                                    UpdateTime = DateTime.Now
                                };

                                var signDetails = new List<SignDetailDO>();

                                allPackages?.ForEach(item =>
                                {
                                    if (request.PackageNos.Contains(item.DeliveryCode))
                                    {
                                        signDetails.Add(new SignDetailDO()
                                        {
                                            RelationNo = packageItem.TransferId,
                                            PackageNo = item.DeliveryCode,
                                            SignUser = request.UpdateBy,
                                            SignTime = DateTime.Now,
                                            ShopId = request.ShopId,
                                            Remark = string.Empty,
                                            CreateBy = request.UpdateBy,
                                            CreateTime = DateTime.Now,
                                            UpdateBy = request.UpdateBy,
                                            UpdateTime = DateTime.Now
                                        });
                                    }

                                });

                                await _receiveRepository.SaveSign(signDO, signDetails);

                                await _transferTaskRepository.UpdatePackageStatus(new WarehouseTransferPackageDO
                                {
                                    Status = PackageStatusEnum.已签收.ToString(),
                                    PackageNos = request.PackageNos,
                                    UpdateBy = request.UpdateBy
                                });

                                await _transferTaskRepository.UpdateProductPackageRelationStatus(new ProductPackageRelationDO
                                {
                                    PackageNos = request.PackageNos,
                                    UpdateBy = request.UpdateBy,
                                    Status = PackageStatusEnum.已签收.ToString()
                                });

                                //await _wmsClient.UpdateProductPackageStatus(new PackageProductClientRequest
                                //{
                                //    PackageNos = request.PackageNos,
                                //    UpdateBy = request.UpdateBy,
                                //    Status = PackageStatusEnum.已签收.ToString()
                                //});

                                await _transferTaskRepository.UpdateWarehouseTransferStatus(new WarehouseTransferTaskDO
                                {
                                    TransferType = packageItem.TransferType,
                                    TransferId = packageItem.TransferId,
                                    TaskStatus = "已送达",
                                    UpdateBy = request.UpdateBy
                                });

                                //是否是 所有的包裹记录
                                //获取已经签收的包裹
                                var haveSignList = await _receiveRepository.GetListAsync(" where relation_no =@relation_no and is_deleted=0", new { relation_no = packageItem.TransferId });
                                var allSign = true;
                                foreach (var r in allPackages)
                                {
                                    if (!haveSignList.Any(k => k.PackageNo == r.DeliveryCode))
                                    {
                                        allSign = false;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                var signStatus = allSign ? 3 : 2;

                                await _receiveRepository.UpdateSignStatus(new SignDO
                                {
                                    UpdateBy = request.UpdateBy,
                                    SignStatus = Convert.ToSByte(signStatus),
                                    HaveSignNum = haveSignList.Count(),
                                    RelationNo = packageItem.TransferId
                                });

                                //然后根据包裹查出关联的单号->订单/铺货单号做清点
                                var packageProducts = await _productPackageRelationRepository.GetListAsync(" where is_deleted=0 and package_no in @packageNos",
                                        new { packageNos = request.PackageNos });

                                var productIds = packageProducts.Select(r => r.ProductId).Distinct().ToList();
                                var products = await _productClient.GetProductsByProductCodes(new ProductDetailSearchClientRequest
                                {
                                    ProductCodes = productIds
                                });

                                long transferId = packageProducts.FirstOrDefault().TransferId;

                                var transferProducts = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                                {
                                    TransferId = transferId
                                });

                                var receivePackage = new ReceivePackageDTO();

                                receivePackage.RelationNo = $"{transferTask.TransferId}{transferTask.TransferType}清点入库";
                                foreach (var item in packageProducts.GroupBy(r => r.PackageNo))
                                {
                                    var packageProduct = new PackageProductDTO
                                    {
                                        PackageNo = item.Key
                                    };
                                    foreach (var productItem in item.GroupBy(r => r.ProductId))
                                    {
                                        string url = string.Empty;
                                        foreach (var searchProduct in products)
                                        {
                                            if (searchProduct.Product.ProductCode == productItem.Key)
                                            {
                                                url = searchProduct.Product.Image1;
                                            }
                                        }
                                        var firstProduct = productItem.First();
                                        packageProduct.Products.Add(new ProductDTO
                                        {
                                            ProductId = firstProduct.ProductId,
                                            ProductName = firstProduct.ProductName,
                                            Num = transferProducts.Where(r => r.ProductId == firstProduct.ProductId).Sum(r => r.Num),
                                            //TODO ReceiveNum 暂时用不上
                                            ActualNum = transferProducts.Where(r => r.ProductId == firstProduct.ProductId).Sum(r => r.Num),
                                            Url = _configuration["QiNiuImageDomain"] + url + "?imageView2/1/w/200/h/200"
                                        });
                                    }
                                    receivePackage.PackageProducts.Add(packageProduct);
                                }

                                res.Data = receivePackage;
                                res.Code = ResultCode.Success;
                                return res;
                            }
                            else
                            {
                                return new ApiResult<ReceivePackageDTO>()
                                {
                                    Code = ResultCode.SYSTEM_ERROR,
                                    Message = CommonConstant.ValidatePackageIsNotExist
                                };
                            }
                        }
                        else
                        {
                            return new ApiResult<ReceivePackageDTO>()
                            {
                                Code = ResultCode.Failed,
                                Message = $"{request.PackageNos?.FirstOrDefault()} {CommonConstant.PackageNotExist}"
                            };
                        }
                    }
                }
                else
                {
                    var packagesRes = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                    {
                        PackageNos = request.PackageNos
                    });

                    if (packagesRes.Any())
                    {
                        if (packagesRes.Select(r => r.TransferId).Distinct().Count() > 1)
                        {
                            return new ApiResult<ReceivePackageDTO>()
                            {
                                Code = ResultCode.Failed,
                                Message = $"清点的包裹类别不一致!"
                            };
                        }

                        if (packagesRes.Any(r => r.Status == PackageStatusEnum.已清点.ToString()))
                        {
                            return new ApiResult<ReceivePackageDTO>()
                            {
                                Code = ResultCode.Failed,
                                Message = $"包裹号:{string.Join(";", packagesRes.Where(r => r.Status == PackageStatusEnum.已清点.ToString()).Select(r => r.DeliveryCode))}已清点!"
                            };
                        }

                        var packageItem = packagesRes.FirstOrDefault();

                        //获取所有的包裹
                        var allPackages = await _transferTaskRepository.GetWarehouseTransferPackages(new WarehouseTransferPackageDO
                        {
                            TransferId = packageItem.TransferId,
                            TransferType = packageItem.TransferType
                        });

                        var transferTask = await _transferTaskRepository.GetWarehouseTransferTask(new WarehouseTransferTaskDO
                        {
                            TransferId = packageItem.TransferId,
                            TransferType = packageItem.TransferType
                        });

                        if (request.ShopId != transferTask.TargetWarehouse)
                        {
                            return new ApiResult<ReceivePackageDTO>()
                            {
                                Code = ResultCode.Failed,
                                Message = CommonConstant.PackageIsNotShop
                            };
                        }

                        var packageProducts = await _productPackageRelationRepository.GetListAsync(" where is_deleted=0 and package_no in @packageNos",
                                       new { packageNos = request.PackageNos });

                        var productIds = packageProducts.Select(r => r.ProductId).Distinct().ToList();
                        var products = await _productClient.GetProductsByProductCodes(new ProductDetailSearchClientRequest
                        {
                            ProductCodes = productIds
                        });

                        long transferId = packageProducts.FirstOrDefault().TransferId;

                        var transferProducts = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                        {
                            TransferId = transferId
                        });

                        var receivePackage = new ReceivePackageDTO();

                        receivePackage.RelationNo = $"{transferTask.TransferId}{transferTask.TransferType}清点入库";
                        foreach (var item in packageProducts.GroupBy(r => r.PackageNo))
                        {
                            var packageProduct = new PackageProductDTO
                            {
                                PackageNo = item.Key
                            };
                            foreach (var productItem in item.GroupBy(r => r.ProductId))
                            {
                                string url = string.Empty;
                                foreach (var searchProduct in products)
                                {
                                    if (searchProduct.Product.ProductCode == productItem.Key)
                                    {
                                        url = searchProduct.Product.Image1;
                                    }
                                }
                                var firstProduct = productItem.First();
                                packageProduct.Products.Add(new ProductDTO
                                {
                                    ProductId = firstProduct.ProductId,
                                    ProductName = firstProduct.ProductName,
                                    Num = transferProducts.Where(r => r.ProductId == firstProduct.ProductId).Sum(r => r.Num),
                                    //TODO ReceiveNum 暂时用不上
                                    ActualNum = transferProducts.Where(r => r.ProductId == firstProduct.ProductId).Sum(r => r.Num),
                                    Url = _configuration["QiNiuImageDomain"] + url + "?imageView2/1/w/200/h/200"
                                });
                            }
                            receivePackage.PackageProducts.Add(packageProduct);
                        }
                        res.Data = receivePackage;
                        res.Code = ResultCode.Success;
                        return res;
                    }
                    else
                    {
                        return new ApiResult<ReceivePackageDTO>()
                        {
                            Code = ResultCode.Failed,
                            Message = $"{request.PackageNos?.FirstOrDefault()} {CommonConstant.PackageNotExist}"
                        };
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SignInStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public sbyte GetSignType(string signType)
        {
            sbyte type = 0;
            if (signType == SignType.订单.ToString())
            {
                type = 1;
            }
            else if (signType == SignType.铺货.ToString())
            {
                type = 2;
            }
            return type;
        }

        /// <summary>
        /// 门店签收公司货物 入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CheckInStockForShop(CompanyInStockDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var shopInfoRes = await _shopMangeClient.GetShopById(new GetShopClientRequest { ShopId = request.LocationId });
                ShopDTO shopInfo = null;
                if (shopInfoRes.Code == ResultCode.Success)
                {
                    shopInfo = shopInfoRes.Data;
                }
                var shopSimpleInfo = new ShopSimpleDTO
                {
                    ShopId = request.LocationId,
                    SimpleName = shopInfo != null ? shopInfo.SimpleName : string.Empty
                };
                //   var companyInfo = _companyRepository.GetAsync(shopInfo?.CompanyId);

                //没有快递单号 不需要添加到签收表  直接把库存做扭转即可
                //该门店的出库单 做入库
                var transferTask = await _transferTaskRepository.GetWarehouseTransferTask(new WarehouseTransferTaskDO
                {
                    SourceWarehouse = shopInfo.CompanyId,
                    TargetWarehouse = request.LocationId,
                    TaskStatus = "已发出",
                    TransferType = "铺货"
                });

                if (transferTask != null)
                {
                    var warehouseTransferProductRequest = new UpdateWarehouseTransferProductRequest
                    {
                        TransferId = transferTask.TransferId,
                        TransferType = transferTask.TransferType,
                        UpdateBy = request.CreateBy
                    };

                    //发出的产品
                    var transferProductRes = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                    {
                        TransferId = transferTask.Id
                    });

                    foreach (var item in transferProductRes)
                    {
                        warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                        {
                            ReceiveNum = item.Num,
                            TransferProductId = item.Id
                        });
                    }

                    //回写出库记录 生成在途->门店的扭转记录
                    if (warehouseTransferProductRequest.TransferList.Any())
                    {
                        await _wmsClient.UpdateWarehouseTransferProductReceiveNum(warehouseTransferProductRequest);
                    }

                    //以每个包裹为单位 生成出库任务
                    var stockId = await _stockInoutRepository.InsertAsync(new StockInoutDO
                    {
                        CreateBy = request.CreateBy,
                        IsDeleted = 0,
                        CreateTime = DateTime.Now,
                        LocationId = request.LocationId,
                        LocationName = shopInfo != null ? shopInfo.SimpleName : string.Empty, //TODO
                        OperateTime = DateTime.Now,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        Operator = string.Empty,
                        SourceInventoryNo = transferTask.TransferId, //如果是空的 需要手动生成
                        SourceType = StockInTypeEnum.铺货入库.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString()  //判断有无差异数目
                    });

                    foreach (var transferProduct in transferProductRes)
                    {
                        await _stockManageService.TransferSyncInventoryData(transferProduct, shopSimpleInfo, new ProductTransferDTO
                        {
                            InOutId = stockId,
                            IsDamage = false,
                            ActualNum = transferProduct.Num,
                            CreateBy = request.CreateBy
                        });
                    }

                    await _transferTaskRepository.UpdateWarehouseTransferStatus(new WarehouseTransferTaskDO
                    {
                        TransferId = transferTask.TransferId,
                        TransferType = transferTask.TransferType,
                        TaskStatus = "已收货",
                        UpdateBy = request.CreateBy
                    });
                    res.Code = ResultCode.Success;
                }
                else
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = "无待收货记录!"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"CheckInStockForShop_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 代理商/供应商发货，门店入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> VenderCheckInStock(AcceptCompanyStockRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                if (!request.Products.Any())
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = "请选择产品!"
                    };
                }

                var shopInfoRes = await _shopMangeClient.GetShopById(new GetShopClientRequest { ShopId = request.LocationId });
                ShopDTO shopInfo = null;
                if (shopInfoRes.Code == ResultCode.Success)
                {
                    shopInfo = shopInfoRes.Data;
                }
                var shopSimpleInfo = new ShopSimpleDTO
                {
                    ShopId = request.LocationId,
                    SimpleName = shopInfo != null ? shopInfo.SimpleName : string.Empty
                };
                //   var companyInfo = _companyRepository.GetAsync(shopInfo?.CompanyId);

                //没有快递单号 不需要添加到签收表  直接把库存做扭转即可
                //该门店的出库单 做入库


                foreach (var item in request.Products)
                {
                    var transferTask = await _transferTaskRepository.GetWarehouseTransferTask(new WarehouseTransferTaskDO
                    {
                        Id = Int64.Parse(item.RelationId),
                        TargetWarehouse = request.LocationId,
                        TaskStatus = "已发出"
                    });

                    var warehouseTransferProductRequest = new UpdateWarehouseTransferProductRequest
                    {
                        TransferId = transferTask.TransferId,
                        TransferType = item.TaskType,
                        UpdateBy = request.UpdateBy
                    };

                    //发出的产品
                    var transferProductRes = await _transferTaskRepository.GetWarehouseTransferProducts(new WarehouseTransferProductDO
                    {
                        TransferId = transferTask.Id
                    });

                    foreach (var transferProduct in transferProductRes)
                    {
                        warehouseTransferProductRequest.TransferList.Add(new UpdateWarehouseTransferProductReceiveRequest
                        {
                            ReceiveNum = transferProduct.Num,
                            TransferProductId = transferProduct.Id
                        });
                    }

                    //回写出库记录 生成在途->门店的扭转记录
                    if (warehouseTransferProductRequest.TransferList.Any())
                    {
                        await _wmsClient.UpdateWarehouseTransferProductReceiveNum(warehouseTransferProductRequest);
                    }

                    //以每个包裹为单位 生成出库任务
                    var stockId = await _stockInoutRepository.InsertAsync(new StockInoutDO
                    {
                        CreateBy = request.UpdateBy,
                        IsDeleted = 0,
                        CreateTime = DateTime.Now,
                        LocationId = request.LocationId,
                        LocationName = shopInfo != null ? shopInfo.SimpleName : string.Empty, //TODO
                        OperateTime = DateTime.Now,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        Operator = string.Empty,
                        SourceInventoryNo = transferTask.TransferId, //如果是空的 需要手动生成
                        SourceType = StockInTypeEnum.铺货入库.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString()  //判断有无差异数目
                    });

                    foreach (var transferProduct in transferProductRes)
                    {
                        await _stockManageService.TransferSyncInventoryData(transferProduct, shopSimpleInfo, new ProductTransferDTO
                        {
                            InOutId = stockId,
                            IsDamage = false,
                            ActualNum = transferProduct.Num,
                            CreateBy = request.UpdateBy
                        });
                    }

                    await _transferTaskRepository.UpdateWarehouseTransferStatus(new WarehouseTransferTaskDO
                    {
                        TransferId = transferTask.TransferId,
                        TransferType = transferTask.TransferType,
                        TaskStatus = "已收货",
                        UpdateBy = request.UpdateBy
                    });
                }
                return new ApiResult<string>
                {
                    Code = ResultCode.Success,
                    Message = "收货成功!"
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"CheckInStockForShop_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }
    }
}







