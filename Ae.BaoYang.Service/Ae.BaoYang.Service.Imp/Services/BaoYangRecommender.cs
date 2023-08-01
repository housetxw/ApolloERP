using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.BaoYang.Service.Common.Constant;
using Ae.BaoYang.Service.Common.Helper;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Imp.Enum;
using Ae.BaoYang.Service.Imp.Model;

namespace Ae.BaoYang.Service.Imp.Services
{
    public class BaoYangRecommender
    {
        private readonly IBaoYangService _baoYangService;
        private PackageDescriptionConfig packageConfig;
        private VehicleAdaptationResultModel adaptations;
        private string userId;
        private readonly string _provinceId;
        private readonly string _cityId;
        private readonly string _channel;
        private readonly List<string> _attentionPid;
        private readonly VehicleRequest _vehicle;
        private readonly List<TargetProduct> _targetProducts;
        private readonly bool _jiYouAdaption;
        private readonly int _shopId;

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

        private readonly string _jiYouUnit4 = "4L";
        private readonly string _jiYouUnit1 = "1L";


        public BaoYangRecommender(IBaoYangService baoYangService, PackageDescriptionConfig packageConfig,
            VehicleAdaptationResultModel adaptations, string userId, string provinceId, string cityId, string channel,
            List<string> attentionPid, VehicleRequest vehicle, List<TargetProduct> targetProducts, bool jiYouAdaption,
            int shopId)
        {
            _baoYangService = baoYangService;
            this.packageConfig = packageConfig;
            this.adaptations = adaptations;
            this.userId = userId;
            this._provinceId = provinceId;
            this._cityId = cityId;
            this._channel = channel;
            _attentionPid = attentionPid;
            _vehicle = vehicle;
            _targetProducts = targetProducts;
            _jiYouAdaption = jiYouAdaption;
            _shopId = shopId;
        }


        public async Task<BaoYangPackageModel> GetBaoYangPackageAsync(BaoYangPackageDescription package,
            List<string> selectType, bool showAll)
        {
            BaoYangPackageModel result = null;
            string packageType = package.PackageType;
            var defaultPackage = packageConfig.Packages.FirstOrDefault(_ => _.PackageType == packageType);
            if (defaultPackage != null)
            {
                result = new BaoYangPackageModel
                {
                    PackageType = defaultPackage.PackageType,
                    ZhName = defaultPackage.DisplayName,
                    SuggestTip = defaultPackage.SuggestTip,
                    BriefDescription = defaultPackage.BriefDescription,
                    DescriptionLink = defaultPackage.DescriptionLink,
                    ImageUrl = defaultPackage.ImageUrl
                };
                if (packageType == "xby" || packageType == "dby")
                {
                    result.GroupName = "dxby";
                }

                if (selectType != null && selectType.Any())
                {
                    result.IsDefaultExpand = selectType.Contains(packageType);
                }

                if (_targetProducts != null && _targetProducts.Any() && !result.IsDefaultExpand)
                {
                    result.IsDefaultExpand = _targetProducts.Any(x => x.PackageType == packageType);
                }

                List<BaoYangPackageItemModel> baoYangTypes = new List<BaoYangPackageItemModel>();
                switch (packageType)
                {
                    case "ys":
                        baoYangTypes = await GetPackageItemCommonAsync(package, showAll);
                        //baoYangTypes = await GetPackageItemForYsAsync(package, showAll);
                        break;
                    case "fsa":
                    case "bsa":
                        baoYangTypes = await GetPackageItemForJzqAsync(package, showAll);
                        break;
                    case "dyn":
                        baoYangTypes = await GetPackageForDynAsync(package, showAll);
                        break;
                    case "mto":
                        baoYangTypes = await GetPackageForMtoAsync(package, showAll);
                        break;
                    default:
                        baoYangTypes = await GetPackageItemCommonAsync(package, showAll);
                        break;
                }

                if (baoYangTypes != null && baoYangTypes.Any())
                {
                    baoYangTypes.ForEach(_ =>
                    {
                        if (_.Products != null && _.Products.Any())
                        {
                            _.Products.ForEach(t =>
                            {
                                t.IsAttention = _attentionPid.Contains(t.Pid);
                                t.IsDefaultSelect = true;
                            });
                        }
                        else if (_.Property == null)
                        {
                            _.InAdaptReason = "暂无合适商品推荐";
                        }
                    });

                    var inAdaptItem =
                        baoYangTypes.Any(_ => (_.Products != null && _.Products.Any()) || _.Property != null);
                    if (inAdaptItem)
                    {
                        result.Items = baoYangTypes;
                    }
                    else
                    {
                        result.Items = new List<BaoYangPackageItemModel>();
                        result.InAdaptReason = "暂无合适商品推荐";
                    }
                }
                else
                {
                    result.Items = new List<BaoYangPackageItemModel>();
                    result.InAdaptReason = "暂无合适商品推荐";
                }
            }

            return result;
        }

        /*/// <summary>
        /// 填充安装方式
        /// </summary>
        /// <param name="package"></param>
        private void FillPackageByInstallType(BaoYangPackageModel package)
        {
            List<InstallTypeConfig> installTypeConfig =
                packageConfig.InstallType.FirstOrDefault(_ => _.PackageType == package.PackageType)
                    ?.InstallTypeConfig ?? new List<InstallTypeConfig>();

            if (installTypeConfig.Count > 1)
            {
                var defaultInstallType = installTypeConfig.FirstOrDefault(_ => _.IsDefault);
                if (defaultInstallType != null)
                {
                    var otherInstallType = installTypeConfig.Where(_ => _.InstallType != defaultInstallType.InstallType)
                        .ToList();

                    package.Items = package.Items
                        .Where(item => defaultInstallType.BaoYangType.Contains(item.BaoYangType)).ToList();

                    package.CurrentInstallType = GetInstallType(defaultInstallType, package);

                    package.AlternateInstallTypes = otherInstallType.Select(o => GetInstallType(o, package)).ToList();
                }
            }
        }*/

        private InstallTypeModel GetInstallType(InstallTypeConfig config, BaoYangPackageModel package)
        {
            InstallTypeModel result = new InstallTypeModel()
            {
                Type = config.InstallType,
                ZhName = config.DisplayName,
                ContainedByTypes = config.BaoYangType
            };

            var byTypes = config.BaoYangType;

            result.Price = package.Items.Where(item => byTypes.Contains(item.BaoYangType) && item.Products != null)
                .Sum(o => o.Products.Sum(p => p.Price * p.Count));

            return result;
        }

        /// <summary>
        /// 变速箱油 Package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageItemModel>> GetPackageForMtoAsync(BaoYangPackageDescription package,bool showAll)
        {
            if (package.ProductType == 0) //配置套餐
            {
                return await GetBaoYangPackageItemsForPackage(package.Items, package.PackageType, showAll);
            }

            List<BaoYangPackageItemModel> result = new List<BaoYangPackageItemModel>();
            var supportType = await GetTransFluidAdaptation(_vehicle);
            var baoYangType = package.Items;
            BaoYangAccessoryModel accessory =
                adaptations.AccessoryTypeDic.FirstOrDefault(_ => _.AccessoryName == "变速箱油");
            const string atoType = "ato", atopType = "atop", mmtoType = "mmto";
            if (supportType == mmtoType)
            {
                #region mmto 手动挡

                var trsModel = new TransFluidModel(accessory);
                if (trsModel.IsValid())
                {
                    var baoYangItem = baoYangType.FirstOrDefault(_ => _.BaoYangType == mmtoType);
                    if (baoYangItem != null)
                    {
                        var mtoResult = await GetPackageItemByVolumeAsync(baoYangItem.BaoYangType,
                            baoYangItem.CatalogName, null, trsModel.Volume, "L");
                        result.Add(new BaoYangPackageItemModel()
                        {
                            BaoYangType = baoYangItem.BaoYangType,
                            ZhName = baoYangItem.DisplayName,
                            GroupName = package.PackageType,
                            Products = mtoResult.Data,
                            InAdaptReason = mtoResult.Reason.ToString()
                        });
                    }
                }

                #endregion
            }
            else if (supportType == atoType)
            {
                var baoYangItem = baoYangType.FirstOrDefault(_ => _.BaoYangType == atoType);
                if (baoYangItem != null)
                {
                    var productIds =
                        adaptations.PartTypeDic.FirstOrDefault(_ => _.BaoYangType == baoYangItem.BaoYangType)?.Pids
                            ?.Select(t => t.Pid)?.Distinct()?.ToList() ?? new List<string>();
                    var mtoResult = await GetPackageItemForAtoAsync(productIds, baoYangItem,
                        package.PackageType, accessory);
                    result.Add(mtoResult);
                }

                var atopItem = baoYangType.FirstOrDefault(_ => _.BaoYangType == atopType);
                if (atopItem != null)
                {
                    BaoYangPartModel atopPart = adaptations.PartTypeDic.FirstOrDefault(_ => _.BaoYangType == atopType);
                    var atopResult = await GetPackageItemByBaoYangTypeAsync(atopType, atopPart, showAll);

                    atopResult.GroupName = package.PackageType;
                    atopResult.ZhName = atopItem.DisplayName;
                    result.Add(atopResult);
                }
            }

            return result;
        }

        private async Task<BaoYangPackageItemModel> GetPackageItemForAtoAsync(List<string> productIds, BaoYangTypeDescription byType, string packageType, BaoYangAccessoryModel accessory)
        {
            BaoYangPackageItemModel result = null;
            var model = new TransFluidModel(accessory);
            if (model.IsValid() && productIds != null && productIds.Any())
            {
                var mtoResult = await GetPackageItemByVolumeAsync(byType.BaoYangType, byType.CatalogName,
                    productIds, model.Volume, "L");
                result = new BaoYangPackageItemModel()
                {
                    BaoYangType = byType.BaoYangType,
                    ZhName = byType.DisplayName,
                    GroupName = packageType,
                    Products = mtoResult.Data,
                    InAdaptReason = mtoResult.Reason.ToString()
                };
            }
            else
            {
                result = new BaoYangPackageItemModel()
                {
                    BaoYangType = byType.BaoYangType,
                    ZhName = byType.DisplayName,
                    GroupName = packageType,
                    Products = new List<BaoYangPackageProductModel>(),
                    InAdaptReason = InAdaptableType.InValidData.ToString()
                };
            }

            return result;
        }

        /// <summary>
        /// 手动 or 自动
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private async Task<string> GetTransFluidAdaptation(VehicleRequest vehicle)
        {
            string supportedType = string.Empty;
            var vehicleConfig = await _baoYangService.GetVehicleConfigByTid(vehicle.Tid);
            if (!string.IsNullOrWhiteSpace(vehicleConfig?.TransmissionTypeC))
            {
                supportedType = vehicleConfig.TransmissionTypeC.Contains(GlobalConstant.AT) ? "mmto" : "ato";
            }

            return supportedType;
        }

        /// <summary>
        /// Dyn Package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageItemModel>> GetPackageForDynAsync(BaoYangPackageDescription package,bool showAll)
        {
            if (package.ProductType == 0) //配置套餐
            {
                return await GetBaoYangPackageItemsForPackage(package.Items, package.PackageType, showAll);
            }

            List<BaoYangPackageItemModel> result = new List<BaoYangPackageItemModel>();
            var baoYangType = package.Items;
            var ysParts = adaptations.PartTypeDic
                .Where(_ => baoYangType.Select(x => x.BaoYangType).Contains(_.BaoYangType)).ToList();

            List<string> fdyn = new List<string>();
            List<string> ndyn = new List<string>();
            List<string> fndyn = new List<string>();
            InAdaptableType fdynType = InAdaptableType.None;
            InAdaptableType ndynType = InAdaptableType.None;
            InAdaptableType fndynType = InAdaptableType.None;

            foreach (var pair in ysParts)
            {
                switch (pair.BaoYangType)
                {
                    case "fdyn":
                        fdyn = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                   ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                    case "ndyn":
                        ndyn = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                   ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                    case "fndyn":
                        fndyn = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                    ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                }
            }

            TypeAdaptationResultModel<List<BaoYangPackageProductModel>> strategyResult =
                new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>();
            if (fndyn.Any())
            {
                strategyResult = await GetPackageItemProductsForDyn("fndyn", fndyn);
            }

            if (strategyResult.Data == null || !strategyResult.Data.Any())
            {
                var fdynResult = await GetPackageItemProductsForDyn("fdyn", fdyn);
                result.Add(new BaoYangPackageItemModel()
                {
                    BaoYangType = "fdyn",
                    ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "fdyn")?.DisplayName,
                    GroupName = package.PackageType,
                    Products = fdynResult.Data,
                    InAdaptReason = fdynResult.Reason.ToString()
                });

                var ndynResult = await GetPackageItemProductsForDyn("ndyn", ndyn);
                result.Add(new BaoYangPackageItemModel()
                {
                    BaoYangType = "ndyn",
                    ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "ndyn")?.DisplayName,
                    GroupName = package.PackageType,
                    Products = ndynResult.Data,
                    InAdaptReason = ndynResult.Reason.ToString()
                });
            }
            else
            {
                result.Add(new BaoYangPackageItemModel()
                {
                    BaoYangType = "fndyn",
                    ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "fndyn")?.DisplayName,
                    GroupName = package.PackageType,
                    Products = strategyResult.Data,
                    InAdaptReason = strategyResult.Reason.ToString()
                });
            }

            return result;
        }

        /// <summary>
        /// 车灯
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        private async Task<TypeAdaptationResultModel<List<BaoYangPackageProductModel>>>
            GetPackageItemProductsForDyn(string baoYangType, List<string> productIds)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;
            if (productIds != null && productIds.Any())
            {
                result = await _baoYangService.GetPartProduct(productIds, 2,
                    adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangType).ToList(), _provinceId, _cityId,
                    _shopId, 1);
                if (result == null || !result.Any())
                {
                    reason = InAdaptableType.NoProduct;
                }
            }
            else
            {
                reason = InAdaptableType.NoProduct;
            }

            return new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>()
            {
                Data = result,
                Reason = reason
            };
        }


        /// <summary>
        /// 减震器 Package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageItemModel>> GetPackageItemForJzqAsync(BaoYangPackageDescription package, bool showAll)
        {
            if (package.ProductType == 0) //配置套餐
            {
                return await GetBaoYangPackageItemsForPackage(package.Items, package.PackageType, showAll);
            }

            List<BaoYangPackageItemModel> result = new List<BaoYangPackageItemModel>();
            var baoYangType = package.Items;
            var ysParts = adaptations.PartTypeDic
                .Where(_ => baoYangType.Select(x => x.BaoYangType).Contains(_.BaoYangType)).ToList();

            List<string> left = new List<string>();
            List<string> right = new List<string>();
            InAdaptableType leftType = InAdaptableType.None;
            InAdaptableType rightType = InAdaptableType.None;
            string leftByType = string.Empty;
            string rightByType = string.Empty;

            switch (package.PackageType)
            {
                case "fsa":
                    leftByType = "lfsa";
                    rightByType = "rfsa";

                    break;
                case "bsa":
                    leftByType = "lbsa";
                    rightByType = "rbsa";
                    break;
            }

            left = ysParts.FirstOrDefault(_ => _.BaoYangType == leftByType)?.Pids?.Select(_ => _.Pid)
                       ?.Distinct()?.ToList() ?? new List<string>();
            right = ysParts.FirstOrDefault(_ => _.BaoYangType == rightByType)?.Pids?.Select(_ => _.Pid)
                        ?.Distinct()?.ToList() ?? new List<string>();

            if (left.Any() && right.Any())
            {
                var zyYsProduct = await GetJzqByPriority(left, right);
                if (zyYsProduct != null && zyYsProduct.Any())
                {
                    result.Add(new BaoYangPackageItemModel()
                    {
                        BaoYangType = leftByType,
                        ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == leftByType)?.DisplayName,
                        GroupName = package.PackageType,
                        Products = zyYsProduct.Where(_ => left.Contains(_.Pid)).ToList(),
                        InAdaptReason = InAdaptableType.None.ToString()
                    });

                    result.Add(new BaoYangPackageItemModel()
                    {
                        BaoYangType = rightByType,
                        ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == rightByType)?.DisplayName,
                        GroupName = package.PackageType,
                        Products = zyYsProduct.Where(_ => right.Contains(_.Pid)).ToList(),
                        InAdaptReason = InAdaptableType.None.ToString()
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 雨刷Package
        /// </summary>
        /// <param name="package"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageItemModel>> GetPackageItemForYsAsync(BaoYangPackageDescription package, bool showAll)
        {
            if (package.ProductType == 0) //配置套餐
            {
                return await GetBaoYangPackageItemsForPackage(package.Items, package.PackageType, showAll);
            }

            List<BaoYangPackageItemModel> result = new List<BaoYangPackageItemModel>();
            var baoYangType = package.Items;
            var ysParts = adaptations.PartTypeDic
                .Where(_ => baoYangType.Select(x => x.BaoYangType).Contains(_.BaoYangType)).ToList();

            List<string> left = null;
            List<string> right = null;
            List<string> front = null;
            InAdaptableType leftType = InAdaptableType.None;
            InAdaptableType rightType = InAdaptableType.None;
            InAdaptableType frontType = InAdaptableType.None;
            foreach (var pair in ysParts)
            {
                switch (pair.BaoYangType)
                {
                    case "zys":
                        left = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                   ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                    case "yys":
                        right = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                    ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                    case "qys":
                        front = ysParts.FirstOrDefault(_ => _.BaoYangType == pair.BaoYangType)?.Pids?.Select(_ => _.Pid)
                                    ?.Distinct()?.ToList() ?? new List<string>();
                        break;
                }
            }

            TypeAdaptationResultModel<List<BaoYangPackageProductModel>> qStrategyResult =
                new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>();

            if (front != null)
            {
                qStrategyResult = await GetPackageItemProduct(front, "qys");
                result.Add(new BaoYangPackageItemModel()
                {
                    BaoYangType = "qys",
                    ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "qys")?.DisplayName,
                    GroupName = package.PackageType,
                    Products = qStrategyResult.Data,
                    InAdaptReason = qStrategyResult.Reason.ToString()
                });
            }

            if (left != null && right != null && left.Any() && right.Any())
            {
                var zyYsProduct = await GetWipersByPriority(left, right);
                if (zyYsProduct != null && zyYsProduct.Any())
                {
                    result.Add(new BaoYangPackageItemModel()
                    {
                        BaoYangType = "zys",
                        ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "zys")?.DisplayName,
                        GroupName = package.PackageType,
                        Products = zyYsProduct.Where(_ => left.Contains(_.Pid)).ToList(),
                        InAdaptReason = InAdaptableType.None.ToString()
                    });

                    result.Add(new BaoYangPackageItemModel()
                    {
                        BaoYangType = "yys",
                        ZhName = baoYangType.FirstOrDefault(_ => _.BaoYangType == "yys")?.DisplayName,
                        GroupName = package.PackageType,
                        Products = zyYsProduct.Where(_ => right.Contains(_.Pid)).ToList(),
                        InAdaptReason = InAdaptableType.None.ToString()
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 左右雨刷
        /// </summary>
        /// <param name="leftList"></param>
        /// <param name="rightList"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetWipersByPriority(List<string> leftList,
            List<string> rightList)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var leftProductTask = _baoYangService.GetProductByPidListAsync(leftList, _shopId);
            var rightProductTask = _baoYangService.GetProductByPidListAsync(rightList, _shopId);
            ////var productStockTask =
            ////    _baoYangService.GetBaoYangProductStock(leftList.Union(rightList).ToList(), _provinceId, _cityId);
            await Task.WhenAll(leftProductTask, rightProductTask);
            var leftProduct = leftProductTask.Result;
            var rightProduct = leftProductTask.Result;
            //var productStock = productStockTask.Result ?? new List<ProductStockModel>();
            if (leftProduct != null && leftProduct.Any() && rightProduct != null && rightProduct.Any())
            {
                //leftProduct.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                //rightProduct.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                List<BaoYangProductModel> tempWipers = new List<BaoYangProductModel>();
                tempWipers.AddRange(leftProduct);
                tempWipers.AddRange(rightProduct);
                var sameSeriesWiper = tempWipers.GroupBy(s => new {s.WiperSeries, s.Brand}).Where(s =>
                {
                    return s.Any()
                           && s.Any(w => leftList.Contains(w.Pid))
                           && s.Any(w => rightList.Contains(w.Pid));
                }).OrderByDescending(x =>
                {
                    return x.Where(_ => leftList.Contains(_.Pid) || rightList.Contains(_.Pid))
                        .All(_ => !_.StockOut);
                }).ToList();

                if (sameSeriesWiper.Any())
                {
                    var wipers = sameSeriesWiper[0];
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => leftList.Contains(w.Pid)), 1));
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => rightList.Contains(w.Pid)), 1));
                }
            }

            return result;
        }

        /// <summary>
        /// 左右减震器
        /// </summary>
        /// <param name="leftList"></param>
        /// <param name="rightList"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetJzqByPriority(List<string> leftList,
            List<string> rightList)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var leftProductTask = _baoYangService.GetProductByPidListAsync(leftList, _shopId);
            var rightProductTask = _baoYangService.GetProductByPidListAsync(rightList, _shopId);
            //var productStockTask =
            //    _baoYangService.GetBaoYangProductStock(leftList.Union(rightList).ToList(), _provinceId, _cityId);
            await Task.WhenAll(leftProductTask, rightProductTask);
            var leftProduct = leftProductTask.Result;
            var rightProduct = leftProductTask.Result;
            //var productStock = productStockTask.Result ?? new List<ProductStockModel>();
            if (leftProduct != null && leftProduct.Any() && rightProduct != null && rightProduct.Any())
            {
                //leftProduct.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                //rightProduct.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        productStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                List<BaoYangProductModel> tempWipers = new List<BaoYangProductModel>();
                tempWipers.AddRange(leftProduct);
                tempWipers.AddRange(rightProduct);
                var sameSeriesWiper = tempWipers.GroupBy(s => new {s.JzqSeries, s.Brand}).Where(s =>
                {
                    return s.Any()
                           && s.Any(w => leftList.Contains(w.Pid))
                           && s.Any(w => rightList.Contains(w.Pid));
                }).OrderByDescending(x =>
                {
                    return x.Where(_ => leftList.Contains(_.Pid) || rightList.Contains(_.Pid))
                        .All(_ => !_.StockOut);
                }).ToList();

                if (sameSeriesWiper.Any())
                {
                    var wipers = sameSeriesWiper[0];
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => leftList.Contains(w.Pid)), 1));
                    result.Add(ConvertProduct(wipers.FirstOrDefault(w => rightList.Contains(w.Pid)), 1));
                }
            }

            return result;
        }

        /// <summary>
        /// 前雨刷
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        private async Task<TypeAdaptationResultModel<List<BaoYangPackageProductModel>>> GetPackageItemProduct(
            List<string> productIds, string baoYangType)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;
            if (productIds != null && productIds.Any())
            {
                result = await _baoYangService.GetPartProduct(productIds, 1,
                    adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangType).ToList(), _provinceId, _cityId,
                    _shopId, 1);
                if (result == null || !result.Any())
                {
                    reason = InAdaptableType.NoProduct;
                }
            }
            else
            {
                reason = InAdaptableType.NoProduct;
            }

            return new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>()
            {
                Data = result,
                Reason = reason
            };
        }

        private async Task<List<BaoYangPackageItemModel>> GetPackageItemCommonAsync(BaoYangPackageDescription package,
            bool showAll)
        {
            string packageType = package.PackageType;
            var baoYangType = package.Items;
            List<BaoYangPackageItemModel> baoYangTypes = new List<BaoYangPackageItemModel>();
            switch (package.ProductType)
            {
                //只卖套餐
                case 0:
                    baoYangTypes.AddRange(await GetBaoYangPackageItemsForPackage(baoYangType, packageType, showAll));
                    break;
                //非套餐
                case 1:
                    baoYangTypes.AddRange(await GetBaoYangPackageItemsForProduct(baoYangType, packageType, showAll));
                    break;
                //两者都卖 暂时不考虑  
                case 2:
                    break;
            }

            return baoYangTypes;
        }

        /// <summary>
        /// 获取baoYangType为套餐的商品
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <param name="packageType"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageItemModel>> GetBaoYangPackageItemsForPackage(
            List<BaoYangTypeDescription> baoYangType, string packageType, bool showAll = false)
        {
            List<string> jiYouPackageType = new List<string>() { "xby-tc", "dby-tc" };
            List<BaoYangPackageItemModel> baoYangTypes = new List<BaoYangPackageItemModel>();
            baoYangType = baoYangType.Where(_ => _.Group == BaoYangTypeGroup.Package.ToString()).ToList();
            foreach (var baoYangItem in baoYangType)
            {
                string targetPid = _targetProducts.FirstOrDefault(x => x.BaoYangType == baoYangItem.BaoYangType)?.Pid;
                BaoYangPackageItemModel baoYang = new BaoYangPackageItemModel
                {
                    BaoYangType = baoYangItem.BaoYangType,
                    ZhName = baoYangItem.DisplayName,
                    GroupName = packageType
                };
                List<BaoYangPackageProductModel> defaultProduct = new List<BaoYangPackageProductModel>();
                var packageRef =
                    adaptations.PackageRef.FirstOrDefault(_ => _.BaoYangType == baoYangItem.BaoYangType);
                var packagePidList = packageRef?.Pids ?? new List<string>();
                if (jiYouPackageType.Contains(baoYangItem.BaoYangType) && !_jiYouAdaption)
                {
                    var products = await _baoYangService.GetProductByCategoryAsync(baoYangItem.CatalogName, 0);
                    packagePidList = products.Select(_ => _.Pid).ToList();
                }

                if (packagePidList.Any())
                {
                    if (showAll)
                    {
                        var adaptiveResult = await _baoYangService.GetPackageAllProductsAsync(packagePidList,
                            adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangItem.BaoYangType)
                                .ToList(), userId,
                            _channel, _provinceId, _cityId, adaptations.PartTypeDic, _vehicle, _shopId);
                        baoYang.Property = adaptiveResult.Item1;
                        defaultProduct = adaptiveResult.Item2;

                    }
                    else
                    {
                        var adaptiveResult =
                            await _baoYangService.GetAdaptationDefaultProductAsync(packagePidList, userId, _channel,
                                _provinceId, _cityId, adaptations.PartTypeDic,
                                adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangItem.BaoYangType)
                                    .ToList(), _vehicle, targetPid, _shopId);
                        if (adaptiveResult.Item1 != null)
                        {
                            baoYang.Property = adaptiveResult.Item1;
                        }
                        else
                        {
                            defaultProduct = adaptiveResult.Item2;
                        }
                    }
                }

                if (defaultProduct.Any())
                {
                    baoYang.Products = defaultProduct;
                }
                else if (baoYang.Property == null)
                {
                    baoYang.InAdaptReason = "暂无合适商品推荐";
                }

                baoYangTypes.Add(baoYang);
            }

            return baoYangTypes;
        }

        /// <summary>
        /// 获取baoYangType 非套餐的商品
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPackageItemModel>> GetBaoYangPackageItemsForProduct(
            List<BaoYangTypeDescription> baoYangType, string packageType, bool showAll)
        {
            List<BaoYangPackageItemModel> baoYangTypes = new List<BaoYangPackageItemModel>();
            baoYangType = baoYangType.Where(_ => _.Group != BaoYangTypeGroup.Package.ToString()).ToList();
            foreach (var baoYangItem in baoYangType)
            {
                string byType = baoYangItem.BaoYangType;
                BaoYangPackageItemModel baoYang = new BaoYangPackageItemModel();
                if (baoYangItem.Group == BaoYangTypeGroup.Accessory.ToString())
                {
                    BaoYangAccessoryModel accessory =
                        adaptations.AccessoryTypeDic.FirstOrDefault(_ =>
                            _.BaoYangType == byType);
                    baoYang = await GetPackageItemByBaoYangTypeAsync(byType, baoYangItem.CatalogName, accessory);

                }
                else if (baoYangItem.Group == BaoYangTypeGroup.Part.ToString())
                {
                    BaoYangPartModel part =
                        adaptations.PartTypeDic.FirstOrDefault(
                            _ => _.BaoYangType == byType);
                    baoYang =
                        await GetPackageItemByBaoYangTypeAsync(byType, part, showAll);
                }
                else if (baoYangItem.Group == BaoYangTypeGroup.Maintainence.ToString())
                {
                    baoYang =
                        await GetPackageItemByBaoYangType(byType, baoYangItem.CatalogName, showAll);
                }

                baoYang.GroupName = packageType;
                baoYang.ZhName = baoYangItem.DisplayName;
                baoYangTypes.Add(baoYang);
            }

            return baoYangTypes;
        }

        /// <summary>
        /// 根据推荐策略推荐产品
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <param name="category"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<BaoYangPackageItemModel> GetPackageItemByBaoYangType(string baoYangType, string category,
            bool showAll)
        {
            List<BaoYangPackageProductModel> products = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;
            var mainProduct =
                await _baoYangService.GetMaintainenceProduct(category,
                    adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangType).ToList(), _provinceId, _cityId,
                    _shopId, showAll ? 0 : 1);
            if (mainProduct != null && mainProduct.Any())
            {
                products = mainProduct;
            }
            else
            {
                reason = InAdaptableType.NoProduct;
            }

            return new BaoYangPackageItemModel
            {
                BaoYangType = baoYangType,
                Products = products,
                InAdaptReason = reason.ToString()
            };
        }

        /// <summary>
        /// 通用配件
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <param name="part"></param>
        /// <param name="showAll"></param>
        /// <returns></returns>
        private async Task<BaoYangPackageItemModel> GetPackageItemByBaoYangTypeAsync(string baoYangType, BaoYangPartModel part, bool showAll)
        {
            List<BaoYangPackageProductModel> products = new List<BaoYangPackageProductModel>();
            PropertySelectModel property = null;
            InAdaptableType reason = InAdaptableType.None;
            ;
            if (part != null)
            {
                List<string> productIds = part.Pids?.Select(_ => _.Pid)?.Distinct()?.ToList();
                if (productIds != null && productIds.Any())
                {
                    products = await _baoYangService.GetPartProduct(productIds, part.Count,
                        adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangType).ToList(), _provinceId,
                        _cityId,
                        _shopId, showAll ? 0 : 1);
                    if (products == null || !products.Any())
                    {
                        reason = InAdaptableType.NoProduct;
                    }
                }
                else
                {
                    reason = InAdaptableType.NoProduct;
                }
            }
            else
            {
                reason = InAdaptableType.NoData;
            }

            return new BaoYangPackageItemModel
            {
                BaoYangType = baoYangType,
                Products = products,
                Property = property,
                InAdaptReason = reason.ToString()
            };
        }

        /// <summary>
        /// 辅料适配到产品
        /// </summary>
        /// <param name="baoYangType"></param>
        /// <param name="category"></param>
        /// <param name="accessory"></param>
        /// <returns></returns>
        private async Task<BaoYangPackageItemModel> GetPackageItemByBaoYangTypeAsync(string baoYangType, string category, BaoYangAccessoryModel accessory)
        {
            string dataTip = string.Empty;
            List<BaoYangPackageProductModel> products = null;
            InAdaptableType reason = InAdaptableType.None;
            PropertySelectModel property = null;
            string tips = string.Empty;
            switch (baoYangType)
            {
                //机油
                case "jiyou":
                    if (accessory == null)
                    {
                        reason = InAdaptableType.NoData;
                        break;
                    }
                    OilModel oil = new OilModel(accessory);
                    if (oil.IsValid())
                    {
                        dataTip = oil.Volume.ToString("F1") + "L";
                        var jiYouResult = await GetPackageItemForJiYouAsync(oil, category);
                        products = jiYouResult.Data;
                        reason = jiYouResult.Reason;
                        tips = jiYouResult.Tips;
                    }
                    else
                    {
                        reason = InAdaptableType.InValidData;
                    }
                    break;
                //空调制冷剂
                case "lm":
                    if (accessory == null)
                    {
                        reason = InAdaptableType.NoData;
                        break;
                    }
                    decimal volume = 0;
                    if (decimal.TryParse(accessory.Volume, out volume) && volume > 0)
                    {
                        var lmResult = await GetPackageItemByVolumeAsync(baoYangType, category, null, volume,
                            GlobalConstant.RefrigerantUnitFix);
                        products = lmResult.Data;
                        reason = lmResult.Reason;
                        tips = lmResult.Tips;
                    }
                    else
                    {
                        reason = InAdaptableType.InValidData;
                    }
                    break;
                //发动机防冻液
                case "gfd":
                    if (accessory == null)
                    {
                        reason = InAdaptableType.NoData;
                        break;
                    }
                    decimal fdVolume = 0;
                    if (decimal.TryParse(accessory.Volume, out fdVolume) && fdVolume > 0)
                    {
                        var fdResult = await GetPackageItemByVolumeAsync(baoYangType, category, null, fdVolume,
                            GlobalConstant.DefaultAccessoryUnitFix);
                        products = fdResult.Data;
                        reason = fdResult.Reason;
                        tips = fdResult.Tips;
                    }
                    else
                    {
                        reason = InAdaptableType.InValidData;
                    }
                    break;
                //制动液
                case "scy":
                    if (accessory == null)
                    {
                        reason = InAdaptableType.NoData;
                        break;
                    }
                    BrakeFluidModel scyModel = new BrakeFluidModel(accessory);
                    if (scyModel.IsValid())
                    {
                        var scyResult = await GetPackageItemByVolumeAsync(baoYangType, category, null,
                            scyModel.Volume, GlobalConstant.DefaultAccessoryUnitFix, scyModel.Level);
                        products = scyResult.Data;
                        reason = scyResult.Reason;
                        tips = scyResult.Tips;
                    }
                    else
                    {
                        reason = InAdaptableType.InValidData;
                    }
                    break;
                //变速箱油
                case "mmto":
                    if (accessory == null)
                    {
                        reason = InAdaptableType.NoData;
                        break;
                    }
                    TransFluidModel trsModel = new TransFluidModel(accessory);

                    if (trsModel.IsValid())
                    {
                        var mtoResult = await GetPackageItemByVolumeAsync(baoYangType, category, null,
                            trsModel.Volume, "L");
                        products = mtoResult.Data;
                        reason = mtoResult.Reason;
                        tips = mtoResult.Tips;
                    }
                    else
                    {
                        reason = InAdaptableType.InValidData;
                    }
                    break;
                default:
                    var defaultResult = await GetPackageItemAsync(baoYangType, category);
                    products = defaultResult.Data;
                    reason = defaultResult.Reason;
                    tips = defaultResult.Tips;
                    break;
            }

            return new BaoYangPackageItemModel
            {
                BaoYangType = baoYangType,
                DataTip = dataTip,
                Products = products,
                Property = property,
                InAdaptReason = reason.ToString()
            };
        }

        private async Task<TypeAdaptationResultModel<List<BaoYangPackageProductModel>>> GetPackageItemForJiYouAsync(OilModel oilConfig, string category)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;

            /*1.级别-品牌优先级
            2.特殊车型 
            3.用户推荐 > 4.地区推荐 > 5. 之前下单推荐*/

            var products = await _baoYangService.GetProductByCategoryAsync(category, _shopId);
            if (products != null && products.Any())
            {
                result = await _baoYangService.SelectBaoYangJiYouBySetting(oilConfig.Volume, oilConfig.Level,
                    oilConfig.Description, oilConfig.Viscosity, products, _provinceId, _cityId);
                if (result == null || !result.Any())
                {
                    reason = InAdaptableType.NoProduct;
                }
            }
            else
            {
                reason = InAdaptableType.NoProduct;
            }

            return new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>()
            {
                Data = result,
                Reason = reason
            };
        }


        /// <summary>
        /// 机油可用粘度
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <param name="viscosity"></param>
        /// <returns></returns>
        private async Task<List<BaoYangProductModel>> SelectBaoYangJiYouBySetting(decimal volume, string level,
            string description, string viscosity)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
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

            if (allOilViscosity.Any())
            {
                result = await FilterAllOilViscosity(volume, level, description, allOilViscosity);
            }

            return result;
        }

        /// <summary>
        /// 机油级别规格推荐
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <param name="viscosity"></param>
        /// <returns></returns>
        private async Task<List<BaoYangProductModel>> FilterAllOilViscosity(decimal volume, string level,
            string description, List<string> viscosity)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            foreach (var viscosityItem in viscosity)
            {
                var oilProducts = await _baoYangService.GetOilProductsByViscosity(viscosityItem); //符合当前车型粘度的产品
                if (oilProducts != null && oilProducts.Any())
                {
                    var availLevel = new List<string>();
                    if (!string.IsNullOrEmpty(level) && level.Contains("HX"))
                    {
                        availLevel = _allLevel.Where(x => x.Contains("HX")).Where(x =>
                                Convert.ToInt32(Regex.Replace(x, "\\D", "")) >=
                                Convert.ToInt32(Regex.Replace(level, "\\D", "")))
                            .OrderBy(x => Convert.ToInt32(Regex.Replace(x, "\\D", ""))).ToList();
                        availLevel.Add("ULTRA");
                    }
                    else if (!string.IsNullOrEmpty(level))
                    {
                        availLevel.Add(level);
                    }

                    var availDescription = new List<string>();
                    if (!string.IsNullOrEmpty(description) && description.Length > 1)
                    {
                        char seriesChar = description.ToCharArray()[0]; //目前有S，C,A
                        var nextChar = description.ToCharArray()[1];
                        availDescription = _allDescription.Where(x => x.StartsWith(seriesChar))
                            .Where(x => x.Substring(1, 1).ToCharArray()[0] >= nextChar).OrderBy(x => x.Substring(1, 1))
                            .ToList();
                    }
                    else
                    {
                        availDescription = _allDescription.OrderBy(x => x.Substring(1, 1)).ToList();
                    }

                    result = await DefaultFilterOil(oilProducts, availLevel, availDescription, volume, null);
                    if (result != null && result.Any())
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 4L 1L机油
        /// </summary>
        /// <param name="oilProducts"></param>
        /// <param name="levels"></param>
        /// <param name="descriptions"></param>
        /// <param name="volume"></param>
        /// <param name="brand"></param>
        /// <returns></returns>
        private async Task<List<BaoYangProductModel>> DefaultFilterOil(List<BaoYangProductModel> oilProducts,
            List<string> levels, List<string> descriptions, decimal volume, string brand)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            foreach (var levelItem in levels)
            {
                foreach (var desItem in descriptions)
                {
                    if (!string.IsNullOrWhiteSpace(brand))
                    {
                        result =
                            oilProducts.Where(
                                x =>
                                    !string.IsNullOrEmpty(x.Level) && !string.IsNullOrEmpty(x.Brand) &&
                                    !string.IsNullOrEmpty(x.Specification) &&
                                    x.Level.Equals(levelItem) && x.Brand.Equals(brand) &&
                                    string.Equals(x.Specification, desItem, StringComparison.OrdinalIgnoreCase)).ToList();

                    }
                    else
                    {
                        result =
                            oilProducts.Where(
                                x =>
                                    !string.IsNullOrEmpty(x.Level) && !string.IsNullOrEmpty(x.Specification) &&
                                    x.Level.Equals(levelItem) && string.Equals(x.Specification, desItem,
                                        StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    volume = Math.Ceiling(volume);
                    if ((volume + 1) % 4 == 0 || volume % 4 == 0)
                    {
                        result = result.Where(o =>
                            string.Equals(o.Unit, _jiYouUnit4, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (result.Exists(_ => _.Unit.Equals(_jiYouUnit4)) && result.Exists(_ => _.Unit.Equals(_jiYouUnit1)))
                    {
                        var groups = result.GroupBy(x => new { x.Brand, x.OilSeries }); //需要4L和1L,参数要一样
                        bool isExistGroups = false;
                        foreach (var group in groups)
                        {
                            var groupList = group.ToList();
                            if (groupList.Any() && groupList.Exists(x => x.Unit.Equals(_jiYouUnit4)) &&
                                groupList.Exists(x => x.Unit.Equals(_jiYouUnit1)))
                            {
                                isExistGroups = true;
                                result =
                                    result.Where(
                                        m =>
                                            (m.Unit.Equals(_jiYouUnit4) || m.Unit.Equals(_jiYouUnit1)) &&
                                            string.Equals(m.Brand, group.Key.Brand) &&
                                            (string.Equals(m.OilSeries, group.Key.OilSeries) ||
                                             (string.IsNullOrEmpty(m.OilSeries) &&
                                              string.IsNullOrEmpty(group.Key.OilSeries)))).ToList();
                                break;
                            }
                        }
                        if (!isExistGroups)
                        {
                            result = new List<BaoYangProductModel>();
                        }
                    }

                    if (result.Any())
                    {
                        break;
                    }
                }
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 根据容量获得各个产品的数量:值返回单品 规格不统一的
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="products"></param>
        /// <param name="unitFix"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetQuantityByVolumeSingle(decimal volume,
            List<BaoYangProductModel> products, string unitFix)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();

            products.ForEach(_ =>
            {
                int count = 1;
                string unitStr = _.Unit;
                decimal unit = 0;
                if (!string.IsNullOrEmpty(unitStr) &&
                    decimal.TryParse(unitStr.Replace(unitFix, string.Empty), out unit))
                {
                    if (unit > 0)
                    {
                        count = (int) Math.Ceiling(volume / unit);
                    }
                }

                _.Count = count;
            });

            products = products.OrderBy(_ => _.StockOut)
                .ThenBy(_ => _.Count * _.Price).ToList();
            if (products.Any())
            {
                var defaultProduct = products[0];
                result.Add(ConvertProduct(defaultProduct, defaultProduct.Count));
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 根据容量获得各个产品的数量
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="products"></param>
        /// <param name="unitFix"></param>
        /// <returns></returns>
        private async Task<List<BaoYangPackageProductModel>> GetQuantityByVolume(decimal volume,
            List<BaoYangProductModel> products, string unitFix)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            if (products != null && products.Any())
            {
                if (products.Count == 1)
                {
                    var product = products[0];
                    string unitStr = product.Unit;
                    decimal unit = 0;
                    int quantity = 0;

                    if (unitStr != null && decimal.TryParse(unitStr.Replace(unitFix, string.Empty), out unit))
                    {
                        quantity = (int)Math.Ceiling((decimal)volume / unit);
                    }
                    else
                    {
                        quantity = 1;
                    }

                    result.Add(ConvertProduct(product, quantity));
                }
                else
                {
                    Dictionary<BaoYangProductModel, decimal> dic = new Dictionary<BaoYangProductModel, decimal>();

                    foreach (var product in products)
                    {
                        string unitStr = product.Unit;
                        decimal unit = 0;
                        if (!string.IsNullOrEmpty(unitStr) &&
                            decimal.TryParse(unitStr.Replace(unitFix, string.Empty), out unit))
                        {
                            dic.Add(product, unit);
                        }
                    }

                    result = SolveInEquation(dic, volume).ToList();
                }
            }

            return await Task.FromResult(result);
        }


        /// <summary>
        /// 解不等式方程x*a+y*b+...+z*c>=result, a,b,...,c为已知数, 求x,y,...,z(只有两个有值，其它都为0)
        /// </summary>
        /// <param name="products"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        private List<BaoYangPackageProductModel> SolveInEquation(
            Dictionary<BaoYangProductModel, decimal> products, decimal volume)
        {
            List<BaoYangPackageProductModel> answer = new List<BaoYangPackageProductModel>();

            //先给已知数排序,降序排序
            List<decimal> numbers = new List<decimal>();
            List<BaoYangProductModel> data = new List<BaoYangProductModel>();

            var sorted = products.OrderByDescending(o => o.Value);
            for (int index = 0; index < sorted.Count(); index++)
            {
                var pair = sorted.ElementAt(index);
                numbers.Add(pair.Value);
                data.Add(pair.Key);
            }

            List<List<Tuple<int, decimal, BaoYangProductModel>>> results =
                new List<List<Tuple<int, decimal, BaoYangProductModel>>>();

            for (int i = 0; i < numbers.Count - 1; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    List<Tuple<int, decimal, BaoYangProductModel>> item =
                        new List<Tuple<int, decimal, BaoYangProductModel>>();

                    decimal volume1 = numbers[i];
                    decimal volume2 = numbers[j];

                    int count1 = (int)Math.Floor(volume / volume1);
                    count1 = count1 == 0 ? 1 : count1;
                    int count2 = (int)Math.Ceiling((volume - count1 * volume1) / volume2);

                    item.Add(Tuple.Create(count1, volume1, data[i]));

                    if (count2 > 0)
                    {
                        item.Add(Tuple.Create(count2, volume2, data[j]));
                    }

                    results.Add(item);

                    if (volume >= volume1)
                    {
                        List<Tuple<int, decimal, BaoYangProductModel>> item2 =
                            new List<Tuple<int, decimal, BaoYangProductModel>>();
                        int count3 = (int)Math.Ceiling(volume / volume1);
                        item2.Add(Tuple.Create(count3, volume1, data[i]));
                        results.Add(item2);
                    }
                }
            }

            var best = results.OrderBy(o => o.Sum(item => item.Item1 * item.Item2))
                .ThenBy(o => o.Count)
                .ThenBy(o => o.Sum(item => item.Item1))
                .ThenBy(o => o.Sum(item => item.Item1 * item.Item3.Price))
                .FirstOrDefault();

            foreach (var item in best)
            {
                var product = item.Item3;
                answer.Add(ConvertProduct(product, item.Item1));
            }
            return answer;
        }

        private async Task<TypeAdaptationResultModel<List<BaoYangPackageProductModel>>> GetPackageItemAsync(
            string baoYangType, string category)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;

            var mainProduct =
                await _baoYangService.GetMaintainenceProduct(category,
                    adaptations.ProductPriority.Where(_ => _.BaoYangType == baoYangType).ToList(), _provinceId, _cityId,
                    _shopId, 1);
            if (mainProduct != null && mainProduct.Any())
            {
                result = mainProduct;
            }
            else
            {
                reason = InAdaptableType.NoProduct;
            }

            return new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>()
            {
                Data = result,
                Reason = reason
            };
        }

        private async Task<TypeAdaptationResultModel<List<BaoYangPackageProductModel>>> GetPackageItemByVolumeAsync(string baoYangType, string category, List<string> productIds, decimal volume,
            string unitFix, int level = 0)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            InAdaptableType reason = InAdaptableType.None;
            List<BaoYangProductModel> products = new List<BaoYangProductModel>();
            if (productIds != null && productIds.Any())
            {
                products = await _baoYangService.GetProductByPidListAsync(productIds, _shopId);
            }
            else
            {
                products = await _baoYangService.GetProductByCategoryAsync(category, _shopId);
            }

            if (products != null && products.Any())
            {
                //var shopStock =
                //    await _baoYangService.GetBaoYangProductStock(products.Select(_ => _.Pid).ToList(), _provinceId,
                //        _cityId) ?? new List<ProductStockModel>();
                //products.ForEach(_ =>
                //{
                //    _.AvailableStockQuantity =
                //        shopStock.FirstOrDefault(t => t.Pid == _.Pid)?.AvailableStockQuantity ?? 0;
                //});
                switch (baoYangType)
                {
                    case "lm":
                        result = await GetQuantityByVolumeSingle(volume, products, unitFix);
                        break;
                    case "gfd":
                        result = await GetRecommendProductForAntifreeze(volume, products);
                        break;
                    case "scy":
                        products = FilterScyLevel(products, level);
                        result = await GetQuantityByVolumeSingle(volume, products, unitFix);
                        break;
                    case "ato":
                    case "mmto":
                        products.ForEach(prodcut =>
                        {
                            if (string.Equals(prodcut.Unit, "0.946L", StringComparison.OrdinalIgnoreCase))
                            {
                                prodcut.Unit = "1L";
                            }
                        });
                        result = await GetProducts(baoYangType, products, volume);
                        break;
                }
            }

            if (result == null || !result.Any())
            {
                reason = InAdaptableType.NoProduct;
            }

            return new TypeAdaptationResultModel<List<BaoYangPackageProductModel>>()
            {
                Data = result,
                Reason = reason
            };
        }


        private async Task<List<BaoYangPackageProductModel>> GetProducts(string type, List<BaoYangProductModel> products, decimal volume)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            if (products != null && products.Any())
            {
                var count1 = Convert.ToInt32(volume % 4);
                var count2 = Convert.ToInt32(volume / 4);
                var product1 =
                    products.Where(t => string.Equals(t.Unit, "1L", StringComparison.CurrentCultureIgnoreCase)).ToList();
                var product2 =
                    products.Where(t => string.Equals(t.Unit, "4L", StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (count2 > 0 && count1 > 0)
                {
                    List<List<BaoYangProductModel>> resultTemp =
                        new List<List<BaoYangProductModel>>();
                    foreach (var p1 in product2)
                    {
                        foreach (var p2 in product1)
                        {
                            p1.Count = count2;
                            p2.Count = count1;
                            if (type == "mmto")
                            {
                                if (string.Equals(p1.Brand, p2.Brand, StringComparison.OrdinalIgnoreCase))
                                {
                                    resultTemp.Add(new List<BaoYangProductModel>() {p1, p2});
                                    break;
                                }
                            }
                            else
                            {
                                if (string.Equals(p1.AtoSeries, p2.AtoSeries, StringComparison.OrdinalIgnoreCase))
                                {
                                    resultTemp.Add(new List<BaoYangProductModel>() {p1, p2});
                                    break;
                                }
                            }
                        }
                    }

                    if (resultTemp.Any())
                    {
                        var defaultProducts = resultTemp.OrderByDescending(_ => { return _.All(t => !t.StockOut); }).ThenBy(_ => _.Sum(t => t.Count * t.Price)).ToList()[0];
                        defaultProducts.ForEach(_ => { result.Add(ConvertProduct(_, _.Count)); });

                    }
                    else if (product1.Any())
                    {
                        result.Add(ConvertProduct(
                            product1.OrderBy(_ => _.StockOut)
                                .ThenBy(_ => _.Price).FirstOrDefault(), count2 * 4 + count1));
                    }
                    else if (product2.Any())
                    {
                        result.Add(ConvertProduct(
                            product2.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price)
                                .FirstOrDefault(), count2 + 1));
                    }
                }
                else if (count2 > 0)
                {
                    if (product2.Any())
                    {
                        result.Add(ConvertProduct(
                            product2.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price)
                                .FirstOrDefault(), count2));
                    }
                    else if (product1.Any())
                    {
                        result.Add(ConvertProduct(
                            product1.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price)
                                .FirstOrDefault(), count2 * 4));
                    }
                }
                else
                {
                    if (product1.Any())
                    {
                        result.Add(ConvertProduct(
                            product1.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price)
                                .FirstOrDefault(), count1));
                    }
                    else if (product2.Any())
                    {
                        result.Add(ConvertProduct(
                            product2.OrderBy(_ => _.StockOut).ThenBy(_ => _.Price)
                                .FirstOrDefault(), 1));
                    }
                }
            }

            return await Task.FromResult(result);
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

        private async Task<List<BaoYangPackageProductModel>> GetRecommendProductForAntifreeze(decimal volume,
            List<BaoYangProductModel> products)
        {
            List<BaoYangPackageProductModel> result = new List<BaoYangPackageProductModel>();
            var setting = await _baoYangService.GetAntifreezeSetting();
            if (products != null && products.Any() && volume >= 2 && !string.IsNullOrEmpty(_cityId))
            {
                bool isOnly4L = volume % 4 == 0 || volume % 4 > 2;
                List<BaoYangProductModel> goalProduct =
                    await GetAntifreezeProducts(setting, products, _cityId, volume, isOnly4L, false);
                if (goalProduct == null || !goalProduct.Any()) //4L的没有时用2L装
                {
                    goalProduct = await GetAntifreezeProducts(setting, products, _cityId, volume, false, true);
                }

                if (goalProduct != null && goalProduct.Any())
                {
                    goalProduct.ForEach(_ => { result.Add(ConvertProduct(_, _.Count)); });
                }
            }

            return result;
        }

        private async Task<List<BaoYangProductModel>> GetAntifreezeProducts(List<AntifreezeSettingModel> setting,
            List<BaoYangProductModel> products, string region, decimal volume, bool isOnly4L = false,
            bool isOnly2L = false)
        {
            List<BaoYangProductModel> result = new List<BaoYangProductModel>();
            AntifreezeSettingModel localSetting = null;
            if (setting != null && setting.Any())
            {
                localSetting = setting.FirstOrDefault(x => x.ProvinceIds.Contains(region.ToString()));

                // 如果没有配置，就默认上海市的配置
                if (localSetting == null)
                {
                    localSetting =
                        setting.FirstOrDefault(x => x.ProvinceIds.Contains(GlobalConstant.DefaultProvinceId));
                }
            }

            int defaultPoint = Convert.ToInt32(localSetting?.FreezingPoint ?? GlobalConstant.DefaultAntifreezePoint);

            var availableProducts = products
                .Where(_ => Convert.ToInt32(_.AntifreezePoint) <= defaultPoint && _.Color.Equals("通用")).ToList();

            if (isOnly4L)
            {
                int count = (int) Math.Ceiling(volume / 4);
                availableProducts = availableProducts.Where(_ => _.Unit.Equals("4L"))
                    .OrderBy(_ => _.StockOut)
                    .ThenBy(_ => _.AntifreezePoint).ThenBy(_ =>
                        localSetting != null && _.Brand.Equals(localSetting.Brand) ? 1 : 0).ThenBy(_ => _.Price)
                    .ToList();
                if (availableProducts.Any())
                {
                    var goalProduct = availableProducts[0];
                    goalProduct.Count = count;
                    result.Add(goalProduct);
                }
            }
            else if (isOnly2L)
            {
                int count = (int) Math.Ceiling(volume / 2);
                availableProducts = availableProducts
                    .Where(_ => _.Unit.Equals("2L")).OrderBy(_ => _.StockOut)
                    .ThenBy(_ => _.AntifreezePoint).ThenBy(_ =>
                        localSetting != null && _.Brand.Equals(localSetting.Brand) ? 1 : 0).ThenBy(_ => _.Price)
                    .ToList();
                if (availableProducts.Any())
                {
                    var goalProduct = availableProducts[0];
                    goalProduct.Count = count;
                    result.Add(goalProduct);
                }
            }
            else
            {
                var count1 = 1; //2L
                var count2 = Convert.ToInt32(volume / 4); //4L
                List<string> product2 = new List<string>();
                List<string> product4 = new List<string>();
                availableProducts.ForEach(_ =>
                {
                    if (_.Unit.Equals("2L"))
                    {
                        product2.Add(_.Pid);
                        _.Count = count1;
                    }
                    else if (_.Unit.Equals("4L"))
                    {
                        product4.Add(_.Pid);
                        _.Count = count2;
                    }
                });

                var sameSeriesWiper = availableProducts.GroupBy(s => new {s.AntifreezePoint, s.BoilingPoint, s.Brand})
                    .Where(s =>
                    {
                        return s.Any()
                               && s.Any(w => product2.Contains(w.Pid))
                               && s.Any(w => product4.Contains(w.Pid));
                    }).OrderByDescending(x =>
                    {
                        return x.Where(_ => product2.Contains(_.Pid) || product4.Contains(_.Pid))
                            .All(_ => !_.StockOut);
                    }).ThenBy(_ => _.Key.AntifreezePoint)
                    .ThenBy(_ => localSetting != null && _.Key.Brand.Equals(localSetting.Brand) ? 1 : 0)
                    .ThenBy(_ => _.Sum(t => t.Count * t.Price)).ToList();

                if (sameSeriesWiper.Any())
                {
                    var wipers = sameSeriesWiper[0];
                    result.Add(wipers.FirstOrDefault(w => product4.Contains(w.Pid)));
                    result.Add(wipers.FirstOrDefault(w => product2.Contains(w.Pid)));
                }
            }

            return await Task.FromResult(result);
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
                return new BaoYangPackageProductModel
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
                    StockOut = product.StockOut, //|| product.AvailableStockQuantity < count,
                    IsOriginal = product.IsOriginal
                };
            }

            return null;
        }
    }
}
