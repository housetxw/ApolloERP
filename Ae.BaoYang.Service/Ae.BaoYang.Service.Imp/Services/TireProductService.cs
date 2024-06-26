using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.BaoYang.Service.Client.Clients;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Common.Exceptions;
using Ae.BaoYang.Service.Common.Helper;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Repository;

namespace Ae.BaoYang.Service.Imp.Services
{
    public class TireProductService : ITireProductService
    {
        private readonly IMapper _mapper;
        private readonly VehicleClient _vehicleClient;
        private readonly ShopManageClient _shopManageClient;
        private readonly ITireCategoryConfigRepository _tireCategoryConfigRepository;
        private readonly ProductClient _productClient;
        private readonly UserClient _userClient;
        private readonly StockClient _stockClient;

        public TireProductService(IMapper mapper, VehicleClient vehicleClient, ShopManageClient shopManageClient,
            ITireCategoryConfigRepository tireCategoryConfigRepository, ProductClient productClient,
            UserClient userClient, StockClient stockClient)
        {
            _mapper = mapper;
            _vehicleClient = vehicleClient;
            _shopManageClient = shopManageClient;
            _tireCategoryConfigRepository = tireCategoryConfigRepository;
            _productClient = productClient;
            _userClient = userClient;
            _stockClient = stockClient;
        }

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TireServiceListResponse> GetTireCategoryListAsync(TireCategoryListRequest request)
        {
            List<TireCategoryVo> category = new List<TireCategoryVo>();

            var checkData = await VehicleCheck(request);
            string defaultTireSize = checkData.Item1;
            List<TireCategoryConfigDO> tireConfig = checkData.Item4;
            VehicleTypeDto vehicleType = checkData.Item6;
            List<string> userAttentionPid = checkData.Item5;
            List<string> oePid = checkData.Item3;
            List<string> vehicleTireSize = checkData.Item2;

            if (string.IsNullOrEmpty(defaultTireSize))
            {
                throw new CustomException("车型数据配置有误：轮胎尺寸不能为空");
            }

            var siLunConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "SiLunService");
            if (siLunConfig != null)
            {
                var siLunService = await GetSiLunService(siLunConfig, vehicleType.ServicePriceLevel, request.ShopId,
                    userAttentionPid);
                if (siLunService != null)
                {
                    category.Add(siLunService);
                }
            }

            var repairConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "RepairTire");
            if (repairConfig != null)
            {
                var repairService = await GetRepairTireService(repairConfig, request.ShopId, userAttentionPid);
                if (repairService != null)
                {
                    category.Add(repairService);
                }
            }

            var reTireConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "ReTire");
            if (reTireConfig != null)
            {
                var reTireService = await GetReTireService(reTireConfig, defaultTireSize, oePid, request.ProvinceId,
                    request.CityId, userAttentionPid, 1);
                if (reTireService != null)
                {
                    category.Add(reTireService);
                }
            }

            return new TireServiceListResponse()
            {
                DefaultTireSize = defaultTireSize,
                TireSizes = vehicleTireSize,
                TireCategory = category
            };
        }

        /// <summary>
        /// 车型check
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async
            Task<Tuple<string, List<string>, List<string>, List<TireCategoryConfigDO>, List<string>, VehicleTypeDto>>
            VehicleCheck(
                TireCategoryListRequest request)
        {
            var tireConfigTask = _tireCategoryConfigRepository.GetTireCategoryConfig();
            var vehicleTypeTask = _vehicleClient.GetVehicleTypeById(new VehicleTypeByIdClientRequest
            {
                VehicleId = request.VehicleId
            });
            var userAttentionPidTask = _userClient.GetUserAttentionPid(request.UserId);
            await Task.WhenAll(tireConfigTask, vehicleTypeTask, userAttentionPidTask);
            var tireConfig = tireConfigTask.Result?.ToList() ?? new List<TireCategoryConfigDO>();
            var vehicleType = vehicleTypeTask.Result;
            if (vehicleType == null)
            {
                throw new CustomException("车系不存在");
            }

            List<string> userAttentionPid = userAttentionPidTask.Result ?? new List<string>();

            var vehicleTireSize =
                vehicleType.Tires?.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                new List<string>(); //车系对应的轮胎尺寸
            List<string> oePid =
                vehicleType.TiresMatch?.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                new List<string>();
            string defaultTireSize = request.TireSize;
            if (!string.IsNullOrEmpty(request.Tid))
            {
                var oeTireSizeTask = _vehicleClient.GetOeTireSizeByTid(new OeTireSizeByTidClientRequest()
                {
                    Tid = request.Tid
                });
                var oeTirePidTask = _vehicleClient.GetOeTirePidByTid(new OeTirePidByTidClientRequest()
                {
                    Tid = request.Tid
                });
                await Task.WhenAll(oeTireSizeTask, oeTirePidTask);
                oePid = oeTirePidTask.Result ?? new List<string>();
                var oeTireSize = oeTireSizeTask.Result;
                if (oeTireSize != null)
                {
                    if (string.IsNullOrEmpty(defaultTireSize))
                    {
                        defaultTireSize = string.IsNullOrEmpty(oeTireSize.FrontTireSize)
                            ? oeTireSize.RearTireSize
                            : oeTireSize.FrontTireSize;
                    }

                    if (!string.IsNullOrEmpty(oeTireSize.FrontTireSize) &&
                        !vehicleTireSize.Contains(oeTireSize.FrontTireSize))
                    {
                        vehicleTireSize.Add(oeTireSize.FrontTireSize);
                    }

                    if (!string.IsNullOrEmpty(oeTireSize.RearTireSize) &&
                        !vehicleTireSize.Contains(oeTireSize.RearTireSize))
                    {
                        vehicleTireSize.Add(oeTireSize.RearTireSize);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(defaultTireSize))
                {
                    defaultTireSize = vehicleTireSize.FirstOrDefault();
                }
            }

            return new
                Tuple<string, List<string>, List<string>, List<TireCategoryConfigDO>, List<string>, VehicleTypeDto>(
                    defaultTireSize, vehicleTireSize, oePid, tireConfig, userAttentionPid, vehicleType);
        }

        /// <summary>
        /// 四轮定位服务
        /// </summary>
        /// <param name="config"></param>
        /// <param name="servicePriceLevel"></param>
        /// <param name="shopId"></param>
        /// <param name="userAttentionPid"></param>
        /// <returns></returns>
        private async Task<TireCategoryVo> GetSiLunService(TireCategoryConfigDO config, string servicePriceLevel,
            int shopId, List<string> userAttentionPid)
        {
            var siLunPid = GetSiLunPidByServiceLevel(servicePriceLevel);
            if (!string.IsNullOrEmpty(siLunPid))
            {
                bool showSiLun = true;
                if (shopId > 0)
                {
                    showSiLun = await ShopServeFilter(shopId, siLunPid);
                }

                if (showSiLun)
                {
                    TireCategoryVo siLun = new TireCategoryVo()
                    {
                        CategoryType = config.CategoryType,
                        ZhName = config.DisplayName,
                        BriefDescription = config.BriefDescription,
                        ImageUrl = ImageHelper.AddImageDomain(config.ImageUrl), 
                        Tags = new List<TagInfo>()
                    };
                    var siLunProduct = await GetProductsByPidList(new List<string>() {siLunPid});
                    siLunProduct.ForEach(_ =>
                    {
                        _.IsAttention = userAttentionPid.Contains(_.Pid);
                        _.IsDefaultSelect = true;
                    });
                    siLun.Products = siLunProduct;
                    return siLun;
                }
            }

            return null;
        }

        /// <summary>
        /// 补胎换位
        /// </summary>
        /// <param name="config"></param>
        /// <param name="shopId"></param>
        /// <param name="userAttentionPid"></param>
        /// <returns></returns>
        private async Task<TireCategoryVo> GetRepairTireService(TireCategoryConfigDO config, int shopId,
            List<string> userAttentionPid)
        {
            List<TireProductVo> result = new List<TireProductVo>();
            int.TryParse(config.CategoryId, out int categoryId);
            var service = await _productClient.SelectProductsByCategoryCache(categoryId);
            if (shopId > 0 && service.Any())
            {
                List<string> serviceId = service.Select(_ => _.ProductCode).ToList();
                var shopServiceList = await _shopManageClient.GetShopServiceListAsync(new ShopServiceListRequest
                {
                    ShopId = shopId,
                    ProductIds = serviceId
                });
                List<string> onSaleService = new List<string>();
                if (shopServiceList != null && shopServiceList.Any())
                {
                    onSaleService = shopServiceList.Where(_ => _.IsOnline).Select(_ => _.Pid).ToList();
                }

                service = service.Where(_ => onSaleService.Contains(_.ProductCode)).ToList();
            }

            service.ForEach(_ =>
            {
                var itemProduct = ConvertProduct(_, 1);
                itemProduct.IsAttention = userAttentionPid.Contains(_.ProductCode);
                result.Add(itemProduct);
            });

            return new TireCategoryVo()
            {
                CategoryType = config.CategoryType,
                ZhName = config.DisplayName,
                BriefDescription = config.BriefDescription,
                ImageUrl = ImageHelper.AddImageDomain(config.ImageUrl),
                Tags = new List<TagInfo>(),
                Products = result
            };
        }

        /// <summary>
        /// 更换轮胎服务
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tireSize"></param>
        /// <param name="oePid"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="userAttentionPid"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private async Task<TireCategoryVo> GetReTireService(TireCategoryConfigDO config, string tireSize,
            List<string> oePid, string provinceId, string cityId, List<string> userAttentionPid, int limit = 0)
        {
            return new TireCategoryVo()
            {
                CategoryType = config.CategoryType,
                ZhName = config.DisplayName,
                BriefDescription = config.BriefDescription,
                ImageUrl = ImageHelper.AddImageDomain(config.ImageUrl),
                Tags = new List<TagInfo>(),
                Products = await GetTireProducts(tireSize, oePid, provinceId, cityId, userAttentionPid, limit)
            };
        }

        private async Task<List<TireProductVo>> GetTireProducts(string tireSize, List<string> oePid, string provinceId,
            string cityId, List<string> userAttentionPid, int limit = 0, List<string> currentPid = null)
        {
            List<TireProductVo> result = new List<TireProductVo>();
            int tireCategoryId = 4; //轮胎商品类目
            var product = await _productClient.SelectProductsByCategoryCache(tireCategoryId);
            product = product.Where(_ =>
                    _.Attributevalues.Any(t => t.AttributeName == "Specifications" && t.AttributeValue == tireSize))
                .ToList(); //过滤轮胎尺寸
            //var productStock = await _stockClient.GetWarehouseStockForAdaptation(provinceId, cityId,
            //    product.Select(_ => _.ProductCode).ToList());
            product.ForEach(_ =>
            {
                //_.AvailableStockQuantity =
                //    productStock.FirstOrDefault(t => t.Pid == _.ProductCode)?.AvailableStockQuantity ?? 0;
                _.IsOriginal = oePid.Contains(_.ProductCode);
            });
            product = product.OrderBy(_ => _.Stockout)
                .ThenByDescending(_ => oePid.Contains(_.ProductCode)).ThenBy(_ => _.SalesPrice).ToList();
            if (limit > 0)
            {
                product = product.Take(limit).ToList();
            }

            product.ForEach(_ =>
            {
                var itemProduct = ConvertProduct(_, 1);
                itemProduct.IsAttention = userAttentionPid.Contains(_.ProductCode);
                result.Add(itemProduct);
            });

            if (currentPid != null && currentPid.Any())
            {
                result.ForEach(_ => { _.IsDefaultSelect = currentPid.Contains(_.Pid); });
            }
            else if (result.Any())
            {
                result[0].IsDefaultSelect = true;
            }

            return result;
        }

        /// <summary>
        /// 轮胎适配列表 - 展示所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TireServiceListResponse> GetTireCategoryListAllProductAsync(TireCategoryListRequest request)
        {
            List<TireCategoryVo> category = new List<TireCategoryVo>();
            var checkData = await VehicleCheck(request);
            string defaultTireSize = checkData.Item1;
            List<TireCategoryConfigDO> tireConfig = checkData.Item4;
            VehicleTypeDto vehicleType = checkData.Item6;
            List<string> userAttentionPid = checkData.Item5;
            List<string> oePid = checkData.Item3;
            List<string> vehicleTireSize = checkData.Item2;

            if (string.IsNullOrEmpty(defaultTireSize))
            {
                throw new CustomException("车型数据配置有误：轮胎尺寸不能为空");
            }

            var siLunConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "SiLunService");
            if (siLunConfig != null)
            {
                var siLunService = await GetSiLunService(siLunConfig, vehicleType.ServicePriceLevel, request.ShopId,
                    userAttentionPid);
                if (siLunService != null)
                {
                    category.Add(siLunService);
                }
            }

            var repairConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "RepairTire");
            if (repairConfig != null)
            {
                var repairService = await GetRepairTireService(repairConfig, request.ShopId, userAttentionPid);
                if (repairService != null)
                {
                    category.Add(repairService);
                }
            }

            var reTireConfig = tireConfig.FirstOrDefault(_ => _.CategoryType == "ReTire");
            if (reTireConfig != null)
            {
                var reTireService = await GetReTireService(reTireConfig, defaultTireSize, oePid, request.ProvinceId,
                    request.CityId, userAttentionPid);
                if (reTireService != null)
                {
                    category.Add(reTireService);
                }
            }

            return new TireServiceListResponse()
            {
                DefaultTireSize = defaultTireSize,
                TireSizes = vehicleTireSize,
                TireCategory = category
            };
        }

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<TireProductVo>, int>> GetTireListAsync(TireListRequest request)
        {
            List<TireProductVo> result = new List<TireProductVo>();
            if (request.CategoryType == "ReTire")
            {
                List<string> oePid = new List<string>();
                if (!string.IsNullOrEmpty(request.Tid))
                {
                    oePid = await _vehicleClient.GetOeTirePidByTid(new OeTirePidByTidClientRequest()
                    {
                        Tid = request.Tid
                    }) ?? new List<string>();
                }
                else
                {
                    var vehicleType = await _vehicleClient.GetVehicleTypeById(new VehicleTypeByIdClientRequest
                    {
                        VehicleId = request.VehicleId
                    });
                    if (vehicleType != null)
                    {
                        oePid = vehicleType.TiresMatch?.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                                    ?.ToList() ?? new List<string>();
                    }
                }

                List<string> userAttentionPid =
                    await _userClient.GetUserAttentionPid(request.UserId) ?? new List<string>();

                var products = await GetTireProducts(request.TireSize, oePid, request.ProvinceId, request.CityId,
                    userAttentionPid, 0, request.PidCount?.Select(x => x.Pid)?.ToList() ?? new List<string>());

                return new Tuple<List<TireProductVo>, int>(products, products.Count);
            }

            return new Tuple<List<TireProductVo>, int>(new List<TireProductVo>(), 0);
        }

        private string GetSiLunPidByServiceLevel(string servicePriceLevel)
        {
            switch (servicePriceLevel.ToUpper())
            {
                case "A":
                    return "FW-LT-SLDW-2-FU";
                case "B":
                    return "FW-LT-SLDW-3-FU";
                case "C":
                    return "FW-LT-SLDW-4-FU";
                default:
                    return "FW-LT-SLDW-2-FU";
            }
        }

        /// <summary>
        /// 服务是否上下架
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private async Task<bool> ShopServeFilter(int shopId, string pid)
        {
            bool onSale = false;
            List<string> serviceId = new List<string>() {pid};
            var shopServiceList = await _shopManageClient.GetShopServiceListAsync(new ShopServiceListRequest
            {
                ShopId = shopId,
                ProductIds = serviceId
            });
            if (shopServiceList != null && shopServiceList.Any())
            {
                onSale = shopServiceList.FirstOrDefault(_ => _.Pid == pid)?.IsOnline ?? false;
            }

            return onSale;
        }

        /// <summary>
        /// 根据Pid查轮胎商品
        /// </summary>
        /// <param name="pidList"></param>
        /// <returns></returns>
        private async Task<List<TireProductVo>> GetProductsByPidList(List<string> pidList)
        {
            List<TireProductVo> result = new List<TireProductVo>();
            var products = await _productClient.GetProductsByProductCodes(pidList);
            if (products != null && products.Any())
            {
                products.ForEach(_ => { result.Add(ConvertProduct(_, 1)); });
            }

            return result;
        }

        private TireProductVo ConvertProduct(ProductDetailDTO product, int count)
        {
            if (product != null)
            {
                return new TireProductVo
                {
                    Pid = product.Product.ProductCode,
                    Brand = product.Product.Brand,
                    DisplayName = product.Product.DisplayName,
                    Image = ImageHelper.AddImageDomain(product.Product.Image1),
                    Price = product.Product.SalesPrice,
                    MarketPrice = product.Product.StandardPrice,
                    StockOut = product.Product.Stockout,
                    Count = count,
                    Unit = product.Product.Unit
                };
            }

            return null;
        }

        private TireProductVo ConvertProduct(ProductBaseInfoDTO product, int count)
        {
            if (product != null)
            {
                return new TireProductVo
                {
                    Pid = product.ProductCode,
                    Brand = product.Brand,
                    DisplayName = product.Name,
                    Image = ImageHelper.AddImageDomain(product.Image1),
                    Price = product.SalesPrice,
                    MarketPrice = product.StandardPrice,
                    StockOut = product.Stockout,
                    Count = count,
                    IsOriginal = product.IsOriginal,
                    Unit = product.Unit
                };
            }

            return null;
        }


        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AdaptiveProductsResponse> VerifyAdaptiveProducts(VerifyTireProductRequest request)
        {
            List<string> tireSize = new List<string>();
            if (!string.IsNullOrEmpty(request.TireSize))
            {
                tireSize.Add(request.TireSize);
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Tid))
                {
                    var oeTireSize = await _vehicleClient.GetOeTireSizeByTid(new OeTireSizeByTidClientRequest()
                    {
                        Tid = request.Tid
                    });
                    var frontSize = oeTireSize?.FrontTireSize;
                    var rearSize = oeTireSize?.RearTireSize;
                    if (!string.IsNullOrEmpty(frontSize))
                    {
                        tireSize.Add(frontSize);
                    }

                    if (!tireSize.Contains(rearSize))
                    {
                        tireSize.Add(rearSize);
                    }
                }
            }

            if (!tireSize.Any())
            {
                var vehicleType = await _vehicleClient.GetVehicleTypeById(new VehicleTypeByIdClientRequest
                {
                    VehicleId = request.VehicleId
                });

                var tireSizes =
                    vehicleType?.Tires?.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                    new List<string>(); //车系对应的轮胎尺寸
                tireSize.AddRange(tireSizes);
            }

            List<string> adaptivePid = new List<string>();
            if (tireSize.Any())
            {
                int tireCategoryId = 4; //轮胎商品类目
                var product = await _productClient.SelectProductsByCategoryCache(tireCategoryId);
                var pidList = product
                    .Where(_ => _.Attributevalues.Any(t =>
                        t.AttributeName == "Specifications" && tireSize.Contains(t.AttributeValue)))
                    .Select(_ => _.ProductCode).ToList(); //过滤轮胎尺寸

                adaptivePid = request.PidList.Where(_ => pidList.Contains(_)).ToList();
            }

            return new AdaptiveProductsResponse()
            {
                AdaptivePid = adaptivePid
            };
        }
    }
}
