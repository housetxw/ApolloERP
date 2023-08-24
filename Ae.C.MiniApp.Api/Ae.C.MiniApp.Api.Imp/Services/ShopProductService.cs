using AutoMapper;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.ShopProduct;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.ShopProduct;
using Ae.C.MiniApp.Api.Core.Request.ShopProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Response.Product;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using ApolloErp.Login.Auth;
using Ae.C.MiniApp.Api.Core.Request.Product;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Product;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Response.PageConfig;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ShopProductService : IShopProductService
    {
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IPromotionClient _promotionClient;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;

        public ShopProductService(IProductClient productClient, IMapper mapper, IPromotionClient promotionClient,
            IIdentityService identityService, IConfiguration configuration)
        {
            this._productClient = productClient;
            this._mapper = mapper;
            _promotionClient = promotionClient;
            _identityService = identityService;
            _configuration = configuration;
        }

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopProductVO>> SelectShopProduct(ShopProductQueryRequest request)
        {
            var result = new List<ShopProductVO>();
            var paras = new ShopProductQueryClientRequest()
            {
                CategoryId = request.CategoryId,
                IsOnSale = 1,
                ShopId = request.ShopId
            };
            var shopProductDtos = await _productClient.SelectShopProduct(paras);
            if (!shopProductDtos.IsNullOrEmpty())
            {
                result = _mapper.Map<List<ShopProductVO>>(shopProductDtos);

                var promotionActivys = await _promotionClient.GetPromotionActivitByProductCodeList(new Core.Request.Promotion.
                    GetPromotionActivitByProductCodeListRequest()
                {
                    ProductCodeList = result?.Select(_ => _.ProductCode)?.ToList(),
                    PromotionChannel = PromotionChannelEnum.MiniApp.ToString()
                });
                result?.ForEach((t) =>
                    {
                        t.Icon = t.Icon.AddImageDomain();
                        var singlePromotionActivy = promotionActivys?.Data?.Where(_ => _.ProductCode == t.ProductCode)?.FirstOrDefault();
                        t.PromotioPrice = singlePromotionActivy?.PromotionPrice ?? 0;
                        t.ActivityId = singlePromotionActivy?.ActicityId ?? string.Empty;
                        t.IsPromotionProduct = !string.IsNullOrEmpty(singlePromotionActivy?.ActicityId);
                        t.AvailQuantity = singlePromotionActivy?.AvailQuantity??0;
                    });
            }
            return result;
        }

        /// <summary>
        /// 获取门店服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ServiceProjectVo>> GetShopServiceProject(ShopServiceProjectRequest request)
        {
            var result = await _productClient.GetOnSaleShopProduct(new OnSaleShopProductClientRequest()
            {
                ShopId = request.ShopId,
                CategoryId = request.CategoryId,
                Channel = PromotionChannelEnum.MiniApp.ToString()
            });

            return result.Select(_ => new ServiceProjectVo
            {
                ProductCode = _.ProductCode,
                DisplayName = _.DisplayName,
                Description = _.Description,
                Price = string.IsNullOrEmpty(_.ActivityId) ? _.SalesPrice : _.PromotionPrice,
                ImageUrl = _.Icon,
                Tags = _.Tags?.Select(t => new Ae.C.MiniApp.Api.Core.Model.TagInfo
                {
                    Type = t.Type,
                    Tag = t.Tag,
                    TagColor = t.TagColor
                })?.ToList() ?? new List<TagInfo>()
            }).ToList();
        }

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductListByServiceTypeResponse> GetProductListByServiceType(
            ProductListByServiceTypeRequest request)
        {
            ProductListByServiceTypeResponse data = new ProductListByServiceTypeResponse();
            var result = await _productClient.GetProductListByServiceType(new ProductListByServiceTypeClientRequest
            {
                SearchContent = request.SearchContent,
                CategoryId = request.CategoryId,
                UserId = _identityService.GetUserId(),
                ShopId = request.ShopId,
                Channel = "MiniApp",
                Vehicle = _mapper.Map<VehicleClientRequest>(request.Vehicle)
            });

            if (result != null)
            {
                data = _mapper.Map<ProductListByServiceTypeResponse>(result);
            }

            return data;
        }

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductDetailPageDataResponse> GetProductDetailPageData(ProductDetailPageDataRequest request)
        {
            var result = await _productClient.GetProductDetailPageData(new ProductDetailPageDataClientRequest
            {
                ProductCode = request.ProductCode,
                ShopId = request.ShopId,
                Channel = "MiniApp"
            });

            if (result != null)
            {
                return _mapper.Map<ProductDetailPageDataResponse>(result);
            }

            throw new CustomException("该商品不存在");
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<HotProductVo>> GetHotProductPageList(
            HotProductPageListRequest request)
        {
            ApiPagedResultData<HotProductVo> response = new ApiPagedResultData<HotProductVo>()
            {
                Items = new List<HotProductVo>()
            };
            var result = await _productClient.GetHotProductPageList(new HotProductPageListClientRequest()
            {
                TerminalType = TerminalType.CbJApplet,
                OnSale = 1,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<HotProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 获取套餐卡列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProduct(
            PackageCardProductRequest request)
        {
            ApiPagedResultData<PackageCardProductVo> result = new ApiPagedResultData<PackageCardProductVo>()
            {
                Items = new List<PackageCardProductVo>()
            };
            var product = await _productClient.GetPackageCardProductPageList(
                new PackageCardProductPageListClientRequest()
                {
                    OnSale = 1,
                    IsRetail = 1,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    result.Items = _mapper.Map<List<PackageCardProductVo>>(product.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 首页功能配置
        /// </summary>
        /// <returns></returns>
        public async Task<MainPageConfigResponse> GetMainPageConfig(MainPageConfigRequest request)
        {
            string frontCategoryVersion = _configuration["ProductServer:FrontCategoryVersion"];
            Int32.TryParse(frontCategoryVersion, out int version);
            var advertisementTask = _productClient.QueryConfigAdvertisement(
                new QueryConfigAdvertisementClientRequest()
                {
                    TerminalType = TerminalType.CbJApplet.ToString()
                });
            var categoryTask = _productClient.GetMainFrontCategory(new MainFrontCategoryClientRequest()
            {
                TerminalType = TerminalType.CbJApplet.ToString()
            });
            await Task.WhenAll(advertisementTask, categoryTask);
            var advertisement = advertisementTask.Result ?? new List<ConfigAdvertisementDto>();
            var category = categoryTask.Result ?? new List<ConfigFrontCategoryDto>();
            MainPageConfigResponse result = new MainPageConfigResponse()
            {
                TopAdvertising = advertisement.Where(_ => _.Type == AdvertisementTypeEnum.Top.ToString()).Select(_ =>
                    new AdvertisingModel
                    {
                        AdvertisingCode = _.Code,
                        ImageUrl = _.ImageUrl,
                        BeginImageUrl = _.BeginImageUrl,
                        BtnImageUrl = _.BtnImageUrl,
                        Btn2ImageUrl = _.Btn2ImageUrl,
                        Btn3ImageUrl = _.Btn3ImageUrl,
                        EndImageUrl = _.EndImageUrl,
                        RouteUrl = _.RouteUrl,
                        GotoUrl = _.GotoUrl,
                        Goto2Url = _.Goto2Url,
                        Goto3Url = _.Goto3Url,
                        Key = _.ExtendId,
                        Type = _.Type
                    }).ToList(),
                Modules = TransModules(category.Where(_ => _.Version == version).ToList())
            };

            if (request != null && request.Version > 1)
            {
                //顶部和底部一起返回
                result.ProminentActive = advertisement.Where(_ => _.Type != "")
                    .Select(
                        _ =>
                            new AdvertisingModel
                            {
                                AdvertisingCode = _.Code,
                                ImageUrl = _.ImageUrl,
                                BeginImageUrl = _.BeginImageUrl,
                                BtnImageUrl = _.BtnImageUrl,
                                Btn2ImageUrl = _.Btn2ImageUrl,
                                Btn3ImageUrl = _.Btn3ImageUrl,
                                EndImageUrl = _.EndImageUrl,
                                RouteUrl = _.RouteUrl,
                                GotoUrl = _.GotoUrl,
                                Goto2Url = _.Goto2Url,
                                Goto3Url = _.Goto3Url,
                                Key = _.ExtendId,
                                Type = _.Type
                            }).ToList();
            }
            else
            {
                result.ProminentActive = new List<AdvertisingModel>()
                {
                    new AdvertisingModel()
                    {
                        AdvertisingCode = "ProminentActive1",
                        ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/home_actvie.png",
                        RouteUrl = "/pages/detailsPages/main",
                        Key = _configuration["ProductServer:ApolloErpJiYouPackage"]
                    }
                };
            }

            return result;
        }

        private List<ModuleBlock> TransModules(List<ConfigFrontCategoryDto> modules)
        {
            List<ModuleBlock> result = new List<ModuleBlock>();
            modules.ForEach(_ =>
            {
                FrontCategoryExtendVo extend = new FrontCategoryExtendVo();
                var extendPara = _.ExtendParam;
                if (!string.IsNullOrEmpty(extendPara))
                {
                    extend = JsonConvert.DeserializeObject<FrontCategoryExtendVo>(extendPara);
                }

                result.Add(new ModuleBlock
                {
                    Category = _.Category,
                    DisplayName = _.DisplayName,
                    ImageUrl = _.ImageUrl,
                    RouteUrl = _.RouteUrl,
                    SubTitle = _.SubTitle,
                    PackageType = extend.PackageType,
                    ServiceType = extend.ServiceType
                });
            });
            return result;
        }
    }
}
