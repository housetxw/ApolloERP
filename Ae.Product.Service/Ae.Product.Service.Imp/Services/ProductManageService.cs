using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Common.Exceptions;
using Ae.Product.Service.Common.Extension;
using Ae.Product.Service.Common.Util;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Ae.Product.Service.Core.Enums;
using System.Text;
using Microsoft.AspNetCore.Http;
using Ae.Product.Service.Core.Request.Product;
using Microsoft.Extensions.Configuration;
using Ae.Product.Service.Client.Clients;
using Ae.Product.Service.Client.Request;
using Ae.Product.Service.Client.Interface;
using Ae.Product.Service.Client.Request.BaoYang;
using Ae.Product.Service.Client.Response.BaoYang;
using Ae.Product.Service.Core.Request.Promotion;
using TagInfo = Ae.Product.Service.Core.Response.TagInfo;
using Ae.Product.Service.Client.Model.BaoYang;
using ApolloErp.Redis;
using Ae.Product.Service.Core.Model.Category;
using Ae.Product.Service.Core.Model.DoorProduct;
using Ae.Product.Service.Core.Request.DoorProduct;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Core.Request.InstallService;
using Ae.Product.Service.Core.Model.InstallService;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Dal.Model.Config;
using Ae.Product.Service.Dal.Repository.Config;
using Ae.Product.Service.Dal.Repository.Promotion;
using Ae.Product.Service.Dal.Model.Promotion;

namespace Ae.Product.Service.Imp.Services
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public class ProductManageService : IProductManageService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductInstallServiceRepository _productInstallRepository;
        private readonly IProductAttributevalueRepository _productAttributevalueRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductPackageRepository _productPackageRepository;
        private readonly IAssociateproductRepository _associateproductRepository;
        private readonly IAssociateproductDetailRepository _associateproductDetailRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ICategoryAttributeRepository _categoryAttributeRepository;
        private readonly IDimSequenceRepository _dimSequenceRepository;
        private readonly ApolloErpLogger<ProductManageService> _logger;
        private readonly IDimAttributemameRepository _dimAttributemameRepository;
        private readonly IDimAttributevalueRepository _dimAttributevalueRepository;
        private readonly IRelCategoryAttributeRepository _relCategoryAttributeRepository;
        private readonly IFctShopProductRepository _fctShopProductRepository;
        private readonly IConfiguration _configuration;
        private readonly IShopManageClient _shopManageClient;
        private readonly IPromotionService _promotionService;
        private readonly IBaoYangClient _baoYangClient;
        private readonly string redisKey = "Rg:Product:Service:ProductManageService";
        private readonly RedisClient _redisClient;
        private readonly IDoorProductRepository _doorProductRepository;
        private readonly IConfigFrontCategoryRepository _configFrontCategoryRepository;
        private readonly IConfigFrontCategoryProductRepository _configFrontCategoryProductRepository;
        private readonly IServiceTypeEnumRepository _serviceTypeEnumRepository;


        public ProductManageService(ICatalogBrandRepository catalogBrandRepository, IMapper mapper,
            IProductRepository productRepository, IProductInstallServiceRepository productInstallRepository,
            IProductAttributevalueRepository productAttributevalueRepository,
            ICategoryRepository categoryRepository, IProductPackageRepository productPackageRepository,
            IAssociateproductRepository associateproductRepository,
            IAssociateproductDetailRepository associateproductDetailRepository,
            IUnitRepository unitRepository, ICategoryAttributeRepository categoryAttributeRepository,
            IDimSequenceRepository dimSequenceRepository, IDimAttributemameRepository dimAttributemameRepository,
            ApolloErpLogger<ProductManageService> logger, IDimAttributevalueRepository dimAttributevalueRepository,
            IRelCategoryAttributeRepository relCategoryAttributeRepository,
            IFctShopProductRepository fctShopProductRepository, IConfiguration configuration,
            IShopManageClient shopManageClient, IPromotionService promotionService, IBaoYangClient baoYangClient,
            RedisClient redisClient, IDoorProductRepository doorProductRepository,
            IConfigFrontCategoryRepository configFrontCategoryRepository,
            IServiceTypeEnumRepository serviceTypeEnumRepository,
        IConfigFrontCategoryProductRepository configFrontCategoryProductRepository)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _productInstallRepository = productInstallRepository;
            _productAttributevalueRepository = productAttributevalueRepository;
            _categoryRepository = categoryRepository;
            _productPackageRepository = productPackageRepository;
            _associateproductRepository = associateproductRepository;
            _associateproductDetailRepository = associateproductDetailRepository;
            _unitRepository = unitRepository;
            _categoryAttributeRepository = categoryAttributeRepository;
            _dimSequenceRepository = dimSequenceRepository;
            _dimAttributemameRepository = dimAttributemameRepository;
            _logger = logger;
            _dimAttributevalueRepository = dimAttributevalueRepository;
            _relCategoryAttributeRepository = relCategoryAttributeRepository;
            _fctShopProductRepository = fctShopProductRepository;
            _configuration = configuration;
            _shopManageClient = shopManageClient;
            _promotionService = promotionService;
            _baoYangClient = baoYangClient;
            _redisClient = redisClient;
            _doorProductRepository = doorProductRepository;
            _configFrontCategoryRepository = configFrontCategoryRepository;
            _configFrontCategoryProductRepository = configFrontCategoryProductRepository;
            _serviceTypeEnumRepository = serviceTypeEnumRepository;
        }

        #region 编辑品牌

        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<BrandVO>> GetCatalogBrandAsync()
        {
            List<BrandVO> result = new List<BrandVO>();
            var brandResult = await _catalogBrandRepository.GetCatalogBrandAsync();
            if (brandResult != null && brandResult.Any())
            {
                result = _mapper.Map<List<BrandVO>>(brandResult);
            }

            return result;
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddBrandAsync(AddBrandRequest request)
        {
            var existBrand = await _catalogBrandRepository.GetProductBrandList(new GetProductBrandListRequest()
            {
                BrandName = request.BrandName,
                IsForbid = (int)IsForbidEnumType.All,
                BrandFullMatch = true
            });
            if (existBrand.Items != null && existBrand.Items.Any())
            {
                throw new CustomException("品牌已存在");
            }

            var result = await _catalogBrandRepository.AddCategoryBrandAsync(new DimBrandDO()
            {
                BrandName = request.BrandName,
                BrandImg = request.BrandImg ?? String.Empty,
                UpdateBy = request.UserId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                CreateBy = request.UserId
            });
            return result > 0;
        }

        #endregion

        #region 编辑单位

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public List<UnitVo> GetUnits()
        {
            var conditon = " where is_deleted=0 ";
            var unitDOs = _unitRepository.GetList(conditon);
            var result = _mapper.Map<List<UnitVo>>(unitDOs)?.ToList();
            return result;
        }

        #endregion

        #region 商品查询接口

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProductSearchResponse SearchProduct(ProductSearchRequest request)
        {
            var condition = "";
            var brandCondition = "";
            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                request.Key = request.Key.Trim(); //去掉空格
                var childProductCon = @" or fc.id in(
                      (select parent_id from fct_product where is_deleted=0 and class_type=2 and  (product_code=@code
                         or name like @name
                         or display_name like @name)
			           )
                 )";
                condition += $@" and (fc.product_code=@code
                or fc.name like @name
                or fc.display_name like @name
                {(request.ClassType == 4 ? childProductCon : "")}
                )";
                brandCondition = condition;
            }

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                condition += " and fc.brand=@brand";
            }

            if (request.StartPrice.HasValue)
            {
                condition += " and fc.sales_price >=@startPrice";
            }

            if (request.EndPrice.HasValue)
            {
                condition += " and fc.sales_price <=@endPrice";
            }

            if (request.MainCategoryId.HasValue)
            {
                condition += " and dc1.id=@mainCategoryId";
            }

            if (request.SecondCategoryId.HasValue)
            {
                condition += " and dc2.id=@secondCategoryId";
            }

            if (request.ChildCategoryId.HasValue)
            {
                condition += " and dc3.id=@childCategoryId";
            }

            if (request.IsOnSale.HasValue)
            {
                condition += " and fc.on_sale=@IsOnSale";
            }

            if (request.IsDeleted.HasValue)
            {
                condition += " and fc.is_deleted=@IsDeleted";
            }

            if (request.ParentProductId.HasValue)
            {
                condition += " and fc.parent_id=@parentId";
            }

            if (!string.IsNullOrWhiteSpace(request.PartCode))
            {
                condition += " and fc.part_code = @partCode";
            }

            if (!string.IsNullOrWhiteSpace(request.PartNo))
            {
                condition += " and fc.part_no = @partNo";
            }

            if (request.IsRetail.HasValue)
            {
                condition += " and fc.is_retail = @isRetail";
            }

            if (request.ProductAttributes != null && request.ProductAttributes.Count > 0)
            {
                var productAttributeCon = " and fc.product_attribute in @product_attribute";
                condition += productAttributeCon;
                brandCondition += productAttributeCon;
            }

            condition += " and fc.class_type=@classType";
            var paras = new
            {
                name = '%' + request.Key + '%',
                code = request.Key,
                startPrice = request.StartPrice,
                endPrice = request.EndPrice,
                brand = request.Brand,
                mainCategoryId = request.MainCategoryId,
                secondCategoryId = request.SecondCategoryId,
                childCategoryId = request.ChildCategoryId,
                IsOnSale = request.IsOnSale,
                IsDeleted = request.IsDeleted,
                classType = request.ClassType,
                parentId = request.ParentProductId,
                product_attribute = request.ProductAttributes,
                partCode = request.PartCode,
                partNo = request.PartNo,
                isRetail = request.IsRetail
            };
            var totalCount = 0;
            var products = _productRepository.SearchProduct(request, condition, paras, out totalCount);
            var result = new ProductSearchResponse();
            result.Items = products;
            result.TotalItems = totalCount;
            //搜索子产品 
            if (request.ClassType == 2)
            {
                var brands = _productRepository.GetDistinctProductBrands(brandCondition, paras);
                result.Brands = brands;
            }

            return result;
        }

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ProductBaseInfoVo>> SearchProductCommon(SearchProductCommonRequest request)
        {
            ApiPagedResultData<ProductBaseInfoVo> result = new ApiPagedResultData<ProductBaseInfoVo>();
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId != null)
            {
                categoryId.Add(request.ChildCategoryId.Value);
            }
            else if (request.MainCategoryId != null || request.SecondCategoryId != null)
            {
                List<DimCategoryDO> category = await GetAllCategoryCache(0);
                if (request.SecondCategoryId != null)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId.Value).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId != null)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId.Value).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var product = await _productRepository.SearchProductCommon(new SearchProductCommonCondition()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                OnSale = request.OnSale,
                ProductAttribute = request.ProductAttribute,
                CategoryId = categoryId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    result.Items = _mapper.Map<List<ProductBaseInfoVo>>(product.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取所有产品类目
        /// </summary>
        /// <returns></returns>
        private async Task<List<DimCategoryDO>> GetAllCategoryCache(long shopId)
        {
            //var timestamp = DateTime.Now.ToString("yyyyMMdd");
            //var key = $"{redisKey}:GetAllProductCategory:{timestamp}";
            //var productCategory = await _redisClient.GetOrSetAsync(key, () => _categoryRepository.GetAllCategory(shopId),
            //    new TimeSpan(0, 0, 30, 0));
            var productCategory = await _categoryRepository.GetAllCategory(shopId);
            return productCategory;
        }

        /// <summary>
        /// 获取所有产品类目
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryInfoVo>> GetAllCategoryFromCache(long shopId)
        {
            var result = await GetAllCategoryCache(shopId);

            return _mapper.Map<List<CategoryInfoVo>>(result);
        }

        /// <summary>
        /// 获取产品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductAttributeVo>> GetProductAttribute(GetProductAttributeRequest request)
        {
            List<ProductAttributeVo> result = new List<ProductAttributeVo>();
            var products = await _productRepository.GetProductByPidList(request.ProductCodes);
            if (products != null && products.Any())
            {
                var pidList = products.Select(_ => _.Id.ToString()).ToList();
                var attributes = await _productAttributevalueRepository.GetProductAttributeValueByIdAsync(pidList);
                products.ForEach(_ =>
                {
                    result.Add(new ProductAttributeVo()
                    {
                        ProductCode = _.ProductCode,
                        AttributeValues = attributes.Where(t => t.ProductId == _.Id.ToString()).ToList()
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// 获取产品类目分级展示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductCategoryVo>> GetProductCategory(GetProductCategoryRequest request)
        {
            List<ProductCategoryVo> category = new List<ProductCategoryVo>();
            var result = await GetAllCategoryCache(request.ShopId);
            var mainCategory = result.Where(_ => _.ParentId == 0).ToList();
            if (request.MainCategoryId != null && request.MainCategoryId.Any())
            {
                mainCategory = mainCategory.Where(_ => request.MainCategoryId.Contains(_.Id)).ToList();
            }

            mainCategory.ForEach(_ =>
            {
                ProductCategoryVo itemC = new ProductCategoryVo()
                {
                    CategoryId = _.Id,
                    CategoryCodeShort = _.CategoryCodeShort,
                    DisplayName = _.DisplayName,
                    CategoryLevel = _.CategoryLevel,
                    ParentId = _.ParentId,
                    Children = result.Where(x => x.ParentId == _.Id).Select(x => new ProductCategoryVo
                    {
                        CategoryId = x.Id,
                        CategoryCodeShort = x.CategoryCodeShort,
                        DisplayName = x.DisplayName,
                        CategoryLevel = x.CategoryLevel,
                        ParentId = x.ParentId,
                        Children = result.Where(t => t.ParentId == x.Id).Select(t => new ProductCategoryVo
                        {
                            CategoryId = t.Id,
                            CategoryCodeShort = t.CategoryCodeShort,
                            DisplayName = t.DisplayName,
                            CategoryLevel = t.CategoryLevel,
                            ParentId = t.ParentId
                        }).ToList()
                    }).ToList()
                };

                category.Add(itemC);
            });

            return category;
        }

        /// <summary>
        /// 搜索产品-子产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchProduct(SearchProductRequest request)
        {
            ApiPagedResultData<BaoYangPackageProductModel> result = new ApiPagedResultData<BaoYangPackageProductModel>()
            {
                Items = new List<BaoYangPackageProductModel>()
            };
            var pageProduct =
                await _productRepository.GetProductsByContent(request.Content, request.PageIndex, request.PageSize);

            var shopGroupProducts = await _shopManageClient.GetShopGrouponProduct(new GetShopGrouponProductRequest() { ShopId = request.ShopId, Status = 1 });

            if (pageProduct != null)
            {
                result.TotalItems = pageProduct.TotalItems;
                var product = pageProduct.Items?.ToList() ?? new List<FctProductDO>();
                if (product.Any())
                {
                    List<ProductPackageVo> packageResult = new List<ProductPackageVo>();
                    List<ProductAllInfoVo> childProduct = new List<ProductAllInfoVo>();
                    List<BaoYangTypeConfigDto> baoYangType = new List<BaoYangTypeConfigDto>();
                    var packageProduct =
                        product.Where(_ => _.ProductAttribute == 1).Select(_ => _.ProductCode).ToList();
                    if (packageProduct.Any())
                    {
                        packageResult = GetPackageProductsByCode(packageProduct);
                        var childPid = packageResult.SelectMany(_ => _.Details).Where(_ => _.ProjectType == 1)
                            .Select(_ => _.ProjectId).ToList();
                        childProduct = _productRepository.GetProductsByProductCode(childPid);
                        baoYangType = await _baoYangClient.GetBaoYangTypeConfig();
                    }

                    product.ForEach(_ =>
                    {
                        string image = string.Empty;
                        if (!string.IsNullOrEmpty(_.Image1))
                        {
                            image = $"{_configuration["ImageDomain"]}{_.Image1}";
                        }

                        BaoYangPackageProductModel item = new BaoYangPackageProductModel()
                        {
                            Pid = _.ProductCode,
                            Brand = _.Brand,
                            DisplayName = _.DisplayName,
                            Name = _.Name,
                            Image = image,
                            Price = _.SalesPrice,
                            OriginalPrice = _.StandardPrice > 0 ? _.StandardPrice : _.SalesPrice,
                            Unit = _.Unit,
                            StockOut = _.Stockout > 0,
                            IsPackageProduct = _.ProductAttribute == 1
                        };

                        var shopGroupProduct = shopGroupProducts?.Data?.Where(t => t.ServiceId == item.Pid)?.FirstOrDefault();
                        if (shopGroupProduct != null)
                        {
                            item.Price = shopGroupProduct.Price;
                        }

                        List<BaoYangPackageProductModel> childItem = new List<BaoYangPackageProductModel>();

                        if (_.ProductAttribute == 1)
                        {
                            var defaultChild =
                                packageResult.FirstOrDefault(x => x.PackageInfo.ProductCode == _.ProductCode)?.Details;
                            if (defaultChild != null && defaultChild.Any())
                            {
                                defaultChild.ForEach(t =>
                                {
                                    if (t.ProjectType == 1)
                                    {
                                        var defaultChildItem =
                                            childProduct.FirstOrDefault(f => f.ProductCode == t.ProjectId);
                                        if (defaultChildItem != null)
                                        {
                                            string image1 = string.Empty;
                                            if (!string.IsNullOrEmpty(defaultChildItem.Image1))
                                            {
                                                image1 = $"{_configuration["ImageDomain"]}{defaultChildItem.Image1}";
                                            }

                                            childItem.Add(new BaoYangPackageProductModel()
                                            {
                                                Pid = defaultChildItem.ProductCode,
                                                Brand = defaultChildItem.Brand,
                                                DisplayName = defaultChildItem.DisplayName,
                                                Name = defaultChildItem.Name,
                                                Image = image1,
                                                Price = defaultChildItem.SalesPrice,
                                                OriginalPrice = defaultChildItem.StandardPrice > 0
                                                    ? defaultChildItem.StandardPrice
                                                    : defaultChildItem.SalesPrice,
                                                Unit = defaultChildItem.Unit,
                                                StockOut = defaultChildItem.Stockout > 0,
                                                ReplaceProduct = t.ReplaceProduct == 1,
                                                PackageDetailId = t.Id
                                            });
                                        }
                                    }
                                    else if (t.ProjectType == 2)
                                    {
                                        childItem.Add(new BaoYangPackageProductModel()
                                        {
                                            Pid = "",
                                            Brand = t.ProjectBrands?.FirstOrDefault() ?? string.Empty,
                                            DisplayName = t.ProjectName,
                                            //   Name = t.ProjectName,
                                            Image = baoYangType.FirstOrDefault(f => f.BaoYangType == t.ProjectId)
                                                ?.ImageUrl ?? string.Empty,
                                            ReplaceProduct = t.ReplaceProduct == 1,
                                            PackageDetailId = t.Id
                                        });
                                    }
                                });
                            }
                        }

                        item.ChildProducts = childItem;

                        result.Items.Add(item);
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        public List<ProductDetailVo> GetProductsByProductCode(ProductDetailSearchRequest request)
        {
            if (request.ProductCodes == null || !request.ProductCodes.Any())
            {
                throw new CustomException("productCodes 必填");
            }

            if (request.ProductCodes.Count > 100)
            {
                //throw new CustomException("productCodes 数量不能超过100");
            }

            var result = new List<ProductDetailVo>();
            //商品信息
            var productInfos = _productRepository.GetProductsByProductCode(request.ProductCodes);
            if (!productInfos.Any())
            {
                return result;
            }

            var pids = productInfos?.Select(t => t.Id.ToString()).Distinct().ToList();
            //查询条件
            var conditon = " where is_deleted=0 and product_id in @productids ";
            var paras = new
            {
                productids = pids
            };
            //安装服务
            var installservice = _productInstallRepository.GetList(conditon, paras, false);
            //属性信息
            var attributevalues = new List<ProductAttributevalueVo>();

            attributevalues = _productAttributevalueRepository.GetProductAttributeValueById(pids);

            foreach (var item in productInfos)
            {
                var productDetail = new ProductDetailVo();
                productDetail.Attributevalues =
                    attributevalues?.Where(t => t.ProductId == item.Id.ToString())?.ToList();
                var productServices = installservice?.Where(t => t.ProductId == item.Id.ToString())?.ToList();
                productDetail.InstallationServices = _mapper.Map<List<ProductInstallserviceVo>>(productServices);
                productDetail.Product = item;
                result.Add(productDetail);
            }

            return result;
        }

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ProductSearchInfoVo>> SelectProductsByCategory(CategoryProductRequest request)
        {
            if (request.PageSize > 100)
            {
                request.PageSize = 100;
            }

            if (!request.CategoryId.HasValue)
            {
                throw new CustomException("CategoryId 必填");
            }

            var category = _categoryRepository.Get(request.CategoryId);
            if (category == null || string.IsNullOrEmpty(category.CategoryCode))
            {
                throw new CustomException("类目信息不存在！");
            }

            var result = new ApiPagedResultData<ProductSearchInfoVo>();
            int totalCount = 0;
            var productBaseInfos =
                _productRepository.GetProductsByCategory(request, category.CategoryLevel, out totalCount);
            if (productBaseInfos.Any())
            {
                List<ProductAttributevalueVo> attribute = new List<ProductAttributevalueVo>();
                List<RelProductInstallserviceDO> installService = new List<RelProductInstallserviceDO>();
                Task<List<ProductAttributevalueVo>> attributeTask = null;
                Task<List<RelProductInstallserviceDO>> installServiceTask = null;
                var pidList = productBaseInfos.Select(t => t.Id.ToString()).Distinct().ToList();

                if (request.HasAttribute)
                {
                    attributeTask = _productAttributevalueRepository.GetProductAttributeValueByIdAsync(pidList);
                }

                if (request.HasInstallService)
                {
                    installServiceTask = _productInstallRepository.GetRelInstallServiceByProductIds(pidList);
                }

                if (attributeTask != null)
                {
                    attribute = await attributeTask ?? new List<ProductAttributevalueVo>();
                }

                if (installServiceTask != null)
                {
                    installService = await installServiceTask ?? new List<RelProductInstallserviceDO>();
                }

                if (request.HasAttribute || request.HasInstallService)
                {
                    productBaseInfos.ForEach(_ =>
                    {
                        _.Attributevalues = attribute.Where(t => t.ProductId == _.Id.ToString()).ToList();
                        _.InstallationServices = installService.Where(t => t.ProductId == _.Id.ToString()).Select(x =>
                            new ProductInstallserviceVo
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                ServiceId = x.ServiceId,
                                ServiceName = x.ServiceName,
                                ServicePrice = x.ServicePrice,
                                Sort = x.Sort
                            }).ToList();
                    });
                }
            }

            result.Items = productBaseInfos;
            result.TotalItems = totalCount;
            return result;
        }

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="packageCodes"></param>
        /// <returns></returns>
        public List<ProductPackageVo> GetPackageProductsByCode(List<string> packageCodes)
        {
            if (packageCodes == null || !packageCodes.Any())
            {
                throw new CustomException("packageCodes 必填");
            }

            if (packageCodes.Count > 100)
            {
                throw new CustomException("packageCodes 数量不能超过100");
            }

            //查询条件
            var conditon = " where is_deleted=0 and  product_attribute=1 and product_code in @packageCodes ";
            var paras = new
            {
                packageCodes = packageCodes
            };
            var result = new List<ProductPackageVo>();
            var productDOs = _productRepository.GetList(conditon, paras);
            var packageIds = productDOs.Select(t => t.Id).ToList();
            var packageInfos = _mapper.Map<List<PackageInfoVo>>(productDOs);
            if (productDOs.Any())
            {
                var con = " where is_deleted=0  and package_pid in @package_pid";
                var packageDetailInfoDos = _productPackageRepository.GetList(con, new { package_pid = packageIds });
                foreach (var item in packageInfos)
                {
                    var productPackage = new ProductPackageVo();
                    productPackage.PackageInfo = item;
                    var packageDetails = packageDetailInfoDos?.Where(t => t.PackagePid == item.PackageId).ToList();
                    productPackage.Details = packageDetails.Select(a => new ProductPackageDetailVo()
                    {
                        Id = a.Id,
                        IsDeleted = a.IsDeleted,
                        ProjectBrands = a.ProjectBrand?.Split(',').ToList(),
                        ProjectId = a.ProjectId,
                        ProjectName = a.ProjectName,

                        ProjectType = a.ProjectType,
                        Quantity = a.Quantity,
                        SalesPrice = a.SalesPrice,
                        Sort = a.Sort,
                        ReplaceProduct = a.ReplaceProduct,
                        CategoryId = a.CategoryId,
                        ShopCategoryId = a.ShopCategoryId,
                        StandardPrice = a.StandardPrice
                    }).ToList();
                    result.Add(productPackage);
                }
            }

            return result;
        }

        /// <summary>
        /// 查询关联商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public AssociateProductVo GetAssociateProductsByCodes(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                throw new CustomException("productCode 必填");
            }

            var result = new AssociateProductVo();
            var conditon = " where is_deleted=0 and product_code=@productCode ";
            var paras = new
            {
                productCode = productCode
            };
            var associateproductDetailDOs = _associateproductDetailRepository.GetList(conditon, paras);
            if (associateproductDetailDOs.Any())
            {
                var associateproductDetail = associateproductDetailDOs.FirstOrDefault();
                var associateproductDO = _associateproductRepository.Get(associateproductDetail.GroupId);
                var attributeNames = associateproductDO?.Attributes.Split(",").ToList();
                var associateProductDetails =
                    _associateproductRepository.GetProductsattributevalueById(associateproductDetail.GroupId,
                        attributeNames);
                result.Attributes = attributeNames;
                result.GroupName = associateproductDO.GroupName;
                result.CategoryId = associateproductDO.CategoryId;
                result.Id = associateproductDO.Id;
                result.Details = associateProductDetails;
            }

            return result;
        }

        /// <summary>
        /// 批量生成字产品
        /// </summary>

        public void BatchGenerationProducts(BatchGenerationProductsRequest request)
        {
            if (request.CategoryId == null || !request.CategoryId.Any())
            {
                return;
            }
            string categoryIds = string.Join(",", request.CategoryId);
            var condition =
                $" where is_deleted=0 and class_type=4 and `id` >={request.MinId} and category_id in({categoryIds})";
            var fctProductDtos = _associateproductRepository.GetList<FctProductDO>(condition);
            var productDOList = new List<FctProductDO>();
            foreach (FctProductDO item in fctProductDtos)
            {
                var productAllInfo = _productRepository.GetProductsByProductCode(new List<string>() { item.ProductCode })
                    .FirstOrDefault();
                productAllInfo.ParentId = productAllInfo.Id.Value;
                productAllInfo.Id = 0;
                productAllInfo.ClassType = 2;
                productAllInfo.ProductCode = GetProductCode(productAllInfo.ChildCategoryId, 0);
                var dtNow = DateTime.Now;
                var productDO = _mapper.Map<FctProductDO>(productAllInfo);
                productDO.CreateBy = "rg";
                productDO.CreateTime = dtNow;
                productDO.UpdateBy = "rg";
                productDO.UpdateTime = dtNow;
                productDO.CategoryId = productAllInfo.ChildCategoryId;
                productDOList.Add(productDO);
                if (productDOList.Count > 100)
                {
                    _productRepository.InsertBatch(productDOList);
                    productDOList.Clear();
                }
            }

            if (productDOList.Count > 0)
            {
                _productRepository.InsertBatch(productDOList);
            }
        }

        /// <summary>
        /// 根据 partNo 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ProductSearchInfoVo> SelectProductsByPartNos(ProductPartRequest request)
        {
            if (request.PartNos == null || !request.PartNos.Any())
            {
                throw new CustomException("partNos 必填");
            }

            if (request.PartNos.Count > 100)
            {
                throw new CustomException("partNos 数量不能超过100");
            }

            //查询条件
            var conditon = " where is_deleted=0 and class_type=2  and part_no in @partNos ";
            if (request.IsOnSale.HasValue)
            {
                conditon += " and on_sale=@on_sale";
            }

            var paras = new
            {
                partNos = request.PartNos,
                on_sale = request.IsOnSale
            };
            var result = new List<ProductSearchInfoVo>();
            var productDOs = _productRepository.GetList(conditon, paras);
            var productIds = productDOs.Select(t => t.Id.ToString()).ToList();
            var baseInfoVos = _mapper.Map<List<ProductSearchInfoVo>>(productDOs);
            if (baseInfoVos.Any() && request.HasAttribute)
            {
                //属性信息
                var attributevalue = _productAttributevalueRepository.GetProductAttributeValueById(productIds);
                foreach (var item in baseInfoVos)
                {
                    item.Attributevalues = attributevalue?.Where(t => t.ProductId == item.Id.ToString())?.ToList();
                }
            }

            return baseInfoVos;
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public List<CategoryAttributeVo> GetAttributesByCategoryId(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                throw new CustomException("categoryId 必填");
            }

            var result = _categoryAttributeRepository.GetAttributesByCategoryId(categoryId);
            return result;
        }

        #endregion

        #region 编辑品牌

        /// <summary>
        /// 分页查询商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetProductBrandVo>> GetProductBrandList(GetProductBrandListRequest request)
        {
            var dalResponse = await _catalogBrandRepository.GetProductBrandList(request);
            ApiPagedResultData<GetProductBrandVo> response =
                _mapper.Map<ApiPagedResultData<GetProductBrandVo>>(dalResponse);
            return response;
        }

        /// <summary>
        /// 编辑商品品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditBrandAsync(AddBrandRequest request)
        {
            var existBrand = await _catalogBrandRepository.GetProductBrandList(new GetProductBrandListRequest()
            {
                Id = request.Id,
                IsForbid = (int)IsForbidEnumType.All
            });
            if (existBrand.Items == null || !existBrand.Items.Any() || existBrand.Items.Count > 1)
            {
                throw new CustomException("品牌不存在");
            }

            var result = await _catalogBrandRepository.EditCategoryBrandAsync(new DimBrandDO()
            {
                Id = existBrand?.Items?.FirstOrDefault()?.Id ?? 0,
                BrandName = request.BrandName,
                BrandImg = request.BrandImg ?? String.Empty,
                UpdateBy = request.UserId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                CreateBy = request.UserId,
                IsDeleted = request.IsForbid
            });
            return result > 0;
        }

        #endregion

        #region 编辑单位

        /// <summary>
        /// 分页查询商品的单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetProductUnitListVo>> GetProductUnitList(
            GetProductUnitListRequest request)
        {
            var dalResponse = await _unitRepository.GetProductUnitList(request);
            ApiPagedResultData<GetProductUnitListVo> response =
                _mapper.Map<ApiPagedResultData<GetProductUnitListVo>>(dalResponse);
            return response;
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddUnitAsync(AddUnitRequest request)
        {
            var existBrand = await _unitRepository.GetProductUnitList(new GetProductUnitListRequest()
            {
                UnitName = request.UnitName,
                IsForbid = (int)IsForbidEnumType.All,
            });
            if (existBrand.Items != null && existBrand.Items.Any())
            {
                throw new CustomException("单位已存在");
            }

            var result = await _unitRepository.InsertAsync(new DimUnitDO()
            {
                UnitName = request.UnitName,
                UpdateBy = request.UserId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                CreateBy = request.UserId
            });
            return result > 0;
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUnitAsync(AddUnitRequest request)
        {
            var existBrand = await _unitRepository.GetProductUnitList(new GetProductUnitListRequest()
            {
                Id = request.Id,
                IsForbid = (int)IsForbidEnumType.All
            });
            if (existBrand.Items == null || !existBrand.Items.Any() || existBrand.Items.Count > 1)
            {
                throw new CustomException("单位不存在");
            }

            var result = await _unitRepository.EditUnitAsync(new DimUnitDO()
            {
                Id = existBrand?.Items?.FirstOrDefault()?.Id ?? 0,
                UnitName = request.UnitName,
                UpdateBy = request.UserId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                CreateBy = request.UserId,
                IsDeleted = request.IsForbid ? (sbyte)IsForbidEnumType.Yes : (sbyte)IsForbidEnumType.No
            });
            return result > 0;
        }

        #endregion

        #region 编辑商品

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveProduct(ProductEditRequest request)
        {
            if (request == null || request.Product == null)
            {
                throw new CustomException("商品信息必填！");
            }

            //商品信息
            var productId = "";
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var productDO = _mapper.Map<FctProductDO>(request.Product);
            if (request.Product.Id.HasValue)
            {
                productId = request.Product.Id.ToString();
            }

            productDO.CategoryId = request.Product.ChildCategoryId;
            var conditon = "";
            if (!request.IsEdit)
            {
                //验证商品名称是否重复
                conditon = " where is_deleted=0 and name=@name and class_type=@classtype";
                var paraName = new
                {
                    name = productDO.Name,
                    classtype = productDO.ClassType
                };
                var sameProductDOs = _productRepository.GetList(conditon, paraName);
                if (!sameProductDOs.IsNullOrEmpty())
                {
                    throw new CustomException("商品名称不能重复!");
                }

                //获取商品编码
                productDO.ProductCode = GetProductCode(productDO.CategoryId, productDO.ProductAttribute);
                productDO.IsDeleted = 0;
                productDO.OnSale = 0;
                productDO.CreateBy = userId;
                productDO.CreateTime = dtNow;
                productDO.UpdateBy = string.Empty;
                productDO.UpdateTime = new DateTime(1900, 1, 1);
            }
            else
            {
                var currentProduct = _productRepository.Get(productDO.Id);
                //验证上下级逻辑
                if (productDO.OnSale != currentProduct.OnSale)
                {
                    if (productDO.ClassType == 2 && productDO.OnSale == 1) //子产品上架，需要验证父产品的状态
                    {
                        conditon = " where is_deleted=0 and id=@parent_id ";
                        var para = new
                        {
                            parent_id = productDO.ParentId
                        };
                        var parentProduct = _productRepository.GetList(conditon, para).FirstOrDefault();
                        if (parentProduct != null && parentProduct.OnSale == 0)
                        {
                            throw new CustomException("先上架父产品，再上架子产品!");
                        }
                    }
                    else if (productDO.ClassType == 4 && productDO.OnSale == 0) //父产品下架，需要验证子产品的状态
                    {
                        conditon = " where is_deleted=0 and parent_id=@id and on_sale=1 ";
                        var para = new
                        {
                            id = productDO.Id
                        };
                        var chiildProducts = _productRepository.GetList(conditon, para);
                        if (!chiildProducts.IsNullOrEmpty())
                        {
                            throw new CustomException("子产品上架中，不能下降!");
                        }
                    }
                }

                productDO.UpdateBy = userId;
                productDO.UpdateTime = dtNow;
            }

            //安装服务
            var installationServices = request.InstallationServices?.Select(t => new RelProductInstallserviceDO()
            {
                CreateBy = userId,
                UpdateBy = userId,
                CreateTime = dtNow,
                UpdateTime = dtNow,
                IsDeleted = t.IsDeleted,
                ServiceId = t.ServiceId,
                ProductId = productId,
                ServiceName = t.ServiceName,
                ServicePrice = t.ServicePrice,
                Id = t.Id,
                Pid = productDO.ProductCode,
                FreeInstall = t.FreeInstall,
                ChangeNum = t.ChangeNum
            }).ToList();
            //属性
            var attributevalues = request.Attributevalues?.Select(t => new RelProductAttributevalueDO()
            {
                ProductId = productId,
                AttributeNameId = t.AttributeNameId,
                CreateBy = userId,
                UpdateBy = userId,
                CreateTime = dtNow,
                UpdateTime = dtNow,
                IsDeleted = t.IsDeleted,
                ProductAttributeValue = t.AttributeValue,
                Id = t.Id
            }).ToList();

            //套餐明细
            List<RelProductPackageDO> packageDetails = new List<RelProductPackageDO>();
            request.PackageDetails?.ForEach(_ =>
            {
                if (!string.IsNullOrEmpty(_.CategoryId))
                {
                    List<string> list = _.CategoryId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (list.Exists(t => !Int32.TryParse(t, out int c)))
                    {
                        throw new CustomException("替换产品类目输入有误");
                    }
                }

                if (!string.IsNullOrEmpty(_.ShopCategoryId))
                {
                    List<string> list = _.ShopCategoryId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    if (list.Exists(t => !Int32.TryParse(t, out int c)))
                    {
                        throw new CustomException("替换外采类目输入有误");
                    }
                }

                packageDetails.Add(new RelProductPackageDO()
                {
                    PackagePid = productId,
                    CreateBy = userId,
                    UpdateBy = userId,
                    CreateTime = dtNow,
                    UpdateTime = dtNow,
                    IsDeleted = _.IsDeleted,
                    SalesPrice = _.SalesPrice,
                    ProjectName = _.ProjectName,
                    ProjectType = _.ProjectType,
                    ProjectId = _.ProjectId,
                    Quantity = _.Quantity,
                    StandardPrice = _.StandardPrice,
                    Sort = _.Sort,
                    ReplaceProduct = _.ReplaceProduct,
                    CategoryId = _.CategoryId,
                    ShopCategoryId = _.ShopCategoryId,
                    Id = _.Id,
                    ProjectBrand = _.ProjectBrands.IsNullOrEmpty() ? "" : string.Join(',', _.ProjectBrands)
                });
            });

            var result = SaveProductData(productDO, installationServices, attributevalues, packageDetails,
                request.IsEdit);

            if (result)
            {
                _baoYangClient.AutoInsertPartsAssociation(new AutoInsertPartsAssociationClientRequest()
                {
                    Pid = productDO.ProductCode,
                    SubmitBy = userId
                }).Wait();
            }

            return result;
        }


        /// <summary>
        /// 获取商品编码
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <param name="productAttribute"></param>
        /// <returns></returns>
        private string GetProductCode(int childCategoryId, int productAttribute)
        {
            var productCode = "";

            var category = _categoryRepository.GetAllCategoryById(childCategoryId).FirstOrDefault();
            if (category == null)
            {
                throw new CustomException("产品类目异常");
            }

            string seqName =
                $"{category.MainCategoryShortCode}-{category.SecondCategoryShortCode}-{category.ChildCategoryShortCode}";

            var isExistSeq = _dimSequenceRepository.GetSequenceBySeqName(seqName);
            if (isExistSeq == null)
            {
                _dimSequenceRepository.InsertAsync(new DimSequenceDO()
                {
                    CategoryId = childCategoryId,
                    CurrentVal = 0,
                    IncrementVal = 1,
                    SeqName = seqName
                });
            }

            //验证商品编码是否存在
            var isExist = true;
            var conditon = " where is_deleted=0 and product_code=@product_code ";
            while (isExist)
            {
                var seqId = _dimSequenceRepository.GenerateProductCode(seqName);
                productCode = seqName + "-" + seqId;
                //服务产品
                if (productAttribute == 2)
                {
                    productCode += "-FU";
                }

                var para = new
                {
                    product_code = productCode
                };
                var sameProductDOs = _productRepository.GetList(conditon, para);
                if (sameProductDOs.IsNullOrEmpty())
                {
                    isExist = false;
                }
            }

            return productCode;
        }

        /// <summary>
        /// 获取 Sequence 
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        public DimSequenceDO GetSequenceNameById(int childCategoryId)
        {
            var conditon = "where category_id=@category_id";
            var paras = new
            {
                category_id = childCategoryId
            };
            var sequences = _dimSequenceRepository.GetList(conditon, paras);
            if (sequences.Any() && sequences.Count() > 0)
            {
                return sequences.FirstOrDefault();
            }
            else
            {
                //TODO  不存在就插入一条记录，后续需要迁移到，类目编辑配置
                var seqName = _categoryRepository.GetAllCategoryById(childCategoryId).Select(t =>
                        t.MainCategoryShortCode + '-' + t.SecondCategoryShortCode + '-' + t.ChildCategoryShortCode)
                    .FirstOrDefault();
                var sequence = new DimSequenceDO()
                {
                    CategoryId = childCategoryId,
                    CurrentVal = 0,
                    IncrementVal = 1,
                    SeqName = seqName
                };
                var count = _dimSequenceRepository.Insert(sequence);
                if (count > 0)
                {
                    return sequence;
                }
            }

            return null;
        }

        /// <summary>
        /// 导入商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ImportProduct(ImportProductRequest request)
        {
            var reusult = new List<ProductImportVo>();
            if (request == null || request.Products.IsNullOrEmpty())
            {
                throw new CustomException("导入失败!");
            }

            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var productDos = request.Products.Select(t => new FctProductDO()
            {
                Name = t.Name,
                DisplayName = t.DisplayName,
                Brand = t.Brand,
                CategoryId = t.CategoryId,
                ClassType = 4,
                ProductAttribute = t.ProductAttribute,
                Unit = t.Unit,
                TaxRate = t.TaxRate,
                StandardPrice = t.StandardPrice,
                SalesPrice = t.SalesPrice,
                Length = t.Length,
                Width = t.Width,
                Height = t.Height,
                PartCode = t.PartCode,
                ProductCode = GetProductCode(t.CategoryId, t.ProductAttribute),
                IsDeleted = 0,
                OnSale = 0,
                CreateBy = userId,
                UpdateBy = userId,
                CreateTime = dtNow,
                UpdateTime = dtNow,
            }).ToList();
            return BatchSaveOrUpdateProductData(productDos);
        }

        /// <summary>
        /// 初始化轮胎数据
        /// </summary>
        /// <param name="specifications"></param>
        /// <param name="pattern"></param>
        /// <param name="price"></param>
        /// <param name="zaiZhong"></param>
        /// <param name="suDu"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<bool> IniTireProduct(string specifications, string pattern, string price, string zaiZhong, string suDu, string remark)
        {
            //var specifications = tireWidth + "/" + tireAspectRatio + "R" + tireRim;
            var tireWidth = specifications.Substring(0, 3);
            var tireAspectRatio = specifications.Substring(4, 2);
            var tireRim = specifications.Substring(7, 2);
            var meridian = specifications.Substring(6, 1);
            var brand = "Maxxis/玛吉斯";
            var name = $"玛吉斯（Maxxis）轮胎 {specifications} {zaiZhong}{suDu} {pattern}";
            if (!string.IsNullOrWhiteSpace(remark))
            {
                name += $"（{remark}）";
            }

            decimal.TryParse(price, out decimal salePrice);
            var productModel = new FctProductDO()
            {
                Name = name,
                DisplayName = name,
                Brand = brand,
                CategoryId = 122,
                ClassType = 4,
                ProductAttribute = 0,
                Unit = "个",
                TaxRate = 0,
                StandardPrice = salePrice + 100,
                SalesPrice = salePrice,
                ProductCode = GetProductCode(122, 0),
                IsDeleted = 0,
                OnSale = 1,
                CreateBy = "System",
                UpdateBy = "System",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var result = await _productRepository.InsertAsync(productModel);
            if (result > 0)
            {
                var childModel = new FctProductDO()
                {
                    ParentId = result,
                    Name = name,
                    DisplayName = name,
                    Brand = brand,
                    CategoryId = 122,
                    ClassType = 2,
                    ProductAttribute = 0,
                    Unit = "个",
                    TaxRate = 0,
                    StandardPrice = salePrice + 100,
                    SalesPrice = salePrice,
                    ProductCode = GetProductCode(122, 0),
                    IsDeleted = 0,
                    OnSale = 1,
                    CreateBy = "System",
                    UpdateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                var childResult = await _productRepository.InsertAsync(childModel);
                if (childResult > 0)
                {
                    List<RelProductAttributevalueDO> childAttribute = new List<RelProductAttributevalueDO>()
                    {
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 32, //胎宽
                            ProductAttributeValue = tireWidth,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 25, //扁平比
                            ProductAttributeValue = tireAspectRatio,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 29, //轮毂尺寸
                            ProductAttributeValue = tireRim,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 27, //轮胎花纹
                            ProductAttributeValue = pattern,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 33, //规格
                            ProductAttributeValue = specifications,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 74, //子午线结构
                            ProductAttributeValue = meridian,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 26, //载重指数
                            ProductAttributeValue = zaiZhong,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        },
                        new RelProductAttributevalueDO()
                        {
                            ProductId = childResult.ToString(),
                            AttributeNameId = 30, //速度级别
                            ProductAttributeValue = suDu,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        }
                    };
                    _productAttributevalueRepository.InsertBatch(childAttribute);
                }
            }

            return true;
        }

        /// <summary>
        ///  批量更新商品接口
        /// </summary>
        /// <param name="products">商品信息</param>
        /// <param name="updateFilds">更新商品字段</param>
        /// <returns></returns>
        public bool BatchUpdateProduct(BatchUpdateProductRequest request)
        {
            if (request.Products.IsNullOrEmpty())
            {
                throw new CustomException("products 必填！");
            }

            if (request.UpdateFilds.IsNullOrEmpty())
            {
                throw new CustomException("更新字段必填！");
            }

            if (request.Products.Count > 100)
            {
                throw new CustomException("products 更新数量不能超过100！");
            }

            var notExistProduct = request.Products.Where(t => string.IsNullOrEmpty(t.ProductCode)).ToList();
            if (!notExistProduct.IsNullOrEmpty())
            {
                throw new CustomException("商品编码必填！");
            }

            var packageCodes = request.Products.Select(t => t.ProductCode).ToList();
            //查询条件
            var conditon = " where is_deleted=0 and product_code in @packageCodes ";
            var paras = new
            {
                packageCodes = packageCodes
            };
            var productDOs = _productRepository.GetList(conditon, paras)?.ToList();
            //不允许更新的字段信息
            var exceptFileds = new List<string>()
            {
                "Id", "ClassType", "ParentId", "ProductCode", "Description", "TaxRate", "StandardPrice",
                "SalesPrice", "Unit", "Length", "Width", "Height", "Weight", "Color", "Image1", "Image2", "Image3",
                "Image4", "Image5", "PartCode", "PartNo",
                "ProductRefer", "ProductAttribute",
            };
            var updateProductDOs = _mapper.Map<List<FctProductDO>>(request.Products);
            var filds = request.UpdateFilds.Except(exceptFileds)?.ToArray();
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            if (!updateProductDOs.IsNullOrEmpty() && !filds.IsNullOrEmpty())
            {
                updateProductDOs.ForEach(item =>
                {
                    var productDO = productDOs.Where(t => t.ProductCode == item.ProductCode).FirstOrDefault();
                    if (productDO != null)
                    {
                        item.Id = productDO.Id;
                        item.CreateBy = userId;
                        item.CreateTime = dtNow;
                        item.UpdateBy = userId;
                        item.UpdateTime = dtNow;
                    }
                });
                return BatchSaveOrUpdateProductData(updateProductDOs, filds);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改或者新增商品
        /// </summary>
        /// <param name="fctProductDO"></param>
        /// <param name="relProductInstallserviceDOs"></param>
        /// <param name="relProductAttributevalueDOs"></param>
        /// <param name="packageDetails"></param>
        /// <param name="IsEdit"></param>
        /// <returns></returns>
        private bool SaveProductData(FctProductDO fctProductDO,
            List<RelProductInstallserviceDO> relProductInstallserviceDOs,
            List<RelProductAttributevalueDO> relProductAttributevalueDOs,
            List<RelProductPackageDO> packageDetails,
            bool IsEdit)
        {
            var result = false;
            var productId = 0;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (IsEdit)
                    {
                        var updateFields = new List<string>() { };
                        _productRepository.Update(fctProductDO, updateFields);
                        if (relProductInstallserviceDOs.Count > 0)
                        {
                            var addInstallservices = relProductInstallserviceDOs?.Where(t => t.Id == 0).ToList();
                            if (addInstallservices.Count > 0)
                            {
                                _productInstallRepository.InsertBatch(addInstallservices);
                            }

                            var updateInstallservices = relProductInstallserviceDOs?.Where(t => t.Id > 0).ToList();
                            updateInstallservices?.ForEach(t =>
                            {
                                _productInstallRepository.Update(t,
                                    new List<string>()
                                        {"IsDeleted", "FreeInstall", "ChangeNum", "UpdateTime", "UpdateBy"});
                            });
                        }

                        if (relProductAttributevalueDOs.Count > 0)
                        {
                            var addProductAttributevalues = relProductAttributevalueDOs?.Where(t => t.Id == 0).ToList();
                            if (addProductAttributevalues.Count > 0)
                            {
                                _productAttributevalueRepository.InsertBatch(addProductAttributevalues);
                            }

                            var updateProductAttributevalues =
                                relProductAttributevalueDOs?.Where(t => t.Id > 0).ToList();
                            updateProductAttributevalues?.ForEach(t =>
                            {
                                if (string.IsNullOrEmpty(t.ProductAttributeValue))
                                {
                                    t.IsDeleted = 1;
                                }

                                _productAttributevalueRepository.Update(t,
                                    new List<string>()
                                        {"ProductAttributeValue", "IsDeleted", "UpdateTime", "UpdateBy"});
                            });
                        }

                        if (packageDetails.Count > 0)
                        {
                            var addPackageDetails = packageDetails?.Where(t => t.Id == 0).ToList();
                            if (addPackageDetails.Count > 0)
                            {
                                _productPackageRepository.InsertBatch(addPackageDetails);
                            }

                            var updatePackageDetails = packageDetails?.Where(t => t.Id > 0).ToList();
                            updatePackageDetails?.ForEach(t =>
                            {
                                _productPackageRepository.Update(t,
                                    new List<string>()
                                    {
                                        "ProjectBrand", "Quantity", "IsDeleted", "UpdateTime", "UpdateBy", "Sort",
                                        "ReplaceProduct", "CategoryId", "ShopCategoryId"
                                    });
                            });
                        }
                    }
                    else
                    {
                        productId = _productRepository.Insert(fctProductDO);
                        if (relProductInstallserviceDOs.Any() && relProductInstallserviceDOs.Count > 0)
                        {
                            relProductInstallserviceDOs.ForEach(t => { t.ProductId = productId.ToString(); });
                            _productInstallRepository.InsertBatch(relProductInstallserviceDOs);
                        }

                        if (relProductAttributevalueDOs.Any() && relProductAttributevalueDOs.Count > 0)
                        {
                            relProductAttributevalueDOs.ForEach(t => { t.ProductId = productId.ToString(); });
                            _productAttributevalueRepository.InsertBatch(relProductAttributevalueDOs);
                        }

                        if (packageDetails.Any() && packageDetails.Count > 0)
                        {
                            packageDetails.ForEach(t => { t.PackagePid = productId.ToString(); });
                            _productPackageRepository.InsertBatch(packageDetails);
                        }
                    }

                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "编辑商品异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        ///  批量保存或更新商品
        /// </summary>
        /// <param name="productDos">商品信息</param>
        /// <param name="updateFilds">更新商品字段</param>
        /// <returns></returns>
        private bool BatchSaveOrUpdateProductData(List<FctProductDO> productDos, params string[] updateFilds)
        {
            var result = false;
            var productId = 0;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (updateFilds.IsNullOrEmpty())
                    {
                        _productRepository.InsertBatch(productDos);
                    }
                    else
                    {
                        productDos.ForEach(t => { _productRepository.Update(t, updateFilds); });
                    }

                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "批量保存商品异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }

            return result;
        }



        /// <summary>
        ///  批量更新商品图片接口
        /// </summary>
        /// <returns></returns>
        public bool BatchUpdateProductImage()
        {
            var condition = " where is_deleted=0  and category_id in(69,70,71,63) and class_type=4 and " +
                            " ( Remark <> 'updateImage' or Remark is null or  Remark ='' )";
            var fctProductDtos = _associateproductRepository.GetList<FctProductDO>(condition).ToList();
            var productDOList = new List<FctProductDO>();
            var userId = "ApolloErp";
            var dtNow = DateTime.Now;
            //var mainDirectory = @"D:\yongshengliu\06 产品数据\机油滤清器";
            //var mainDirectory = @"D:\yongshengliu\06 产品数据\4滤图片";
            //var mainDirectory = @"D:\yongshengliu\06 产品数据\空气滤2";
            //var mainDirectory = @"D:\yongshengliu\06 产品数据\空气滤1";
            //var mainDirectory = @"D:\yongshengliu\06 产品数据\空滤补充图片\燃油滤清器";
            var mainDirectory = @"D:\yongshengliu\06 产品数据\1-14 优冠";
            //更新的字段信息
            var filds = new List<string>()
                {"Image1", "Image2", "Image3", "Image4", "Image5", "UpdateBy", "UpdateTime", "Remark"};
            var qiniuHelper = new QiniuHelper(_configuration);
            foreach (FctProductDO item in fctProductDtos)
            {
                var filePath = mainDirectory + "//" + item.PartCode.Replace('/', '-');
                if (!Directory.Exists(filePath) || item.PartCode.IsNullOrEmpty())
                {
                    continue;
                }

                var files = Directory.GetFiles(filePath);
                var productDO = item;
                for (int i = 0; i < files.Length; i++)
                {
                    var localPath = files[i];
                    var imageUrl = qiniuHelper.UploadFile(localPath);
                    if (i == 0)
                    {
                        productDO.Image1 = imageUrl;
                    }
                    else if (i == 1)
                    {
                        productDO.Image2 = imageUrl;
                    }
                    else if (i == 2)
                    {
                        productDO.Image3 = imageUrl;
                    }
                    else if (i == 3)
                    {
                        productDO.Image4 = imageUrl;
                    }
                    else if (i == 4)
                    {
                        productDO.Image5 = imageUrl;
                        break;
                    }
                }

                productDO.UpdateBy = userId;
                productDO.UpdateTime = dtNow;
                productDO.Remark = "updateImage";
                productDOList.Add(productDO);
                if (productDOList.Count >= 100)
                {
                    BatchSaveOrUpdateProductData(productDOList, filds.ToArray());
                    productDOList.Clear();
                }
            }

            if (productDOList.Count > 0)
            {
                BatchSaveOrUpdateProductData(productDOList, filds.ToArray());
            }

            return true;
        }

        #endregion

        #region 编辑属性

        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <returns></returns>
        public ApiPagedResultData<AttributeNameVo> SearchAttribute(AttributeSearchRequest request)
        {
            var result = new ApiPagedResultData<AttributeNameVo>();
            var conditon = " where is_deleted=@isDeleted and ( attribute_name like @key or display_name like @key )";
            var paras = new
            {
                key = '%' + request.Key + '%',
                isDeleted = request.IsDeleted
            };
            var attributePage =
                _dimAttributemameRepository.GetListPaged(request.PageIndex, request.PageSize, conditon, "id", paras);
            result.Items = _mapper.Map<List<AttributeNameVo>>(attributePage.Items) ?? new List<AttributeNameVo>();
            result.TotalItems = attributePage.TotalItems;
            return result;
        }

        /// <summary>
        /// 获取所有的属性信息
        /// </summary>
        /// <returns></returns>
        public List<AttributeNameVo> SelectAttributeNames(AttributeNameSearchRequest request)
        {
            var conditon = " where is_deleted=0 ";
            if (!request.AttributeNameIds.IsNullOrEmpty())
            {
                conditon += " and id in @attributeNameIds";
            }

            var paras = new
            {
                attributeNameIds = request.AttributeNameIds
            };
            var attributemameDos = _dimAttributemameRepository.GetList(conditon, paras);
            var result = _mapper.Map<List<AttributeNameVo>>(attributemameDos);
            return result;
        }

        /// <summary>
        /// 导入商品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ImportProductAttribute(ImportProductAttributeRequest request)
        {
            if (request.ProductAttributes.IsNullOrEmpty())
            {
                throw new CustomException("ProductAttributes 必填！");
            }

            //查询条件
            var conditon = " where product_id in @productIds and is_deleted=0 ";
            var productIds = request.ProductAttributes.Select(t => t.ProductId).Distinct().ToList();
            var paras = new
            {
                productIds = productIds
            };
            var productAttributes = _productAttributevalueRepository.GetList(conditon, paras);
            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var relProductAttributevalueDOs = new List<RelProductAttributevalueDO>();
            foreach (ProductAttributeImportVo item in request.ProductAttributes)
            {
                var relProductAttributevalueDo = new RelProductAttributevalueDO();
                var productAttribute = productAttributes
                    .Where(t => t.ProductId == item.ProductId && t.AttributeNameId == item.AttributeNameId)
                    .FirstOrDefault();
                if (productAttribute != null)
                {
                    relProductAttributevalueDo.Id = productAttribute.Id;
                }

                relProductAttributevalueDo.CreateTime = dtNow;
                relProductAttributevalueDo.CreateBy = userId;
                relProductAttributevalueDo.UpdateTime = dtNow;
                relProductAttributevalueDo.UpdateBy = userId;
                relProductAttributevalueDo.IsDeleted = 0;
                relProductAttributevalueDo.ProductId = item.ProductId;
                relProductAttributevalueDo.ProductAttributeValue = item.AttributeValue;
                relProductAttributevalueDo.AttributeNameId = item.AttributeNameId;
                relProductAttributevalueDOs.Add(relProductAttributevalueDo);
            }

            if (!relProductAttributevalueDOs.IsNullOrEmpty())
            {
                return BatchSaveOrUpdateProductAttributevalueData(relProductAttributevalueDOs);
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 修改或者新增商品属性
        /// </summary>
        /// <param name="relProductAttributevalueDOs"></param>
        /// <returns></returns>
        private bool BatchSaveOrUpdateProductAttributevalueData(
            List<RelProductAttributevalueDO> relProductAttributevalueDOs)
        {
            var result = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (relProductAttributevalueDOs.Count > 0)
                    {
                        var addProductAttributevalues = relProductAttributevalueDOs?.Where(t => t.Id == 0).ToList();
                        if (addProductAttributevalues.Count > 0)
                        {
                            _productAttributevalueRepository.InsertBatch(addProductAttributevalues);
                        }

                        var updateProductAttributevalues = relProductAttributevalueDOs?.Where(t => t.Id > 0).ToList();
                        updateProductAttributevalues?.ForEach(t =>
                        {
                            if (string.IsNullOrEmpty(t.ProductAttributeValue))
                            {
                                t.IsDeleted = 1;
                            }

                            _productAttributevalueRepository.Update(t,
                                new List<string>() { "ProductAttributeValue", "IsDeleted", "UpdateTime", "UpdateBy" });
                        });
                    }

                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "编辑商品属性异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        /// 保存 编辑 属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveAttribute(AttributeEditRequest request)
        {
            if (request == null || request.AttributeName == null)
            {
                throw new CustomException("属性信息必填！");
            }

            var userId = request.UserId;
            var dtNow = DateTime.Now;
            var attributeNameId = 0;
            //属性名称
            var attributemameDo = _mapper.Map<DimAttributemameDO>(request.AttributeName);
            if (request.IsEdit)
            {
                attributemameDo.CreateBy = userId;
                attributemameDo.UpdateTime = dtNow;
                attributeNameId = attributemameDo.Id;
            }
            else
            {
                attributemameDo.CreateTime = dtNow;
                attributemameDo.UpdateBy = userId;
            }

            //属性值
            var attributevalueDos = request.AttributeValues?.Select(t =>
                new DimAttributevalueDO()
                {
                    Id = t.Id,
                    UpdateTime = dtNow,
                    UpdateBy = userId,
                    CreateTime = dtNow,
                    CreateBy = userId,
                    AttributeValue = t.AttributeValue,
                    IsDeleted = t.IsDeleted,
                    AttributeNameId = attributeNameId
                }).ToList();
            return SaveAttributeData(attributemameDo, attributevalueDos, request.IsEdit);
        }

        /// <summary>
        /// 保存或则更新商品属性
        /// </summary>
        /// <param name="dimAttributemame"></param>
        /// <param name="dimAttributevalues"></param>
        /// <param name="IsEdit"></param>
        /// <returns></returns>
        private bool SaveAttributeData(DimAttributemameDO dimAttributemame,
            List<DimAttributevalueDO> dimAttributevalues, bool IsEdit)
        {
            var result = false;
            var attributeNameId = 0;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (IsEdit)
                    {
                        _dimAttributemameRepository.Update(dimAttributemame, new List<string>()
                        {
                            "AttributeName", "DisplayName", "Description",
                            "ControlType", "DataType", "MinValue", "MaxValue", "MaxLength",
                            "IsRequired", "IsDeleted", "UpdateBy", "UpdateTime", "AttributeType", "Sort"
                        });
                        if (!dimAttributevalues.IsNullOrEmpty())
                        {
                            var dimAttributevaluesAdd = dimAttributevalues.Where(t => t.Id == 0)?.ToList();
                            var dimAttributevaluesUpdate = dimAttributevalues.Where(t => t.Id > 0)?.ToList();
                            if (!dimAttributevaluesAdd.IsNullOrEmpty())
                            {
                                _dimAttributevalueRepository.InsertBatch(dimAttributevaluesAdd);
                            }

                            if (!dimAttributevaluesUpdate.IsNullOrEmpty())
                            {
                                var updateFileds = new List<string>()
                                    {"AttributeNameId", "AttributeValue", "UpdateBy", "UpdateTime", "IsDeleted"};
                                dimAttributevaluesUpdate.ForEach((t) =>
                                {
                                    _dimAttributevalueRepository.Update(t, updateFileds);
                                });
                            }
                        }
                    }
                    else
                    {
                        attributeNameId = _dimAttributemameRepository.Insert(dimAttributemame);
                        if (!dimAttributevalues.IsNullOrEmpty())
                        {
                            dimAttributevalues.ForEach((t) => { t.AttributeNameId = attributeNameId; });
                            _dimAttributevalueRepository.InsertBatch(dimAttributevalues);
                        }
                    }

                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "保存商品属性异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }

            return result;
        }


        /// <summary>
        /// 获取单个属性信息
        /// </summary>
        /// <returns></returns>
        public AttributeResponse GetAttributeById(int attributeNameId)
        {
            var result = new AttributeResponse();
            var attributemame = _dimAttributemameRepository.Get(attributeNameId);
            result.AttributeName = _mapper.Map<AttributeNameVo>(attributemame);
            var conditon = " where  attribute_name_id=@attributeNameId and is_deleted=0";
            var paras = new
            {
                attributeNameId = attributeNameId
            };
            var attributevalues = _dimAttributevalueRepository.GetList(conditon, paras);
            result.AttributeValues = _mapper.Map<List<AttributevalueVo>>(attributevalues);
            return result;
        }

        #endregion

        #region 编辑类目

        /// <summary>
        /// 查询类目息 by 类目 和 类目 level  
        /// </summary>
        /// <param name="categoryId">类目Id</param>
        /// <param name="level">类目级别</param>
        /// <returns></returns>
        public List<CategoryInfoVo> GetCategorysById(int? categoryId, int? level)
        {
            if (!categoryId.HasValue)
            {
                throw new CustomException("categoryId 必填");
            }

            if (!level.HasValue)
            {
                throw new CustomException("level 必填");
            }

            IEnumerable<CategoryInfoVo> result = new List<CategoryInfoVo>();
            //查询条件
            var conditon = "";
            var paras = new
            {
                categoryId = categoryId,
                level = level
            };
            IEnumerable<DimCategoryDO> categoryDos = null;
            if (level == 1)
            {
                conditon = " where is_deleted=0 and category_level=@level ";
                categoryDos = _categoryRepository.GetList(conditon, paras, false);
            }
            else if (level == 2 || level == 3)
            {
                conditon = " where is_deleted=0 and category_level=@level and parent_id=@categoryId";
                categoryDos = _categoryRepository.GetList(conditon, paras, false);
            }

            result = _mapper.Map<List<CategoryInfoVo>>(categoryDos);
            return result.ToList();
        }

        public async Task<ApiResult<List<ListCategoryVo>>> ListCategory(GetCategoryListRequest request)
        {
            var result = Result.Failed<List<ListCategoryVo>>("加载异常");
            try
            {
                var response = new List<ListCategoryVo>();
                var conditon = "where shop_id=@ShopId";
                var paras = new
                {
                    ShopId = request.ShopId,
                };
                var list = (await _categoryRepository.GetListAsync(conditon, paras))?.ToList();
                if (list == null || !list.Any())
                {
                    result = Result.Success(new List<ListCategoryVo>(), "查询成功");
                    return result;
                }

                var firstCategories = list.FindAll(_ => _.CategoryLevel == 1 && _.ParentId == 0);
                if (!firstCategories.Any())
                {
                    result = Result.Success(new List<ListCategoryVo>(), "查询成功");
                    return result;
                }

                foreach (var firstCategory in firstCategories)
                {
                    var listCategoryVo = _mapper.Map<ListCategoryVo>(firstCategory);
                    var findSecondChildren = list.FindAll(_ => _.ParentId == firstCategory.Id);
                    if (findSecondChildren.Any())
                    {
                        foreach (var findSecondChild in findSecondChildren)
                        {
                            var secondChild = _mapper.Map<SecondCategoryVo>(findSecondChild);
                            var findThirdChildren = list.FindAll(_ => _.ParentId == findSecondChild.Id);
                            if (findThirdChildren.Any())
                            {
                                foreach (var findThirdChild in findThirdChildren)
                                {
                                    var thirdChild = _mapper.Map<ThirdCategoryVo>(findThirdChild);
                                    secondChild.Children.Add(thirdChild);
                                }
                            }

                            listCategoryVo.Children.Add(secondChild);
                        }
                    }

                    response.Add(listCategoryVo);
                }

                var displayName = request.DisplayName;
                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    response = response.Where(_ =>
                        _.CategoryCode == displayName || _.CategoryCodeShort == displayName ||
                        _.DisplayName.Contains(displayName) || _.Children.Exists(t =>
                            t.CategoryCode == displayName || t.CategoryCodeShort == displayName ||
                            t.DisplayName.Contains(displayName) || t.Children.Exists(x =>
                                x.CategoryCode == displayName || x.CategoryCodeShort == displayName ||
                                x.DisplayName.Contains(displayName)))).ToList();
                }

                result = Result.Success(response, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("ListCategoryEx", ex);
            }

            return result;
        }

        public async Task<ApiResult<DimCategoryVo>> GetCategory(int id)
        {
            var result = Result.Failed<DimCategoryVo>("加载异常");
            try
            {
                var dimCategoryDo = await _categoryRepository.GetAsync(id);
                var dimCategoryVo = _mapper.Map<DimCategoryVo>(dimCategoryDo);
                result = Result.Success(dimCategoryVo, "查询成功");
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("GetCategoryEx", ex);
            }

            return result;
        }

        /// <summary>
        /// 父级类目查子集类目集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<DimCategoryVo>>> GetCategoryByParentId(CategoryByParentIdRequest request)
        {
            var condition = "WHERE `parent_id` = @parentId";
            var list = (await _categoryRepository.GetListAsync(condition, new { parentId = request.ParentId })).ToList();
            return Result.Success(_mapper.Map<List<DimCategoryVo>>(list));
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetProductBrandVo>> GetAllProductBrandList()
        {
            var list = await _catalogBrandRepository.GetListAsync<DimBrandDO>("");

            return _mapper.Map<List<GetProductBrandVo>>(list);
        }

        public async Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect(GetCategoryListRequest request)
        {
            var result = Result.Failed<List<CategoryTreeSelectVo>>("加载异常");
            try
            {
                var listCategoryResult = await ListCategory(request);
                var listCategory = listCategoryResult.GetSuccessData();
                if (listCategory != null)
                {
                    var response = _mapper.Map<List<CategoryTreeSelectVo>>(listCategory);
                    result = Result.Success(response, "查询成功");
                }
                else
                {
                    result = Result.Failed<List<CategoryTreeSelectVo>>(listCategoryResult.Code,
                        listCategoryResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("CategoryTreeSelectEx", ex);
            }

            return result;
        }

        public async Task<ApiResult> UpdateCategory(DimCategoryVo vo)
        {
            var result = Result.Failed("修改失败");
            try
            {
                var categoryDo = _mapper.Map<DimCategoryDO>(vo);
                var count = await _categoryRepository.UpdateCategory(categoryDo);
                if (count > 0)
                {
                    result = Result.Success("修改成功");
                }
                else
                {
                    throw new CustomException("修改失败，类目编号或缩写已存在");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCategoryEx", ex);
            }

            return result;
        }

        public async Task<ApiResult> AddCategory(DimCategoryVo vo)
        {
            var result = Result.Failed("新增失败");
            try
            {
                vo.CreateTime = DateTime.Now;
                if (string.IsNullOrWhiteSpace(vo.CreateBy))
                {
                    throw new CustomException("必须指定创建人");
                }

                if (vo.ParentId > 0)
                {
                    var parentCategoryDo = await _categoryRepository.GetAsync(vo.ParentId);
                    if (parentCategoryDo != null)
                    {
                        vo.CategoryLevel = parentCategoryDo.CategoryLevel + 1;
                        if (vo.CategoryLevel > 3)
                        {
                            throw new CustomException("最多支持添加三级类目");
                        }
                    }
                    else
                    {
                        throw new CustomException("父级类目信息异常");
                    }
                }
                else
                {
                    vo.CategoryLevel = 1;
                }

                var dimCategoryDO = _mapper.Map<DimCategoryDO>(vo);
                var count = await _categoryRepository.AddCategory(dimCategoryDO);
                if (count > 0)
                {
                    result = Result.Success("新增成功");
                }
                else
                {
                    throw new CustomException("新增失败，请检查输入的编号是否已存在其他重复记录");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("AddCategoryEx", ex);
            }

            return result;
        }

        public async Task<ApiResult> DeleteCategory(DimCategoryVo vo)
        {
            var result = Result.Failed("删除失败");
            try
            {
                if (vo.Id <= 0)
                {
                    throw new CustomException("请指定要删除的项目ID");
                }

                if (string.IsNullOrWhiteSpace(vo.UpdateBy))
                {
                    throw new CustomException("请指定删除操作人");
                }

                vo.IsDeleted = 1;
                vo.UpdateTime = DateTime.Now;
                var categoryDo = _mapper.Map<DimCategoryDO>(vo);
                var count = await _categoryRepository.UpdateAsync(categoryDo,
                    new string[] { "IsDeleted", "UpdateBy", "UpdateTime" });
                if (count > 0)
                {
                    result = Result.Success("删除成功");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("DeleteCategoryEx", ex);
            }

            return result;
        }

        #endregion

        #region 编辑类目属性

        /// <summary>
        /// 保存 编辑 属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveCategoryAttribute(CategoryAttributeEditRequest request)
        {
            if (request == null || request.CategoryAttributes.IsNullOrEmpty())
            {
                throw new CustomException("类目属性信息必填！");
            }

            var userId = request.UserId;
            var dtNow = DateTime.Now;
            //类目属性信息
            var relCategoryAttributes = request.CategoryAttributes.Select(t => new RelCategoryAttributeDO()
            {
                UpdateTime = dtNow,
                UpdateBy = userId,
                CreateTime = dtNow,
                CreateBy = userId,
                IsDeleted = t.IsDeleted,
                AttributeMameId = t.AttributeNameId,
                Id = t.Id,
                CategoryId = request.CategoryId

            }).ToList();
            return SaveCategoryAttributeData(relCategoryAttributes);
        }


        /// <summary>
        /// 保存或则更新类目属性
        /// </summary>
        /// <param name="dimAttributevalues"></param>
        /// <returns></returns>
        private bool SaveCategoryAttributeData(List<RelCategoryAttributeDO> relCategoryAttributes)
        {
            var result = false;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    if (!relCategoryAttributes.IsNullOrEmpty())
                    {
                        var relAttributevaluesAdd = relCategoryAttributes.Where(t => t.Id == 0)?.ToList();
                        var relAttributevaluesUpdate = relCategoryAttributes.Where(t => t.Id > 0)?.ToList();
                        if (!relAttributevaluesAdd.IsNullOrEmpty())
                        {
                            _relCategoryAttributeRepository.InsertBatch(relAttributevaluesAdd);
                        }

                        if (!relAttributevaluesUpdate.IsNullOrEmpty())
                        {
                            var updateFileds = new List<string>() { "UpdateBy", "UpdateTime", "IsDeleted" };
                            relAttributevaluesUpdate.ForEach((t) =>
                            {
                                _relCategoryAttributeRepository.Update(t, updateFileds);
                            });
                        }
                    }

                    tran.Complete();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                    var errorMsg = "保存类目属性异常：" + e.Message;
                    _logger.Error(errorMsg);
                    throw new CustomException(errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<CategoryAttributeVo> SelectCategoryAttribute(CategoryAttributeRequest request)
        {
            var result = _categoryAttributeRepository.SelectCategoryAttribute(request);
            return result;
        }


        #endregion

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductListByServiceTypeResponse> GetProductListByServiceType(
            ProductListByServiceTypeRequest request)
        {
            ProductListByServiceTypeResponse result = new ProductListByServiceTypeResponse();
            int categoryId = request.CategoryId;
            var vehicle = request.Vehicle;
            var shopProductTask =
                _fctShopProductRepository.GetShopProductByCategoryId(categoryId, request.ShopId); //门店商品
            int groupId = 0;
            if (categoryId == 1)
            {
                //保养
                var baoYangResult = await _baoYangClient.GetBaoYangPackagesAllProductAsync(
                    new GetBaoYangPackagesRequest()
                    {
                        Vehicle = _mapper.Map<VehicleClientRequest>(request.Vehicle),
                        UserId = request.UserId,
                        Channel = request.Channel,
                        ShopId = request.ShopId
                    });
                int dxByGroupId = 0;
                var baoYangTypeData = baoYangResult.SelectMany(_ => _.Items).ToList();
                foreach (var itemBy in baoYangTypeData)
                {
                    var byGroupId = 0;
                    if (itemBy.BaoYangType == "xby-tc" || itemBy.BaoYangType == "dby-tc")
                    {
                        if (dxByGroupId == 0)
                        {
                            groupId++;
                            byGroupId = groupId;
                            dxByGroupId = byGroupId;
                        }
                        else
                        {
                            byGroupId = dxByGroupId;
                        }
                    }
                    else
                    {
                        groupId++;
                        byGroupId = groupId;
                    }

                    var property = itemBy.Property;
                    foreach (var itemProduct in itemBy.Products)
                    {
                        BaoYangPackageProductModel product = new BaoYangPackageProductModel()
                        {
                            Pid = itemProduct.Pid,
                            Brand = itemProduct.Brand,
                            DisplayName = itemProduct.DisplayName,
                            Description = itemProduct.Description,
                            Image = itemProduct.Image,
                            Price = itemProduct.Price,
                            OriginalPrice = itemProduct.MarketPrice > 0 ? itemProduct.MarketPrice : itemProduct.Price,
                            Count = itemProduct.Count,
                            Unit = itemProduct.Unit,
                            Tags = itemProduct?.Tags?.Select(t => new TagInfo
                            {
                                Tag = t.Tag,
                                TagColor = t.TagColor,
                                Type = t.Type
                            })?.ToList() ?? new List<TagInfo>(),
                            StockOut = itemProduct.StockOut,
                            IsOriginal = itemProduct.IsOriginal,
                            FeedbackRate = itemProduct.FeedbackRate,
                            IsPackageProduct = itemProduct.IsPackageProduct,
                            IsAttention = itemProduct.IsAttention,
                            IsDefaultSelect = false,
                            GotoProductDetail = true,
                            GroupId = byGroupId,
                            ReplaceProduct = itemProduct.ReplaceProduct,
                            PackageDetailId = itemProduct.PackageDetailId,
                            ChildProducts = itemProduct.ChildProducts?.Select(t => new BaoYangPackageProductModel()
                            {
                                Pid = t.Pid,
                                Brand = t.Brand,
                                DisplayName = t.DisplayName,
                                Description = t.Description,
                                Image = t.Image,
                                Price = t.Price,
                                Count = t.Count,
                                Unit = t.Unit,
                                Tags = t.Tags?.Select(x => new TagInfo
                                {
                                    Tag = x.Tag,
                                    TagColor = x.TagColor,
                                    Type = x.Type
                                })?.ToList() ?? new List<TagInfo>(),
                                StockOut = t.StockOut,
                                IsOriginal = t.IsOriginal,
                                IsAttention = t.IsAttention,
                                IsDefaultSelect = false,
                                GotoProductDetail = false,
                                ReplaceProduct = t.ReplaceProduct,
                                PackageDetailId = t.PackageDetailId,
                                FeedbackRate = t.FeedbackRate
                            }) ?? new List<BaoYangPackageProductModel>()
                        };
                        if (property != null)
                        {
                            product.Property = _mapper.Map<PropertySelectModel>(property);
                        }

                        result.Products.Add(product);
                    }
                }
            }
            else if (categoryId == 2)
            {
                //轮胎
                var tireResult = await _baoYangClient.GetTireCategoryListAllProductAsync(new TireCategoryListRequest()
                {
                    VehicleId = vehicle.VehicleId,
                    Tid = vehicle.Tid,
                    TireSize = vehicle.TireSize,
                    ShopId = request.ShopId,
                    UserId = request.UserId
                });
                result.DefaultTireSize = tireResult?.DefaultTireSize ?? string.Empty;
                result.TireSizes = tireResult?.TireSizes ?? new List<string>();
                var categoryList = tireResult?.TireCategory ?? new List<TireCategoryDto>();
                foreach (var itemTire in categoryList)
                {
                    var products = itemTire.Products;
                    if (products != null && products.Any())
                    {
                        groupId++;
                        foreach (var itemProduct in products)
                        {
                            BaoYangPackageProductModel product = new BaoYangPackageProductModel()
                            {
                                Pid = itemProduct.Pid,
                                Brand = itemProduct.Brand,
                                DisplayName = itemProduct.DisplayName,
                                Image = itemProduct.Image,
                                Price = itemProduct.Price,
                                OriginalPrice = itemProduct.MarketPrice > 0
                                    ? itemProduct.MarketPrice
                                    : itemProduct.Price,
                                Count = itemProduct.Count,
                                Unit = itemProduct.Unit,
                                Tags = itemProduct.Tags?.Select(t => new TagInfo
                                { Tag = t.Tag, TagColor = t.TagColor, Type = t.Type })?.ToList() ??
                                       new List<TagInfo>(),
                                StockOut = itemProduct.StockOut,
                                IsOriginal = itemProduct.IsOriginal,
                                IsPackageProduct = itemProduct.IsPackageProduct,
                                IsAttention = itemProduct.IsAttention,
                                IsDefaultSelect = false,
                                GotoProductDetail = true,
                                GroupId = groupId,
                                ChildProducts =
                                    itemProduct.ChildProducts?.Select(t => new BaoYangPackageProductModel
                                    {
                                        Pid = t.Pid,
                                        Brand = t.Brand,
                                        DisplayName = t.DisplayName,
                                        Image = t.Image,
                                        Price = t.Price,
                                        Count = t.Count,
                                        Tags = t.Tags?.Select(x => new TagInfo
                                        { Tag = x.Tag, TagColor = x.TagColor, Type = x.Type })?.ToList() ??
                                               new List<TagInfo>(),
                                        StockOut = t.StockOut,
                                        IsOriginal = t.IsOriginal,
                                        IsAttention = t.IsAttention,
                                        IsDefaultSelect = false,
                                        GotoProductDetail = false
                                    }) ?? new List<BaoYangPackageProductModel>(),
                            };
                            result.Products.Add(product);
                        }
                    }
                }
            }
            else if (categoryId == 3)
            {
                //美容洗车
                var normalWash = (await SelectProductsByCategory(new CategoryProductRequest
                { CategoryId = 41, HasAttribute = false, IsOnSale = 1, PageIndex = 1, PageSize = 100 })).Items
                    .ToList();
                var specialWash = (await SelectProductsByCategory(new CategoryProductRequest
                { CategoryId = 43, HasAttribute = false, IsOnSale = 1, PageIndex = 1, PageSize = 100 })).Items
                    .ToList();
                normalWash.AddRange(specialWash);
                if (normalWash.Any())
                {
                    if (request.ShopId > 0)
                    {
                        var allPid = normalWash.Select(_ => _.ProductCode).ToList();
                        var service = await _shopManageClient.GetShopServiceListAsync(new ShopServiceListClientRequest()
                        {
                            ShopId = request.ShopId,
                            ProductIds = allPid
                        });
                        var onSalePid = service?.Where(_ => _.IsOnline)?.Select(_ => _.Pid)?.ToList() ??
                                        new List<string>();
                        normalWash = normalWash.Where(_ => onSalePid.Contains(_.ProductCode)).ToList();
                    }

                    groupId++;
                    foreach (var itemWash in normalWash)
                    {
                        string image = string.Empty;
                        if (!string.IsNullOrEmpty(itemWash.Image1))
                        {
                            image = $"{_configuration["ImageDomain"]}{itemWash.Image1}";
                        }

                        var product = new BaoYangPackageProductModel
                        {
                            Pid = itemWash.ProductCode,
                            Brand = itemWash.Brand,
                            DisplayName = itemWash.DisplayName,
                            Image = image,
                            Price = itemWash.SalesPrice,
                            OriginalPrice = itemWash.StandardPrice > 0 ? itemWash.StandardPrice : itemWash.SalesPrice,
                            Count = 1,
                            Unit = itemWash.Unit,
                            StockOut = itemWash.Stockout > 0,
                            IsDefaultSelect = false,
                            GotoProductDetail = true,
                            GroupId = groupId
                        };

                        result.Products.Add(product);
                    }
                }
            }
            else if (categoryId == 6)
            {
                List<string> onSalePid = new List<string>();
                string otherPid = "BYFW-QTFW-QTFWS-2-FU";
                if (request.ShopId > 0)
                {
                    var service = await _shopManageClient.GetShopServiceListAsync(new ShopServiceListClientRequest()
                    {
                        ShopId = request.ShopId,
                        ProductIds = new List<string>() { otherPid }
                    });
                    onSalePid = service?.Where(_ => _.IsOnline)?.Select(_ => _.Pid)?.ToList() ??
                                new List<string>();
                }
                else
                {
                    onSalePid = new List<string>() { otherPid };
                }

                if (onSalePid.Contains(otherPid))
                {
                    var productItem = _productRepository.GetProductsByProductCode(new List<string>() { otherPid })
                        .FirstOrDefault();
                    if (productItem != null)
                    {
                        groupId++;
                        string image = string.Empty;
                        if (!string.IsNullOrEmpty(productItem.Image1))
                        {
                            image = $"{_configuration["ImageDomain"]}{productItem.Image1}";
                        }

                        var product = new BaoYangPackageProductModel
                        {
                            Pid = productItem.ProductCode,
                            Brand = productItem.Brand,
                            DisplayName = productItem.DisplayName,
                            Image = image,
                            Price = productItem.SalesPrice,
                            OriginalPrice = productItem.StandardPrice > 0
                                ? productItem.StandardPrice
                                : productItem.SalesPrice,
                            Count = 1,
                            Unit = productItem.Unit,
                            StockOut = productItem.Stockout > 0,
                            IsDefaultSelect = false,
                            GotoProductDetail = true,
                            GroupId = groupId
                        };

                        result.Products.Add(product);
                    }
                }
            }
            else { //C端门店产品展示时使用

                var serviceType = await _serviceTypeEnumRepository.GetAsync<ServiceTypeEnumDo>(categoryId);//服务大类查询
                var categoryStr = serviceType?.CategoryIds ?? string.Empty;
                if (!string.IsNullOrEmpty(categoryStr))
                {
                    var categoryList = categoryStr.Split(new char[] { ',' }).ToList();

                    if (categoryList.Any())
                    {
                        var products = await _productRepository.SearchProductCommon(new SearchProductCommonCondition()
                        {
                            OnSale = 1,
                            IsShow = 1,
                            ProductAttribute = new List<int> { 2}, //服务产品
                            CategoryId = categoryList.Select<string, int>(x => Convert.ToInt32(x)).ToList(),
                            PageIndex = 1,
                            PageSize = 50
                        });
                        var normalWash = products.Items;

                        if (normalWash.Any())
                        {
                            if (request.ShopId > 0)
                            {
                                var allPid = normalWash.Select(_ => _.ProductCode).ToList();
                                var service = await _shopManageClient.GetShopServiceListAsync(new ShopServiceListClientRequest()
                                {
                                    ShopId = request.ShopId,
                                    ProductIds = allPid
                                });
                                var onSalePid = service?.Where(_ => _.IsOnline)?.Select(_ => _.Pid)?.ToList() ??
                                                new List<string>();
                                normalWash = normalWash.Where(_ => onSalePid.Contains(_.ProductCode)).ToList();
                            }

                            groupId++;
                            foreach (var itemWash in normalWash)
                            {
                                string image = string.Empty;
                                if (!string.IsNullOrEmpty(itemWash.Image1))
                                {
                                    image = $"{_configuration["ImageDomain"]}{itemWash.Image1}";
                                }

                                var product = new BaoYangPackageProductModel
                                {
                                    Pid = itemWash.ProductCode,
                                    Brand = itemWash.Brand,
                                    DisplayName = itemWash.DisplayName,
                                    Description = itemWash.Advertisement,
                                    Image = image,
                                    Price = itemWash.SalesPrice,
                                    OriginalPrice = itemWash.StandardPrice > 0 ? itemWash.StandardPrice : itemWash.SalesPrice,
                                    Count = 1,
                                    Unit = itemWash.Unit,
                                    StockOut = itemWash.Stockout > 0,
                                    IsDefaultSelect = false,
                                    GotoProductDetail = true,
                                    GroupId = groupId
                                };

                                result.Products.Add(product);
                            }
                        }
                    }                       

                }
            }

            var shopProduct = shopProductTask.Result;
            if (shopProduct != null && shopProduct.Any())
            {
                foreach (var itemProduct in shopProduct)
                {
                    groupId++;
                    string imageUrl = $"{_configuration["ImageDomain"]}{_configuration["DefaultIcon"]}";
                    if (!string.IsNullOrEmpty(itemProduct.Icon))
                    {
                        imageUrl = $"{_configuration["ImageDomain"]}{itemProduct.Icon}";
                    }

                    BaoYangPackageProductModel product = new BaoYangPackageProductModel
                    {
                        Pid = itemProduct.ProductCode,
                        DisplayName = itemProduct.DisplayName,
                        Description = itemProduct.Description,
                        Image = imageUrl,
                        Price = itemProduct.SalesPrice,
                        OriginalPrice = itemProduct.StandardPrice > 0
                            ? itemProduct.StandardPrice
                            : itemProduct.SalesPrice,
                        DiscountRate = itemProduct.DiscountRate,
                        Count = 1,
                        GotoProductDetail = true,
                        GroupId = groupId,
                        ProductType = 1
                    };
                    result.Products.Add(product);
                }
            }

            List<Core.Model.Promotion.ProductPromotionInfoVo> promotion =
                new List<Core.Model.Promotion.ProductPromotionInfoVo>();

            #region 促销

            var pidList = result.Products.Select(t => t.Pid).Distinct().ToList();
            if (pidList.Any())
            {
                promotion = await _promotionService.GetPromotionActivitByProductCodeList(
                    new PromotionActivitByProductCodeListRequest
                    {
                        ProductCodeList = pidList,
                        PromotionChannel = request.Channel
                    });
            }

            result.Products.ForEach(_ =>
            {
                var markPrice = _.OriginalPrice;
                var price = _.Price;
                var existPromotion = promotion.FirstOrDefault(t => t.ProductCode == _.Pid);
                if (existPromotion != null)
                {
                    markPrice = price;
                    price = existPromotion.PromotionPrice;
                    _.Tags.Add(new Core.Response.TagInfo
                    {
                        Tag = existPromotion.Label,
                        TagColor = "#FF0000",
                        Type = "Promotion"
                    });
                    _.ActivityId = existPromotion.ActicityId;
                }

                if (_.DiscountRate > 0 && request.EntranceType == 1)
                {
                    markPrice = price;
                    price = Ceiling(price * (1 - _.DiscountRate), 2);
                }

                _.OriginalPrice = markPrice;
                _.Price = price;
            });

            #endregion

            #region 美容团购

            if (request.ShopId > 0)
            {
                var shopGroupProducts = await _shopManageClient.GetShopGrouponProduct(new GetShopGrouponProductRequest() { ShopId = request.ShopId, Status = 1 });
                result.Products.ForEach(_ =>
                {

                    var price = _.Price;
                    var shopGroupProduct = shopGroupProducts?.Data?.FirstOrDefault(t => t.ServiceId == _.Pid);
                    if (shopGroupProduct != null)
                    {
                        _.Price = shopGroupProduct.Price;
                    }
                });
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 实现数据的向上取整
        /// </summary>
        /// <param name="v">要进行处理的数据</param>
        /// <param name="x">保留的小数位数</param>
        /// <returns>向上取整后的结果</returns>
        public decimal Ceiling(decimal v, int x)
        {
            var isNegative = false;
            //如果是负数
            if (v < 0)
            {
                isNegative = true;
                v = -v;
            }

            var IValue = 1;
            for (var i = 1; i <= x; i++)
            {
                IValue = IValue * 10;
            }

            var Int = Math.Ceiling(v * IValue);
            v = Int / IValue;

            if (isNegative)
            {
                v = -v;
            }

            return v;
        }

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductDetailPageDataResponse> GetProductDetailPageData(ProductDetailPageDataRequest request)
        {
            ProductDataVo productData = new ProductDataVo();
            ProductPromotionVo productPromotion = null;
            ShopInfoVo shopInfo = null;
            var productTask = _productRepository.GetProductByPid(request.ProductCode);
            var shopProductTask = _fctShopProductRepository.GetShopProductByPid(request.ProductCode);
            var shopTask = _shopManageClient.GetShopById(request.ShopId);
            var promotionTask = _promotionService.GetPromotionActivityByPid(request.ProductCode, request.Channel);
            await Task.WhenAll(productTask, shopProductTask, shopTask, promotionTask);
            var product = productTask.Result;
            var shopProduct = shopProductTask.Result;
            var shop = shopTask.Result;
            var promotion = promotionTask.Result;

            List<Core.Response.TagInfo> tags = new List<Core.Response.TagInfo>();
            if (promotion != null)
            {
                tags.Add(new Core.Response.TagInfo
                {
                    Tag = promotion.Label,
                    TagColor = "#FF0000",
                    Type = "Promotion"
                });

                productPromotion = new ProductPromotionVo
                {
                    AvailQuantity = promotion.AvailQuantity,
                    ActivityId = promotion.ActivityId,
                    StartTime = promotion.StartDate,
                    EndTime = promotion.EndDate,
                    PromotionPrice = promotion.PromotionPrice
                };
            }

            if (product != null)
            {
                if (product.OnSale == 0)
                {
                    throw new CustomException("该商品已下架");
                }

                #region 自营产品

                List<string> images = new List<string>();
                if (!string.IsNullOrEmpty(product.Image1))
                {
                    images.Add($"{_configuration["ImageDomain"]}{product.Image1}");
                }

                if (!string.IsNullOrEmpty(product.Image2))
                {
                    images.Add($"{_configuration["ImageDomain"]}{product.Image2}");
                }

                if (!string.IsNullOrEmpty(product.Image3))
                {
                    images.Add($"{_configuration["ImageDomain"]}{product.Image3}");
                }

                if (!string.IsNullOrEmpty(product.Image4))
                {
                    images.Add($"{_configuration["ImageDomain"]}{product.Image4}");
                }

                if (!string.IsNullOrEmpty(product.Image5))
                {
                    images.Add($"{_configuration["ImageDomain"]}{product.Image5}");
                }

                var price = product.SalesPrice;
                var markPrice = product.StandardPrice;
                string description = product.Description;

                if (promotion != null)
                {
                    markPrice = product.SalesPrice;
                    price = promotion.PromotionPrice;
                    description = promotion.Description;
                }

                productData.ProductCode = product.ProductCode;
                productData.Name = product.Name;
                productData.DisplayName = product.DisplayName;
                productData.Brand = product.Brand;
                productData.StandardPrice = markPrice;
                productData.SalesPrice = price;
                productData.Unit = product.Unit;
                productData.StockOut = product.Stockout;
                productData.IsRetail = product.IsRetail;
                productData.Images = images;
                productData.Description = description;
                productData.Tags = tags;

                #endregion
            }
            else if (shopProduct != null)
            {
                if (shopProduct.OnSale == 0)
                {
                    throw new CustomException("该商品已下架");
                }

                #region 门店项目

                List<string> images = new List<string>();
                if (!string.IsNullOrEmpty(shopProduct.Icon))
                {
                    images.Add($"{_configuration["ImageDomain"]}{shopProduct.Icon}");
                }
                else
                {
                    images.Add($"{_configuration["ImageDomain"]}{_configuration["DefaultIcon"]}");
                }
                var price = shopProduct.SalesPrice;
                var markPrice = shopProduct.StandardPrice;
                if (promotion != null)
                {
                    markPrice = shopProduct.SalesPrice;
                    price = promotion.PromotionPrice;
                    productData.Description = promotion.Description;
                }
                else
                {
                    productData.Detail = shopProduct.Detail;
                    productData.DetailImages =
                        shopProduct.ImageUrl?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            ?.Select(t => $"{_configuration["ImageDomain"]}{t}")?.ToList() ?? new List<string>();
                }

                productData.ProductCode = shopProduct.ProductCode;
                productData.Name = shopProduct.ProductName;
                productData.DisplayName = shopProduct.DisplayName;
                productData.SalesPrice = price;
                productData.StandardPrice = markPrice;
                productData.IsRetail = 1;
                productData.Images = images;
                productData.Tags = tags;

                #endregion
            }
            else
            {
                throw new CustomException("该商品不存在");
            }

            if (shop != null)
            {
                shopInfo = new ShopInfoVo
                {
                    SimpleName = shop.SimpleName,
                    Address = $"{shop.Province}{shop.City}{shop.District}{shop.Address}"
                };
            }

            return new ProductDetailPageDataResponse
            {
                ProductData = productData,
                PromotionInfo = productPromotion,
                ShopInfo = shopInfo
            };
        }

        /// <summary>
        /// 获取上门服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductBaseInfoVo> GetShangMenService(ShangMenServiceRequest request)
        {
            List<int> baoYangCategoryId = new List<int>()
            {
                163, 164
            };
            string pid = "BYFW-SMFW-SSMFW-2-FU";
            var products = await _productRepository.GetProductByPidList(request.PidList);
            var existBaoYang =
                products.Exists(_ => baoYangCategoryId.Contains(_.CategoryId) && _.ProductAttribute == 1);
            if (!existBaoYang)
            {
                pid = "BYFW-SMFW-SSMFW-4-FU";
            }

            var product = await _productRepository.GetProductByPid(pid);
            if (product != null)
            {
                return _mapper.Map<ProductBaseInfoVo>(product);
            }

            return null;
        }

        /// <summary>
        /// 判断是否上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CheckDoorProductVo>> CheckDoorProduct(CheckDoorProductRequest request)
        {
            List<CheckDoorProductVo> result = new List<CheckDoorProductVo>();
            var pidList = request.PidList;
            if (pidList != null && pidList.Any())
            {
                List<string> existProduct = (await _doorProductRepository.GetDoorProductByPidList(pidList))
                    .Select(_ => _.Pid).ToList();

                pidList.ForEach(_ =>
                {
                    result.Add(new CheckDoorProductVo()
                    {
                        Pid = _,
                        IsDoorProduct = existProduct.Contains(_)
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// 获取上门服务费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductBaseInfoVo> GetDoorService(DoorServiceRequest request)
        {
            string pid = "BYFW-SMFW-SSMFW-2-FU";

            var product = await _productRepository.GetProductByPid(pid);

            if (product != null)
            {
                var doorProduct = await _doorProductRepository.GetDoorProductByPidList(request.PidList);
                if (doorProduct.Exists(_ => _.FreeDoorFee == 1))
                {
                    product.SalesPrice = 0;
                }

                return _mapper.Map<ProductBaseInfoVo>(product);
            }

            return null;
        }

        /// <summary>
        /// 获取批发商品列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ProductBaseInfoVo>> GetWholesaleProducts(WholesaleProductsRequest request)
        {
            var result = await _productRepository.GetWholesaleProducts(request.PageIndex, request.PageSize);

            return new ApiPagedResultData<ProductBaseInfoVo>()
            {
                TotalItems = result.TotalItems,
                Items = _mapper.Map<List<ProductBaseInfoVo>>(result.Items)
            };
        }

        /// <summary>
        /// 前台类目搜索产品
        /// 子产品-上架-实物产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pid"></param>
        /// <param name="brand"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ProductBaseInfoVo>> GetProductByFrontCategoryConfig(
            List<string> categoryId, List<string> pid, List<string> brand, int pageIndex, int pageSize)
        {
            ApiPagedResultData<ProductBaseInfoVo> result = new ApiPagedResultData<ProductBaseInfoVo>()
            {
                Items = new List<ProductBaseInfoVo>()
            };
            List<int> childCategory = new List<int>();
            if (categoryId != null && categoryId.Any())
            {
                List<DimCategoryDO> category = await GetAllCategoryCache(0);

                categoryId.ForEach(_ => { childCategory.AddRange(GetChildCategory(category, _)); });
            }

            var products =
                await _productRepository.GetProductByFrontCategoryConfig(childCategory, pid, brand, pageIndex,
                    pageSize);

            if (products != null)
            {
                result.TotalItems = products.TotalItems;
                result.Items = _mapper.Map<List<ProductBaseInfoVo>>(products.Items);
            }

            return result;
        }

        private List<int> GetChildCategory(List<DimCategoryDO> category, string categoryId)
        {
            List<int> result = new List<int>();
            int.TryParse(categoryId, out int categoryIdr);
            var current = category.FirstOrDefault(_ => _.Id == categoryIdr);
            if (current != null)
            {
                if (current.CategoryLevel == 3)
                {
                    result.Add(categoryIdr);
                }
                else if (current.CategoryLevel == 2)
                {
                    result = category.Where(_ => _.ParentId == categoryIdr).Select(_ => _.Id).ToList();
                }
                else if (current.CategoryLevel == 1)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == categoryIdr).Select(_ => _.Id).ToList();
                    result = category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            return result;
        }

        #region 上门产品管理

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<DoorProductVo>> GetDoorProductPageList(DoorProductPageListRequest request)
        {
            ApiPagedResultData<DoorProductVo> response = new ApiPagedResultData<DoorProductVo>()
            {
                Items = new List<DoorProductVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<DimCategoryDO> category = await GetAllCategoryCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var result = await _doorProductRepository.GetDoorProductPageList(new DoorProductPageListCondition()
            {
                Key = request.Key,
                Brand = request.Brand,
                CategoryId = categoryId,
                IsDoorProduct = request.IsDoorProduct,
                FreeDoorFee = request.FreeDoorFee,
                OnSale = request.OnSale,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<DoorProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditDoorProduct(EditDoorProductRequest request)
        {
            DoorProductDO doorProductDo = new DoorProductDO()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            List<string> field = new List<string>();

            if (request.FreeDoorFee != null)
            {
                doorProductDo.FreeDoorFee = request.FreeDoorFee.Value;
                field.Add("FreeDoorFee");
            }

            if (request.IsDeleted != null)
            {
                doorProductDo.IsDeleted = request.IsDeleted.Value;
                field.Add("IsDeleted");
            }

            if (field.Any())
            {
                field.Add("UpdateBy");
                field.Add("UpdateTime");
                await _doorProductRepository.UpdateAsync(doorProductDo, field);
            }

            return true;
        }

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddDoorProducts(AddDoorProductsRequest request)
        {
            if (request.DoorProducts != null && request.DoorProducts.Any())
            {
                List<string> pidList = request.DoorProducts.Select(_ => _.Pid).ToList();
                List<string> existProduct = (await _doorProductRepository.GetDoorProductByPidList(pidList))
                    .Select(_ => _.Pid).ToList();
                List<DoorProductDO> doorProducts = request.DoorProducts.Where(_ => !existProduct.Contains(_.Pid))
                    .Select(_ => new DoorProductDO
                    {
                        Pid = _.Pid,
                        CategoryId = _.CategoryId,
                        FreeDoorFee = _.FreeDoorFee,
                        CreateBy = request.SubmitBy,
                        CreateTime = DateTime.Now
                    }).ToList();
                await _doorProductRepository.InsertBatchAsync(doorProducts);
            }

            return true;
        }

        #endregion

        #region 安装服务管理

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        public async Task<ApiPagedResultData<ProductInstallServiceVo>> GetProductInstallService(
            ProductInstallServiceRequest request)
        {
            ApiPagedResultData<ProductInstallServiceVo> result = new ApiPagedResultData<ProductInstallServiceVo>()
            {
                Items = new List<ProductInstallServiceVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<DimCategoryDO> category = await GetAllCategoryCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var pageProducts = await _productInstallRepository.GetProductInstallServicePageList(
                new ProductInstallServicePageListCondition()
                {
                    Key = request.Key,
                    Brand = request.Brand,
                    CategoryId = categoryId,
                    HasInstallService = request.HasInstallService,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (pageProducts != null)
            {
                result.TotalItems = pageProducts.TotalItems;
                if (pageProducts.Items != null && pageProducts.Items.Any())
                {
                    var pidList = pageProducts.Items.Select(_ => _.Pid).ToList();
                    var installService = await _productInstallRepository.GetRelInstallServiceByPidList(pidList);
                    foreach (var itemS in pageProducts.Items)
                    {
                        result.Items.Add(new ProductInstallServiceVo()
                        {
                            ProductId = itemS.ProductId,
                            Pid = itemS.Pid,
                            Name = itemS.Name,
                            Brand = itemS.Brand,
                            Price = itemS.Price,
                            OnSale = itemS.OnSale,
                            InstallService = installService.Where(_ => _.Pid == itemS.Pid).Select(_ =>
                                new InstallServiceVo
                                {
                                    Id = _.Id,
                                    ServiceId = _.ServiceId,
                                    ServiceName = _.ServiceName,
                                    FreeInstall = _.FreeInstall,
                                    ChangeNum = _.ChangeNum
                                }).ToList()
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductInstallServiceVo> GetProductInstallServiceDetail(
            ProductInstallServiceDetailRequest request)
        {
            List<string> pidList = new List<string>()
            {
                request.Pid
            };
            var installService =
                await _productInstallRepository.GetRelInstallServiceByPidList(new List<string>() { request.Pid });
            if (installService.Any())
            {
                pidList.AddRange(installService.Select(_ => _.ServiceId));
            }

            var products = await _productRepository.GetProductByPidList(pidList);

            var mainProduct = products.FirstOrDefault(_ => _.ProductCode == request.Pid);
            var serviceProducts = products.Where(_ => _.ProductCode != request.Pid).ToList();
            if (mainProduct != null)
            {
                List<InstallServiceVo> service = new List<InstallServiceVo>();
                installService.ForEach(_ =>
                {
                    var itemProduct = serviceProducts.FirstOrDefault(x => x.ProductCode == _.ServiceId);
                    service.Add(new InstallServiceVo
                    {
                        Id = _.Id,
                        ServiceId = _.ServiceId,
                        ServiceName = itemProduct?.Name ?? _.ServiceName,
                        Price = itemProduct?.SalesPrice ?? _.ServicePrice,
                        FreeInstall = _.FreeInstall,
                        ChangeNum = _.ChangeNum
                    });
                });

                return new ProductInstallServiceVo()
                {
                    ProductId = mainProduct.Id,
                    Pid = mainProduct.ProductCode,
                    Name = mainProduct.Name,
                    Brand = mainProduct.Brand,
                    Price = mainProduct.SalesPrice,
                    OnSale = mainProduct.OnSale,
                    InstallService = service
                };
            }

            return null;
        }

        /// <summary>
        /// 添加安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddInstallService(AddInstallServiceRequest request)
        {
            if (request.Products == null || !request.Products.Any())
            {
                return false;
            }

            if (request.InstallService == null)
            {
                return false;
            }

            var pidList = request.Products.Select(_ => _.Pid).ToList();

            List<RelProductInstallserviceDO> add = new List<RelProductInstallserviceDO>();
            List<RelProductInstallserviceDO> update = new List<RelProductInstallserviceDO>();

            var installService =
                await _productInstallRepository.GetRelInstallServiceByPidList(pidList, request.InstallService.ServiceId,
                    false);
            request.Products.ForEach(_ =>
            {
                var exist = installService.Where(t => t.Pid == _.Pid).ToList();
                if (exist.Any())
                {
                    update.AddRange(exist.Select(x => new RelProductInstallserviceDO
                    {
                        Id = x.Id,
                        ServiceName = request.InstallService.ServiceName,
                        ServicePrice = request.InstallService.Price,
                        FreeInstall = request.InstallService.FreeInstall,
                        ChangeNum = request.InstallService.ChangeNum,
                        UpdateBy = request.SubmitBy,
                        UpdateTime = DateTime.Now
                    }));
                }
                else
                {
                    add.Add(new RelProductInstallserviceDO()
                    {
                        ProductId = _.ProductId.ToString(),
                        Pid = _.Pid,
                        ServiceId = request.InstallService.ServiceId,
                        ServiceName = request.InstallService.ServiceName,
                        ServicePrice = request.InstallService.Price,
                        FreeInstall = request.InstallService.FreeInstall,
                        ChangeNum = request.InstallService.ChangeNum,
                        CreateBy = request.SubmitBy,
                        CreateTime = DateTime.Now
                    });
                }
            });

            using (TransactionScope ts = new TransactionScope())
            {
                if (add.Any())
                {
                    await _productInstallRepository.InsertBatchAsync(add);
                }

                if (update.Any())
                {
                    foreach (var itemU in update)
                    {
                        await _productInstallRepository.UpdateAsync(itemU, new List<string>()
                        {
                            "ServiceName", "ServicePrice", "FreeInstall", "ChangeNum", "UpdateBy", "UpdateTime"
                        });
                    }
                }

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallService(EditInstallServiceRequest request)
        {
            if (request.Products == null || !request.Products.Any())
            {
                return false;
            }

            if (request.InstallService == null)
            {
                return false;
            }

            var pidList = request.Products.Select(_ => _.Pid).ToList();
            var serviceId = request.InstallService.Select(_ => _.ServiceId).ToList();

            List<RelProductInstallserviceDO> add = new List<RelProductInstallserviceDO>();
            List<RelProductInstallserviceDO> update = new List<RelProductInstallserviceDO>();
            List<RelProductInstallserviceDO> delete = new List<RelProductInstallserviceDO>();

            var installService =
                await _productInstallRepository.GetRelInstallServiceByPidList(pidList, false);
            request.Products.ForEach(_ =>
            {
                var defaultInstall = installService.Where(x => x.Pid == _.Pid).ToList();
                request.InstallService.ForEach(t =>
                {
                    var exist = defaultInstall.Where(x => x.ServiceId == t.ServiceId).ToList();
                    if (exist.Any())
                    {
                        update.AddRange(exist.Select(x => new RelProductInstallserviceDO
                        {
                            Id = x.Id,
                            ServiceName = t.ServiceName,
                            ServicePrice = t.Price,
                            FreeInstall = t.FreeInstall,
                            ChangeNum = t.ChangeNum,
                            UpdateBy = request.SubmitBy,
                            UpdateTime = DateTime.Now
                        }));
                    }
                    else
                    {
                        add.Add(new RelProductInstallserviceDO()
                        {
                            ProductId = _.ProductId.ToString(),
                            Pid = _.Pid,
                            ServiceId = t.ServiceId,
                            ServiceName = t.ServiceName,
                            ServicePrice = t.Price,
                            FreeInstall = t.FreeInstall,
                            ChangeNum = t.ChangeNum,
                            CreateBy = request.SubmitBy,
                            CreateTime = DateTime.Now
                        });
                    }
                });

                delete.AddRange(defaultInstall.Where(x => !serviceId.Contains(x.ServiceId)).Select(x =>
                    new RelProductInstallserviceDO()
                    {
                        Id = x.Id,
                        IsDeleted = 1,
                        UpdateBy = request.SubmitBy,
                        UpdateTime = DateTime.Now
                    }));
            });

            using (TransactionScope ts = new TransactionScope())
            {
                if (add.Any())
                {
                    await _productInstallRepository.InsertBatchAsync(add);
                }

                if (update.Any())
                {
                    foreach (var itemU in update)
                    {
                        await _productInstallRepository.UpdateAsync(itemU, new List<string>()
                        {
                            "ServiceName", "ServicePrice", "FreeInstall", "ChangeNum", "UpdateBy", "UpdateTime"
                        });
                    }
                }

                if (delete.Any())
                {
                    foreach (var itemU in delete)
                    {
                        await _productInstallRepository.UpdateAsync(itemU, new List<string>()
                        {
                            "IsDeleted", "UpdateBy", "UpdateTime"
                        });
                    }
                }

                ts.Complete();
            }

            return true;
        }

        #endregion

        #region 安装服务 对外api

        /// <summary>
        /// 获取产品对应安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<InstallServiceByProductResponse> GetInstallServiceByProduct(
            InstallServiceByProductRequest request)
        {
            List<ProductRelateServiceVo> item1 = new List<ProductRelateServiceVo>();
            List<InstallServiceDetailVo> item2 = new List<InstallServiceDetailVo>();
            var products = request.Products;
            var pidList = products.Select(_ => _.Pid).ToList();
            var installService = await _productInstallRepository.GetRelInstallServiceByPidList(pidList);
            products.ForEach(_ =>
            {
                var itemService = installService.Where(x => x.Pid == _.Pid).ToList();
                if (string.IsNullOrEmpty(_.InstallType))
                {
                    itemService = itemService.Where(x => x.IsDefault == 1).ToList();
                }
                else
                {
                    itemService = itemService.Where(x => x.Type == _.InstallType).ToList();
                }

                item1.Add(new ProductRelateServiceVo()
                {
                    ProductId = _.Pid,
                    ServiceId = itemService.Select(x => x.ServiceId).ToList()
                });

                foreach (var itemS in itemService)
                {
                    int count = 1;
                    var defaultExist = item2.FirstOrDefault(x =>
                        x.ServiceId == itemS.ServiceId && x.FreeInstall == itemS.FreeInstall &&
                        x.ChangeNum == itemS.ChangeNum);
                    if (defaultExist == null)
                    {
                        if (itemS.ChangeNum == 1)
                        {
                            count = _.Num;
                        }

                        item2.Add(new InstallServiceDetailVo()
                        {
                            ServiceId = itemS.ServiceId,
                            ChangeNum = itemS.ChangeNum,
                            FreeInstall = itemS.FreeInstall,
                            Count = count
                        });
                    }
                    else
                    {
                        if (itemS.ChangeNum == 1)
                        {
                            item2.ForEach(x =>
                            {
                                if (x.ServiceId == itemS.ServiceId && x.FreeInstall == itemS.FreeInstall &&
                                    x.ChangeNum == itemS.ChangeNum)
                                {
                                    x.Count += _.Num;
                                }
                            });
                        }
                    }
                }
            });

            if (item2.Any())
            {
                item2 = item2.GroupBy(_ => new { _.ServiceId, _.FreeInstall }).Select(_ => new InstallServiceDetailVo
                {
                    ServiceId = _.Key.ServiceId,
                    FreeInstall = _.Key.FreeInstall,
                    Count = _.Sum(t => t.Count)

                }).ToList();

                var serviceIdList = item2.Select(_ => _.ServiceId).Distinct().ToList();

                var services = await _productRepository.GetProductByPidList(serviceIdList);

                item2.ForEach(_ =>
                {
                    var defaultS = services.FirstOrDefault(t => t.ProductCode == _.ServiceId);

                    _.ServiceName = defaultS?.Name ?? string.Empty;
                    _.Price = _.FreeInstall == 1 ? 0 : (defaultS?.SalesPrice ?? 0);
                    _.MarketPrice = defaultS?.StandardPrice ?? 0;
                    _.Description = defaultS?.Remark ?? string.Empty;
                    _.ImageUrl = defaultS?.Image1.AddImageDomain();
                });
            }

            return new InstallServiceByProductResponse()
            {
                ProductInstallService = item1,
                InstallService = item2
            };
        }

        #endregion

        #region 替换套餐子产品

        /// <summary>
        /// 搜索替换商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchReplaceProduct(
            SearchReplaceProductRequest request)
        {
            ApiPagedResultData<BaoYangPackageProductModel> result = new ApiPagedResultData<BaoYangPackageProductModel>()
            {
                Items = new List<BaoYangPackageProductModel>()
            };
            var packageDetail = await _productPackageRepository.GetAsync(request.PackageDetailId);
            if (packageDetail != null)
            {
                List<int> categoryId = packageDetail.CategoryId
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => Convert.ToInt32(_))
                    .ToList();
                List<int> shopCategoryId = packageDetail.ShopCategoryId
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => Convert.ToInt32(_))
                    .ToList();
                switch (request.Type)
                {
                    case 1: //自营产品
                        var product = await _productRepository.SearchProductCommon(new SearchProductCommonCondition()
                        {
                            ProductName = request.Content,
                            OnSale = 1,
                            CategoryId = categoryId,
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize
                        });
                        result.TotalItems = product.TotalItems;
                        result.Items = product.Items.Select(_ => new BaoYangPackageProductModel
                        {
                            Pid = _.ProductCode,
                            Brand = _.Brand,
                            DisplayName = _.DisplayName,
                            Image = _.Image1.AddImageDomain(),
                            Price = _.SalesPrice,
                            OriginalPrice = _.StandardPrice > 0 ? _.StandardPrice : _.SalesPrice,
                            Unit = _.Unit,
                            StockOut = _.Stockout > 0,
                            IsPackageProduct = _.ProductAttribute == 1,
                            ReplaceProduct = true,
                            PackageDetailId = request.PackageDetailId
                        }).ToList();
                        break;
                    case 2: //外采产品
                        var shopProduct = await _fctShopProductRepository.SearchShopProduct(
                            new SearchShopProductCondition()
                            {
                                Key = request.Content,
                                ShopId = request.ShopId,
                                IsOnSale = 1,
                                IsStoreoutside = 1,
                                CategoryId = shopCategoryId,
                                PageIndex = request.PageIndex,
                                PageSize = request.PageSize
                            });
                        result.TotalItems = shopProduct.TotalItems;
                        result.Items = shopProduct.Items.Select(_ => new BaoYangPackageProductModel
                        {
                            Pid = _.ProductCode,
                            DisplayName = _.DisplayName,
                            Image = _.Icon.AddImageDomain(),
                            Price = _.SalesPrice,
                            OriginalPrice = _.StandardPrice > 0 ? _.StandardPrice : _.SalesPrice,
                            Unit = _.Unit,
                            ProductType = 2,
                            ReplaceProduct = true,
                            PackageDetailId = request.PackageDetailId
                        }).ToList();
                        break;
                }
            }

            return result;
        }

        #endregion

        #region 前台类目配置

        /// <summary>
        /// 获取前台类目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<FrontCategoryVo>> GetFrontCategoryPageList(
            FrontCategoryPageListRequest request)
        {
            ApiPagedResultData<FrontCategoryVo> result = new ApiPagedResultData<FrontCategoryVo>()
            {
                Items = new List<FrontCategoryVo>()
            };
            var frontCategory = (await _configFrontCategoryRepository.GetListAsync("")).ToList();
            var parentCategory = frontCategory.Where(t => t.ParentId == 0).ToList();
            if (request.TerminalType != null)
            {
                parentCategory = parentCategory.Where(t => t.TerminalType == request.TerminalType.ToString()).ToList();
            }

            result.TotalItems = parentCategory.Count;
            parentCategory = parentCategory.OrderBy(_ => _.Rank).ToList();

            parentCategory.ForEach(_ =>
            {
                FrontCategoryVo itemC = new FrontCategoryVo()
                {
                    Id = _.Id,
                    Category = _.Category,
                    DisplayName = _.DisplayName,
                    ImageUrl = _.ImageUrl.AddImageDomain(),
                    RouteUrl = _.RouteUrl,
                    SubTitle = _.SubTitle,
                    TerminalType = (TerminalType)Enum.Parse(typeof(TerminalType), _.TerminalType),
                    Rank = _.Rank,
                    Version = _.Version,
                    ExtendParam = _.ExtendParam
                };
                itemC.Children = ExpandChild(itemC, frontCategory);
                result.Items.Add(itemC);
            });
            return result;
        }

        /// <summary>
        /// 前台分类详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FrontCategoryDetailVo> GetFrontCategoryDetail(FrontCategoryDetailRequest request)
        {
            FrontCategoryDetailVo response = null;
            var frontCategory = (await _configFrontCategoryRepository.GetListAsync("")).ToList();
            var categoryProduct =
                await _configFrontCategoryProductRepository.GetConfigFrontCategoryProductByCategoryId(
                    request.CategoryId) ?? new List<ConfigFrontCategoryProductDo>();
            var result = frontCategory.FirstOrDefault(_ => _.Id == request.CategoryId);
            if (result != null)
            {
                List<long> parentNode = new List<long>();
                GetParentNode(frontCategory, result, ref parentNode);
                response = new FrontCategoryDetailVo()
                {
                    Id = result.Id,
                    Category = result.Category,
                    DisplayName = result.DisplayName,
                    ImageUrl = result.ImageUrl.AddImageDomain(),
                    RouteUrl = result.RouteUrl,
                    SubTitle = result.SubTitle,
                    TerminalType = (TerminalType)Enum.Parse(typeof(TerminalType), result.TerminalType),
                    Rank = result.Rank,
                    Version = result.Version,
                    ExtendParam = result.ExtendParam,
                    ParentId = result.ParentId,
                    ParentNode = parentNode
                };

                List<string> category = categoryProduct.Where(_ => _.ProductType == "category").Select(_ => _.ProductId)
                    .ToList();
                List<string> pid = categoryProduct.Where(_ => _.ProductType == "product").Select(_ => _.ProductId)
                    .ToList();
                List<string> brand = categoryProduct.Where(_ => _.ProductType == "brand").Select(_ => _.ProductId)
                    .ToList();
                response.PidList = pid;
                response.BrandList = brand;
                if (category.Any())
                {
                    var allCategory = await GetAllCategoryCache(0);
                    foreach (var _ in category)
                    {
                        int thirdId = Convert.ToInt32(_);
                        List<int> itemC = new List<int>();
                        var third = allCategory.FirstOrDefault(x => x.Id == thirdId);
                        if (third != null)
                        {
                            itemC.Insert(0, third.Id);
                            var second = allCategory.FirstOrDefault(t => t.Id == third.ParentId);
                            if (second != null)
                            {
                                itemC.Insert(0, second.Id);
                                var first = allCategory.First(t => t.Id == second.ParentId);
                                if (first != null)
                                {
                                    itemC.Insert(0, first.Id);
                                }
                            }
                        }

                        response.CategoryIdList.Add(itemC);
                    }
                }
            }

            return response;
        }

        private void GetParentNode(List<ConfigFrontCategoryDo> allCategory, ConfigFrontCategoryDo current,
            ref List<long> parentNode)
        {
            var defaultItem = allCategory.FirstOrDefault(_ => _.Id == current.ParentId);
            if (defaultItem != null)
            {
                parentNode.Insert(0, defaultItem.Id);
                GetParentNode(allCategory, defaultItem, ref parentNode);
            }
        }

        /// <summary>
        /// 递归子集
        /// </summary>
        /// <param name="frontCategory"></param>
        /// <param name="categoryList"></param>
        /// <returns></returns>
        private List<FrontCategoryVo> ExpandChild(FrontCategoryVo frontCategory,
            List<ConfigFrontCategoryDo> categoryList)
        {
            var list = categoryList.Where(t => t.ParentId == frontCategory.Id).OrderBy(_ => _.Rank).ToList();
            if (!list.Any())
            {
                return null;
            }

            List<FrontCategoryVo> childList = new List<FrontCategoryVo>();

            foreach (var _ in list)
            {
                FrontCategoryVo itemC = new FrontCategoryVo()
                {
                    Id = _.Id,
                    Category = _.Category,
                    DisplayName = _.DisplayName,
                    ImageUrl = _.ImageUrl.AddImageDomain(),
                    RouteUrl = _.RouteUrl,
                    SubTitle = _.SubTitle,
                    TerminalType = (TerminalType)Enum.Parse(typeof(TerminalType), _.TerminalType),
                    Rank = _.Rank,
                    Version = _.Version,
                    ExtendParam = _.ExtendParam
                };

                itemC.Children = ExpandChild(itemC, categoryList);

                childList.Add(itemC);
            }

            return childList;
        }

        /// <summary>
        /// 新增前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddFrontCategory(AddFrontCategoryRequest request)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                long categoryId = await _configFrontCategoryRepository.InsertAsync<long>(new ConfigFrontCategoryDo()
                {
                    Category = request.Category,
                    DisplayName = request.DisplayName,
                    ImageUrl = request.ImageUrl,
                    RouteUrl = request.RouteUrl,
                    SubTitle = request.SubTitle,
                    ParentId = request.ParentId,
                    TerminalType = request.TerminalType.ToString(),
                    Rank = request.Rank,
                    Version = request.Version,
                    ExtendParam = request.ExtendParam,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                });
                if (categoryId > 0)
                {
                    if (request.Product != null && request.Product.Any())
                    {
                        List<ConfigFrontCategoryProductDo> products = request.Product.Select(_ =>
                            new ConfigFrontCategoryProductDo
                            {
                                CategoryId = categoryId,
                                ProductType = _.ProductType,
                                ProductId = _.ProductId,
                                CreateBy = request.SubmitBy,
                                CreateTime = DateTime.Now
                            }).ToList();
                        await _configFrontCategoryProductRepository.InsertBatchAsync(products);
                    }
                }

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 编辑前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditFrontCategory(EditFrontCategoryRequest request)
        {
            ConfigFrontCategoryDo update = new ConfigFrontCategoryDo()
            {
                Id = request.CategoryId,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };

            List<string> field = new List<string>();

            if (request.Category != null)
            {
                update.Category = request.Category;
                field.Add("Category");
            }

            if (request.DisplayName != null)
            {
                update.DisplayName = request.DisplayName;
                field.Add("DisplayName");
            }

            if (request.ImageUrl != null)
            {
                update.ImageUrl = request.ImageUrl;
                field.Add("ImageUrl");
            }

            if (request.RouteUrl != null)
            {
                update.RouteUrl = request.RouteUrl;
                field.Add("RouteUrl");
            }

            if (request.SubTitle != null)
            {
                update.SubTitle = request.SubTitle;
                field.Add("SubTitle");
            }

            if (request.ParentId.HasValue)
            {
                update.ParentId = request.ParentId.Value;
                field.Add("ParentId");
            }

            if (request.Rank.HasValue)
            {
                update.Rank = request.Rank.Value;
                field.Add("Rank");
            }

            if (request.ExtendParam != null)
            {
                update.ExtendParam = request.ExtendParam;
                field.Add("ExtendParam");
            }

            if (request.IsDeleted.HasValue)
            {
                update.IsDeleted = request.IsDeleted.Value;
                field.Add("IsDeleted");
            }

            if (field.Any())
            {
                field.Add("UpdateBy");
                field.Add("UpdateTime");

                await _configFrontCategoryRepository.UpdateAsync(update, field);
            }

            if (request.Product != null)
            {
                var categoryProduct =
                    await _configFrontCategoryProductRepository.GetConfigFrontCategoryProductByCategoryId(
                        request.CategoryId);

                List<ConfigFrontCategoryProductDo> add = request.Product
                    .Where(_ => !categoryProduct.Exists(t =>
                        t.ProductId == _.ProductId && t.ProductType == _.ProductType)).Select(_ =>
                        new ConfigFrontCategoryProductDo
                        {
                            CategoryId = request.CategoryId,
                            ProductType = _.ProductType,
                            ProductId = _.ProductId,
                            UpdateBy = request.SubmitBy,
                            UpdateTime = DateTime.Now
                        }).ToList();

                List<ConfigFrontCategoryProductDo> delete = categoryProduct
                    .Where(_ => !request.Product.Exists(t =>
                        t.ProductType == _.ProductType && t.ProductId == _.ProductId)).Select(_ =>
                        new ConfigFrontCategoryProductDo
                        {
                            Id = _.Id,
                            IsDeleted = 1,
                            UpdateBy = request.SubmitBy,
                            UpdateTime = DateTime.Now
                        }).ToList();

                if (add.Any())
                {
                    await _configFrontCategoryProductRepository.InsertBatchAsync(add);
                }

                foreach (var itemD in delete)
                {
                    await _configFrontCategoryProductRepository.UpdateAsync(itemD,
                        new List<string>() { "IsDeleted", "UpdateBy", "UpdateTime" });
                }
            }

            return true;
        }

        #endregion

        #region 产品搜索

        /// <summary>
        /// 搜索商品通用：平台产品or门店外采
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchShopProductCommon(
            SearchShopProductCommon request)
        {
            ApiPagedResultData<BaoYangPackageProductModel> result = new ApiPagedResultData<BaoYangPackageProductModel>()
            {
                Items = new List<BaoYangPackageProductModel>()
            };

            List<int> categoryId = await GetChildCategoryId(request.MainCategoryId, request.SecondCategoryId,
                request.ChildCategoryId);

            switch (request.ProductType)
            {
                case 1: //直营
                    var product = await _productRepository.SearchProductCommon(new SearchProductCommonCondition()
                    {
                        ProductName = request.Content,
                        OnSale = 1,
                        ProductAttribute = request.ProductAttributes,
                        CategoryId = categoryId,
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize
                    });
                    if (product != null)
                    {
                        result.TotalItems = product.TotalItems;
                        var itemP = product.Items?.ToList() ?? new List<FctProductDO>();
                        if (itemP.Any())
                        {
                            List<ProductPackageVo> packageResult = new List<ProductPackageVo>();
                            List<ProductAllInfoVo> childProduct = new List<ProductAllInfoVo>();
                            List<BaoYangTypeConfigDto> baoYangType = new List<BaoYangTypeConfigDto>();
                            var packageProduct =
                                itemP.Where(_ => _.ProductAttribute == 1).Select(_ => _.ProductCode).ToList();
                            if (packageProduct.Any())
                            {
                                packageResult = GetPackageProductsByCode(packageProduct);
                                var childPid = packageResult.SelectMany(_ => _.Details).Where(_ => _.ProjectType == 1)
                                    .Select(_ => _.ProjectId).ToList();
                                childProduct = _productRepository.GetProductsByProductCode(childPid);
                                baoYangType = await _baoYangClient.GetBaoYangTypeConfig();
                            }

                            itemP.ForEach(_ =>
                            {
                                BaoYangPackageProductModel item = new BaoYangPackageProductModel()
                                {
                                    Pid = _.ProductCode,
                                    Brand = _.Brand,
                                    DisplayName = _.DisplayName,
                                    Image = _.Image1.AddImageDomain(),
                                    Price = _.SalesPrice,
                                    SettlementPrice = _.SettlementPrice,
                                    OriginalPrice = _.StandardPrice > 0 ? _.StandardPrice : _.SalesPrice,
                                    Unit = _.Unit,
                                    StockOut = _.Stockout > 0,
                                    IsPackageProduct = _.ProductAttribute == 1
                                };

                                List<BaoYangPackageProductModel> childItem = new List<BaoYangPackageProductModel>();

                                if (_.ProductAttribute == 1)
                                {
                                    var defaultChild =
                                        packageResult.FirstOrDefault(x => x.PackageInfo.ProductCode == _.ProductCode)
                                            ?.Details;
                                    if (defaultChild != null && defaultChild.Any())
                                    {
                                        defaultChild.ForEach(t =>
                                        {
                                            if (t.ProjectType == 1)
                                            {
                                                var defaultChildItem =
                                                    childProduct.FirstOrDefault(f => f.ProductCode == t.ProjectId);
                                                if (defaultChildItem != null)
                                                {
                                                    childItem.Add(new BaoYangPackageProductModel()
                                                    {
                                                        Pid = defaultChildItem.ProductCode,
                                                        Brand = defaultChildItem.Brand,
                                                        DisplayName = defaultChildItem.DisplayName,
                                                        Image = defaultChildItem.Image1.AddImageDomain(),
                                                        Price = defaultChildItem.SalesPrice,
                                                        SettlementPrice = defaultChildItem.SettlementPrice,
                                                        OriginalPrice = defaultChildItem.StandardPrice > 0
                                                            ? defaultChildItem.StandardPrice
                                                            : defaultChildItem.SalesPrice,
                                                        Unit = defaultChildItem.Unit,
                                                        StockOut = defaultChildItem.Stockout > 0,
                                                        ReplaceProduct = t.ReplaceProduct == 1,
                                                        PackageDetailId = t.Id
                                                    });
                                                }
                                            }
                                            else if (t.ProjectType == 2)
                                            {
                                                childItem.Add(new BaoYangPackageProductModel()
                                                {
                                                    Pid = "",
                                                    Brand = t.ProjectBrands?.FirstOrDefault() ?? string.Empty,
                                                    DisplayName = t.ProjectName,
                                                    Image = baoYangType.FirstOrDefault(
                                                            f => f.BaoYangType == t.ProjectId)
                                                        ?.ImageUrl ?? string.Empty,
                                                    ReplaceProduct = t.ReplaceProduct == 1,
                                                    PackageDetailId = t.Id
                                                });
                                            }
                                        });
                                    }
                                }

                                item.ChildProducts = childItem;

                                result.Items.Add(item);
                            });
                        }
                    }

                    break;
                case 2: //外采
                    var shopProduct = await _fctShopProductRepository.SearchShopProduct(
                        new SearchShopProductCondition()
                        {
                            Key = request.Content,
                            ShopId = request.ShopId,
                            IsOnSale = 1,
                            IsStoreoutside = 1,
                            CategoryId = categoryId,
                            PageIndex = request.PageIndex,
                            PageSize = request.PageSize
                        });
                    result.TotalItems = shopProduct.TotalItems;
                    result.Items = shopProduct.Items.Select(_ => new BaoYangPackageProductModel
                    {
                        Pid = _.ProductCode,
                        DisplayName = _.DisplayName,
                        Image = _.Icon.AddImageDomain(),
                        Price = _.SalesPrice,
                        OriginalPrice = _.StandardPrice > 0 ? _.StandardPrice : _.SalesPrice,
                        Unit = _.Unit,
                        ProductType = 2
                    }).ToList();
                    break;
            }

            return result;
        }

        private async Task<List<int>> GetChildCategoryId(int mainId, int secondId, int childId)
        {
            List<int> categoryId = new List<int>();

            if (childId > 0)
            {
                categoryId.Add(childId);
            }
            else if (mainId > 0 || secondId > 0)
            {
                List<DimCategoryDO> category = await GetAllCategoryCache(0);
                if (secondId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == secondId).Select(_ => _.Id)
                        .ToList();
                }
                else if (mainId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == mainId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            return categoryId;
        }

        #endregion
    }
}
