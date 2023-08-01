using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Redis;
using Ae.BaoYang.Service.Client.Clients;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Common.Common;
using Ae.BaoYang.Service.Common.Exceptions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Repository;
using Ae.BaoYang.Service.Imp.Model;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Core.Request.Config;
using Ae.BaoYang.Service.Dal.Model.Extend;
using Ae.BaoYang.Service.Core.Response.Config;
using Ae.BaoYang.Service.Imp.Enum;
using Ae.BaoYang.Service.Common.Helper;
using System.Text.RegularExpressions;
using Ae.BaoYang.Service.Core.Model;
using ApolloErp.Web.WebApi;
using System.Transactions;
using Ae.BaoYang.Service.Dal.Model.Condition;

namespace Ae.BaoYang.Service.Imp.Services
{
    /// <summary>
    /// 保养配置Service
    /// </summary>
    public class BaoYangConfigService : IBaoYangConfigService
    {
        private readonly IMapper _mapper;
        private readonly IBaoYangConfigRepository _baoYangConfigRepository;
        private readonly IBaoYangPartAccessoryRepository _baoYangPartAccessoryRepository;
        private readonly IBaoYangPartsRepository _baoYangPartsRepository;
        private readonly IBaoYangProductRefRepository _baoYangProductRefRepository;
        private readonly ProductClient _productClient;
        private readonly IBaoYangPackageRefRepository _baoYangPackageRefRepository;
        private readonly IBaoYangTypeConfigRepository _baoYangTypeConfigRepository;
        private readonly IBaoYangPartsMapConfigRepository _baoYangPartsMapConfigRepository;
        private readonly IVehicleTypeTimingRepository _vehicleTypeTimingRepository;
        private readonly IBaoYangPartAccessoryMapRepository _baoYangPartAccessoryMapRepository;
        private readonly string redisKey = "Rg:BaoYang:Service:BaoYangConfig";
        private readonly RedisClient _redisClient;
        private readonly IBaoYangService _baoYangService;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IBaoYangPropertyAdaptationRepository _baoYangPropertyAdaptationRepository;
        private readonly IBaoYangOeCodeMapRepository _baoYangOeCodeMapRepository;
        private readonly IBaoYangPropertyDescriptionRepository _baoYangPropertyDescriptionRepository;
        private readonly IBaoYangPackageConfigRepository _baoYangPackageConfigRepository;
        private readonly IBaoYangPackageMapConfigRepository _baoYangPackageMapConfigRepository;
        private readonly IBaoYangCategoryConfigRepository _baoYangCategoryConfigRepository;
        private readonly IBaoYangInstallFeeConfigRepository _baoYangInstallFeeConfigRepository;

        private readonly List<string> _scyLevel = new List<string>() {"DOT-3", "DOT-4"};

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

        public BaoYangConfigService(IMapper mapper, IBaoYangConfigRepository baoYangConfigRepository,
            IBaoYangPartAccessoryRepository baoYangPartAccessoryRepository,
            IBaoYangPartsRepository baoYangPartsRepository, IBaoYangProductRefRepository baoYangProductRefRepository,
            ProductClient productClient, IBaoYangPackageRefRepository baoYangPackageRefRepository,
            IBaoYangTypeConfigRepository baoYangTypeConfigRepository,
            IBaoYangPartsMapConfigRepository baoYangPartsMapConfigRepository,
            IVehicleTypeTimingRepository vehicleTypeTimingRepository, RedisClient redisClient,
            IBaoYangPartAccessoryMapRepository baoYangPartAccessoryMapRepository, IBaoYangService baoYangService,
            IVehicleTypeRepository vehicleTypeRepository,
            IBaoYangPropertyAdaptationRepository baoYangPropertyAdaptationRepository,
            IBaoYangOeCodeMapRepository baoYangOeCodeMapRepository,
            IBaoYangPropertyDescriptionRepository baoYangPropertyDescriptionRepository,
            IBaoYangPackageConfigRepository baoYangPackageConfigRepository,
            IBaoYangPackageMapConfigRepository baoYangPackageMapConfigRepository,
            IBaoYangCategoryConfigRepository baoYangCategoryConfigRepository,
            IBaoYangInstallFeeConfigRepository baoYangInstallFeeConfigRepository)
        {
            _mapper = mapper;
            _baoYangConfigRepository = baoYangConfigRepository;
            _baoYangPartAccessoryRepository = baoYangPartAccessoryRepository;
            _baoYangPartsRepository = baoYangPartsRepository;
            _baoYangProductRefRepository = baoYangProductRefRepository;
            _productClient = productClient;
            _baoYangPackageRefRepository = baoYangPackageRefRepository;
            _baoYangTypeConfigRepository = baoYangTypeConfigRepository;
            _baoYangPartsMapConfigRepository = baoYangPartsMapConfigRepository;
            _vehicleTypeTimingRepository = vehicleTypeTimingRepository;
            _redisClient = redisClient;
            _baoYangPartAccessoryMapRepository = baoYangPartAccessoryMapRepository;
            _baoYangService = baoYangService;
            _vehicleTypeRepository = vehicleTypeRepository;
            _baoYangPropertyAdaptationRepository = baoYangPropertyAdaptationRepository;
            _baoYangOeCodeMapRepository = baoYangOeCodeMapRepository;
            _baoYangPropertyDescriptionRepository = baoYangPropertyDescriptionRepository;
            _baoYangPackageConfigRepository = baoYangPackageConfigRepository;
            _baoYangPackageMapConfigRepository = baoYangPackageMapConfigRepository;
            _baoYangCategoryConfigRepository = baoYangCategoryConfigRepository;
            _baoYangInstallFeeConfigRepository = baoYangInstallFeeConfigRepository;
        }

        #region 配件

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetPartNameResponse>> GetPartNameListAsync()
        {
            List<GetPartNameResponse> result = new List<GetPartNameResponse>();
            var baoYangAdaptation = await GetBaoYangAdaptationConfigAsync();
            if (baoYangAdaptation != null)
            {
                foreach (var baoYangItem in baoYangAdaptation.ProductAdaptations ?? new List<ProductAdaptation>())
                {
                    result.Add(new GetPartNameResponse
                    {
                        DisplayName = baoYangItem.DisplayName,
                        PartNames = baoYangItem.PartNames
                            .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        Brands = baoYangItem.Brands?.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                            ?.ToList() ?? new List<string>(),
                        IsSpecialPart = false
                    });
                }

                foreach (var specialItem in baoYangAdaptation.SpecialProductAdaptations ??
                                            new List<SpecialProductAdaptation>())
                {
                    result.Add(new GetPartNameResponse
                    {
                        DisplayName = specialItem.DisplayName,
                        PartNames = specialItem.PartNames
                            .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        Brands = specialItem.Brands?.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                            ?.ToList() ?? new List<string>(),
                        IsSpecialPart = true
                    });
                }
            }

            return result;
        }

        private async Task<BaoYangAdaptationConfig> GetBaoYangAdaptationConfigAsync()
        {
            const string configName = "BaoYangAdaptation";
            BaoYangAdaptationConfig result = null;
            try
            {
                var configXml = await _baoYangConfigRepository.FetchBaoYangConfigByKeyAsync(configName);
                result = XmlHelper.Deserialize<BaoYangAdaptationConfig>(configXml);
            }
            catch (Exception ex)
            {
                //todo Error
            }

            return result;
        }

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetBaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            GetBaoYangPartAdaptationsRequest request)
        {
            var mapConfig = (await GetBaoYangPartsMapConfigCache())?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
            List<string> specialPart = mapConfig.Where(x => x.RefType == "PartNo").Select(x => x.PartNames).ToList();
            List<GetBaoYangPartAdaptationsResponse> result = new List<GetBaoYangPartAdaptationsResponse>();
            var partAdaptationTask =
                _baoYangConfigRepository.GetBaoYangPartAdaptationsAsync(request.TidList, specialPart);
            var configTask = GetBaoYangAdaptationConfigAsync();
            await Task.WhenAll(partAdaptationTask, configTask);
            var partAdaptation = partAdaptationTask.Result?.ToList() ?? new List<BaoYangPartsAdaptationDO>();
            var config = configTask.Result;
            foreach (var tid in request.TidList)
            {
                GetBaoYangPartAdaptationsResponse itemPartAdaptation = new GetBaoYangPartAdaptationsResponse
                {
                    Tid = tid
                };
                List<BaoYangPartsAdaptationDO> partsAdaptation = partAdaptation.Where(t => t.Tid == tid).ToList();
                List<BaoYangPartsAdaptationDetail> adaptations = new List<BaoYangPartsAdaptationDetail>(config
                    .ProductAdaptations.Select(
                        _ =>
                        {
                            BaoYangPartsAdaptationDetail adaptation = new BaoYangPartsAdaptationDetail
                            {
                                PartDisplayName = _.DisplayName
                            };
                            List<BaoYangPartsOeCodeAdaptationDetail> oeDetail =
                                new List<BaoYangPartsOeCodeAdaptationDetail>();
                            List<string> partNames = _.PartNames
                                .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            List<string> brands = _.Brands
                                .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList();
                            var singlePart = partsAdaptation.Where(t =>
                                partNames.Contains(t.PartName) &&
                                (brands.Contains(t.Brand) || string.IsNullOrEmpty(t.Brand))).ToList();
                            if (singlePart.Any())
                            {
                                var groupPart = singlePart.GroupBy(o => o.OePartCode)
                                    .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                                foreach (var itemPart in groupPart)
                                {
                                    var itemPartCode = itemPart.Value;
                                    BaoYangPartsOeCodeAdaptationDetail oeItem = new BaoYangPartsOeCodeAdaptationDetail
                                    {
                                        OePartCode = itemPart.Key,
                                        PartCodeAdaptation = itemPartCode.GroupBy(o => o.PartCode).Select(o =>
                                            new BaoYangPartsPartCodeAdaptationDetail
                                            {
                                                Id = o.FirstOrDefault()?.Id ?? 0,
                                                Brand = o.FirstOrDefault()?.Brand,
                                                PartCode = o.FirstOrDefault()?.PartCode,
                                                IsValidation = true,
                                                PartsProducts = o.Where(d => !string.IsNullOrEmpty(d.Pid))
                                                    .Distinct(x => x.Pid).Select(t =>
                                                        new BaoYangPartsProduct
                                                        {
                                                            ProductPid = t.Pid,
                                                            IsOnSale = t.OnSale,
                                                            IsStockOut = t.StockOut
                                                        }).ToList()
                                            }).OrderBy(t => t.Brand).ToList()
                                    };
                                    oeDetail.Add(oeItem);
                                }
                            }

                            adaptation.OeCodeAdaptationDetails = oeDetail;

                            return adaptation;
                        }).ToList());
                itemPartAdaptation.PartsAdaptation = adaptations;

                List<BaoYangPartsSpecialAdaptation> specialPartsAdaptation = new List<BaoYangPartsSpecialAdaptation>();
                foreach (var adaptation in config.SpecialProductAdaptations)
                {
                    BaoYangPartsSpecialAdaptation itemSpecialPart = new BaoYangPartsSpecialAdaptation()
                    {
                        PartDisplayName = adaptation.DisplayName
                    };
                    List<SpecialAdaptationPart> specialAdaptation = new List<SpecialAdaptationPart>();
                    string partNames = adaptation.PartNames;
                    List<string> partNamesList = partNames.Split(new string[] {", "},
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    var singlePart = partsAdaptation.Where(t => partNamesList.Contains(t.PartName)).ToList();
                    if (singlePart.Any())
                    {
                        var groupPart = singlePart.GroupBy(o => o.PartName)
                            .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                        foreach (var itemPart in groupPart)
                        {
                            var itemPartCode = itemPart.Value;
                            SpecialAdaptationPart oeItem = new SpecialAdaptationPart
                            {
                                PartType = itemPart.Key,
                                PartCodeAdaptation = itemPartCode.GroupBy(o => o.PartCode).Select(o =>
                                    new BaoYangPartsPartCodeAdaptationDetail
                                    {
                                        Id = o.FirstOrDefault()?.Id ?? 0,
                                        Brand = o.FirstOrDefault()?.Brand,
                                        PartCode = o.FirstOrDefault()?.PartCode,
                                        IsValidation = true,
                                        PartsProducts = o.Where(d => !string.IsNullOrEmpty(d.Pid)).Distinct(x => x.Pid)
                                            .Select(t =>
                                                new BaoYangPartsProduct
                                                {
                                                    ProductPid = t.Pid,
                                                    IsOnSale = t.OnSale,
                                                    IsStockOut = t.StockOut
                                                }).ToList()
                                    }).OrderBy(t => t.Brand).ToList()
                            };
                            specialAdaptation.Add(oeItem);
                        }
                    }

                    itemSpecialPart.SpecialAdaptationParts = specialAdaptation;
                    specialPartsAdaptation.Add(itemSpecialPart);
                }

                itemPartAdaptation.PartsSpecialAdaptations = specialPartsAdaptation;

                result.Add(itemPartAdaptation);
            }

            return result;
        }

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPartCodeAsync(AddPartCodeRequest request)
        {
            bool result = false;
            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            string partNames = config.GetPartNames(request.PartName);
            var partName = partNames.Contains(",")
                ? partNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)[0]
                : partNames;
            if (!string.IsNullOrEmpty(partName))
            {
                List<Task<int>> tasks = new List<Task<int>>();
                foreach (var tid in request.TidList)
                {
                    foreach (var partCode in request.PartCodes)
                    {
                        var task = _baoYangConfigRepository.InsertBaoYangPartCodeAsync(new BaoYangPartsDO
                        {
                            Tid = tid,
                            PartName = partName,
                            PartCode = partCode.PartCode,
                            OePartCode = request.OePartCode,
                            Source = "RG-BOSS",
                            Brand = partCode.Brand,
                            CreateBy = request.SubmitBy
                        });
                        tasks.Add(task);
                    }
                }

                await Task.WhenAll(tasks.ToArray());
                result = tasks.Find(_ => _.Result > 0) != null;
            }

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(request.TidList);

            return result;
        }

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRequest request)
        {
            bool result = false;
            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            var partNames = config.GetSpecialPartNames(request.PartName);
            if (!string.IsNullOrEmpty(partNames))
            {
                List<Task<int>> tasks = new List<Task<int>>();
                foreach (var tid in request.TidList)
                {
                    foreach (var partCode in request.PartCodes)
                    {
                        var task = _baoYangConfigRepository.InsertBaoYangPartCodeAsync(new BaoYangPartsDO
                        {
                            Tid = tid,
                            PartName = partCode.PartType,
                            PartCode = partCode.PartCode,
                            Source = "RG-BOSS",
                            Brand = partCode.Brand,
                            CreateBy = request.SubmitBy
                        });
                        tasks.Add(task);
                    }
                }

                await Task.WhenAll(tasks.ToArray());
                result = tasks.Find(_ => _.Result > 0) != null;
            }

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(request.TidList);

            return result;
        }

        /// <summary>
        /// 批量添加配件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAdaptationAsync(BatchAddAdaptationRequest request)
        {
            bool result = false;
            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            List<Task<int>> tasks = new List<Task<int>>();
            foreach (var tid in request.TidList)
            {
                if (request.NormalPart != null && request.NormalPart.Any())
                {
                    foreach (var normalItem in request.NormalPart)
                    {
                        string partNames = config.GetPartNames(normalItem.PartName);
                        var partName = partNames.Contains(",")
                            ? partNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)[0]
                            : partNames;
                        if (!string.IsNullOrEmpty(partName))
                        {
                            foreach (var partCode in normalItem.PartCodes)
                            {
                                var task = _baoYangConfigRepository.InsertBaoYangPartCodeAsync(new BaoYangPartsDO
                                {
                                    Tid = tid,
                                    PartName = partName,
                                    PartCode = partCode.PartCode,
                                    OePartCode = normalItem.OePartCode,
                                    Source = "RG-BOSS",
                                    Brand = partCode.Brand,
                                    CreateBy = request.SubmitBy
                                });
                                tasks.Add(task);
                            }
                        }
                    }
                }

                if (request.SpecialPart != null && request.SpecialPart.Any())
                {
                    foreach (var specialItem in request.SpecialPart)
                    {
                        var partNames = config.GetSpecialPartNames(specialItem.PartName);
                        if (!string.IsNullOrEmpty(partNames))
                        {
                            foreach (var partCode in specialItem.PartCodes)
                            {
                                var task = _baoYangConfigRepository.InsertBaoYangPartCodeAsync(new BaoYangPartsDO
                                {
                                    Tid = tid,
                                    PartName = partCode.PartType,
                                    PartCode = partCode.PartCode,
                                    Source = "RG-BOSS",
                                    Brand = partCode.Brand,
                                    CreateBy = request.SubmitBy
                                });
                                tasks.Add(task);
                            }
                        }
                    }
                }
            }

            await Task.WhenAll(tasks.ToArray());
            result = tasks.Find(_ => _.Result > 0) != null;
            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(request.TidList);
            return result;
        }

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeAsync(DeletePartCodeRequest request)
        {
            if (request.Id > 0)
            {
                return await _baoYangConfigRepository.DeletePartCodeByIdAsync(request.Tid, request.Id,
                    request.SubmitBy);
            }

            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            string displayName = request.PartName;

            if (!string.IsNullOrEmpty(request.PartType))
            {
                var partNames = config.GetSpecialPartNames(displayName);
                if (string.IsNullOrEmpty(partNames))
                {
                    throw new CustomException("配件类型有误");
                }

                return await _baoYangConfigRepository.DeletePartCodeByPartNamesAsync(request.Tid,
                    new List<string> {request.PartType}, request.SubmitBy);
            }

            if (request.OePartCode != null)
            {
                string partNames = config.GetPartNames(displayName);
                var partName = partNames.Contains(",")
                    ? partNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)[0]
                    : partNames;
                if (string.IsNullOrEmpty(partName))
                {
                    throw new CustomException("配件类型有误");
                }

                return await _baoYangConfigRepository.DeletePartCodeByOeCodeAsync(request.Tid, partName,
                    request.OePartCode, request.SubmitBy);
            }

            string partNameN = config.GetPartNames(displayName);
            if (string.IsNullOrEmpty(partNameN))
            {
                partNameN = config.GetSpecialPartNames(displayName);
            }

            if (!string.IsNullOrEmpty(partNameN))
            {
                var partName = partNameN.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList();
                return await _baoYangConfigRepository.DeletePartCodeByPartNamesAsync(request.Tid, partName,
                    request.SubmitBy);
            }

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(new List<string> {request.Tid});

            return false;
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOePartCodeAsync(UpdateOePartCodeRequest request)
        {
            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            string displayName = request.PartName;
            string partNames = config.GetPartNames(displayName);
            var partName = partNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (partName.Any())
            {
                return await _baoYangConfigRepository.UpdateOePartCodeAsync(request.Tid, partName,
                    request.OriginalOePartCode, request.OePartCode, request.SubmitBy);
            }

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(new List<string> {request.Tid});

            return false;
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePartCodeAsync(UpdatePartCodeRequest request)
        {
            var result = await _baoYangConfigRepository.UpdatePartCodeAsync(request.Tid, request.Id, request.PartCode,
                request.Brand, request.SubmitBy);

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(new List<string> {request.Tid});

            return result;
        }

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PartCodeVo>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeRequest request)
        {
            List<PartCodeVo> result = new List<PartCodeVo>();
            BaoYangAdaptationConfig config = await GetBaoYangAdaptationConfigAsync();
            var defaultPart = config[request.PartName];
            string partNames = defaultPart?.PartNames ?? "";
            string brands = defaultPart?.Brands ?? "";
            if (!string.IsNullOrEmpty(partNames) && !string.IsNullOrEmpty(brands))
            {
                List<string> brand = brands.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> partName = partNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToList();
                var oeMap = await _baoYangOeCodeMapRepository.GetPartCodeByOe(request.OeCode);
                if (oeMap != null && oeMap.Any())
                {
                    result = oeMap.Where(x => partName.Contains(x.PartName) && brand.Contains(x.Brand)).Select(x =>
                        new PartCodeVo
                        {
                            Brand = x.Brand,
                            PartCode = x.PartCode
                        }).Distinct(x => x.Brand + x.PartCode).ToList();
                }
                else
                {
                    var partsData =
                        await _baoYangPartsRepository.GetBaoYangPartsByOeCode(request.OeCode, partName, brand);
                    if (partsData != null && partsData.Any())
                    {
                        result = partsData.Select(x => new PartCodeVo
                        {
                            PartCode = x.PartCode,
                            Brand = x.Brand
                        }).Distinct(x => x.Brand + x.PartCode).ToList();
                    }
                }
            }

            return result;
        }

        #endregion

        #region 辅料

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        public async Task<BaoYangAccessoryConfig> GetPartAccessoryConfig()
        {
            const string configName = "BaoYangPartAccessoryConfig";
            BaoYangAccessoryConfig result = null;
            try
            {
                var configXml = await _baoYangConfigRepository.FetchBaoYangConfigByKeyAsync(configName);
                result = XmlHelper.Deserialize<BaoYangAccessoryConfig>(configXml);
            }
            catch (Exception ex)
            {
                //todo Error
            }

            return result;
        }

        /// <summary>
        /// 查询辅料配置数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPartAccessory>> GetBaoYangPartAccessory(BaoYangPartAccessoryRequest request)
        {
            List<BaoYangPartAccessory> result = new List<BaoYangPartAccessory>();
            var partAccessory = (await _baoYangPartAccessoryRepository.GetPartAccessoryByTidListAsync(request.TidList))
                ?.ToList() ?? new List<BaoYangPartAccessoryDO>();
            foreach (var tid in request.TidList)
            {
                BaoYangPartAccessory accessoryItem = new BaoYangPartAccessory()
                {
                    Tid = tid
                };

                List<PartAccessory> detail = partAccessory.Where(_ => _.Tid == tid).Select(_ => new PartAccessory
                {
                    AccessoryName = _.AccessoryName,
                    Volume = _.Volume,
                    Level = _.Level,
                    Quantity = _.Quantity,
                    Interface = _.Interface,
                    Description = _.Description,
                    Size = _.Size,
                    Viscosity = _.Viscosity,
                    Grade = _.Grade
                }).ToList();
                accessoryItem.Details = detail;
                result.Add(accessoryItem);
            }

            return result;
        }

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAccessory(UpdateAccessoryRequest request)
        {
            var result = await UpdateAccessoryConfig(request.Tid, request.AccessoryName, new List<PartAttribute>()
            {
                new PartAttribute()
                {
                    AttributeName = request.AttributeName,
                    AttributeValue = request.AttributeValue
                }
            }, request.SubmitBy);

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(new List<string> {request.Tid});

            return result;
        }

        private async Task<bool> UpdateAccessoryConfig(string tid, string accessoryName, List<PartAttribute> attribute,
            string submitBy)
        {
            ;
            BaoYangPartAccessoryDO createAccessory = new BaoYangPartAccessoryDO()
            {
                Tid = tid,
                AccessoryName = accessoryName
            };
            foreach (var itemAttribute in attribute)
            {
                HandleAccessoryConfig(createAccessory, itemAttribute);
            }

            var defaultAccessory =
                await _baoYangPartAccessoryRepository.GetPartAccessoryByTidAndTypeAsync(tid, accessoryName, false);

            int result = 0;
            if (defaultAccessory == null)
            {
                createAccessory.CreateBy = submitBy;
                createAccessory.CreateTime = DateTime.Now;

                result = await _baoYangPartAccessoryRepository.InsertAsync(createAccessory);
            }
            else
            {
                createAccessory.Id = defaultAccessory.Id;
                createAccessory.UpdateBy = submitBy;
                createAccessory.UpdateTime = DateTime.Now;
                List<string> updateFields = new List<string>();
                updateFields.AddRange(attribute.Select(_ => _.AttributeName));
                updateFields.Add("UpdateBy");
                updateFields.Add("UpdateTime");
                result = await _baoYangPartAccessoryRepository.UpdateAsync(createAccessory, updateFields);
            }

            return result > 0;
        }

        private void HandleAccessoryConfig(BaoYangPartAccessoryDO partAccessory, PartAttribute attribute)
        {
            var attributeName = attribute.AttributeName;
            var attributeValue = attribute.AttributeValue;

            switch (attributeName)
            {
                case "Volume":
                    partAccessory.Volume = attributeValue;
                    break;
                case "Level":
                    partAccessory.Level = attributeValue;
                    break;
                case "Quantity":
                    partAccessory.Quantity = Convert.ToInt32(attributeValue);
                    break;
                case "Size":
                    partAccessory.Size = attributeValue;
                    break;
                case "Interface":
                    partAccessory.Interface = attributeValue;
                    break;
                case "Description":
                    partAccessory.Description = attributeValue;
                    break;
                case "Viscosity":
                    partAccessory.Viscosity = attributeValue;
                    break;
                case "Grade":
                    partAccessory.Grade = attributeValue;
                    break;
                default:
                    throw new CustomException("属性名有误");
            }
        }

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchEditAccessory(BatchEditAccessoryRequest request)
        {
            await Task.WhenAll(request.TidList.Select(_ =>
                UpdateAccessoryConfig(_, request.AccessoryName, request.Attribute, request.SubmitBy)));

            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(request.TidList);

            return true;
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccessory(DeleteAccessoryRequest request)
        {
            var result =
                await _baoYangPartAccessoryRepository.DeleteAccessory(request.Tid, request.AccessoryName,
                    request.SubmitBy);
            var refreshConfigTask = _baoYangService.RefreshAdaptationConfigRedis(new List<string> {request.Tid});
            return result;
        }

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> CheckPartCode(CheckPartCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.PartCode))
            {
                return new List<string>();
            }

            var result =
                (await _baoYangPartsRepository.GetBaoYangPartsByParCode(new List<string>() {request.PartCode}))
                ?.ToList() ?? new List<BaoYangPartsDO>();
            return result.Select(_ => _.PartName).Distinct().ToList();
        }

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckProductResponse> CheckProduct(CheckProductRequest request)
        {
            if (string.IsNullOrEmpty(request.Pid))
            {
                return null;
            }

            var product = (await _productClient.GetProductsByProductCodes(new List<string>() {request.Pid}))
                ?.FirstOrDefault();
            if (product != null)
            {
                return new CheckProductResponse()
                {
                    Category = product.Product.ChildCategoryName,
                    Name = product.Product.Name
                };
            }

            return null;
        }

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckPartCodeResultResponse> CheckPartCodeAndProduct(CheckPartCodeAndProductRequest request)
        {
            bool succ = false;
            var partCodeTask = _baoYangPartsRepository.GetBaoYangPartsByParCode(new List<string>() {request.PartCode});
            var productTask = _productClient.GetProductsByProductCodes(new List<string>() {request.Pid});
            var baoYangAdaptationTask = GetBaoYangAdaptationConfigAsync();
            await Task.WhenAll(partCodeTask, productTask, baoYangAdaptationTask);
            var partCode = partCodeTask.Result?.Select(_ => _.PartName).Distinct().ToList() ?? new List<string>();
            var product = productTask.Result?.FirstOrDefault();
            var config = baoYangAdaptationTask.Result;
            if (!partCode.Any() || product == null)
            {
                return new CheckPartCodeResultResponse
                {
                    IsSuccess = false,
                    Message = "配件号或商品不存在"
                };
            }

            foreach (var item in config.ProductAdaptations)
            {
                if (item.CategoryName == product.Product.ChildCategoryId.ToString() &&
                    partCode.Contains(item.DisplayName))
                {
                    succ = true;
                    break;
                }
            }

            if (!succ)
            {
                foreach (var item in config.SpecialProductAdaptations)
                {
                    if (item.CategoryName == product.Product.ChildCategoryId.ToString() &&
                        partCode.Contains(item.DisplayName))
                    {
                        succ = true;
                        break;
                    }
                }
            }

            if (!succ)
            {
                return new CheckPartCodeResultResponse
                {
                    IsSuccess = false,
                    Message = "配件分类与产品分类不一致"
                };
            }

            return new CheckPartCodeResultResponse
            {
                IsSuccess = true,
                Message = "验证成功"
            };
        }

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InsertPartsAssociation(InsertPartsAssociationRequest request)
        {
            List<string> partCode = request.PartProductRef.Select(_ => _.PartCode).Distinct().ToList();
            var baoYangAdaptation = await GetBaoYangAdaptationConfigAsync();
            var parts = (await _baoYangPartsRepository.GetBaoYangPartsByParCode(partCode))?.ToList() ??
                        new List<BaoYangPartsDO>();
            await Task.WhenAll(request.PartProductRef.Select(_ => InsertBaoYangProductRef(new BaoYangProductRefDO()
                {
                    Brand = parts.FirstOrDefault(t => t.PartCode == _.PartCode)?.Brand,
                    PartCode = _.PartCode,
                    Pid = _.Pid,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                }, baoYangAdaptation,
                parts.Where(t => t.PartCode == _.PartCode).Select(t => t.PartName).Distinct().ToList())));

            return true;
        }

        private async Task<bool> InsertBaoYangProductRef(BaoYangProductRefDO baoYangProductRefDo,
            BaoYangAdaptationConfig config, List<string> partNames)
        {
            List<string> byType = new List<string>();
            foreach (var item in config.ProductAdaptations)
            {
                foreach (var parts in item.PartNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (partNames.Contains(parts))
                    {
                        byType.Add(item.DisplayName);
                    }
                }
            }

            foreach (var item in config.SpecialProductAdaptations)
            {
                foreach (var parts in item.PartNames.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (partNames.Contains(parts))
                    {
                        byType.Add(item.DisplayName);
                    }
                }
            }

            var part = await _baoYangProductRefRepository.GetBaoYangProductRef(baoYangProductRefDo.PartCode,
                baoYangProductRefDo.Pid, false);
            if (part == null || !part.Any())
            {
                baoYangProductRefDo.PartType = string.Join(",", byType.Distinct());
                return await _baoYangProductRefRepository.InsertAsync(baoYangProductRefDo) > 0;
            }

            return true;
        }

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationRequest request)
        {
            var product = (await _productClient.GetProductsByProductCodes(new List<string>()
            {
                request.Pid
            }, false))?.FirstOrDefault();
            if (product != null && product.Product.ClassType == 2 && !string.IsNullOrEmpty(product.Product.PartCode))
            {
                int categoryId = product.Product.ChildCategoryId;
                var pid = product.Product.ProductCode;
                var partCode = product.Product.PartCode;
                var brand = product.Product.Brand;
                var part = await _baoYangProductRefRepository.GetBaoYangProductRef(partCode, pid, false);
                if (part == null || !part.Any())
                {
                    var baoYangTypesTask = GetBaoYangTypeConfigCache();
                    var partsMapTask = GetBaoYangPartsMapConfigCache();
                    var baoYangAdaptationTask = GetBaoYangAdaptationConfigAsync();
                    await Task.WhenAll(baoYangTypesTask, partsMapTask, baoYangAdaptationTask);
                    var baoYangTypes = baoYangTypesTask.Result?.ToList() ?? new List<BaoYangTypeConfigDO>();
                    var partsMap = partsMapTask.Result?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
                    var config = baoYangAdaptationTask.Result;
                    var defaultConfig = baoYangTypes.Where(_ =>
                        _.TypeGroup == "Part" && _.ChildCategories
                            .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                            .Contains(categoryId.ToString())).Select(_ => _.BaoYangType).ToList();
                    if (defaultConfig.Any())
                    {
                        var defaultMap = partsMap.Where(_ =>
                            defaultConfig.Contains(_.BaoYangType) && _.RefType == "PartCode").ToList();
                        if (defaultMap.Any())
                        {
                            List<string> partNames = new List<string>();
                            defaultMap.ForEach(_ =>
                            {
                                partNames.AddRange(_.PartNames.Split(new[] {','},
                                    StringSplitOptions.RemoveEmptyEntries));
                            });
                            List<string> byType = new List<string>();
                            foreach (var item in config.ProductAdaptations)
                            {
                                foreach (var parts in item.PartNames.Split(new string[] {", "},
                                    StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (partNames.Contains(parts))
                                    {
                                        byType.Add(item.DisplayName);
                                    }
                                }
                            }

                            foreach (var item in config.SpecialProductAdaptations)
                            {
                                foreach (var parts in item.PartNames.Split(new string[] {", "},
                                    StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (partNames.Contains(parts))
                                    {
                                        byType.Add(item.DisplayName);
                                    }
                                }
                            }

                            BaoYangProductRefDO baoYangProductRefDo = new BaoYangProductRefDO()
                            {
                                Brand = brand,
                                PartCode = partCode,
                                Pid = pid,
                                PartType = string.Join(",", byType.Distinct()),
                                CreateBy = request.SubmitBy,
                                CreateTime = DateTime.Now
                            };
                            await _baoYangProductRefRepository.InsertAsync(baoYangProductRefDo);
                        }
                    }

                }
            }

            return true;
        }

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<BaoYangPartProductVo>, int>> GetBaoYangProductRef(BaoYangProductRefRequest request)
        {
            int totalCount = 0;
            List<BaoYangPartProductVo> parts = new List<BaoYangPartProductVo>();
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                var temp1 = Convert.ToDateTime(request.StartDate);
                startTime = new DateTime(temp1.Year, temp1.Month, temp1.Day);
            }

            if (!string.IsNullOrEmpty(request.EndDate))
            {
                var temp2 = Convert.ToDateTime(request.EndDate).AddDays(1);
                endTime = new DateTime(temp2.Year, temp2.Month, temp2.Day);
            }

            var result = await _baoYangProductRefRepository.GetPageBaoYangProductRef(request.PartCode, request.Pid,
                startTime, endTime, request.PageIndex, request.PageSize);
            var data = result.Items.ToList();
            totalCount = result.TotalItems;
            if (data.Any())
            {
                var products = await _productClient.GetProductsByProductCodes(data.Select(_ => _.Pid).ToList(), false);
                parts = data.Select(_ => new BaoYangPartProductVo
                {
                    PartCode = _.PartCode,
                    PartType = _.PartType,
                    Pid = _.Pid,
                    ProductName = products.FirstOrDefault(f => f.Product.ProductCode == _.Pid)?.Product.Name,
                    RelateTime = _.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateBy = _.CreateBy,
                    Id = _.Id
                }).ToList();
            }

            return new Tuple<List<BaoYangPartProductVo>, int>(parts, totalCount);
        }

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefRequest request)
        {
            var result = await _baoYangProductRefRepository.UpdateAsync(new BaoYangProductRefDO()
            {
                Id = request.Id,
                IsDeleted = true,
                UpdateBy = request.SubmitBy ?? string.Empty,
                UpdateTime = DateTime.Now
            }, new List<string>()
            {
                "IsDeleted", "UpdateBy", "UpdateTime"
            });

            return result > 0;
        }

        #endregion

        #region 套餐

        /// <summary>
        /// 套餐适配默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AdaptiveDefaultCarsByPackageId(AdaptiveDefaultCarsByPackageIdRequest request)
        {
            if (request.BaoYangPackage != null && request.BaoYangPackage.Any())
            {
                await Task.WhenAll(request.BaoYangPackage.Select(_ => AdaptiveDefaultCarsByPackageId(_)));

                return true;
            }

            return false;
        }

        /// <summary>
        /// 单个套餐适配默认车型
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private async Task<bool> AdaptiveDefaultCarsByPackageId(BaoYangPackageRequest package)
        {
            string byType = package.BaoYangType;
            var packageDetail =
                (await _productClient.GetPackageProductsByCodesAsync(new List<string>() {package.PackagePid}))
                ?.FirstOrDefault();
            if (packageDetail != null)
            {
                var childProduct = packageDetail.Details
                    .Where(t => (t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")) || t.ProjectType == 2).ToList();
                if (childProduct.Any())
                {
                    var tidList = await AdaptiveTidForDby(childProduct);

                    await AddBaoYangPackageRef(tidList, package.PackagePid, byType, "System");
                }
            }

            return true;
        }

        #region 获取所有车型Tid

        private async Task<List<string>> GetAllTidCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":GetAllTid:{timestamp}";
            var packageConfig = await RedisHelper.GetOrSetAsync(_redisClient, key, () => GetAllTid(),
                new TimeSpan(1, 0, 0, 0));
            return packageConfig;
        }

        private async Task<List<string>> GetAllTid()
        {
            var result = await _vehicleTypeTimingRepository.GetAllTid();

            return result?.Select(_ => _.Tid)?.ToList() ?? new List<string>();
        }

        #endregion


        private async Task<List<string>> AdaptiveTidForDby(List<ProductPackageDetailVo> packageDetail)
        {
            var baoYangConfig = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
            var partMapConfig =
                (await GetBaoYangPartsMapConfigCache())?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
            var accessoryConfig = (await _baoYangPartAccessoryMapRepository.GetParAccessoryMapAsync())?.ToList() ??
                                  new List<BaoYangPartAccessoryMapDO>();
            List<List<string>> tidList = new List<List<string>>();
            var products =
                await _productClient.GetProductsByProductCodes(packageDetail.Where(_ => _.ProjectType == 1)
                    .Select(_ => _.ProjectId).ToList());
            var baoYangPart = packageDetail.Where(_ => _.ProjectType == 2).ToList();
            foreach (var product in products)
            {
                var categoryId = product.Product.ChildCategoryId; //类目
                var itemConfig = baoYangConfig.FirstOrDefault(_ =>
                    _.ChildCategories.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .Contains(categoryId.ToString()));
                if (itemConfig != null)
                {
                    if (itemConfig.TypeGroup == BaoYangTypeGroup.Maintainence.ToString())
                    {
                        tidList.Add(await GetAllTidCache());
                    }
                    else if (itemConfig.TypeGroup == BaoYangTypeGroup.Part.ToString())
                    {
                        var mapConfig = partMapConfig.FirstOrDefault(_ => _.BaoYangType == itemConfig.BaoYangType);
                        if (mapConfig != null)
                        {
                            if (mapConfig.RefType == "PartCode")
                            {
                                var tidData = await _baoYangPartsRepository.GetTidByPid(product.Product.ProductCode);
                                tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
                            }
                            else if (mapConfig.RefType == "PartNo")
                            {
                                var tidData = await _baoYangPartsRepository.GetTidByParCode(product.Product.PartNo);
                                if (categoryId == 45)
                                {
                                    var seriesProduct = products.Where(_ => _.Product.ChildCategoryId == categoryId)
                                        .ToList();
                                    int volume = 0;
                                    seriesProduct.ForEach(_ =>
                                    {
                                        if (string.Equals(_.Product.Unit, "0.946L", StringComparison.OrdinalIgnoreCase))
                                        {
                                            _.Product.Unit = "1L";
                                        }

                                        int itemVolume = Convert.ToInt32(Regex.Replace(_.Product.Unit, "\\D", ""));
                                        int count = packageDetail
                                            .FirstOrDefault(t => t.ProjectId == _.Product.ProductCode)
                                            ?.Quantity ?? 1;
                                        volume = volume + itemVolume * count;
                                    });
                                    var tidData1 = await _baoYangPartAccessoryRepository.GetTidByVolume("变速箱油", volume);
                                    tidData = tidData.Where(_ => tidData1.Contains(_));
                                    tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
                                }
                                else
                                {
                                    tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
                                }
                            }
                            else if (mapConfig.RefType == "ProductId")
                            {
                                var tidData =
                                    await _baoYangPartsRepository.GetTidByParCode(product.Product.ProductCode);
                                tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
                            }
                        }
                    }
                    else if (itemConfig.TypeGroup == BaoYangTypeGroup.Accessory.ToString())
                    {
                        var mapConfig = accessoryConfig.FirstOrDefault(_ => _.BaoYangType == itemConfig.BaoYangType);
                        if (mapConfig != null)
                        {
                            var seriesProduct = products
                                .Where(_ => _.Product.ChildCategoryId == categoryId).ToList();
                            int volume = 0;
                            foreach (var itemProduct in seriesProduct)
                            {
                                int itemVolume = Convert.ToInt32(Regex.Replace(itemProduct.Product.Unit, "\\D", ""));
                                int count = packageDetail
                                    .FirstOrDefault(t => t.ProjectId == itemProduct.Product.ProductCode)
                                    ?.Quantity ?? 1;
                                volume = volume + itemVolume * count;
                            }

                            string accessoryName = mapConfig.AccessoryNames;
                            List<string> viscosity = null;
                            List<string> level = null;
                            List<string> description = null;
                            if (product.Product.SecondCategoryId == 12) //机油
                            {
                                var oilViscosity = product.Attributevalues
                                    .FirstOrDefault(_ => _.AttributeName == "Viscosity")?.AttributeValue;
                                var oilLevel = product.Attributevalues
                                    .FirstOrDefault(_ => _.AttributeName == "OilLevel")?.AttributeValue;
                                var oilDescription = product.Attributevalues
                                    .FirstOrDefault(_ => _.AttributeName == "OilDescription")
                                    ?.AttributeValue;
                                if (string.IsNullOrEmpty(oilViscosity) || string.IsNullOrEmpty(oilLevel) ||
                                    string.IsNullOrEmpty(oilDescription))
                                {
                                    return new List<string>();
                                }

                                viscosity = GetAvailViscosity(oilViscosity);
                                level = GetAvailLevel(oilLevel);
                                description = GetAvailDescription(oilDescription);
                            }
                            else if (categoryId == 50) //刹车油
                            {
                                var scyLevel = product.Attributevalues
                                    .FirstOrDefault(_ => _.AttributeName == "ScyLevel")?.AttributeValue;
                                if (string.IsNullOrEmpty(scyLevel))
                                {
                                    return new List<string>();
                                }

                                int tempLevel;
                                if (Int32.TryParse(scyLevel, out tempLevel))
                                {
                                    level = _scyLevel.Where(_ =>
                                        Convert.ToInt32(_.Split(new[] {'-'})[1]) <= tempLevel).ToList();
                                }
                                else
                                {
                                    return new List<string>();
                                }
                            }

                            var tidData = await _baoYangPartAccessoryRepository.GetTidByVolume(accessoryName, volume,
                                viscosity, level, description);
                            tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
                        }
                    }
                }
            }

            foreach (var itemPart in baoYangPart)
            {
                var mapConfig =
                    partMapConfig.FirstOrDefault(_ => _.BaoYangType == itemPart.ProjectId)?.PartNames
                        ?.Split(new char[] {','})?.ToList() ?? new List<string>();
                if (!mapConfig.Any())
                {
                    return new List<string>();
                }

                var tidData = await _baoYangPartsRepository.GetTidByParCode(itemPart.ProjectBrands, mapConfig);
                tidList.Add(tidData?.Select(_ => _.Tid)?.ToList() ?? new List<string>());
            }

            if (tidList.Any())
            {
                List<string> result = new List<string>();
                for (int i = 0; i < tidList.Count; i++)
                {
                    if (i == 0)
                    {
                        result = tidList[i];
                    }
                    else
                    {
                        result = result.Where(_ => tidList[i].Contains(_)).ToList();
                    }
                }

                return result;
            }

            return new List<string>();
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
                    return viscosityS <= currentviscosityS && viscosityP >= currentviscosityP;
                }
                else
                {
                    return false;
                }
            }).ToList();
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
                    Convert.ToInt32(Regex.Replace(x, "\\D", "")) <=
                    Convert.ToInt32(Regex.Replace(oilLevel, "\\D", ""))).ToList();
            }
            else if (oilLevel == "ULTRA")
            {
                availLevel.AddRange(_allLevel);
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
                List<string> splitDes =
                    oilDescription.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var itemDes in splitDes)
                {
                    if (itemDes.Length > 1)
                    {
                        char seriesChar = itemDes.ToCharArray()[0]; //目前有S，C,A
                        var nextChar = itemDes.ToCharArray()[1];
                        var availItem = _allDescription.Where(x => x.StartsWith(seriesChar))
                            .Where(x => x.Substring(1, 1).ToCharArray()[0] <= nextChar).ToList();
                        availDescription.AddRange(availItem);
                    }
                    else
                    {
                        availDescription.Add(oilDescription);
                    }
                }
            }
            else
            {
                availDescription.Add(oilDescription);
            }

            return availDescription;
        }

        /// <summary>
        /// 车型添加套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> RelateVehicleAndPackage(RelateVehicleAndPackageRequest request)
        {
            await Task.WhenAll(request.PackageId.Select(_ =>
                AddBaoYangPackageRef(request.Tid, _, request.ByType, request.SubmitBy)));

            return true;
        }

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangPackageRef(DeleteBaoYangPackageRefRequest request)
        {
            BaoYangPackageRefDO package = new BaoYangPackageRefDO()
            {
                Id = request.Id,
                IsDeleted = true,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            var result = await _baoYangPackageRefRepository.UpdateAsync(package,
                new List<string>() {"IsDeleted", "UpdateBy", "UpdateTime"});

            return result > 0;
        }

        public async Task<bool> AddBaoYangPackageRef(string tid, string packagePid, string byType, string createBy)
        {
            var productRef =
                await _baoYangPackageRefRepository.GetBaoYangPackageRefAsync(tid, byType, packagePid, false);
            if (productRef == null || !productRef.Any())
            {
                BaoYangPackageRefDO baoYangPackageRefDo = new BaoYangPackageRefDO()
                {
                    Tid = tid,
                    BaoYangType = byType,
                    PackageId = packagePid,
                    CreateBy = createBy,
                    CreateTime = DateTime.Now
                };

                return await _baoYangPackageRefRepository.InsertAsync(baoYangPackageRefDo) > 0;
            }

            return true;
        }

        public async Task<bool> AddBaoYangPackageRef(List<string> tidList, string packagePid, string byType,
            string createBy)
        {
            var productRef =
                await _baoYangPackageRefRepository.GetBaoYangPackageRefAsync(tidList, byType, packagePid, false);

            foreach (var tid in tidList)
            {
                var firstItem = productRef.FirstOrDefault(_ => _.Tid == tid);
                if (firstItem == null)
                {
                    BaoYangPackageRefDO baoYangPackageRefDo = new BaoYangPackageRefDO()
                    {
                        Tid = tid,
                        BaoYangType = byType,
                        PackageId = packagePid,
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    };

                    await _baoYangPackageRefRepository.InsertAsync(baoYangPackageRefDo);
                }
            }

            return true;
        }

        /// <summary>
        /// 车型关联套餐列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageRefVo>> GetBaoYangPackageRef(BaoYangPackageRefRequest request)
        {
            List<BaoYangPackageRefVo> data = new List<BaoYangPackageRefVo>();
            var result =
                (await _baoYangPackageRefRepository.GetBaoYangPackageRefByTidsAsync(request.TidList, request.PackageId))
                ?.ToList() ?? new List<BaoYangPackageRefDO>();
            var packagePid = result.Select(_ => _.PackageId).ToList();
            var packageDetail = await _productClient.GetPackageProductsByCodesAsync(packagePid);
            var baoYangConfig = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
            result.ForEach(_ =>
            {
                var itemTemp = packageDetail.FirstOrDefault(t => t.PackageInfo.ProductCode == _.PackageId);
                BaoYangPackageRefVo itemPackage = new BaoYangPackageRefVo()
                {
                    Id = _.Id,
                    Tid = _.Tid,
                    BaoYangType = _.BaoYangType,
                    ByTypeName = baoYangConfig.FirstOrDefault(t => t.BaoYangType == _.BaoYangType)?.DisplayName,
                    PackagePid = _.PackageId,
                    PackageName = itemTemp?.PackageInfo?.Name,
                    PackagePrice = itemTemp?.PackageInfo?.SalesPrice ?? 0
                };
                data.Add(itemPackage);
            });
            return data;
        }

        /// <summary>
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigVo>> GetPackageByType()
        {
            var result = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
            result = result.Where(_ => _.TypeGroup == "Package").ToList();
            return result.Select(_ => new BaoYangTypeConfigVo
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName
            }).ToList();
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

        #region 五级属性适配

        /// <summary>
        /// 五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyAdaptationVo>> GetBaoYangPropertyAdaptation(
            BaoYangPropertyAdaptationRequest request)
        {
            List<BaoYangPropertyAdaptationVo> result = new List<BaoYangPropertyAdaptationVo>();
            var partsMapConfig =
                (await GetBaoYangPartsMapConfigCache())?.ToList() ?? new List<BaoYangPartsMapConfigDO>();
            var defaultPartMapConfig = partsMapConfig.FirstOrDefault(_ => _.BaoYangType == request.BaoYangType);
            if (defaultPartMapConfig != null)
            {
                var partNames =
                    defaultPartMapConfig.PartNames?.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        ?.ToList() ?? new List<string>();
                var refType = defaultPartMapConfig.RefType;
                var propertyTask =
                    _baoYangPropertyAdaptationRepository.GetPropertyAdaptation(request.TidList, request.BaoYangType);
                var baoYangPartTask = _baoYangPartsRepository.GetBaoYangParts(request.TidList, partNames);
                await Task.WhenAll(propertyTask, baoYangPartTask);
                var property = propertyTask.Result?.ToList() ?? new List<BaoYangPropertyAdaptationDO>();
                var baoYangPart = baoYangPartTask.Result?.ToList() ?? new List<BaoYangPartsDO>();
                var tidList = baoYangPart.Select(_ => _.Tid).Distinct().ToList();
                foreach (var tid in tidList)
                {
                    var itemParts = baoYangPart.Where(_ => _.Tid == tid).ToList();
                    var itemProperty = property.Where(_ => _.Tid == tid).ToList();
                    if (refType == "PartCode")
                    {
                        var subPart = itemParts.GroupBy(_ => _.OePartCode)
                            .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                        foreach (var subItem in subPart)
                        {
                            var subProperty = itemProperty.FirstOrDefault(x => x.OePartCode == subItem.Key);
                            BaoYangPropertyAdaptationVo itemData = new BaoYangPropertyAdaptationVo()
                            {
                                Tid = tid,
                                OeCode = subItem.Key,
                                PartCode = string.Join(",", subItem.Value.Select(x => x.PartCode).Distinct()),
                                Property = subProperty?.Property ?? string.Empty,
                                PropertyValue = subProperty?.PropertyValue ?? string.Empty,
                                ImageUrl = subProperty?.ImageUrl ?? string.Empty,
                                Id = subProperty?.Id ?? 0
                            };
                            result.Add(itemData);
                        }
                    }
                    else
                    {
                        var subPart = itemParts.Select(_ => _.PartCode).Distinct().ToList();
                        foreach (var subItem in subPart)
                        {
                            var subProperty = itemProperty.FirstOrDefault(x => x.OePartCode == subItem);
                            BaoYangPropertyAdaptationVo itemData = new BaoYangPropertyAdaptationVo()
                            {
                                Tid = tid,
                                OeCode = "",
                                PartCode = subItem,
                                Property = subProperty?.Property ?? string.Empty,
                                PropertyValue = subProperty?.PropertyValue ?? string.Empty,
                                ImageUrl = subProperty?.ImageUrl ?? string.Empty,
                                Id = subProperty?.Id ?? 0
                            };
                            result.Add(itemData);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyDescriptionVo>> GetBaoYangPropertyDescription()
        {
            var result = await _baoYangPropertyDescriptionRepository.GetBaoYangPropertyDescription();

            return result?.Select(_ => new BaoYangPropertyDescriptionVo
            {
                DisplayName = _.Name
            })?.ToList() ?? new List<BaoYangPropertyDescriptionVo>();
        }

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePropertyAdaptation(DeletePropertyAdaptationRequest request)
        {
            var result = await _baoYangPropertyAdaptationRepository.UpdateAsync(new BaoYangPropertyAdaptationDO()
            {
                Id = request.Id,
                IsDeleted = true,
                UpdateBy = request.SubmitBy ?? string.Empty,
                UpdateTime = DateTime.Now
            }, new List<string>() {"IsDeleted", "UpdateBy", "UpdateTime"});

            return true;
        }

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SavePropertyAdaptation(SavePropertyAdaptationRequest request)
        {
            if (request.Id > 0)
            {
                var result = await _baoYangPropertyAdaptationRepository.UpdateAsync(new BaoYangPropertyAdaptationDO()
                {
                    Id = request.Id,
                    Property = request.Property,
                    PropertyValue = request.PropertyValue,
                    ImageUrl = request.ImageUrl,
                    UpdateBy = request.SubmitBy ?? string.Empty,
                    UpdateTime = DateTime.Now
                }, new List<string>() {"Property", "PropertyValue", "ImageUrl", "UpdateBy", "UpdateTime"});
            }
            else
            {
                var existDataTask = _baoYangPropertyAdaptationRepository.GetPropertyAdaptationByTid(request.Tid);
                var baoYangTypeMapTask = _baoYangService.GetBaoYangParts();
                await Task.WhenAll(existDataTask, baoYangTypeMapTask);
                var existData = existDataTask.Result?.ToList() ?? new List<BaoYangPropertyAdaptationDO>();
                var baoYangTypeMap = baoYangTypeMapTask.Result ?? new List<BaoYangPartsResponse>();
                var defaultPartType = baoYangTypeMap.First(_ => _.BaoYangType == request.BaoYangType);
                if (defaultPartType != null)
                {
                    var refType = defaultPartType.RefType;
                    if (refType == "PartCode")
                    {
                        var defaultProperty = existData.FirstOrDefault(_ =>
                            _.PartNameAbbr == request.BaoYangType && _.OePartCode == request.OePartCode);
                        if (defaultProperty != null)
                        {
                            var result = await _baoYangPropertyAdaptationRepository.UpdateAsync(
                                new BaoYangPropertyAdaptationDO()
                                {
                                    Id = defaultProperty.Id,
                                    Property = request.Property,
                                    PropertyValue = request.PropertyValue,
                                    ImageUrl = request.ImageUrl,
                                    UpdateBy = request.SubmitBy ?? string.Empty,
                                    UpdateTime = DateTime.Now
                                },
                                new List<string>() {"Property", "PropertyValue", "ImageUrl", "UpdateBy", "UpdateTime"});
                        }
                        else
                        {
                            var result = await _baoYangPropertyAdaptationRepository.InsertAsync(
                                new BaoYangPropertyAdaptationDO()
                                {
                                    Tid = request.Tid,
                                    PartName = defaultPartType.DisplayName,
                                    PartNameAbbr = defaultPartType.BaoYangType,
                                    Property = request.Property,
                                    PropertyValue = request.PropertyValue,
                                    ImageUrl = request.ImageUrl,
                                    OePartCode = request.OePartCode,
                                    CreateBy = request.SubmitBy,
                                    CreateTime = DateTime.Now
                                });
                        }
                    }
                    else
                    {
                        var defaultProperty = existData.FirstOrDefault(_ =>
                            _.PartNameAbbr == request.BaoYangType && _.PartCode == request.PartCode);
                        if (defaultProperty != null)
                        {
                            var result = await _baoYangPropertyAdaptationRepository.UpdateAsync(
                                new BaoYangPropertyAdaptationDO()
                                {
                                    Id = defaultProperty.Id,
                                    Property = request.Property,
                                    PropertyValue = request.PropertyValue,
                                    ImageUrl = request.ImageUrl,
                                    UpdateBy = request.SubmitBy ?? string.Empty,
                                    UpdateTime = DateTime.Now
                                },
                                new List<string>() {"Property", "PropertyValue", "ImageUrl", "UpdateBy", "UpdateTime"});
                        }
                        else
                        {
                            var result = await _baoYangPropertyAdaptationRepository.InsertAsync(
                                new BaoYangPropertyAdaptationDO()
                                {

                                    Tid = request.Tid,
                                    PartName = defaultPartType.DisplayName,
                                    PartNameAbbr = defaultPartType.BaoYangType,
                                    Property = request.Property,
                                    PropertyValue = request.PropertyValue,
                                    ImageUrl = request.ImageUrl,
                                    PartCode = request.PartCode,
                                    CreateBy = request.SubmitBy,
                                    CreateTime = DateTime.Now
                                });
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageVo>> GetBaoYangPackageByTid(BaoYangPackageByTidRequest request)
        {
            List<BaoYangPackageVo> result = new List<BaoYangPackageVo>();
            var baoYangProductRef =
                (await _baoYangPackageRefRepository.GetBaoYangPackageRefByParaAsync(request.Tid,
                    new List<string>() {request.BaoYangType}))?.ToList() ?? new List<BaoYangPackageRefDO>(); //已绑定的套餐
            var relatedPid = baoYangProductRef.Select(_ => _.PackageId).ToList();
            var baoYangConfig = (await GetBaoYangTypeConfigCache())?.ToList() ?? new List<BaoYangTypeConfigDO>();
            var itemConfig = baoYangConfig.FirstOrDefault(_ => _.BaoYangType == request.BaoYangType);
            if (itemConfig != null)
            {
                int categoryId = 0;
                if (Int32.TryParse(itemConfig.CategoryName, out categoryId))
                {
                    var product = await _productClient.SelectProductsByCategoryCache(categoryId);
                    if (product != null && product.Any())
                    {
                        product = product.Where(_ => _.ProductAttribute == 1).ToList();
                        if (product.Any())
                        {
                            List<ProductDetailDTO> childProductDetail = new List<ProductDetailDTO>(); //子产品 详情Model
                            var packageList =
                                await _productClient.GetPackageProductsByCodesAsync(product.Select(_ => _.ProductCode)
                                    .ToList());
                            var childProduct = packageList.SelectMany(_ => _.Details)
                                .Where(t => t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")).ToList();
                            var partProduct = packageList.SelectMany(_ => _.Details).Where(t => t.ProjectType == 2)
                                .ToList();
                            List<string> adaptivePid = new List<string>();
                            if (childProduct.Any())
                            {
                                var childPid = childProduct.Select(_ => _.ProjectId).Distinct().ToList();
                                childProductDetail = await _productClient.GetProductsByProductCodes(childPid);
                                adaptivePid = await _baoYangService.GetAdaptiveProducts(request.Tid,
                                    childProductDetail.Select(_ => new ProductRequest
                                        {
                                            Pid = _.Product.ProductCode,
                                            CategoryId = _.Product.ChildCategoryId.ToString()
                                        })
                                        .ToList(), 0);

                            }

                            List<PartProductRefVo> baoYangPart = new List<PartProductRefVo>();
                            if (partProduct.Any())
                            {
                                baoYangPart =
                                    await _baoYangService.GetAdaptiveProductByPartType(
                                        partProduct.Select(t => new BaoYangPartRequest
                                            {PartType = t.ProjectId, BrandList = t.ProjectBrands}).ToList(),
                                        request.Tid, "", "") ?? new List<PartProductRefVo>();
                            }

                            foreach (var _ in product)
                            {
                                var itemChild = packageList
                                    .FirstOrDefault(t => t.PackageInfo.ProductCode == _.ProductCode)?.Details;
                                if (itemChild != null && itemChild.Any())
                                {
                                    bool adaptive = true;
                                    var realProduct = itemChild
                                        .Where(t => t.ProjectType == 1 && !t.ProjectId.EndsWith("-FU")).ToList();
                                    var realPid = realProduct.Select(t => t.ProjectId).ToList();
                                    if (realPid.Any())
                                    {
                                        if (realPid.Any(t => !adaptivePid.Contains(t)))
                                        {
                                            adaptive = false;
                                        }

                                        #region 升数校验

                                        var jiYouProduct = childProductDetail.Where(t =>
                                                realPid.Contains(t.Product.ProductCode) &&
                                                t.Product.SecondCategoryId == 12)
                                            .ToList();
                                        if (jiYouProduct.Any())
                                        {
                                            var accessoryConfig =
                                                await _baoYangService.GetPartAccessoryByTidItem(request.Tid, "发动机油");
                                            int volume = 0;
                                            foreach (var itemP in jiYouProduct)
                                            {
                                                int itemVolume =
                                                    Convert.ToInt32(Regex.Replace(itemP.Product.Unit, "\\D", ""));
                                                int count = realProduct.FirstOrDefault(t =>
                                                                t.ProjectId == itemP.Product.ProductCode)?.Quantity ??
                                                            1;
                                                volume = volume + itemVolume * count;
                                            }

                                            if (accessoryConfig != null &&
                                                !string.IsNullOrEmpty(accessoryConfig.Volume))
                                            {
                                                int volumeD = (int) Math.Ceiling(decimal.Parse(accessoryConfig.Volume));
                                                if (volume > volumeD)
                                                {
                                                    adaptive = false;
                                                }

                                                //if (volume != volumeD)
                                                //{
                                                //    adaptive = false;
                                                //}
                                            }
                                            else
                                            {
                                                adaptive = false;
                                            }
                                        }

                                        #endregion
                                    }

                                    var itemPart = itemChild.Where(t => t.ProjectType == 2).ToList();
                                    if (itemPart.Any())
                                    {
                                        foreach (var itemBaoYang in itemPart)
                                        {
                                            var defaultData = baoYangPart.FirstOrDefault(x =>
                                                x.PartType == itemBaoYang.ProjectId && string.Join(",", x.BrandList) ==
                                                string.Join(",", itemBaoYang.ProjectBrands))?.Pid;
                                            if (string.IsNullOrEmpty(defaultData))
                                            {
                                                adaptive = false;
                                                break;
                                            }
                                        }
                                    }

                                    if (adaptive)
                                    {
                                        result.Add(new BaoYangPackageVo()
                                        {
                                            PackagePid = _.ProductCode,
                                            PackageName = _.Name
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            result.RemoveAll(_ => relatedPid.Contains(_.PackagePid));

            return result;
        }

        private async Task<IEnumerable<BaoYangTypeConfigDO>> GetBaoYangTypeConfigCache()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":GetBaoYangTypeConfig:{timestamp}";
            var packageConfig = await RedisHelper.GetOrSetAsync(_redisClient, key,
                () => _baoYangTypeConfigRepository.GetBaoYangTypeConfigAsync(),
                new TimeSpan(0, 2, 0, 0));
            return packageConfig;
        }

        public void TestIni()
        {
            var configXml =
                "<BaoYangConfig><BaoYangTypes><!--Accessory--><BaoYangType type=\"jiyou\" displayName=\"机油\" zh=\"机油\" names=\"发动机油,机油\" catalogName=\"Oil\" group=\"Accessory\" /><BaoYangType type=\"lm\" displayName=\"空调制冷剂\" zh=\"空调制冷剂\" names=\"空调制冷剂\" catalogName=\"Refrigerant\" group=\"Accessory\" /><BaoYangType type=\"ldy\" displayName=\"冷冻油\" zh=\"冷冻油\" names=\"冷冻油\" catalogName=\"LDY\" group=\"Accessory\" /><BaoYangType type=\"gfd\" displayName=\"更换防冻冷却液\" zh=\"更换防冻冷却液\" names=\"发动机防冻液\" catalogName=\"Antifreeze\" group=\"Accessory\" /><BaoYangType type=\"scy\" displayName=\"刹车油\" zh=\"刹车油\" names=\"刹车油\" catalogName=\"Dampingoil\" group=\"Accessory\" /><BaoYangType type=\"mmto\" displayName=\"变速箱油\" zh=\"手动变速箱油\" names=\"变速箱油\" catalogName=\"ManualTransFluid\" group=\"Accessory\" /><BaoYangType type=\"zxy\" displayName=\"助力转向油\" zh=\"助力转向油\" names=\"助力转向油\" catalogName=\"PowerSteeringFluid\" group=\"Accessory\" /><!--Part--><BaoYangType type=\"ato\" displayName=\"变速箱油\" zh=\"自动变速箱油\" names=\"变速箱油\" catalogName=\"AutoTransFluid\" group=\"Part\" /><BaoYangType type=\"atop\" displayName=\"自动变速箱养护包\" zh=\"自动变速箱养护包\" names=\"自动变速箱养护包\" catalogName=\"ZDBSXYXLB\" group=\"Part\" /><BaoYangType type=\"jv\" displayName=\"机油滤清器\" zh=\"机油滤清器\" names=\"机油滤清器,机油滤清器滤芯\" catalogName=\"OilFilter\" group=\"Part\" /><BaoYangType type=\"odb\" displayName=\"放油螺栓\" zh=\"放油螺栓\" names=\"放油螺栓或垫片,放油螺栓\" catalogName=\"ODP\" group=\"Part\" /><BaoYangType type=\"qv\" displayName=\"空气滤清器\" zh=\"空气滤清器\" names=\"空气滤清器,空气滤清器芯\" catalogName=\"AirFilter\" group=\"Part\" /><BaoYangType type=\"rv\" displayName=\"燃油滤清器\" zh=\"燃油滤清器\" names=\"燃油滤清器\" catalogName=\"FuelFilter\" group=\"Part\" /><BaoYangType type=\"irv\" displayName=\"内置燃油滤\" zh=\"内置燃油滤\" names=\"燃油泵内置滤清器\" catalogName=\"FuelFilter\" group=\"Part\" /><BaoYangType type=\"okv\" displayName=\"空调滤清器\" zh=\"空调滤清器\" names=\"空调滤清器（外）\" catalogName=\"CabinFilter\" group=\"Part\" /><BaoYangType type=\"ikv\" displayName=\"内置空调滤清器\" zh=\"内置空调滤清器\" names=\"空调滤清器\" catalogName=\"CabinFilter\" group=\"Part\" /><BaoYangType type=\"zys\" displayName=\"左前雨刷\" zh=\"左前雨刷\" names=\"左前雨刷\" catalogName=\"Wiper\" group=\"Part\" /><BaoYangType type=\"yys\" displayName=\"右前雨刷\" zh=\"右前雨刷\" names=\"右前雨刷\" catalogName=\"Wiper\" group=\"Part\" /><BaoYangType type=\"qys\" displayName=\"前雨刷\" zh=\"前雨刷\" names=\"前雨刷\" catalogName=\"Wiper\" group=\"Part\" /><BaoYangType type=\"hys\" displayName=\"非总成后雨刷\" zh=\"非总成后雨刷\" names=\"非总成后雨刷\" catalogName=\"Wiper\" group=\"Part\" /><BaoYangType type=\"hysa\" displayName=\"总成后雨刷\" zh=\"总成后雨刷\" names=\"总成后雨刷\" catalogName=\"Wiper\" group=\"Part\" /><BaoYangType type=\"scpanf\" displayName=\"刹车盘前\" zh=\"刹车盘前\" names=\"刹车盘前\" catalogName=\"BrakeDisc\" group=\"Part\" /><BaoYangType type=\"scpanb\" displayName=\"刹车盘后\" zh=\"刹车盘后\" names=\"刹车盘后\" catalogName=\"BrakeDisc\" group=\"Part\" /><BaoYangType type=\"scpf\" displayName=\"刹车片前\" zh=\"刹车片前\" names=\"刹车片前\" catalogName=\"BrakePad\" group=\"Part\" /><BaoYangType type=\"scpb\" displayName=\"刹车片后\" zh=\"刹车片后\" names=\"刹车片后\" catalogName=\"BrakePad\" group=\"Part\" /><BaoYangType type=\"sclf\" displayName=\"前刹车报警线\" zh=\"前刹车报警线\" names=\"前刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"sclb\" displayName=\"后刹车报警线\" zh=\"后刹车报警线\" names=\"后刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"sclyf\" displayName=\"右前刹车报警线\" zh=\"右前刹车报警线\" names=\"右前刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"sclyb\" displayName=\"右后刹车报警线\" zh=\"右后刹车报警线\" names=\"右后刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"sclzf\" displayName=\"左前刹车报警线\" zh=\"左前刹车报警线\" names=\"左前刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"sclzb\" displayName=\"左后刹车报警线\" zh=\"左后刹车报警线\" names=\"左后刹车报警线\" catalogName=\"SCGYBJX1\" group=\"Part\" /><BaoYangType type=\"lfsa\" displayName=\"左前减振器\" zh=\"左前减振器\" names=\"左前减振器\" catalogName=\"JZQ\" group=\"Part\" /><BaoYangType type=\"rfsa\" displayName=\"右前减振器\" zh=\"右前减振器\" names=\"右前减振器\" catalogName=\"JZQ\" group=\"Part\" /><BaoYangType type=\"lbsa\" displayName=\"左后减振器\" zh=\"左后减振器\" names=\"左后减振器\" catalogName=\"JZQ\" group=\"Part\" /><BaoYangType type=\"rbsa\" displayName=\"右后减振器\" zh=\"右后减振器\" names=\"右后减振器\" catalogName=\"JZQ\" group=\"Part\" /><BaoYangType type=\"hhs\" displayName=\"火花塞\" zh=\"火花塞\" names=\"火花塞\" catalogName=\"Sparkplug\" group=\"Part\" /><BaoYangType type=\"battery\" displayName=\"蓄电池\" zh=\"蓄电池\" names=\"蓄电池\" catalogName=\"Battery\" group=\"Part\" /><BaoYangType type=\"fdyn\" displayName=\"远光灯\" zh=\"远光灯\" names=\"远光灯\" catalogName=\"Dynamo\" group=\"Part\" /><BaoYangType type=\"ndyn\" displayName=\"近光灯\" zh=\"近光灯\" names=\"近光灯\" catalogName=\"Dynamo\" group=\"Part\" /><BaoYangType type=\"fndyn\" displayName=\"远近一体灯\" zh=\"远近一体灯\" names=\"远近一体\" catalogName=\"Dynamo\" group=\"Part\" /><BaoYangType type=\"ffdyn\" displayName=\"雾灯\" zh=\"雾灯\" names=\"前雾灯\" catalogName=\"Dynamo\" group=\"Part\" /><BaoYangType type=\"tbk\" displayName=\"正时皮带套装\" zh=\"正时皮带套装\" names=\"正时皮带套装\" catalogName=\"TimingBeltSet\" group=\"Part\" /><BaoYangType type=\"gbs\" displayName=\"发电机皮带套装\" zh=\"发电机皮带套装\" names=\"发电机皮带套装\" catalogName=\"DJPDTZ\" group=\"Part\" /><BaoYangType type=\"ppbs\" displayName=\"助力泵皮带套装\" zh=\"助力泵皮带套装\" names=\"助力泵皮带套装\" catalogName=\"ZLBPDTZ\" group=\"Part\" /><BaoYangType type=\"accbs\" displayName=\"空调压缩机皮带套装\" zh=\"空调压缩机皮带套装\" names=\"空调压缩机皮带套装\" catalogName=\"KTPDTZ\" group=\"Part\" /><BaoYangType type=\"wp\" displayName=\"水泵\" zh=\"水泵\" names=\"水泵\" catalogName=\"BYSB\" group=\"Part\" /><BaoYangType type=\"lfsmmk\" displayName=\"左前减振器顶胶\" zh=\"左前减振器顶胶\" names=\"左前减振器顶胶\" catalogName=\"JZQDJ\" group=\"Part\" /><BaoYangType type=\"rfsmmk\" displayName=\"右前减振器顶胶\" zh=\"右前减振器顶胶\" names=\"右前减振器顶胶\" catalogName=\"JZQDJ\" group=\"Part\" /><BaoYangType type=\"lbsmmk\" displayName=\"左后减振器顶胶\" zh=\"左后减振器顶胶\" names=\"左后减振器顶胶\" catalogName=\"JZQDJ\" group=\"Part\" /><BaoYangType type=\"rbsmmk\" displayName=\"右后减振器顶胶\" zh=\"右后减振器顶胶\" names=\"右后减振器顶胶\" catalogName=\"JZQDJ\" group=\"Part\" /><BaoYangType type=\"lfsap\" displayName=\"左前减振器修理包\" zh=\"左前减振器修理包\" names=\"左前减振器修理包\" catalogName=\"JZQXLB\" group=\"Part\" /><BaoYangType type=\"rfsap\" displayName=\"右前减振器修理包\" zh=\"右前减振器修理包\" names=\"右前减振器修理包\" catalogName=\"JZQXLB\" group=\"Part\" /><BaoYangType type=\"lbsap\" displayName=\"左后减振器修理包\" zh=\"左后减振器修理包\" names=\"左后减振器修理包\" catalogName=\"JZQXLB\" group=\"Part\" /><BaoYangType type=\"rbsap\" displayName=\"右后减振器修理包\" zh=\"右后减振器修理包\" names=\"右后减振器修理包\" catalogName=\"JZQXLB\" group=\"Part\" /><!--Maintainence--><BaoYangType type=\"rx\" displayName=\"燃油系统养护\" zh=\"燃油系统养护\" names=\"燃油系统养护\" catalogName=\"Additive\" group=\"Maintainence\" /><BaoYangType type=\"stc\" displayName=\"节气门清洗\" zh=\"节气门清洗\" names=\"节气门清洗\" catalogName=\"SolarTtermCleaning\" group=\"Maintainence\" /><BaoYangType type=\"ew\" displayName=\"发动机清洗\" zh=\"发动机清洗\" names=\"发动机清洗\" catalogName=\"EngLubCleaning\" group=\"Maintainence\" /><BaoYangType type=\"asc\" displayName=\"空调管路杀菌\" zh=\"空调管路杀菌\" names=\"空调管路杀菌\" catalogName=\"AirSystemCleaning\" group=\"Maintainence\" /><BaoYangType type=\"ebc\" displayName=\"蒸发箱清洗\" zh=\"蒸发箱清洗\" names=\"蒸发箱清洗\" catalogName=\"AirConditioningClean\" group=\"Maintainence\" /><BaoYangType type=\"bmk\" displayName=\"刹车系统养护\" zh=\"刹车系统养护\" names=\"刹车系统养护\" catalogName=\"BrakeMaintenanceKit\" group=\"Maintainence\" /><BaoYangType type=\"wtm\" displayName=\"冷却系统养护\" zh=\"冷却系统养护\" names=\"冷却系统养护\" catalogName=\"WaterTankMaintenance\" group=\"Maintainence\" /><BaoYangType type=\"wtc\" displayName=\"水箱清洗\" zh=\"水箱清洗\" names=\"水箱清洗\" catalogName=\"WaterTankCleaning\" group=\"Maintainence\" /><BaoYangType type=\"fic\" displayName=\"进气系统清洗\" zh=\"进气系统清洗\" names=\"进气系统清洗\" catalogName=\"FuelIntakeCleaning\" group=\"Maintainence\" /><BaoYangType type=\"twc\" displayName=\"三元催化清洗\" zh=\"三元催化清洗\" names=\"三元催化清洗\" catalogName=\"TWCCleaning\" group=\"Maintainence\" /><BaoYangType type=\"pyc\" displayName=\"喷油嘴清洗\" zh=\"喷油嘴清洗\" names=\"喷油嘴清洗\" catalogName=\"PYZCLEAN\" group=\"Maintainence\" /><BaoYangType type=\"lsm\" displayName=\"发动机养护\" zh=\"发动机养护\" names=\"发动机养护\" catalogName=\"Oil aAdditive\" group=\"Maintainence\" /><BaoYangType type=\"ecc\" displayName=\"发动机舱清洗\" zh=\"发动机舱清洗\" names=\"发动机舱清洗\" catalogName=\"EngineCompartmentCleaner\" group=\"Maintainence\" /><BaoYangType type=\"ebvc\" displayName=\"蒸发箱可视化清洗\" zh=\"蒸发箱可视化清洗\" names=\"蒸发箱可视化清洗\" catalogName=\"EvaporatorBoxVisualCleaning\" group=\"Maintainence\" /><BaoYangType type=\"asm\" displayName=\"空调压缩机管路养护\" zh=\"空调压缩机管路养护\" names=\"空调压缩机管路养护\" catalogName=\"AC\" group=\"Maintainence\" /><BaoYangType type=\"ecvc\" displayName=\"发动机缸内可视化清洗\" zh=\"发动机缸内可视化清洗\" names=\"发动机缸内可视化清洗\" catalogName=\"EngineVisualCleaning\" group=\"Maintainence\" /></BaoYangTypes><BaoYangPackages><BaoYangPackage type=\"xby\" items=\"jiyou,jv,odb\" zh=\"小保养服务\" suggestTip=\"5000km或6个月/次\" birefDescription=\"更换机油和机油滤芯，保护发动机正常工作\" No=\"1\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1221&amp;_k=vjosee\" /><BaoYangPackage type=\"dby\" items=\"jiyou,jv,odb,qv,rv\" zh=\"大保养服务\" suggestTip=\"10000km或12个月/次\" birefDescription=\"更换机油和机滤、空气滤、燃油滤\" No=\"2\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1204&amp;_k=vjosee\" /><BaoYangPackage type=\"irv\" items=\"irv\" zh=\"内置燃油滤\" suggestTip=\"50000km或36个月/次\" birefDescription=\"滤除发动机燃油气系统中的有害颗粒和水分\" No=\"23\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1217&amp;_k=vjosee\" /><BaoYangPackage type=\"fd\" items=\"gfd\" zh=\"更换防冻冷却液\" suggestTip=\"24个月/次\" birefDescription=\"防冻冷却液，让发动机处在合适的运转温度\" No=\"11\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1207&amp;_k=vjosee\" /><BaoYangPackage type=\"ys\" items=\"zys,yys,qys\" zh=\"前雨刷\" suggestTip=\"6个月/次\" birefDescription=\"保障驾驶员在雨雪天气视线清晰\" No=\"4\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1208&amp;_k=vjosee\" /><BaoYangPackage type=\"hys\" items=\"hys,hysa\" zh=\"后雨刷\" suggestTip=\"6个月/次\" birefDescription=\"保障驾驶员在雨雪天气视线清晰\" No=\"4\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=235842&amp;_k=vjosee\" /><BaoYangPackage type=\"rx\" items=\"rx\" zh=\"燃油系统养护\" suggestTip=\"5000km/次\" birefDescription=\"有效清洁燃油系统的积碳、胶质、油泥\" No=\"3\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=275764&amp;_k=vjosee\" /><BaoYangPackage type=\"kv\" items=\"okv,ikv\" zh=\"空调滤清器\" suggestTip=\"12个月/次\" birefDescription=\"有效过滤烟臭、花粉、尘埃、有害气体和异味\" No=\"10\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1205&amp;_k=vjosee\" /><BaoYangPackage type=\"scpan\" items=\"scpanf,scpanb\" zh=\"刹车盘\" suggestTip=\"90000km/次\" birefDescription=\"制动卡钳用刹车片夹住刹车盘，摩擦制动\" No=\"22\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=275763&amp;_k=vjosee\" /><BaoYangPackage type=\"scp\" items=\"scpf,scpb,sclf,sclb,sclyf,sclyb,sclzf,sclzb\" zh=\"刹车片\" catalogName=\"BrakePad\" suggestTip=\"45000km/次\" birefDescription=\"刹车皮，在刹车盘/鼓上产生摩擦制动效果\" No=\"21\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=275761&amp;_k=vjosee\" /><BaoYangPackage type=\"fsa\" items=\"lfsa,rfsa,lfsmmk,rfsmmk,lfsap,rfsap\" zh=\"前减振器\" catalogName=\"JZQ\" suggestTip=\"80000km/次或5年/次\" birefDescription=\"加速车架与车身振动的衰减，以改善汽车的行驶平顺性（舒适性）\" No=\"22\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=182855&amp;_k=vjosee\" /><BaoYangPackage type=\"bsa\" items=\"lbsa,rbsa,lbsmmk,rbsmmk,lbsap,rbsap\" zh=\"后减振器\" catalogName=\"JZQ\" suggestTip=\"80000km/次或5年/次\" birefDescription=\"加速车架与车身振动的衰减，以改善汽车的行驶平顺性（舒适性）\" No=\"22\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=182855&amp;_k=vjosee\" /><BaoYangPackage type=\"mto\" items=\"mmto,ato,atop\" zh=\"变速箱油\" suggestTip=\"60000km或24个月/次\" birefDescription=\"齿轮油/波箱油，保护变速箱正常工作\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=11002&amp;_k=vjosee\" /><BaoYangPackage type=\"lm\" items=\"lm,ldy\" zh=\"空调制冷剂\" suggestTip=\"制冷效果不佳时\" birefDescription=\"冷媒，雪种，热量的搬运工，爱车冬暖夏凉\" No=\"12\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1211&amp;_k=vjosee\" /><BaoYangPackage type=\"stc\" items=\"stc\" zh=\"节气门清洗\" catalogName=\"SolarTtermCleaning\" suggestTip=\"20000km或24个月/次\" birefDescription=\"清除节气门积碳，提升加速性能\" No=\"13\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1212&amp;_k=vjosee\" /><BaoYangPackage type=\"ew\" items=\"ew\" zh=\"发动机清洗\" catalogName=\"EngLubCleaning\" suggestTip=\"5000km或6个月/次\" birefDescription=\"清洁引擎内部积碳、提升发动机功效，降低噪音，减少燃油和机油损耗\" No=\"8\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183648&amp;_k=vjosee\" /><BaoYangPackage type=\"asc\" items=\"asc\" zh=\"空调管路杀菌\" catalogName=\"AirSystemCleaning\" suggestTip=\"10000km或12个月/次\" birefDescription=\"有效清除空调管路内的灰尘、霉菌和微生物\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183661&amp;_k=vjosee\" /><BaoYangPackage type=\"scy\" items=\"scy\" zh=\"刹车油\" catalogName=\"Dampingoil\" suggestTip=\"40000km或24个月/次\" birefDescription=\"制动液，用于液压制动系统传递制动压力\" No=\"20\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1215&amp;_k=vjosee\" /><BaoYangPackage type=\"hhs\" items=\"hhs\" zh=\"火花塞\" catalogName=\"Sparkplug\" suggestTip=\"30000km/次\" birefDescription=\"定期检查，防止积碳抖动，爱车一触即发\" No=\"19\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1216&amp;_k=vjosee\" /><BaoYangPackage type=\"bmk\" items=\"bmk\" zh=\"刹车系统养护\" catalogName=\"BrakeMaintenanceKit\" suggestTip=\"45000km/次\" birefDescription=\"刹车系统耐高温防咬合、深化清洁润滑保养\" No=\"6\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1218&amp;_k=vjosee\" /><BaoYangPackage type=\"wtm\" items=\"wtm\" zh=\"水箱防锈保护\" catalogName=\"WaterTankMaintenance\" suggestTip=\"24个月/次\" birefDescription=\"冷却系统中的锈蚀会大大降低冷却系统的效率，增加能耗，增大发动机负担，本品可保护冷却系统中的重要金属，并在水泵及冷却系统其他部件表面形成润滑保护层，从而延长水泵和冷却系统的使用寿命\" No=\"5\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1220&amp;_k=vjosee\" /><BaoYangPackage type=\"wtc\" items=\"wtc\" zh=\"水箱清洗\" catalogName=\"WaterTankCleaning\" suggestTip=\"24个月/次\" birefDescription=\"去除水箱水垢、污渍、锈斑，提高散热效果\" No=\"18\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1432&amp;_k=vjosee\" /><BaoYangPackage type=\"fic\" items=\"fic\" zh=\"进气系统清洗\" catalogName=\"FuelIntakeCleaning\" suggestTip=\"20000km或24个月/次\" birefDescription=\"清除进气管积碳，改善引擎耗油及马力不足\" No=\"14\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1431&amp;_k=vjosee\" /><BaoYangPackage type=\"twc\" items=\"twc\" zh=\"三元催化清洗\" catalogName=\"TWCCleaning\" suggestTip=\"20000km或24个月/次\" birefDescription=\"延长三元催化寿命，使尾气排放更清洁\" No=\"15\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1426&amp;_k=vjosee\" /><BaoYangPackage type=\"pyc\" items=\"pyc\" zh=\"喷油嘴清洗\" catalogName=\"PYZCLEAN\" suggestTip=\"20000km或24个月/次\" birefDescription=\"提高燃油雾化质量，促进完全燃烧\" No=\"16\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=1430&amp;_k=vjosee\" /><BaoYangPackage type=\"lsm\" items=\"lsm\" zh=\"发动机养护\" catalogName=\"Oil aAdditive\" suggestTip=\"5000km/次\" birefDescription=\"发动机修复剂抗磨剂，减少磨损，延长寿命\" No=\"16\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183676&amp;_k=vjosee\" /><BaoYangPackage type=\"battery\" items=\"battery\" zh=\"蓄电池\" catalogName=\"Battery\" suggestTip=\"24个月/次\" birefDescription=\"定期维护更换，避免电瓶老化造成出行不便\" No=\"18\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=2737&amp;_k=vjosee\" /><BaoYangPackage type=\"dyn\" items=\"fdyn,ndyn,fndyn\" zh=\"大灯\" catalogName=\"Dynamo\" suggestTip=\"照明效果不佳时\" birefDescription=\"夜间照明汽车头灯，分为近光灯和远光灯\" No=\"19\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=2733&amp;_k=vjosee\" /><BaoYangPackage type=\"zxy\" items=\"zxy\" zh=\"助力转向油\" suggestTip=\"40000km或24个月/次\" birefDescription=\"加注在助力转向系统，传递转向力\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=76525&amp;_k=vjosee\" /><BaoYangPackage type=\"ffdyn\" items=\"ffdyn\" zh=\"雾灯\" catalogName=\"Dynamo\" suggestTip=\"照明效果不佳时\" birefDescription=\"保障夜间开车和坏天气时驾驶安全性\" No=\"20\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=2735&amp;_k=vjosee\" /><BaoYangPackage type=\"tbk\" items=\"tbk\" zh=\"正时皮带套装\" suggestTip=\"60000km或24个月/次\" birefDescription=\"确保正时传动系统及发动机能处于理想状态\" No=\"25\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183691&amp;_k=vjosee\" /><BaoYangPackage type=\"abp\" items=\"gbs,ppbs,accbs\" zh=\"附件皮带套装\" suggestTip=\"60000km或24个月/次\" birefDescription=\"通过传动带与带轮的摩擦力驱动发电机、助力泵和空调压缩机等发动机附件系统。\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=305689&amp;_k=vjosee\" /><BaoYangPackage type=\"wp\" items=\"wp\" zh=\"水泵\" suggestTip=\"60000km或24个月/次\" birefDescription=\"泵送冷却液，帮助发动机散热，保证发动机和系统的正常工作\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=320578&amp;_k=vjoseev\" /><BaoYangPackage type=\"ecc\" items=\"ecc\" zh=\"发动机舱清洗\" suggestTip=\"10000km或12个月/次\" birefDescription=\"清除发动机表面上的油泥、蒙尘，保持散热性\" No=\"26\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183696&amp;_k=vjosee\" /><BaoYangPackage type=\"ebc\" items=\"ebc,ebvc\" zh=\"蒸发箱清洗\" suggestTip=\"10000km或12个月/次\" birefDescription=\"清除蒸发箱上的异物，保证空调空气质量，提高空调制冷效率\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=183667&amp;_k=vjosee\" /><BaoYangPackage type=\"asm\" items=\"asm\" zh=\"空调压缩机管路养护\" catalogName=\"AC\" suggestTip=\"24个月/次\" birefDescription=\"改善空调管路脏堵问题，提升空调系统制冷效果\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=231586&amp;_k=vjosee\" /><BaoYangPackage type=\"ecvc\" items=\"ecvc\" zh=\"发动机缸内可视化清洗\" suggestTip=\"首次推荐20000km，间隔20000km\" birefDescription=\"清洁燃烧室内积碳、节气门和节流阀处的积碳及残留物，改善爆震、怠速不稳、耗油及马力不足现象\" No=\"9\" descriptionLink=\"https://faxian.tuhu.cn/react/messageShow/#/?id=260421&amp;_k=vjosee\" /></BaoYangPackages><TypeNameMap><PartAccessory><Item type=\"jiyou\" accessoryName=\"发动机油\" /><Item type=\"gfd\" accessoryName=\"发动机防冻液\" /><Item type=\"scy\" accessoryName=\"制动液\" /><Item type=\"lm\" accessoryName=\"空调制冷剂\" /><Item type=\"ldy\" accessoryName=\"冷冻油\" /><Item type=\"mmto\" accessoryName=\"变速箱油\" /><Item type=\"zxy\" accessoryName=\"助力转向油\" /></PartAccessory><TuhuBaoYangParts><Item type=\"odb\" partNames=\"放油螺栓或垫片\" refType=\"PartCode\" /><Item type=\"ato\" partNames=\"自动变速箱油\" refType=\"PartNo\" /><Item type=\"atop\" partNames=\"自动变速箱养护包\" refType=\"PartCode\" /><Item type=\"jv\" partNames=\"机油滤清器,机油滤清器滤芯\" refType=\"PartCode\" /><Item type=\"qv\" partNames=\"空气滤清器,空气滤清器芯\" refType=\"PartCode\" /><Item type=\"rv\" partNames=\"燃油滤清器\" refType=\"PartCode\" /><Item type=\"irv\" partNames=\"燃油泵内置滤清器\" refType=\"PartCode\" /><Item type=\"okv\" partNames=\"空调滤清器（外）\" refType=\"PartCode\" /><Item type=\"ikv\" partNames=\"空调滤清器\" refType=\"PartCode\" /><Item type=\"zys\" partNames=\"左前雨刷\" refType=\"PartNo\" /><Item type=\"yys\" partNames=\"右前雨刷\" refType=\"PartNo\" /><Item type=\"qys\" partNames=\"前雨刷\" refType=\"PartNo\" /><Item type=\"hys\" partNames=\"后雨刷(非总成)\" refType=\"PartNo\" /><Item type=\"hysa\" partNames=\"后雨刷(总成)\" refType=\"PartNo\" /><Item type=\"pm\" partNames=\"PM 2.5\" refType=\"ProductId\" /><Item type=\"scpanf\" partNames=\"前制动盘\" refType=\"PartCode\" /><Item type=\"scpanb\" partNames=\"后制动盘\" refType=\"PartCode\" /><Item type=\"scpf\" partNames=\"前制动片\" refType=\"PartCode\" /><Item type=\"scpb\" partNames=\"后制动片\" refType=\"PartCode\" /><Item type=\"hhs\" partNames=\"火花塞\" refType=\"PartCode\" /><Item type=\"battery\" partNames=\"蓄电池\" refType=\"PartCode\" /><Item type=\"fdyn\" partNames=\"远光灯\" refType=\"PartNo\" /><Item type=\"ndyn\" partNames=\"近光灯\" refType=\"PartNo\" /><Item type=\"fndyn\" partNames=\"远近一体\" refType=\"PartNo\" /><Item type=\"ffdyn\" partNames=\"前雾灯\" refType=\"PartNo\" /><Item type=\"tbk\" partNames=\"正时套装\" refType=\"PartCode\" /><Item type=\"sclf\" partNames=\"前刹车报警线\" refType=\"PartCode\" /><Item type=\"sclb\" partNames=\"后刹车报警线\" refType=\"PartCode\" /><Item type=\"sclzf\" partNames=\"左前刹车报警线\" refType=\"PartCode\" /><Item type=\"sclzb\" partNames=\"左后刹车报警线\" refType=\"PartCode\" /><Item type=\"sclyf\" partNames=\"右前刹车报警线\" refType=\"PartCode\" /><Item type=\"sclyb\" partNames=\"右后刹车报警线\" refType=\"PartCode\" /><Item type=\"lfsa\" partNames=\"左前减振器\" refType=\"PartCode\" /><Item type=\"rfsa\" partNames=\"右前减振器\" refType=\"PartCode\" /><Item type=\"lbsa\" partNames=\"左后减振器\" refType=\"PartCode\" /><Item type=\"rbsa\" partNames=\"右后减振器\" refType=\"PartCode\" /><Item type=\"gbs\" partNames=\"发电机皮带套装\" refType=\"PartNo\" /><Item type=\"ppbs\" partNames=\"助力泵皮带套装\" refType=\"PartNo\" /><Item type=\"accbs\" partNames=\"空调压缩机皮带套装\" refType=\"PartNo\" /><Item type=\"wp\" partNames=\"水泵\" refType=\"PartCode\" /><Item type=\"lfsmmk\" partNames=\"左前减振器顶胶\" refType=\"PartNo\" /><Item type=\"rfsmmk\" partNames=\"右前减振器顶胶\" refType=\"PartNo\" /><Item type=\"lbsmmk\" partNames=\"左后减振器顶胶\" refType=\"PartNo\" /><Item type=\"rbsmmk\" partNames=\"右后减振器顶胶\" refType=\"PartNo\" /><Item type=\"lfsap\" partNames=\"左前减振器修理包\" refType=\"PartNo\" /><Item type=\"rfsap\" partNames=\"右前减振器修理包\" refType=\"PartNo\" /><Item type=\"lbsap\" partNames=\"左后减振器修理包\" refType=\"PartNo\" /><Item type=\"rbsap\" partNames=\"右后减振器修理包\" refType=\"PartNo\" /></TuhuBaoYangParts><PrioritySetting><Item type=\"fdyn\" partName=\"车灯\" /><Item type=\"ndyn\" partName=\"车灯\" /><Item type=\"fndyn\" partName=\"车灯\" /><Item type=\"ffdyn\" partName=\"车灯\" /><Item type=\"ew\" partName=\"发动机清洗\" /><Item type=\"jiyou\" partName=\"机油\" /><Item type=\"jv\" partName=\"机油滤清器\" /><Item type=\"stc\" partName=\"节气门清洗\" /><Item type=\"asc\" partName=\"空调管路杀菌\" /><Item type=\"ebc\" partName=\"蒸发箱清洗\" /><Item type=\"okv\" partName=\"空调滤清器\" /><Item type=\"ikv\" partName=\"空调滤清器\" /><Item type=\"lm\" partName=\"空调制冷剂\" /><Item type=\"qv\" partName=\"空气滤清器\" /><Item type=\"wtm\" partName=\"冷却系统养护\" /><Item type=\"rv\" partName=\"燃油滤清器\" /><Item type=\"rx\" partName=\"燃油系统养护\" /><Item type=\"lsm\" partName=\"发动机养护\" /><Item type=\"scpanf\" partName=\"刹车盘\" /><Item type=\"scpanb\" partName=\"刹车盘\" /><Item type=\"scpf\" partName=\"刹车片\" /><Item type=\"scpb\" partName=\"刹车片\" /><Item type=\"bmk\" partName=\"刹车系统养护\" /><Item type=\"scy\" partName=\"刹车油\" /><Item type=\"wtc\" partName=\"水箱清洗\" /><Item type=\"battery\" partName=\"蓄电池\" /><Item type=\"ys\" partName=\"雨刷\" /><Item type=\"ldy\" partName=\"冷冻油\" /><Item type=\"pyc\" partName=\"喷油嘴清洗\" /><Item type=\"twc\" partName=\"三元催化清洗\" /><Item type=\"fic\" partName=\"进气系统清洗\" /><Item type=\"ptc\" partName=\"底盘装甲\" /><Item type=\"mmto\" partName=\"手动变速箱油\" /><Item type=\"ato\" partName=\"自动变速箱油\" /><Item type=\"hhs\" partName=\"火花塞\" /><Item type=\"zxy\" partName=\"助力转向油\" /><Item type=\"tbk\" partName=\"正时皮带套装\" /><Item type=\"ecc\" partName=\"发动机舱清洗\" /><Item type=\"sclf\" partName=\"刹车报警线\" /><Item type=\"sclb\" partName=\"刹车报警线\" /><Item type=\"sclyf\" partName=\"刹车报警线\" /><Item type=\"sclyb\" partName=\"刹车报警线\" /><Item type=\"sclzf\" partName=\"刹车报警线\" /><Item type=\"sclzb\" partName=\"刹车报警线\" /><Item type=\"sa\" partName=\"减振器\" /><Item type=\"ebvc\" partName=\"蒸发箱可视化清洗\" /><Item type=\"asm\" partName=\"空调压缩机管路养护\" /><Item type=\"hys\" partName=\"非总成后雨刷\" /><Item type=\"hysa\" partName=\"总成后雨刷\" /><Item type=\"ecvc\" partName=\"发动机缸内可视化清洗\" /><Item type=\"odb\" partName=\"放油螺栓或垫片\" /><Item type=\"gbs\" partName=\"发电机皮带套装\" /><Item type=\"ppbs\" partName=\"助力泵皮带套装\" /><Item type=\"accbs\" partName=\"空调压缩机皮带套装\" /><Item type=\"wp\" partName=\"水泵\" /></PrioritySetting></TypeNameMap><VehicleFuelConfig><Fuel type=\"柴油\" notSupportedPackageTypes=\"hhs,rx,fic,twc,pyc,lsm,stc\" /><Fuel type=\"电动\" notSupportedPackageTypes=\"mto,dby,ecc,ew,fd,hhs,stc,fic,wtm,irv,pyc,rx,twc,wtc,tbk,zxy,xby,lsm\" /><Fuel type=\"纯电动\" notSupportedPackageTypes=\"mto,dby,ecc,ew,fd,hhs,stc,fic,wtm,irv,pyc,rx,twc,wtc,tbk,zxy,xby,lsm\" /><Fuel type=\"增程式电动\" notSupportedPackageTypes=\"mto,dby,ecc,ew,fd,hhs,stc,fic,wtm,irv,pyc,rx,twc,wtc,tbk,zxy,xby,lsm\" /><Fuel type=\"压缩天然气(CNG)\" notSupportedPackageTypes=\"twc\" /><Fuel type=\"汽油/压缩天然气(CNG)\" notSupportedPackageTypes=\"twc\" /><Fuel type=\"液化石油气(LPG)\" notSupportedPackageTypes=\"twc\" /><Fuel type=\"汽油/液化石油气(LPG)\" notSupportedPackageTypes=\"twc\" /></VehicleFuelConfig><BaoYangServiceConfig><BaoYangService type=\"irv\" serviceId=\"FU-BY-RY|\" /><BaoYangService type=\"xby\" serviceId=\"FU-BY-XBY|\" /><BaoYangService type=\"gfd\" serviceId=\"FU-BY-BYGH|1\" /><BaoYangService type=\"lm\" serviceId=\"FU-BY-BYGH|2\" /><BaoYangService type=\"ikv\" serviceId=\"FU-BY-KONGTIAO|\" /><BaoYangService type=\"okv\" serviceId=\"FU-BY-KONGTIAOW|\" /><BaoYangService type=\"pm\" serviceId=\"FU-PM-ANZHUANGFEI|\" /><BaoYangService type=\"ys\" serviceId=\"FU-BY-YUSHUA|\" /><BaoYangService type=\"hys\" serviceId=\"FU-TU-GHHYSP|1\" /><BaoYangService type=\"hysa\" serviceId=\"FU-TU-GHHYSPZC|1\" /><BaoYangService type=\"scpanb\" serviceId=\"FU-TU-ABD|\" /><BaoYangService type=\"scpanf\" serviceId=\"FU-TU-BBD|\" /><BaoYangService type=\"scpb\" serviceId=\"FU-TU-ABP|\" /><BaoYangService type=\"scpf\" serviceId=\"FU-TU-BBP|\" /><BaoYangService type=\"ew\" serviceId=\"FU-TU-FadongjiQXJ|1\" /><BaoYangService type=\"stc\" serviceId=\"FU-TU-Jieqimen|\" /><BaoYangService type=\"asc\" serviceId=\"FU-TU-Kongtiaoguanlu|\" /><BaoYangService type=\"ebc\" serviceId=\"FU-BY-KONGTIQX|1\" /><BaoYangService type=\"scy\" serviceId=\"FU-TU-Shacheyou|\" /><BaoYangService type=\"hhs\" serviceId=\"FU-TU-Huohuasai|\" /><BaoYangService type=\"qv\" serviceId=\"FU-BY-KONGQI|\" /><BaoYangService type=\"rv\" serviceId=\"FU-BY-RANYOU|\" /><BaoYangService type=\"bmk\" serviceId=\"FU-TU-SCYH|\" /><BaoYangService type=\"wtm\" serviceId=\"FU-BY-LQYH|\" /><BaoYangService type=\"wtc\" serviceId=\"FU-BY-BYQX|4\" /><BaoYangService type=\"fic\" serviceId=\"FU-BY-BYQX|1\" /><BaoYangService type=\"twc\" serviceId=\"FU-BY-BYQX|2\" /><BaoYangService type=\"pyc\" serviceId=\"FU-BY-BYQX|3\" /><BaoYangService type=\"ptc\" serviceId=\"FU-TU-DPZJFW|1\" /><BaoYangService type=\"battery\" serviceId=\"FU-TU-XDC|\" /><BaoYangService type=\"zxy\" serviceId=\"FU-TU-ZLZXY|1\" /><BaoYangService type=\"mmto\" serviceId=\"FU-TU-Biansuxiangyou|1\" /><BaoYangService type=\"ato\" serviceId=\"FU-TU-Biansuxiangyou|2\" /><BaoYangService type=\"ato,atop\" serviceId=\"FU-TU-Biansuxiangyou|3\" /><BaoYangService type=\"tbk\" serviceId=\"FU-TU-Pidai|1\" /><BaoYangService type=\"ecc\" serviceId=\"FU-BY-CDJCQX|1\" /><BaoYangService type=\"lfsa\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"rfsa\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"lbsa\" serviceId=\"FU-BY-GHJZQ|4\" /><BaoYangService type=\"rbsa\" serviceId=\"FU-BY-GHJZQ|4\" /><BaoYangService type=\"ebvc\" serviceId=\"FU-TUHU-ZFXKSHQX|1\" /><BaoYangService type=\"ecvc\" serviceId=\"FU-BY-GNJTQX|1\" /><BaoYangService type=\"asm\" serviceId=\"FU-BY-KOYSJGLYH|1\" /><BaoYangService type=\"ffdyn\" serviceId=\"FU-BY-CDAZ|1\" additionalServiceId=\"FU-BY-CDAZ|2\" /><BaoYangService type=\"fdyn\" serviceId=\"FU-BY-CDAZ|1\" additionalServiceId=\"FU-BY-CDAZ|2\" /><BaoYangService type=\"ndyn\" serviceId=\"FU-BY-CDAZ|1\" additionalServiceId=\"FU-BY-CDAZ|2\" /><BaoYangService type=\"fndyn\" serviceId=\"FU-BY-CDAZ|1\" additionalServiceId=\"FU-BY-CDAZ|2\" /><BaoYangService type=\"gbs\" serviceId=\"FU-TU-FJPD|1\" /><BaoYangService type=\"ppbs\" serviceId=\"FU-TU-FJPD|1\" /><BaoYangService type=\"accbs\" serviceId=\"FU-TU-FJPD|1\" /><BaoYangService type=\"wp\" serviceId=\"FU-BY-BYGHSB|1\" /><BaoYangService type=\"lfsmmk\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"rfsmmk\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"lbsmmk\" serviceId=\"FU-BY-GHJZQ|4\" /><BaoYangService type=\"rbsmmk\" serviceId=\"FU-BY-GHJZQ|4\" /><BaoYangService type=\"lfsap\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"rfsap\" serviceId=\"FU-BY-GHJZQ|3\" /><BaoYangService type=\"lbsap\" serviceId=\"FU-BY-GHJZQ|4\" /><BaoYangService type=\"rbsap\" serviceId=\"FU-BY-GHJZQ|4\" /></BaoYangServiceConfig><DependNumService><BaoYangService serviceId=\"FU-BY-CDAZ|1\" /><BaoYangService serviceId=\"FU-BY-BYGH|2\" /><BaoYangService serviceId=\"FU-TU-Huohuasai|\" /><BaoYangService serviceId=\"FU-TU-DPZJFW|1\" /></DependNumService><PropertyDescriptionConfig><PropertyDescription name=\"款型\" title=\"请选择车型款型\" content=\"您所选的车型存在多个款型，不同款型的车所用产品存在差异。\" /><PropertyDescription name=\"发动机\" title=\"请选择发动机型号\" content=\"您所选的车型存在多款发动机型号，不同发动机型号的车型，配件存在差异。\" /><PropertyDescription name=\"生产年月\" title=\"请选择生产年月\" content=\"您所选的车型存在多个生产年月，不同生产年月的车型，配件存在差异。\" /><PropertyDescription name=\"轮胎尺寸\" title=\"请选择轮胎尺寸\" content=\"您所选的车型存在多种轮胎尺寸，不同轮胎尺寸的车型，配件存在差异。\" /><PropertyDescription name=\"驱动形式\" title=\"请选择驱动形式\" content=\"您所选的车型存在多种驱动形式，不同驱动形式的车型，配件存在差异。\" /><PropertyDescription name=\"制动形式\" title=\"请选择制动形式\" content=\"您所选的车型存在多种制动类型，不同制动类型的车型，配件存在差异。\" /><PropertyDescription name=\"ABS(防抱死制动系统)\" title=\"请选择ABS配置信息\" content=\"您所选的车型存在多种配置，不同配置的车型，配件存在差异。\" /><PropertyDescription name=\"年款\" title=\"请选择年款\" content=\"您所选的车型存在多个年款，不同年款的车型，配件存在差异。\" /><PropertyDescription name=\"接口类型\" title=\"请选择接口类型\" content=\"您所选的车型存在多个接口类型，不同接口类型的车型，配件存在差异。\" /><PropertyDescription name=\"刹车类型\" title=\"请选择刹车类型\" content=\"您所选的车型存在多种刹车类型，不同刹车类型的车型，配件存在差异。\" /><PropertyDescription name=\"机滤类型\" title=\"请选择机滤类型\" content=\"您所选的车型存在多种机滤类型，不同机滤类型的车型，配件存在差异。\" /><PropertyDescription name=\"车灯类型\" title=\"请选择车灯类型\" content=\"您所选的车型存在多种车灯类型，不同车灯类型的车型，配件存在差异。\" /><PropertyDescription name=\"发动机排放标准\" title=\"请选择发动机排放标准\" content=\"您所选的车型存在多种发动机排放标准，不同发动机排放标准的车型，配件存在差异。\" /><PropertyDescription name=\"变速箱类型\" title=\"请选择变速箱类型\" content=\"您所选的车型存在多种变速箱类型，不同变速箱类型的车型，配件存在差异。\" /><PropertyDescription name=\"底盘号\" title=\"请选择底盘号\" content=\"您所选的车型存在多种底盘号，不同底盘号的车型，配件存在差异。\" /><PropertyDescription name=\"刹车片形状\" title=\"请选择刹车片形状\" content=\"您所选的车型存在多种刹车片形状，不同刹车片形状的车型，配件存在差异。\" /><PropertyDescription name=\"发动机功率\" title=\"请选择发动机功率\" content=\"您所选的车型存在多种发动机功率，不同发动机功率的车型，配件存在差异。\" /><PropertyDescription name=\"电瓶位置\" title=\"请选择电瓶位置\" content=\"您所选的车型存在多种电瓶位置，不同电瓶位置的车型，配件存在差异。\" /><PropertyDescription name=\"启停功能\" title=\"请选择启停功能\" content=\"您所选的车型需要选择启停功能，有无启停功能的车型，配件存在差异。\" /></PropertyDescriptionConfig><ShaidanConfig><MenuItem name=\"小保养\" categories=\"Oil,OilFilter,ODB\" /><MenuItem name=\"大保养\" categories=\"Oil,OilFilter,AirFilter,FuelFilter,ODB\" /><MenuItem name=\"发动机养护\" categories=\"Oil Additive\" /><MenuItem name=\"燃油系统养护\" categories=\"Additive\" /><MenuItem name=\"制动系统保养\" categories=\"Dampingoil,BrakePad,BrakeDisc,BrakeMaintenanceKit\" /><MenuItem name=\"冷却系统保养\" categories=\"Antifreeze,WaterTankCleaning,WaterTankMaintenance\" /><MenuItem name=\"空调系统养护\" categories=\"AirFilter,FilterElement,AirSystemCleaning,Refrigerant,AirConditioningClean,AC,EvaporatorBoxVisualCleaning\" /><MenuItem name=\"引擎深度养护\" categories=\"Sparkplug,SolarTtermCleaning,PYZCLEAN,FuelIntakeCleaning,TWCCleaning,TimingBeltSet,EngLubCleaning,DJPDTZ,ZLBPDTZ,KTPDTZ,BYSB\" /><MenuItem name=\"变速箱养护\" categories=\"Oilchemicals\" /><MenuItem name=\"雨刷\" categories=\"Wiper\" /><MenuItem name=\"大灯\" categories=\"Dynamo\" /><MenuItem name=\"蓄电池\" categories=\"battery\" /><MenuItem name=\"助力转向油\" categories=\"PowerSteeringFluid\" /><MenuItem name=\"减振器\" categories=\"JZQ,JZQDJ,JZQXLB\" /></ShaidanConfig><BaoYangManualTablePartMatch><Package name=\"小保养服务\" parts=\"发动机油,机油滤清器,放油螺栓或垫片\" /><Package name=\"大保养服务\" parts=\"空气滤清器,发动机油,机油滤清器,燃油滤清器，放油螺栓或垫片\" /><Package name=\"内置燃油滤\" parts=\"内置燃油滤\" /><Package name=\"更换防冻冷却液\" parts=\"防冻液\" /><Package name=\"雨刷\" parts=\"雨刷\" /><Package name=\"燃油系统养护\" parts=\"燃油添加剂\" /><Package name=\"空调滤清器\" parts=\"空调滤清器,外置空调滤清器\" /><Package name=\"刹车盘\" parts=\"刹车盘\" /><Package name=\"刹车片\" parts=\"刹车片\" /><Package name=\"变速箱油\" parts=\"变速箱油,变速箱滤网\" /><Package name=\"空调制冷剂\" parts=\"空调制冷剂\" /><Package name=\"助力转向油\" parts=\"助力转向油\" /><Package name=\"制动液\" parts=\"制动液\" /><Package name=\"火花塞\" parts=\"火花塞\" /><Package name=\"正时皮带套装\" parts=\"正时皮带套装\" /><Package name=\"蓄电池\" parts=\"蓄电池\" /><Package name=\"减振器\" parts=\"减振器\" /></BaoYangManualTablePartMatch><DisplaySetting><PackageDescriptionSetting show=\"allshow\" /><SpecialPackageDescriptionSetting><Package type=\"xby\" biref=\"123123123123xxxxxxxxxxxxxxxxxxxxxxxxx\" h5url=\"https://baidu.com\" show=\"true\" /></SpecialPackageDescriptionSetting></DisplaySetting><InstallTypeConfig><Package type=\"mto\"><InstallType type=\"OnlyMmto\" name=\"\" baoyangTypes=\"mmto\" /><InstallType type=\"WithAtop\" name=\"含拆换滤油器服务\" baoyangTypes=\"ato,atop\" needAll=\"false\" /><InstallType type=\"WithoutAtop\" name=\"不含拆换滤油器服务\" baoyangTypes=\"ato\" /></Package><Package type=\"ys\"><InstallType type=\"QYS\" name=\"雨刷套装\" baoyangTypes=\"qys\" /><InstallType type=\"ZYYS\" name=\"雨刷单支装\" baoyangTypes=\"zys,yys\" isDefault=\"true\" /></Package><Package type=\"fsa\"><InstallType type=\"All\" name=\"前减振器\" baoyangTypes=\"lfsa,rfsa\" needAll=\"true\" /></Package><Package type=\"bsa\"><InstallType type=\"All\" name=\"后减振器\" baoyangTypes=\"lbsa,rbsa\" needAll=\"true\" /></Package></InstallTypeConfig><BaoYangPackageOrder>xby,dby,kv,pm,ys,lm,fd,scy,hhs,irv,scpan,scp,mto,battery,dyn,ffdyn,ew,stc,asc,ebc,rx,bmk,wtm,wtc,fic,twc,pyc,lsm,ptc,zxy,tbk,ecc,fsa,bsa</BaoYangPackageOrder></BaoYangConfig>";
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(configXml);
            var typeNodes = document.SelectNodes("/BaoYangConfig/BaoYangTypes/BaoYangType");
            var packageNodes = document.SelectNodes("/BaoYangConfig/BaoYangPackages/BaoYangPackage");
            var service = document.SelectNodes("/BaoYangConfig/BaoYangServiceConfig/BaoYangService");
            var parts = document.SelectNodes("/BaoYangConfig/TypeNameMap/TuhuBaoYangParts/Item");
            foreach (System.Xml.XmlNode node in parts)
            {
                //BaoYangPartsMapConfigDO da = new BaoYangPartsMapConfigDO()
                //{
                //    BaoYangType = node.Attributes["type"].Value,
                //    PartNames = node.Attributes["partNames"].Value,
                //    RefType = node.Attributes["refType"].Value,
                //    CreateBy = "System",
                //    CreateTime = DateTime.Now,
                //    UpdateBy = "System",
                //    UpdateTime = DateTime.Now
                //};
                //_baoYangPartsMapConfigRepository.Insert(da);
            }
            //foreach (System.Xml.XmlNode node in typeNodes)
            //{
            //    BaoYangTypeConfigDO da = new BaoYangTypeConfigDO()
            //    {
            //        BaoYangType = node.Attributes["type"].Value,
            //        DisplayName = node.Attributes["displayName"].Value,
            //        CategoryName = node.Attributes["catalogName"].Value,
            //        TypeGroup = node.Attributes["group"].Value,
            //        CreateBy = "System",
            //        CreateTime = DateTime.Now,
            //        UpdateBy = "",
            //        UpdateTime = DateTime.Now
            //    }; 
            //    _baoYangTypeConfigRepository.Insert(da);
            //}

            foreach (System.Xml.XmlNode node in packageNodes)
            {
                var type = node.Attributes["type"].Value;
                //var baoYangType = node.Attributes["items"].Value.Split(new[] {','}).ToList();
                //List<string> installService = new List<string>();
                //foreach (System.Xml.XmlNode node1 in service)
                //{
                //    var sd = node1.Attributes["type"].Value;
                //    if (sd == type || baoYangType.Contains(sd))
                //    {
                //        installService.Add(node1.Attributes["serviceId"].Value);
                //    }
                //}

                //BaoYangPackageConfigDO ff = new BaoYangPackageConfigDO()
                //{
                //    PackageType = type,
                //    DisplayName = node.Attributes["zh"].Value,
                //    SuggestTip = node.Attributes["suggestTip"].Value,
                //    BriefDescription = node.Attributes["birefDescription"].Value,
                //    DescriptionLink = node.Attributes["descriptionLink"].Value,
                //    DetailDescription = "",
                //    ProductType = 0,
                //    ImageUrl = "",
                //    ServiceId = string.Join(',', installService.Distinct()),
                //    DisplayIndex = Convert.ToInt32(node.Attributes["No"]?.Value ?? "0"),
                //    CreateBy = "System",
                //    CreateTime = DateTime.Now,
                //    UpdateBy = "",
                //    UpdateTime = DateTime.Now
                //};
                //_baoYangPackageConfigRepository.Insert(ff);



                //BaoYangTypeConfigDO da = new BaoYangTypeConfigDO()
                //{
                //    BaoYangType = type + "-tc",
                //    DisplayName = node.Attributes["zh"].Value + "套餐",
                //    CategoryName = "",
                //    TypeGroup = "Package",
                //    CreateBy = "System",
                //    CreateTime = DateTime.Now,
                //    UpdateBy = "",
                //    UpdateTime = DateTime.Now
                //};
                //_baoYangTypeConfigRepository.Insert(da);

                //BaoYangPackageMapConfigDO asa = new BaoYangPackageMapConfigDO()
                //{
                //    PackageType = type,
                //    BaoYangType = type + "-tc",
                //    CreateBy = "System",
                //    CreateTime = DateTime.Now,
                //    UpdateTime = DateTime.Now,
                //    UpdateBy = ""
                //};
                //_baoYangPackageMapConfigRepository.Insert(asa);
            }
        }

        /// <summary>
        /// 初始化配件数据
        /// </summary>
        /// <param name="partCode"></param>
        /// <param name="brand"></param>
        /// <param name="vehicle"></param>
        /// <param name="paiLiang"></param>
        /// <param name="startNian"></param>
        /// <param name="endNian"></param>
        /// <param name="nian"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        public async Task<bool> InitializeAdaptivePartConfig(string partCode, string brand, string vehicle,
            string paiLiang, string startNian, string endNian, string nian, string partName)
        {
            if (string.IsNullOrEmpty(partCode) || string.IsNullOrEmpty(brand))
            {
                return false;
            }

            var vehicleType = (await _vehicleTypeRepository.GetVehicleByBrand(brand))?.ToList() ??
                              new List<VehicleTypeDO>();
            if (!vehicleType.Any())
            {
                return false;
            }

            var brandList = vehicleType.Select(_ => _.Brand).Distinct().ToList();
            if (brandList.Count != 1)
            {
                return false;
            }

            brand = brandList[0].Substring(4);

            List<string> vehicleId = new List<string>();
            if (!string.IsNullOrEmpty(vehicle))
            {
                var vehicleList = vehicleType.Where(_ => _.Vehicle.IndexOf(vehicle) > -1).ToList();
                if (vehicleList.Count == 0)
                {
                    return false;
                }

                vehicleId = vehicleList.Select(_ => _.VehicleId).ToList();
            }

            var vehicleDetail =
                (await _vehicleTypeTimingRepository.SearchVehicleTypeTiming(brand, vehicleId, paiLiang, startNian,
                    endNian, nian, ""))?.ToList() ?? new List<VehicleTypeTimingDO>();
            if (!vehicleDetail.Any())
            {
                return false;
            }

            var tids = vehicleDetail.Select(_ => _.Tid).ToList();
            await Task.WhenAll(tids.Select(_ => InsertBaoYangPart(new BaoYangPartsDO()
            {
                Tid = _,
                PartName = partName,
                PartCode = partCode,
                OePartCode = "",
                Source = "ApolloErp",
                Brand = "AUK/优冠",
                Validated = true,
                CreateBy = "System",
                CreateTime = DateTime.Now
            })));

            return true;
        }

        private async Task<bool> InsertBaoYangPart(BaoYangPartsDO baoYangPartsDo)
        {
            var exist = await _baoYangPartsRepository.GetBaoYangPartsByTidAndCodeAsync(baoYangPartsDo.Tid,
                baoYangPartsDo.PartName,
                baoYangPartsDo.PartCode, false);
            if (exist == null || !exist.Any())
            {
                await _baoYangPartsRepository.InsertAsync(baoYangPartsDo);
            }

            return true;
        }

        #endregion

        #region 保养OE映射关系

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OeCodeMapVo>> GetOeCodeMapList(OeCodeMapRequest request)
        {
            ApiPagedResultData<OeCodeMapVo> data = new ApiPagedResultData<OeCodeMapVo>
            {
                Items = new List<OeCodeMapVo>()
            };
            var result = await _baoYangOeCodeMapRepository.GetOeCodeMap(request.OeCode, request.PartCode,
                request.PartType, request.PageIndex, request.PageSize);
            if (result != null)
            {
                data.TotalItems = result.Item2;
                if (result.Item1 != null && result.Item1.Any())
                {
                    data.Items = result.Item1.Select(t => new OeCodeMapVo
                    {
                        PartName = t.PartName,
                        OePartCode = t.OePartCode,
                        PartCode = t.PartCode,
                        VehicleBrand = t.VehicleBrand
                    }).ToList();
                }
            }

            return data;
        }

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OePartCodeDetailVo> GetOeCodeMapDetailByOeCode(OeCodeMapDetailByOeCodeRequest request)
        {
            var detail = (await _baoYangOeCodeMapRepository.GetPartCodeByOe(request.OePartCode))?.ToList() ??
                         new List<BaoYangOeCodeMapDO>();
            if (detail.Any())
            {
                var defaultItem = detail[0];
                return new OePartCodeDetailVo()
                {
                    OePartCode = defaultItem.OePartCode,
                    PartType = defaultItem.PartName,
                    VehicleBrand =
                        defaultItem.VehicleBrand?.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                        new List<string>(),
                    PartCodes = detail.Select(_ => new ParCodeDetailVo
                    {
                        Brand = _.Brand,
                        PartCode = _.PartCode
                    }).ToList()
                };
            }

            return null;
        }

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOePartCode(DeleteOePartCodeRequest request)
        {
            var result = await _baoYangOeCodeMapRepository.DeleteOePartCode(request.OePartCode, request.SubmitBy);

            return result > 0;
        }

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditOePartCode(EditOePartCodeRequest request)
        {
            var oldDetail = (await _baoYangOeCodeMapRepository.GetPartCodeByOe(request.OePartCode, false))?.ToList() ??
                            new List<BaoYangOeCodeMapDO>();
            var vehicleBrand = string.Empty;
            if (request.VehicleBrand != null && request.VehicleBrand.Any())
            {
                vehicleBrand = string.Join(";", request.VehicleBrand);
            }

            List<BaoYangOeCodeMapDO> addList = new List<BaoYangOeCodeMapDO>();
            List<BaoYangOeCodeMapDO> updateList = new List<BaoYangOeCodeMapDO>();
            List<BaoYangOeCodeMapDO> deleteList = new List<BaoYangOeCodeMapDO>();
            var codeList = request.PartCode ?? new List<ParCodeDetailRequest>();
            codeList = codeList.Distinct(_ => _.PartCode).ToList();
            if (!codeList.Any())
            {
                codeList.Add(new ParCodeDetailRequest()
                {
                    Brand = "",
                    PartCode = ""
                });
            }

            foreach (var itemCode in codeList)
            {
                var defaultCode = oldDetail.FirstOrDefault(_ =>
                    _.OePartCode == request.OePartCode && _.PartCode == itemCode.PartCode);
                if (defaultCode == null)
                {
                    addList.Add(new BaoYangOeCodeMapDO()
                    {
                        OePartCode = request.OePartCode,
                        PartCode = itemCode.PartCode,
                        Brand = itemCode.Brand,
                        Source = "ApolloErp",
                        PartName = request.PartName,
                        VehicleBrand = vehicleBrand,
                        CreateBy = request.SubmitBy,
                        CreateTime = DateTime.Now
                    });
                }
                else
                {
                    oldDetail.RemoveAll(_ => _.Id == defaultCode.Id);
                    updateList.Add(new BaoYangOeCodeMapDO()
                    {
                        Id = defaultCode.Id,
                        OePartCode = request.OePartCode,
                        PartCode = itemCode.PartCode,
                        Brand = itemCode.Brand,
                        Source = "ApolloErp",
                        PartName = request.PartName,
                        VehicleBrand = vehicleBrand,
                        UpdateBy = request.SubmitBy,
                        UpdateTime = DateTime.Now
                    });
                }
            }

            deleteList = oldDetail.Select(t => new BaoYangOeCodeMapDO
            {
                Id = t.Id,
                IsDeleted = true,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            }).ToList();

            using (TransactionScope ts = new TransactionScope())
            {
                if (addList.Any())
                {
                    await _baoYangOeCodeMapRepository.InsertBatchAsync(addList);
                }

                if (updateList.Any())
                {
                    foreach (var itemUpdate in updateList)
                    {
                        await _baoYangOeCodeMapRepository.UpdateAsync(itemUpdate, new List<string>()
                        {
                            "Brand", "PartName", "VehicleBrand", "UpdateBy", "UpdateTime"
                        });
                    }
                }

                if (deleteList.Any())
                {
                    foreach (var itemUpdate in deleteList)
                    {
                        await _baoYangOeCodeMapRepository.UpdateAsync(itemUpdate, new List<string>()
                        {
                            "IsDeleted", "UpdateBy", "UpdateTime"
                        });
                    }
                }

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddOePartCode(AddOePartCodeRequest request)
        {
            var existOeCode = await _baoYangOeCodeMapRepository.GetPartCodeByOe(request.OePartCode, false);
            if (existOeCode.Any())
            {
                throw new CustomException($"已存在OE号：{request.OePartCode} ！");
            }

            var vehicleBrand = string.Empty;
            if (request.VehicleBrand != null && request.VehicleBrand.Any())
            {
                vehicleBrand = string.Join(";", request.VehicleBrand);
            }

            List<BaoYangOeCodeMapDO> oeCodeMapList = new List<BaoYangOeCodeMapDO>();
            if (request.PartCode != null && request.PartCode.Any())
            {
                var distinctPartCode = request.PartCode.Distinct(_ => _.PartCode).ToList();
                oeCodeMapList = distinctPartCode.Select(_ => new BaoYangOeCodeMapDO
                {
                    OePartCode = request.OePartCode,
                    PartCode = _.PartCode,
                    Brand = _.Brand,
                    Source = "ApolloErp",
                    PartName = request.PartName,
                    VehicleBrand = vehicleBrand,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                }).ToList();
            }
            else
            {
                oeCodeMapList.Add(new BaoYangOeCodeMapDO()
                {
                    OePartCode = request.OePartCode,
                    PartCode = "",
                    Brand = "",
                    Source = "ApolloErp",
                    PartName = request.PartName,
                    VehicleBrand = vehicleBrand,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                });
            }

            await _baoYangOeCodeMapRepository.InsertBatchAsync(oeCodeMapList);

            return true;
        }

        #endregion

        #region Package 配置

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageConfigVo>> GetPackageTypeConfig(
            PackageTypeConfigRequest request)
        {
            var resultTask = _baoYangPackageConfigRepository.GetAllBaoYangPackageConfigAsync();
            var packageMapTask = _baoYangPackageMapConfigRepository.GetPackageMapConfigAsync();
            var categoryConfigTask = _baoYangCategoryConfigRepository.GetBaoYangCategoryConfigAsync(); //一级分类
            await Task.WhenAll(resultTask, packageMapTask, categoryConfigTask);
            var result = resultTask.Result ?? new List<BaoYangPackageConfigDO>();
            var packageMap = packageMapTask.Result?.ToList() ?? new List<BaoYangPackageMapConfigDO>();
            var categoryConfig = categoryConfigTask.Result?.ToList() ?? new List<BaoYangCategoryConfigDO>();

            if (!string.IsNullOrEmpty(request.DisplayName))
            {
                result = result.Where(_ => _.DisplayName.Contains(request.DisplayName)).ToList();
            }

            if (request.IsDeleted == 1)
            {
                result = result.Where(_ => _.IsDeleted).ToList();
            }
            else if (request.IsDeleted == 2)
            {
                result = result.Where(_ => !_.IsDeleted).ToList();
            }

            var package = _mapper.Map<List<BaoYangPackageConfigVo>>(result);
            package.ForEach(_ =>
            {
                var defaultCategory = categoryConfig.FirstOrDefault(f => f.PackageType == _.PackageType);
                _.CategoryAlias = defaultCategory?.CategoryAlias ?? string.Empty;
                _.CategoryName = defaultCategory?.CategoryName ?? string.Empty;
                _.BaoYangType = packageMap.Where(x => x.PackageType == _.PackageType).Select(x => x.BaoYangType)
                    .ToList();
            });

            if (!string.IsNullOrEmpty(request.CategoryAlias))
            {
                package = package.Where(_ => _.CategoryAlias == request.CategoryAlias).ToList();
            }

            return new ApiPagedResultData<BaoYangPackageConfigVo>()
            {
                TotalItems = result.Count,
                Items = package.OrderBy(_ => _.DisplayIndex).Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).ToList()
            };
        }

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigVo>> GetValidBaoYangTypeConfig()
        {
            var result = await _baoYangTypeConfigRepository.GetAllBaoYangTypeConfigAsync();

            return result?.Select(_ => new BaoYangTypeConfigVo
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName,
                ImageUrl = _.ImageUrl
            })?.ToList() ?? new List<BaoYangTypeConfigVo>();
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangCategoryConfigVo>> GetBaoYangCategoryConfig()
        {
            List<BaoYangCategoryConfigVo> result = new List<BaoYangCategoryConfigVo>();
            var categoryConfig = await _baoYangCategoryConfigRepository.GetBaoYangCategoryConfigAsync(); //一级分类

            var categoryGroup = categoryConfig.GroupBy(o => o.CategoryAlias)
                .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());

            foreach (var category in categoryGroup)
            {
                result.Add(new BaoYangCategoryConfigVo()
                {
                    CategoryAlias = category.Key,
                    CategoryName = category.Value[0].CategoryName,
                    CategorySimpleName = category.Value[0].CategorySimpleName
                });
            }

            return result;
        }

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageTypeConfig(EditPackageTypeConfigRequest request)
        {
            await CheckPackageType(request.Id, request.PackageType);
            BaoYangPackageConfigDO baoYangPackageConfig = new BaoYangPackageConfigDO()
            {
                Id = request.Id,
                UpdateBy = request.UpdateBy,
                UpdateTime = DateTime.Now
            };
            List<string> updateFields = new List<string>();
            if (request.PackageType != null)
            {
                baoYangPackageConfig.PackageType = request.PackageType;
                updateFields.Add("PackageType");
            }

            if (request.DisplayName != null)
            {
                baoYangPackageConfig.DisplayName = request.DisplayName;
                updateFields.Add("DisplayName");
            }

            if (request.SuggestTip != null)
            {
                baoYangPackageConfig.SuggestTip = request.SuggestTip;
                updateFields.Add("SuggestTip");
            }

            if (request.BriefDescription != null)
            {
                baoYangPackageConfig.BriefDescription = request.BriefDescription;
                updateFields.Add("BriefDescription");
            }

            if (request.DescriptionLink != null)
            {
                baoYangPackageConfig.DescriptionLink = request.DescriptionLink;
                updateFields.Add("DescriptionLink");
            }

            if (request.DetailDescription != null)
            {
                baoYangPackageConfig.DetailDescription = request.DetailDescription;
                updateFields.Add("DetailDescription");
            }

            if (request.ProductType != null)
            {
                baoYangPackageConfig.ProductType = request.ProductType.Value;
                updateFields.Add("ProductType");
            }

            if (request.ImageUrl != null)
            {
                baoYangPackageConfig.ImageUrl = request.ImageUrl;
                updateFields.Add("ImageUrl");
            }

            if (request.DisplayIndex != null)
            {
                baoYangPackageConfig.DisplayIndex = request.DisplayIndex.Value;
                updateFields.Add("DisplayIndex");
            }

            if (request.ServiceId != null)
            {
                baoYangPackageConfig.ServiceId = request.ServiceId;
                updateFields.Add("ServiceId");
            }

            if (request.IsDeleted != null)
            {
                baoYangPackageConfig.IsDeleted = request.IsDeleted.Value;
                updateFields.Add("IsDeleted");
            }

            if (request.BaoYangType != null)
            {
                await UpdatePackageMapConfig(request.PackageType, request.BaoYangType, request.UpdateBy);
            }

            if (request.CategoryAlias != null)
            {
                await EditBaoYangCategoryConfig(request.PackageType, request.CategoryAlias, request.UpdateBy);
            }

            if (updateFields.Any())
            {
                updateFields.Add("UpdateBy");
                updateFields.Add("UpdateTime");

                await _baoYangPackageConfigRepository.UpdateAsync(baoYangPackageConfig, updateFields);
            }

            var refreshTask = _baoYangService.RefreshPackageDescriptionConfig();

            return true;
        }

        /// <summary>
        /// packageType重复校验
        /// </summary>
        /// <param name="id"></param>
        /// <param name="packageType"></param>
        /// <returns></returns>
        private async Task CheckPackageType(int id, string packageType)
        {
            if (!string.IsNullOrEmpty(packageType))
            {
                var result = (await _baoYangPackageConfigRepository.GetAllBaoYangPackageConfigAsync())?.ToList() ??
                             new List<BaoYangPackageConfigDO>();
                if (result.Exists(_ => _.PackageType == packageType && _.Id != id))
                {
                    throw new CustomException($"已存在：PackageType={packageType}的数据，请更改PackageType");
                }
            }
        }

        /// <summary>
        /// UpdatePackageMapConfig
        /// </summary>
        /// <param name="packageType"></param>
        /// <param name="baoYangType"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        private async Task<bool> UpdatePackageMapConfig(string packageType, List<string> baoYangType, string submitBy)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                await _baoYangPackageMapConfigRepository.DeletePackageMapConfig(packageType, submitBy);

                if (baoYangType != null && baoYangType.Any())
                {
                    List<BaoYangPackageMapConfigDO> mapList = baoYangType.Select(_ => new BaoYangPackageMapConfigDO
                    {
                        PackageType = packageType,
                        BaoYangType = _,
                        CreateBy = submitBy,
                        CreateTime = DateTime.Now,
                        UpdateBy = string.Empty,
                        UpdateTime = new DateTime(1900, 1, 1)
                    }).ToList();

                    await _baoYangPackageMapConfigRepository.InsertBatchAsync(mapList);
                }

                ts.Complete();
            }

            var refreshPackageMapConfigTask = _baoYangService.RefreshPackageMapConfig();

            return true;
        }

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddPackageTypeConfig(AddPackageTypeConfigRequest request)
        {
            await CheckPackageType(-1, request.PackageType);

            var result = await _baoYangPackageConfigRepository.InsertAsync<int>(new BaoYangPackageConfigDO()
            {
                PackageType = request.PackageType,
                DisplayName = request.DisplayName,
                SuggestTip = request.SuggestTip,
                BriefDescription = request.BriefDescription,
                DescriptionLink = request.DescriptionLink,
                DetailDescription = request.DetailDescription,
                ProductType = request.ProductType,
                ImageUrl = request.ImageUrl,
                DisplayIndex = request.DisplayIndex,
                ServiceId = request.ServiceId,
                IsDeleted = request.IsDeleted,
                CreateBy = request.CreateBy,
                CreateTime = DateTime.Now,
                UpdateBy = string.Empty,
                UpdateTime = new DateTime(1900, 1, 1)
            });

            if (!string.IsNullOrEmpty(request.CategoryAlias))
            {
                await InsertBaoYangCategoryConfig(request.PackageType, request.CategoryAlias, request.CreateBy);
            }

            if (request.BaoYangType != null && request.BaoYangType.Any())
            {
                await UpdatePackageMapConfig(request.PackageType, request.BaoYangType, request.CreateBy);
            }

            var refreshTask = _baoYangService.RefreshPackageDescriptionConfig();

            return result;
        }

        /// <summary>
        /// Insert一级分类
        /// </summary>
        /// <param name="packageType"></param>
        /// <param name="categoryAlias"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        private async Task<bool> InsertBaoYangCategoryConfig(string packageType, string categoryAlias, string submitBy)
        {
            var categoryConfig =
                await _baoYangCategoryConfigRepository
                    .GetBaoYangCategoryConfigByAlias(categoryAlias); //一级分类
            if (categoryConfig != null)
            {
                await _baoYangCategoryConfigRepository.InsertAsync(new BaoYangCategoryConfigDO()
                {
                    CategoryAlias = categoryConfig.CategoryAlias,
                    CategoryName = categoryConfig.CategoryName,
                    CategorySimpleName = categoryConfig.CategorySimpleName,
                    PackageType = packageType,
                    CreateBy = submitBy,
                    CreateTime = DateTime.Now
                });
            }

            return true;
        }


        /// <summary>
        /// 编辑一级分类
        /// </summary>
        /// <param name="packageType"></param>
        /// <param name="categoryAlias"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        private async Task<bool> EditBaoYangCategoryConfig(string packageType, string categoryAlias, string submitBy)
        {
            var packageTypes =
                await _baoYangCategoryConfigRepository.GetBaoYangCategoryConfigByPackageType(packageType);
            foreach (var itemType in packageTypes)
            {
                if (itemType.CategoryAlias != categoryAlias)
                {
                    await _baoYangCategoryConfigRepository.UpdateAsync(new BaoYangCategoryConfigDO()
                        {
                            Id = itemType.Id,
                            IsDeleted = true,
                            UpdateBy = submitBy,
                            UpdateTime = DateTime.Now
                        },
                        new List<string>()
                        {
                            "IsDeleted", "UpdateBy", "UpdateTime"
                        });
                }
            }

            if (!string.IsNullOrEmpty(categoryAlias) && !packageTypes.Exists(_ => _.CategoryAlias == categoryAlias))
            {
                await InsertBaoYangCategoryConfig(packageType, categoryAlias, submitBy);
            }

            return true;
        }

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangTypeConfig>> GetBaoYangTypeConfig(BaoYangTypeConfigRequest request)
        {
            var result = await _baoYangTypeConfigRepository.GetAllBaoYangTypeConfigAsync();

            if (!string.IsNullOrEmpty(request.DisplayName))
            {
                result = result.Where(_ => _.DisplayName.Contains(request.DisplayName)).ToList();
            }

            if (request.IsDeleted == 1)
            {
                result = result.Where(_ => _.IsDeleted).ToList();
            }
            else if (request.IsDeleted == 2)
            {
                result = result.Where(_ => !_.IsDeleted).ToList();
            }

            if (!string.IsNullOrEmpty(request.TypeGroup))
            {
                result = result.Where(_ => _.TypeGroup == request.TypeGroup).ToList();
            }

            var data = result.OrderBy(_ => _.Id).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            return new ApiPagedResultData<BaoYangTypeConfig>()
            {
                TotalItems = result.Count,
                Items = _mapper.Map<List<BaoYangTypeConfig>>(data)
            };
        }

        /// <summary>
        /// 编辑BaoYangType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigRequest request)
        {
            await CheckBaoYangType(request.Id, request.BaoYangType);
            BaoYangTypeConfigDO baoYangTypeConfig = new BaoYangTypeConfigDO()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            List<string> updateFields = new List<string>();
            if (request.BaoYangType != null)
            {
                baoYangTypeConfig.BaoYangType = request.BaoYangType;
                updateFields.Add("BaoYangType");
            }

            if (request.DisplayName != null)
            {
                baoYangTypeConfig.DisplayName = request.DisplayName;
                updateFields.Add("DisplayName");
            }

            if (request.CategoryName != null)
            {
                baoYangTypeConfig.CategoryName = request.CategoryName;
                updateFields.Add("CategoryName");
            }

            if (request.ChildCategories != null)
            {
                baoYangTypeConfig.ChildCategories = request.ChildCategories;
                updateFields.Add("ChildCategories");
            }

            if (request.TypeGroup != null)
            {
                baoYangTypeConfig.TypeGroup = request.TypeGroup;
                updateFields.Add("TypeGroup");
            }

            if (request.ImageUrl != null)
            {
                baoYangTypeConfig.ImageUrl = request.ImageUrl;
                updateFields.Add("ImageUrl");
            }

            if (request.IsDeleted != null)
            {
                baoYangTypeConfig.IsDeleted = request.IsDeleted.Value;
                updateFields.Add("IsDeleted");
            }

            if (updateFields.Any())
            {
                updateFields.Add("UpdateBy");
                updateFields.Add("UpdateTime");

                await _baoYangTypeConfigRepository.UpdateAsync(baoYangTypeConfig, updateFields);

                var refreshTask = _baoYangService.RefreshPackageDescriptionConfig();
                var refreshBaoYangTypeConfigTask = _baoYangService.RefreshBaoYangTypeConfig();
            }

            return true;
        }

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigRequest request)
        {
            await CheckBaoYangType(-1, request.BaoYangType);

            var result = await _baoYangTypeConfigRepository.InsertAsync(new BaoYangTypeConfigDO()
            {
                BaoYangType = request.BaoYangType,
                DisplayName = request.DisplayName,
                CategoryName = request.CategoryName,
                ChildCategories = request.ChildCategories,
                TypeGroup = request.TypeGroup,
                ImageUrl = request.ImageUrl,
                IsDeleted = request.IsDeleted,
                CreateBy = request.CreateBy,
                CreateTime = DateTime.Now,
                UpdateBy = string.Empty,
                UpdateTime = new DateTime(1900, 1, 1)
            });

            var refreshTask = _baoYangService.RefreshPackageDescriptionConfig();
            var refreshBaoYangTypeConfigTask = _baoYangService.RefreshBaoYangTypeConfig();

            return result;
        }

        /// <summary>
        /// baoYangType重复校验
        /// </summary>
        /// <param name="id"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        private async Task CheckBaoYangType(int id, string baoYangType)
        {
            if (!string.IsNullOrEmpty(baoYangType))
            {
                var result = (await _baoYangTypeConfigRepository.GetAllBaoYangTypeConfigAsync())?.ToList() ??
                             new List<BaoYangTypeConfigDO>();
                if (result.Exists(_ => _.BaoYangType == baoYangType && _.Id != id))
                {
                    throw new CustomException($"已存在：BaoYangType={baoYangType}的数据，请更改BaoYangType");
                }
            }
        }

        #endregion

        #region 安装服务费加价配置

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<InstallAddFeeConfigVo>> GetInstallAddFeeConfig(
            InstallAddFeeConfigRequest request)
        {
            ApiPagedResultData<InstallAddFeeConfigVo> result = new ApiPagedResultData<InstallAddFeeConfigVo>()
            {
                Items = new List<InstallAddFeeConfigVo>()
            };
            var installFee = await _baoYangInstallFeeConfigRepository.GetBaoYangInstallFeeConfigPageList(
                new BaoYangInstallFeeConfigPageListCondition()
                {
                    ServiceId = request.ServiceId,
                    GuidePrice = request.GuidePrice,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (installFee != null)
            {
                result.TotalItems = installFee.TotalItems;
                if (installFee.Items != null && installFee.Items.Any())
                {
                    var serviceIds = installFee.Items.Select(_ => _.ServiceId).Distinct().ToList();
                    var services = await _productClient.GetProductsByProductCodes(serviceIds, false);
                    foreach (var itemS in installFee.Items)
                    {
                        var defaultS = services.FirstOrDefault(_ => _.Product.ProductCode == itemS.ServiceId);
                        if (defaultS != null)
                        {
                            result.Items.Add(new InstallAddFeeConfigVo()
                            {
                                Id = itemS.Id,
                                ServiceId = itemS.ServiceId,
                                ServiceName = defaultS.Product.Name,
                                CarMinPrice = itemS.CarMinPrice,
                                CarMaxPrice = itemS.CarMaxPrice,
                                OriginalPrice = defaultS.Product.SalesPrice,
                                AdditionalPrice = itemS.AdditionalPrice,
                                Remark = itemS.Remark,
                                CreateBy = itemS.CreateBy,
                                CreateTime = itemS.CreateTime
                            });
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddInstallAddFeeConfig(AddInstallAddFeeConfigRequest request)
        {
            BaoYangInstallFeeConfigDO insert = new BaoYangInstallFeeConfigDO()
            {
                ServiceId = request.ServiceId,
                CarMinPrice = request.CarMinPrice,
                CarMaxPrice = request.CarMaxPrice,
                AdditionalPrice = request.AdditionalPrice,
                Remark = request.Remark,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now
            };
            var existList = await _baoYangInstallFeeConfigRepository.GetInstallFeeConfigByPid(request.ServiceId, false);
            CheckParaForInstallFee(existList, insert);
            await _baoYangInstallFeeConfigRepository.InsertAsync(insert);
            return true;
        }

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallAddFeeConfig(EditInstallAddFeeConfigRequest request)
        {
            List<string> field = new List<string>();
            BaoYangInstallFeeConfigDO update = new BaoYangInstallFeeConfigDO()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            if (request.IsDeleted.HasValue)
            {
                update.IsDeleted = request.IsDeleted.Value;
                field.Add("IsDeleted");
                field.Add("UpdateBy");
                field.Add("UpdateTime");
                return await _baoYangInstallFeeConfigRepository.UpdateAsync(update, field) > 0;
            }

            if (request.ServiceId != null)
            {
                update.ServiceId = request.ServiceId;
                field.Add("ServiceId");
            }

            if (request.CarMinPrice.HasValue)
            {
                update.CarMinPrice = request.CarMinPrice.Value;
                field.Add("CarMinPrice");
            }

            if (request.CarMaxPrice.HasValue)
            {
                update.CarMaxPrice = request.CarMaxPrice.Value;
                field.Add("CarMaxPrice");
            }

            if (request.AdditionalPrice.HasValue)
            {
                update.AdditionalPrice = request.AdditionalPrice.Value;
                field.Add("AdditionalPrice");
            }

            if (request.Remark != null)
            {
                update.Remark = request.Remark;
                field.Add("Remark");
            }

            if (field.Any())
            {
                field.Add("UpdateBy");
                field.Add("UpdateTime");

                var existList =
                    await _baoYangInstallFeeConfigRepository.GetInstallFeeConfigByPid(request.ServiceId, false);
                CheckParaForInstallFee(existList.Where(_ => _.Id != request.Id).ToList(), update);
                await _baoYangInstallFeeConfigRepository.UpdateAsync(update, field);
            }

            return true;
        }

        private void CheckParaForInstallFee(List<BaoYangInstallFeeConfigDO> exist, BaoYangInstallFeeConfigDO current)
        {
            if (current.AdditionalPrice < 0)
            {
                throw new CustomException("加价金额不能为负数");
            }

            if (current.CarMinPrice < 0 || current.CarMaxPrice < 0)
            {
                throw new CustomException("指导价金额不能为负数");
            }

            if (current.CarMinPrice >= current.CarMaxPrice)
            {
                throw new CustomException("最大指导价必须大于最小指导价");
            }

            if (exist != null && exist.Any())
            {
                exist.ForEach(_ =>
                {
                    if (current.CarMinPrice < _.CarMaxPrice && current.CarMaxPrice > _.CarMinPrice)
                    {
                        throw new CustomException("指导价金额配置有交叉，请再次确认指导价金额");
                    }
                });
            }
        }

        #endregion
    }
}
