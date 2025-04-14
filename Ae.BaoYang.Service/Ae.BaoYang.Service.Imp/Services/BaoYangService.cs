using Ae.BaoYang.Service.Client.Clients;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Imp.Model;
using AutoMapper;
using Ae.BaoYang.Service.Dal.Model.Extend;
using Ae.BaoYang.Service.Imp.Enum;
using Ae.BaoYang.Service.Common.Common;
using Ae.BaoYang.Service.Common.Exceptions;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Common.Constant;
using System.Text.RegularExpressions;
using ApolloErp.Redis;
using Ae.BaoYang.Service.Common.Helper;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Client.Response;
using ApolloErp.Web.WebApi;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Ae.BaoYang.Service.Core.Model.Config;

namespace Ae.BaoYang.Service.Imp.Services
{
    public class BaoYangService: IBaoYangService
    {
        private readonly IMapper _mapper;
        private readonly IBaoYangCategoryConfigRepository _baoYangCategoryConfigRepository;
        private readonly IBaoYangPackageConfigRepository _baoYangPackageConfigRepository;
        private readonly IBaoYangPackageMapConfigRepository _baoYangPackageMapConfigRepository;
        private readonly IBaoYangTypeConfigRepository _baoYangTypeConfigRepository;
        private readonly VehicleClient _vehicleClient;
        private readonly IBaoYangPartAccessoryRepository _baoYangPartAccessoryRepository;
        private readonly IBaoYangPartsRepository _baoYangPartsRepository;
        private readonly IBaoYangInstallTypeConfigRepository _baoYangInstallTypeConfigRepository;
        private readonly IBaoYangAntifreezeSettingRepository _baoYangAntifreezeSettingRepository;
        private readonly IBaoYangPartAccessoryMapRepository _baoYangPartAccessoryMapRepository;
        private readonly IVehicleFuelTypeConfigRepository _vehicleFuelTypeConfigRepository;
        private readonly IBaoYangPackageRefRepository _baoYangPackageRefRepository;
        private readonly IBaoYangPartsMapConfigRepository _baoYangPartsMapConfigRepository;
        private readonly ShopManageClient _shopManageClient;
        private readonly ProductClient _productClient;
        private readonly UserClient _userClient;
        private readonly StockClient _stockClient;
        private readonly IBaoYangConfigRepository _baoYangConfigRepository;
        private readonly RedisClient _redisClient;
        private readonly OrderClient _orderClient;
        private readonly IBaoYangPropertyAdaptationRepository _baoYangPropertyAdaptationRepository;
        private readonly IBaoYangPropertyDescriptionRepository _baoYangPropertyDescriptionRepository;
        private readonly IServiceTypeEnumRepository _serviceTypeEnumRepository;
        private readonly string redisKey = "Ae:BaoYang:Service:BaoYang";
        private readonly IConfiguration _configuration;
        private readonly IBaoYangInstallFeeConfigRepository _baoYangInstallFeeConfigRepository;
        private readonly IBaoYangPrioritySettingRepository _baoYangPrioritySettingRepository;

        private readonly List<string> _allViscosity = new List<string>()
        {
            "0W-20",
            "0W-30",
            "0W-40",
            "0W-50",
            "5W-20",
            "5W-30",
            "5W-40",
            "5W-50",
            "10W-20",
            "10W-30",
            "10W-40",
            "10W-50",
            "10W-60",
            "15W-30",
            "15W-40",
            "15W-50",
            "20W-50",
            "20W-60",
            "25W-60"
        };

        private readonly List<string> _allLevel = new List<string>()
        {
            "HX3", "HX5", "HX7", "HX8", "ULTRA"
        };

        private readonly List<string> _allDescription = new List<string>
        {
            "SA", "SB", "SC", "SD", "SE", "SF", "SG", "SH", "SJ", "SL", "SM", "SN", "CA", "CB", "CC", "CD", "CE", "CF",
            "CF-2", "CF-4", "CG-4", "CH-4", "CI-4", "CJ-4"
        };

        public BaoYangService(IMapper mapper, IBaoYangCategoryConfigRepository baoYangCategoryConfigRepository,
            IBaoYangPackageConfigRepository baoYangPackageConfigRepository,
            IBaoYangPackageMapConfigRepository baoYangPackageMapConfigRepository,
            IBaoYangTypeConfigRepository baoYangTypeConfigRepository, VehicleClient vehicleClient,
            IBaoYangPartAccessoryRepository baoYangPartAccessoryRepository,
            IBaoYangPartsRepository baoYangPartsRepository,
            IBaoYangInstallTypeConfigRepository baoYangInstallTypeConfigRepository,
            IBaoYangAntifreezeSettingRepository baoYangAntifreezeSettingRepository,
            IBaoYangPartAccessoryMapRepository baoYangPartAccessoryMapRepository,
            IVehicleFuelTypeConfigRepository vehicleFuelTypeConfigRepository,
            IBaoYangPackageRefRepository baoYangPackageRefRepository,
            IBaoYangPartsMapConfigRepository baoYangPartsMapConfigRepository, ShopManageClient shopManageClient,
            ProductClient productClient, UserClient userClient, StockClient stockClient,
            IBaoYangConfigRepository baoYangConfigRepository, RedisClient redisClient, OrderClient orderClient,
            IBaoYangPropertyAdaptationRepository baoYangPropertyAdaptationRepository,
            IBaoYangPropertyDescriptionRepository baoYangPropertyDescriptionRepository,
            IServiceTypeEnumRepository serviceTypeEnumRepository, IConfiguration configuration,
            IBaoYangInstallFeeConfigRepository baoYangInstallFeeConfigRepository,
            IBaoYangPrioritySettingRepository baoYangPrioritySettingRepository)
        {
            _mapper = mapper;
            _baoYangCategoryConfigRepository = baoYangCategoryConfigRepository;
            _baoYangPackageConfigRepository = baoYangPackageConfigRepository;
            _baoYangPackageMapConfigRepository = baoYangPackageMapConfigRepository;
            _baoYangTypeConfigRepository = baoYangTypeConfigRepository;
            _vehicleClient = vehicleClient;
            _baoYangPartAccessoryRepository = baoYangPartAccessoryRepository;
            _baoYangPartsRepository = baoYangPartsRepository;
            _baoYangInstallTypeConfigRepository = baoYangInstallTypeConfigRepository;
            _baoYangAntifreezeSettingRepository = baoYangAntifreezeSettingRepository;
            _baoYangPartAccessoryMapRepository = baoYangPartAccessoryMapRepository;
            _vehicleFuelTypeConfigRepository = vehicleFuelTypeConfigRepository;
            _baoYangPackageRefRepository = baoYangPackageRefRepository;
            _baoYangPartsMapConfigRepository = baoYangPartsMapConfigRepository;
            _shopManageClient = shopManageClient;
            _productClient = productClient;
            _userClient = userClient;
            _stockClient = stockClient;
            _baoYangConfigRepository = baoYangConfigRepository;
            _redisClient = redisClient;
            _orderClient = orderClient;
            _baoYangPropertyAdaptationRepository = baoYangPropertyAdaptationRepository;
            _baoYangPropertyDescriptionRepository = baoYangPropertyDescriptionRepository;
            _serviceTypeEnumRepository = serviceTypeEnumRepository;
            _configuration = configuration;
            _baoYangInstallFeeConfigRepository = baoYangInstallFeeConfigRepository;
            _baoYangPrioritySettingRepository = baoYangPrioritySettingRepository;
        }

        /// <summary>
        /// 保养适配首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangCategory>>> GetBaoYangPackagesAsync(GetBaoYangPackagesRequest request)
        {
            List<BaoYangCategory> result = new List<BaoYangCategory>();
            List<TargetProduct> targetProduct = new List<TargetProduct>();
            if (request.ProductId != null && request.ProductId.Any())
            {
                targetProduct = await GetDefaultProduct(request.ProductId);
            }

            var categoryConfigTask = _baoYangCategoryConfigRepository.GetBaoYangCategoryConfigAsync(); //一级分类
            var packageTask = GetAllBaoYangPackagesAsync(request.Vehicle, request.UserId, request.ProvinceId,
                request.CityId, request.Channel, request.BaoYangType, targetProduct, request.ShopId); //
            await Task.WhenAll(categoryConfigTask, packageTask);
            var categoryConfig = categoryConfigTask.Result?.ToList() ?? new List<BaoYangCategoryConfigDO>();
            var packageResult = packageTask.Result;
            if (packageResult.Code == ResultCode.Success && !string.IsNullOrEmpty(packageResult.Message))
            {
                return new ApiResult<List<BaoYangCategory>>()
                {
                    Code = packageResult.Code,
                    Message = packageResult.Message
                };
            }

            var packageData = packageResult.Data ?? new List<BaoYangPackageModel>();
            var categoryGroup = categoryConfig.GroupBy(o => o.CategoryAlias)
                .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());

            foreach (var category in categoryGroup)
            {

                BaoYangCategory categoryItem = new BaoYangCategory
                {
                    CategoryType = category.Key,
                    CategoryName = category.Value[0].CategoryName,
                    SimpleCategoryName = category.Value[0].CategorySimpleName,
                    PackageItems = packageData
                        .Where(x => category.Value.Select(_ => _.PackageType).Contains(x.PackageType)).ToList()
                };
                result.Add(categoryItem);
            }

            result.RemoveAll(_ => _.PackageItems == null || !_.PackageItems.Any());

            return Result.Success(result);
        }

        /// <summary>
        /// 保养适配首页接口 - 返回所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangPackageModel>>> GetBaoYangPackagesAllProductAsync(
            GetBaoYangPackagesRequest request)
        {
            var packages = await GetAllBaoYangPackagesAsync(request.Vehicle, request.UserId, request.ProvinceId,
                request.CityId, request.Channel, request.BaoYangType, new List<TargetProduct>(), request.ShopId, true);

            return packages;
        }

        /// <summary>
        /// 指定商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<TargetProduct>> GetDefaultProduct(List<string> productId)
        {
            List<TargetProduct> result = new List<TargetProduct>();
            if (productId != null && productId.Any())
            {
                var baoYangTypeConfig =
                    (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
                var baoYangTypeConfigMap =
                    (await GetPackageMapConfigCache())?.ToList() ?? new List<BaoYangPackageMapConfigDO>();
                var products = await _productClient.GetProductsByProductCodes(productId);
                foreach (var product in products)
                {
                    var categoryId = product.Product.ChildCategoryId;
                    var defaultBaoYangType =
                        baoYangTypeConfig.FirstOrDefault(x =>
                            x.ChildCategories.Split(new[] {','}).Contains(categoryId.ToString()));
                    if (defaultBaoYangType != null)
                    {
                        var defaultMap =
                            baoYangTypeConfigMap.FirstOrDefault(x => x.BaoYangType == defaultBaoYangType.BaoYangType);
                        if (defaultMap != null)
                        {
                            result.Add(new TargetProduct()
                            {
                                PackageType = defaultMap.PackageType,
                                BaoYangType = defaultBaoYangType.BaoYangType,
                                Pid = product.Product.ProductCode
                            });
                        }
                    }
                }
            }

            return result;
        }

        #region 更多商品列表

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsWithConditionAsync(
            SearchProductRequest request)
        {
            request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);

            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            string baoYangType = request.BaoYangType;
            var baoYangTypeItemTask = GetBaoYangTypeItem(baoYangType);
            var userAttentionPidTask = _userClient.GetUserAttentionPid(request.UserId);
            var priorityTask = GetProductPrioritySettingCache();
            await Task.WhenAll(baoYangTypeItemTask, userAttentionPidTask, priorityTask);
            List<string> userAttentionPid = userAttentionPidTask.Result ?? new List<string>();
            var baoYangTypeItem = baoYangTypeItemTask.Result;
            var priority = priorityTask.Result?.Where(_ => _.BaoYangType == baoYangType)?.ToList() ??
                           new List<BaoYangProductPriorityModel>();
            if (baoYangTypeItem != null)
            {
                switch (baoYangTypeItem.TypeGroup)
                {
                    case "Package":
                        result = await SearchPackageProductsForPackage(request, priority);
                        break;
                    case "Accessory":
                        result = await SearchPackageProductsForAccessory(request, baoYangTypeItem.CategoryName);
                        break;
                    case "Part":
                        result = await SearchPackageProductsForPart(request, priority);
                        break;
                    case "Maintainence":
                        result = await SearchPackageProductsForMaintainence(request, baoYangTypeItem.CategoryName,
                            priority);
                        break;
                }

                if (result != null && result.Products != null)
                {
                    var currentPid = request.PidCount.Select(_ => _.Pid).ToList();
                    foreach (var itemProduct in result.Products)
                    {
                        itemProduct.IsAttention = userAttentionPid.Contains(itemProduct.Pid);
                        itemProduct.IsDefaultSelect = currentPid.Contains(itemProduct.Pid);
                    }
                }
            }
            else
            {
                throw new CustomException("baoYangType参数有误");
            }

            return result;
        }

        /// <summary>
        /// 获取BaoYangTypeItem
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        private async Task<BaoYangTypeConfigDO> GetBaoYangTypeItem(string baoYangType)
        {
            var baoYangTypes = (await GetBaoYangTypeConfigCache())?.ToList() ??
                               new List<BaoYangTypeConfigDO>(); //baoYangType

            return baoYangTypes.FirstOrDefault(_ => _.BaoYangType == baoYangType);
        }

        /// <summary>
        /// 套餐商品更多列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        private async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsForPackage(
            SearchProductRequest request, List<BaoYangProductPriorityModel> priority)
        {
            var baoYangTypes = await GetBaoYangTypeConfigCache();
            var jiYouAdaption = Convert.ToInt32(_configuration["SwitchConfig:JiYouAdaption"]) > 0; //机油是否要适配
            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            List<string> packagePidList = new List<string>();
            List<string> jiYouPackageType = new List<string>() {"xby-tc", "dby-tc"};
            if (jiYouPackageType.Contains(request.BaoYangType) && !jiYouAdaption)
            {
                var itemBaoYangType = baoYangTypes.FirstOrDefault(_ => _.BaoYangType == request.BaoYangType);
                if (itemBaoYangType != null)
                {
                    var products = await GetProductByCategoryAsync(itemBaoYangType.CategoryName, 0);

                    packagePidList = products.Select(_ => _.Pid).ToList();
                }
            }
            else
            {
                var packageId =
                    (await _baoYangPackageRefRepository.GetBaoYangPackageRefByParaAsync(request.Vehicle.Tid,
                        new List<string>()
                        {
                            request.BaoYangType
                        })
                    )?.ToList() ?? new List<BaoYangPackageRefDO>();
                packagePidList = packageId.Select(_ => _.PackageId).ToList();
            }

            if (packagePidList.Any())
            {
                var baoYangParts = await GetBaoYangPartsByTid(request.Vehicle.Tid);
                List<BaoYangPackageProductModel> products = (await GetPackageProductsAsync(packagePidList, priority,
                    request.UserId, request.Channel, request.ProvinceId, request.CityId, baoYangParts,
                    request.Vehicle, 0, null, request.ShopId)).Item2;
                result.Products = products;
            }

            return result;
        }

        /// <summary>
        /// Maintainence 商品更多列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        private async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsForMaintainence(
            SearchProductRequest request, string category, List<BaoYangProductPriorityModel> priority)
        {
            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            result.Products =
                await GetMaintainenceProduct(category, priority, request.ProvinceId, request.CityId, request.ShopId);
            return result;
        }

        /// <summary>
        /// Maintainence 适配商品列表
        /// </summary>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageProductModel>> GetMaintainenceProduct(string category,
            List<BaoYangProductPriorityModel> priority, string provinceId, string cityId, int shopId,
            int limitCount = 0)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var products = await GetProductByCategoryAsync(category, shopId);
            if (products != null && products.Any())
            {
                //var productStock =
                //    await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
                //        products.Select(_ => _.Pid).ToList());
                //products.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                products = products.OrderByDescending(_ => RankProduct(priority, _)).ThenBy(_ => _.Price).ToList();
                if (limitCount > 0)
                {
                    products = products.Take(limitCount).ToList();
                }

                products.ForEach(_ =>
                {
                    var itemProduct = ConvertProduct(_, 1);
                    result.Add(itemProduct);
                });
            }

            return result;
        }

        private int RankProduct(List<BaoYangProductPriorityModel> priority, BaoYangProductModel product)
        {
            int sum = 0;
            int priorityLength = priority.Count;
            if (!product.StockOut)
            {
                sum += (int) System.Math.Pow(10, priorityLength + 1);
            }

            int i = 0;
            foreach (BaoYangProductPriorityModel itemP in priority.OrderBy(_ => _.Priority))
            {
                switch (itemP.PriorityField)
                {
                    case ProductPriorityField.Brand:
                        if (product.Brand == itemP.PriorityValue)
                        {
                            sum += (int) System.Math.Pow(10, priorityLength - i);
                        }

                        break;
                    case ProductPriorityField.Sku:
                        if (product.Pid == itemP.PriorityValue)
                        {
                            sum += (int) System.Math.Pow(10, priorityLength - i);
                        }

                        break;
                }

                i++;
            }

            return sum;
        }

        private int RankProduct(List<BaoYangProductPriorityModel> priority, PackageInfoVo product)
        {
            int sum = 0;
            int priorityLength = priority.Count;
            if (!product.StockOut)
            {
                sum += (int) System.Math.Pow(10, priorityLength + 1);
            }

            int i = 0;
            foreach (BaoYangProductPriorityModel itemP in priority.OrderBy(_ => _.Priority))
            {
                switch (itemP.PriorityField)
                {
                    case ProductPriorityField.Brand:
                        if (product.Brand == itemP.PriorityValue)
                        {
                            sum += (int) System.Math.Pow(10, priorityLength - i);
                        }

                        break;
                    case ProductPriorityField.Sku:
                        if (product.ProductCode == itemP.PriorityValue)
                        {
                            sum += (int) System.Math.Pow(10, priorityLength - i);
                        }

                        break;
                }

                i++;
            }

            return sum;
        }

        /// <summary>
        /// Part 商品更多列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        private async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsForPart(
            SearchProductRequest request, List<BaoYangProductPriorityModel> priority)
        {
            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            var baoYangParts =
                await GetBaoYangPartsByTid(request.Vehicle.Tid, new List<string>() {request.BaoYangType});
            if (baoYangParts != null && baoYangParts.Any())
            {
                result.Products =
                    await GetPartProduct(
                        baoYangParts.SelectMany(_ => _.Pids?.Select(t => t.Pid) ?? new List<string>()).ToList(),
                        baoYangParts[0].Count, priority, request.ProvinceId, request.CityId, request.ShopId);
            }

            return result;
        }

        /// <summary>
        /// Part 适配商品列表
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="count"></param>
        /// <param name="priority"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageProductModel>> GetPartProduct(List<string> pidList, int count,
            List<BaoYangProductPriorityModel> priority, string provinceId, string cityId, int shopId,
            int limitCount = 0)
        {
            if (count == 0)
            {
                count = 1;
            }

            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            if (pidList != null && pidList.Any())
            {
                var productResultTask = GetProductByPidListAsync(pidList, shopId);
                //var productStockTask = _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId, pidList);
                await Task.WhenAll(productResultTask);
                var productResult = productResultTask.Result;
                //var productStock = productStockTask.Result ?? new List<WarehouseStock>();
                if (productResult != null && productResult.Any())
                {
                    //productResult.ForEach(_ =>
                    //{
                    //    _.AvailableStockQuantity =
                    //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                    //});
                    productResult = ProductRecommendRule(priority, productResult);
                    if (limitCount > 0)
                    {
                        productResult = productResult.Take(limitCount).ToList();
                    }

                    productResult.ForEach(_ =>
                    {
                        var itemProduct = ConvertProduct(_, count);
                        result.Add(itemProduct);
                    });
                }
            }

            return result;
        }

        private List<BaoYangProductModel> ProductRecommendRule(List<BaoYangProductPriorityModel> priority,
            List<BaoYangProductModel> product)
        {
            return product.OrderByDescending(_ => RankProduct(priority, _)).ThenBy(_ => _.Price).ToList();
        }

        /// <summary>
        /// Accessory 商品更多列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsForAccessory(
            SearchProductRequest request, string category)
        {
            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            var accessories = await GetAccessoryByTid(request.Vehicle.Tid, request.BaoYangType);
            if (accessories != null && accessories.Any())
            {
                result.Products =
                    await GetAccessoryProduct(request, accessories.FirstOrDefault(), category);
            }

            return result;
        }

        private async Task<List<BaoYangPackageProductModel>> GetAccessoryProduct(SearchProductRequest request,
            BaoYangAccessoryModel accessory, string category)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            switch (request.BaoYangType)
            {
                case "jiyou":
                    result = await GetJiYouProducts(accessory, category, request.ProvinceId, request.CityId,
                        request.PidCount.FirstOrDefault()?.Pid, request.ShopId);
                    break;
                case "scy":
                    result = await GetScyProducts(accessory, category, request.ProvinceId, request.CityId,
                        GlobalConstant.DefaultAccessoryUnitFix, request.ShopId);
                    break;
                default:
                    result = await GetAccessoryProductByCategory(category, request.ProvinceId, request.CityId,
                        request.ShopId);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 机油适配商品
        /// </summary>
        /// <param name="accessory"></param>
        /// <param name="category"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="currentPid"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetJiYouProducts(BaoYangAccessoryModel accessory,
            string category, string provinceId, string cityId, string currentPid,int shopId, int limitCount = 0)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var productsTask = GetProductByCategoryAsync(category, shopId);
            var currentProductTask = GetProductByPidListAsync(new List<string> { currentPid }, 0);
            await Task.WhenAll(productsTask, currentProductTask);
            var products = productsTask.Result;
            var currentProduct = currentProductTask.Result?.FirstOrDefault();
            if (products != null && products.Any() && currentProduct != null)
            {
                OilModel oil = new OilModel(accessory);
                if (oil.IsValid())
                {
                    var availViscosity = GetAvailViscosity(oil.Viscosity);
                    var availLevel = GetAvailLevel(oil.Level);
                    var availDescription = GetAvailDescription(oil.Description);
                    products = products.Where(_ =>
                        availViscosity.Contains(_.Viscosity) && availLevel.Contains(_.Level) &&
                        ValidDescription(_.Specification, availDescription) && _.Unit == currentProduct.Unit).ToList();
                    //var productStock = await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
                    //    products.Select(_ => _.Pid).ToList());
                    products.ForEach(_ =>
                    {
                        //_.AvailableStockQuantity =
                        //    productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                        _.IsOriginal = _.Viscosity == oil.Viscosity;
                    });

                    products = products.OrderBy(_ => _.StockOut)
                        .ThenByDescending(_ => _.Viscosity == oil.Viscosity).ThenByDescending(_ => _.Level == oil.Level)
                        .ThenByDescending(_ => _.Specification == oil.Description).ThenBy(_ => _.Price).ToList();
                    if (limitCount > 0)
                    {
                        products = products.Take(limitCount).ToList();
                    }

                    products.ForEach(_ =>
                    {
                        var itemProduct = ConvertProduct(_, _.Count);
                        result.Add(itemProduct);
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 机油适配推荐
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <param name="viscosity"></param>
        /// <param name="products"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageProductModel>> SelectBaoYangJiYouBySetting(decimal volume, string level,
            string description, string viscosity, List<BaoYangProductModel> products, string provinceId, string cityId)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var availViscosity = GetAvailViscosity(viscosity);
            var availLevel = GetAvailLevel(level);
            var availDescription = GetAvailDescription(description);
            products = products.Where(_ =>
                availViscosity.Contains(_.Viscosity) && availLevel.Contains(_.Level) &&
                ValidDescription(_.Specification, availDescription)).ToList();
            //var productStock =
            //    await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
            //        products.Select(_ => _.Pid).ToList());
            products.ForEach(_ =>
            {
                //_.AvailableStockQuantity =
                //    productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                _.IsOriginal = _.Viscosity == viscosity;
            });
            int volumeN = (int) Math.Ceiling(volume);
            var count1 = volumeN % 4;
            var count4 = volumeN / 4;
            var product1 =
                products.Where(t => string.Equals(t.Unit, "1L", StringComparison.CurrentCultureIgnoreCase)).ToList();
            var product4 =
                products.Where(t => string.Equals(t.Unit, "4L", StringComparison.CurrentCultureIgnoreCase)).ToList();
            if (count1 > 0 && count4 > 0)
            {
                products.ForEach(_ =>
                {
                    if (_.Unit.Equals("1L"))
                    {
                        _.Count = count1;
                    }
                    else if (_.Unit.Equals("4L"))
                    {
                        _.Count = count4;
                    }
                });

                var product1Pid = product1.Select(x => x.Pid).ToList();
                var product4Pid = product4.Select(x => x.Pid).ToList();

                var sameSeriesWiper = products
                    .GroupBy(s => new {s.Viscosity, s.Level, s.Specification, s.Brand, s.OilSeries})
                    .Where(s =>
                    {
                        return s.Any()
                               && s.Any(w => product1Pid.Contains(w.Pid))
                               && s.Any(w => product4Pid.Contains(w.Pid));
                    }).OrderByDescending(x =>
                    {
                        return x.Where(_ => product1Pid.Contains(_.Pid) || product4Pid.Contains(_.Pid))
                            .All(_ => !_.StockOut);
                    }).ThenBy(_ => availViscosity.IndexOf(_.Key.Viscosity)).ThenBy(_ => availLevel.IndexOf(_.Key.Level))
                    .ThenBy(_ => availDescription.IndexOf(_.Key.Specification))
                    .ThenBy(_ => _.Sum(t => t.Count * t.Price)).ToList();
                if (sameSeriesWiper.Any())
                {
                    var wipers = sameSeriesWiper[0];
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => product4Pid.Contains(w.Pid)), count4));
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => product1Pid.Contains(w.Pid)), count1));
                }
                else if (product4.Any())
                {
                    result.Add(OilRecommenderRule(product4, availViscosity, availLevel, availDescription, count4 + 1));
                }
                else if (product1.Any())
                {
                    result.Add(OilRecommenderRule(product1, availViscosity, availLevel, availDescription, count4 * 4 + count1));
                }
            }
            else if (count4 > 0)
            {
                if (product4.Any())
                {
                    result.Add(OilRecommenderRule(product4, availViscosity, availLevel, availDescription, count4));
                }
                else if (product1.Any())
                {
                    result.Add(OilRecommenderRule(product1, availViscosity, availLevel, availDescription, count4 * 4));
                }
            }
            else if (count1 > 0)
            {
                if (product1.Any())
                {
                    result.Add(OilRecommenderRule(product1, availViscosity, availLevel, availDescription, count1));
                }
                else if (product4.Any())
                {
                    result.Add(OilRecommenderRule(product4, availViscosity, availLevel, availDescription, 1));
                }
            }

            return result;
        }

        /// <summary>
        /// 机油排序规则
        /// </summary>
        /// <param name="products"></param>
        /// <param name="availViscosity"></param>
        /// <param name="availLevel"></param>
        /// <param name="availDescription"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private BaoYangPackageProductModel OilRecommenderRule(List<BaoYangProductModel> products,
            List<string> availViscosity, List<string> availLevel, List<string> availDescription, int count)
        {
            if (products != null && products.Any())
            {
                return ConvertProduct(
                    products.OrderBy(_ => _.StockOut)
                        .ThenBy(_ => availViscosity.IndexOf(_.Viscosity)).ThenBy(_ => availLevel.IndexOf(_.Level))
                        .ThenBy(_ => availDescription.IndexOf(_.Specification)).ThenBy(_ => _.Price).FirstOrDefault(),
                    count);
            }

            return null;
        }

        /// <summary>
        /// 获取机油可用粘度
        /// </summary>
        /// <param name="viscosity"></param>
        /// <returns></returns>
        private List<string> GetAvailViscosity(string viscosity)
        {
            var allOilViscosity = _allViscosity.Where(x =>
            {
                int viscosityP;
                int viscosityS;
                int currentviscosityP;
                int currentviscosityS;
                if (Int32.TryParse(x.Split('-')[1], out viscosityS) &&
                    Int32.TryParse(x.Split('-')[0].Replace("W", ""), out viscosityP) &&
                    Int32.TryParse(viscosity.Split('-')[1], out currentviscosityS) &&
                    Int32.TryParse(viscosity.Split('-')[0].Replace("W", ""), out currentviscosityP))
                {
                    return viscosityS >= currentviscosityS && viscosityP <= currentviscosityP;
                }
                else
                {
                    return false;
                }
            }).Distinct()
                .OrderBy(x => Convert.ToInt32(x.Split('-')[1]))
                .ThenByDescending(x => Convert.ToInt32(x.Split('-')[0].Replace("W", ""))).ToList();
            if (!allOilViscosity.Any() && !string.IsNullOrEmpty(viscosity))
            {
                allOilViscosity = new List<string>
                {
                    viscosity
                };
            }

            return allOilViscosity;
        }

        /// <summary>
        /// 获取机油粘度等级
        /// </summary>
        /// <param name="oilLevel"></param>
        /// <returns></returns>
        private List<string> GetAvailLevel(string oilLevel)
        {
            var availLevel = new List<string>();
            if (!string.IsNullOrEmpty(oilLevel) && oilLevel.Contains("HX"))
            {
                availLevel = _allLevel.Where(x => x.Contains("HX")).Where(x =>
                        Convert.ToInt32(Regex.Replace(x, "\\D", "")) >=
                        Convert.ToInt32(Regex.Replace(oilLevel, "\\D", "")))
                    .OrderBy(x => Convert.ToInt32(Regex.Replace(x, "\\D", ""))).ToList();
                availLevel.Add("ULTRA");
            }
            else if (!string.IsNullOrEmpty(oilLevel))
            {
                availLevel.Add(oilLevel);
            }

            return availLevel;
        }

        /// <summary>
        /// 获取机油可用级别
        /// </summary>
        /// <param name="oilDescription"></param>
        /// <returns></returns>
        private List<string> GetAvailDescription(string oilDescription)
        {
            var availDescription = new List<string>();
            if (!string.IsNullOrEmpty(oilDescription) && oilDescription.Length > 1)
            {
                char seriesChar = oilDescription.ToCharArray()[0]; //目前有S，C,A
                var nextChar = oilDescription.ToCharArray()[1];
                availDescription = _allDescription.Where(x => x.StartsWith(seriesChar))
                    .Where(x => x.Substring(1, 1).ToCharArray()[0] >= nextChar).OrderBy(x => x.Substring(1, 1))
                    .ToList();
            }
            else
            {
                availDescription = _allDescription.OrderBy(x => x.Substring(1, 1)).ToList();
            }

            return availDescription;
        }

        private bool ValidDescription(string description, List<string> availDescription)
        {
            List<string> itemDes = description?.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                                   new List<string>();

            return itemDes.Exists(_ => availDescription?.Contains(_) ?? false);
        }

        /// <summary>
        /// 刹车油适配商品
        /// </summary>
        /// <param name="accessory"></param>
        /// <param name="category"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="unitFix"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetScyProducts(BaoYangAccessoryModel accessory,
            string category, string provinceId, string cityId, string unitFix, int shopId, int limitCount = 0)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var products = await GetProductByCategoryAsync(category, shopId);
            if (products != null && products.Any())
            {
                BrakeFluidModel scyModel = new BrakeFluidModel(accessory);
                if (scyModel.IsValid())
                {
                    products = FilterScyLevel(products, scyModel.Level);
                    //var productStock = await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
                    //    products.Select(_ => _.Pid).ToList());
                    products.ForEach(_ =>
                    {
                        //_.AvailableStockQuantity =
                        //    productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                        string unitStr = _.Unit;
                        int count = 1;
                        decimal unit = 0;
                        if (!string.IsNullOrEmpty(unitStr) &&
                            decimal.TryParse(unitStr.Replace(unitFix, string.Empty), out unit))
                        {
                            if (unit > 0)
                            {
                                count = (int) Math.Ceiling(scyModel.Volume / unit);
                            }
                        }

                        _.Count = count;
                    });

                    products = products.OrderBy(_ => _.StockOut)
                        .ThenBy(_ => _.Price * _.Count).ToList();
                    if (limitCount > 0)
                    {
                        products = products.Take(limitCount).ToList();
                    }

                    products.ForEach(_ =>
                    {
                        var itemProduct = ConvertProduct(_, _.Count);
                        result.Add(itemProduct);
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 根据类目适配商品
        /// </summary>
        /// <param name="category"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetAccessoryProductByCategory(string category,
            string provinceId, string cityId, int shopId, int limitCount = 0)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var products = await GetProductByCategoryAsync(category, shopId);
            if (products != null && products.Any())
            {
                //var productStock =
                //    await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
                //        products.Select(_ => _.Pid).ToList());
                //products.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                products = products.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price).ToList();
                if (limitCount > 0)
                {
                    products = products.Take(limitCount).ToList();
                }

                products.ForEach(_ =>
                {
                    var itemProduct = ConvertProduct(_, 1);
                    result.Add(itemProduct);
                });
            }

            return result;
        }

        private List<BaoYangProductModel> FilterScyLevel(List<BaoYangProductModel> product, int level)
        {
            return product.Where(o =>
            {
                int current = 0;
                Int32.TryParse(o.ScyLevel, out current);
                return current >= level;
            }).ToList();
        }

        #endregion

        /// <summary>
        /// 套餐查询
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="priority"></param>
        /// <param name="userId"></param>
        /// <param name="channel"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="partDic"></param>
        /// <param name="vehicle"></param>
        /// <param name="limitCount"></param>
        /// <param name="targetPid"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        private async Task<Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>> GetPackageProductsAsync(
            List<string> pidList, List<BaoYangProductPriorityModel> priority, string userId, string channel, string provinceId, string cityId,
            List<BaoYangPartModel> partDic, VehicleRequest vehicle, int limitCount = 0, string targetPid = null, int shopId = 0)
        {
            PropertySelectModel carProperty = null;
            // List<string> userPurchased = new List<string>();
            // if (!string.IsNullOrEmpty(userId))
            // {
            //     userPurchased = await GetUserPurchase(userId);
            // }

            List<string> onLineService = new List<string>();
            if (shopId > 0)
            {
                onLineService = await GetAllOnLineServiceCache(shopId);
            }

            var propertyDescription = (await GetBaoYangPropertyDescriptionCache())?.ToList() ??
                                      new List<BaoYangPropertyDescriptionDO>();

            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var package = await _productClient.GetPackageProductsByCodesAsync(pidList);
            if (package != null && package.Any())
            {

                List<string> allPidList = package.Select(_ => _.PackageInfo.ProductCode).ToList();
                List<string> childPid = package.SelectMany(_ => _.Details).Where(_ => _.ProjectType == 1)
                    .Select(_ => _.ProjectId).ToList();
                allPidList.AddRange(childPid);
                var products = await GetProductByPidListAsync(allPidList, 0);
                var onAllPidList = products.Select(x => x.Pid).ToList();

                //var realPid = package.SelectMany(_ => _.Details)
                //    .Where(_ => _.ProjectType == 1 && !_.ProjectId.EndsWith("-FU"))
                //    .Select(_ => _.ProjectId).ToList(); //实物产品pid
                //var productStock = await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId, realPid);
                package = package.Where(_ => onAllPidList.Contains(_.PackageInfo.ProductCode)).ToList(); //剔除未上架的商品
                package.ForEach(_ =>
                {
                    //var realItem = _.Details.Where(t => t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")).ToList();
                    //realItem.ForEach(x =>
                    //{
                    //    x.AvailableStockQuantity =
                    //        (productStock.FirstOrDefault(d => d.Pid == x.ProjectId)?.AvailableStockQuantity ?? 0) /
                    //        x.Quantity;
                    //});
                    //_.PackageInfo.AvailableStockQuantity = realItem.Min(t => t.AvailableStockQuantity);
                   // _.PackageInfo.Purchased = userPurchased.Contains(_.PackageInfo.ProductCode);
                    _.PackageInfo.StockOut =
                        products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode)?.StockOut ?? true;
                    _.PackageInfo.Brand = products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode)?.Brand ??
                                          string.Empty;

                    var partType = _.Details.Where(t => t.ProjectType == 2).ToList(); //只配了类型，未配到pid
                    if (partType.Any())
                    {
                        foreach (var itemPart in partType)
                        {
                            var defaultPart = partDic.FirstOrDefault(t => t.BaoYangType == itemPart.ProjectId);
                            var brandList = itemPart.ProjectBrands ?? new List<string>();
                            var configPid = defaultPart?.Pids?.ToList() ?? new List<PartProduct>();
                            var property = defaultPart?.Properties ?? new List<PropertyAdaptive>();
                            if (itemPart.ProjectId == "jv" && !configPid.Any())
                            {
                                continue;
                            }
                            else if (!configPid.Any())
                            {
                                _.HasAdaptionData = false;
                                break;
                            }

                            if (brandList.Any())
                            {
                                configPid = configPid.Where(x => brandList.Contains(x.Brand)).ToList();
                            }

                            #region 五级属性

                            var propertyAdaptation = PropertyAdaptation(configPid, vehicle, property, brandList,
                                propertyDescription); //五级属性适配
                            carProperty = propertyAdaptation.Item1;
                            var propertyProduct = propertyAdaptation.Item2;
                            if (itemPart.ProjectId == "jv" && !propertyProduct.Any())
                            {
                                continue;
                            }
                            else if (!propertyProduct.Any())
                            {
                                _.HasAdaptionData = false;
                                break;
                            }

                            #endregion
                        }
                    }

                    var fuPid = _.Details.Where(t => t.ProjectType == 1 && t.ProjectId.EndsWith("-FU")).ToList();
                    if (fuPid.Any() && shopId > 0)
                    {
                        if (!fuPid.All(t => onLineService.Contains(t.ProjectId)))
                        {
                            _.HasAdaptionData = false;
                        }
                    }

                    #region 机油升数匹配

                    // var realProduct = _.Details.Where(t => t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")).ToList();
                    // var realPid = realProduct.Select(t => t.ProjectId).ToList();
                    // if (realPid.Any())
                    // {
                    //     var jiYouProduct = products.Where(t => realPid.Contains(t.Pid) && t.SecondCategoryId == 12)
                    //         .ToList();
                    //     if (jiYouProduct.Any())
                    //     {
                    //         var accessoryConfig = accessoryDic.FirstOrDefault(x => x.AccessoryName == "发动机油");
                    //         int volume = 0;
                    //         foreach (var itemP in jiYouProduct)
                    //         {
                    //             int itemVolume = Convert.ToInt32(Regex.Replace(itemP.Unit, "\\D", ""));
                    //             int count = realProduct.FirstOrDefault(t => t.ProjectId == itemP.Pid)?.Quantity ?? 1;
                    //             volume = volume + itemVolume * count;
                    //         }
                    //
                    //         if (accessoryConfig != null && !string.IsNullOrEmpty(accessoryConfig.Volume))
                    //         {
                    //             int volumeD = (int) Math.Ceiling(decimal.Parse(accessoryConfig.Volume));
                    //             if (volume != volumeD)
                    //             {
                    //                 _.HasAdaptionData = false;
                    //             }
                    //         }
                    //     }
                    // }

                    #endregion
                });
                package.RemoveAll(_ => !_.HasAdaptionData);
                if (carProperty != null)
                {
                    return new Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>(carProperty,
                        new List<BaoYangPackageProductModel>());
                }
                // if (!package.Any() && carProperty != null)
                // {
                //     return new Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>(carProperty,
                //         new List<BaoYangPackageProductModel>());
                // }

                package = package.OrderByDescending(_ => RankProduct(priority, _.PackageInfo))
                    .ThenBy(_ => _.PackageInfo.SalesPrice).ToList();
                if (!string.IsNullOrEmpty(targetPid))
                {
                    package = package.OrderByDescending(x => x.PackageInfo.ProductCode == targetPid).ToList();
                }

                if (limitCount > 0)
                {
                    package = package.Take(limitCount).ToList();
                }

                foreach (var _ in package)
                {
                    var product = products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode);
                    if (product != null)
                    {
                        List<BaoYangPackageProductModel> childProduct = new List<BaoYangPackageProductModel>();
                        var productItem = ConvertProduct(product, 1);
                        foreach (var detail in _.Details.OrderBy(x => x.Sort))
                        {
                            if (detail.ProjectType == 1)
                            {
                                var itemProduct = products.FirstOrDefault(x => x.Pid == detail.ProjectId);
                                if (itemProduct != null)
                                {
                                    childProduct.Add(ConvertProduct(itemProduct, detail.Quantity));
                                }
                            }
                            else if (detail.ProjectType == 2)
                            {
                                childProduct.Add(new BaoYangPackageProductModel
                                {
                                    Count = detail.Quantity,
                                    Pid = "",
                                    DisplayName = detail.ProjectName,
                                    Image = await GetUrlByBaoYangType(detail.ProjectId)
                                });
                            }
                        }

                        productItem.IsPackageProduct = true;
                        productItem.ChildProducts = childProduct;

                        result.Add(productItem);
                    }
                }
            }

            return new Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>(null, result);
        }

        private async Task<string> GetUrlByBaoYangType(string baoYangType)
        {
            var types = await GetBaoYangTypeConfigCache();

            return types?.FirstOrDefault(_ => _.BaoYangType == baoYangType)?.ImageUrl ?? string.Empty;
        }

        /// <summary>
        /// 套餐适配查询，展示所有适配的套餐
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="priorityCu"></param>
        /// <param name="userId"></param>
        /// <param name="channel"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="partDic"></param>
        /// <param name="vehicle"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>> GetPackageAllProductsAsync(
            List<string> pidList, List<BaoYangProductPriorityModel> priorityCu, string userId, string channel, string provinceId, string cityId,
            List<BaoYangPartModel> partDic, VehicleRequest vehicle, int shopId)
        {
            string commonJvPid = _configuration["SwitchConfig:CommonJvPid"];
            PropertySelectModel carProperty = null;
            // List<string> userPurchased = new List<string>();
            // if (!string.IsNullOrEmpty(userId))
            // {
            //     userPurchased = await GetUserPurchase(userId);
            // }

            List<string> onLineService = new List<string>();
            if (shopId > 0)
            {
                onLineService = await GetAllOnLineServiceCache(shopId);
            }

            var priority = await GetProductPrioritySettingCache();
            var propertyDescription = (await GetBaoYangPropertyDescriptionCache())?.ToList() ??
                                      new List<BaoYangPropertyDescriptionDO>();
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var package = await _productClient.GetPackageProductsByCodesAsync(pidList);
            Dictionary<string, List<string>> baoYangTypePid = new Dictionary<string, List<string>>();
            if (package != null && package.Any())
            {
                List<string> allPidList = package.Select(_ => _.PackageInfo.ProductCode).ToList();
                List<string> childPid = package.SelectMany(_ => _.Details).Where(_ => _.ProjectType == 1)
                    .Select(_ => _.ProjectId).ToList();
                allPidList.AddRange(childPid);
                var products = await GetProductByPidListAsync(allPidList, 0);
                var onAllPidList = products.Select(x => x.Pid).ToList();
                package = package.Where(_ => onAllPidList.Contains(_.PackageInfo.ProductCode)).ToList(); //剔除未上架的商品
                package.ForEach(_ =>
                {
                    //_.PackageInfo.Purchased = userPurchased.Contains(_.PackageInfo.ProductCode);
                    _.PackageInfo.StockOut =
                        products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode)?.StockOut ?? true;
                    _.PackageInfo.Brand = products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode)?.Brand ??
                                          string.Empty;
                    var partType = _.Details.Where(t => t.ProjectType == 2).ToList(); //只配了类型，未配到pid
                    if (partType.Any())
                    {
                        foreach (var itemPart in partType)
                        {
                            var defaultPart = partDic.FirstOrDefault(t => t.BaoYangType == itemPart.ProjectId);
                            var brandList = itemPart.ProjectBrands ?? new List<string>();
                            var configPid = defaultPart?.Pids?.ToList() ?? new List<PartProduct>();
                            var property = defaultPart?.Properties ?? new List<PropertyAdaptive>();
                            if (itemPart.ProjectId == "jv" && !configPid.Any())
                            {
                                if (!baoYangTypePid.ContainsKey(itemPart.ProjectId))
                                {
                                    baoYangTypePid.Add(itemPart.ProjectId, new List<string>() { commonJvPid });
                                }
                                continue;
                            }
                            else if (!configPid.Any())
                            {
                                _.HasAdaptionData = false;
                                break;
                            }

                            if (brandList.Any())
                            {
                                configPid = configPid.Where(x => brandList.Contains(x.Brand)).ToList();
                            }

                            #region 五级属性

                            var propertyAdaptation = PropertyAdaptation(configPid, vehicle, property, brandList,
                                propertyDescription); //五级属性适配
                            carProperty = propertyAdaptation.Item1;
                            var propertyProduct = propertyAdaptation.Item2;

                            if (carProperty == null)
                            {
                                if (itemPart.ProjectId == "jv" && !propertyProduct.Any())
                                {
                                    if (!baoYangTypePid.ContainsKey(itemPart.ProjectId))
                                    {
                                        baoYangTypePid.Add(itemPart.ProjectId, new List<string>() {commonJvPid});
                                    }

                                    continue;
                                }
                                else if (!propertyProduct.Any())
                                {
                                    _.HasAdaptionData = false;
                                    break;
                                }

                                if (!baoYangTypePid.ContainsKey(itemPart.ProjectId))
                                {
                                    baoYangTypePid.Add(itemPart.ProjectId, propertyProduct.Select(t => t.Pid).ToList());
                                }
                            }

                            // if (carProperty == null && !propertyProduct.Any())
                            // {
                            //     _.HasAdaptionData = false;
                            //     break;
                            // }

                            #endregion
                        }
                    }

                    var fuPid = _.Details.Where(t => t.ProjectType == 1 && t.ProjectId.EndsWith("-FU")).ToList();
                    if (fuPid.Any() && shopId > 0)
                    {
                        if (!fuPid.All(t => onLineService.Contains(t.ProjectId)))
                        {
                            _.HasAdaptionData = false;
                        }
                    }

                    #region 机油升数匹配

                    // var realProduct = _.Details.Where(t => t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")).ToList();
                    // var realPid = realProduct.Select(t => t.ProjectId).ToList();
                    // if (realPid.Any())
                    // {
                    //     var jiYouProduct = products.Where(t => realPid.Contains(t.Pid) && t.SecondCategoryId == 12)
                    //         .ToList();
                    //     if (jiYouProduct.Any())
                    //     {
                    //         var accessoryConfig = accessoryDic.FirstOrDefault(x => x.AccessoryName == "发动机油");
                    //         int volume = 0;
                    //         foreach (var itemP in jiYouProduct)
                    //         {
                    //             int itemVolume = Convert.ToInt32(Regex.Replace(itemP.Unit, "\\D", ""));
                    //             int count = realProduct.FirstOrDefault(t => t.ProjectId == itemP.Pid)?.Quantity ?? 1;
                    //             volume = volume + itemVolume * count;
                    //         }
                    //
                    //         if (accessoryConfig != null && !string.IsNullOrEmpty(accessoryConfig.Volume))
                    //         {
                    //             int volumeD = (int)Math.Ceiling(decimal.Parse(accessoryConfig.Volume));
                    //             if (volume != volumeD)
                    //             {
                    //                 _.HasAdaptionData = false;
                    //             }
                    //         }
                    //     }
                    // }

                    #endregion
                });
                package.RemoveAll(_ => !_.HasAdaptionData);
                package = package.OrderByDescending(_ => RankProduct(priorityCu, _.PackageInfo))
                    .ThenBy(_ => _.PackageInfo.SalesPrice).ToList();

                List<string> partPid = baoYangTypePid.SelectMany(t => t.Value).ToList();
                List<BaoYangProductModel> partProduct = new List<BaoYangProductModel>();
                if (partPid.Any())
                {
                    partProduct = await GetProductByPidListAsync(partPid, 0);
                }

                foreach (var _ in package)
                {
                    var product = products.FirstOrDefault(t => t.Pid == _.PackageInfo.ProductCode);
                    if (product != null)
                    {
                        List<BaoYangPackageProductModel> childProduct = new List<BaoYangPackageProductModel>();
                        var productItem = ConvertProduct(product, 1);
                        foreach (var detail in _.Details.OrderBy(x => x.Sort))
                        {
                            if (detail.ProjectType == 1)
                            {
                                var itemProduct = products.FirstOrDefault(x => x.Pid == detail.ProjectId);
                                if (itemProduct != null)
                                {
                                    BaoYangPackageProductModel childP = ConvertProduct(itemProduct, detail.Quantity);
                                    childP.ReplaceProduct = detail.ReplaceProduct == 1;
                                    childP.PackageDetailId = detail.Id;
                                    childProduct.Add(childP);
                                }
                            }
                            else if (detail.ProjectType == 2)
                            {
                                if (baoYangTypePid.TryGetValue(detail.ProjectId, out List<string> cuPid))
                                {
                                    BaoYangProductModel itemProduct =
                                        ProductRecommendRule(
                                            priority.Where(t => t.BaoYangType == detail.ProjectId).ToList(),
                                            partProduct.Where(t => cuPid.Contains(t.Pid)).ToList()).FirstOrDefault();
                                    if (itemProduct != null)
                                    {
                                        BaoYangPackageProductModel
                                            childP = ConvertProduct(itemProduct, detail.Quantity);
                                        childP.ReplaceProduct = detail.ReplaceProduct == 1;
                                        childP.PackageDetailId = detail.Id;
                                        childProduct.Add(childP);
                                    }
                                    else
                                    {
                                        childProduct.Add(new BaoYangPackageProductModel
                                        {
                                            Count = detail.Quantity,
                                            Pid = "",
                                            DisplayName = detail.ProjectName,
                                            Image = await GetUrlByBaoYangType(detail.ProjectId),
                                            ReplaceProduct = detail.ReplaceProduct == 1,
                                            PackageDetailId = detail.Id
                                        });
                                    }
                                }
                                else
                                {
                                    childProduct.Add(new BaoYangPackageProductModel
                                    {
                                        Count = detail.Quantity,
                                        Pid = "",
                                        DisplayName = detail.ProjectName,
                                        Image = await GetUrlByBaoYangType(detail.ProjectId),
                                        ReplaceProduct = detail.ReplaceProduct == 1,
                                        PackageDetailId = detail.Id
                                    });
                                }
                            }
                        }

                        productItem.IsPackageProduct = true;
                        productItem.ChildProducts = childProduct;

                        result.Add(productItem);
                    }
                }
            }

            return new Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>(carProperty, result);
        }

        /// <summary>
        /// package配置
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="shopId"></param>
        /// <param name="targetProduct"></param>
        /// <returns></returns>
        private async Task<ApiResult<PackageDescriptionConfig>> GetPackageDescriptionConfig(VehicleRequest vehicle,
            int shopId,
            List<TargetProduct> targetProduct)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PackageDescriptionConfig:{timestamp}";
            var packageConfig = await RedisHelper.GetOrSetAsync(_redisClient, key, () => GetPackageDescriptionConfig(),
                new TimeSpan(1, 0, 0, 0));
            await VehicleFuelTypeFilter(vehicle, packageConfig.Packages); //机油类别过滤packageType
            var currentPackage = packageConfig.Packages.Select(_ => _.PackageType).ToList();
            if (targetProduct != null && targetProduct.Any())
            {
                foreach (var itemProduct in targetProduct)
                {
                    if (!currentPackage.Contains(itemProduct.PackageType))
                    {
                        return new ApiResult<PackageDescriptionConfig>()
                        {
                            Message = "车型和活动套餐不匹配，请查看其他套餐",
                            Code = ResultCode.Success
                        };
                    }
                }
            }

            if (shopId > 0)
            {
                await ShopServeFilter(shopId, packageConfig.Packages); //门店服务过滤packageType
                var currentPackage2 = packageConfig.Packages.Select(_ => _.PackageType).ToList();
                if (targetProduct != null && targetProduct.Any())
                {
                    foreach (var itemProduct in targetProduct)
                    {
                        if (!currentPackage2.Contains(itemProduct.PackageType))
                        {
                            return new ApiResult<PackageDescriptionConfig>()
                            {
                                Message = "该门店暂未上架此套餐，请查看其他套餐",
                                Code = ResultCode.Success
                            };
                        }
                    }
                }
            }

            return Result.Success(packageConfig);
        }

        /// <summary>
        /// 保养分类配置
        /// </summary>
        /// <returns></returns>
        private async Task<PackageDescriptionConfig> GetPackageDescriptionConfig()
        {
            var packageConfigTask = _baoYangPackageConfigRepository.GetBaoYangPackageConfigAsync(); //packageType
            var packageMapTask = GetPackageMapConfigCache(); //packageType关联baoyangType
            var baoYangTypeTask = GetBaoYangTypeConfigCache(); //baoYangType
            await Task.WhenAll(packageConfigTask, packageMapTask, baoYangTypeTask);
            var packageConfig = packageConfigTask.Result?.ToList() ?? new List<BaoYangPackageConfigDO>();
            var packageMap = packageMapTask.Result?.ToList() ?? new List<BaoYangPackageMapConfigDO>();
            var baoYangType = baoYangTypeTask.Result?.ToList() ?? new List<BaoYangTypeConfigDO>();
            var baoYangItem = baoYangType.Select(_ => new BaoYangTypeDescription
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName,
                CatalogName = _.CategoryName,
                Group = _.TypeGroup
            }).ToList();
            List<BaoYangPackageDescription> packages = new List<BaoYangPackageDescription>();
            foreach (var packageItem in packageConfig)
            {
                BaoYangPackageDescription package = new BaoYangPackageDescription
                {
                    PackageType = packageItem.PackageType,
                    DisplayName = packageItem.DisplayName,
                    BriefDescription = packageItem.BriefDescription,
                    SuggestTip = packageItem.SuggestTip,
                    DescriptionLink = packageItem.DescriptionLink,
                    DetailDescription = packageItem.DetailDescription,
                    ProductType = packageItem.ProductType,
                    ImageUrl = packageItem.ImageUrl,
                    ServiceIds =
                        packageItem.ServiceId?.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                            ?.ToList() ?? new List<string>()
                };
                List<string> baoYangTypeList = packageMap.Where(_ => _.PackageType == packageItem.PackageType)
                    .Select(_ => _.BaoYangType).ToList();
                package.Items = baoYangItem.Where(_ => baoYangTypeList.Contains(_.BaoYangType)).ToList();
                packages.Add(package);
            }

            return new PackageDescriptionConfig
            {
                Packages = packages,
            };
        }

        private async Task<ApiResult<List<BaoYangPackageModel>>> GetAllBaoYangPackagesAsync(VehicleRequest vehicle,
            string userId,
            string provinceId, string cityId, string channel, List<string> selectType,
            List<TargetProduct> targetProduct, int shopId = 0, bool showAll = false)
        {
            var jiYouAdaption = Convert.ToInt32(_configuration["SwitchConfig:JiYouAdaption"]) > 0;//机油是否要适配
            vehicle = await FillVehicleInfoAsync(vehicle);
            var userAttentionPidTask = _userClient.GetUserAttentionPid(userId);
            var packageConfigResult = await GetPackageDescriptionConfig(vehicle, shopId, targetProduct);
            if (packageConfigResult.Code == ResultCode.Success && !string.IsNullOrEmpty(packageConfigResult.Message))
            {
                return new ApiResult<List<BaoYangPackageModel>>()
                {
                    Code = packageConfigResult.Code,
                    Message = packageConfigResult.Message
                };
            }

            var packageConfig = packageConfigResult.Data;
            var baoYangConfigItem =
                packageConfig.Packages.Where(_ => _.ProductType == 1).SelectMany(_ => _.Items).ToList();
            var baoYangPackageConfigItem =
                packageConfig.Packages.Where(_ => _.ProductType == 0).SelectMany(_ => _.Items).ToList();
            List<BaoYangPackageRefModel> packageRef = new List<BaoYangPackageRefModel>();
            List<BaoYangAccessoryModel> accessories = new List<BaoYangAccessoryModel>();
            Task<List<BaoYangPackageRefModel>> packageRefTask = null;
            Task<List<BaoYangPartModel>> partTask = GetBaoYangPartsByTid(vehicle.Tid);
            Task<List<BaoYangAccessoryModel>> accessoriesTask = null;
            Task<List<BaoYangProductPriorityModel>> priorityTask = GetProductPrioritySettingCache();
            var accessoryConfig =
                baoYangConfigItem.Where(_ => _.Group == BaoYangTypeGroup.Accessory.ToString()).ToList();

            if (accessoryConfig.Any())
            {
                accessoriesTask = GetAccessoryByTid(vehicle.Tid);
            }

            if (baoYangPackageConfigItem.Any())
            {
                packageRefTask = GetBaoYangPackageRef(vehicle.Tid);
            }

            List<BaoYangPartModel> baoYangParts = await partTask;
            List<BaoYangProductPriorityModel> priority = await priorityTask;

            if (accessoriesTask != null)
            {
                accessories = await accessoriesTask;
            }

            if (packageRefTask != null)
            {
                packageRef = await packageRefTask;
            }

            var userAttentionPid = await userAttentionPidTask;
            BaoYangRecommender recommender = new BaoYangRecommender(this, packageConfig,
                new VehicleAdaptationResultModel
                {
                    PackageRef = packageRef,
                    AccessoryTypeDic = accessories,
                    PartTypeDic = baoYangParts,
                    ProductPriority = priority
                }, userId, provinceId, cityId, channel, userAttentionPid, vehicle, targetProduct, jiYouAdaption,
                shopId);
            var packageTask =
                await Task.WhenAll(packageConfig.Packages.Select(_ =>
                    recommender.GetBaoYangPackageAsync(_, selectType, showAll)));
            if (targetProduct != null && targetProduct.Any())
            {
                foreach (var itemProduct in targetProduct)
                {
                    var packageItem = packageTask.FirstOrDefault(_ => _.PackageType == itemProduct.PackageType);
                    if (packageItem != null)
                    {
                        var baoYangItem = packageItem.Items ?? new List<BaoYangPackageItemModel>();
                        var defaultBaoYang = baoYangItem.FirstOrDefault(x => x.BaoYangType == itemProduct.BaoYangType);
                        if (defaultBaoYang == null)
                        {
                            return new ApiResult<List<BaoYangPackageModel>>()
                            {
                                Message = "车型和活动套餐不匹配，请查看其他套餐",
                                Code = ResultCode.Success
                            };
                        }

                        var pids = defaultBaoYang.Products?.Select(x => x.Pid) ?? new List<string>();
                        if (!pids.Contains(itemProduct.Pid))
                        {
                            return new ApiResult<List<BaoYangPackageModel>>()
                            {
                                Message = "车型和活动套餐不匹配，请查看其他套餐",
                                Code = ResultCode.Success
                            };
                        }
                    }
                }
            }

            return Result.Success(packageTask.ToList());
        }

        /// <summary>
        /// 辅料配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        public async Task<List<BaoYangAccessoryModel>> GetAccessoryByTid(string tid, string baoYangType = "")
        {
            List<BaoYangAccessoryModel> result = new List<BaoYangAccessoryModel>();
            var accessoriesTask = GetPartAccessoryByTidCache(tid);
            var accessoryRefTask = GetParAccessoryMapCache();
            await Task.WhenAll(accessoriesTask, accessoryRefTask);
            var accessories = accessoriesTask.Result?.ToList() ?? new List<BaoYangPartAccessoryDO>();
            var accessoryRef = accessoryRefTask.Result?.ToList() ?? new List<BaoYangPartAccessoryMapDO>();
            if (!string.IsNullOrEmpty(baoYangType))
            {
                accessoryRef = accessoryRef.Where(_ => _.BaoYangType == baoYangType).ToList();
                accessories = accessories
                    .Where(_ => accessoryRef.Select(t => t.AccessoryNames).Contains(_.AccessoryName)).ToList();
            }
            result = (from t in accessories
                join s in accessoryRef on t.AccessoryName equals s.AccessoryNames into temp
                from tt in temp.DefaultIfEmpty()
                select new BaoYangAccessoryModel
                {
                    Tid = t.Tid,
                    AccessoryName = t.AccessoryName,
                    Volume = t.Volume,
                    Level = t.Level,
                    Quantity = t.Quantity,
                    Size = t.Size,
                    Interface = t.Interface,
                    Description = t.Description,
                    Viscosity = t.Viscosity,
                    Grade = t.Grade,
                    BaoYangType = tt?.BaoYangType
                }).ToList();
            return result;
        }

        /// <summary>
        /// 获取套餐适配数据
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageRefModel>> GetBaoYangPackageRef(string tid)
        {
            List<BaoYangPackageRefModel> result = new List<BaoYangPackageRefModel>();
            var packageRef = (await _baoYangPackageRefRepository.GetBaoYangPackageRefByTidAsync(tid))?.ToList();
            if (packageRef != null && packageRef.Any())
            {
                result = packageRef.GroupBy(_ => _.BaoYangType).Select(_ => new BaoYangPackageRefModel
                {
                    BaoYangType = _.Key,
                    Tid = tid,
                    Pids = _.Select(x => x.PackageId).ToList()
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPartModel>> GetBaoYangPartsByTid(string tid, List<string> baoYangType = null)
        {
            List<BaoYangPartModel> result = new List<BaoYangPartModel>();
            var partsMap = (await GetBaoYangPartsMapConfigCache())?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
            if (baoYangType != null && baoYangType.Any())
            {
                partsMap = partsMap.Where(_ => baoYangType.Contains(_.BaoYangType)).ToList();
            }
            if (partsMap.Any())
            {
                var partCodeRef = partsMap.Where(_ => _.RefType == PartProductRefType.PartCode.ToString()).ToList();
                var partNoRef = partsMap.Where(_ => _.RefType == PartProductRefType.PartNo.ToString()).ToList();
                var productIdRef = partsMap.Where(_ => _.RefType == PartProductRefType.ProductId.ToString()).ToList();
                List<string> codePartNames = new List<string>();
                List<string> partNoPartNames = new List<string>();
                List<string> pidPartNames = new List<string>();
                partCodeRef.ForEach(_ =>
                {
                    codePartNames.AddRange(_.PartNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList());
                });
                partNoRef.ForEach(_ =>
                {
                    partNoPartNames.AddRange(_.PartNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList());
                });
                productIdRef.ForEach(_ =>
                {
                    pidPartNames.AddRange(_.PartNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList());
                });
                var codeProductsTask = _baoYangPartsRepository.GetBaoYangPartProductsAsync(tid, codePartNames);
                var baoYangPartsTask = GetBaoYangPartsByTidCache(tid);
                var propertyAdaptationTask = GetPropertyAdaptationByTidCache(tid);//五级属性
                await Task.WhenAll(codeProductsTask, baoYangPartsTask, propertyAdaptationTask);
                var codeProducts = codeProductsTask.Result?.ToList() ?? new List<BaoYangPartProductDO>();
                var baoYangParts = baoYangPartsTask.Result?.ToList() ?? new List<BaoYangPartsDO>();
                var propertyAdaptation =
                    propertyAdaptationTask.Result?.ToList() ?? new List<BaoYangPropertyAdaptationDO>();
                var partNoParts = baoYangParts.Where(_ => partNoPartNames.Contains(_.PartName)).ToList();
                var partNoList = partNoParts.Select(_ => _.PartCode).Distinct().ToList();
                var partNoProducts = await GetProductByPartNoAsync(partNoList);
                var pidParts = baoYangParts.Where(_ => pidPartNames.Contains(_.PartName)).ToList();
                partCodeRef.ForEach(_ =>
                {
                    List<string> partName =
                        _.PartNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var partProduct = codeProducts.Where(t => partName.Contains(t.PartName));
                    var property = propertyAdaptation.Where(x => x.PartNameAbbr == _.BaoYangType);
                    BaoYangPartModel itemPart = new BaoYangPartModel
                    {
                        Tid = tid,
                        BaoYangType = _.BaoYangType,
                        Properties = property.Select(x => new PropertyAdaptive
                        {
                            Property = x.Property,
                            PropertyValue = x.PropertyValue,
                            ImageUrl = x.ImageUrl,
                            Products = partProduct.Where(f => f.OePartCode == x.OePartCode)
                                .Select(t => new PartProduct {Pid = t.Pid, Brand = t.Brand}).ToList()
                        }).ToList(),
                        Pids = partProduct.Select(t => new PartProduct {Pid = t.Pid, Brand = t.Brand}).ToList()
                    };
                    result.Add(itemPart);
                });
                partNoRef.ForEach(_ =>
                {
                    List<string> partName =
                        _.PartNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var partCodeList = partNoParts.Where(t => partName.Contains(t.PartName)).Select(t => t.PartCode)
                        .Distinct().ToList();
                    var partProduct = partNoProducts.Where(t => partCodeList.Contains(t.PartNo));
                    var property = propertyAdaptation.Where(x => x.PartNameAbbr == _.BaoYangType);
                    BaoYangPartModel itemPart = new BaoYangPartModel
                    {
                        Tid = tid,
                        BaoYangType = _.BaoYangType,
                        Properties = property.Select(x => new PropertyAdaptive
                        {
                            Property = x.Property,
                            PropertyValue = x.PropertyValue,
                            ImageUrl = x.ImageUrl,
                            Products = partProduct.Where(f => f.PartNo == x.PartCode)
                                .Select(t => new PartProduct { Pid = t.Pid, Brand = t.Brand }).ToList()
                        }).ToList(),
                        Pids = partProduct.Select(t => new PartProduct
                        {
                            Pid = t.Pid,
                            Brand = t.Brand
                        }).ToList()
                    };
                    result.Add(itemPart);

                });
                productIdRef.ForEach(_ =>
                {
                    List<string> partName =
                        _.PartNames.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var partProduct = pidParts.Where(t => partName.Contains(t.PartName));
                    var property = propertyAdaptation.Where(x => x.PartNameAbbr == _.BaoYangType);
                    BaoYangPartModel itemPart = new BaoYangPartModel
                    {
                        Tid = tid,
                        BaoYangType = _.BaoYangType,
                        Properties = property.Select(x => new PropertyAdaptive
                        {
                            Property = x.Property,
                            PropertyValue = x.PropertyValue,
                            ImageUrl = x.ImageUrl,
                            Products = partProduct.Where(f => f.PartCode == x.PartCode)
                                .Select(t => new PartProduct {Pid = t.PartCode, Brand = t.Brand}).ToList()
                        }).ToList(),
                        Pids = partProduct.Select(t => new PartProduct {Pid = t.PartCode, Brand = t.Brand}).ToList()
                    };
                    result.Add(itemPart);
                });
            }

            return result;
        }

        /// <summary>
        /// GetProductPrioritySettingCache
        /// </summary>
        /// <returns></returns>
        private async Task<List<BaoYangProductPriorityModel>> GetProductPrioritySettingCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":ProductPrioritySetting:{timestamp}";
            return await _redisClient.GetOrSetAsync(key, () => GetProductPrioritySetting(), new TimeSpan(0, 0, 10, 0));
        }

        /// <summary>
        /// GetProductPrioritySetting
        /// </summary>
        /// <returns></returns>
        private async Task<List<BaoYangProductPriorityModel>> GetProductPrioritySetting()
        {
            var result = await _baoYangPrioritySettingRepository.GetListAsync("");

            return result.Select(_ => new BaoYangProductPriorityModel
            {
                BaoYangType = _.BaoYangType,
                PriorityField = _.PriorityField,
                PriorityValue = _.PriorityValue,
                Priority = _.Priority
            }).ToList();
        }

        /// <summary>
        /// 根据tid查询适配配件数据
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        private async Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidCache(string tid)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":BaoYangPartsByTid:{tid}:{timestamp}";
            var baoYangPart = await _redisClient.GetOrSetAsync(key,
                () => _baoYangPartsRepository.GetBaoYangPartsByTidAsync(tid), new TimeSpan(0, 2, 0, 0));

            return baoYangPart;
        }

        /// <summary>
        /// 保养五级属性描述配置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPropertyDescriptionDO>> GetBaoYangPropertyDescriptionCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":BaoYangPropertyDescription:{timestamp}";
            var baoYangPart = await _redisClient.GetOrSetAsync(key,
                () => _baoYangPropertyDescriptionRepository.GetBaoYangPropertyDescription(), new TimeSpan(0, 8, 0, 0));

            return baoYangPart;
        }

        /// <summary>
        /// 五级属性适配
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        private async Task<IEnumerable<BaoYangPropertyAdaptationDO>> GetPropertyAdaptationByTidCache(string tid)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PropertyAdaptationByTid:{tid}:{timestamp}";
            var baoYangPart = await _redisClient.GetOrSetAsync(key,
                () => _baoYangPropertyAdaptationRepository.GetPropertyAdaptationByTid(tid), new TimeSpan(0, 2, 0, 0));

            return baoYangPart;
        }

        /// <summary>
        /// 根据Tid查询辅料适配数据
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        private async Task<IEnumerable<BaoYangPartAccessoryDO>> GetPartAccessoryByTidCache(string tid)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PartAccessoryByTid:{tid}:{timestamp}";
            var baoYangPart = await _redisClient.GetOrSetAsync(key,
                () => _baoYangPartAccessoryRepository.GetPartAccessoryByTidAsync(tid), new TimeSpan(0, 2, 0, 0));

            return baoYangPart;
        }


        /// <summary>
        /// 根据Tid查询辅料适配数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <returns></returns>
        public async Task<BaoYangPartAccessoryVo> GetPartAccessoryByTidItem(string tid, string accessoryName)
        {
            var result = (await GetPartAccessoryByTidCache(tid))?.ToList() ?? new List<BaoYangPartAccessoryDO>();

            var defaultItem = result.FirstOrDefault(x => x.AccessoryName == accessoryName);

            if (defaultItem != null)
            {
                return new BaoYangPartAccessoryVo
                {
                    Tid = defaultItem.Tid,
                    AccessoryName = defaultItem.AccessoryName,
                    Volume = defaultItem.Volume,
                    Level = defaultItem.Level,
                    Quantity = defaultItem.Quantity,
                    Size = defaultItem.Size,
                    Interface = defaultItem.Interface,
                    Description = defaultItem.Description,
                    Viscosity = defaultItem.Viscosity,
                    Grade = defaultItem.Grade
                };
            }
            return null;
        }

        /// <summary>
        /// 辅料关联baoYangType Map
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<BaoYangPartAccessoryMapDO>> GetParAccessoryMapCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":ParAccessoryMap:{timestamp}";
            var baoYangPart = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _baoYangPartAccessoryMapRepository.GetParAccessoryMapAsync(),
                new TimeSpan(0, 2, 0, 0));

            return baoYangPart;
        }

        /// <summary>
        /// 获取默认套餐
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="userId"></param>
        /// <param name="channel"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="partDic"></param>
        /// <param name="priority"></param>
        /// <param name="vehicle"></param>
        /// <param name="targetPid"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>>
            GetAdaptationDefaultProductAsync(List<string> pidList, string userId, string channel, string provinceId,
                string cityId, List<BaoYangPartModel> partDic, List<BaoYangProductPriorityModel> priority,
                VehicleRequest vehicle, string targetPid, int shopId)
        {
            return await GetPackageProductsAsync(pidList, priority, userId, channel, provinceId, cityId, partDic,
                vehicle, 1,
                targetPid, shopId);
        }

        /// <summary>
        /// 根据粘度查机油产品
        /// </summary>
        /// <param name="viscosity"></param>
        /// <returns></returns>
        public async Task<List<BaoYangProductModel>> GetOilProductsByViscosity(string viscosity)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 根据类目查产品
        /// </summary>
        /// <param name="category"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<BaoYangProductModel>> GetProductByCategoryAsync(string category, int shopId)
        {
            int categoryId = 0;

            if (!Int32.TryParse(category, out categoryId))
            {
                return new List<BaoYangProductModel>();
            }

            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            var products = await _productClient.SelectProductsByCategoryCache(categoryId);
            if (products != null && products.Any())
            {

                List<string> onLineService = new List<string>();
                if (shopId > 0)
                {
                    onLineService = await GetAllOnLineServiceCache(shopId);
                }


                products.ForEach(_ =>
                {
                    if (shopId > 0 && _.InstallationServices != null && _.InstallationServices.Any())
                    {
                        if (_.InstallationServices.All(t => onLineService.Contains(t.ServiceId)))
                        {
                            result.Add(ConvertProduct(_));
                        }
                    }
                    else
                    {
                        result.Add(ConvertProduct(_));
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// 根据pid查询商品信息
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<BaoYangProductModel>> GetProductByPidListAsync(List<string> pidList, int shopId)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            var products = await _productClient.GetProductsByProductCodes(pidList);
            if (products != null && products.Any())
            {
                List<string> onLineService = new List<string>();
                if (shopId > 0)
                {
                    onLineService = await GetAllOnLineServiceCache(shopId);
                }

                products.ForEach(_ =>
                {
                    if (shopId > 0 && _.InstallationServices != null && _.InstallationServices.Any())
                    {
                        if (_.InstallationServices.All(t => onLineService.Contains(t.ServiceId)))
                        {
                            result.Add(ConvertProduct(_));
                        }
                    }
                    else
                    {
                        result.Add(ConvertProduct(_));
                    }
                });
            }

            return result;
        }

        private List<List<string>> PageHandler(List<string> pidList, int pageSize)
        {
            List<List<string>> result = new List<List<string>>();
            int totalSize = pidList.Count / pageSize;
            int t = pidList.Count % pageSize;
            if (t > 0)
            {
                totalSize = totalSize + 1;
            }

            for (int i = 0; i < totalSize; i++)
            {
                var item = pidList.Skip(i * pageSize).Take(pageSize).ToList();
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 根据partNo查商品信息
        /// </summary>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public async Task<List<BaoYangProductModel>> GetProductByPartNoAsync(List<string> partNo)
        {
            if (partNo == null || !partNo.Any())
            {
                return new List<BaoYangProductModel>();
            }
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            var pageResult = PageHandler(partNo, 100);
            var allTask = await Task.WhenAll(pageResult.Select(_ => _productClient.SelectProductsByPartNos(
                new SelectProductsByPartNosRequest
                {
                    PartNos = _,
                    IsOnSale = 1,
                    HasAttribute = true
                })));

            foreach (var itemTask in allTask)
            {
                itemTask.ForEach(_ => { result.Add(ConvertProduct(_)); });
            }

            return result;
        }

        /// <summary>
        /// 填充车型信息
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private async Task<VehicleRequest> FillVehicleInfoAsync(VehicleRequest vehicle)
        {
            if (vehicle != null)
            {
                var vehicles = await _vehicleClient.GetVehicleBaseInfoListAsync(new VehicleInfoListClientRequest
                {
                    VehicleId = vehicle.VehicleId,
                    PaiLiang = vehicle.PaiLiang,
                    Nian = vehicle.Nian,
                    Tid = vehicle.Tid
                });
                if (vehicles != null && vehicles.Any())
                {
                    var current = vehicles[0];
                    if (!string.IsNullOrEmpty(vehicle.Tid))
                    {
                        vehicle.Brand = current.Brand;
                        vehicle.FuelType = current.FuelType;
                        vehicle.AvgPrice = current.AvgPrice;
                        vehicle.GuidePrice = current.GuidePrice;
                    }
                    else
                    {
                        vehicle.Tid = current.Tid;
                        vehicle.Brand = current.Brand;
                        vehicle.FuelType = current.FuelType;
                        vehicle.AvgPrice = current.AvgPrice;
                        vehicle.GuidePrice = current.GuidePrice;
                    }
                }
                else
                {
                    throw new CustomException("此车型不存在");
                }
            }

            return vehicle;
        }

        /// <summary>
        /// 根据tid查询车型配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<VehicleConfigModel> GetVehicleConfigByTid(string tid)
        {
            var result = await _vehicleClient.GetVehicleConfigByTidList(new VehicleConfigClientRequest
            {
                TidList = new List<string>
                {
                    tid
                }
            });
            if (result != null && result.Any())
            {
                return _mapper.Map<VehicleConfigModel>(result[0]);
            }

            return null;
        }

        /// <summary>
        /// 获取防冻液冰点配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<AntifreezeSettingModel>> GetAntifreezeSetting()
        {
            var result = (await _baoYangAntifreezeSettingRepository.GetBaoYangAntifreezeSettingAsync())?.ToList() ??
                         new List<BaoYangAntifreezeSettingDO>();
            if (result.Any())
            {
                return result.Select(_ => new AntifreezeSettingModel
                {
                    FreezingPoint = _.FreezingPoint.ToString(),
                    Brand = _.Brand,
                    ProvinceIds =
                        _.ProvinceIds?.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                        new List<string>()
                }).ToList();
            }

            return new List<AntifreezeSettingModel>();
        }

        /// <summary>
        /// 机油类别过滤packageType
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="package"></param>
        public async Task VehicleFuelTypeFilter(VehicleRequest vehicle, List<BaoYangPackageDescription> package)
        {
            if (!string.IsNullOrEmpty(vehicle?.FuelType))
            {
                var vehicleFuelType =
                    (await _vehicleFuelTypeConfigRepository.GetVehicleFuelTypeConfigAsync())?.ToList() ??
                    new List<VehicleFuelTypeConfigDO>();
                var currentConfig = vehicleFuelType.FirstOrDefault(_ => _.FuelType == vehicle.FuelType)
                    ?.NotSupportedTypes;
                if (!string.IsNullOrEmpty(currentConfig))
                {
                    var notSupport = currentConfig.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    package.RemoveAll(_ => notSupport.Contains(_.PackageType));
                }
            }
        }

        /// <summary>
        /// 门店上下架服务过滤PackageType
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        public async Task ShopServeFilter(int shopId, List<BaoYangPackageDescription> package)
        {
            var onSaleService = await GetAllOnLineServiceCache(shopId);

            package.RemoveAll(_ => _.ServiceIds.Any() && !_.ServiceIds.Exists(t => onSaleService.Contains(t)));
        }

        /// <summary>
        /// 获取已上架的服务
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        private async Task<List<string>> GetAllOnLineServiceCache(long shopId)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");

            var key = redisKey + $":AllOnLineService:{shopId}:{timestamp}";

            var service = await _redisClient.GetOrSetAsync(key,
                () => _shopManageClient.GetAllOnLineServicesByShopId(new GetAllOnLineServicesByShopIdRequest()
                    {ShopId = shopId}),
                new TimeSpan(0, 0, 0, 30));

            return service?.Select(_ => _.Pid)?.ToList() ?? new List<string>();
        }

        /// <summary>
        /// Model转换
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private BaoYangPackageProductModel ConvertProduct(BaoYangProductModel product, int count)
        {
            if (product != null)
            {
                var result = new BaoYangPackageProductModel
                {
                    Count = count,
                    Pid = product.Pid,
                    DisplayName = product.Name,
                    Description = product.DisplayName,
                    Image = ImageHelper.AddImageDomain(product.Image),
                    Price = product.Price,
                    MarketPrice = product.MarketPrice,
                    Unit = product.Unit,
                    FeedbackRate = product.FavorableRate,
                    StockOut = product.StockOut, // || product.AvailableStockQuantity < count,
                    IsOriginal = product.IsOriginal
                };

                if (!string.IsNullOrEmpty(product.OilGrade))
                {
                    if (product.OilGrade.StartsWith("全合成"))
                    {
                        result.Tags.Add(new TagInfo()
                        {
                            Type = "FullSynthetic",
                            Tag = "全合成",
                            TagColor = "#e4b872"
                        });
                    }
                    else if (product.OilGrade.StartsWith("半合成"))
                    {
                        result.Tags.Add(new TagInfo()
                        {
                            Type = "SemiSynthetic",
                            Tag = "半合成",
                            TagColor = "#8d97f7"
                        });

                    }
                    else if (product.OilGrade.StartsWith("矿物"))
                    {
                        result.Tags.Add(new TagInfo()
                        {
                            Type = "Mineral",
                            Tag = "矿物质",
                            TagColor = "#f37c3e"
                        });
                    }
                }

                if (!string.IsNullOrEmpty(product.Viscosity))
                {
                    result.Tags.Add(new TagInfo()
                    {
                        Type = "OilUnit",
                        Tag = product.Viscosity,
                        TagColor = "#8d97f7"
                    });
                }

                return result;
            }

            return null;
        }

        private BaoYangProductModel ConvertProduct(ProductDetailDTO product)
        {
            if (product != null)
            {
                return new BaoYangProductModel
                {
                    Pid = product.Product.ProductCode,
                    DisplayName = product.Product.DisplayName,
                    Image = product.Product.Image1,
                    Name = product.Product.Name,
                    Price = product.Product.SalesPrice,
                    MarketPrice = product.Product.StandardPrice,
                    StockOut = product.Product.Stockout,
                    Brand = product.Product.Brand,
                    FavorableRate = product.Product.FavorableRate,
                    //Description = product.Product.Description,
                    Viscosity = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "Viscosity")
                        ?.AttributeValue,
                    Level = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilLevel")?.AttributeValue,
                    OilSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilSeries")
                        ?.AttributeValue, //级别系列HX5
                    Specification = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilDescription")
                        ?.AttributeValue, //机油等级SN
                    OilGrade = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "BaseOilGrade")
                        ?.AttributeValue, //机油等级：全合成  半合成  矿物油
                    Unit = product.Product.Unit,
                    AntifreezePoint = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "AntifreezePoint")
                        ?.AttributeValue,
                    Color = product.Product.Color,
                    BoilingPoint = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "BoilingPoint")
                        ?.AttributeValue,
                    PartNo = product.Product.PartNo,
                    WiperSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "WiperSeries")
                        ?.AttributeValue,
                    JzqSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "JzqSeries")
                        ?.AttributeValue,
                    ScyLevel = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "ScyLevel")
                        ?.AttributeValue,
                    SecondCategoryId = product.Product.SecondCategoryId
                };
            }

            return null;
        }

        private BaoYangProductModel ConvertProduct(ProductBaseInfoDTO product)
        {
            if (product != null)
            {
                return new BaoYangProductModel
                {
                    Pid = product.ProductCode,
                    DisplayName = product.DisplayName,
                    Image = product.Image1,
                    Price = product.SalesPrice,
                    MarketPrice = product.StandardPrice,
                    StockOut = product.Stockout,
                    Brand = product.Brand,
                    Name = product.Name,
                    FavorableRate = product.FavorableRate,
                    Viscosity = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "Viscosity")
                        ?.AttributeValue,
                    Level = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilLevel")
                        ?.AttributeValue, //级别系列HX5
                    OilSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilSeries")
                        ?.AttributeValue, //品牌系列： 美孚一号
                    Specification = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "OilDescription")
                        ?.AttributeValue, //机油等级SN
                    OilGrade = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "BaseOilGrade")
                        ?.AttributeValue, //机油等级：全合成  半合成  矿物油
                    Unit = product.Unit,
                    AntifreezePoint = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "AntifreezePoint")
                        ?.AttributeValue,
                    Color = product.Color,
                    BoilingPoint = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "BoilingPoint")
                        ?.AttributeValue,
                    PartNo = product.PartNo,
                    WiperSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "WiperSeries")
                        ?.AttributeValue,
                    JzqSeries = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "JzqSeries")
                        ?.AttributeValue,
                    ScyLevel = product.Attributevalues.FirstOrDefault(_ => _.AttributeName == "ScyLevel")
                        ?.AttributeValue
                };
            }

            return null;
        }

        /// <summary>
        /// 获取用户购买过商品pid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetUserPurchase(string userId)
        {
            var key = redisKey + $":GetUserPurchase:{userId}";
            var pidList = await RedisHelper.GetOrSetAsync(_redisClient, key, () => _orderClient.GetPidsByUserId(userId),
                new TimeSpan(0, 0, 10, 0));
            return pidList;
        }

        private async Task<IEnumerable<BaoYangTypeConfigDO>> GetBaoYangTypeConfigCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":BaoYangTypeConfig:{timestamp}";
            var pidList = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _baoYangTypeConfigRepository.GetBaoYangTypeConfigAsync(),
                new TimeSpan(0, 2, 0, 0));
            return pidList;
        }

        /// <summary>
        /// 获取BaoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigVo>> GetBaoYangTypeConfig()
        {
            var result = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();

            return result.Select(_ => new BaoYangTypeConfigVo
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName,
                ImageUrl = _.ImageUrl
            }).ToList();
        }

        public async Task<IEnumerable<BaoYangPackageMapConfigDO>> GetPackageMapConfigCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PackageMapConfig:{timestamp}";
            var pidList = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _baoYangPackageMapConfigRepository.GetPackageMapConfigAsync(),
                new TimeSpan(0, 2, 0, 0));
            return pidList;
        }

        private async Task<IEnumerable<BaoYangPartsMapConfigDO>> GetBaoYangPartsMapConfigCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":BaoYangPartsMapConfig:{timestamp}";
            var result = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _baoYangPartsMapConfigRepository.GetBaoYangPartsMapAsync(),
                new TimeSpan(0, 8, 0, 0));
            return result;
        }

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPartsResponse>> GetBaoYangParts()
        {
            var baoYangTypeMap =
                (await GetBaoYangPartsMapConfigCache())?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
            var baoYangTypes = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
            List<BaoYangPartsResponse> result = new List<BaoYangPartsResponse>();
            baoYangTypeMap.ForEach(t =>
            {
                result.Add(new BaoYangPartsResponse()
                {
                    BaoYangType = t.BaoYangType,
                    RefType = t.RefType,
                    DisplayName = baoYangTypes.FirstOrDefault(_ => _.BaoYangType == t.BaoYangType)?.DisplayName
                });
            });

            return result;
        }

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AdaptiveProductsResponse> VerifyAdaptiveProducts(
            VerifyAdaptiveProductsRequest request)
        {
            request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);

            var adaptivePid = await GetAdaptiveProducts(request.Vehicle.Tid, request.Products, 0);

            return new AdaptiveProductsResponse()
            {
                AdaptivePid = adaptivePid
            };
        }

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyAdaptiveProductForBuyResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyRequest request)
        {
            request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);
            var tid = request.Vehicle.Tid;

            var baoYangTypesTask = GetBaoYangTypeConfigCache(); //baoYangType
            var propertyDescriptionTask = GetBaoYangPropertyDescriptionCache();

            await Task.WhenAll(baoYangTypesTask, propertyDescriptionTask);

            var baoYangTypes = baoYangTypesTask.Result?.ToList() ?? new List<BaoYangTypeConfigDO>();
            var propertyDescription =
                propertyDescriptionTask.Result?.ToList() ?? new List<BaoYangPropertyDescriptionDO>();

            baoYangTypes = baoYangTypes.Where(_ =>
                _.ChildCategories.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Contains(request.CategoryId.ToString())).ToList();
            var partBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Part.ToString()).ToList();
            if (partBaoYang.Any())
            {
                var baoYangParts = await GetBaoYangPartsByTid(tid, partBaoYang.Select(_ => _.BaoYangType).ToList());
                var part1 = baoYangParts.FirstOrDefault(_ => _.Properties != null && _.Properties.Any());
                if (part1 != null)
                {
                    var propertyKey = part1.Properties[0].Property;
                    var defaultDescription = propertyDescription.FirstOrDefault(_ => _.Name == propertyKey);
                    var existProperty = request.Vehicle.Properties?.FirstOrDefault(_ => _.PropertyKey == propertyKey);
                    if (existProperty != null)
                    {
                        var products =
                            part1.Properties.FirstOrDefault(_ => _.PropertyValue == existProperty.PropertyValue)
                                ?.Products?.Select(_ => _.Pid) ?? new List<string>();
                        if (products.Contains(request.Pid))
                        {
                            return new VerifyAdaptiveProductForBuyResponse()
                            {
                                IsAdaptive = 1,
                                Tip = "此商品适配车型"
                            };
                        }
                        else
                        {
                            return new VerifyAdaptiveProductForBuyResponse()
                            {
                                IsAdaptive = 0,
                                Tip = "此商品不匹配车型"
                            };
                        }
                    }
                    else
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            Property = new PropertySelectModel()
                            {
                                Type = propertyKey == "款型" ? "TID" : "Property",
                                Name = propertyKey,
                                Title = defaultDescription?.Title ?? "",
                                Content = defaultDescription?.Content ?? "",
                                Values = part1.Properties.Select(_ => new PropertyResult
                                {
                                    Key = _.PropertyValue,
                                    DisplayValue = _.PropertyValue,
                                    ImageUrl = _.ImageUrl
                                }).ToList()
                            },
                            IsAdaptive = 2
                        };
                    }
                }
                else
                {
                    var exist = baoYangParts.SelectMany(_ => _.Pids).Select(_ => _.Pid).Contains(request.Pid);
                    if (exist)
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 1,
                            Tip = "此商品适配车型"
                        };
                    }
                    else
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 0,
                            Tip = "此商品不匹配车型"
                        };
                    }
                }
            }

            var maintainBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Maintainence.ToString())
                .ToList();
            if (maintainBaoYang.Any())
            {
                List<string> pidList = new List<string>();
                var taskList =
                    await Task.WhenAll(maintainBaoYang.Select(_ =>
                        GetProductByCategoryAsync(_.CategoryName, request.ShopId)));
                foreach (var itemTask in taskList)
                {
                    pidList.AddRange(itemTask.Select(_ => _.Pid));
                }

                if (pidList.Contains(request.Pid))
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 1,
                        Tip = "此商品适配车型"
                    };
                }
                else
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }

            var packageBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Package.ToString()).ToList();
            if (packageBaoYang.Any())
            {
                var packageId =
                    (await _baoYangPackageRefRepository.GetBaoYangPackageRefByParaAsync(tid,
                        packageBaoYang.Select(_ => _.BaoYangType).ToList()))?.ToList() ??
                    new List<BaoYangPackageRefDO>();
                if (packageId.Any())
                {
                    var packagePidList = packageId.Select(_ => _.PackageId).ToList();
                    var baoYangParts =await GetBaoYangPartsByTid(tid);
                    var adaptiveResult = await GetPackageAllProductsAsync(packagePidList,
                        new List<BaoYangProductPriorityModel>(), "", "", "", "", baoYangParts,
                        request.Vehicle, request.ShopId);
                    var property = adaptiveResult.Item1;
                    if (property != null)
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            Property = property,
                            IsAdaptive = 2
                        };
                    }

                    var products = adaptiveResult.Item2?.Select(_ => _.Pid) ?? new List<string>();
                    if (products.Contains(request.Pid))
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 1,
                            Tip = "此商品适配车型"
                        };
                    }
                    else
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 0,
                            Tip = "此商品不匹配车型"
                        };
                    }
                }
                else
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }

            var accessoryBaoYang =
                baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Accessory.ToString()).ToList();
            foreach (var itemAccessory in accessoryBaoYang)
            {
                var products = await GetProductByCategoryAsync(itemAccessory.CategoryName, request.ShopId);
                if (itemAccessory.BaoYangType == "jiyou")
                {
                    var accessories = await GetAccessoryByTid(tid, itemAccessory.BaoYangType);
                    if (accessories != null && accessories.Any())
                    {
                        OilModel oil = new OilModel(accessories.FirstOrDefault());
                        if (oil.IsValid())
                        {
                            var availViscosity = GetAvailViscosity(oil.Viscosity);
                            var availLevel = GetAvailLevel(oil.Level);
                            var availDescription = GetAvailDescription(oil.Description);
                            products = products.Where(_ =>
                                availViscosity.Contains(_.Viscosity) && availLevel.Contains(_.Level) &&
                                ValidDescription(_.Specification, availDescription)).ToList();
                            if (products.Exists(_ => _.Pid == request.Pid))
                            {
                                return new VerifyAdaptiveProductForBuyResponse()
                                {
                                    IsAdaptive = 1,
                                    Tip = "此商品适配车型"
                                };
                            }
                            else
                            {
                                return new VerifyAdaptiveProductForBuyResponse()
                                {
                                    IsAdaptive = 0,
                                    Tip = "此商品不匹配车型"
                                };
                            }
                        }
                    }
                }
                else if (itemAccessory.BaoYangType == "scy")
                {
                    var accessories = await GetAccessoryByTid(tid, itemAccessory.BaoYangType);
                    if (accessories != null && accessories.Any())
                    {
                        BrakeFluidModel scyModel = new BrakeFluidModel(accessories.FirstOrDefault());
                        if (scyModel.IsValid())
                        {
                            products = FilterScyLevel(products, scyModel.Level);
                            if (products.Exists(_ => _.Pid == request.Pid))
                            {
                                return new VerifyAdaptiveProductForBuyResponse()
                                {
                                    IsAdaptive = 1,
                                    Tip = "此商品适配车型"
                                };
                            }
                            else
                            {
                                return new VerifyAdaptiveProductForBuyResponse()
                                {
                                    IsAdaptive = 0,
                                    Tip = "此商品不匹配车型"
                                };
                            }
                        }
                    }
                }
                else
                {
                    if (products.Exists(_ => _.Pid == request.Pid))
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 1,
                            Tip = "此商品适配车型"
                        };
                    }
                    else
                    {
                        return new VerifyAdaptiveProductForBuyResponse()
                        {
                            IsAdaptive = 0,
                            Tip = "此商品不匹配车型"
                        };
                    }
                }
            }

            return new VerifyAdaptiveProductForBuyResponse()
            {
                IsAdaptive = 0,
                Tip = "此商品不匹配车型"
            };
        }

        /// <summary>
        /// 获取适配的商品
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="productRequest"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetAdaptiveProducts(string tid, List<ProductRequest> productRequest, int shopId)
        {
            List<string> adaptivePid = new List<string>();

            List<string> categoryId = productRequest.Select(_ => _.CategoryId).Distinct().ToList();

            var baoYangTypes = (await GetBaoYangTypeConfigCache())?.ToList() ??
                               new List<BaoYangTypeConfigDO>(); //baoYangType

            baoYangTypes = baoYangTypes.Where(_ =>
                    _.ChildCategories.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Any(t => categoryId.Contains(t)))
                .ToList();
            var partBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Part.ToString()).ToList();
            if (partBaoYang.Any())
            {
                var baoYangParts = await GetBaoYangPartsByTid(tid, partBaoYang.Select(_ => _.BaoYangType).ToList());
                baoYangParts.ForEach(_ => { adaptivePid.AddRange(_.Pids.Select(x => x.Pid)); });
            }

            var maintainBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Maintainence.ToString())
                .ToList();
            if (maintainBaoYang.Any())
            {
                var taskList =
                    await Task.WhenAll(maintainBaoYang.Select(_ => GetProductByCategoryAsync(_.CategoryName, shopId)));
                foreach (var itemTask in taskList)
                {
                    adaptivePid.AddRange(itemTask.Select(_ => _.Pid));
                }
            }

            var packageBaoYang = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Package.ToString()).ToList();
            if (packageBaoYang.Any())
            {
                var packageId =
                    (await _baoYangPackageRefRepository.GetBaoYangPackageRefByParaAsync(tid,
                        packageBaoYang.Select(_ => _.BaoYangType).ToList()))?.ToList() ??
                    new List<BaoYangPackageRefDO>();
                adaptivePid.AddRange(packageId.Select(_ => _.PackageId));
            }

            var accessoryBaoYang =
                baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Accessory.ToString()).ToList();
            foreach (var itemAccessory in accessoryBaoYang)
            {
                var products = await GetProductByCategoryAsync(itemAccessory.CategoryName, shopId);
                if (itemAccessory.BaoYangType == "jiyou")
                {
                    var accessories = await GetAccessoryByTid(tid, itemAccessory.BaoYangType);
                    if (accessories != null && accessories.Any())
                    {
                        OilModel oil = new OilModel(accessories.FirstOrDefault());
                        if (oil.IsValid())
                        {
                            var availViscosity = GetAvailViscosity(oil.Viscosity);
                            var availLevel = GetAvailLevel(oil.Level);
                            var availDescription = GetAvailDescription(oil.Description);
                            products = products.Where(_ =>
                                availViscosity.Contains(_.Viscosity) && availLevel.Contains(_.Level) &&
                                ValidDescription(_.Specification, availDescription)).ToList();
                            adaptivePid.AddRange(products.Select(_ => _.Pid));
                        }
                    }
                }
                else if (itemAccessory.BaoYangType == "scy")
                {
                    var accessories = await GetAccessoryByTid(tid, itemAccessory.BaoYangType);
                    if (accessories != null && accessories.Any())
                    {
                        BrakeFluidModel scyModel = new BrakeFluidModel(accessories.FirstOrDefault());
                        if (scyModel.IsValid())
                        {
                            products = FilterScyLevel(products, scyModel.Level);
                            adaptivePid.AddRange(products.Select(_ => _.Pid));
                        }
                    }
                }
                else
                {
                    adaptivePid.AddRange(products.Select(_ => _.Pid));
                }
            }

            List<string> currentPid = productRequest.Select(_ => _.Pid).Distinct().ToList();
            List<string> response = currentPid.Where(_ => adaptivePid.Contains(_)).ToList();
            return response;
        }

        /// <summary>
        /// 根据配件类型适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PartProductRefVo>> GetAdaptiveProductByPartType(
            AdaptiveProductByPartTypeRequest request)
        {
            List<PartProductRefVo> result = new List<PartProductRefVo>();
            if (request.BaoYangPart == null || !request.BaoYangPart.Any())
            {
                return result;
            }

            request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);

            result = await GetAdaptiveProductByPartType(request.BaoYangPart, request.Vehicle.Tid, request.ProvinceId,
                request.CityId, request.Vehicle);

            return result;
        }

        /// <summary>
        /// 根据CarId 和 配件类型 适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PartProductRefVo>> GetAdaptiveProductByPartTypeAndCarId(
            AdaptiveProductByPartTypeAndCarIdRequest request)
        {
            List<PartProductRefVo> result = new List<PartProductRefVo>();
            if (request.BaoYangPart == null || !request.BaoYangPart.Any())
            {
                return result;
            }

            var vehicleData = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest()
            {
                UserId = request.UserId,
                CarId = request.CarId
            });

            if (vehicleData == null)
            {
                throw new CustomException("用户车型不存在");
            }

            VehicleRequest vehicle = new VehicleRequest
            {
                VehicleId = vehicleData.VehicleId,
                PaiLiang = vehicleData.PaiLiang,
                Nian = vehicleData.Nian,
                Tid = vehicleData.Tid,
                Properties =
                    vehicleData.CarProperties?.Select(_ => new VehicleProperty
                        {PropertyKey = _.PropertyKey, PropertyValue = _.PropertyValue})?.ToList() ??
                    new List<VehicleProperty>(),
                Distance = vehicleData.TotalMileage,
                OnRoadTime = (!string.IsNullOrEmpty(vehicleData.BuyYear) && !string.IsNullOrEmpty(vehicleData.BuyMonth))
                    ? $"{vehicleData.BuyYear}-{vehicleData.BuyMonth}"
                    : String.Empty
            };

            vehicle = await FillVehicleInfoAsync(vehicle);

            result = await GetAdaptiveProductByPartType(request.BaoYangPart, vehicle.Tid, request.ProvinceId,
                request.CityId, vehicle);

            return result;
        }

        /// <summary>
        /// 根据类型适配数据
        /// </summary>
        /// <param name="baoYangPart"></param>
        /// <param name="tid"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public async Task<List<PartProductRefVo>> GetAdaptiveProductByPartType(List<BaoYangPartRequest> baoYangPart,
            string tid, string provinceId, string cityId, VehicleRequest vehicle = null)
        {
            string commonJvPid = _configuration["SwitchConfig:CommonJvPid"];
            baoYangPart = baoYangPart.Distinct(_ => $"{_.PartType}-{string.Join(",", _.BrandList)}").ToList();
            List<PartProductRefVo> result = new List<PartProductRefVo>();

            var baoYangTypes = (await GetBaoYangTypeConfigCache())?.ToList() ??
                               new List<BaoYangTypeConfigDO>(); //baoYangType
            var priority = await GetProductPrioritySettingCache();
            var allBaoYangType = baoYangTypes.Where(_ => _.TypeGroup == BaoYangTypeGroup.Part.ToString())
                .Select(_ => _.BaoYangType);
            baoYangPart = baoYangPart.Where(_ => allBaoYangType.Contains(_.PartType)).ToList();
            if (baoYangPart.Any())
            {
                var baoYangParts = await GetBaoYangPartsByTid(tid, baoYangPart.Select(_ => _.PartType).ToList());
                foreach (var _ in baoYangPart)
                {
                    PartProductRefVo itemPart = new PartProductRefVo()
                    {
                        PartType = _.PartType,
                        BrandList = _.BrandList
                    };
                    var defaultBaoYangItem = baoYangParts.FirstOrDefault(t => t.BaoYangType == _.PartType);
                    var itemPid = defaultBaoYangItem?.Pids ?? new List<PartProduct>();
                    var property = defaultBaoYangItem?.Properties ?? new List<PropertyAdaptive>();
                    if (itemPid.Any())
                    {
                        var brandList = _.BrandList ?? new List<string>();
                        if (brandList.Any())
                        {
                            itemPid = itemPid.Where(t => brandList.Contains(t.Brand)).ToList();
                        }

                        itemPid = PropertyAdaptation(itemPid, vehicle, property, brandList,
                            new List<BaoYangPropertyDescriptionDO>()).Item2; //五级属性适配
                        int count = baoYangParts.FirstOrDefault(t => t.BaoYangType == _.PartType)?.Count ?? 1;
                        var product = await GetPartProduct(itemPid.Select(x => x.Pid).Distinct().ToList(), count,
                            priority.Where(t => t.BaoYangType == _.PartType).ToList(),
                            provinceId, cityId, 0, 1);
                        string pid = product.FirstOrDefault()?.Pid;
                        if (string.IsNullOrEmpty(pid) && _.PartType == "jv")
                        {
                            pid = commonJvPid;
                        }

                        itemPart.Pid = pid;
                    }
                    else if (_.PartType == "jv")
                    {
                        itemPart.Pid = commonJvPid;
                    }

                    result.Add(itemPart);
                }
            }

            return result;
        }

        /// <summary>
        /// 五级属性适配
        /// </summary>
        /// <param name="products"></param>
        /// <param name="vehicle"></param>
        /// <param name="property"></param>
        /// <param name="brandList"></param>
        /// <param name="propertyDescription"></param>
        /// <returns></returns>
        private Tuple<PropertySelectModel, List<PartProduct>> PropertyAdaptation(List<PartProduct> products,
            VehicleRequest vehicle,
            List<PropertyAdaptive> property, List<string> brandList,
            List<BaoYangPropertyDescriptionDO> propertyDescription)
        {
            PropertySelectModel carProperty = null;
            if (vehicle != null && property.Any())
            {
                var defaultProperty = property[0].Property;
                var userProperty =
                    vehicle.Properties?.FirstOrDefault(x => x.PropertyKey == defaultProperty);
                if (userProperty != null)
                {
                    var propertyValue = userProperty.PropertyValue;
                    products = property.Where(x => x.PropertyValue == propertyValue)
                        .SelectMany(x => x.Products).ToList();
                    if (brandList.Any())
                    {
                        products = products.Where(x => brandList.Contains(x.Brand)).ToList();
                    }
                }
                else
                {
                    products = new List<PartProduct>();
                    carProperty = new PropertySelectModel()
                    {
                        Type = defaultProperty == "款型" ? "TID" : "Property",
                        Name = defaultProperty,
                        Title = propertyDescription.FirstOrDefault(t => t.Name == defaultProperty)
                            ?.Title,
                        Content = propertyDescription.FirstOrDefault(t => t.Name == defaultProperty)
                            ?.Content,
                        Values = property.Select(x => new PropertyResult
                        {
                            Key = x.PropertyValue,
                            DisplayValue = x.PropertyValue,
                            ImageUrl = x.ImageUrl
                        }).ToList()
                    };
                }
            }

            return new Tuple<PropertySelectModel, List<PartProduct>>(carProperty, products);
        }

        #region 内部用

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RefreshRedis(string key)
        {
            // var result = await _redisClient.Redis.KeyDeleteAsync(key);
            //
            //
            // var pattern = $"{redisKey}:*";

            var redisResult = _redisClient.GetKeyByPattern(key);

            foreach (var itemRedis in redisResult)
            {
                await _redisClient.Redis.KeyDeleteAsync(itemRedis);
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeEnumVo>> GetServiceTypeEnum()
        {
            List<ServiceTypeEnumVo> result = new List<ServiceTypeEnumVo>();
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":ServiceTypeEnum:{timestamp}";
            var packageConfig = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _serviceTypeEnumRepository.GetServiceTypeEnum(), new TimeSpan(1, 0, 0, 0));
            if (packageConfig != null)
            {
                result = packageConfig.Select(_ => new ServiceTypeEnumVo
                {
                    Id = _.Id,
                    ServiceType = _.ServiceType,
                    Description = _.Description,
                    DisplayName = _.DisplayName,
                    ImageUrl = ImageHelper.AddImageDomain( _.ImageUrl),
                    RouteUrl = _.RouteUrl,
                    DepositAmount = _.DepositAmount
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 刷新保养适配配置缓存数据
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<bool> RefreshAdaptationConfigRedis(List<string> tidList)
        {
            foreach (var _ in tidList)
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd");
                var key = redisKey + $":BaoYangPartsByTid:{_}:{timestamp}";
                var key2 = redisKey + $":PartAccessoryByTid:{_}:{timestamp}";
                var partTask = _baoYangPartsRepository.GetBaoYangPartsByTidAsync(_);
                var accessoryTask = _baoYangPartAccessoryRepository.GetPartAccessoryByTidAsync(_);
                await Task.WhenAll(partTask, accessoryTask);
                await _redisClient.Redis.StringSetAsync(key, JsonConvert.SerializeObject(partTask.Result), new TimeSpan(0, 2, 0, 0));
                await _redisClient.Redis.StringSetAsync(key2, JsonConvert.SerializeObject(accessoryTask.Result), new TimeSpan(0, 2, 0, 0));
            }
            return true;
        }

        /// <summary>
        /// 刷新配置缓存
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshPackageDescriptionConfig()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PackageDescriptionConfig:{timestamp}";
            var result = await GetPackageDescriptionConfig();
            await _redisClient.Redis.StringSetAsync(key, JsonConvert.SerializeObject(result), new TimeSpan(1, 0, 0, 0));
            return true;
        }

        /// <summary>
        /// 刷新BaoYangTypeConfig缓存
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshBaoYangTypeConfig()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":BaoYangTypeConfig:{timestamp}";
            var result = await _baoYangTypeConfigRepository.GetBaoYangTypeConfigAsync();
            await _redisClient.Redis.StringSetAsync(key, JsonConvert.SerializeObject(result), new TimeSpan(0, 2, 0, 0));
            return true;
        }

        /// <summary>
        /// 刷新PackageMapConfig缓存
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshPackageMapConfig()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":PackageMapConfig:{timestamp}";
            var result = await _baoYangPackageMapConfigRepository.GetPackageMapConfigAsync();
            await _redisClient.Redis.StringSetAsync(key, JsonConvert.SerializeObject(result), new TimeSpan(0, 2, 0, 0));
            return true;
        }

        /// <summary>
        /// 根据产品带出服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<InstallServiceByProductResponse> GetInstallServiceByProduct(
            InstallServiceByProductRequest request)
        {
            InstallServiceByProductResponse result = new InstallServiceByProductResponse();
            var installService = await _productClient.GetInstallServiceByProduct(
                new InstallServiceByProductClientRequest()
                {
                    Products = request.Products.Select(_ => new InstallProductClientRequest
                    {
                        Pid = _.Pid,
                        InstallType = _.InstallType,
                        Num = _.Num
                    }).ToList()
                });
            if (installService != null)
            {
                result.InstallService =
                    _mapper.Map<List<InstallServiceDetailVo>>(
                        installService.InstallService ?? new List<InstallServiceDetailDto>());
                result.ProductInstallService =
                    _mapper.Map<List<ProductRelateServiceVo>>(
                        installService.ProductInstallService ?? new List<ProductRelateServiceDto>());
            }

            if (request.Vehicle != null && result.InstallService.Any())
            {
                request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);

                var servicePid = result.InstallService.Select(_ => _.ServiceId).Distinct().ToList();

                var addFee =
                    await _baoYangInstallFeeConfigRepository.GetInstallFeeConfigByPidList(servicePid,
                        request.Vehicle.GuidePrice);
                result.InstallService.ForEach(_ =>
                {
                    _.OriginalPrice = _.Price;
                    var defaultAdd = addFee.FirstOrDefault(t => t.ServiceId == _.ServiceId);
                    if (defaultAdd != null && _.FreeInstall == 0)
                    {
                        _.Price += defaultAdd.AdditionalPrice;
                        _.MarketPrice += defaultAdd.AdditionalPrice;
                        _.AdditionalPrice = defaultAdd.AdditionalPrice;
                        _.Remark = defaultAdd.Remark;
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// 查询服务费加价
        /// </summary>
        /// <returns></returns>
        public async Task<List<InstallServiceDetailVo>> GetAdditionalPriceByServiceId(
            AdditionalPriceByServiceIdRequest request)
        {
            var products = await _productClient.GetProductsByProductCodes(request.ServiceId);

            List<InstallServiceDetailVo> result = products.Select(_ => new InstallServiceDetailVo
            {
                ServiceName = _.Product.Name,
                ServiceId = _.Product.ProductCode,
                Price = _.Product.SalesPrice,
                Description = _.Product.Remark,
                ImageUrl = ImageHelper.AddImageDomain(_.Product.Image1),
                MarketPrice = _.Product.StandardPrice,
                OriginalPrice = _.Product.SalesPrice
            }).ToList();

            if (request.Vehicle != null && result.Any())
            {
                request.Vehicle = await FillVehicleInfoAsync(request.Vehicle);
                var addFee =
                    await _baoYangInstallFeeConfigRepository.GetInstallFeeConfigByPidList(request.ServiceId,
                        request.Vehicle.GuidePrice);
                result.ForEach(_ =>
                {
                    _.OriginalPrice = _.Price;
                    var defaultAdd = addFee.FirstOrDefault(t => t.ServiceId == _.ServiceId);
                    if (defaultAdd != null && _.FreeInstall == 0)
                    {
                        _.Price += defaultAdd.AdditionalPrice;
                        _.MarketPrice += defaultAdd.AdditionalPrice;
                        _.AdditionalPrice = defaultAdd.AdditionalPrice;
                        _.Remark = defaultAdd.Remark;
                    }
                });
            }

            return result;
        }
    }
}
