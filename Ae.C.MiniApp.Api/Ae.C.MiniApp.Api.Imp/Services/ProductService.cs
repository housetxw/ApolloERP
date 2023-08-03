using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Redis;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Inteface;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.Product;
using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Client.Request.Shop;
using Ae.C.MiniApp.Api.Client.Response.Adapter;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Product;
using Ae.C.MiniApp.Api.Core.Response.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ae.C.MiniApp.Api.Core.Enums;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<ProductService> _logger;
        private readonly IProductClient _productClient;
        private readonly IProductAdapterClient _productAdapterClient;
        private readonly IStockClient _stockClient;
        private readonly IOrderCommentClient _orderCommentClient;
        private readonly IShopClient _shopClient;
        private readonly string redisKey = "Rg:C:MiniApp:ProductSearch";
        private readonly RedisClient _redisClient;
        private readonly IConfiguration _configuration;

        public ProductService(IMapper mapper, ApolloErpLogger<ProductService> logger, IProductClient productClient,
            IProductAdapterClient productAdapterClient,
            IStockClient stockClient, IOrderCommentClient orderCommentClient, IShopClient shopClient,
            RedisClient redisClient, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._productClient = productClient;
            this._productAdapterClient = productAdapterClient;
            this._stockClient = stockClient;
            this._orderCommentClient = orderCommentClient;
            this._shopClient = shopClient;
            this._redisClient = redisClient;
            _configuration = configuration;
        }

        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSearchResponse> ProductSearch(ProductSearchRequest request)
        {
            //请求参数
            var paras = new ProductSearchClientRequest()
            {
                Brand = request.Brand,
                ChildCategoryId = request.ChildCategoryId,
                EndPrice = request.EndPrice,
                Key = request.Key,
                MainCategoryId = request.MainCategoryId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                SecondCategoryId = request.SecondCategoryId,
                Sort = (int)request.Sort,
                StartPrice = request.StartPrice,
                ClassType = 2,//默认查询子产品
                ProductAttributes = new List<int>() { 0, 3 } //查询实物产品
            };
            var result = new ProductSearchResponse();
            //var cacheKey = redisKey + CreateLuceneCacheKey(request);
            ////判断缓存中是否存在 
            //if (_redisClient.Redis.KeyExists(cacheKey))
            //{
            //    result = _redisClient.Redis.GetStringKey<ProductSearchResponse>(cacheKey);
            //    return result;
            //}
            var productSearchClientResponse = await _productClient.ProductSearch(paras);
            if (productSearchClientResponse != null)
            {
                result.Brands = productSearchClientResponse.Brands;
                var productSearch = productSearchClientResponse.Items ?? new List<ProductSearchInfoDto>();
                var firstData = productSearchClientResponse.Items;
                var adapterProducts = new List<ProductSearchInfoDto>();
                // 首次加载去适配       
                if (!string.IsNullOrEmpty(request.VehicleId) &&
                    !string.IsNullOrEmpty(request.Nian) &&
                    !string.IsNullOrEmpty(request.PaiLiang)
                    && request.PageIndex == 1
                    && firstData.Count > 0)
                {
                    var totalPages = productSearchClientResponse.TotalItems % request.PageSize == 0 ?
                         productSearchClientResponse.TotalItems / request.PageSize :
                        (productSearchClientResponse.TotalItems / request.PageSize) + 1;
                    for (int i = 1; i < totalPages + 1; i++)
                    {
                        var products = await AdapterProduct(request, productSearch);
                        if (products != null && products.Count > 0)
                        {
                            adapterProducts.AddRange(products);
                        }
                        if (adapterProducts.Count >= 5)
                        {
                            break;
                        }
                        var pageIndex = i + 1;
                        if (pageIndex < (totalPages + 1))
                        {
                            paras.PageIndex = pageIndex;
                            productSearchClientResponse = await _productClient.ProductSearch(paras);
                            if (productSearchClientResponse != null)
                            {
                                productSearch = productSearchClientResponse.Items;
                            }
                        }
                    }
                    productSearch = adapterProducts;
                    //productSearch.AddRange(firstData.Take(15));
                }
                var productCodes = productSearch?.Select(t => t.ProductCode).ToList();
                var productComments = await _orderCommentClient.GetProductCommentTotalAsync(new ProductCommentTotalClientRequest() { ProductIds = productCodes });
                result.products = productSearch?.Select(t => new SearchProductVo()
                {
                    DisplayName = t.DisplayName,
                    Image1 = t.Image1.AddImageDomain(),
                    ProductCode = t.ProductCode,
                    Name = t.Name,
                    SalesPrice = t.SalesPrice,
                    CommentNumber = 0,
                    FavorableRate = t.FavorableRate,
                    OriginalFactory = true
                }).ToList();
                result.products?.ForEach((t) =>
                {
                    var productComment = productComments.Where(a => a.ProductId == t.ProductCode).FirstOrDefault();
                    if (productComment != null)
                    {
                        t.CommentNumber = productComment.CommentCount;
                    }
                });
                if (result.products == null && result.products.Count == 0)
                {
                    result.Brands = new List<string>();
                }
                //设置缓存时间 
                //if (!result.products.IsNullOrEmpty())
                //{
                //    _redisClient.Redis.SetStringKey<ProductSearchResponse>(cacheKey, result, TimeSpan.FromMinutes(5));
                //}
            }
            return result;
        }


        /// <summary>
        /// 构建缓存key
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="isProduct"></param>
        /// <returns></returns>
        public string CreateLuceneCacheKey(ProductSearchRequest request, string flag = "")
        {
            var properties = request.GetProperties();
            var cachekey = new StringBuilder();
            var notIncludedKey = new List<string> { "pageIndex", "pageSize", "offset", "limit" };
            foreach (var property in properties)
            {
                if (!notIncludedKey.Contains(property.Key))
                {
                    cachekey.Append($"{property.Key}={property.Value}&");
                }
            }
            return HttpUtility.UrlEncode(cachekey.ToString()).ToLower();
        }

        /// <summary>
        ///  商品适配
        /// </summary>
        /// <param name="request"></param>
        /// <param name="productSearch"></param>
        /// <returns></returns>
        public async Task<List<ProductSearchInfoDto>> AdapterProduct(ProductSearchRequest request, List<ProductSearchInfoDto> productSearch)
        {
            var adapterProductIds = new List<string>();
            var adapterProducts = new List<ProductSearchInfoDto>();
            var vehicle = new VehicleClientRequest() { Nian = request.Nian, VehicleId = request.VehicleId, PaiLiang = request.PaiLiang, Tid = request.TId };
            var baoYangProducts = productSearch?.Where(t => t.MainCategoryId == 1).ToList();//保养
            var tireProducts = productSearch?.Where(t => t.MainCategoryId == 4).ToList();//轮胎
            var ortherProducts = productSearch?.Where(t => t.MainCategoryId != 4 && t.MainCategoryId != 1)?.ToList();//其他类目的商品
            if (!ortherProducts.IsNullOrEmpty())
            {
                adapterProducts.AddRange(ortherProducts);
            }
            if (baoYangProducts != null && baoYangProducts.Count > 0)
            {
                var currentPageItem = baoYangProducts;
                var products = currentPageItem?.Select(t => new ProductClinetRequest() { CategoryId = t.ChildCategoryId.ToString(), Pid = t.ProductCode }).ToList();
                var para = new VerifyAdaptiveProductsClientRequest() { Products = products, Vehicle = vehicle };
                var adaptiveProduct = await _productAdapterClient.VerifyAdaptiveBaoYangProducts(para);
                if (adaptiveProduct != null && adaptiveProduct.AdaptivePid != null && adaptiveProduct.AdaptivePid.Count > 0)
                {
                    adapterProductIds.AddRange(adaptiveProduct.AdaptivePid);
                }
            }
            if (tireProducts != null && tireProducts.Count > 0)
            {
                var currentPageItem = tireProducts;
                var pidList = currentPageItem?.Select(t => t.ProductCode).ToList();
                var para = new VerifyTireProductClientRequest() { VehicleId = request.VehicleId, PidList = pidList };
                var tireProduct = await _productAdapterClient.VerifyAdaptiveTireProducts(para);
                if (tireProduct != null && tireProduct.AdaptivePid != null && tireProduct.AdaptivePid.Count > 0)
                {
                    adapterProductIds.AddRange(tireProduct.AdaptivePid);
                }
            }
            if (adapterProductIds != null && adapterProductIds.Count > 0)
            {
                productSearch.ForEach((t) =>
                {
                    if (adapterProductIds.Contains(t.ProductCode))
                    {
                        adapterProducts.Add(t);
                    }
                });
            }
            return adapterProducts;
        }

        /// <summary>
        ///  商品详情
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public async Task<ProductDetailResponse> ProductDetail(ProductDetailRequest request)
        {
            var paras = new ProductDetailSearchClientRequest()
                {ProductCodes = new List<string>() {request.ProductCode}};
            var details = await _productClient.ProductDetail(paras);
            var productDetail = details.FirstOrDefault();
            var result = new ProductDetailResponse();
            if (productDetail != null)
            {
                var product = productDetail.Product;
                var imageList = new List<string>();
                if (product != null)
                {
                    if (!string.IsNullOrEmpty(product.Image1))
                    {
                        imageList.Add(product.Image1.AddImageDomain());
                    }

                    if (!string.IsNullOrEmpty(product.Image2))
                    {
                        imageList.Add(product.Image2.AddImageDomain());
                    }

                    if (!string.IsNullOrEmpty(product.Image3))
                    {
                        imageList.Add(product.Image3.AddImageDomain());
                    }

                    if (!string.IsNullOrEmpty(product.Image4))
                    {
                        imageList.Add(product.Image4.AddImageDomain());
                    }

                    if (!string.IsNullOrEmpty(product.Image5))
                    {
                        imageList.Add(product.Image5.AddImageDomain());
                    }

                    result.Product = _mapper.Map<ProductDetalInfoVo>(product) ?? new ProductDetalInfoVo();
                    // var warehouseStocs = await _stockClient.GetWarehouseStockForAdaptation(request.ProvinceId, request.CityId, new List<string>() { request.ProductCode });
                    // if (warehouseStocs.Any())
                    // {
                    //     result.Product.HasStock = warehouseStocs.FirstOrDefault(t => t.Pid == request.ProductCode)?.AvailableStockQuantity > 0 ? 1 : 0;
                    // }
                    result.Images = imageList;
                    if (!string.IsNullOrEmpty(product.Description))
                    {
                        result.DescriptionImages = product.Description.GetHtmlImageUrlList();
                    }
                }

                var installationServices = productDetail.InstallationServices;
                result.InstallationServices = _mapper.Map<List<InstallserviceVo>>(installationServices) ??
                                              new List<InstallserviceVo>();
                result.InstallationServices.Add(new InstallserviceVo()
                {
                    ServiceName = "无需服务",
                    ServicePrice = 0
                });
                if (!productDetail.Attributevalues.IsNullOrEmpty())
                {
                    result.Attributevalues = productDetail.Attributevalues.Where(t => t.AttributeName == "Viscosity")
                        ?.Select(t => t.AttributeValue).Distinct().ToList();
                }

                switch (request.ProductEntrance)
                {
                    case ProductEntranceEnum.SecKill:
                        var flashSale = await _productClient.GetFlashSaleProduct(request.ProductCode);
                        if (flashSale != null)
                        {
                            result.Product.SalesPrice = flashSale.Price;
                        }

                        break;
                }
            }

            return result;
        }


        /// <summary>
        ///  商品规格接口
        ///  查询是否设置关联商品
        /// </summary>
        /// <param name="productCode">商品编码</param>
        /// <returns></returns>
        public async Task<ProductSpecificationResponse> Specifications(string productCode)
        {
            var result = new ProductSpecificationResponse();
            //result.Specifications = new List<SpecificationVo>() {
            // new SpecificationVo(){ Name="产品尺寸", Items=new List<SpecificationItemVo>(){
            //         new SpecificationItemVo(){ Name="1升"},
            //         new SpecificationItemVo(){ Name="4升" }
            //        }
            // },
            // new SpecificationVo(){ Name="规格", Items=new List<SpecificationItemVo>(){
            //         new SpecificationItemVo(){ Name="5W-30"},
            //         new SpecificationItemVo(){ Name="0W-40" },
            //         new SpecificationItemVo(){ Name="5W-40" }
            //        }
            // },
            //};
            //result.Products = new List<ProductCombinationVo>(){
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 5W-30 SN级（1L装）", ProductCode = "BYFW-JY-MO-26", SalesPrice = 100, Difference = "1升,5W-30"},
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 0W-40 SN级（1L装）", ProductCode = "BYFW-JY-MO-27", SalesPrice = 100, Difference = "1升,0W-40"},
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 5W-40 SN级（1L装）", ProductCode = "BYFW-JY-MO-28", SalesPrice = 100, Difference = "1升,5W-40"},
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 5W-30 SN级（4L装）", ProductCode = "BYFW-JY-MO-29", SalesPrice = 560, Difference = "4升,5W-30"},
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 0W-40 SN级（4L装）", ProductCode = "BYFW-JY-MO-30", SalesPrice = 560, Difference = "4升,0W-40"},
            //    new ProductCombinationVo() { Name = "壳牌/Shell 喜力矿物机油 HX5 5W-40 SN级（4L装）", ProductCode = "BYFW-JY-MO-31", SalesPrice = 560, Difference = "4升,5W-40"},
            //};

            //return result;
            //BYFW-JY-TSO-150
            var associateProductDto = await _productClient.GetAssociateProduct(productCode);
            if (associateProductDto != null && associateProductDto.Details.Any())
            {
                var productCodes = associateProductDto.Details.Select(t => t.ProductCode).Distinct().ToList();
                var details = await _productClient.ProductDetail(new ProductDetailSearchClientRequest() { ProductCodes = productCodes });
                var specifications = new List<SpecificationVo>();
                //计算属性规格
                foreach (var attr in associateProductDto.Attributes)
                {
                    var items = new List<SpecificationItemVo>();
                    var specification = new SpecificationVo(); ;
                    var associateProductDetails = associateProductDto.Details.Where(t => t.AttributeName == attr).ToList();
                    if (associateProductDetails.Count > 0)
                    {
                        specification.Name = associateProductDetails.FirstOrDefault().AttributeDisplayName;
                        items = associateProductDetails.Select(t => t.AttributeValue).Distinct().Select(t => new SpecificationItemVo() { Name = t }).ToList();
                    }
                    specification.Items = items;
                    specifications.Add(specification);
                }
                result.Specifications = specifications;
                var products = new List<ProductCombinationVo>();
                //计算商品规格
                foreach (var code in productCodes)
                {
                    var productCombinationVo = new ProductCombinationVo();
                    productCombinationVo.ProductCode = code;
                    var groups = associateProductDto.Details.Where(t => t.ProductCode == code).OrderBy(t => t.AttributeName).ToList();
                    if (groups.Count > 0)
                    {
                        var group = groups.FirstOrDefault();
                        productCombinationVo.Name = group.Name;
                        productCombinationVo.SalesPrice = group.SalesPrice;
                    }
                    var difference = "";
                    foreach (var item in groups)
                    {
                        difference += "," + item.AttributeValue;
                    }
                    productCombinationVo.Difference = difference.Trim(',');
                    products.Add(productCombinationVo);
                }
                result.Products = products;
            }
            return result;
        }

        /// <summary>
        ///  获取首页美容洗车类目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CategoryInfoVo> GetBeautyWashCategorys(int categoryId)
        {
            var result = new List<CategoryInfoVo>();
            if (categoryId == 1) //美容洗车
            {
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "普洗", Id = 144,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/puxi.png",
                    Description = "清洗车辆外饰"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "精洗", Id = 145,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/jingxi.png",
                    Description = "清洗车辆外饰和车内清洁"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "内饰清洗",
                    Id = 315,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/inclean.png",
                    Description = "清洗车辆内饰"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "打蜡", Id = 155,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/dala.png",
                    Description = "防水，防酸雨"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "镀晶", Id = 156,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/dujing.png",
                    Description = "抗氧化，高亮度，高硬度"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "封釉", Id = 157,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/fengyou.png",
                    Description = "对车漆的整体保护更全面"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "抛光", Id = 158,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/paoguang.png",
                    Description = "去除油漆表面氧化层、浅层的划痕、氧化造成的漆面失光等"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "镀膜",
                    Id = 314,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/dumo.png",
                    Description = "对车漆的整体保护更全面"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "漆面美容",
                    Id = 317,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/qimianmeirong.png",
                    Description = "漆面更光亮"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "行走系统清洁",
                    Id = 318,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/roadclean.png",
                    Description = "轮胎、轮毂深度清洁 "
                });
            }

            if (categoryId == 2) //美容洗车套餐卡
            {
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "标准洗套餐卡", Id = 165,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/standard_wash_icon.png",
                    Description = "清洗车辆外饰套餐卡"
                });
                result.Add(new CategoryInfoVo()
                {
                    CategoryName = "精洗套餐卡", Id = 166,
                    ImageUrl = $"{_configuration["ImageDomain"]}/washcar-icon/fine_wash_icon.png",
                    Description = "清洗车辆外饰和车内清洁套餐卡"
                });
            }

            return result;
        }

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CategoryProductVo>> SelectProductsByCategory(CategoryProductRequest request)
        {
            var para = new CategoryProductClientRequest()
            {
                CategoryId = request.CategoryId,
                HasAttribute = false,
                IsOnSale = 1,
                PageIndex = 1,
                PageSize = 100
            };
            var baseInfoDtos = await _productClient.SelectProductsByCategory(para);
            var baseInfoVos = _mapper.Map<List<CategoryProductVo>>(baseInfoDtos);
            var result = new List<CategoryProductVo>();
            //验证门店服务是否上架
            if (request.ShopId.HasValue)
            {
                var serviceListRequest = new ShopServiceListRequest()
                {
                    ProductIds = baseInfoDtos.Select(t => t.ProductCode).ToList(),
                    ShopId = request.ShopId.Value
                };
                var shopOnSaleServices = await _shopClient.GetOnSaleShopServiceList(serviceListRequest);
                baseInfoVos.ForEach((t =>
                {
                    var onSaleService = shopOnSaleServices.Where(a => a.Pid == t.ProductCode && a.IsOnline).FirstOrDefault();
                    if (onSaleService != null)
                    {
                        result.Add(t);
                    }
                }));
            }
            else
            {
                result = baseInfoVos;
            }
            if (result.Count > 0)
            {
                result.ForEach((t) =>
                {
                    t.Image1 = t.Image1.AddImageDomain();
                });
            }
            return result;
        }

        /// <summary>
        /// 获取有效的秒杀活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<FlashSaleConfigVo>>> GetActiveFlashSaleConfigs(GetActiveFlashSaleConfigsRequest request)
        {
            return await _productClient.GetActiveFlashSaleConfigs(request);
        }
    }
}
