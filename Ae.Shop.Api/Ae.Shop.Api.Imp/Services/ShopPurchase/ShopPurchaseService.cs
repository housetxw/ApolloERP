using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.Product;
using Ae.Shop.Api.Client.Model.ShopManage;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Common.Extension;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Request.ShopPurchase;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Core.Response.ShopPurchase;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using Ae.Shop.Api.Dal.Repositorys.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Dal.Repositorys.Product;
using Ae.Shop.Api.Client.Clients.Fiance;
using Dapper;
using ProductDao = Ae.Shop.Api.Dal.Repositorys.Product;
using Ae.Shop.Api.Dal.Repositorys;
using DAL = Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Repositorys.Company;
using Ae.Shop.Api.Client.Request;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopPurchaseService : IShopPurchaseService
    {
        #region
        private readonly IVenderRepository _venderRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderProductRepository _purchaseOrderProductRepository;
        private readonly IProductClient _productClient;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly ApolloErpLogger<ShopPurchaseService> _logger;
        private readonly IPurchaseOrderLogRepository purchaseOrderLogRepository;
        private readonly IStockManageService _stockManageService;
        private readonly ProductDao.IFctShopProductRepository fctShopProductRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IFianceClient fianceClient;
        private readonly IInventoryFlowItemRepository _inventoryFlowItemRepository;
        private readonly IShopRepository shopRepository;
        private readonly IPurchaseMonthPayRepository _monthPayRepository;
        private readonly IWmsClient wmsClient;
        private readonly ICompanyRepository _companyRepository;


        private readonly IStockInoutRepository stockInoutRepository;
        private readonly IStockInoutItemRepository stockInoutItemRepository;
        private readonly IInventoryBatchFlowRepository _inventoryBatchFlowRepository;
        private readonly IInventoryBatchRepository inventoryBatchRepository;
        private readonly IStockManageRepository _stockManageRepository;

        public ShopPurchaseService(IVenderRepository venderRepository, IMapper mapper, IIdentityService identityService,
           IPurchaseOrderRepository _purchaseOrderRepository, IPurchaseOrderProductRepository purchaseOrderProductRepository,
           IProductClient productClient, IShopMangeClient shopMangeClient, ApolloErpLogger<ShopPurchaseService> logger,
           IPurchaseOrderLogRepository purchaseOrderLogRepository,
           IStockManageService stockManageService, ProductDao.IFctShopProductRepository fctShopProductRepository,
           ICategoryRepository categoryRepository, IFianceClient fianceClient,
           IPurchaseMonthPayRepository monthPayRepository, IWmsClient wmsClient,
           IInventoryFlowItemRepository inventoryFlowItemRepository,
           IShopRepository _shopRepository, ICompanyRepository companyRepository,
           IStockInoutRepository stockInoutRepository, IStockInoutItemRepository stockInoutItemRepository,
          IInventoryBatchFlowRepository inventoryBatchFlowRepository, IInventoryBatchRepository inventoryBatchRepository,
         IStockManageRepository stockManageRepository)
        {
            this._venderRepository = venderRepository;
            this._mapper = mapper;
            this._identityService = identityService;
            this._purchaseOrderRepository = _purchaseOrderRepository;
            this._purchaseOrderProductRepository = purchaseOrderProductRepository;
            this._productClient = productClient;
            this._shopMangeClient = shopMangeClient;
            _logger = logger;
            this.purchaseOrderLogRepository = purchaseOrderLogRepository;
            this._stockManageService = stockManageService;
            this.fctShopProductRepository = fctShopProductRepository;
            this.categoryRepository = categoryRepository;
            this.fianceClient = fianceClient;
            this._monthPayRepository = monthPayRepository;

            this._inventoryFlowItemRepository = inventoryFlowItemRepository;

            this.shopRepository = _shopRepository;
            this.wmsClient = wmsClient;
            this._companyRepository = companyRepository;

            this.stockInoutRepository = stockInoutRepository;
            this.stockInoutItemRepository = stockInoutItemRepository;
            this._inventoryBatchFlowRepository = inventoryBatchFlowRepository;
            this.inventoryBatchRepository = inventoryBatchRepository;
            this._stockManageRepository = stockManageRepository;
        }
        #endregion

        /// <summary>
        ///  获取所有的供应商信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<VenderDTO>>> GetSupplierList()
        {
            var res = new ApiResult<List<VenderDTO>>();
            try
            {
                //获取门店所属公司下的所有供应商

                var organizationId = _identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                var shopIds = new List<long>();
                if (_identityService.GetOrgType() == "0")
                {
                    //shopIds = await GetCurrentCompanyAllShops(shopId, true);
                    shopIds.Add(shopId);
                }
                else if (_identityService.GetOrgType() == "1")
                {
                    var shopInfo = await shopRepository.GetAsync(shopId);
                    shopIds.Add(shopInfo.CompanyId);
                    //shopIds = await GetCurrentCompanyAllShops(shopId, false);
                }
                var condition = " where is_deleted=0 and is_active=1 and shop_id  in @shop_ids";
                var venderDOs = await _venderRepository.GetListAsync(condition, new { shop_ids = shopIds });
                var result = _mapper.Map<List<VenderDTO>>(venderDOs ?? new List<VenderDO>());

                res.Data = result;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetSupplierList_Error", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<List<long>> GetCurrentCompanyAllShops(long shopId, bool isCompany)
        {
            List<long> shopIds = new List<long>();
            long companyId = shopId;

            if (!isCompany)
            {
                var shopInfo = await shopRepository.GetAsync(shopId);
                companyId = shopInfo.CompanyId;
            }

            var shopInfos = await shopRepository.GetListAsync(" where status=0 and  online=1 and  check_status=2 and is_deleted=0 and company_id =@CompanyId and type = 2",
                new { CompanyId = companyId });
            shopIds = shopInfos.Select(r => r.Id).ToList();

            return shopIds;

        }

        /// <summary>
        /// 分页查询门店采购信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<PurchaseOrderDTO>> SearchPurchaseOrder(PurchaseOrderSearchRequest request)
        {
            var res = new ApiPagedResult<PurchaseOrderDTO>();
            try
            {
                var organizationId = _identityService.GetOrganizationId();
                var orgType = _identityService.GetOrgType();
                long.TryParse(organizationId, out long shopId);
                var shopIds = new List<long>();
                //公司
                if (orgType == "0")
                {
                    shopIds = await GetCurrentCompanyAllShops(shopId, true);
                    shopIds.Add(shopId);//公司也可下采购单，包含boss
                }
                //门店
                else if (orgType == "1")
                {
                    shopIds.Add(shopId);
                }

                //var shopId = 0;//_identityService.GetOrganizationId();
                var result = new ApiPagedResultData<PurchaseOrderDTO>();
                var condition = " where  is_deleted=0 ";

                if (shopIds != null && shopIds.Any())
                {
                    condition += " and (shop_id in @shopIds or (purchase_type = 3 and vender_id = @SelfId))";
                }

                if (request.SaleCompanyId > 0)
                {
                    condition += " and (purchase_type = 3 and vender_id = @SaleCompanyId) ";
                }

                if (request.VenderId > 0)
                {
                    condition += " and (purchase_type <> 3 and vender_id=@vender_id) ";
                }

                var tempshopIds = new List<long>();
                if (request.ShopId > 0)
                {
                    condition += " and shop_id in @tempshopids";
                    tempshopIds.Add(request.ShopId);
                }

                if (request.PurchaseType > 0)
                {
                    condition += " and purchase_type=@PurchaseType ";
                }
                if (request.WarehouseId > 0)
                {
                    condition += " and warehouse_id=@WarehouseId ";
                }
                if (request.PurchaseOrderId > 0)
                {
                    condition += " and id=@id ";
                }
                if (!string.IsNullOrEmpty(request.Key))
                {
                    condition += @" and id in (
                                  select distinct op.po_id from purchase_order_product op,purchase_order o where op.is_deleted = 0  and o.id=op.po_id
                                    and (op.product_name like @key or op.product_code like @key)
                                     and (o.shop_id in @shopIds or (o.purchase_type = 3 and o.vender_id = @SelfId))
                                ) ";
                }
                if (request.Status > 0)
                {
                    condition += " and status=@status ";
                }
                if (request.PayStatus > 0)
                {
                    condition += " and pay_status=@pay_status ";
                }
                if (request.PurchaseOrderStartTime.HasValue && request.PurchaseOrderEenTime.HasValue)
                {
                    condition += "  and create_time >=@startDate and create_time <= @endDate";
                }
                if (request.PayType > 0)
                {
                    condition += " and pay_type=@pay_type ";
                }

                var paras = new
                {
                    tempshopids = tempshopIds,
                    vender_id = request.VenderId,
                    SelfId = shopId,
                    SaleCompanyId = request.SaleCompanyId,
                    WarehouseId = request.WarehouseId,
                    PurchaseType = request.PurchaseType,
                    id = request.PurchaseOrderId,
                    key = '%' + request.Key + '%',
                    status = request.Status,
                    pay_status = request.PayStatus,
                    startDate = request.PurchaseOrderStartTime?.ToString("yyyy-MM-dd"),
                    endDate = request.PurchaseOrderEenTime?.ToString("yyyy-MM-dd"),
                    pay_type = request.PayType,
                    shopIds = shopIds
                };

                _logger.Info($"SearchPurchaseOrder:{condition},{JsonConvert.SerializeObject(paras)}");
                var purchaseOrderPage = await _purchaseOrderRepository.GetListPagedAsync(request.PageIndex, request.PageSize, condition, "id desc", paras);
                result.Items = _mapper.Map<ICollection<PurchaseOrderDTO>>(purchaseOrderPage.Items);
                result.TotalItems = purchaseOrderPage.TotalItems;
                //Dictionary<long, DAL.ShopDO> shopDic = new Dictionary<long, DAL.ShopDO>();


                if (result.Items != null && result.Items.Any())
                {
                    foreach (var item in result.Items)
                    {
                        //if (shopDic.ContainsKey(item.ShopId))
                        //{
                        //    item.ShopName = shopDic[item.ShopId].SimpleName;
                        //}
                        //else
                        //{
                        //    var shopInfo = await shopRepository.GetAsync(item.ShopId);
                        //    shopDic[item.ShopId] = shopInfo;
                        //    item.ShopName = shopInfo.SimpleName;
                        //}

                        var products = await _purchaseOrderProductRepository.GetListAsync(" where po_id =@poId and is_deleted=0 and status<>3", new { poId = item.Id });

                        StringBuilder builder = new StringBuilder();
                        if (products != null && products.Any())
                        {

                            foreach (var product in products)
                            {
                                builder.Append($"{product.ProductName}_{product.Num}_{product.PurchaseCost}" + "\n");
                            }

                            item.joinText = builder.ToString().TrimEnd(',');
                        }

                    }
                }

                res.Data = result;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SearchPurchaseOrder_Error Data:{request}", ex);
            }
            return res;
        }

        /// <summary>
        /// 编辑采购单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> SavePurchaseOrder(PurchaseOrderEditRequest request)
        {
            var res = new ApiResult<bool>();
            if (request == null || request.PurchaseOrder == null)
            {
                throw new CustomException("采销信息必填！");
            }
            try
            {
                var shopInfo = await _stockManageService.GetShopById();
                request.PurchaseOrder.ShopId = shopInfo?.ShopId ?? 0;
                request.PurchaseOrder.ShopName = shopInfo?.SimpleName ?? "";
               
                var userName = _identityService.GetUserName();
                var dtNow = DateTime.Now;
                var result = 0;
                //采购信息
                var purchaseOrderDO = _mapper.Map<PurchaseOrderDO>(request.PurchaseOrder);
                var purchaseOrderProductDOs = _mapper.Map<List<PurchaseOrderProductDO>>(request.PurchaseOrderProducts);

                if (!request.IsEdit)
                {
                    purchaseOrderDO.CreateBy = userName;
                    purchaseOrderDO.UpdateBy = userName;
                    purchaseOrderDO.CreateTime = dtNow;
                    purchaseOrderDO.UpdateTime = dtNow;
                    //purchaseOrderDO.ShopId = shopId;
                    purchaseOrderDO.PayStatus = 1; //默认待付款
                    purchaseOrderDO.Buyer = string.IsNullOrEmpty(purchaseOrderDO.Buyer) ? userName : purchaseOrderDO.Buyer;

                    if (string.IsNullOrWhiteSpace(purchaseOrderDO.VenderShortName))//供应商名称
                    {
                        if (purchaseOrderDO.PurchaseType == 3)
                        {
                            var companyDo = await _companyRepository.GetAsync(purchaseOrderDO.VenderId);
                            purchaseOrderDO.VenderShortName = companyDo.SimpleName;
                        }
                        else
                        {
                            var venderDo = await _venderRepository.GetAsync(purchaseOrderDO.VenderId);
                            purchaseOrderDO.VenderShortName = venderDo.VenderShortName;
                        }
                    }
                        
                    if (purchaseOrderDO.WarehouseId <= 0)//仓库
                    {
                        purchaseOrderDO.WarehouseId = request.PurchaseOrder.ShopId;
                        purchaseOrderDO.WarehouseName = request.PurchaseOrder.ShopName;
                    }
                    //_logger.Info($"wmsClient.GetWarehouseConfig,{JsonConvert.SerializeObject(purchaseOrderDO)}");
                    if (string.IsNullOrWhiteSpace(purchaseOrderDO.WarehouseName))//仓库名称
                    {
                        //var warehouse = await wmsClient.GetWarehouseConfig(new WarehouseConfigRequest { Id = purchaseOrderDO.WarehouseId, CompanyId = purchaseOrderDO.ShopId });
                        //if (warehouse != null && warehouse.Code == ResultCode.Success)
                        //{
                        //    purchaseOrderDO.WarehouseName = warehouse.Data?.WarehouseName ?? "";
                        //}
                        var shopResult = await _shopMangeClient.GetShopById(new GetShopClientRequest
                        {
                            ShopId = purchaseOrderDO.WarehouseId
                        });
                        if (shopResult.Code == ResultCode.Success)
                        {
                            purchaseOrderDO.WarehouseName = shopResult.Data?.SimpleName ?? "";
                        }
                    }
                    if (purchaseOrderDO.PurchaseType == 3)//内销单
                    {
                        //取内销客户的默认仓库
                        var companyResult = await _companyRepository.GetShopWareHouseListAsync(new GetShopListRequest
                        { 
                            CompanyId = purchaseOrderDO.VenderId,
                            QueryType = -1
                        });
                        if (!companyResult.Any())
                        {
                            throw new CustomException("内销单客户未设置仓库，请先设置仓库！");
                        }
                        var vw = companyResult.FirstOrDefault();
                        purchaseOrderDO.VenderWarehouseId = vw.Id;
                        purchaseOrderDO.VenderWarehouseName = vw.SimpleName;
                    }
                    var totalAmt = purchaseOrderProductDOs.Sum(t => t.Num * t.Price);
                    purchaseOrderDO.TotalAmount = totalAmt;
                    purchaseOrderDO.ActualAmount = totalAmt - purchaseOrderDO.CouponAmount - purchaseOrderDO.OwnFreight;

                    var pid = await _purchaseOrderRepository.InsertAsync(purchaseOrderDO);

                    if (!purchaseOrderProductDOs.IsNullOrEmpty())
                    {
                        var productIds = purchaseOrderProductDOs.Select(r => r.ProductCode).ToList();
                        var targetProducts = await fctShopProductRepository.GetShopProductByProductCodes(productIds);

                        purchaseOrderProductDOs.ForEach((t) =>
                        {
                            t.PoId = pid;
                            t.CreateBy = userName;
                            t.UpdateBy = userName;
                            t.CreateTime = dtNow;
                            t.UpdateTime = dtNow;

                            t.SalePrice = t.Price;
                            t.PurchaseCost = t.Price;
                            t.PurchasePrice = t.Price;
                            t.Amount = t.Price * t.Num;
                            t.TotalCost = t.PurchaseCost * t.Num;
                            t.TotalPrice = t.PurchasePrice * t.Num;

                            var productInfo = targetProducts.FirstOrDefault(r => r.ProductCode == t.ProductCode);
                            //同步部分产品信息
                            if (productInfo != null)
                            {
                                t.Specification = productInfo.Specification;
                                t.OeNumber = productInfo.OeNumber;
                                var categoryInfo = categoryRepository.GetAsync(productInfo.CategoryId);
                                t.CategoryName = categoryInfo.Result.DisplayName;
                            }
                        });
                        await _purchaseOrderProductRepository.InsertBatchAsync(purchaseOrderProductDOs);
                    }
                    result = pid;

                    await InsertPurchaseLog(new PurchaseOrderLogDO
                    {
                        ObjectId = pid.ToString(),
                        ObjectType = LogTypeEnum.Purchase.ToString(),
                        LogLevel = LogLevelEnum.Info.ToString(),
                        BeforeValue = string.Empty,
                        AfterValue = JsonConvert.SerializeObject(request),
                        Remark = "新增采销单",
                        CreateBy = userName,
                        CreateTime = DateTime.Now
                    });
                }
                else
                {
                    purchaseOrderDO.UpdateBy = userName;
                    purchaseOrderDO.UpdateTime = dtNow;
                    var updateFile = new List<string>(){ "Status", "PinvoiceType" , "PayType", "PayStatus", "TotalAmount", "WaybillNumber", "OwnFreight",
                    "Remark","UpdateBy" ,"UpdateTime","Buyer"};
                    result = await _purchaseOrderRepository.UpdateAsync(purchaseOrderDO, updateFile);
                    var condition = " where is_deleted=0 and po_id=" + purchaseOrderDO.Id;
                    var purchaseOrderProductDoOlds = await _purchaseOrderProductRepository.GetListAsync(condition);

                    //如果编辑了  先删除后增加
                    if (!purchaseOrderProductDoOlds.IsNullOrEmpty())
                    {
                        //禁用采购商品明细
                        purchaseOrderProductDoOlds?.ToList()?.ForEach(async (t) =>
                        {
                            t.IsDeleted = 1;
                            t.UpdateBy = userName;
                            t.UpdateTime = dtNow;
                            await _purchaseOrderProductRepository.UpdateAsync(t, new List<string>() { "IsDeleted", "UpdateBy", "UpdateTime" });
                        });
                    }
                    if (!purchaseOrderProductDOs.IsNullOrEmpty())
                    {
                        var productIds = purchaseOrderProductDOs.Select(r => r.ProductCode).ToList();
                        var targetProducts = await fctShopProductRepository.GetShopProductByProductCodes(productIds);

                        purchaseOrderProductDOs.ForEach((t) =>
                        {
                            t.PoId = purchaseOrderDO.Id;
                            t.CreateBy = userName;
                            t.UpdateBy = userName;
                            t.CreateTime = dtNow;
                            t.UpdateTime = dtNow;
                            var productInfo = targetProducts.FirstOrDefault(r => r.ProductCode == t.ProductCode);
                            //同步部分产品信息
                            if (productInfo != null)
                            {
                                t.Specification = productInfo.Specification;
                                t.OeNumber = productInfo.OeNumber;
                                var categoryInfo = categoryRepository.GetAsync(productInfo.CategoryId);
                                t.CategoryName = categoryInfo.Result.DisplayName;
                            }
                        });
                        await _purchaseOrderProductRepository.InsertBatchAsync(purchaseOrderProductDOs);
                    }

                    var remark = string.Empty;

                    //提交时 采购单变成待发货
                    if (purchaseOrderDO.Status == 2)
                    {
                        remark = "提交采购单";
                    }
                    else
                    {
                        remark = "编辑采购单";
                    }
                    //await InsertPurchaseLog(new PurchaseOrderLogDO
                    //{
                    //    ObjectId = purchaseOrderDO.Id.ToString(),
                    //    ObjectType = LogTypeEnum.Purchase.ToString(),
                    //    LogLevel = LogLevelEnum.Info.ToString(),
                    //    BeforeValue = string.Empty,
                    //    AfterValue = JsonConvert.SerializeObject(request),
                    //    Remark = remark,
                    //    CreateBy = this._identityService.GetUserName(),
                    //    CreateTime = DateTime.Now
                    //});
                }

                res.Data = true;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Data = false;
                res.Code = ResultCode.Exception;
                _logger.Error($"SavePurchaseOrder_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }


        /// <summary>
        /// 获取采购单信息
        /// </summary>
        /// <param name="PurchaseOrderId">采购单Id</param>
        /// <returns></returns>
        public async Task<ApiResult<PurchaseOrderDetailResponse>> GetPurchaseOrderDetail(string purchaseOrderId)
        {
            var res = new ApiResult<PurchaseOrderDetailResponse>();
            try
            {
                var result = new PurchaseOrderDetailResponse();
                if (string.IsNullOrEmpty(purchaseOrderId))
                {
                    return null;
                }
                var purchaseOrderDO = await _purchaseOrderRepository.GetAsync(purchaseOrderId);
                if (purchaseOrderDO?.Id > 0)
                {
                    //采购单信息
                    result.PurchaseOrder = _mapper.Map<PurchaseOrderDTO>(purchaseOrderDO);

                    //var shopInfo = await shopRepository.GetAsync(purchaseOrderDO.ShopId);
                    //result.PurchaseOrder.ShopName = shopInfo.SimpleName;
                }
                var condition = " where is_deleted=0 and po_id=" + purchaseOrderId;
                var purchaseOrderProductDOs = await _purchaseOrderProductRepository.GetListAsync(condition);
                if (!purchaseOrderProductDOs.IsNullOrEmpty())
                {
                    //采购商品信息
                    result.PurchaseOrderProducts = _mapper.Map<List<PurchaseOrderProductDTO>>(purchaseOrderProductDOs);
                }

                //获取采购单操作日志
                var logs = await purchaseOrderLogRepository.GetListAsync(" where object_id =@object_id and object_type =@object_type",
                    new { object_id = purchaseOrderId, object_type = LogTypeEnum.Purchase.ToString() });

                if (logs != null && logs.Any())
                {
                    result.Logs = _mapper.Map<List<PurchaseOrderLogDTO>>(logs.OrderByDescending(_ => _.CreateTime));
                }
                res.Code = ResultCode.Success;
                res.Data = result;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetPurchaseOrderDetail_Error purchaseOrderId:{purchaseOrderId}", ex);
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
            if (request.PurchaseType != 2) // 查询总部商品
            {
                var para = _mapper.Map<ProductSearchClientRequest>(request);
                para.ProductAttributes = new List<string>() { "0" };// 只查询实物产品
                para.ClassType = 2;
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
                        Unit = t.Unit ?? "个"
                    }).ToList();
                    result.TotalItems = product.TotalItems;
                }
            }
            else if (request.PurchaseType == 2)// 非总部
            {
                var organizationId = _identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                // var shopIds = await GetCurrentCompanyAllShops(shopId, false);

                List<long> shopIds = new List<long>();
                shopIds.Add(shopId);
                //产品是公用的
                var para = _mapper.Map<ShopProductSearchClientRequest>(request);
                //para.ShopId = shopId;
                para.IsDeleted = 0;
                para.shopIds = shopIds;
                // para.AuditStatus = 1;  //审核通过
                para.IsStoreoutside = 1;  //是外采商品
                para.IsDisabled = 0;
                var pageShopProduct = await _productClient.SearchShopProduct(para);
                if (pageShopProduct != null && pageShopProduct.Data.TotalItems > 0)
                {
                    var serviceTypeDtos = await _shopMangeClient.GetShopServiceTypeAsync(new Client.Request.ShopManage.ShopServiceTypeClientRequest()
                    { ShopId = shopId });
                    result.Items = pageShopProduct.Data.Items.Select(t => new ProductAllInfoVo()
                    {
                        CategoryName = ConverToShopServiceType(serviceTypeDtos, t.CategoryId),
                        IsOnSale = t.OnSale,
                        ProductCode = t.ProductCode,
                        ProductName = t.ProductName,
                        SalesPrice = t.SalesPrice,
                        StandardPrice = t.StandardPrice,
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
        /// 修改采购单状态
        /// </summary>
        /// <param name="purchaseOrderId">采购单号</param>
        /// <param name="status">采购单状态(1新建 2待发货 3已取消 4已发货 5部分收货 6已收货 )  </param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateShopPurchaseStatusById(long purchaseOrderId, int status, string createBy)
        {

            var res = new ApiResult<bool>();
            try
            {
                var purchaseOrderDO = await _purchaseOrderRepository.GetAsync(purchaseOrderId);
                if (purchaseOrderDO == null || purchaseOrderDO.Id == 0)
                {
                    throw new CustomException("采购单不存在！");
                }
                var updateFile = new List<string>() { "Status", "UpdateBy", "UpdateTime" };
                //var userId = _identityService.GetUserId();
                purchaseOrderDO.UpdateTime = DateTime.Now;
                purchaseOrderDO.UpdateBy = createBy;
                purchaseOrderDO.Status = status;
                await _purchaseOrderRepository.UpdateAsync(purchaseOrderDO, updateFile);

                //var statusText = ((PurchaseStatusEnum)status).GetEnumDescription();
                //await InsertPurchaseLog(new PurchaseOrderLogDO
                //{
                //    ObjectId = purchaseOrderId.ToString(),
                //    ObjectType = LogTypeEnum.Purchase.ToString(),
                //    LogLevel = LogLevelEnum.Info.ToString(),
                //    BeforeValue = $"purchaseOrderId:{purchaseOrderId},status:{status}",
                //    AfterValue = string.Empty,
                //    Remark = "采购单" + (!string.IsNullOrWhiteSpace(statusText) ? statusText : string.Empty),
                //    CreateBy = this._identityService.GetUserName(),
                //    CreateTime = DateTime.Now
                //});

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateShopPurchaseStatusById_Error purchaseOrderId:{purchaseOrderId},status:{status}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiPagedResult<VenderVO>> SearchVender(SearchVenderRequest request)
        {
            var result = Result.Failed(ResultCode.Failed, CommonConstant.QueryFailed, new List<VenderVO>(), 0);
            try
            {
                var organizationId = _identityService.GetOrganizationId();
                var orgType = _identityService.GetOrgType();
                long.TryParse(organizationId, out long shopId);
                List<long> shopIds = new List<long>();
                if (orgType == "0")//公司
                {
                    shopIds.Add(shopId);
                    //shopIds = await GetCurrentCompanyAllShops(shopId, true);
                }
                else if (orgType == "1")//门店
                {
                    //shopIds = await GetCurrentCompanyAllShops(shopId, false);
                    var shopInfo = await shopRepository.GetAsync(shopId);

                    shopIds.Add(shopInfo.CompanyId);
                }

                var condition = new StringBuilder("where 1=1 ");

                condition.Append("and shop_id in @ShopIds ");
                request.ShopIds = shopIds;

                if (!string.IsNullOrWhiteSpace(request.VenderName))
                {
                    condition.Append("and (vender_short_name like concat('%',@VenderName,'%') or vender_name like concat('%',@VenderName,'%')) ");
                }
                if (!string.IsNullOrWhiteSpace(request.Contact))
                {
                    condition.Append("and (contact like concat('%',@Contact,'%') or tel_num like concat('%',@Contact,'%')) ");
                }
                switch (request.Status)
                {
                    case 1:
                        condition.Append("and is_active=1 and is_deleted=0 ");
                        break;
                    case 2:
                        condition.Append("and is_active=0 and is_deleted=0 ");
                        break;
                    case 3:
                        condition.Append("and is_deleted=1 ");
                        break;
                    default:
                        condition.Append("and is_deleted=0 ");
                        break;
                }
                if (request.SettlementMethod > 0)
                {
                    condition.Append("and settlement_method=@SettlementMethod ");
                }

                Dictionary<long, DAL.ShopDO> shopDic = new Dictionary<long, DAL.ShopDO>();

                _logger.Info($"GetOrgType={orgType},{JsonConvert.SerializeObject(request)}");

                var pageList = await _venderRepository.GetListPagedAsync(request.PageIndex, request.PageSize, condition.ToString(), "id desc", request);
                if (pageList != null)
                {
                    var response = _mapper.Map<List<VenderVO>>(pageList.Items);

                    //foreach (var item in response)
                    //{
                    //    if (shopDic.ContainsKey(item.ShopId))
                    //    {
                    //        item.ShopName = shopDic[item.ShopId].SimpleName;
                    //    }
                    //    else
                    //    {
                    //        var shopInfo = await shopRepository.GetAsync(item.ShopId);
                    //        shopDic[item.ShopId] = shopInfo;
                    //        item.ShopName = shopInfo.SimpleName;
                    //    }
                    //}

                    result = Result.Success(response, pageList.TotalItems);
                }
                else
                {
                    result = Result.Success(new List<VenderVO>(), 0);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("SearchVenderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> UpsertVender(VenderVO data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                if (_identityService.GetOrgType() == "1")
                {
                    return new ApiResult { Code = ResultCode.Failed, Message = "登录用户为门店，不允许创建/修改供应商!" };
                }

                var venderDO = _mapper.Map<VenderDO>(data);
                var organizationId = _identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                //var userName = $"{_identityService.GetUserName()}@{_identityService.GetUserId()}";

                var userName = _identityService.GetUserName();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                if (venderDO.Id > 0)
                {
                    venderDO.UpdateBy = userName;
                    venderDO.UpdateTime = DateTime.Now;
                    var count = await _venderRepository.UpdateAsync(venderDO);
                    if (count > 0)
                    {
                        result = Result.Success(CommonConstant.OperateSuccess);
                    }
                }
                else
                {
                    var venderRes = await _venderRepository.GetListAsync(" where vender_short_name =@vender_short_name and is_deleted =0 and shop_id=@shop_id",
                        new { vender_short_name = venderDO.VenderShortName, shop_id = shopId });
                    if (venderRes != null && venderRes.Any())
                    {
                        return new ApiResult
                        {
                            Code = ResultCode.Exception,
                            Message = "供应商已存在!"
                        };
                    }

                    venderDO.ShopId = shopId;
                    venderDO.CreateBy = userName;
                    venderDO.CreateTime = DateTime.Now;
                    var count = await _venderRepository.InsertAsync(venderDO);
                    if (count > 0)
                    {
                        result = Result.Success(CommonConstant.OperateSuccess);
                    }
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("UpsertVenderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<VenderVO>> GetVender(BaseGetByIdRequest request)
        {
            var result = Result.Failed<VenderVO>(CommonConstant.QueryFailed);
            try
            {
                var venderDO = await _venderRepository.GetAsync(request.Id);
                if (venderDO != null)
                {
                    var venderVO = _mapper.Map<VenderVO>(venderDO);
                    result = Result.Success(venderVO, CommonConstant.QuerySuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("GetVenderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> ActiveVender(VenderVO data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var venderDO = _mapper.Map<VenderDO>(data);
                if (venderDO.Id < 0)
                {
                    throw new CustomException("参数错误");
                }
                var userName = $"{_identityService.GetUserName()}@{_identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                venderDO.UpdateBy = userName;
                venderDO.UpdateTime = DateTime.Now;
                var count = await _venderRepository.UpdateAsync(venderDO, new List<string>() { "UpdateBy", "UpdateTime", "IsActive" });
                if (count > 0)
                {
                    result = Result.Success(CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("ActiveVenderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<string>> UpdatePurchasePrice(List<PurchaseOrderProductDTO> request)
        {
            var res = new ApiResult<string>();
            var updateBy = this._identityService.GetUserName();
            try
            {
                var updateRequest = new List<PurchaseOrderProductDO>();
                var changed = request.Where(c => c.OldPurchaseCost != c.PurchaseCost);
                if (changed == null)
                {
                    res.Code = ResultCode.Success;
                    return res;
                }
                foreach (var item in changed)
                {
                    updateRequest.Add(new PurchaseOrderProductDO
                    {
                        Id = item.Id,
                        PoId = item.PoId,
                        UpdateBy = updateBy,
                        PurchaseCost = item.PurchaseCost,
                        ProductCode = item.ProductCode
                    });
                }

                await _purchaseOrderRepository.UpdatePurchasePrice(updateRequest);

                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = changed.FirstOrDefault().PoId.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = string.Empty,
                    AfterValue = JsonConvert.SerializeObject(changed),
                    Remark = $"修改价格:{changed.FirstOrDefault().OldPurchaseCost.ToString()}-->{changed.FirstOrDefault().PurchaseCost.ToString()}",
                    CreateBy = updateBy,
                    CreateTime = DateTime.Now
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdatePurchasePrice_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<string>> UpdatePurchaseOrderDeliveryInfo(PurchaseOrderDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                await _purchaseOrderRepository.UpdatePurchaseOrderDeliveryInfo(new PurchaseOrderDO
                {
                    Id = request.Id,
                    UpdateBy = this._identityService.GetUserName(),
                    WaybillNumber = request.WaybillNumber,
                    OwnFreight = request.OwnFreight
                });

                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = request.Id.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = string.Empty,
                    AfterValue = JsonConvert.SerializeObject(request),
                    Remark = $"采购单发货:运单号:{request.WaybillNumber}",
                    CreateBy = this._identityService.GetUserName(),
                    CreateTime = DateTime.Now
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdatePurchaseOrderDeliveryInfo_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        /// <summary>
        /// 删除采购单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeletePurchaseOrder(PurchaseOrderDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //主单取消
                await _purchaseOrderRepository.DeletePurchaseOrder(new PurchaseOrderDO
                {
                    UpdateBy = this._identityService.GetUserName(),
                    Id = request.Id
                });

                //取消子单
                await _purchaseOrderProductRepository.DeletePurchaseOrderProduct(new PurchaseOrderProductDO
                {
                    UpdateBy = this._identityService.GetUserName(),
                    PoId = request.Id
                });

                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = request.Id.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = string.Empty,
                    AfterValue = JsonConvert.SerializeObject(request),
                    Remark = "删除采购单",
                    CreateBy = this._identityService.GetUserName(),
                    CreateTime = DateTime.Now
                });

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"DeletePurchaseOrder_Error Data:{JsonConvert.SerializeObject(request)}", ex);
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

        public async Task<ApiResult<string>> QuickCreateVender(VenderDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                return new ApiResult<string> { Code = ResultCode.Exception, Message = "APP上不允许创建供应商!" };

                var organizationId = _identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);

                var venderRes = await _venderRepository.GetListAsync(" where vender_short_name =@vender_short_name and is_deleted =0 and shop_id=@shop_id",
                        new { vender_short_name = request.VenderShortName, shop_id = shopId });
                if (venderRes != null && venderRes.Any())
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = "供应商已存在!"
                    };
                }
                await _venderRepository.InsertAsync(new VenderDO
                {
                    VenderShortName = request.VenderShortName,
                    IsActive = 1,
                    IsDeleted = 0,
                    CreateBy = request.CreateBy,
                    CreateTime = DateTime.Now,
                    ShopId = request.ShopId,
                    ClassType = "普通",
                    Contact = request.Contact,
                    OfficeAddress = request.OfficeAddress,
                    ProvinceId = request.ProvinceId,
                    ProvinceName = request.ProvinceName,
                    CityId = request.CityId,
                    CityName = request.CityName,
                    DistrictId = request.DistrictId,
                    DistrictName = request.DistrictName,
                    InvoiceType = 1,  //无需发票
                    SettlementMethod = request.SettlementMethod,  //现结
                    TelNum = request.TelNum
                });
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"QuickCreateVender_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<VenderDTO>>> GetVenders(VenderDTO request)
        {
            var res = new ApiResult<List<VenderDTO>>();
            try
            {
                var shopIds = new List<long>();
                if (request.ShopId > 0)
                {
                    var shopInfo = await shopRepository.GetAsync(request.ShopId);

                    shopIds.Add(shopInfo.CompanyId);
                }
                else
                {
                    var organizationId = _identityService.GetOrganizationId();
                    long.TryParse(organizationId, out long shopId);

                    if (_identityService.GetOrgType() == "0")
                    {
                        shopIds.Add(shopId);
                    }
                    else if (_identityService.GetOrgType() == "1")
                    {
                        var shopInfo = await shopRepository.GetAsync(shopId);

                        shopIds.Add(shopInfo.CompanyId);
                    }
                }
                var venders = await _venderRepository.GetListAsync(" where is_deleted=0 and is_active =1 and shop_id in @shop_ids ", new { shop_ids = shopIds });
                var result = _mapper.Map<List<VenderDTO>>(venders);
                res.Data = result;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetVenders_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> CreatePurchaseOrder(PurchaseOrderEditRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var purchaseOrder = request.PurchaseOrder;
                var poResult = await _purchaseOrderRepository.InsertAsync(new PurchaseOrderDO
                {
                    CreateBy = purchaseOrder.CreateBy,
                    CreateTime = DateTime.Now,
                    IsDeleted = 0,
                    ShopId = purchaseOrder.ShopId,
                    Status = 6,
                    Buyer = purchaseOrder.CreateBy,
                    VenderId = purchaseOrder.VenderId,
                    VenderShortName = purchaseOrder.VenderShortName,
                    PayStatus = 3,
                    PayType = 1,
                    PinvoiceType = 0,
                    PurchaseType = 0,
                    TotalAmount = request.PurchaseOrderProducts.Sum(r => r.Num * r.PurchasePrice)
                });

                var stockInOut = new StockInOutDTO
                {
                    SourceInventoryNo = poResult.ToString(),
                    CreateBy = purchaseOrder.CreateBy,
                    OperateTime = DateTime.Now,
                    SourceType = StockInTypeEnum.采购入库.ToString(),
                    LocationId = purchaseOrder.ShopId
                };

                if (request.PurchaseOrderProducts.Any())
                {
                    var productIds = request.PurchaseOrderProducts.Select(r => r.ProductCode).ToList();
                    var targetProducts = await fctShopProductRepository.GetShopProductByProductCodes(productIds);

                    foreach (var item in request.PurchaseOrderProducts)
                    {
                        var purchaseProduct = new PurchaseOrderProductDO
                        {
                            PurchasePrice = item.PurchasePrice,
                            PurchaseCost = item.PurchasePrice,
                            IsDeleted = 0,
                            CreateBy = purchaseOrder.CreateBy,
                            CreateTime = DateTime.Now,
                            PlanInstockDate = DateTime.Now,
                            Num = item.Num,
                            PoId = poResult,
                            ProductCode = item.ProductCode,
                            ProductName = item.ProductName,
                            Status = PurchaseOrderStatusEnum.已收货.ToString(),
                            TaxPoint = 1,
                            TotalCost = item.PurchasePrice * item.Num,
                            TotalPrice = item.PurchasePrice * item.Num
                        };

                        var productInfo = targetProducts.FirstOrDefault(r => r.ProductCode == item.ProductCode);
                        //同步部分产品信息
                        if (productInfo != null)
                        {
                            purchaseProduct.Specification = productInfo.Specification;
                            purchaseProduct.OeNumber = productInfo.OeNumber;
                            purchaseProduct.Unit = productInfo.Unit;
                            var categoryInfo = categoryRepository.GetAsync(productInfo.CategoryId);
                            purchaseProduct.CategoryName = categoryInfo.Result.DisplayName;
                        }

                        var purchaseProductResult = await _purchaseOrderProductRepository.InsertAsync(purchaseProduct);
                        stockInOut.StockItems.Add(new StockInoutItemDTO
                        {
                            Num = item.Num,
                            ActualNum = item.Num,
                            ProductId = item.ProductCode,
                            ProductName = item.ProductName,
                            CreateBy = purchaseOrder.CreateBy,
                            GoodNum = item.Num,
                            UomText = item.Unit,
                            ReleationId = purchaseProductResult
                        });
                    }
                    //要直接入库的  需要创建入库单
                    await this._stockManageService.CreateInStockTask(stockInOut);

                    //同步财务
                    await fianceClient.CalculationPurchaseReconciliationFee(new CalculatePurchaseOrderRequest
                    {
                        PurchaseId = poResult,
                        CreateUser = purchaseOrder.CreateBy
                    });
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreatePurchaseOrder_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 采购单入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> PurchaseInStock(PurchaseOrderDTO request, bool isSyncFms = true)
        {
            //_logger.Info($"PurchaseInStock,{JsonConvert.SerializeObject(request)}");
            var res = new ApiResult<string>();
            try
            {
                var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.Id);

                var purchaseOrderProducts = await _purchaseOrderProductRepository.GetListAsync(" where po_id =@poId and is_deleted=0 and status<>3", new { poId = request.Id });
                string createBy = string.IsNullOrWhiteSpace(request.CreateBy) ? _identityService.GetUserName() : request.CreateBy;

                if (purchaseOrder == null || purchaseOrderProducts == null || !purchaseOrderProducts.Any())
                {
                    res.Code = ResultCode.Exception;
                    res.Message = "采购单或产品不存在!";
                    return res;
                }

                //组装库存更新数据
                var stockInoutRequest = new StockInOutDTO
                {
                    LocationId = purchaseOrder.WarehouseId>0 ? purchaseOrder.WarehouseId: purchaseOrder.ShopId,
                    LocationName = purchaseOrder.WarehouseName,
                    OperationType = purchaseOrder.PurchaseType < 3 ? StockOperateTypeEnum.入库.ToString() : StockOperateTypeEnum.出库.ToString(),
                    SourceInventoryNo = purchaseOrder.Id.ToString(),
                    SourceType = purchaseOrder.PurchaseType < 3 ? StockInTypeEnum.采购入库.ToString() : StockOutTypeEnum.销售出库.ToString(),
                    Status = StockInOutStatusEnum.已收货.ToString(),
                    CreateBy = createBy,
                    Operator = createBy,
                    OperateTime = DateTime.Now,
                    Remark = ""
                };

                //取得门店产品信息
                var productIds = purchaseOrderProducts.Where(item => item.ProductCode.StartsWith("MD-")).Select(r => r.ProductCode).ToList();
                var shopProducts = await fctShopProductRepository.GetShopProductByProductCodes(productIds);

                foreach (var item in purchaseOrderProducts)
                {
                    stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                    {
                        ReleationId = item.Id,
                        ProductId = item.ProductCode,
                        ProductName = item.ProductName,
                        Num = item.Num,
                        GoodNum = item.Num,
                        ActualNum = item.Num,
                        UomText = item.Unit,
                        Cost = item.PurchaseCost,
                        CreateBy = createBy,
                        Status = StockInOutStatusEnum.已收货.ToString()
                    });
                    //更新门店产品信息的采购成本价格
                    if (purchaseOrder.PurchaseType < 3)
                    {
                        var productDo = shopProducts.FirstOrDefault(r => r.ProductCode == item.ProductCode && r.IsDeleted == 0);
                        if (productDo != null)
                        {
                            productDo.PurchaseCost = item.PurchaseCost;
                            productDo.UpdateBy = createBy;
                            productDo.UpdateTime = DateTime.Now;
                            await fctShopProductRepository.UpdateAsync(productDo, new List<string> { "PurchaseCost", "UpdateBy", "UpdateTime" });
                        }
                    }
                        
                }

                //要直接入库的  需要创建入库单
                //await this._stockManageService.CreateInStockTask(stockInOut);
                await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);

                //更新内销客户入库
                if (purchaseOrder.PurchaseType == 3)
                {
                    stockInoutRequest.LocationId = purchaseOrder.VenderWarehouseId;
                    stockInoutRequest.LocationName = purchaseOrder.VenderWarehouseName;
                    stockInoutRequest.OperationType = StockOperateTypeEnum.入库.ToString();
                    stockInoutRequest.SourceInventoryNo = purchaseOrder.Id.ToString();
                    stockInoutRequest.SourceType = StockInTypeEnum.内销入库.ToString();
                    stockInoutRequest.Status = StockInOutStatusEnum.已收货.ToString();
                    stockInoutRequest.CreateBy = createBy;
                    stockInoutRequest.Operator = createBy;
                    stockInoutRequest.OperateTime = DateTime.Now;
                    stockInoutRequest.Remark = $"内销来自{purchaseOrder.WarehouseName}";

                    await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);
                }


                //回写采购单状态
                await UpdateShopPurchaseStatusById(purchaseOrder.Id,(int) PurchaseStatusEnum.已收货, createBy);

                if (isSyncFms)
                {
                    //同步财务
                    await fianceClient.CalculationPurchaseReconciliationFee(new CalculatePurchaseOrderRequest
                    {
                        PurchaseId = request.Id,
                        CreateUser = createBy
                    });
                }

                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = request.Id.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = string.Empty,
                    AfterValue = JsonConvert.SerializeObject(request),
                    Remark = "采购单已入库",
                    CreateBy = createBy,
                    CreateTime = DateTime.Now
                });

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"PurchaseInStock_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }


        public async Task<ApiResult<string>> CreatePurchaseOrderFouOut(PurchaseOrderEditRequest request)
        {
            var res = new ApiResult<string>();
         
            try
            {
                var purchaseOrder = request.PurchaseOrder;

                if (string.IsNullOrWhiteSpace(purchaseOrder.ShopName))//名称
                {
                    var shopResult = await _shopMangeClient.GetShopById(new GetShopClientRequest
                    {
                        ShopId = purchaseOrder.ShopId
                    });
                    if (shopResult.Code == ResultCode.Success)
                    {
                        purchaseOrder.ShopName = shopResult.Data?.SimpleName ?? "";
                    }
                }

                //取供应商的结算方式
                var venderInfo = await _venderRepository.GetAsync(purchaseOrder.VenderId);
                int payType = venderInfo.SettlementMethod;

                //结算方式取供应商的
                var poResult = await _purchaseOrderRepository.InsertAsync(new PurchaseOrderDO
                {
                    CreateBy = purchaseOrder.CreateBy,
                    CreateTime = DateTime.Now,
                    IsDeleted = 0,
                    ShopId = purchaseOrder.ShopId,
                    ShopName = purchaseOrder.ShopName,
                    WarehouseId = purchaseOrder.ShopId,
                    WarehouseName = purchaseOrder.ShopName,
                    Status = 2,
                    Buyer = purchaseOrder.CreateBy,
                    VenderId = purchaseOrder.VenderId,
                    VenderShortName = purchaseOrder.VenderShortName,
                    PayStatus = 1,
                    PinvoiceType = 0,
                    PurchaseType = 2,
                    PayType = payType,
                    TotalAmount = request.PurchaseOrderProducts.Sum(r => r.Num * r.PurchasePrice)
                });


                if (request.PurchaseOrderProducts.Any())
                {
                    var productIds = request.PurchaseOrderProducts.Select(r => r.ProductCode).ToList();
                    var targetProducts = await fctShopProductRepository.GetShopProductByProductCodes(productIds);

                    foreach (var item in request.PurchaseOrderProducts)
                    {
                        var purchaseProduct = new PurchaseOrderProductDO
                        {
                            PurchasePrice = item.PurchasePrice,
                            PurchaseCost = item.PurchasePrice,
                            Price = item.PurchasePrice,
                            IsDeleted = 0,
                            CreateBy = purchaseOrder.CreateBy,
                            CreateTime = DateTime.Now,
                            PlanInstockDate = DateTime.Now,
                            Num = item.Num,
                            Amount = item.PurchasePrice * item.Num,

                            PoId = poResult,
                            ProductCode = item.ProductCode,
                            ProductName = item.ProductName,
                            Status = PurchaseOrderStatusEnum.已发货.ToString(),
                            TaxPoint = 0,
                            TotalCost = item.PurchasePrice * item.Num,
                            TotalPrice = item.PurchasePrice * item.Num
                        };

                        var productInfo = targetProducts.FirstOrDefault(r => r.ProductCode == item.ProductCode);
                        //同步部分产品信息
                        if (productInfo != null)
                        {
                            purchaseProduct.Specification = productInfo.Specification;
                            purchaseProduct.OeNumber = productInfo.OeNumber;
                            purchaseProduct.Unit = productInfo.Unit;
                            var categoryInfo = categoryRepository.GetAsync(productInfo.CategoryId);
                            purchaseProduct.CategoryName = categoryInfo.Result.DisplayName;
                        }

                        await _purchaseOrderProductRepository.InsertAsync(purchaseProduct);

                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreatePurchaseOrder_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 采购单发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SendPurchaseOrder(PurchaseOrderBatchSendDTO request)
        {
            _logger.Info($"SendPurchaseOrder,{JsonConvert.SerializeObject(request)}");
            var res = new ApiResult<string>();

            if (request.PurchaseOrderIds == null || !request.PurchaseOrderIds.Any())           
            {
                res.Code = ResultCode.Exception;
                res.Message = "采购单号不能为空!";
                return res;
            }

            try
            {
                foreach (var item in request.PurchaseOrderIds)
                {
                    await _purchaseOrderRepository.UpdatePurchaseOrderDeliveryInfo(new PurchaseOrderDO
                    {
                        Id = item,
                        UpdateBy = request.UpateBy,
                        OwnFreight = 0,
                        WaybillNumber = request.DeliveryCode
                    });
                    //采购单发货并入库
                    await PurchaseInStock(new PurchaseOrderDTO
                    {
                        Id = item,
                        CreateBy = request.UpateBy
                    }, false);
                }


                //await _purchaseOrderRepository.UpdatePurchaseOrderPayType(request.PurchaseOrderIds, new PurchaseOrderDO
                //{
                //    UpdateBy = request.UpateBy,
                //    PayStatus = 1,
                //    WaybillNumber = request.DeliveryCode
                //});


                //await _purchaseOrderProductRepository.BatchUpdatePurchaseOrderProductStatus(request.PurchaseOrderIds, request.UpateBy);

                res.Message = "操作成功!";
                res.Code = ResultCode.Success;

            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"SendPurchaseOrder_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<List<PurchaseOrderViewDTO>> SelectOutPurchaseOrders(SelectOutPurchaseOrdersRequest request)
        {
            //未发货的采购单

            var list = await _purchaseOrderRepository.SelectPurchaseOrderViewPages(request);
            return list;

        }

        /// <summary>
        /// 获取供应商的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PurchaseOrderPayDTO> GetPurchaseOrderPayInfo(PurchaseOrderViewDTO request)
        {
            PurchaseOrderPayDTO payInfo = new PurchaseOrderPayDTO();

            var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PoId);

            var venderInfo = await _venderRepository.GetAsync(purchaseOrder.VenderId);

            payInfo.AccountNo = venderInfo.Account;
            payInfo.BankName = venderInfo.Bank;
            payInfo.VenderName = venderInfo.VenderShortName;
            payInfo.OpeningBank = venderInfo.OpeningBank;
            payInfo.ReceiveBankName = venderInfo.ReceiveBankName;
            var products = await _purchaseOrderProductRepository.GetListAsync("where po_id =@po_id and status<>3 and is_deleted=0",
                new { po_id = request.PoId });

            payInfo.Amount = products.Sum(r => r.Num * r.PurchasePrice);

            return payInfo;

        }

        /// <summary>
        /// 保存支付信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SavePayInfo(PurchaseOrderDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                PurchaseOrderDO purchaseOrder = new PurchaseOrderDO
                {
                    Id = request.Id,
                    PayMethod = request.PayMethod,
                    SerialNumber = request.SerialNumber,
                    Payer = _identityService.GetUserName(),
                    PayTime = DateTime.Now,
                    UpdateBy = _identityService.GetUserName(),
                    UpdateTime = DateTime.Now,
                    PayStatus = 3
                };

                await _purchaseOrderRepository.UpdateAsync(purchaseOrder, new List<string> { "PayMethod", "SerialNumber", "PayStatus", "Payer", "PayTime", "UpdateBy", "UpdateTime" });
                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"SavePayInfo Request:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> UpsertPurchaseMonthPayInfo(PurchaseMonthPayDTO request)
        {
            var res = new ApiResult<string>();
            if (request.Id > 0)
            {

            }
            else
            {
                //新增
                request.CreateBy = _identityService.GetUserName();
                request.CreateTime = DateTime.Now;
                var shopId = Convert.ToInt64(_identityService.GetOrganizationId());

                request.ShopId = shopId;
                request.Status = "新建";
                var vo = _mapper.Map<PurchaseMonthPayDO>(request);
                vo.RelationPurchaseIds = string.Join(",", request.PurchaseIds);
                await _monthPayRepository.InsertAsync(vo);

                //将采购单锁定
                await _purchaseOrderRepository.BatchUpdatePurchasePayStatusToAudit(request.PurchaseIds, _identityService.GetUserName());


                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }

            return res;
        }

        public async Task<ApiPagedResult<PurchaseMonthPayDTO>> SelectPurchaseMonthPayInfos(PurchaseMonthPayRequest request)
        {
            var res = new ApiPagedResult<PurchaseMonthPayDTO>();

            var param = new DynamicParameters();
            var conditions = new StringBuilder();

            var shopId = Convert.ToInt64(_identityService.GetOrganizationId());

            var shopIds = new List<long>();
            if (_identityService.GetOrgType() == "0")
            {
                //shopIds = await GetCurrentCompanyAllShops(shopId, true);

                shopIds.Add(shopId);
            }
            else if (_identityService.GetOrgType() == "1")
            {
                shopIds.Add(shopId);
            }


            conditions.Append("where is_deleted =0 and shop_id in @shopIds ");
            param.Add("@shopIds", shopIds);

            if (!string.IsNullOrWhiteSpace(request.Status))
            {

                param.Add("@status", request.Status);
                conditions.Append(" and status=@status");
            }

            var result = await _monthPayRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", param);

            var vo = _mapper.Map<ApiPagedResultData<PurchaseMonthPayDTO>>(result);
            res.Data = vo;
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        public async Task<ApiResult<string>> AuditPurchaseMonthPay(PurchaseMonthPayDTO request)
        {
            var res = new ApiResult<string>();


            PurchaseMonthPayDO monthPayDO = new PurchaseMonthPayDO();
            monthPayDO.UpdateBy = _identityService.GetUserName();
            monthPayDO.UpdateTime = DateTime.Now;

            monthPayDO.Id = request.Id;
            if (request.Type == 1)
            {
                monthPayDO.AuditTime = DateTime.Now;
                monthPayDO.AuditUser = _identityService.GetUserName();
                monthPayDO.Status = "已审核";
                await _monthPayRepository.UpdateAsync(monthPayDO, new List<string> { "UpdateBy", "UpdateTime", "AuditTime", "AuditUser", "Status" });
            }
            else if (request.Type == 2)
            {
                monthPayDO.CancleTime = DateTime.Now;
                monthPayDO.CancleUser = _identityService.GetUserName();

                monthPayDO.Status = "已取消";
                await _monthPayRepository.UpdateAsync(monthPayDO, new List<string> { "UpdateBy", "UpdateTime", "CancleTime", "CancleUser", "Status" });

                var payInfo = await _monthPayRepository.GetAsync(request.Id);

                List<string> purchaseIds = payInfo.RelationPurchaseIds.Split(',').ToList();

                await _purchaseOrderRepository.BatchReleasePurchasePayStatus(purchaseIds, _identityService.GetUserName());

            }
            res.Code = ResultCode.Success;
            res.Message = "操作成功!";
            return res;
        }

        public async Task<ApiResult<string>> SavePurchaseMonthPay(PurchaseMonthPayDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                PurchaseMonthPayDO purchaseOrder = new PurchaseMonthPayDO
                {
                    Id = request.Id,
                    PayMethod = request.PayMethod,
                    SerialNumber = request.SerialNumber,
                    Payer = _identityService.GetUserName(),
                    PayTime = DateTime.Now,
                    UpdateBy = _identityService.GetUserName(),
                    UpdateTime = DateTime.Now,
                    Status = "已付款"
                };

                await _monthPayRepository.UpdateAsync(purchaseOrder, new List<string> { "PayMethod", "SerialNumber", "Status",
                    "Payer", "PayTime", "UpdateBy", "UpdateTime" });

                //将申请单号的 采购单 的状态全部改成 已付款

                var payInfo = await _monthPayRepository.GetAsync(request.Id);


                List<string> purchaseIds = payInfo.RelationPurchaseIds.Split(',').ToList();

                PurchaseOrderDO tempDo = new PurchaseOrderDO();
                tempDo.PayMethod = request.PayMethod;
                tempDo.SerialNumber = request.SerialNumber;
                tempDo.PayTime = DateTime.Now;
                tempDo.Payer = _identityService.GetUserName();

                await _purchaseOrderRepository.BatchUpdatePurchasePayStatus(purchaseIds, tempDo);

                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"SavePurchaseMonthPay Request:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<GetTargetVenderPurchaseOrderResponse>> GetVenderPurchaseOrders(GetVenderPurchaseOrdersRequest request)
        {
            var res = new ApiResult<GetTargetVenderPurchaseOrderResponse>();

            List<long> shopIds = new List<long>();


            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            //条件
            if (request.ShopId > 0)
            {
                shopIds.Add(request.ShopId);
            }
            //公司
            else if (_identityService.GetOrgType() == "0")
            {
                shopIds = await GetCurrentCompanyAllShops(shopId, true);
            }
            //门店
            else if (_identityService.GetOrgType() == "1")
            {
                shopIds.Add(shopId);
            }

            GetTargetVenderPurchaseOrderResponse orderResponse = new GetTargetVenderPurchaseOrderResponse();
            //获取 指定供应商 的未付款结算方式，月结的，未取消的采购单
            var venderInfo = await _venderRepository.GetAsync(request.VenderId);

            orderResponse.Account = venderInfo.Account;
            orderResponse.Bank = venderInfo.Bank;
            orderResponse.OpeningBank = venderInfo.OpeningBank;

            orderResponse.ReceiveBankName = venderInfo.ReceiveBankName;

            var tempOrders = await _purchaseOrderRepository.SelectPurchasePayInfos(shopIds, request.VenderId,request.Times);
            if (tempOrders != null && tempOrders.Any())
            {
                orderResponse.purchaseOrders = new List<AddPurchasePayInfo>();

                Dictionary<long, DAL.ShopDO> shopDic = new Dictionary<long, DAL.ShopDO>();

                foreach (var item in tempOrders.GroupBy(r => r.PurchaseId))
                {
                    AddPurchasePayInfo parentInfo = new AddPurchasePayInfo();
                    var purchaseInfo = item.First();
                    parentInfo.PurchaseId = item.Key;

                    parentInfo.createTime = purchaseInfo.createTime;
                    parentInfo.deliveryCode = purchaseInfo.deliveryCode;
                    parentInfo.relationPurchaseId = purchaseInfo.relationPurchaseId;
                    parentInfo.hcType = purchaseInfo.hcType;

                    if (purchaseInfo.hcType == "红冲订单")
                    {
                        parentInfo.Amount = -(item.ToList().Sum(r => r.Num * r.Price));
                    }
                    else
                    {
                        parentInfo.Amount = item.ToList().Sum(r => r.Num * r.Price);
                    }
                    if (shopDic.ContainsKey(purchaseInfo.shopId))
                    {
                        parentInfo.shopName = shopDic[purchaseInfo.shopId].SimpleName;
                    }
                    else
                    {
                        var shopInfo = await shopRepository.GetAsync(purchaseInfo.shopId);
                        shopDic[purchaseInfo.shopId] = shopInfo;
                        parentInfo.shopName = shopInfo.SimpleName;
                    }
                    //parentInfo.products = new List<PurchaseOrderProductInfo>();
                    StringBuilder builder = new StringBuilder();
                    foreach (var child in item.ToList())
                    {
                        builder.Append($"{child.ProductName}_{child.Num}_{child.Price}" + "\n");
                        //parentInfo.products.Add(new PurchaseOrderProductInfo
                        //{
                        //    Price = child.Price,
                        //    Num = child.Num,
                        //    ProductId = child.ProductId,
                        //    ProductName = child.ProductName
                        //});
                    }
                    parentInfo.joinText = builder.ToString();
                    orderResponse.purchaseOrders.Add(parentInfo);
                }
                res.Data = orderResponse;
            }

            res.Code = ResultCode.Success;
            return res;

        }

        /// <summary>
        /// 查看明细的接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<AddPurchasePayInfo>>> GetTargetPurchaseOrders(PurchaseMonthPayDTO request)
        {
            var res = new ApiResult<List<AddPurchasePayInfo>>();


            var payInfo = await _monthPayRepository.GetAsync(request.Id);
            List<string> purchaseIds = payInfo.RelationPurchaseIds.Split(',').ToList();

            var purchaseOrders = new List<AddPurchasePayInfo>();

            var targetOrders = await _purchaseOrderRepository.GetTargetPurchaseOrders(purchaseIds);
            if (targetOrders != null && targetOrders.Any())
            {
                Dictionary<long, DAL.ShopDO> shopDic = new Dictionary<long, DAL.ShopDO>();

                foreach (var item in targetOrders.GroupBy(r => r.PurchaseId))
                {
                    var purchaseInfo = item.First();
                    AddPurchasePayInfo parentInfo = new AddPurchasePayInfo();
                    parentInfo.PurchaseId = item.Key;
                    parentInfo.Amount = item.ToList().Sum(r => r.Num * r.Price);
                    parentInfo.createTime = purchaseInfo.createTime;
                    parentInfo.deliveryCode = purchaseInfo.deliveryCode;

                    parentInfo.relationPurchaseId = purchaseInfo.relationPurchaseId;
                    parentInfo.hcType = purchaseInfo.hcType;
                    if (purchaseInfo.hcType == "红冲订单")
                    {
                        parentInfo.Amount = -(item.ToList().Sum(r => r.Num * r.Price));
                    }
                    else
                    {
                        parentInfo.Amount = item.ToList().Sum(r => r.Num * r.Price);
                    }

                    if (shopDic.ContainsKey(purchaseInfo.shopId))
                    {
                        parentInfo.shopName = shopDic[purchaseInfo.shopId].SimpleName;
                    }
                    else
                    {
                        var shopInfo = await shopRepository.GetAsync(purchaseInfo.shopId);
                        shopDic[purchaseInfo.shopId] = shopInfo;
                        parentInfo.shopName = shopInfo.SimpleName;
                    }

                    // parentInfo.products = new List<PurchaseOrderProductInfo>();
                    StringBuilder builder = new StringBuilder();
                    foreach (var child in item.ToList())
                    {

                        builder.Append($"{child.ProductName}_{child.Num}_{child.Price}" + "\n");
                        //parentInfo.products.Add(new PurchaseOrderProductInfo
                        //{
                        //    Price = child.Price,
                        //    Num = child.Num,
                        //    ProductId = child.ProductId,
                        //    ProductName = child.ProductName
                        //});
                    }
                    parentInfo.joinText = builder.ToString();
                    purchaseOrders.Add(parentInfo);
                }
            }
            res.Code = ResultCode.Success;
            res.Data = purchaseOrders;
            return res;

        }

        public async Task<ApiResult<PurchaseOrderDTO>> GetPurchasePayInfo(PurchaseOrderViewDTO request)
        {
            ApiResult<PurchaseOrderDTO> res = new ApiResult<PurchaseOrderDTO>();
            var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PoId);

            res.Code = ResultCode.Success;
            res.Data = new PurchaseOrderDTO
            {
                PayMethod = purchaseOrder.PayMethod,
                SerialNumber = purchaseOrder.SerialNumber,
                Payer = purchaseOrder.Payer,
                PayTime = purchaseOrder.PayTime
            };
            return res;
        }


        /// <summary>
        /// 采购退货--部分退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> PurchaseReturnPart(PurchaseOrderEditRequest request)
        {
            ApiResult<string> res = new ApiResult<string>();

            var aItem = request.PurchaseOrderProducts?.Where(t => t.EditBackNum > 0); 
            if (aItem == null || !aItem.Any())
            {
                res.Code = ResultCode.Failed;
                res.Message = "退货数量为0";
                return res;
            }

            string responseMsg = string.Empty;
            string createBy = _identityService.GetUserName();

            var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PurchaseOrder.Id);

            //未收货的采购单，直接整单取消
            if (purchaseOrder.Status != 6)
            {
                PurchaseOrderDO tempDo = new PurchaseOrderDO
                {
                    Status = 3,
                    UpdateBy = createBy,
                    UpdateTime = DateTime.Now,
                    PayStatus = 1,
                    Id = purchaseOrder.Id,
                    Remark = "采购单退货!"
                };
                //直接取消掉
                await _purchaseOrderRepository.UpdateAsync(tempDo, new List<string> { "update_by", "update_time", "pay_status", "status" });

                res.Code = ResultCode.Success;
                res.Message = "退货成功!";
                return res;
            }

            //否则 生成退货采购单
            IEnumerable<PurchaseOrderProductDO> purchaseProducts = await _purchaseOrderProductRepository.GetListAsync(" where is_deleted=0 and po_id=@po_id and status<>3",
            new { po_id = purchaseOrder.Id });

            foreach (var item in purchaseProducts)
            {
                var returnItem = request.PurchaseOrderProducts?.FirstOrDefault(r => r.ProductCode == item.ProductCode);
                item.Num = returnItem?.EditBackNum ?? 0;
            }

            var returnProducts = purchaseProducts.Where(t => t.Num > 0);

            List<string> productIds = returnProducts.Select(r => r.ProductCode)?.ToList();
            if (productIds == null)
            {
                res.Code = ResultCode.Failed;
                res.Message = "退货数量为0";
                return res;
            }

            //txw20221206 简化库存，去掉批次和占库
            //var shopProductStockRes = await _stockManageService.GetShopOutPurchaseStocks(new GetBatchProductStockRequest
            //{
            //    LocationId = purchaseOrder.ShopId,
            //    ProductIds = productIds
            //});
            //var shopProductStocks = new List<ProductStockDTO>();

            //if (shopProductStockRes.Code == ResultCode.Success)
            //{
            //    shopProductStocks = shopProductStockRes.Data;

            //    bool isOccupy = false;

            //    //只要发现有一个产品占用了，那么就不能做退货操作了
            //    foreach (var item in returnProducts)
            //    {
            //        var inStockItem = shopProductStocks.FirstOrDefault(r => r.ProductId == item.ProductCode);
            //        if (inStockItem.Num < item.Num)
            //        {
            //            //产品发生有占用/出库
            //            isOccupy = true;
            //            responseMsg = $"产品:【{item.ProductName}】的退货数量:【{item.Num}】,可用库存:【{inStockItem.Num}】,采购单退货失败!";
            //            break;
            //        }
            //    }
            //    if (isOccupy)
            //    {
            //        res.Code = ResultCode.Failed;
            //        res.Message = responseMsg;
            //        return res;
            //    }

            //}

            //创建退货采购单，然后库存需要出掉

            var poResult = await _purchaseOrderRepository.InsertAsync(new PurchaseOrderDO
            {
                CreateBy = createBy,
                CreateTime = DateTime.Now,
                IsDeleted = 0,
                ShopId = purchaseOrder.ShopId,
                ShopName = purchaseOrder.ShopName,
                Status = PurchaseStatusEnum.待发货.ToInt(),
                Buyer = createBy,
                VenderId = purchaseOrder.VenderId,
                VenderShortName = purchaseOrder.VenderShortName,
                WarehouseId = purchaseOrder.WarehouseId,
                WarehouseName = purchaseOrder.WarehouseName,
                PayStatus = 1,
                PinvoiceType = purchaseOrder.PinvoiceType,
                PurchaseType = purchaseOrder.PurchaseType,
                PayType = purchaseOrder.PayType,
                TotalAmount = returnProducts.Sum(r => r.Num * r.PurchasePrice),
                RelationPurchaseId = purchaseOrder.Id,
                HcType = "红冲订单"
            });

            //txw20221206 简化库存，去掉批次和占库
            //获取退货的批次数据
            //var inventoryAllBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id in @product_ids and is_deleted=0",
            //     new
            //     {
            //         shop_Id = purchaseOrder.ShopId,
            //         product_ids = productIds
            //     });

            foreach (var item in returnProducts)
            {
                var purchaseProduct = new PurchaseOrderProductDO
                {
                    PurchasePrice = item.PurchasePrice,
                    PurchaseCost = item.PurchaseCost,
                    SalePrice = item.SalePrice,
                    Unit = item.Unit,
                    IsDeleted = 0,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now,
                    PlanInstockDate = DateTime.Now,
                    Num = item.Num,
                    PoId = poResult,
                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName,
                    Status = PurchaseOrderStatusEnum.已发货.ToInt().ToString(),
                    TaxPoint = item.TaxPoint,
                    TotalCost = item.PurchaseCost * item.Num,
                    TotalPrice = item.PurchasePrice * item.Num,
                    Specification = item.Specification,
                    OeNumber = item.OeNumber,
                    CategoryName = item.CategoryName
                };

                // purchaseProduct.StockId = flowItemResult;
                var purchaseProductId = await _purchaseOrderProductRepository.InsertAsync(purchaseProduct);

                //txw20221206 简化库存，去掉批次和占库
                //var inventoryBatchs = inventoryAllBatchs.Where(t => t.ProductId == item.ProductCode);

                //int totalNum = item.Num;
                //List<int> flowItemIds = new List<int>();

                ////获取可用的库存
                //foreach (var itemBatch in inventoryBatchs)
                //{
                //    if (totalNum <= 0) 
                //    { break; }
                //    List<long> batchNos = new List<long>();
                //    batchNos.Add(itemBatch.Id);
                //    var inventoryFlowItems = await _inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", 
                //        new { batchNos = batchNos, product_id = item.ProductCode, location_id = purchaseOrder.ShopId });

                //    //获取可用库存
                //    var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                //    int availabelNum = Convert.ToInt32(itemBatch.CanUseNum - sumUnAvailabelNum);
                //    if (availabelNum > 0)
                //    {
                //        int oneNum = 0;
                //        if (availabelNum >= totalNum)
                //        {
                //            //全部占用这个批次的数据
                //            oneNum = totalNum;
                //            totalNum = 0;
                //        }
                //        else
                //        {
                //            //扣减掉
                //            totalNum = totalNum - availabelNum;
                //            oneNum = availabelNum;
                //        }

                //        var flowItemResult = await _inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                //        {
                //            InventoryId = shopProductStocks.FirstOrDefault(r => r.ProductId == item.ProductCode).InventoryId,
                //            SourceNo = poResult.ToString(),
                //            SourceInventoryNo = purchaseProductId.ToString(),
                //            LocationId = purchaseOrder.ShopId,
                //            LocationName = purchaseOrder.ShopId.ToString(),
                //            BatchNo = itemBatch.Id.ToString(),
                //            ProductId = item.ProductCode,
                //            ProductName = item.ProductName,
                //            BusinessCategory = 3,
                //            BeforeOccupyNum = 0,
                //            AfterOccupyNum = oneNum,
                //            ChangeOccupyNum = oneNum,
                //            CreateBy = "占库系统",
                //            CreateTime = DateTime.Now
                //        });
                //        flowItemIds.Add(flowItemResult);
                //    }
                //}

                //PurchaseOrderProductDO orderProductDO = new PurchaseOrderProductDO
                //{
                //    Id = purchaseProductId,
                //    StockId = string.Join(",", flowItemIds),
                //    Remark = $"采购单退货,生成的红冲采购单:【{poResult}】"
                //};
                //await _purchaseOrderProductRepository.UpdateAsync(orderProductDO, new List<string> { "StockId" });

                //更新原采购单明细
                PurchaseOrderProductDO oldOrderProductDO = new PurchaseOrderProductDO
                {
                    Id = item.Id,
                    Status = PurchaseOrderStatusEnum.已退货.ToInt().ToString(),//已退货
                    BackNum = item.Num + item.BackNum
                };
                await _purchaseOrderProductRepository.UpdateAsync(oldOrderProductDO, new List<string> { "Status", "BackNum" });
            }


            //await InsertPurchaseLog(new PurchaseOrderLogDO
            //{
            //    ObjectId = purchaseOrder.Id.ToString(),
            //    ObjectType = LogTypeEnum.Purchase.ToString(),
            //    LogLevel = LogLevelEnum.Info.ToString(),
            //    BeforeValue = string.Empty,
            //    AfterValue = JsonConvert.SerializeObject(request),
            //    Remark = $"采购单申请退货成功，创建退货采购单:{poResult}",
            //    CreateBy = this._identityService.GetUserName(),
            //    CreateTime = DateTime.Now
            //});

            res.Code = ResultCode.Success;
            res.Message = "创建红冲采购单成功!";
            return res;
        }

        /// <summary>
        /// 采购退货--整单退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> PurchaseReturn(PurchaseOrderViewDTO request)
        {
            ApiResult<string> res = new ApiResult<string>();

            string responseMsg = string.Empty;

            var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PoId);
            if (purchaseOrder.Status != 6)
            {
                PurchaseOrderDO tempDo = new PurchaseOrderDO
                {
                    Status = 3,
                    UpdateBy = _identityService.GetUserName(),
                    UpdateTime = DateTime.Now,
                    PayStatus = 1,
                    Id = request.PoId,
                    Remark = "采购单退货!"
                };
                //直接取消掉
                await _purchaseOrderRepository.UpdateAsync(tempDo, new List<string> { "update_by", "update_time", "pay_status", "status" });

                res.Code = ResultCode.Success;
                res.Message = "退货成功!";
            }
            else
            {
                //否则 生成退货采购单

                IEnumerable<PurchaseOrderProductDO> products = await _purchaseOrderProductRepository.GetListAsync(" where is_deleted=0 and po_id=@po_id and status<>3",
                    new { po_id = request.PoId });

                List<string> productIds = products?.Select(r => r.ProductCode)?.ToList();

                var shopProductStockRes = await _stockManageService.GetShopOutPurchaseStocks(new GetBatchProductStockRequest
                {
                    LocationId = purchaseOrder.ShopId,
                    ProductIds = productIds
                });

                if (shopProductStockRes.Code == ResultCode.Success)
                {
                    var shopProductStocks = shopProductStockRes.Data;

                    bool isOccupy = false;

                    //只要发现有一个产品占用了，那么就不能做退货操作了
                    foreach (var item in products)
                    {
                        var inStockItem = shopProductStocks.FirstOrDefault(r => r.ProductId == item.ProductCode);
                        if (inStockItem.Num < item.Num)
                        {
                            //产品发生有占用/出库
                            isOccupy = true;
                            responseMsg = $"产品:【{item.ProductName}】的采购数量:【{item.Num}】,可用库存:【{inStockItem.Num}】,采购单退货失败!";
                            break;
                        }
                    }
                    if (!isOccupy)
                    {
                        //创建退货采购单，然后库存需要出掉

                        var purchaseProducts = await _purchaseOrderProductRepository.GetListAsync(" where po_id=@po_id and is_deleted=0 and  status<>3", new { po_id = purchaseOrder.Id });

                        var venderInfo = await _venderRepository.GetAsync(purchaseOrder.VenderId);
                        int payType = venderInfo.SettlementMethod;

                        //结算方式取供应商的
                        var poResult = await _purchaseOrderRepository.InsertAsync(new PurchaseOrderDO
                        {
                            CreateBy = purchaseOrder.CreateBy,
                            CreateTime = DateTime.Now,
                            IsDeleted = 0,
                            ShopId = purchaseOrder.ShopId,
                            Status = 2,
                            Buyer = purchaseOrder.CreateBy,
                            VenderId = purchaseOrder.VenderId,
                            VenderShortName = purchaseOrder.VenderShortName,
                            PayStatus = 1,
                            PinvoiceType = 0,
                            PurchaseType = 0,
                            PayType = payType,
                            TotalAmount = purchaseProducts.Sum(r => r.Num * r.PurchasePrice),
                            RelationPurchaseId = request.PoId,
                            HcType = "红冲订单"
                        });

                        var shopInfo = await shopRepository.GetAsync(purchaseOrder.ShopId);

                        if (purchaseProducts.Any())
                        {
                            var targetProductIds = purchaseProducts.Select(r => r.ProductCode).ToList();
                            var targetProducts = await fctShopProductRepository.GetShopProductByProductCodes(targetProductIds);

                            foreach (var item in purchaseProducts)
                            {
                                var purchaseProduct = new PurchaseOrderProductDO
                                {
                                    PurchasePrice = item.PurchasePrice,
                                    PurchaseCost = item.PurchasePrice,
                                    IsDeleted = 0,
                                    CreateBy = purchaseOrder.CreateBy,
                                    CreateTime = DateTime.Now,
                                    PlanInstockDate = DateTime.Now,
                                    Num = item.Num,
                                    PoId = poResult,
                                    ProductCode = item.ProductCode,
                                    ProductName = item.ProductName,
                                    Status = PurchaseOrderStatusEnum.已发货.ToString(),
                                    TaxPoint = 1,
                                    TotalCost = item.PurchasePrice * item.Num,
                                    TotalPrice = item.PurchasePrice * item.Num
                                };

                                var productInfo = targetProducts.FirstOrDefault(r => r.ProductCode == item.ProductCode);
                                //同步部分产品信息
                                if (productInfo != null)
                                {
                                    purchaseProduct.Specification = productInfo.Specification;
                                    purchaseProduct.OeNumber = productInfo.OeNumber;
                                    var categoryInfo = categoryRepository.GetAsync(productInfo.CategoryId);
                                    purchaseProduct.CategoryName = categoryInfo.Result.DisplayName;
                                }
                                // purchaseProduct.StockId = flowItemResult;
                                var purchaseProductId = await _purchaseOrderProductRepository.InsertAsync(purchaseProduct);

                                var filterProductIds = new List<string>();
                                filterProductIds.Add(item.ProductCode);

                                //获取退货的批次数据
                                var inventoryBatchs = await inventoryBatchRepository.GetListAsync(" where shop_Id =@shop_Id and product_id in @product_ids and is_deleted=0",
                                     new
                                     {
                                         shop_Id = purchaseOrder.ShopId,
                                         product_ids = filterProductIds
                                     });


                                int totalNum = item.Num;
                                List<int> flowItemIds = new List<int>();

                                //获取可用的库存
                                foreach (var inventory in inventoryBatchs)
                                {
                                    List<long> batchNos = new List<long>();
                                    batchNos.Add(inventory.Id);
                                    var inventoryFlowItems = await _inventoryFlowItemRepository.GetListAsync(" where is_deleted=0 and location_id=@location_id and product_id=@product_id and batch_no in @batchNos", new { batchNos = batchNos, product_id = item.ProductCode, location_id = purchaseOrder.ShopId });

                                    //获取可用库存
                                    var sumUnAvailabelNum = inventoryFlowItems.Sum(r => r.AfterOccupyNum) + inventoryFlowItems.Sum(r => r.AfterLockNum);
                                    int availabelNum = Convert.ToInt32(inventory.CanUseNum - sumUnAvailabelNum);
                                    if (availabelNum > 0)
                                    {
                                        if (availabelNum >= totalNum)
                                        {
                                            //全部占用这个批次的数据
                                            var flowItemResult = await _inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                            {
                                                SourceNo = poResult.ToString(),
                                                LocationId = purchaseOrder.ShopId,
                                                LocationName = shopInfo.SimpleName,
                                                BatchNo = inventory.Id.ToString(),   //这个值 需要修改
                                                ProductId = item.ProductCode,
                                                ProductName = item.ProductName,
                                                BusinessCategory = 3,
                                                BeforeOccupyNum = 0,
                                                AfterOccupyNum = totalNum,
                                                ChangeOccupyNum = totalNum,
                                                CreateBy = "占库系统",
                                                CreateTime = DateTime.Now
                                            });
                                            flowItemIds.Add(flowItemResult);
                                            break;
                                        }
                                        else
                                        {
                                            //扣减掉
                                            totalNum = totalNum - availabelNum;

                                            var flowItemResult = await _inventoryFlowItemRepository.InsertAsync(new InventoryFlowItemDO
                                            {
                                                SourceNo = poResult.ToString(),
                                                LocationId = purchaseOrder.ShopId,
                                                LocationName = shopInfo.SimpleName,
                                                BatchNo = inventory.Id.ToString(),   //这个值 需要修改
                                                ProductId = item.ProductCode,
                                                ProductName = item.ProductName,
                                                BusinessCategory = 3,
                                                BeforeOccupyNum = 0,
                                                AfterOccupyNum = availabelNum,
                                                ChangeOccupyNum = availabelNum,
                                                CreateBy = "占库系统",
                                                CreateTime = DateTime.Now
                                            });

                                            flowItemIds.Add(flowItemResult);
                                            continue;
                                        }
                                    }
                                }

                                PurchaseOrderProductDO orderProductDO = new PurchaseOrderProductDO
                                {
                                    Id = purchaseProductId,
                                    StockId = string.Join(",", flowItemIds),
                                    Remark = $"采购单退货,生成的红冲采购单:【{poResult}】"
                                };
                                await _purchaseOrderProductRepository.UpdateAsync(orderProductDO, new List<string> { "StockId" });
                            }
                        }


                        //需要修改原订单的状态

                        PurchaseOrderDO tempDo = new PurchaseOrderDO
                        {
                            Status = 6,
                            UpdateBy = _identityService.GetUserName(),
                            UpdateTime = DateTime.Now,
                            PayStatus = 1,
                            Id = request.PoId
                        };

                        await InsertPurchaseLog(new PurchaseOrderLogDO
                        {
                            ObjectId = request.PoId.ToString(),
                            ObjectType = LogTypeEnum.Purchase.ToString(),
                            LogLevel = LogLevelEnum.Info.ToString(),
                            BeforeValue = string.Empty,
                            AfterValue = JsonConvert.SerializeObject(request),
                            Remark = $"采购单申请退货成功，创建退货采购单:{poResult}",
                            CreateBy = this._identityService.GetUserName(),
                            CreateTime = DateTime.Now
                        });

                        //直接取消掉
                        await _purchaseOrderRepository.UpdateAsync(tempDo, new List<string> { "update_by", "update_time", "pay_status", "status" });

                        res.Code = ResultCode.Success;
                        res.Message = "创建红冲采购单成功!";
                    }
                    else
                    {
                        res.Code = ResultCode.Exception;
                        res.Message = responseMsg;
                    }
                }
            }
            return res;
        }

        public async Task<ApiResult<string>> ReleasePurchaseOccupyStock(PurchaseOrderViewDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var createBy = _identityService.GetUserName();
                var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PoId);
                var shopInfo = await shopRepository.GetAsync(purchaseOrder.ShopId);


                //扣除掉产品的总库存 ，清除占用库存
                var products = await _purchaseOrderProductRepository.GetListAsync(" where po_id =@poId and is_deleted=0", new { poId = request.PoId });

                var stockInoutId = await stockInoutRepository.InsertAsync(new StockInoutDO
                {
                    CreateBy = createBy,
                    IsDeleted = 0,
                    CreateTime = DateTime.Now,
                    LocationId = shopInfo.Id,
                    LocationName = shopInfo.SimpleName,
                    OperateTime = DateTime.Now,
                    OperationType = StockOperateTypeEnum.出库.ToString(),
                    Operator = string.Empty,
                    SourceInventoryNo = request.PoId.ToString(), //如果是空的 需要手动生成
                    SourceType = StockOutTypeEnum.采购退货.ToString(),
                    Status = StockInOutStatusEnum.已出库.ToString()   //判断有无差异数目
                });

                foreach (var item in products)
                {
                    //之前会占用多个库存的数据
                    foreach (var stockIdItem in item.StockId.Split(','))
                    {
                        int stockId = Convert.ToInt32(stockIdItem);

                        var flowItem = await _inventoryFlowItemRepository.GetAsync(stockId);

                        int num = Convert.ToInt32(item.Num);

                        var batchInfo = await inventoryBatchRepository.GetAsync(flowItem.BatchNo);

                        var relationId = await this.stockInoutItemRepository.InsertAsync(new StockInoutItemDO
                        {
                            ActualNum = num,
                            IsDeleted = 0,
                            BadNum = 0,
                            BatchNo = flowItem.BatchNo,
                            Cost = batchInfo.Cost,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            GoodNum = num,
                            InoutId = stockInoutId,
                            Num = num,
                            ProductId = item.ProductCode,
                            ProductName = item.ProductName,
                            ReleationId = item.Id,  //其他入库  无关联,
                            Status = StockInOutStatusEnum.已出库.ToString(), //判断有误差异数据
                            SupplierId = batchInfo != null ? Convert.ToInt64(batchInfo.SupplierId) : 0, //其他入库  无关联,
                            SupplierName = batchInfo != null ? batchInfo.SupplierName : string.Empty,
                            UomText = "个"
                        });

                        //2.扣除批次可用数量
                        await inventoryBatchRepository.UpdateInventoryBatchNum(new InventoryBatchDO
                        {
                            Id = batchInfo.Id,
                            UpdateBy = createBy,
                            CanUseNum = batchInfo.CanUseNum - flowItem.AfterOccupyNum
                        });

                        //3.生成批次流水记录
                        await _inventoryBatchFlowRepository.InsertAsync(new InventoryBatchFlowDO
                        {
                            ShopId = shopInfo.Id,
                            ShopName = shopInfo.SimpleName,
                            SourceId = request.PoId.ToString(),
                            IsDeleted = 0,
                            Cost = batchInfo.Cost,
                            BatchNo = batchInfo.Id,
                            SourceInventoryNo = item.Id.ToString(),
                            OperationType = StockOperateTypeEnum.出库.ToString(),
                            SourceType = StockOutTypeEnum.采购退货.ToString(),
                            ProductId = item.ProductCode,
                            ProductAttrType = ProductAttrTypeEnum.良品.ToString(),
                            ProductName = item.ProductName,
                            CreateBy = createBy,
                            CreateTime = DateTime.Now,
                            BeforeTotalNum = 0,
                            AfterTotalNum = batchInfo.TotalNum,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum - flowItem.AfterOccupyNum,
                            Amount = item.Num * batchInfo.Cost,
                            CurrentCanUseNum = batchInfo.CanUseNum - flowItem.AfterOccupyNum,
                            SupplierId = batchInfo.SupplierId,
                            SupplierName = batchInfo.SupplierName,
                            OwnId = batchInfo.OwnId,
                            OwnType = batchInfo.OwnType
                        });

                        var stockParams = new DynamicParameters();
                        stockParams.Add("@location_id", shopInfo.Id);
                        stockParams.Add("@product_id", item.ProductCode);
                        var inventoryResult = await _stockManageRepository.GetListAsync(" where location_id =@location_id and product_id =@product_id and is_deleted=0", stockParams);

                        var inventoryData = inventoryResult?.FirstOrDefault();
                        //4.扣除锁定数量
                        await _inventoryFlowItemRepository.UpdateInventoryFlowItemOccupy(new InventoryFlowItemDO
                        {
                            UpdateBy = createBy,
                            StockIds = new List<long> { stockId },
                            BeforeTotalNum = inventoryData.TotalNum,
                            AfterTotalNum = inventoryData.TotalNum - flowItem.AfterOccupyNum,
                            ChangeTotalNum = flowItem.AfterOccupyNum,
                            BeforeCanUseNum = batchInfo.CanUseNum,
                            AfterCanUseNum = batchInfo.CanUseNum - flowItem.AfterOccupyNum,
                            ChangeCanUseNum = flowItem.AfterOccupyNum
                        });

                        //5.扣除总库存
                        await _stockManageRepository.UpdateInventoryData(new InventoryDO
                        {
                            UpdateBy = createBy,
                            LocationId = shopInfo.Id,
                            ProductId = item.ProductCode,
                            TotalCost = inventoryData.TotalCost - batchInfo.Cost * flowItem.AfterOccupyNum,
                            TotalNum = inventoryData.TotalNum - flowItem.AfterOccupyNum,
                            CanUseNum = inventoryData.CanUseNum - flowItem.AfterOccupyNum
                        });
                    }
                }

                PurchaseOrderDO tempDo = new PurchaseOrderDO
                {
                    Status = 6,
                    UpdateBy = _identityService.GetUserName(),
                    UpdateTime = DateTime.Now,
                    Id = request.PoId
                };


                await InsertPurchaseLog(new PurchaseOrderLogDO
                {
                    ObjectId = request.PoId.ToString(),
                    ObjectType = LogTypeEnum.Purchase.ToString(),
                    LogLevel = LogLevelEnum.Info.ToString(),
                    BeforeValue = string.Empty,
                    AfterValue = JsonConvert.SerializeObject(request),
                    Remark = $"采购单退货成功!",
                    CreateBy = this._identityService.GetUserName(),
                    CreateTime = DateTime.Now
                });


                //直接取消掉
                await _purchaseOrderRepository.UpdateAsync(tempDo, new List<string> { "update_by", "update_time", "pay_status", "status" });
                res.Code = ResultCode.Success;
                res.Message = "退货成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                res.Message = "退货发生异常!";
            }
            return res;
        }

        /// <summary>
        /// 采购退货--退货出库:txw20221206 简化库存，去掉批次和占库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ReleasePurchaseOccupyStock_s(PurchaseOrderViewDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                //var shopInfo = await shopRepository.GetAsync(purchaseOrder.ShopId);
                var createBy = _identityService.GetUserName();
                var purchaseOrder = await _purchaseOrderRepository.GetAsync(request.PoId);

                //采购单明细
                var purchaseOrderProducts = await _purchaseOrderProductRepository.GetListAsync(" where po_id =@poId and is_deleted=0 and status<>3", new { poId = request.PoId });

                if (purchaseOrder == null || purchaseOrderProducts == null || !purchaseOrderProducts.Any())
                {
                    res.Code = ResultCode.Exception;
                    res.Message = "采购单或产品不存在!";
                    return res;
                }

                //组装库存更新数据
                var stockInoutRequest = new StockInOutDTO
                {
                    LocationId = purchaseOrder.WarehouseId > 0 ? purchaseOrder.WarehouseId : purchaseOrder.ShopId,
                    LocationName = purchaseOrder.WarehouseName,
                    OperationType = StockOperateTypeEnum.出库.ToString(),
                    SourceInventoryNo = purchaseOrder.Id.ToString(),
                    SourceType = StockOutTypeEnum.采购退货.ToString(),
                    Status = StockInOutStatusEnum.已出库.ToString(),
                    CreateBy = createBy,
                    Operator = createBy,
                    OperateTime = DateTime.Now,
                    Remark = ""
                };

                foreach (var item in purchaseOrderProducts)
                {
                    stockInoutRequest.StockItems.Add(new StockInoutItemDTO
                    {
                        ReleationId = item.Id,
                        ProductId = item.ProductCode,
                        ProductName = item.ProductName,
                        Num = item.Num,
                        GoodNum = item.Num,
                        ActualNum = item.Num,
                        UomText = item.Unit,
                        Cost = item.PurchaseCost,
                        CreateBy = createBy,
                        Status = StockInOutStatusEnum.已出库.ToString()
                    });

                }

                await _stockManageService.CreateInOutStockAndUpdateInventory(stockInoutRequest);
                //回写采购单状态
                await UpdateShopPurchaseStatusById(purchaseOrder.Id, (int)PurchaseStatusEnum.已收货, createBy);


                res.Code = ResultCode.Success;
                res.Message = "退货成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                res.Message = "退货发生异常!";
            }
            return res;
        }

        public async Task<ApiResult<string>> UpdatePurchaseDeliveryCode(PurchaseOrderDTO request)
        {
            PurchaseOrderDO orderDO = new PurchaseOrderDO();
            orderDO.WaybillNumber = request.WaybillNumber;
            orderDO.UpdateBy = _identityService.GetUserName();
            orderDO.UpdateTime = DateTime.Now;
            orderDO.OwnFreight = request.OwnFreight;
            orderDO.Id = request.Id;
            await _purchaseOrderRepository.UpdateAsync(orderDO, new List<string> { "WaybillNumber", "UpdateBy", "UpdateTime" });


            //await InsertPurchaseLog(new PurchaseOrderLogDO
            //{
            //    ObjectId = request.Id.ToString(),
            //    ObjectType = LogTypeEnum.Purchase.ToString(),
            //    LogLevel = LogLevelEnum.Info.ToString(),
            //    BeforeValue = string.Empty,
            //    AfterValue = JsonConvert.SerializeObject(request),
            //    Remark = $"采购单补填运单号:{request.WaybillNumber}",
            //    CreateBy = this._identityService.GetUserName(),
            //    CreateTime = DateTime.Now
            //});

            return new ApiResult<string> { Code = ResultCode.Success, Message = "操作成功!" };
        }
    }
}















