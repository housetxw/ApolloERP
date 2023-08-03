using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
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
using Ae.Shop.Api.Dal.Model;
using System.Linq;
using Dapper;
using Ae.Shop.Api.Client.Clients.Order;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Dal.Repositorys.HomeCare;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Core.Request.HomeCare;

namespace Ae.Shop.Api.Imp.Services
{
    public class HomeCareService : IHomeCareService
    {
        private readonly ApolloErpLogger<HomeCareService> _logger;
        private readonly IHomeCareOrderRepository _homeCareOrderRepository;
        private readonly IHomeCareProductRepository _homeCareProductRepository;
        private readonly IHomeCareRecordRepository _homeCareRecordRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IIdentityService identityService;
        private readonly IOrderClient _orderClient;
        private readonly IOrderDispatchRepository _orderDispatchRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStockManageService _stockManageService;
        private readonly IHomeReturnRecordRepository _homeReturnRecordRepository;
        private readonly IHomeReturnProductRepository _homeReturnProductRepository;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly IStockInoutRepository _stockInoutRepository;
        private readonly IStockInoutItemRepository _stockInoutItemRepository;

        private readonly ITechClaimProductRepository techClaimProductRepository;
        private readonly IProductClaimRecordRepository productClaimRecordRepository;

        public HomeCareService(ApolloErpLogger<HomeCareService> logger,
          IHomeCareOrderRepository homeCareOrderRepository, IHomeCareProductRepository homeCareProductRepository,
        IHomeCareRecordRepository homeCareRecordRepository, IMapper mapper,
          IConfiguration configuration, IIdentityService identityService,
          IOrderClient orderClient, IOrderDispatchRepository orderDispatchRepository,
          IEmployeeRepository employeeRepository, IStockManageService stockManageService,
          IHomeReturnRecordRepository homeReturnRecordRepository,
          IHomeReturnProductRepository homeReturnProductRepository, IStockInoutRepository stockInoutRepository,
           IStockInoutItemRepository stockInoutItemRepository,
          IShopMangeClient shopMangeClient, ITechClaimProductRepository _techClaimProductRepository,
          IProductClaimRecordRepository _productClaimRecordRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            this.techClaimProductRepository = _techClaimProductRepository;
            this.productClaimRecordRepository = _productClaimRecordRepository;

            this.identityService = identityService;
            this._homeCareOrderRepository = homeCareOrderRepository;
            this._homeCareProductRepository = homeCareProductRepository;
            this._homeCareRecordRepository = homeCareRecordRepository;
            this._orderClient = orderClient;
            this._orderDispatchRepository = orderDispatchRepository;
            this._employeeRepository = employeeRepository;
            this._stockManageService = stockManageService;
            this._homeReturnProductRepository = homeReturnProductRepository;
            this._homeReturnRecordRepository = homeReturnRecordRepository;
            this._shopMangeClient = shopMangeClient;
            this._stockInoutRepository = stockInoutRepository;
            this._stockInoutItemRepository = stockInoutItemRepository;
        }


        #region 技师领料
        public async Task<ApiResult<string>> CreateHomeCareRecord(HomeCareRecordDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var shopInfo = await _stockManageService.GetShopById();
                var recordId = await _homeCareRecordRepository.InsertAsync(new HomeCareRecordDO
                {
                    CategoryNum = request.Products.Select(r => r.CategoryName).Distinct().Count(),
                    IsDeleted = 0,
                    CreateBy = this.identityService.GetUserName(),
                    CreateTime = DateTime.Now,
                    ReceiveName = request.ReceiveName,
                    ReceiveTime = request.ReceiveTime,
                    Remark = string.Empty,
                    ShopId = shopInfo.ShopId,
                    ShopName = shopInfo.SimpleName,
                    Status = HomeCareStatusEnum.未完结.ToString(),
                    TechId = request.TechId,
                    TechName = request.TechName,
                    SumProductNum = request.Products.Sum(r => r.Num)
                });
                if (request.Products.Any())
                {
                    foreach (var item in request.Products)
                    {
                        await _homeCareProductRepository.InsertAsync(new HomeCareProductDO
                        {
                            IsDeleted = 0,
                            CategoryName = item.CategoryName,
                            CreateBy = this.identityService.GetUserName(),
                            CreateTime = DateTime.Now,
                            Num = item.Num,
                            ReceiveNum = item.Num,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Status = HomeCareStatusEnum.未完结.ToString(),
                            RecordId = recordId
                        });
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateHomeCareRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        public async Task<ApiResult<List<EmployeeDTO>>> GetEmployes()
        {
            var res = new ApiResult<List<EmployeeDTO>>();
            try
            {
                var orgType = identityService.GetOrgType();
                long shopId = Convert.ToInt64(identityService.GetOrganizationId());
                
                var result = await _employeeRepository.GetListAsync(" where type=@orgType and is_deleted=0 and organization_id=@shopId and dimission_type=0", new { shopId = shopId, orgType= orgType });
                var vo = _mapper.Map<List<EmployeeDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateHomeCareRecord_Error", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareProducts(HomeCareProductDTO request)
        {
            var res = new ApiResult<List<HomeCareProductDTO>>();
            try
            {

                var result = await _homeCareProductRepository.GetListAsync(" where record_id =@record_id and is_deleted=0", new { record_id = request.RecordId });
                var vo = _mapper.Map<List<HomeCareProductDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeCareProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecordPages(HomeCareRecordRequest request)
        {
            var res = new ApiPagedResult<HomeCareRecordDTO>();
            try
            {
                var orgType = identityService.GetOrgType();
                long shopId = 0;
                if (orgType == "1")
                {
                    shopId = Convert.ToInt64(identityService.GetOrganizationId());
                }

                request.ShopId = shopId;

                if (request.Times != null && request.Times.Any())
                {
                    request.StartTime = Convert.ToDateTime(request.Times[0]);
                    request.EndTime = Convert.ToDateTime(request.Times[1]);
                }

                var result = await _homeCareRecordRepository.GetHomeCareRecordPages(request);

                var vo = _mapper.Map<ApiPagedResultData<HomeCareRecordDTO>>(result);
                res.Data = vo;
                res.Message = "查询成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeCareRecordPages_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        #endregion

        #region 技师退料
        public async Task<ApiResult<string>> OrderInstallNotify(HomeCareOrderDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //要回写领取任务表的安装值
                var orderInfo = await _orderClient.QueryUseStockOrderDetail(new QueryUseStockOrderDetailClientRequest
                {
                    OrderNo = request.OrderNo
                });
                if (orderInfo.Code == ResultCode.Success
                    && orderInfo.Data != null)
                {
                    var shopType = 0;

                    var shopId = orderInfo.Data.ShopId;

                    var shopResult = await _shopMangeClient.GetShopById(new GetShopClientRequest
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
                    var orderProducts = orderInfo.Data.Products;

                    var realProducts = new List<InstallOrderProdcutDTO>();
                    orderProducts.ForEach(r =>
                    {
                        //单品
                        if (r.PackageOrProduct.ProductAttribute == 0)
                        {
                            realProducts.Add(new InstallOrderProdcutDTO
                            {
                                Num = r.PackageOrProduct.TotalNumber,
                                OrderListId = r.PackageOrProduct.Id,
                                OrderNo = request.OrderNo,
                                ProductId = r.PackageOrProduct.ProductId,
                                ProductName = r.PackageOrProduct.ProductName
                            });
                        }
                        //套装
                        else if (r.PackageOrProduct.ProductAttribute == 1)
                        {
                            r.PackageProducts.ForEach(k =>
                            {
                                if (k.ProductAttribute == 0)
                                {
                                    realProducts.Add(new InstallOrderProdcutDTO
                                    {
                                        Num = k.TotalNumber,
                                        OrderListId = k.Id,
                                        OrderNo = request.OrderNo,
                                        ProductId = k.ProductId,
                                        ProductName = k.ProductName
                                    });
                                }
                            });
                        }
                    });
                    #endregion

                    //获取订单派工的技师
                    var orderDispatchRes = await _orderDispatchRepository.GetListAsync(" where order_no=@order_no and is_deleted=0", new { order_no = request.OrderNo });

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
                            return new ApiResult<string> { Code = ResultCode.Exception, Message = $"【{request.OrderNo}】暂无养车记录需同步！" };
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
                                            UpdateBy = request.UpdateBy,
                                            ReceiveNum = orginNum,
                                            Status = status,
                                            Remark = $" 更新安装数量:{orginNum} "
                                        });

                                        await SyncHomeCareRecordStatus(filterProduct.RecordId, request.UpdateBy);

                                        await _homeCareOrderRepository.InsertAsync(new HomeCareOrderDO
                                        {
                                            CreateBy = request.UpdateBy,
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
                                            UpdateBy = request.UpdateBy,
                                            Status = status,
                                            Remark = $" 更新安装数量:{filterProduct.ReceiveNum} "
                                        });

                                        await SyncHomeCareRecordStatus(filterProduct.RecordId, request.UpdateBy);

                                        orginNum = orginNum - filterProduct.ReceiveNum;

                                        await _homeCareOrderRepository.InsertAsync(new HomeCareOrderDO
                                        {
                                            CreateBy = request.UpdateBy,
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

        //public async Task<ApiResult<string>> UpdateHomeCareProductNum(HomeCareProductDTO request)
        //{
        //    var res = new ApiResult<string>();
        //    try
        //    {

        //        res.Code = ResultCode.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Code = ResultCode.Exception;
        //        _logger.Error($"UpdateHomeCareProductNum_Error Data:{JsonConvert.SerializeObject(request)}", ex);
        //    }
        //    return res;
        //}

        public async Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecordPages(HomeCareRecordRequest request)
        {
            var res = new ApiPagedResult<HomeReturnRecordDTO>();
            try
            {
                var orgType = identityService.GetOrgType();
                long shopId = 0;
                if (orgType == "1")
                {
                    shopId = Convert.ToInt64(identityService.GetOrganizationId());
                }

                request.ShopId = shopId;

                if (request.Times != null && request.Times.Any())
                {
                    request.StartTime = Convert.ToDateTime(request.Times[0]);
                    request.EndTime = Convert.ToDateTime(request.Times[1]);
                }

                var result = await _homeReturnRecordRepository.GetHomeReturnRecordPages(request);

                var vo = _mapper.Map<ApiPagedResultData<HomeReturnRecordDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
                res.Message = "查询成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeReturnRecordPages_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> CreateHomeReturnRecord(HomeReturnRecordDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var shopInfo = await _stockManageService.GetShopById();
                var returnRecord = await _homeReturnRecordRepository.InsertAsync(new HomeReturnRecordDO
                {
                    CategoryNum = request.Products.Select(r => r.CategoryName).Distinct().Count(),
                    CreateBy = identityService.GetUserName(),
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    ReceiveName = request.ReceiveName,
                    ReceiveTime = request.ReceiveTime,
                    ShopId = shopInfo.ShopId,
                    TechId = request.TechId,
                    SumProductNum = request.Products.Sum(r => r.ExceptionNum) +
                    request.Products.Sum(r => r.LessNum) + request.Products.Sum(r => r.ActualNum)
                });

                if (request.Products.Any())
                {
                    foreach (var r in request.Products)
                    {
                        if (r.ExceptionNum > 0 || r.LessNum > 0 || r.ActualNum > 0)
                        {
                            var returnProduct = await _homeReturnProductRepository.InsertAsync(new HomeReturnProductDO
                            {
                                IsDeleted = 0,
                                ActualNum = r.ActualNum,
                                CategoryName = r.CategoryName,
                                CreateBy = identityService.GetUserName(),
                                CreateTime = DateTime.Now,
                                ExceptionNum = r.ExceptionNum,
                                LessNum = r.LessNum,
                                ProductId = r.ProductId,
                                ProductName = r.ProductName,
                                RecordId = returnRecord
                            });

                            if (r.ActualNum > 0)
                            {
                                await SyncHomeCareProductNum(r, shopInfo, request, r.ActualNum, 1);
                            }

                            if (r.ExceptionNum > 0)
                            {
                                await SyncHomeCareProductNum(r, shopInfo, request, r.ExceptionNum, 2);
                            }

                            if (r.LessNum > 0)
                            {
                                await SyncHomeCareProductNum(r, shopInfo, request, r.LessNum, 3);
                            }

                            //差异处理
                            await CreateStockOutTask(r, returnRecord, returnProduct);

                        }
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateHomeReturnRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> SyncHomeCareProductNum(HomeReturnProductDTO returnProduct,
            ShopSimpleDTO shopInfo, HomeReturnRecordDTO request, int orginActualNum, int type)
        {
            var res = new ApiResult<string>();
            try
            {
                //总共有三个值 都要往home_care_product 表中回写 并判断状态
                //TODO:回写到那一条记录上？？？
                var products = await _homeCareProductRepository.GetHomeCareProductsByTechId(new HomeCareRecordDO
                {
                    ShopId = shopInfo.ShopId,
                    TechId = request.TechId,
                    StatusList = new List<string> { HomeCareStatusEnum.未完结.ToString() }
                });
                var filterProducts = products?.Where(r => r.ProductId == returnProduct.ProductId)?.OrderBy(r => r.CreateTime);
                if (filterProducts != null && filterProducts.Any())
                {
                    foreach (var filterProduct in filterProducts)
                    {
                        if (filterProduct.ReceiveNum >= orginActualNum)
                        {
                            var status = string.Empty;

                            if (filterProduct.LessNum > 0 || filterProduct.ExceptionNum > 0)
                            {
                                if (filterProduct.ReceiveNum > orginActualNum)
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
                                if (filterProduct.ReceiveNum > orginActualNum)
                                {
                                    status = HomeCareStatusEnum.未完结.ToString();
                                }
                                else
                                {
                                    status = HomeCareStatusEnum.正常完结.ToString();
                                }
                            }

                            //矫正状态
                            if (status == HomeCareStatusEnum.正常完结.ToString())
                            {
                                if (type == 2 || type == 3)
                                {
                                    status = HomeCareStatusEnum.异常完结.ToString();
                                }
                            }

                            //回写数量
                            await _homeCareProductRepository.UpdateProductReturnNum(new HomeCareProductDO
                            {
                                Id = filterProduct.Id,
                                Status = status,
                                ReceiveNum = orginActualNum,
                                ActualNum = orginActualNum,
                                ExceptionNum = orginActualNum,
                                LessNum = orginActualNum,
                                UpdateBy = identityService.GetUserName(),
                                Remark = GetRemarkValue(orginActualNum, type)
                            }, type);

                            await SyncHomeCareRecordStatus(filterProduct.RecordId, identityService.GetUserName());
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
                            //矫正状态
                            if (status == HomeCareStatusEnum.正常完结.ToString())
                            {
                                if (type == 2 || type == 3)
                                {
                                    status = HomeCareStatusEnum.异常完结.ToString();
                                }
                            }

                            await _homeCareProductRepository.UpdateProductReturnNum(new HomeCareProductDO
                            {
                                Id = filterProduct.Id,
                                Status = status,
                                ReceiveNum = filterProduct.ReceiveNum,
                                ActualNum = filterProduct.ReceiveNum,
                                ExceptionNum = filterProduct.ReceiveNum,
                                LessNum = filterProduct.ReceiveNum,
                                UpdateBy = identityService.GetUserName(),
                                Remark = GetRemarkValue(filterProduct.ReceiveNum, type)
                            }, type);

                            orginActualNum -= filterProduct.ReceiveNum;

                            await SyncHomeCareRecordStatus(filterProduct.RecordId, identityService.GetUserName());
                            continue;
                        }
                    }
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SyncHomeCareProductNum_Error Data:{JsonConvert.SerializeObject(returnProduct)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 获取备注值
        /// </summary>
        /// <param name="num">数量</param>
        /// <param name="type">类型(1->实收 2->货损 3->缺少)</param>
        /// <returns></returns>
        public string GetRemarkValue(int num, int type)
        {
            string remark = string.Empty;
            if (type == 1)
            {
                remark = $" 退料【实收】数量{num} ";
            }
            else if (type == 2)
            {
                remark = $" 退料【货损】数量{num} ";
            }
            else if (type == 3)
            {
                remark = $" 退料【缺少】数量{num} ";
            }
            return remark;
        }

        public async Task<ApiResult<string>> CreateStockOutTask(HomeReturnProductDTO r, long returnRecord, long returnProduct)
        {
            var res = new ApiResult<string>();
            try
            {
                //回写领料表
                if (r.ExceptionNum > 0)
                {
                    var stockOutRequest = new StockInOutDTO
                    {
                        OperateTime = DateTime.Now,
                        Operator = identityService.GetUserName(),
                        Remark = $"【{r.ProductName}】货损出库",
                        SourceType = StockOutTypeEnum.货损出库.ToString(),
                        SourceInventoryNo = returnRecord.ToString()
                    };
                    stockOutRequest.StockItems.Add(new StockInoutItemDTO
                    {
                        ProductId = r.ProductId,
                        ProductName = r.ProductName,
                        Num = r.ExceptionNum,
                        Cost = 0,
                        ReleationId = returnProduct
                    });

                    res = await _stockManageService.CreateStorageOutStockTask(stockOutRequest);
                }

                if (r.LessNum > 0)
                {
                    var stockOutRequest = new StockInOutDTO
                    {
                        OperateTime = DateTime.Now,
                        Operator = identityService.GetUserName(),
                        Remark = $"【{r.ProductName}】丢失出库",
                        SourceType = StockOutTypeEnum.丢失出库.ToString(),
                        SourceInventoryNo = returnRecord.ToString()

                    };
                    stockOutRequest.StockItems.Add(new StockInoutItemDTO
                    {
                        ProductId = r.ProductId,
                        ProductName = r.ProductName,
                        Num = r.LessNum,
                        Cost = 0,
                        ReleationId = returnProduct
                    });
                    res = await _stockManageService.CreateStorageOutStockTask(stockOutRequest);
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateStockOutTask_Error Data:{JsonConvert.SerializeObject(r)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProducts(HomeReturnProductDTO request)
        {
            var res = new ApiResult<List<HomeReturnProductDTO>>();
            try
            {
                var result = await _homeReturnProductRepository.GetListAsync(" where record_id =@record_id and is_deleted=0", new { record_id = request.RecordId });
                var vo = _mapper.Map<List<HomeReturnProductDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeReturnProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProductsByTech(HomeReturnRecordDTO request)
        {
            var res = new ApiResult<List<HomeReturnProductDTO>>();
            try
            {
                var orgType = identityService.GetOrgType();
                long shopId = 0;
                if (orgType == "1")
                {
                    shopId = Convert.ToInt64(identityService.GetOrganizationId());
                }
                //技师的产品需要合并
                // var products = await _homeCareProductRepository.GetListAsync(" where is_deleted=0 and ")
                var vo = await _homeCareProductRepository.GetHomeCareProductsByTechId(new HomeCareRecordDO
                {
                    ShopId = shopId,
                    TechId = request.TechId,
                    StatusList = new List<string> { HomeCareStatusEnum.未完结.ToString() }

                });

                var products = new List<HomeReturnProductDTO>();

                foreach (var item in vo.GroupBy(r => r.ProductId))
                {
                    var detailProduct = item.FirstOrDefault();

                    products.Add(new HomeReturnProductDTO
                    {
                        CategoryName = detailProduct.CategoryName,
                        ProductId = detailProduct.ProductId,
                        ProductName = detailProduct.ProductName,
                        ReceiveNum = item.Sum(r => r.ReceiveNum)
                    });
                }

                res.Data = products;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeReturnProductsByTech_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        #endregion

        #region B端App接口
        public async Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareStocks(HomeCareStockRequest request)
        {
            var res = new ApiResult<List<HomeCareProductDTO>>();
            try
            {
                var productResult = await _homeCareProductRepository.GetHomeCareProductsByTechId(new HomeCareRecordDO
                {
                    ShopId = request.ShopId,
                    TechId = request.TechId,
                    StatusList = new List<string> { HomeCareStatusEnum.未完结.ToString() }
                });

                var products = new List<HomeCareProductDTO>();
                //按照pid  Group by
                foreach (var item in productResult.GroupBy(r => r.ProductId))
                {
                    var product = item.First();
                    products.Add(new HomeCareProductDTO
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Num = item.Sum(r => r.ReceiveNum)
                    });
                }
                res.Data = products;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeCareStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecords(HomeCareRequest request)
        {
            var res = new ApiPagedResult<HomeCareRecordDTO>();
            try
            {
                var parm = new DynamicParameters();
                parm.Add("@shop_id", request.ShopId);
                parm.Add("@tech_id", request.TechId);

                var result = await _homeCareRecordRepository.GetListPagedAsync(request.PageIndex, request.PageSize,
                    " where shop_id=@shop_id and tech_id = @tech_id and is_deleted = 0", "id desc", parm);

                var vo = _mapper.Map<ApiPagedResultData<HomeCareRecordDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeCareRecords_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<HomeCareRecordDTO>> GetHomeCareRecord(HomeCareDetailRequest request)
        {
            var res = new ApiResult<HomeCareRecordDTO>();
            try
            {
                var homeCareRecord = await _homeCareRecordRepository.GetAsync(request.Id);
                if (homeCareRecord == null)
                {
                    res.Code = ResultCode.Exception;
                    return res;
                }
                var homeCareProducts = await _homeCareProductRepository.GetListAsync("where is_deleted=0 and record_id=@record_id", new { record_id = request.Id });
                var homeCareRecordRes = _mapper.Map<HomeCareRecordDTO>(homeCareRecord);
                var homeCareProductsRes = _mapper.Map<List<HomeCareProductDTO>>(homeCareProducts);

                if (string.IsNullOrWhiteSpace(homeCareRecordRes.ReceiveName))
                {
                    homeCareRecordRes.ReceiveName = homeCareRecordRes.TechName;
                }
                var employeesRes = await _employeeRepository.GetListAsync(" where type=1 and is_deleted=0 and organization_id=@shopId and dimission_type=0", new { shopId = homeCareRecord.ShopId });

                var tel = employeesRes.FirstOrDefault(r => r.Id == homeCareRecordRes.TechId)?.Mobile;

                homeCareRecordRes.ReceiveName += " " + tel;
                homeCareRecordRes.Products = homeCareProductsRes;

                res.Data = homeCareRecordRes;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeCareRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecords(HomeCareRequest request)
        {
            var res = new ApiPagedResult<HomeReturnRecordDTO>();
            try
            {
                var parm = new DynamicParameters();
                parm.Add("@shop_id", request.ShopId);
                parm.Add("@tech_id", request.TechId);

                var result = await _homeReturnRecordRepository.GetListPagedAsync(request.PageIndex, request.PageSize,
                    " where shop_id=@shop_id and tech_id = @tech_id and is_deleted = 0", "id desc", parm);

                var vo = _mapper.Map<ApiPagedResultData<HomeReturnRecordDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeReturnRecords_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<HomeReturnRecordDTO>> GetHomeReturnRecord(HomeCareDetailRequest request)
        {
            var res = new ApiResult<HomeReturnRecordDTO>();
            try
            {
                var homeCareRecord = await _homeReturnRecordRepository.GetAsync(request.Id);

                if (homeCareRecord == null)
                {
                    res.Code = ResultCode.Exception;
                    return res;
                }
                var homeCareProducts = await _homeReturnProductRepository.GetListAsync("where is_deleted=0 and record_id=@record_id", new { record_id = request.Id });
                var homeCareRecordRes = _mapper.Map<HomeReturnRecordDTO>(homeCareRecord);
                var homeCareProductsRes = _mapper.Map<List<HomeReturnProductDTO>>(homeCareProducts);
                homeCareRecordRes.Products = homeCareProductsRes;

                var employeesRes = await _employeeRepository.GetListAsync(" where type=1 and is_deleted=0 and organization_id=@shopId and dimission_type=0", new { shopId = homeCareRecord.ShopId });

                var tel = employeesRes.FirstOrDefault(r => r.Id == homeCareRecordRes.TechId)?.Mobile;

                homeCareRecordRes.ReceiveName += " " + tel;

                res.Data = homeCareRecordRes;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetHomeReturnRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        #endregion

        #region
        public async Task<ApiPagedResult<ProductClaimRecordDTO>> GetProductClaimRecordPages(HomeCareRecordRequest request)
        {
            var res = new ApiPagedResult<ProductClaimRecordDTO>();
            try
            {
                var conditions = new StringBuilder();
                var param = new DynamicParameters();

                var orgType = identityService.GetOrgType();
                long shopId = 0;
                if (orgType == "1")
                {
                    shopId = Convert.ToInt64(identityService.GetOrganizationId());
                }

                //request.ShopId = shopId;
                conditions.Append(" where shop_id =@shop_id and is_deleted=0 ");
                param.Add("@shop_id", shopId);
                if (request.Times != null && request.Times.Any())
                {
                    request.StartTime = Convert.ToDateTime(request.Times[0]);
                    request.EndTime = Convert.ToDateTime(request.Times[1]).AddDays(1);

                    conditions.Append(" and create_time>=@startTime and create_time  < @endTime");
                    param.Add("@startTime", request.StartTime);
                    param.Add("@endTime", request.EndTime);

                }

                if (!string.IsNullOrWhiteSpace(request.TechId))
                {
                    conditions.Append(" and tech_id=@tech_id");
                    param.Add("@tech_id", request.TechId);
                }

                var result = await productClaimRecordRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", param);

                var vo = _mapper.Map<ApiPagedResultData<ProductClaimRecordDTO>>(result);

                if (vo.Items != null && vo.Items.Any())
                {
                    List<long> recordIds = vo.Items.Select(r => r.Id).ToList();

                    var products = await techClaimProductRepository.GetListAsync("where record_id in @recordIds and is_deleted=0 ", new { recordIds = recordIds });

                    foreach (var item in vo.Items)
                    {
                        var builder = new StringBuilder();

                        var filterProducts = products.Where(r => r.RecordId == item.Id);
                        filterProducts.ToList().ForEach(r =>
                        {
                            builder.Append($"{r.ProductName}_{r.Num}" + "\n");
                        });
                        item.JoinText = builder.ToString();
                    }
                }

                res.Data = vo;
                res.Message = "查询成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeCareRecordPages_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;

        }

        public async Task<ApiResult<string>> CreateProductClaimRecord(ProductClaimRecordDTO request)
        {
            //创建 耗材出库的 出库任务

            var res = new ApiResult<string>();
            var createBy = identityService.GetUserName();
            try
            {

                var shopInfo = await _stockManageService.GetShopById();
                var recordId = await productClaimRecordRepository.InsertAsync(new ProductClaimRecordDO
                {
                    IsDeleted = 0,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    ReceiveName = request.ReceiveName,
                    ReceiveTime = request.ReceiveTime,
                    ShopId = shopInfo.ShopId,
                    ShopName = shopInfo.SimpleName,
                    Status = HomeCareStatusEnum.正常完结.ToString(),
                    TechId = request.TechId,
                    TechName = request.TechName,
                    Remark = request.Remark,
                });
                if (request.Products.Any())
                {
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = shopInfo.ShopId,
                        LocationName = shopInfo.SimpleName,
                        OperationType = StockOperateTypeEnum.出库.ToString(),
                        SourceInventoryNo = recordId.ToString(),
                        SourceType = StockOutTypeEnum.耗材领用.ToString(),
                        Status = StockInOutStatusEnum.已出库.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = "领料出库"
                    };
                    foreach (var item in request.Products)
                    {
                        var claimId = await techClaimProductRepository.InsertAsync(new TechClaimProductDO
                        {
                            RecordId = recordId,
                            IsDeleted = 0,
                            CategoryName = item.CategoryName,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            Num = item.Num,
                            CostPrice = item.CostPrice,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Status = HomeCareStatusEnum.正常完结.ToString(),
                            Remark = item.Remark
                        });
                        stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ReleationId = claimId,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Num = item.Num,
                            Cost = item.CostPrice,
                            GoodNum = item.Num,
                            ActualNum = item.Num,
                            UomText = "个",
                            CreateBy = createBy,
                            Status = StockInOutStatusEnum.已出库.ToString()
                        });

                    }


                    //更新库存
                    var resResult = await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                    //var result = await _stockManageService.BatchCreateStockOutTask(stockOutRequest);

                }

                res.Message = "操作成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateProductClaimRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> CancelProductClaimRecord(HomeCareDetailRequest request)
        {
            //创建 耗材出库的 出库任务

            var res = new ApiResult<string>();
            var createBy = identityService.GetUserName();
            try
            {

                var shopInfo = await _stockManageService.GetShopById();
                var recordInfo = await productClaimRecordRepository.GetListAsync(" where id =@record_id and is_deleted=0 and shop_id = @shop_id", 
                    new { record_id = request.Id, shop_id = shopInfo.ShopId });

                if (recordInfo == null || !recordInfo.Any())
                {
                    res.Message = "该门店未查到此领料单!";
                    res.Code = ResultCode.Exception;
                    return res;
                }

                var productClaimRecord = recordInfo.FirstOrDefault();
                productClaimRecord.Status = HomeCareStatusEnum.已取消.ToString();
                productClaimRecord.UpdateBy = createBy;
                productClaimRecord.UpdateTime = DateTime.Now;
                var recordId = await productClaimRecordRepository.UpdateAsync(productClaimRecord, new List<string> { "Status", "UpdateTime", "UpdateBy" });

                var productInfo = await techClaimProductRepository.GetListAsync(" where record_id =@record_id and is_deleted=0", new { record_id = request.Id });

                if (productInfo.Any())
                {
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = shopInfo.ShopId,
                        LocationName = shopInfo.SimpleName,
                        OperationType = StockOperateTypeEnum.入库.ToString(),
                        SourceInventoryNo = productClaimRecord.Id.ToString(),
                        SourceType = StockInTypeEnum.领料退货.ToString(),
                        Status = StockInOutStatusEnum.已收货.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = "领料退货入库"
                    };
                    foreach (var item in productInfo)
                    {
                        item.Status = HomeCareStatusEnum.已取消.ToString();
                        item.UpdateBy = createBy;
                        item.UpdateTime = DateTime.Now;
                        var claimId = await techClaimProductRepository.UpdateAsync(item, new List<string> { "Status", "UpdateTime", "UpdateBy" });
                       
                        stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ReleationId = item.Id,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Num = item.Num,
                            Cost = item.CostPrice,
                            GoodNum = item.Num,
                            ActualNum = item.Num,
                            UomText = "个",
                            CreateBy = createBy,
                            Status = StockInOutStatusEnum.已收货.ToString()
                        });

                    }

                    //更新库存
                    var resResult = await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                    //var result = await _stockManageService.BatchCreateStockOutTask(stockOutRequest);

                }

                res.Message = "操作成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateProductClaimRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> ClaimRepeatReduceStock(HomeCareDetailRequest request)
        {
            //创建 耗材出库的 出库任务

            var res = new ApiResult<string>();
            var createBy = identityService.GetUserName();
            try
            {

                var shopInfo = await _stockManageService.GetShopById();
                var recordInfo = await productClaimRecordRepository.GetListAsync(" where id =@record_id and is_deleted=0 and shop_id = @shop_id",
                    new { record_id = request.Id, shop_id = shopInfo.ShopId });

                if (recordInfo == null || !recordInfo.Any())
                {
                    res.Message = "该门店未查到此领料单!";
                    res.Code = ResultCode.Exception;
                    return res;
                }

                var productClaimRecord = recordInfo.FirstOrDefault();

                var productInfo = await techClaimProductRepository.GetListAsync(" where record_id =@record_id and is_deleted=0", new { record_id = request.Id });
                                
                if (!productInfo.Any())
                {
                    res.Message = "无产品，无需操作!";
                    res.Code = ResultCode.Success;
                    return res;
                }
                //获取出库记录
                var stockInoutRes = await _stockInoutRepository.GetListAsync(" where source_inventory_no =@source_inventory_no and is_deleted=0 and source_type=@source_type", new { source_inventory_no = request.Id, source_type = StockOutTypeEnum.耗材领用.ToString() });
                //无记录则补上
                if (stockInoutRes == null || !stockInoutRes.Any())
                {
                    var stockInoutRequest = new StockInOutDTO
                    {
                        LocationId = shopInfo.ShopId,
                        LocationName = shopInfo.SimpleName,
                        OperationType = StockOperateTypeEnum.出库.ToString(),
                        SourceInventoryNo = productClaimRecord.Id.ToString(),
                        SourceType = StockOutTypeEnum.耗材领用.ToString(),
                        Status = StockInOutStatusEnum.已出库.ToString(),
                        CreateBy = createBy,
                        Operator = createBy,
                        OperateTime = DateTime.Now,
                        Remark = "领料出库"
                    };
                    foreach (var item in productInfo)
                    {
                        stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                        {
                            ReleationId = item.Id,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Num = item.Num,
                            Cost = item.CostPrice,
                            GoodNum = item.Num,
                            ActualNum = item.Num,
                            UomText = "个",
                            CreateBy = createBy,
                            Status = StockInOutStatusEnum.已出库.ToString()
                        });

                    }

                    //更新库存
                    var resResult = await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                }

                if(productClaimRecord.Status == HomeCareStatusEnum.已取消.ToString())
                {
                    //获取入库记录
                    stockInoutRes = await _stockInoutRepository.GetListAsync(" where source_inventory_no =@source_inventory_no and is_deleted=0 and source_type=@source_type", new { source_inventory_no = request.Id, source_type = StockInTypeEnum.领料退货.ToString() });
                    //无记录则补上
                    if (stockInoutRes == null || !stockInoutRes.Any())
                    { 
                        var stockInoutRequest = new StockInOutDTO
                        {
                            LocationId = shopInfo.ShopId,
                            LocationName = shopInfo.SimpleName,
                            OperationType = StockOperateTypeEnum.入库.ToString(),
                            SourceInventoryNo = productClaimRecord.Id.ToString(),
                            SourceType = StockInTypeEnum.领料退货.ToString(),
                            Status = StockInOutStatusEnum.已收货.ToString(),
                            CreateBy = createBy,
                            Operator = createBy,
                            OperateTime = DateTime.Now,
                            Remark = "领料退货入库"
                        };
                        foreach (var item in productInfo)
                        {
                            stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                            {
                                ReleationId = item.Id,
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                Num = item.Num,
                                Cost = item.CostPrice,
                                GoodNum = item.Num,
                                ActualNum = item.Num,
                                UomText = "个",
                                CreateBy = createBy,
                                Status = StockInOutStatusEnum.已收货.ToString()
                            });
                        }
                        //更新库存
                        var resResult = await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                    }

                }

                res.Message = "操作成功!";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CreateProductClaimRecord_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<TechClaimProductDTO>>> GetProductClaimRecords(TechClaimProductDTO request)
        {
            var res = new ApiResult<List<TechClaimProductDTO>>();
            try
            {
                var result = await techClaimProductRepository.GetListAsync(" where record_id =@record_id and is_deleted=0", new { record_id = request.RecordId });
                var vo = _mapper.Map<List<TechClaimProductDTO>>(result);
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GetHomeCareProducts_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        #endregion
    }
}
