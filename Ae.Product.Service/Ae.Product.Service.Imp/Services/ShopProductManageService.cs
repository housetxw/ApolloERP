using AutoMapper;
using AutoMapper.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Common.Exceptions;
using Ae.Product.Service.Common.Extension;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model.Promotion;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.Promotion;
using Ae.Product.Service.Core.Request.ShopProduct;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Repository.Promotion;

namespace Ae.Product.Service.Imp.Services
{
    public class ShopProductManageService : IShopProductManageService
    {
        private readonly IFctShopProductRepository _fctShopProductRepository;
        private readonly IMapper _mapper;
        private readonly IDimSequenceRepository _dimSequenceRepository;
        private readonly ApolloErpLogger<ShopProductManageService> _logger;
        private readonly IDimShopBisicserviceRepository _dimShopBisicserviceRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IFctShopproductAuditlogRepository _fctShopproductAuditlogRepository;
        private readonly IPromotionActivityRepository _promotionActivityRepository;
        private readonly ICategoryRepository _categoryRepository;


        public ShopProductManageService(IFctShopProductRepository fctShopProductRepository,
            IMapper mapper, IDimSequenceRepository dimSequenceRepository,
            ApolloErpLogger<ShopProductManageService> logger,
            IDimShopBisicserviceRepository dimShopBisicserviceRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IFctShopproductAuditlogRepository fctShopproductAuditlogRepository,
            IPromotionActivityRepository promotionActivityRepository, ICategoryRepository categoryRepository)
        {
            this._fctShopProductRepository = fctShopProductRepository;
            this._mapper = mapper;
            this._dimSequenceRepository = dimSequenceRepository;
            this._logger = logger;
            this._dimShopBisicserviceRepository = dimShopBisicserviceRepository;
            this._configuration = configuration;
            this._fctShopproductAuditlogRepository = fctShopproductAuditlogRepository;
            this._promotionActivityRepository = promotionActivityRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        public ShopProductVo GetShopProductByCode(string shopProductCode)
        {
            var condition = " where  product_code=@shopProductCode";
            var paras = new
            {
                shopProductCode = shopProductCode
            };
            var shopProductDOs = _fctShopProductRepository.GetList(condition, paras);
            if (!shopProductDOs.IsNullOrEmpty())
            {
                var productDO = shopProductDOs.FirstOrDefault();
                var result = _mapper.Map<ShopProductVo>(productDO);
                var defaultUrl = _configuration["DefaultIcon"];
                result.Icon = string.IsNullOrEmpty(result.Icon) ? defaultUrl : result.Icon;
                if (!string.IsNullOrEmpty(productDO.ImageUrl))
                {
                    result.Images = productDO.ImageUrl?.Split(',').ToList();
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 查询门店商品byCodes
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        public List<ShopProductVo> GetShopProductByCodes(List<string> shopProductCodes)
        {
            var result = new List<ShopProductVo>();
            var condition = " where  product_code in @shopProductCodes";
            var paras = new
            {
                shopProductCodes = shopProductCodes
            };
            var shopProductDOs = _fctShopProductRepository.GetList(condition, paras);
            if (!shopProductDOs.IsNullOrEmpty())
            {
                result = _mapper.Map<List<ShopProductVo>>(shopProductDOs);
                var defaultUrl = _configuration["DefaultIcon"];
                result.ForEach((t) =>
                {
                    t.Icon = string.IsNullOrEmpty(t.Icon) ? defaultUrl : t.Icon;
                    //处理图片信息
                    var productDo = shopProductDOs.Where(a => a.Id == t.Id).FirstOrDefault();
                    if (!string.IsNullOrEmpty(productDo.ImageUrl))
                    {
                        t.Images = productDo?.ImageUrl?.Split(',').ToList();
                    }
                });
            }
            return result;
        }

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ShopProductVo> SelectShopProduct(ShopProductQueryRequest request)
        {
            var result = new List<ShopProductVo>();
            var condition = " where is_deleted=0 and shop_id=@shop_id ";
            if (request.CategoryId.HasValue)
            {
                condition += " and category_id=@category_id ";
            }
            if (request.IsOnSale.HasValue)
            {
                condition += " and on_sale=@on_sale ";
                if (request.IsOnSale == 0)//下降的
                {
                    condition += " and audit_status <> 2 ";
                }
                else if (request.IsOnSale == 2)//审核中
                {
                    request.IsOnSale = 0;
                    condition += " and audit_status = 2 ";
                }
            }
            if (!string.IsNullOrEmpty(request.Key))
            {
                condition += " and ( product_name like @key or product_code  like @key or display_name like @key ) ";
            }
            var paras = new
            {
                shop_id = request.ShopId,
                category_id = request.CategoryId,
                on_sale = request.IsOnSale,
                key = '%' + request.Key + '%'
            };
            var shopProductDOs = _fctShopProductRepository.GetList(condition, paras);
            if (!shopProductDOs.IsNullOrEmpty())
            {
                result = _mapper.Map<List<ShopProductVo>>(shopProductDOs);
                var defaultUrl = _configuration["DefaultIcon"];
                result.ForEach((t) =>
                {
                    t.Icon = string.IsNullOrEmpty(t.Icon) ? defaultUrl : t.Icon;
                });
                result = result.OrderByDescending(t => t.IsTop).ThenByDescending(t => t.OnSaleTime).ToList();
            }
            return result;
        }

        /// <summary>
        /// 分页查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ApiPagedResultData<ShopProductVo> SearchShopProduct(ShopProductSearchRequest request)
        {
            var result = new ApiPagedResultData<ShopProductVo>();
            var conditon = " where  fc.shop_id > 0 and  fc.category_id > 0 ";
            if (request.ShopId > 0)
            {
                conditon += " and fc.shop_id=@shop_id ";
            }

            if (request.shopIds != null && request.shopIds.Any())
            {
                conditon += " and fc.shop_id in @shop_ids ";
            }

            if (request.CategoryId.HasValue)
            {
                conditon += " and fc.category_id=@category_id ";
            }
            if (request.IsDeleted.HasValue)
            {
                conditon += " and fc.is_deleted=@is_deleted ";
            }
            if (request.IsOnSale.HasValue)
            {
                conditon += " and fc.on_sale=@on_sale ";
            }
            if (request.AuditStatus.HasValue)
            {
                conditon += " and fc.audit_status=@audit_status ";
            }
            if (request.IsStoreoutside.HasValue)
            {
                conditon += " and fc.is_storeoutside=@is_storeoutside ";
            }

            if (request.IsDisabled.HasValue)
            {
                conditon += " and fc.is_disabled=@is_disabled ";
            }
            if (!string.IsNullOrEmpty(request.Key))
            {
                conditon += " and ( fc.product_name like @key or fc.product_code  like @key or fc.display_name like @key ) ";
            }
            var paras = new
            {
                shop_ids = request.shopIds,
                shop_id = request.ShopId,
                category_id = request.CategoryId,
                is_deleted = request.IsDeleted,
                on_sale = request.IsOnSale,
                key = '%' + request.Key + '%',
                audit_status = request.AuditStatus,
                is_storeoutside = request.IsStoreoutside,
                is_disabled = request.IsDisabled
            };
            var totalCount = 0;
            var shopProducts = _fctShopProductRepository.SearchShopProduct(request, conditon, paras, out totalCount);
            result.Items = shopProducts ?? new List<ShopProductVo>();
            result.TotalItems = totalCount;
            return result;
        }

        /// <summary>
        /// 保存门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveShopProduct(ShopProductEditRequest request)
        {
            if (request == null || request.ShopProduct == null)
            {
                throw new CustomException("门店商品必填！");
            }
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var result = 0;
            //属性名称
            var shopProductDo = _mapper.Map<FctShopProductDO>(request.ShopProduct);
            if (!request.Images.IsNullOrEmpty())
            {
                shopProductDo.ImageUrl = string.Join(",", request.Images);
            }
            //置顶
            shopProductDo.IsTop = shopProductDo.SortType == 1 ? 1 : 0;
            //验证商品名称是否重复
            var conditon = " where shop_id=@shop_id and is_deleted=0 and (product_name = @product_name or display_name =@display_name )";
            if (!request.IsEdit)
            {
                var paraName = new
                {
                    shop_id = shopProductDo.ShopId,
                    product_name = shopProductDo.ProductName,
                    display_name = shopProductDo.DisplayName,
                };
                var sameProductDOs = _fctShopProductRepository.GetList(conditon, paraName);
                if (!sameProductDOs.IsNullOrEmpty())
                {
                    throw new CustomException("名称不能重复,核对商品名称和小程序显示名称!");
                }
                shopProductDo.CreateBy = userId;
                shopProductDo.UpdateTime = dtNow;
                shopProductDo.CreateTime = dtNow;
                shopProductDo.UpdateBy = userId;
                shopProductDo.IsDeleted = 0;
                shopProductDo.ProductCode = GetShopProductCode(shopProductDo.ShopId, shopProductDo.CategoryId);
                result = _fctShopProductRepository.Insert(shopProductDo);
            }
            else
            {
                //更新上下架时间
                if (shopProductDo.OnSale == 1)
                {
                    //验证是否已经上架
                    var productDO = _fctShopProductRepository.Get(shopProductDo.Id);
                    if (productDO.Id > 0 && productDO.OnSale == 1)
                    {
                        shopProductDo.OnSaleTime = productDO.OnSaleTime;//保留原来的上架时间
                    }
                    else
                    {
                        shopProductDo.OnSaleTime = DateTime.Now;
                    }
                }
                conditon += " and id!=@id";
                if (shopProductDo.OnSale == 1 || shopProductDo.AuditStatus == 2)//上架 或者上架申请 校验，名称是否重复
                {
                    var paraName = new
                    {
                        id = shopProductDo.Id,
                        shop_id = shopProductDo.ShopId,
                        product_name = shopProductDo.ProductName,
                        display_name = shopProductDo.DisplayName,
                    };
                    var sameProductDOs = _fctShopProductRepository.GetList(conditon, paraName);
                    if (!sameProductDOs.IsNullOrEmpty())
                    {
                        throw new CustomException("名称不能重复,核对商品名称和小程序显示名称!");
                    }
                }
                shopProductDo.UpdateTime = dtNow;
                shopProductDo.UpdateBy = userId;
                var updateFlids = new List<string>()
                {
                    "ProductName", "DisplayName", "Description",
                    "StandardPrice", "SalesPrice", "DiscountRate", "SortType", "IsTop", "Icon", "Remark",
                    "IsDeleted", "UpdateBy", "UpdateTime", "RefPid", "Detail", "ImageUrl"
                };
                // Boss 上架 默认审核通过
                if (shopProductDo.AuditStatus == 1)
                {
                    updateFlids.Add("OnSale");
                    updateFlids.Add("AuditTime");
                    updateFlids.Add("AuditUser");
                    updateFlids.Add("AuditStatus");
                    updateFlids.Add("AuditOpinion");
                }
                //下架
                if (shopProductDo.OnSale == 0)
                {
                    updateFlids.Add("OnSale");
                    updateFlids.Add("AuditStatus");
                }
                //上架更新上架时间
                if (shopProductDo.OnSale == 1)
                {
                    updateFlids.Add("OnSaleTime");
                }
                //申请上架
                if (shopProductDo.AuditStatus == 2)
                {
                    updateFlids.Add("ApplyTime");
                    updateFlids.Add("AuditStatus");
                }
                result = _fctShopProductRepository.Update(shopProductDo, updateFlids);
            }
            return result > 0;
        }


        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveShopPurchaseProduct(AddShopProductRequest request)
        {
            int shopId = request.ShopId;
            var existData = await _fctShopProductRepository.GetExistProducts(request.ProductCode, request.ProductName,
                request.OeNumber, shopId);
            if (existData.Exists(t => t.ProductName == request.ProductName))
            {
                throw new CustomException("产品名称不能重复,核对产品名称!");
            }

            if (existData.Exists(t => t.OeNumber == request.OeNumber))
            {
                throw new CustomException("原厂编号不能重复,核对原厂编号!");
            }

            if (string.IsNullOrEmpty(request.ProductCode))
            {
                //新增
                FctShopProductDO fctShopProductDo = new FctShopProductDO
                {
                    ShopId = shopId,
                    CategoryId = request.CategoryId,
                    ProductCode = await GetShopProductCodeV2(shopId, request.CategoryId),
                    ProductName = request.ProductName,
                    DisplayName = request.ProductName,
                    SalesPrice = request.SalesPrice,
                    IsStoreoutside = 1,
                    Specification = request.Specification,
                    Unit = request.Unit,
                    OeNumber = request.OeNumber,
                    PurchasePrice = request.PurchasePrice,
                    OnSale = 1,
                    OnSaleTime = DateTime.Now,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                };
                var record = await _fctShopProductRepository.InsertAsync(fctShopProductDo);

                return record > 0;
            }
            else
            {
                //编辑
                FctShopProductDO fctShopProductDo = new FctShopProductDO
                {
                    ShopId = shopId,
                    ProductCode = request.ProductCode,
                    ProductName = request.ProductName,
                    SalesPrice = request.SalesPrice,
                    Specification = request.Specification,
                    Unit = request.Unit,
                    OeNumber = request.OeNumber,
                    PurchasePrice = request.PurchasePrice,
                    UpdateBy = request.SubmitBy
                };
                var record = await _fctShopProductRepository.UpdateShopProduct(fctShopProductDo);

                return record > 0;
            }
        }

        /// <summary>
        /// 批量导入门店商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ImportShopProduct(ImportShopProductRequest request)
        {
            var reusult = new List<ShopProductImportVo>();
            if (request == null || request.Products.IsNullOrEmpty())
            {
                throw new CustomException("导入失败!");
            }
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var defaultUrl = _configuration["DefaultIcon"];
            var productDos = request.Products.Select(t => new FctShopProductDO()
            {
                ShopId = t.ShopId,
                ProductName = t.ProductName,
                DisplayName = t.DisplayName,
                CategoryId = t.CategoryId,
                SalesPrice = t.SalesPrice,
                StandardPrice = t.SalesPrice,
                Description = t.Description,
                ProductCode = GetShopProductCode(t.ShopId, t.CategoryId),
                IsDeleted = 0,
                OnSale = 0,
                Icon = string.IsNullOrEmpty(t.Icon) ? defaultUrl : t.Icon,
                CreateBy = userId,
                UpdateBy = userId,
                CreateTime = dtNow,
                UpdateTime = dtNow,
            }).ToList();
            return BatchSaveShopProductData(productDos);
        }

        /// <summary>
        /// 批量保存门店商品
        /// </summary>
        /// <param name="productDos"></param>
        /// <returns></returns>
        private bool BatchSaveShopProductData(List<FctShopProductDO> productDos)
        {
            var result = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (!productDos.IsNullOrEmpty())
                    {
                        _fctShopProductRepository.InsertBatch(productDos);
                    }
                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "批量保存门店商品异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }
            return result;
        }
        /// <summary>
        /// 生成门店商品编码
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetShopProductCode(long shopId, int categoryId)
        {
            var seqId = _dimSequenceRepository.GenerateProductCode("SP-WC-BM");
            return "SW-" + shopId + "-" + categoryId + "-" + seqId;
        }

        /// <summary>
        /// 生成门店商品编码
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<string> GetShopProductCodeV2(long shopId, int categoryId)
        {
            List<int> serviceProject = new List<int>() { 1, 2, 3, 4, 5, 6 };
            if (serviceProject.Contains(categoryId))
            {
                return GetShopProductCode(shopId, categoryId); //兼容老包
            }

            string productCode = string.Empty;
            string wcFuCategory = _configuration["Config:WaiCaiFuCategory"];
            List<int> wcFuCategoryId = wcFuCategory.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => Convert.ToInt32(_)).ToList();
            var category = _categoryRepository.GetAllCategoryById(categoryId).FirstOrDefault();
            if (category == null)
            {
                throw new CustomException("产品类目异常");
            }

            string seqName =
                $"{category.MainCategoryShortCode}-{category.SecondCategoryShortCode}-{category.ChildCategoryShortCode}";

            var isExistSeq = _dimSequenceRepository.GetSequenceBySeqName(seqName);
            if (isExistSeq == null)
            {
                await _dimSequenceRepository.InsertAsync(new DimSequenceDO()
                {
                    CategoryId = categoryId,
                    CurrentVal = 0,
                    IncrementVal = 1,
                    SeqName = seqName
                });
            }

            //验证商品编码是否存在
            var isExistProduct = true;
            while (isExistProduct)
            {
                var seqId = _dimSequenceRepository.GenerateProductCode(seqName);
                productCode = seqName + "-" + seqId;
                //服务产品
                if (wcFuCategoryId.Contains(categoryId))
                {
                    productCode += "-FU";
                }

                var shopProduct = await _fctShopProductRepository.GetShopProductByPid(productCode);
                if (shopProduct == null)
                {
                    isExistProduct = false;
                }
            }

            return productCode;
        }

        /// <summary>
        /// 初始化门店基础服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool InitShopBasicService(InitShopServiceRequest request)
        {
            //获取所有的基础服务
            var bisicserviceDOs = _dimShopBisicserviceRepository.GetList(" where is_deleted=0 ");
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var productDos = bisicserviceDOs.Select(t => new FctShopProductDO()
            {
                ShopId = request.ShopId,
                ProductName = t.ProductName,
                DisplayName = t.DisplayName,
                CategoryId = t.CategoryId,
                SalesPrice = t.SalesPrice,
                StandardPrice = t.SalesPrice,
                ProductCode = GetShopProductCode(request.ShopId, t.CategoryId),
                Icon = t.Icon,
                IsDeleted = 0,
                OnSale = 0,
                CreateBy = userId,
                UpdateBy = userId,
                CreateTime = dtNow,
                UpdateTime = dtNow,
                RefPid = t.Id
            }).ToList();
            return BatchSaveShopProductData(productDos);
        }

        /// <summary>
        /// 审核门店商品上架
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool AuditShopProduct(ShopProductAuditRequest request)
        {
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var shopProductDo = new FctShopProductDO();
            shopProductDo.Id = request.ShopProductId;
            shopProductDo.UpdateTime = dtNow;
            shopProductDo.UpdateBy = userId;
            shopProductDo.AuditTime = dtNow;
            shopProductDo.AuditUser = request.AuditUser;
            shopProductDo.AuditStatus = request.AuditStatus;
            shopProductDo.AuditOpinion = request.AuditOpinion;
            var updateFlids = new List<string>() { "UpdateBy", "UpdateTime", "AuditTime", "AuditUser", "AuditStatus", "AuditOpinion" };
            if (request.AuditStatus == 1)// 通过
            {
                shopProductDo.OnSale = 1;
                shopProductDo.OnSaleTime = dtNow;
                updateFlids.Add("OnSale");
                updateFlids.Add("OnSaleTime");
                return _fctShopProductRepository.Update(shopProductDo, updateFlids) > 0;
            }
            else if (request.AuditStatus == 3)// 驳回
            {
                if (string.IsNullOrEmpty(request.AuditOpinion))
                {
                    throw new CustomException("审核意见必填");
                }
                var result = _fctShopProductRepository.Update(shopProductDo, updateFlids);
                if (result > 0)
                {
                    var log = new FctShopproductAuditlogDO();
                    log.AuditOpinion = request.AuditOpinion;
                    log.AuditStatus = request.AuditStatus == 1 ? "通过" : "拒绝";
                    log.ProductCode = request.ShopProductCode;
                    log.AuditUser = request.AuditUser;
                    log.CreateBy = userId;
                    log.UpdateTime = dtNow;
                    log.CreateTime = dtNow;
                    log.UpdateBy = userId;
                    log.IsDeleted = 0;
                    _fctShopproductAuditlogRepository.Insert(log);
                }
            }
            return true;
        }
        /// <summary>
        /// 自动审核门店商品上架
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool AutoAuditShopProduct()
        {
            _logger.Info("AutoAuditShopProduct --->Start" + DateTime.Now);
            var conditon = " where is_deleted=0 and audit_status=2 and TIMESTAMPDIFF(hour,apply_time,current_timestamp())>24";
            var sameProductDOs = _fctShopProductRepository.GetList(conditon);
            if (!sameProductDOs.IsNullOrEmpty())
            {
                var dtNow = DateTime.Now;
                var updateFlids = new List<string>() { "UpdateTime", "AuditTime", "AuditUser", "AuditStatus", "AuditOpinion" };
                sameProductDOs.ToList().ForEach((t) =>
                {
                    t.AuditOpinion = "系统自动审核";
                    t.AuditStatus = 1;
                    t.AuditTime = dtNow;
                    t.AuditUser = "System";
                    t.UpdateTime = dtNow;
                    t.OnSale = 1;
                    t.OnSaleTime = dtNow;
                    _fctShopProductRepository.Update(t, updateFlids);
                });
            }
            _logger.Info("AutoAuditShopProduct --->End" + DateTime.Now);
            return true;
        }

        /// <summary>
        /// 获取门店上架的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopProductDetailVo>> GetOnSaleShopProduct(OnSaleShopProductRequest request)
        {
            List<ShopProductDetailVo> data = new List<ShopProductDetailVo>();
            List<ProductPromotionInfoVo> promotion = new List<ProductPromotionInfoVo>();
            var shopProduct =
                await _fctShopProductRepository.GetShopProductByCategoryId(request.CategoryId, request.ShopId); //门店商品

            var pidList = shopProduct.Select(t => t.ProductCode).ToList();
            if (pidList.Any())
            {
                promotion = await GetPromotionActivityByProductCodeList(
                    new PromotionActivitByProductCodeListRequest
                    {
                        ProductCodeList = pidList,
                        PromotionChannel = request.Channel
                    }) ?? new List<ProductPromotionInfoVo>();
            }

            shopProduct.ForEach(_ =>
            {
                var image = ConvertProductIcon(_);

                var itemProduct = new ShopProductDetailVo()
                {
                    Id = _.Id,
                    ShopId = _.ShopId,
                    CategoryId = _.CategoryId,
                    ProductCode = _.ProductCode,
                    ProductName = _.ProductName,
                    DisplayName = _.DisplayName,
                    Description = _.Description,
                    StandardPrice = _.StandardPrice,
                    SalesPrice = _.SalesPrice,
                    SortType = _.SortType,
                    IsTop = _.IsTop,
                    OnSale = _.OnSale,
                    OnSaleTime = _.OnSaleTime,
                    Icon = image.Item1,
                    Remark = _.Remark,
                    Detail = _.Detail,
                    Images = image.Item2
                };

                var existPromotion = promotion.FirstOrDefault(t => t.ProductCode == _.ProductCode);
                if (existPromotion != null)
                {
                    itemProduct.PromotionPrice = existPromotion.PromotionPrice;
                    itemProduct.Tags.Add(new TagInfo()
                    {
                        Tag = existPromotion.Label,
                        TagColor = "#FF0000",
                        Type = "Promotion"
                    });
                    itemProduct.ActivityId = existPromotion.ActicityId;
                }

                data.Add(itemProduct);
            });

            return data;
        }

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<ProductPromotionInfoVo>> GetPromotionActivityByProductCodeList(PromotionActivitByProductCodeListRequest request)
        {
            DateTime now = DateTime.Now;
            List<ProductPromotionInfoVo> result = new List<ProductPromotionInfoVo>();
            if (request.ProductCodeList == null || !request.ProductCodeList.Any())
            {
                return result;
            }
            var productList = await _promotionActivityRepository.GetPromotionByPidList(request.ProductCodeList, false);
            productList = productList.Where(_ => _.AuditStatus == 1 && now.CompareTo(_.StartTime) >= 0 && now.CompareTo(_.EndTime) < 0 && _.ApplyChannel.Split(new char[] { ',' }).Contains(request.PromotionChannel)).ToList();

            result = productList.Select(_ => new ProductPromotionInfoVo
            {
                ProductCode = _.ProductCode,
                ActicityId = _.Id.ToString(),
                PromotionPrice = _.PromotionPrice,
                Label = _.Label,
                AvailQuantity = _.LimitQuantity - _.SoldQuantity
            }).ToList();

            return result;
        }

        private Tuple<string, List<string>> ConvertProductIcon(FctShopProductDO product)
        {
            List<string> images = new List<string>();
            string icon = $"{_configuration["ImageDomain"]}{_configuration["DefaultIcon"]}";
            if (!string.IsNullOrEmpty(product.Icon))
            {
                icon = $"{_configuration["ImageDomain"]}{product.Icon}";
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                images = product.ImageUrl.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(_ => $"{_configuration["ImageDomain"]}{_}").ToList();
            }

            return new Tuple<string, List<string>>(icon, images);
        }

        public async Task<ApiResult<string>> UpdateProductPriceInfo(UpdateProductPriceRequest request)
        {
            FctShopProductDO productDO = new FctShopProductDO
            {
                SalesPrice = request.SalePrice,
                UpdateBy = request.UpdateBy,
                UpdateTime = DateTime.Now,
                ShopId = request.ShopId,
                ProductCode = request.ProductCode,
            };

            await _fctShopProductRepository.UpdateProductPriceInfo(productDO);

            return new ApiResult<string> { Code = ResultCode.Success, Message = "更新成功!" };
        }
    }
}
