using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.BaoYang;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopProduct;
using Ae.Shop.Api.Core.Request.ShopProduct;
using Ae.Shop.Api.Dal.Model.Product;
using Ae.Shop.Api.Dal.Repositorys.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Api.Client.Clients.Product;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Client.Model.BaoYang;
using Ae.Shop.Api.Client.Model.Product;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Client.Response.Product;
using Dao = Ae.Shop.Api.Dal.Repositorys;
using DAL =Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopProductService : IShopProductService
    {
        private readonly IFctShopProductRepository fctShopProductRepository;
        private readonly IIdentityService identityService;
        private readonly IBaoYangClient baoYangClient;
        private readonly IDimSequenceRepository dimSequenceRepository;
        private readonly IShopMangeClient shopMangeClient;
        private readonly IUnitRepository unitRepository;
        private readonly IConfiguration configuration;
        private readonly IProductClient productClient;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly Dao.IShopRepository shopRepository;

        public ShopProductService(IFctShopProductRepository fctShopProductRepository, IIdentityService identityService,
            IBaoYangClient baoYangClient, IDimSequenceRepository dimSequenceRepository,
            IShopMangeClient shopMangeClient, IUnitRepository unitRepository, IConfiguration configuration,
            IProductClient productClient, IMapper mapper, ICategoryRepository categoryRepository, Dao.IShopRepository _shopRepository)
        {
            this.fctShopProductRepository = fctShopProductRepository;
            this.identityService = identityService;
            this.baoYangClient = baoYangClient;
            this.dimSequenceRepository = dimSequenceRepository;
            this.shopMangeClient = shopMangeClient;
            this.unitRepository = unitRepository;
            this.configuration = configuration;
            this.productClient = productClient;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.shopRepository = _shopRepository;
        }

        /// <summary>
        /// 外采商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopProductListVo>> GetShopProductList(ShopProductListRequest request)
        {
            ApiPagedResultData<ShopProductListVo> result = new ApiPagedResultData<ShopProductListVo>
            {
                Items = new List<ShopProductListVo>()
            };

            var organizationId = identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            List<long> shopIds = new List<long>();
            if (identityService.GetOrgType() == "0")
            {
                var shopInfos = await shopRepository.GetListAsync(" where status=0 and  online=1 and  check_status=2 and is_deleted=0 and company_id =@companyId", new { companyId = shopId });
                shopIds = shopInfos.Select(r => r.Id).ToList();
                // shopIds = await GetCurrentCompanyAllShops(shopId, true);
            }
            else if (identityService.GetOrgType() == "1")
            {
                shopIds.Add(shopId);
            }
            // Int32.TryParse(identityService.GetOrganizationId(), out var shopId);

            Dictionary<long, DAL.ShopDO> shopDic = new Dictionary<long, DAL.ShopDO>();


            var products = await fctShopProductRepository.SearchShopProductByCondition(request.SearchContent,
                request.Specification, shopIds, request.PageIndex, request.PageSize, request.CategoryId);
            if (products != null)
            {
                var serviceTypeEnumTask = baoYangClient.GetServiceTypeEnum();
                var secondCategoryTask = GetShopProductCategory();
                await Task.WhenAll(serviceTypeEnumTask, secondCategoryTask);
                var serviceTypeEnum = serviceTypeEnumTask.Result ?? new List<ServiceTypeEnumDto>();
                var secondCategory = secondCategoryTask.Result ?? new List<ProductCategoryVo>();

                result.TotalItems = products.TotalItems;
                if (products.Items != null)
                {
                    result.Items = products.Items.Select(_ => new ShopProductListVo
                    {


                        ProductCode = _.ProductCode,
                        ProductName = _.ProductName,
                        Specification = _.Specification,
                        CategoryName = GetCategoryName(_.CategoryId, serviceTypeEnum, secondCategory),
                        SalesPrice = _.SalesPrice,
                        PurchasePrice = _.PurchasePrice,
                        Unit = _.Unit,
                        OeNumber = _.OeNumber,
                        ShopId = _.ShopId
                    }).ToList();

                    foreach (var item in result.Items)
                    {
                        if (shopDic.ContainsKey(item.ShopId))
                        {
                            item.ShopName = shopDic[item.ShopId].SimpleName;
                        }
                        else
                        {
                            var shopInfo = await shopRepository.GetAsync(item.ShopId);
                            shopDic[item.ShopId] = shopInfo;
                            item.ShopName = shopInfo.SimpleName;
                        }
                    }
                }
            }

            return result;
        }

        public string GetCategoryName(int categoryId, List<ServiceTypeEnumDto> serviceType,
            List<ProductCategoryVo> secondCategory)
        {
            var defaultService = serviceType.FirstOrDefault(t => t.Id == categoryId);
            if (defaultService != null)
            {
                return defaultService.DisplayName;
            }

            foreach (var item1 in secondCategory)
            {
                foreach (var item2 in item1.Children ?? new List<ProductCategoryVo>())
                {
                    if (item2.CategoryId == categoryId)
                    {
                        return $"{item1.DisplayName} / {item2.DisplayName}";
                    }
                }
            }

            return string.Empty;
        }

        public async Task<string> GetCategoryName(int categoryId, List<ServiceTypeEnumDto> serviceType)
        {
            var defaultService = serviceType.FirstOrDefault(t => t.Id == categoryId);
            if (defaultService != null)
            {
                return defaultService.DisplayName;
            }

            var category = await categoryRepository.GetAllCategoryById(categoryId);

            return $"{category.SecondCategoryName} / {category.ChildCategoryName}";
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopProductDetailVo> GetShopProductDetail(ShopProductDetailRequest request)
        {
            //Int32.TryParse(identityService.GetOrganizationId(), out var shopId);

            var productTask = fctShopProductRepository.GetShopProductByProductCode(request.ProductCode, request.ShopId);
            var serveceTypeEnumTask = baoYangClient.GetServiceTypeEnum();
            await Task.WhenAll(productTask, serveceTypeEnumTask);
            var product = productTask.Result;
            var serveceTypeEnum = serveceTypeEnumTask.Result ?? new List<Client.Model.BaoYang.ServiceTypeEnumDto>();
            if (product == null)
            {
                throw new CustomException("商品不存在");
            }

            ShopProductDetailVo result = new ShopProductDetailVo
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Specification = product.Specification,
                CategoryId = product.CategoryId,
                CategoryName =
                    await GetCategoryName(product.CategoryId,
                        serveceTypeEnum), //serveceTypeEnum.FirstOrDefault(t => t.Id == product.CategoryId)?.DisplayName ?? "",
                SalesPrice = product.SalesPrice,
                PurchasePrice = product.PurchasePrice,
                Unit = product.Unit,
                OeNumber = product.OeNumber,
                ShopId = product.ShopId
            };

            return result;
        }

        /// <summary>
        /// 删除商品 禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShopProduct(DeleteShopProductRequest request)
        {
            //Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var result = await fctShopProductRepository.DeleteShopProduct(request.ProductCode, request.ShopId, identityService.GetUserName());

            return result > 0;
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveShopProduct(AddShopProductRequest request)
        {
            long shopId = 0;
            if (request.ShopId > 0)
            {
                shopId = request.ShopId;
            }
            else {
                shopId =  Convert.ToInt64(identityService.GetOrganizationId());
            }
           
            var existData = await fctShopProductRepository.GetExistProducts(request.ProductCode, request.ProductName,
                request.OeNumber, shopId);
            if (existData.Exists(t => t.ProductName == request.ProductName))
            {
                throw new CustomException("产品名称不能重复,核对产品名称!");
            }

            if (existData.Exists(t => t.OeNumber == request.OeNumber && !string.IsNullOrEmpty(request.OeNumber)))
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
                    //ProductCode = await GetShopProductCode(shopId, request.CategoryId),
                    ProductCode = await GetProductCode(request.CategoryId),
                    ProductName = request.ProductName,
                    DisplayName = request.ProductName,
                    SalesPrice = request.SalesPrice,
                    IsStoreoutside = 1,
                    Specification = request.Specification,
                    Unit = request.Unit,
                    OeNumber = request.OeNumber,
                    //PurchasePrice = request.PurchasePrice,
                    OnSale = 1,
                    OnSaleTime = DateTime.Now,
                    CreateBy = identityService.GetUserName(),
                    CreateTime = DateTime.Now,
                    PurchasePrice = request.PurchasePrice
                };
                var record = await fctShopProductRepository.InsertAsync(fctShopProductDo);

                return record > 0;
            }
            else
            {
                //编辑
                FctShopProductDO fctShopProductDo = new FctShopProductDO
                {
                    ShopId = request.ShopId,
                    ProductCode = request.ProductCode,
                    ProductName = request.ProductName,
                    SalesPrice = request.SalesPrice,
                    Specification = request.Specification,
                    Unit = request.Unit,
                    OeNumber = request.OeNumber,
                    //PurchasePrice = request.PurchasePrice,
                    UpdateBy = identityService.GetUserName(),
                    PurchasePrice = request.PurchasePrice
                };
                var record = await fctShopProductRepository.UpdateShopProduct(fctShopProductDo);

                return record > 0;
            }
        }

        /// <summary>
        /// 获取门店服务大类
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeVo>> GetShopServiceType()
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);

            var serviceTypeDtos = await shopMangeClient.GetShopServiceTypeAsync(new Client.Request.ShopManage.ShopServiceTypeClientRequest()
            { ShopId = shopId });

            return serviceTypeDtos?.Select(_ => new ShopServiceTypeVo
            {
                Id = _.ServiceTypeId,
                DisplayName = _.DisplayName
            })?.ToList() ?? new List<ShopServiceTypeVo>();
        }

        /// <summary>
        /// 获取商品单位
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetShopProductUnit()
        {
            var result = await unitRepository.GetDimUnitList();

            return result?.Select(_ => _.UnitName)?.ToList() ?? new List<string>();
        }

        /// <summary>
        /// 获取外采产品类目
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductCategoryVo>> GetShopProductCategory()
        {
            List<ProductCategoryVo> category = new List<ProductCategoryVo>();
            var wcCategoryId = configuration["ProductServer:WaiCaiCategory"];
            Int32.TryParse(wcCategoryId, out int categoryId);
            var result = await productClient.GetProductCategory(new ProductCategoryClientRequest()
            {
                MainCategoryId = new List<int>() { categoryId }
            });
            var child = result.FirstOrDefault()?.Children;
            if (child != null && child.Any())
            {
                child.ForEach(_ =>
                {
                    category.Add(new ProductCategoryVo()
                    {
                        CategoryId = _.CategoryId,
                        CategoryCodeShort = _.CategoryCodeShort,
                        DisplayName = _.DisplayName,
                        CategoryLevel = _.CategoryLevel,
                        ParentId = _.ParentId,
                        Children = _.Children?.Select(t => new ProductCategoryVo
                        {
                            CategoryId = t.CategoryId,
                            CategoryCodeShort = t.CategoryCodeShort,
                            DisplayName = t.DisplayName,
                            CategoryLevel = t.CategoryLevel,
                            ParentId = t.ParentId
                        })?.ToList()
                    });
                });
            }

            return category;
        }

        /// <summary>
        /// 生成门店商品编码
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private async Task<string> GetShopProductCode(long shopId, int categoryId)
        {
            var seqId = await dimSequenceRepository.GenerateProductCode("SP-WC-BM");

            return "SW-" + shopId + "-" + categoryId + "-" + seqId;
        }

        /// <summary>
        /// 获取商品编码 - 外采
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        private async Task<string> GetProductCode(int childCategoryId)
        {
            string productCode = string.Empty;
            string wcFuCategory = configuration["ProductServer:WaiCaiFuCategory"];
            List<int> wcFuCategoryId = wcFuCategory.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => Convert.ToInt32(_)).ToList();
            var category = await categoryRepository.GetAllCategoryById(childCategoryId);
            if (category == null)
            {
                throw new CustomException("产品类目异常");
            }

            string seqName =
                $"{category.MainCategoryShortCode}-{category.SecondCategoryShortCode}-{category.ChildCategoryShortCode}";

            var isExist = await dimSequenceRepository.GetSequenceBySeqName(seqName);
            if (isExist == null)
            {
                await dimSequenceRepository.InsertAsync(new DimSequenceDo()
                {
                    CategoryId = childCategoryId,
                    CurrentVal = 0,
                    IncrementVal = 1,
                    SeqName = seqName
                });
            }

            //验证商品编码是否存在
            var isExistProduct = true;
            while (isExistProduct)
            {
                var seqId = await dimSequenceRepository.GenerateProductCode(seqName);
                productCode = seqName + "-" + seqId;
                //服务产品
                if (wcFuCategoryId.Contains(childCategoryId))
                {
                    productCode += "-FU";
                }

                var shopProduct = await fctShopProductRepository.GetShopProductByPid(productCode);
                if (shopProduct == null)
                {
                    isExistProduct = false;
                }
            }

            return productCode;
        }

        #region 产品管理

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<CatalogBrandVo>> GetBrandList()
        {
            var result = await productClient.GetBrandList();

            return result.Select(_ => new CatalogBrandVo
            {
                BrandName = _.BrandName,
                BrandImg = _.BrandImg
            }).ToList();
        }

        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<CategoryInfoVo>> GetCategorysById(int? categoryId, int? level)
        {
            var result = await productClient.GetCategorysById(categoryId, level);

            return mapper.Map<List<CategoryInfoVo>>(result);
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ProductAllInfoVo>> SearchProductPageList(
            SearchProductPageListRequest request)
        {
            ApiPagedResultData<ProductAllInfoVo> result = new ApiPagedResultData<ProductAllInfoVo>()
            {
                Items = new List<ProductAllInfoVo>()
            };
            Int64.TryParse(identityService.GetOrganizationId(), out long shopId);
            ProductSearchClientRequest para = new ProductSearchClientRequest()
            {
                Key = request.Key,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId,
                SecondCategoryId = request.SecondCategoryId,
                ChildCategoryId = request.ChildCategoryId,
                IsDeleted = request.IsDeleted,
                ClassType = 2,
                IsRetail = request.IsRetail,
                ShopId = shopId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            if (!string.IsNullOrEmpty(request.ProductAttribute))
            {
                para.ProductAttributes = new List<string>() { request.ProductAttribute };
            }

            var product = await productClient.SearchProduct(para);
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    result.Items = mapper.Map<List<ProductAllInfoVo>>(product.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UnitVo>> GetUnitList()
        {
            var result = await productClient.GetUnitList();

            return result.Select(_ => new UnitVo
            {
                Id = _.Id,
                UnitName = _.UnitName,
                IsDeleted = _.IsDeleted
            }).ToList();
        }

        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveProduct(ProductEditRequest request)
        {
            Int64.TryParse(identityService.GetOrganizationId(), out long shopId);
            ProductEditClientRequest clientRequest = new ProductEditClientRequest()
            {
                Product = mapper.Map<ProductAllInfoDto>(request.Product),
                Attributevalues = mapper.Map<List<ProductAttributevalueDto>>(request.Attributevalues),
                InstallationServices = new List<ProductInstallserviceDto>(),
                PackageDetails = mapper.Map<List<ProductPackageDetailDto>>(request.PackageDetails),
                IsEdit = request.IsEdit,
                UserId = identityService.GetUserName()
            };
            clientRequest.Product.ShopId = shopId;
            var result = await productClient.SaveProduct(clientRequest);
            return result;
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        public async Task<ProductDetailVo> GetProductByProductCode(string productCode)
        {

            if (string.IsNullOrEmpty(productCode))
            {
                return null;
            }

            var product = (await productClient.GetProductsByProductCodes(new ProductDetailSearchClientRequest()
            { ProductCodes = new List<string>() { productCode } })).FirstOrDefault();
            if (product != null)
            {
                var result = mapper.Map<ProductDetailVo>(product);
                if (result.Product.ProductAttribute == 1)
                {
                    var package = (await productClient.GetPackageProductsByCodes(new ProductDetailSearchClientRequest()
                    { ProductCodes = new List<string>() { productCode } })).FirstOrDefault();
                    if (package != null)
                    {
                        result.PackageDetails = mapper.Map<List<ProductPackageDetailVo>>(package.Details);
                    }

                }
                else
                {
                    var categoryAttributeValue =
                        await productClient.GetAttributesByCategoryId(product.Product.ChildCategoryId.ToString());
                    var attributeList = mapper.Map<List<ProductAttributeValueVo>>(categoryAttributeValue);
                    var productAttribute = result.Attributevalues;
                    if (productAttribute != null && productAttribute.Any())
                    {
                        foreach (var item in attributeList)
                        {
                            var itemA = productAttribute?.FirstOrDefault(t =>
                                t.AttributeNameId == item.AttributeNameId);
                            if (itemA != null)
                            {
                                item.Id = itemA.Id;
                                item.AttributeValue = itemA.AttributeValue;
                            }
                        }
                    }

                    result.Attributevalues = attributeList;
                }

                return result;
            }

            return null;
        }

        #endregion
    }
}
